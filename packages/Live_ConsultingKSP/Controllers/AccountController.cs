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
using System.Linq;
using System.Globalization;

namespace Live_ConsultingKSP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public AccountController(IMapper mapper)
        {
            _mapper = mapper;
        }



        //public IActionResult Index()
        //{
        //    return View();
        //}
        #region Master Unit Master

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetUnitData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACUnit.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Unit_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Unit_Name.Contains(searchValue) ||
                    i.Unit_ShortName.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "unitname" => query.OrderBy(i => i.Unit_Name),
                    "unitshortname" => query.OrderBy(i => i.Unit_ShortName),
                    "mode" => query.OrderBy(i => i.Mode),
                    "status" => query.OrderBy(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Unit_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "unitname" => query.OrderByDescending(i => i.Unit_Name),
                    "unitshortname" => query.OrderByDescending(i => i.Unit_ShortName),
                    "mode" => query.OrderByDescending(i => i.Mode),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Unit_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/EditUnitMaster/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    unitname = i.Unit_Name,
                    unitshortname = i.Unit_ShortName,
                    mode = i.Mode,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewUnitMaster()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult EditUnitMaster(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcUnit());
            }

            AC_Unit MY = s.ACUnit.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Unit_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewUnitMaster");
            }
            else
            {
                AcUnit MY1 = new AcUnit();
                MY1 = _mapper.Map<AcUnit>(MY);
                return View("AddUnitMaster", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddUnitMaster(string UUID)
        {
            if (UUID == null)
            {
                AcUnit m = new AcUnit();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var unit = s.ACUnit.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Unit_Id).FirstOrDefault();

                if (unit == null)
                {
                    return RedirectToAction("ViewUnitMaster");
                }
                else
                {
                    return View(unit);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddUnitMaster(AcUnit model)
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

                        var duplicateRecord = s.ACUnit.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                (c.Unit_Name == model.Unit_Name || c.Unit_ShortName == model.Unit_ShortName) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_Unit unit = _mapper.Map<AC_Unit>(model);
                            s.ACUnit.Add(unit);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewUnitMaster");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACUnit.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACUnit.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                (c.Unit_Name == model.Unit_Name || c.Unit_ShortName == model.Unit_ShortName) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcUnit>(model));
                        }
                        else
                        {
                            existingRecord.Unit_Name = model.Unit_Name;
                            existingRecord.Unit_ShortName = model.Unit_ShortName;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACUnit.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewUnitMaster");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcUnit>(model));
            }
        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteUnitMaster(string uuid)
        {
            try
            {
                AC_Unit MY = s.ACUnit.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Unit_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACUnit.Update(MY);

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
            return RedirectToAction("ViewUnitMaster");
        }

        #endregion

        #region Mode of Payment

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetModeOfPaymentData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACModeOfPayment.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfPayment_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Title.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "title" => query.OrderBy(i => i.Title),
                    "description" => query.OrderBy(c => c.Description),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.ModeOfPayment_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "title" => query.OrderByDescending(i => i.Title),
                    "description" => query.OrderByDescending(i => i.Description),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.ModeOfPayment_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/EditModeofPayment/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    title = i.Title,
                    description = i.Description,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewModeofPayment()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditModeofPayment(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcModeOfPayment());
            }

            AC_ModeOfPayment MY = s.ACModeOfPayment.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfPayment_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewModeofPayment");
            }
            else
            {
                AcModeOfPayment MY1 = new AcModeOfPayment();
                MY1 = _mapper.Map<AcModeOfPayment>(MY);
                return View("AddModeofPayment", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddModeofPayment(string UUID)
        {
            if (UUID == null)
            {
                AcModeOfPayment m = new AcModeOfPayment();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var modeOfPayment = s.ACModeOfPayment.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfPayment_Id).FirstOrDefault();

                if (modeOfPayment == null)
                {
                    return RedirectToAction("ViewModeofPayment");
                }
                else
                {
                    return View(modeOfPayment);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddModeofPayment(AcModeOfPayment model)
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

                        var duplicateRecord = s.ACModeOfPayment.Get()
                           .FirstOrDefault(c =>
                               c.IsActive == true &&
                               c.Title == model.Title &&
                               c.Master_Company_UUID == model.Master_Company_UUID &&
                               c.Master_Environment_UUID == model.Master_Environment_UUID &&
                               c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_ModeOfPayment modeOfPayment = _mapper.Map<AC_ModeOfPayment>(model);
                            s.ACModeOfPayment.Add(modeOfPayment);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewModeofPayment");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACModeOfPayment.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACModeOfPayment.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                (c.Title == model.Title || c.Description == model.Description) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcModeOfPayment>(model));
                        }
                        else
                        {
                            existingRecord.Title = model.Title;
                            existingRecord.Description = model.Description;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACModeOfPayment.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewModeofPayment");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcModeOfPayment>(model));
            }
        }


        [CheckCookie("UserUUID")]
        public IActionResult DeleteModeofPayment(string uuid)
        {
            try
            {
                AC_ModeOfPayment MY = s.ACModeOfPayment.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfPayment_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACModeOfPayment.Update(MY);

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
            return RedirectToAction("ViewModeofPayment");
        }




        #endregion

        #region Terms Of Payment

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetTermsOfPaymentData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACTermsOfPayment.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TermsOfPayment_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Title.Contains(searchValue) ||
                    i.Description.ToString().Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(c => c.UUID),
                    "title" => query.OrderBy(c => c.Title),
                    "description" => query.OrderBy(c => c.Description),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.TermsOfPayment_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.UUID),
                    "title" => query.OrderByDescending(c => c.Title),
                    "description" => query.OrderByDescending(c => c.Description),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.TermsOfPayment_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/EditTermsofPayment/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    title = i.Title,
                    description = i.Description,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewTermsofPayment()
        {
            return View();
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult EditTermsofPayment(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcTermsOfPayment());
            }

            AC_TermsOfPayment MY = s.ACTermsOfPayment.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TermsOfPayment_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewTermsofPayment");
            }
            else
            {
                AcTermsOfPayment MY1 = new AcTermsOfPayment();
                MY1 = _mapper.Map<AcTermsOfPayment>(MY);
                return View("AddTermsofPayment", MY1);
            }
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddTermsofPayment(string UUID)
        {
            if (UUID == null)
            {
                AcTermsOfPayment m = new AcTermsOfPayment();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var termsOfPayment = s.ACTermsOfPayment.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TermsOfPayment_Id).FirstOrDefault();

                if (termsOfPayment == null)
                {
                    return RedirectToAction("ViewTermsofPayment");
                }
                else
                {
                    return View(termsOfPayment);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddTermsofPayment(AcTermsOfPayment model)
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

                        var duplicateRecord = s.ACTermsOfPayment.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_TermsOfPayment termsOfPayment = _mapper.Map<AC_TermsOfPayment>(model);
                            s.ACTermsOfPayment.Add(termsOfPayment);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewTermsofPayment");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACTermsOfPayment.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACTermsOfPayment.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcTermsOfPayment>(model));
                        }
                        else
                        {
                            existingRecord.Title = model.Title;
                            existingRecord.Description = model.Description;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACTermsOfPayment.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewTermsofPayment");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcTermsOfPayment>(model));
            }
        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteTermsofPayment(string uuid)
        {
            try
            {
                AC_TermsOfPayment MY = s.ACTermsOfPayment.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TermsOfPayment_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACTermsOfPayment.Update(MY);

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
            return RedirectToAction("ViewTermsofPayment");
        }


        #endregion

        #region Mode of Transport

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetModeOfTransportData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACModeOfTransport.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfTransport_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Title.Contains(searchValue) ||
                    i.Description.ToString().Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.UUID),
                    "title" => query.OrderBy(c => c.Title),
                    "description" => query.OrderBy(c => c.Description),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.ModeOfTransport_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.UUID),
                    "title" => query.OrderByDescending(c => c.Title),
                    "description" => query.OrderByDescending(c => c.Description),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.ModeOfTransport_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/EditModeofTransport/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    title = i.Title,
                    description = i.Description,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewModeofTransport()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult EditModeofTransport(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcModeOfTransport());
            }

            AC_ModeOfTransport MY = s.ACModeOfTransport.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfTransport_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewModeofTransport");
            }
            else
            {
                AcModeOfTransport MY1 = new AcModeOfTransport();
                MY1 = _mapper.Map<AcModeOfTransport>(MY);
                return View("AddModeofTransport", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddModeofTransport(string UUID)
        {
            if (UUID == null)
            {
                AcModeOfTransport m = new AcModeOfTransport();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var modeOfTransport = s.ACModeOfTransport.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfTransport_Id).FirstOrDefault();

                if (modeOfTransport == null)
                {
                    return RedirectToAction("ViewModeofTransport");
                }
                else
                {
                    return View(modeOfTransport);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddModeofTransport(AcModeOfTransport model)
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

                        var duplicateRecord = s.ACModeOfTransport.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_ModeOfTransport modeOfTransport = _mapper.Map<AC_ModeOfTransport>(model);
                            s.ACModeOfTransport.Add(modeOfTransport);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewModeofTransport");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACModeOfTransport.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACModeOfTransport.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcModeOfTransport>(model));
                        }
                        else
                        {
                            existingRecord.Title = model.Title;
                            existingRecord.Description = model.Description;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACModeOfTransport.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewModeofTransport");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcModeOfTransport>(model));
            }
        }


        public IActionResult DeleteModeofTransport(string UUID)
        {
            try
            {
                AC_ModeOfTransport MY = s.ACModeOfTransport.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ModeOfTransport_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACModeOfTransport.Update(MY);

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
            return RedirectToAction("ViewModeofTransport");
        }




        #endregion

        #region AllowanceType

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetAllowanceTypeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACAllowanceType.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.AllowanceType_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Title.Contains(searchValue) ||
                    i.Code.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.UUID),
                    "mode" => query.OrderBy(i => i.Mode),
                    "type" => query.OrderBy(i => i.Type),
                    "code" => query.OrderBy(i => i.Code),
                    "title" => query.OrderBy(i => i.Title),
                    "status" => query.OrderBy(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.AllowanceType_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "mode" => query.OrderByDescending(i => i.Mode),
                    "type" => query.OrderByDescending(i => i.Type),
                    "code" => query.OrderByDescending(i => i.Code),
                    "title" => query.OrderByDescending(i => i.Title),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.AllowanceType_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/MasterEditAllowanceType/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    mode = i.Mode,
                    type = i.Type,
                    code = i.Code,
                    title = i.Title,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public IActionResult MasterViewAllowanceType()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditAllowanceType(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcAllowanceType());
            }

            AC_AllowanceType MY = s.ACAllowanceType.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.AllowanceType_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewAllowanceType");
            }
            else
            {
                AcAllowanceType MY1 = new AcAllowanceType();
                MY1 = _mapper.Map<AcAllowanceType>(MY);
                return View("MasterAddAllowanceType", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult MasterAddAllowanceType(string UUID)
        {
            if (UUID == null)
            {
                AcAllowanceType m = new AcAllowanceType();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var allowanceType = s.ACAllowanceType.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.AllowanceType_Id).FirstOrDefault();

                if (allowanceType == null)
                {
                    return RedirectToAction("MasterViewAllowanceType");
                }
                else
                {
                    return View(allowanceType);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddAllowanceType(AcAllowanceType model)
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

                        var duplicateRecord = s.ACAllowanceType.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                (c.Title == model.Title || c.Code == model.Code) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_AllowanceType allowanceType = _mapper.Map<AC_AllowanceType>(model);
                            s.ACAllowanceType.Add(allowanceType);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewAllowanceType");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACAllowanceType.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACAllowanceType.Get()
                           .FirstOrDefault(c =>
                               c.IsActive == true &&
                               (c.Title == model.Title || c.Code == model.Code) &&
                               c.Master_Company_UUID == model.Master_Company_UUID &&
                               c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcAllowanceType>(model));
                        }
                        else
                        {
                            existingRecord.Mode = model.Mode;
                            existingRecord.Type = model.Type;
                            existingRecord.Title = model.Title;
                            existingRecord.Code = model.Code;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACAllowanceType.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewAllowanceType");
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcAllowanceType>(model));
            }

        }



        public IActionResult DeleteAllowanceType(string Uuid)
        {
            try
            {
                AC_AllowanceType MY = s.ACAllowanceType.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.AllowanceType_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACAllowanceType.Update(MY);

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

            return RedirectToAction("MasterViewAllowanceType");
        }


        #endregion

        #region Payment Status

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetpaymentStatusData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACPaymentStatus.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaymentStatus_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                      i.Title.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.UUID),
                    "mode" => query.OrderBy(i => i.Mode),
                    "title" => query.OrderBy(i => i.Title),
                    "status" => query.OrderBy(i => i.IsDisplay),
                    _ => query.OrderBy(a => a.PaymentStatus_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "mode" => query.OrderByDescending(i => i.Mode),
                    "title" => query.OrderByDescending(i => i.Title),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(a => a.PaymentStatus_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/MasterEditPaymentStatus/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    mode = i.Mode,
                    title = i.Title,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }



        [CheckCookie("UserUUID")]
        public IActionResult MasterViewPaymentStatus()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditPaymentStatus(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcPaymentStatus());
            }

            AC_PaymentStatus MY = s.ACPaymentStatus.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaymentStatus_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewPaymentStatus");
            }
            else
            {
                AcPaymentStatus MY1 = new AcPaymentStatus();
                MY1 = _mapper.Map<AcPaymentStatus>(MY);
                return View("MasterAddPaymentStatus", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> MasterAddPaymentStatus(string UUID)
        {
            if (UUID == null)
            {
                AcPaymentStatus m = new AcPaymentStatus();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var paymentStatus = s.ACPaymentStatus.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaymentStatus_Id).FirstOrDefault();

                if (paymentStatus == null)
                {
                    return RedirectToAction("MasterViewPaymentStatus");
                }
                else
                {
                    return View(paymentStatus);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddPaymentStatus(AcPaymentStatus model)
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

                        var duplicateRecord = s.ACPaymentStatus.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            Master_Year paymentStatus = _mapper.Map<Master_Year>(model);
                            s.MasterYear.Add(paymentStatus);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewPaymentStatus");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACPaymentStatus.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACPaymentStatus.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcPaymentStatus>(model));
                        }
                        else
                        {
                            existingRecord.Mode = model.Mode;
                            existingRecord.Title = model.Title;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACPaymentStatus.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewPaymentStatus");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcPaymentStatus>(model));
            }
        }

        [CheckCookie("UserUUID")]
        public IActionResult DeletePaymentStatus(string uuid)
        {
            try
            {
                AC_PaymentStatus MY = s.ACPaymentStatus.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaymentStatus_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACPaymentStatus.Update(MY);

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
            return RedirectToAction("MasterViewPaymentStatus");
        }



        #endregion

        #region FinancialYear

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetFinancialYearData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACFinancialYear.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.FinancialYear_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Title.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(a => a.UUID),
                    "title" => query.OrderBy(a => a.Title),
                    "startdate" => query.OrderBy(a => a.Start_date),
                    "enddate" => query.OrderBy(a => a.End_date),
                    "status" => query.OrderBy(a => a.IsDisplay),
                    _ => query.OrderBy(a => a.FinancialYear_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(a => a.UUID),
                    "title" => query.OrderByDescending(a => a.Title),
                    "startdate" => query.OrderByDescending(a => a.Start_date),
                    "enddate" => query.OrderByDescending(a => a.End_date),
                    "status" => query.OrderByDescending(a => a.IsDisplay),
                    _ => query.OrderByDescending(a => a.FinancialYear_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/MasterEditFinancialYear/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    title = i.Title,
                    startdate = i.Start_date,
                    enddate = i.End_date,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult MasterViewFinancialYear()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditFinancialYear(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcFinancialYear());
            }

            AC_FinancialYear MY = s.ACFinancialYear.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.FinancialYear_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewFinancialYear");
            }
            else
            {
                AcFinancialYear MY1 = new AcFinancialYear();
                MY1 = _mapper.Map<AcFinancialYear>(MY);
                return View("MasterAddFinancialYear", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> MasterAddFinancialYear(string UUID)
        {
            if (UUID == null)
            {
                AcFinancialYear m = new AcFinancialYear();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var financialYear = s.ACFinancialYear.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.FinancialYear_Id).FirstOrDefault();

                if (financialYear == null)
                {
                    return RedirectToAction("MasterViewFinancialYear");
                }
                else
                {
                    return View(financialYear);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddFinancialYear(AcFinancialYear model)
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

                        var duplicateRecord = s.ACFinancialYear.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_FinancialYear financialYear = _mapper.Map<AC_FinancialYear>(model);
                            s.ACFinancialYear.Add(financialYear);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewFinancialYear");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACFinancialYear.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACFinancialYear.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Title == model.Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcFinancialYear>(model));
                        }
                        else
                        {
                            existingRecord.Title = model.Title;
                            existingRecord.Start_date = model.Start_date;
                            existingRecord.End_date = model.End_date;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACFinancialYear.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewFinancialYear");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcFinancialYear>(model));
            }
        }
        [CheckCookie("UserUUID")]
        public IActionResult DeleteFinancialYear(string uuid)
        {
            try
            {
                AC_FinancialYear MY = s.ACFinancialYear.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.FinancialYear_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACFinancialYear.Update(MY);

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

            return RedirectToAction("MasterViewFinancialYear");
        }



        #endregion

        public IActionResult ViewNomenClature()
        {
            return View();
        }
        public IActionResult AddNomenClature()
        {
            return View();
        }
        public IActionResult EditNomenClature()
        {
            return View();
        }

        #region Payment Means Code

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetPaymentMeansCodeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACPaymentMeansCode.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.PaymentMeansCode_Name.Contains(searchValue) ||
                    i.Usage_in_EN16931.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(a => a.UUID),
                    "code" => query.OrderBy(a => a.Code),
                    "name" => query.OrderBy(a => a.PaymentMeansCode_Name),
                    "usage" => query.OrderBy(a => a.Usage_in_EN16931),
                    "status" => query.OrderBy(a => a.IsDisplay),
                    _ => query.OrderBy(a => a.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(a => a.UUID),
                    "code" => query.OrderByDescending(a => a.Code),
                    "name" => query.OrderByDescending(a => a.PaymentMeansCode_Name),
                    "usage" => query.OrderByDescending(a => a.Usage_in_EN16931),
                    "status" => query.OrderByDescending(a => a.IsDisplay),
                    _ => query.OrderByDescending(a => a.Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/EditPaymentMeansCode/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    code = i.Code,
                    name = i.PaymentMeansCode_Name,
                    usage = i.Usage_in_EN16931,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewPaymentMeansCode()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditPaymentMeansCode(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcPaymentMeansCode());
            }

            AC_PaymentMeansCode MY = s.ACPaymentMeansCode.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewPaymentMeansCode");
            }
            else
            {
                AcPaymentMeansCode MY1 = new AcPaymentMeansCode();
                MY1 = _mapper.Map<AcPaymentMeansCode>(MY);
                return View("AddPaymentMeansCode", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddPaymentMeansCode(string UUID)
        {
            if (UUID == null)
            {
                AcPaymentMeansCode m = new AcPaymentMeansCode();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var paymentMeansCode = s.ACPaymentMeansCode.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (paymentMeansCode == null)
                {
                    return RedirectToAction("ViewPaymentMeansCode");
                }
                else
                {
                    return View(paymentMeansCode);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddPaymentMeansCode(AcPaymentMeansCode model)
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

                        var duplicateRecord = s.ACPaymentMeansCode.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.PaymentMeansCode_Name == model.PaymentMeansCode_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_PaymentMeansCode paymentMeansCode = _mapper.Map<AC_PaymentMeansCode>(model);
                            s.ACPaymentMeansCode.Add(paymentMeansCode);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewPaymentMeansCode");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACPaymentMeansCode.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACPaymentMeansCode.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.PaymentMeansCode_Name == model.PaymentMeansCode_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcPaymentMeansCode>(model));
                        }
                        else
                        {
                            existingRecord.PaymentMeansCode_Name = model.PaymentMeansCode_Name;
                            existingRecord.Code = model.Code;
                            existingRecord.Usage_in_EN16931 = model.Usage_in_EN16931;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACPaymentMeansCode.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewPaymentMeansCode");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcPaymentMeansCode>(model));
            }
        }
        [CheckCookie("UserUUID")]
        public IActionResult DeletePaymentMeansCode(string uuid)
        {
            try
            {
                AC_PaymentMeansCode MY = s.ACPaymentMeansCode.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UUID).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACPaymentMeansCode.Update(MY);

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
            return RedirectToAction("ViewPaymentMeansCode");
        }



        #endregion

        #region Language

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetLanguageData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ACLanguage.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Language_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Language_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(a => a.UUID),
                    "name" => query.OrderBy(a => a.Language_Name),
                    "status" => query.OrderBy(a => a.IsDisplay),
                    _ => query.OrderBy(a => a.Language_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(a => a.UUID),
                    "name" => query.OrderByDescending(a => a.Language_Name),
                    "status" => query.OrderByDescending(a => a.IsDisplay),
                    _ => query.OrderByDescending(a => a.Language_Id)
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
                data = data.Select(i => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Account/MasterEditLanguage/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    name = i.Language_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult MasterViewLanguage()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditLanguage(string UUID)
        {
            if (UUID == null)
            {
                return View(new AcLanguage());
            }

            AC_Language MY = s.ACLanguage.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Language_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewLanguage");
            }
            else
            {
                AcLanguage MY1 = new AcLanguage();
                MY1 = _mapper.Map<AcLanguage>(MY);
                return View("MasterAddLanguage", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> MasterAddLanguage(string UUID)
        {
            if (UUID == null)
            {
                AcLanguage m = new AcLanguage();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var language = s.ACLanguage.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Language_Id).FirstOrDefault();

                if (language == null)
                {
                    return RedirectToAction("MasterViewLanguage");
                }
                else
                {
                    return View(language);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddLanguage(AcLanguage model)
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

                        var duplicateRecord = s.ACLanguage.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Language_Name == model.Language_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();

                            AC_Language language = _mapper.Map<AC_Language>(model);
                            s.ACLanguage.Add(language);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewLanguage");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ACLanguage.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ACLanguage.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Language_Name == model.Language_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<AcLanguage>(model));
                        }
                        else
                        {
                            existingRecord.Language_Name = model.Language_Name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ACLanguage.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewLanguage");
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<AcLanguage>(model));
            }

        }
        [CheckCookie("UserUUID")]
        public IActionResult DeleteLanguage(string uuid)
        {
            try
            {
                AC_Language MY = s.ACLanguage.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UUID).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ACLanguage.Update(MY);

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
            return RedirectToAction("MasterViewLanguage");
        }



        #endregion

        public IActionResult ViewSalesCustomerInquiry()
        {
            return View();
        }
        public IActionResult AddSalesCustomerInquiry()
        {
            return View();
        }

        public IActionResult ViewSalesOrder()
        {
            return View();
        }
        public IActionResult AddSalesOrder()
        {
            return View();
        }
        public IActionResult PDF_sales_order()
        {
            return View();
        }

        public IActionResult ViewSalePerfomaInvoice()
        {
            return View();
        }
        public IActionResult AddSalesPerfomaInvoice()
        {
            return View();
        }
        public IActionResult ViewSaleInvoice()
        {
            return View();
        }
        public IActionResult PDF_packing_slip()
        {
            return View();
        }

        public IActionResult AddSaleInvoice()
        {
            return View();
        }
        public IActionResult ViewPurchaseInquiry()
        {
            return View();
        }
        public IActionResult AddPurchaseInquiry()
        {
            return View();
        }
        public IActionResult ViewPurchaseQuotation()
        {
            return View();
        }
        public IActionResult AddPurchaseQuotation()
        {
            return View();
        }
        public IActionResult ViewPurchaseOrder()
        {
            return View();
        }
        public IActionResult AddPurchaseOrder()
        {
            return View();
        }
        public IActionResult ViewPurchasePerformaInvoice()
        {
            return View();
        }
        public IActionResult AddPurchasePerformaInvoice()
        {
            return View();
        }
        public IActionResult ViewPurchaseInvoice()
        {
            return View();
        }
        public IActionResult AddPurchaseInvoice()
        {
            return View();
        }
        public IActionResult ViewTaxGroup()
        {
            return View();
        }
        public IActionResult AddTaxGroup()
        {
            return View();
        }
        public IActionResult ViewTaxCode()
        {
            return View();
        }
        public IActionResult AddTaxCode()
        {
            return View();
        }
        public IActionResult ViewTaxData()
        {
            return View();
        }
        public IActionResult AddTaxData()
        {
            return View();
        }
        public IActionResult ViewCustomer()
        {
            return View();
        }
        public IActionResult AddCustomer()
        {
            return View();
        }

        public IActionResult ViewVendor()
        {
            return View();
        }
        public IActionResult AddVendor()
        {
            return View();
        }

        public IActionResult ViewItemGroup()
        {
            return View();
        }
        public IActionResult AddItemGroup()
        {
            return View();
        }

        public IActionResult ViewItemMaster()
        {
            return View();
        }
        public IActionResult AddItemMaster()
        {
            return View();
        }

        public IActionResult ViewInvoiceType()
        {
            return View();
        }
        public IActionResult AddInvoiceType()
        {
            return View();
        }

        public IActionResult ViewInvoiceSubType()
        {
            return View();
        }
        public IActionResult AddInvoiceSubType()
        {
            return View();
        }
        public IActionResult ViewInvoiceTypeCode()
        {
            return View();
        }
        public IActionResult AddInvoiceTypeCode()
        {
            return View();
        }
        public IActionResult PDF_commercial_invoice()
        {
            return View();
        }
        public IActionResult PDF_credit_note()
        {
            return View();
        }
        public IActionResult PDF_debit_note()
        {
            return View();
        }
        public IActionResult PDF_delievery_note()
        {
            return View();
        }
        public IActionResult PDF_journal_voucher()
        {
            return View();
        }
        public IActionResult PDF_material_transfer_voucher()
        {
            return View();
        }
        public IActionResult PDF_payment_vaoucher_template()
        {
            return View();
        }
        public IActionResult PDF_proforma_invoice()
        {
            return View();
        }
        public IActionResult PDF_purchase_order()
        {
            return View();
        }
        public IActionResult PDF_sales_day_book()
        {
            return View();
        }
        public IActionResult PDF_SteelBear_Purchase_Order()
        {
            return View();
        }




    }
}
