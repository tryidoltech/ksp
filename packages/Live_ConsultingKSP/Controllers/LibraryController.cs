
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
    public class LibraryController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public LibraryController(IMapper mapper)
        {
            _mapper = mapper;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        #region Library Category

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetCategoryData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterCategory.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() 
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Category_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Category_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "categoryname" => query.OrderBy(i => i.Category_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Category_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "categoryname" => query.OrderByDescending(i => i.Category_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Category_Id)
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
                    uuid = $"<a href='/Library/EditLibraryCategory/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    categoryname = i.Category_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewLibraryCategory()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditLibraryCategory(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterCategory());
            }

            Master_Category MY = s.MasterCategory.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() 
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Category_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewLibraryCategory");
            }
            else
            {
                MasterCategory MY1 = new MasterCategory();
                MY1 = _mapper.Map<MasterCategory>(MY);
                return View("AddLibraryCategory", MY1);
            }

        }
        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddLibraryCategory(string UUID)
        {
            if (UUID == null)
            {
                MasterCategory m = new MasterCategory();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var year = s.MasterCategory.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() 
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Category_Id).FirstOrDefault();

                if (year == null)
                {
                    return RedirectToAction("ViewLibraryCategory");
                }
                else
                {
                    return View(year);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddLibraryCategory(MasterCategory model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }
                if (string.IsNullOrEmpty(model.UUID))
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                    model.IsActive = true;
                    model.AddedIP = u.GetLocalIPAddress();
                    model.IsAddedOn = u.CurrentIndianTime();
                    model.RecordNo = u.GetRecordNo();
                    model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                    model.UUID = u.GetUUID();
                    var year = s.MasterCategory.Get().Where(c => c.IsActive == true && c.Category_Name == model.Category_Name 
                    && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Category_Id).FirstOrDefault();


                    if (year != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(model);
                    }
                    Master_Category MY = new Master_Category();
                    MY = _mapper.Map<Master_Category>(model);
                    s.MasterCategory.Add(MY);

                    TempData["Message"] = "Data Inserted Successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("ViewLibraryCategory");
                }
                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();

                    var year = s.MasterCategory.Get().FirstOrDefault(c => c.UUID == model.UUID);
                    if (year == null)
                    {
                        TempData["Message"] = "Record not found!";
                        TempData["MessageType"] = "danger";
                        return RedirectToAction("MasterViewYear");
                    }

                    var isDuplicate = s.MasterCategory.Get()
                .FirstOrDefault(c => c.IsActive == true &&
                                     c.Category_Name == model.Category_Name &&
                                     c.Master_Company_UUID == model.Master_Company_UUID &&
                                     c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                     c.UUID != model.UUID);

                    if (isDuplicate != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(_mapper.Map<MasterYear>(model));
                    }

                    year.Category_Name = model.Category_Name;
                    year.IsDisplay = model.IsDisplay;
                    year.IsUpdatedOn = u.CurrentIndianTime();
                    year.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                    year.UpdatedIP = u.GetLocalIPAddress();


                    s.MasterCategory.Update(year);
                    TempData["Message"] = "Data Updated Successfully!";
                    TempData["MessageType"] = "success";

                    return RedirectToAction("ViewLibraryCategory");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterCategory>(model));
            }

        }
        [CheckCookie("UserUUID")]
        public IActionResult DeleteLibraryCategory(string uuid)
        {
            try
            {
                Master_Category MY = s.MasterCategory.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Category_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    s.MasterCategory.Update(MY);

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
            return RedirectToAction("ViewLibraryCategory");
        }


        #endregion


        #region Library Category Tag

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetCategoryTagData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterCategorTag.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CategoryTag_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Category_Tag_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "categorytagname" => query.OrderBy(i => i.Category_Tag_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.CategoryTag_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "categorytagname" => query.OrderByDescending(i => i.Category_Tag_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.CategoryTag_Id)
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
                    uuid = $"<a href='/Library/EditLibraryCategoryTag/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    categorytagname = i.Category_Tag_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewLibraryCategoryTag()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditLibraryCategoryTag(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterCategorTag());
            }

            Master_CategorTag MY = s.MasterCategorTag.Get().Where(c => c.IsActive == true && c.UUID == UUID &&
            c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CategoryTag_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewLibraryCategoryTag");
            }
            else
            {
                MasterCategorTag MY1 = new MasterCategorTag();
                MY1 = _mapper.Map<MasterCategorTag>(MY);
                return View("AddLibraryCategoryTag", MY1);
            }

        }
        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddLibraryCategoryTag(string UUID)
        {
            if (UUID == null)
            {
                MasterCategorTag m = new MasterCategorTag();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var year = s.MasterCategorTag.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CategoryTag_Id).FirstOrDefault();

                if (year == null)
                {
                    return RedirectToAction("ViewLibraryCategoryTag");
                }
                else
                {
                    return View(year);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddLibraryCategoryTag(MasterCategorTag model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }
                if (string.IsNullOrEmpty(model.UUID))
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                    model.IsActive = true;
                    model.AddedIP = u.GetLocalIPAddress();
                    model.RecordNo = u.GetRecordNo();
                    model.IsAddedOn = u.CurrentIndianTime();
                    model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                    model.UUID = u.GetUUID();
                    var year = s.MasterCategorTag.Get().Where(c => c.IsActive == true && c.Category_Tag_Name == model.Category_Tag_Name && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                    c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CategoryTag_Id).FirstOrDefault();


                    if (year != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(model);
                    }
                    Master_CategorTag MY = new Master_CategorTag();
                    MY = _mapper.Map<Master_CategorTag>(model);
                    s.MasterCategorTag.Add(MY);

                    TempData["Message"] = "Data Inserted Successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("ViewLibraryCategoryTag");
                }
                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();

                    var year = s.MasterCategorTag.Get().FirstOrDefault(c => c.UUID == model.UUID);
                    if (year == null)
                    {
                        TempData["Message"] = "Record not found!";
                        TempData["MessageType"] = "danger";
                        return RedirectToAction("ViewLibraryCategoryTag");
                    }

                    var isDuplicate = s.MasterCategorTag.Get()
                .FirstOrDefault(c => c.IsActive == true &&
                                     c.Category_Tag_Name == model.Category_Tag_Name &&
                                     c.Master_Company_UUID == model.Master_Company_UUID &&
                                     c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                     c.UUID != model.UUID);

                    if (isDuplicate != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(_mapper.Map<MasterCategorTag>(model));
                    }

                    year.Category_Tag_Name = model.Category_Tag_Name;
                    year.IsDisplay = model.IsDisplay;
                    year.IsUpdatedOn = u.CurrentIndianTime();
                    year.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                    year.UpdatedIP = u.GetLocalIPAddress();


                    s.MasterCategorTag.Update(year);
                    TempData["Message"] = "Data Updated Successfully!";
                    TempData["MessageType"] = "success";

                    return RedirectToAction("ViewLibraryCategoryTag");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterCategorTag>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteLibraryCategoryTag(string uuid)
        {
            try
            {
                Master_CategorTag MY = s.MasterCategorTag.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CategoryTag_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    s.MasterCategorTag.Update(MY);

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
            return RedirectToAction("ViewLibraryCategoryTag");
        }

        #endregion

        #region Document Category

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetDocumentCategoryData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterDocumentCategory.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Document_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.DocumentCategory_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "documentname" => query.OrderBy(i => i.DocumentCategory_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Document_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "documentname" => query.OrderByDescending(i => i.DocumentCategory_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Document_Id)
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
                    uuid = $"<a href='/Library/EditDocumentCategory/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    documentname = i.DocumentCategory_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }

        
        [CheckCookie("UserUUID")]
        public IActionResult ViewDocumentCategory()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult EditDocumentCategory(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterDocumentCategory());
            }

            Master_DocumentCategory MY = s.MasterDocumentCategory.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Document_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewDocumentCategory");
            }
            else
            {
                MasterDocumentCategory MY1 = new MasterDocumentCategory();
                MY1 = _mapper.Map<MasterDocumentCategory>(MY);
                return View("AddDocumentCategory", MY1);
            }
        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddDocumentCategory(string Uuid)
        {
            if (Uuid == null)
            {
                MasterDocumentCategory m = new MasterDocumentCategory();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var year = s.MasterDocumentCategory.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Document_Id).FirstOrDefault();

                if (year == null)
                {
                    return RedirectToAction("ViewDocumentCategory");
                }
                else
                {
                    return View(year);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddDocumentCategory(MasterDocumentCategory model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }
                if (string.IsNullOrEmpty(model.UUID))
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                    model.IsActive = true;
                    model.AddedIP = u.GetLocalIPAddress();
                    model.IsAddedOn = u.CurrentIndianTime();
                    model.RecordNo = u.GetRecordNo();
                    model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                    model.UUID = u.GetUUID();
                    var year = s.MasterDocumentCategory.Get().Where(c => c.IsActive == true && c.DocumentCategory_Name == model.DocumentCategory_Name && 
                    c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Document_Id).FirstOrDefault();


                    if (year != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(model);
                    }
                    Master_DocumentCategory MY = new Master_DocumentCategory();
                    MY = _mapper.Map<Master_DocumentCategory>(model);
                    s.MasterDocumentCategory.Add(MY);

                    TempData["Message"] = "Data Inserted Successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("ViewDocumentCategory");
                }
                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();

                    var year = s.MasterDocumentCategory.Get().FirstOrDefault(c => c.UUID == model.UUID);
                    if (year == null)
                    {
                        TempData["Message"] = "Record not found!";
                        TempData["MessageType"] = "danger";
                        return RedirectToAction("MasterViewYear");
                    }

                    var isDuplicate = s.MasterDocumentCategory.Get()
                .FirstOrDefault(c => c.IsActive == true &&
                                     c.DocumentCategory_Name == model.DocumentCategory_Name &&
                                     c.Master_Company_UUID == model.Master_Company_UUID &&
                                     c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                     c.UUID != model.UUID);

                    if (isDuplicate != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(_mapper.Map<MasterYear>(model));
                    }

                    year.DocumentCategory_Name = model.DocumentCategory_Name;
                    year.IsDisplay = model.IsDisplay;
                    year.IsUpdatedOn = u.CurrentIndianTime();
                    year.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                    year.UpdatedIP = u.GetLocalIPAddress();


                    s.MasterDocumentCategory.Update(year);
                    TempData["Message"] = "Data Updated Successfully!";
                    TempData["MessageType"] = "success";

                    return RedirectToAction("ViewDocumentCategory");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterDocumentCategory>(model));
            }
        }


        [HttpPost]
        public IActionResult DeleteDocumentCategory(string uuid)
        {
            try
            {
                Master_DocumentCategory MY = s.MasterDocumentCategory.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Document_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterDocumentCategory.Update(MY);

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
            return RedirectToAction("ViewDocumentCategory");
        }

        #endregion

        #region Document Category Tag

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetDocumentCategoryTagData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterDocumentCategoryTag.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.DocumentCategory_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.DocumentCategoryTag.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "documentname" => query.OrderBy(i => i.DocumentCategoryTag),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.DocumentCategory_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "documentname" => query.OrderByDescending(i => i.DocumentCategoryTag),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.DocumentCategory_Id)
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
                    uuid = $"<a href='/Library/EditDocumentCategoryTag/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    documentname = i.DocumentCategoryTag,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewDocumentCategoryTag()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditDocumentCategoryTag(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterDocumentCategoryTag());
            }

            Master_DocumentCategoryTag MY = s.MasterDocumentCategoryTag.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.DocumentCategory_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewDocumentCategoryTag");
            }
            else
            {
                MasterDocumentCategoryTag MY1 = new MasterDocumentCategoryTag();
                MY1 = _mapper.Map<MasterDocumentCategoryTag>(MY);
                return View("AddDocumentCategoryTag", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddDocumentCategoryTag(string UUID)
        {
            if (UUID == null)
            {
                MasterDocumentCategoryTag m = new MasterDocumentCategoryTag();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var year = s.MasterDocumentCategoryTag.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.DocumentCategory_Id).FirstOrDefault();

                if (year == null)
                {
                    return RedirectToAction("ViewDocumentCategoryTag");
                }
                else
                {
                    return View(year);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddDocumentCategoryTag(MasterDocumentCategoryTag model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);

                }
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
                    var year = s.MasterDocumentCategoryTag.Get().Where(c => c.IsActive == true && c.DocumentCategoryTag == model.DocumentCategoryTag && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
                    c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.DocumentCategory_Id).FirstOrDefault();


                    if (year != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(model);
                    }
                    Master_DocumentCategoryTag MY = new Master_DocumentCategoryTag();
                    MY = _mapper.Map<Master_DocumentCategoryTag>(model);
                    s.MasterDocumentCategoryTag.Add(MY);

                    TempData["Message"] = "Data Inserted Successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("ViewDocumentCategoryTag");
                }
                else
                {
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();

                    var year = s.MasterDocumentCategoryTag.Get().FirstOrDefault(c => c.UUID == model.UUID);
                    if (year == null)
                    {
                        TempData["Message"] = "Record not found!";
                        TempData["MessageType"] = "danger";
                        return RedirectToAction("MasterViewYear");
                    }

                    var isDuplicate = s.MasterDocumentCategoryTag.Get()
                .FirstOrDefault(c => c.IsActive == true &&
                                     c.DocumentCategoryTag == model.DocumentCategoryTag &&
                                     c.Master_Company_UUID == model.Master_Company_UUID &&
                                     c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                     c.UUID != model.UUID);

                    if (isDuplicate != null)
                    {
                        TempData["Message"] = "Data already exists!";
                        TempData["MessageType"] = "danger";
                        return View(_mapper.Map<MasterDocumentCategoryTag>(model));
                    }

                    year.DocumentCategoryTag = model.DocumentCategoryTag;
                    year.IsDisplay = model.IsDisplay;
                    year.IsUpdatedOn = u.CurrentIndianTime();
                    year.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                    year.UpdatedIP = u.GetLocalIPAddress();


                    s.MasterDocumentCategoryTag.Update(year);
                    TempData["Message"] = "Data Updated Successfully!";
                    TempData["MessageType"] = "success";

                    return RedirectToAction("ViewDocumentCategoryTag");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterDocumentCategoryTag>(model));
            }
        }

        [HttpPost]
        public IActionResult DeleteDocumentCategoryTag(string uuid)
        {
            try
            {
                Master_DocumentCategoryTag MY = s.MasterDocumentCategoryTag.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && 
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.DocumentCategory_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterDocumentCategoryTag.Update(MY);

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
            return RedirectToAction("ViewDocumentCategoryTag");
        }
        #endregion



        public IActionResult ViewDocument()
        {
            return View();
        }
        public IActionResult AddDocument()
        {
            return View();
        }
        public IActionResult EditDocument()
        {
            return View();
        }
        public IActionResult DocumentTreeView()
        {
            return View();
        }
        public IActionResult ManageLibrarySearch()
        {
            return View();
        }
        public IActionResult ViewRenewableDocumentsListReminder()
        {
            return View();
        }
        public IActionResult ViewRenewableDocumentHistory()
        {
            return View();
        }

        public IActionResult ManageRenewableDocument()
        {
            return View();
        }










    }
}
