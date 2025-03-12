using Live_ConsultingKSP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using KSPLogin.Models;
using System.Net.Sockets;
using System.Net;
using DataAccess;
using DataAccess.Entities;
using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Live_ConsultingKSP.Controllers
{
    public class BusinessDevelopmentController : Controller
    {

        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public BusinessDevelopmentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Research channel Type

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetResearchChannelData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.BDResearchChannelType.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchChannel_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.ResearchChannel_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "researchchannel" => query.OrderBy(i => i.ResearchChannel_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.ResearchChannel_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "researchchannel" => query.OrderByDescending(i => i.ResearchChannel_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.ResearchChannel_Id)
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.IsActive == true)
             .ToList();
            var srNo = start + 1;

            return Json(new
            {
                draw = draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(c => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/BusinessDevelopment/EditResearchChannelType/{c.UUID}' class='btnEdit'target='_blank'>{c.UUID}</a>",

                    researchchannel = c.ResearchChannel_Name,
                    status = (bool)c.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + c.UUID + "'>Delete</button>"
                })
            });
        }
        [CheckCookie("UserUUID")]
        public IActionResult ViewResearchChannelType()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditResearchChannelType(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new BdResearchChannelType());
            }

            BD_ResearchChannelType MY = s.BDResearchChannelType.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID ==
            Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].
            ToString()).OrderByDescending(c => c.ResearchChannel_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewResearchChannelType");
            }
            else
            {
                BdResearchChannelType MY1 = new BdResearchChannelType();
                MY1 = _mapper.Map<BdResearchChannelType>(MY);
                return View("AddResearchChannelType", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddResearchChannelType(string Uuid)
        {
            if (Uuid == null)
            {
                BdResearchChannelType m = new BdResearchChannelType();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var channeltype = s.BDResearchChannelType.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID ==
                Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .OrderByDescending(c => c.ResearchChannel_Id).FirstOrDefault();

                if (channeltype == null)
                {
                    return RedirectToAction("ViewResearchChannelType");
                }
                else
                {
                    return View(channeltype);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddResearchChannelType(BdResearchChannelType model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }
                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                    if (string.IsNullOrEmpty(model.UUID))
                    {
                        model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                        model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                        model.IsActive = true;
                        model.AddedIP = u.GetLocalIPAddress();
                        model.IsAddedOn = u.CurrentIndianTime();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.RecordNo = u.GetRecordNo();
                        model.UUID = u.GetUUID();
                        var channeltype = s.BDResearchChannelType.Get().Where(c => c.IsActive == true && c.ResearchChannel_Name == model.ResearchChannel_Name && c.Master_Company_UUID == Request
                        .Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchChannel_Id).FirstOrDefault();


                        if (channeltype != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            BD_ResearchChannelType MY = new BD_ResearchChannelType();
                            MY = _mapper.Map<BD_ResearchChannelType>(model);
                            s.BDResearchChannelType.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewResearchChannelType");
                        }
                    }
                    else
                    {


                        var channeltype = s.BDResearchChannelType.Get().FirstOrDefault(c => c.UUID == model.UUID);
                     

                        var isDuplicate = s.BDResearchChannelType.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.ResearchChannel_Name == model.ResearchChannel_Name &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<BdResearchChannelType>(model));
                        }

                        else
                        {
                            channeltype.ResearchChannel_Name = model.ResearchChannel_Name;
                            channeltype.IsDisplay = model.IsDisplay;
                            channeltype.IsUpdatedOn = u.CurrentIndianTime();
                            channeltype.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            channeltype.UpdatedIP = u.GetLocalIPAddress();


                            s.BDResearchChannelType.Update(channeltype);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewResearchChannelType");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<BdResearchChannelType>(model));
            }
        }
        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult DeleteResearchChannelType(string uuid)
        {
            try
            {
                BD_ResearchChannelType MY = s.BDResearchChannelType.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchChannel_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.BDResearchChannelType.Update(MY);

                    TempData["Message"] = "Data Deleted Successfully!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Data Not Found!";
                    TempData["MessageType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("ViewResearchChannelType");
        }
        #endregion

        #region Research Audience

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetResearchAudienceData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.BDResearchAudience.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchAudience_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.ResearchAudience_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "researchaudience" => query.OrderBy(i => i.ResearchAudience_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.ResearchAudience_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "researchaudience" => query.OrderByDescending(i => i.ResearchAudience_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.ResearchAudience_Id)
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.IsActive == true)
             .ToList();
            var srNo = start + 1;


            return Json(new
            {
                draw = draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(a => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/BusinessDevelopment/EditResearchAudience/{a.UUID}' class='btnEdit' target='_blank'>{a.UUID}</a>",
                    researchaudience = a.ResearchAudience_Name,
                    status = (bool)a.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + a.UUID + "'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewResearchAudience()
        {
            return View();
        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditResearchAudience(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new BdResearchAudience());
            }

            BD_ResearchAudience MY = s.BDResearchAudience.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
            .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchAudience_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewResearchAudience");
            }
            else
            {
                BdResearchAudience MY1 = new BdResearchAudience();
                MY1 = _mapper.Map<BdResearchAudience>(MY);
                return View("AddResearchAudience", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddResearchAudience(string Uuid)
        {
            if (Uuid == null)
            {
                BdResearchAudience m = new BdResearchAudience();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var reseachaudience = s.BDResearchAudience.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
                .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchAudience_Id).FirstOrDefault();

                if (reseachaudience == null)
                {
                    return RedirectToAction("ViewResearchAudience");
                }
                else
                {
                    return View(reseachaudience);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddResearchAudience(BdResearchAudience model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }

                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();

                    if (string.IsNullOrEmpty(model.UUID))
                    {
                        model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                        model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                        model.IsActive = true;
                        model.AddedIP = u.GetLocalIPAddress();
                        model.IsAddedOn = u.CurrentIndianTime();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.RecordNo = u.GetRecordNo();
                        model.UUID = u.GetUUID();
                        var reseachaudience = s.BDResearchAudience.Get().Where(c => c.IsActive == true && c.ResearchAudience_Name == model.ResearchAudience_Name && c.Master_Company_UUID
                        == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchAudience_Id).FirstOrDefault();


                        if (reseachaudience != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            BD_ResearchAudience MY = new BD_ResearchAudience();
                            MY = _mapper.Map<BD_ResearchAudience>(model);
                            s.BDResearchAudience.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewResearchAudience");
                        }
                    }
                    else
                    {

                        var reseachaudience = s.BDResearchAudience.Get()
                        .FirstOrDefault(c => c.UUID == model.UUID);
                      



                        var isDuplicate = s.BDResearchAudience.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.ResearchAudience_Name == model.ResearchAudience_Name &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<BdResearchAudience>(model));
                        }

                        else
                        {
                            reseachaudience.ResearchAudience_Name = model.ResearchAudience_Name;
                            reseachaudience.IsDisplay = model.IsDisplay;
                            reseachaudience.IsUpdatedOn = u.CurrentIndianTime();
                            reseachaudience.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            reseachaudience.UpdatedIP = u.GetLocalIPAddress();


                            s.BDResearchAudience.Update(reseachaudience);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewResearchAudience");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<BdResearchAudience>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteResearchAudience(string uuid)
        {
            try
            {
                BD_ResearchAudience MY = s.BDResearchAudience.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchAudience_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.BDResearchAudience.Update(MY);

                    TempData["Message"] = "Data Deleted Successfully!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Data Not Found!";
                    TempData["MessageType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("ViewResearchAudience");
        }
        #endregion

        #region Research Communication Mode

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetResearchCommunicationModeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.BDResearchCommunicationMode.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ResearchCommunicationMode_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.ResearchCommunicationMode_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "researchcommunicationmode" => query.OrderBy(i => i.ResearchCommunicationMode_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.ResearchCommunicationMode_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "researchcommunicationmode" => query.OrderByDescending(i => i.ResearchCommunicationMode_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.ResearchCommunicationMode_Id)
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.IsActive == true)
             .ToList();
            var srNo = start + 1;

            return Json(new
            {
                draw = draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(c => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/BusinessDevelopment/EditResearchCommunicationMode/{c.UUID}' class='btnEdit' target='_blank'>{c.UUID}</a>",
                    researchcommunicationmode = c.ResearchCommunicationMode_Name,
                    status = (bool)c.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + c.UUID + "'>Delete</button>"
                })
            });
        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewResearchCommunicationMode()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditResearchCommunicationMode(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new BdResearchCommunicationMode());
            }

            BD_ResearchCommunicationMode MY = s.BDResearchCommunicationMode.Get().Where(c => c.IsActive == true && c.UUID == Uuid &&
            c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                .OrderByDescending(c => c.ResearchCommunicationMode_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewResearchCommunicationMode");
            }
            else
            {
                BdResearchCommunicationMode MY1 = new BdResearchCommunicationMode();
                MY1 = _mapper.Map<BdResearchCommunicationMode>(MY);
                return View("AddResearchCommunicationMode", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddResearchCommunicationMode(string Uuid)
        {
            if (Uuid == null)
            {
                BdResearchCommunicationMode m = new BdResearchCommunicationMode();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var communication = s.BDResearchCommunicationMode.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID
                == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .OrderByDescending(c => c.ResearchCommunicationMode_Id).FirstOrDefault();

                if (communication == null)
                {
                    return RedirectToAction("ViewResearchCommunicationMode");
                }
                else
                {
                    return View(communication);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddResearchCommunicationMode(BdResearchCommunicationMode model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }
                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();

                    if (string.IsNullOrEmpty(model.UUID))
                    {
                        model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                        model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                        model.IsActive = true;
                        model.AddedIP = u.GetLocalIPAddress();
                        model.IsAddedOn = u.CurrentIndianTime();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.RecordNo = u.GetRecordNo();
                        model.UUID = u.GetUUID();

                        var communication = s.BDResearchCommunicationMode.Get().Where(c => c.IsActive == true && c.ResearchCommunicationMode_Name ==
                        model.ResearchCommunicationMode_Name && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                        c.Master_Environment_UUID == Request.Cookies["EnvUUID"]
                        .ToString()).OrderByDescending(c => c.ResearchCommunicationMode_Id).FirstOrDefault();


                        if (communication != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            BD_ResearchCommunicationMode MY = new BD_ResearchCommunicationMode();
                            MY = _mapper.Map<BD_ResearchCommunicationMode>(model);
                            s.BDResearchCommunicationMode.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewResearchCommunicationMode");
                        }
                    }
                    else
                    {


                        var communication = s.BDResearchCommunicationMode.Get().FirstOrDefault(c => c.UUID == model.UUID);
                     

                        var isDuplicate = s.BDResearchCommunicationMode.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.ResearchCommunicationMode_Name == model.ResearchCommunicationMode_Name &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<BdResearchCommunicationMode>(model));
                        }

                        else
                        {
                            communication.ResearchCommunicationMode_Name = model.ResearchCommunicationMode_Name;
                            communication.IsDisplay = model.IsDisplay;
                            communication.IsUpdatedOn = u.CurrentIndianTime();
                            communication.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            communication.UpdatedIP = u.GetLocalIPAddress();


                            s.BDResearchCommunicationMode.Update(communication);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewResearchCommunicationMode");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<BdResearchCommunicationMode>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteResearchCommunicationMode(string uuid)
        {
            try
            {
                BD_ResearchCommunicationMode MY = s.BDResearchCommunicationMode.Get().Where(c => c.IsActive == true && c.UUID == uuid &&
                c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"]
                .ToString()).OrderByDescending(c => c.ResearchCommunicationMode_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.BDResearchCommunicationMode.Update(MY);

                    TempData["Message"] = "Data Deleted Successfully!";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Data Not Found!";
                    TempData["MessageType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("ViewResearchCommunicationMode");
        }


        #endregion

        #region History Type

        public IActionResult ViewHistoryType()
        {
            return View();
        }
        public IActionResult AddHistoryType()
        {
            return View();
        }

        #endregion
        public IActionResult ViewHistoryTypeStatus()
        {
            return View();
        }
        public IActionResult AddHistoryTypeStatus()
        {
            return View();
        }
        public IActionResult EditHistoryTypeStatus()
        {
            return View();
        }
        public IActionResult ViewResearchGroup()
        {
            return View();
        }
        public IActionResult AddResearchGroup()
        {
            return View();
        }
        public IActionResult EditResearchGroup()
        {
            return View();
        }
        public IActionResult ViewResearchGrpData()
        {
            return View();
        }
        public IActionResult AddMiningData()
        {
            return View();
        }

        public IActionResult ViewDataFilteration()
        {
            return View();
        }
        public IActionResult ManageFilterationData()
        {
            return View();
        }



        public IActionResult ManagePositiveLead()
        {
            return View();
        }
        public IActionResult UpdatePositiveData()
        {
            return View();
        }

    }
}
