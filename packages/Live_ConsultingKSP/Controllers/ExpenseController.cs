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
using System.Data.Entity;

namespace Live_ConsultingKSP.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public ExpenseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region ViewHeadDesignation

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetHeadDesignationData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ERHeadDesignation.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.HeadDesignation_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Designation_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "designationname" => query.OrderBy(i => i.Designation_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.HeadDesignation_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "designationname" => query.OrderByDescending(i => i.Designation_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.HeadDesignation_Id)
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.IsActive == true)
             .ToList();
            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(a => new
                {
                    srno = srNo++,

                    uuid = $"<a href='/Expense/EditHeadDesignation/{a.UUID}' class='btnEdit'target='_blank'>{a.UUID}</a>",
                    designationname = a.Designation_Name,
                    status = a.IsDisplay == true
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{a.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewHeadDesignation()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditHeadDesignation(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new ErHeadDesignation());
            }

            ER_HeadDesignation MY = s.ERHeadDesignation.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies
            ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.HeadDesignation_Id)
            .FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewHeadDesignation");
            }
            else
            {
                ErHeadDesignation MY1 = new ErHeadDesignation();
                MY1 = _mapper.Map<ErHeadDesignation>(MY);
                return View("AddHeadDesignation", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddHeadDesignation(string Uuid)
        {
            if (Uuid == null)
            {
                ErHeadDesignation m = new ErHeadDesignation();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var designation = s.ERHeadDesignation.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending
                (c => c.HeadDesignation_Id).FirstOrDefault();

                if (designation == null)
                {
                    return RedirectToAction("ViewHeadDesignation");
                }
                else
                {
                    return View(designation);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddHeadDesignation(ErHeadDesignation model)
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
                        model.RecordNo = u.GetRecordNo();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.UUID = u.GetUUID();
                        var designation = s.ERHeadDesignation.Get().Where(c => c.IsActive == true && c.Designation_Name == model.Designation_Name &&
                        c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"]
                        .ToString()).OrderByDescending(c => c.HeadDesignation_Id).FirstOrDefault();


                        if (designation != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            ER_HeadDesignation MY = new ER_HeadDesignation();
                            MY = _mapper.Map<ER_HeadDesignation>(model);
                            s.ERHeadDesignation.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewHeadDesignation");
                        }
                    }
                    else
                    {


                        var designation = s.ERHeadDesignation.Get().FirstOrDefault(c => c.UUID == model.UUID);
                      

                        var isDuplicate = s.ERHeadDesignation.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.Designation_Name == model.Designation_Name &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ErHeadDesignation>(model));
                        }

                        else
                        {
                            designation.Designation_Name = model.Designation_Name;
                            designation.IsDisplay = model.IsDisplay;
                            designation.IsUpdatedOn = u.CurrentIndianTime();
                            designation.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            designation.UpdatedIP = u.GetLocalIPAddress();


                            s.ERHeadDesignation.Update(designation);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewHeadDesignation");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ErHeadDesignation>(model));
            }

        }

        [CheckCookie("UserUUID")]

        public IActionResult DeleteHeadDesignation(string uuid)
        {
            try
            {
                ER_HeadDesignation MY = s.ERHeadDesignation.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID ==
                Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(
                    c => c.HeadDesignation_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ERHeadDesignation.Update(MY);

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
            return RedirectToAction("ViewHeadDesignation");
        }
        #endregion


        #region Expense Unit

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetExpenseUnitData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ERExpenseUnit.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.ExpenseUnit_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Unit_Name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "unitname" => query.OrderBy(i => i.Unit_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.ExpenseUnit_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "unitname" => query.OrderByDescending(i => i.Unit_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.UUID)
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.IsActive == true)
             .ToList();
            var srNo = start + 1;


            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(a => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Expense/EditExpenseUnitMaster/{a.UUID}' class='btnEdit' target='_blank'>{a.UUID}</a>",
                    unitname = a.Unit_Name,
                    status = (bool)a.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{a.UUID}'>Delete</button>"
                })
            });
        }
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewExpenseUnitMaster()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditExpenseUnitMaster(string UUID)
        {
            if (UUID == null)
            {
                return View(new ErExpenseUnit());
            }

            ER_ExpenseUnit MY = s.ERExpenseUnit.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies
            ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending
            (c => c.ExpenseUnit_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewExpenseUnitMaster");
            }
            else
            {
                ErExpenseUnit MY1 = new ErExpenseUnit();
                MY1 = _mapper.Map<ErExpenseUnit>(MY);
                return View("AddExpenseUnitMaster", MY1);
            }

        }
        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddExpenseUnitMaster(string UUID)
        {
            if (UUID == null)
            {
                ErExpenseUnit m = new ErExpenseUnit();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var erunit = s.ERExpenseUnit.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending
                (c => c.ExpenseUnit_Id).FirstOrDefault();

                if (erunit == null)
                {
                    return RedirectToAction("ViewExpenseUnitMaster");
                }
                else
                {
                    return View(erunit);
                }

            }

        }
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddExpenseUnitMaster(ErExpenseUnit model)
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
                        model.RecordNo = u.GetRecordNo();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.UUID = u.GetUUID();
                        var erunit = s.ERExpenseUnit.Get().Where(c => c.IsActive == true && c.Unit_Name == model.Unit_Name && c.Master_Company_UUID ==
                        Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                            .OrderByDescending(c => c.ExpenseUnit_Id).FirstOrDefault();


                        if (erunit != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            ER_ExpenseUnit MY = new ER_ExpenseUnit();
                            MY = _mapper.Map<ER_ExpenseUnit>(model);
                            s.ERExpenseUnit.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewExpenseUnitMaster");
                        }
                    }
                    else
                    {


                        var erunit = s.ERExpenseUnit.Get().FirstOrDefault(c => c.UUID == model.UUID);
                        if (erunit == null)
                        {
                            TempData["Message"] = "Record not found!";
                            TempData["MessageType"] = "danger";
                            return RedirectToAction("ViewExpenseUnitMaster");
                        }

                        var isDuplicate = s.ERExpenseUnit.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.Unit_Name == model.Unit_Name &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ErExpenseUnit>(model));
                        }

                        else
                        {
                            erunit.Unit_Name = model.Unit_Name;
                            erunit.IsDisplay = model.IsDisplay;
                            erunit.IsUpdatedOn = u.CurrentIndianTime();
                            erunit.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            erunit.UpdatedIP = u.GetLocalIPAddress();


                            s.ERExpenseUnit.Update(erunit);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewExpenseUnitMaster");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ErExpenseUnit>(model));
            }

        }
        [CheckCookie("UserUUID")]
        public IActionResult DeleteExpenseUnitMaster(string uuid)
        {
            try
            {
                ER_ExpenseUnit MY = s.ERExpenseUnit.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID ==
                Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .OrderByDescending(c => c.ExpenseUnit_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ERExpenseUnit.Update(MY);

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
            return RedirectToAction("ViewExpenseUnitMaster");
        }

        #endregion

        #region Expense Data Range

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetExpenseDataRange()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ERExpenseDataRange.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].
            ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                .OrderByDescending(c => c.ExpenseDataRange_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                    string.Join(", ", i.Days).Contains(searchValue));
            }


            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "days" => query.OrderBy(i => i.Days),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.ExpenseDataRange_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "days" => query.OrderByDescending(i => i.Days),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.ExpenseDataRange_Id)
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.IsActive == true)
             .ToList();
            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(a => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/Expense/EditExpenseDateRangeMaster/{a.UUID}' class='btnEdit'target='_blank'>{a.UUID}</a>",
                    days = a.Days + " Days",
                    status = (bool)a.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{a.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewExpenseDateRangeMaster()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult EditExpenseDateRangeMaster(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new ErExpenseDataRange());
            }

            ER_ExpenseDataRange MY = s.ERExpenseDataRange.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID
            == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                .OrderByDescending(c => c.ExpenseDataRange_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewExpenseDateRangeMaster");
            }
            else
            {
                ErExpenseDataRange MY1 = new ErExpenseDataRange();
                MY1 = _mapper.Map<ErExpenseDataRange>(MY);
                return View("AddExpenseDateRangeMaster", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddExpenseDateRangeMaster(string Uuid)
        {
            if (Uuid == null)
            {
                ErExpenseDataRange m = new ErExpenseDataRange();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var range = s.ERExpenseDataRange.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID ==
                Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .OrderByDescending(c => c.ExpenseDataRange_Id).FirstOrDefault();

                if (range == null)
                {
                    return RedirectToAction("ViewExpenseDateRangeMaster");
                }
                else
                {
                    return View(range);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddExpenseDateRangeMaster(ErExpenseDataRange model)
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
                        model.RecordNo = u.GetRecordNo();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.UUID = u.GetUUID();
                        var range = s.ERExpenseDataRange.Get().Where(c => c.IsActive == true && c.Days == model.Days && c.Master_Company_UUID
                        == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                            .OrderByDescending(c => c.ExpenseDataRange_Id).FirstOrDefault();


                        if (range != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            ER_ExpenseDataRange MY = new ER_ExpenseDataRange();
                            MY = _mapper.Map<ER_ExpenseDataRange>(model);
                            s.ERExpenseDataRange.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewExpenseDateRangeMaster");
                        }
                    }
                    else
                    {


                        var range = s.ERExpenseDataRange.Get().FirstOrDefault(c => c.UUID == model.UUID);
                       
                        var isDuplicate = s.ERExpenseDataRange.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.Days == model.Days &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ErExpenseDataRange>(model));
                        }

                        else
                        {
                            range.Days = model.Days;
                            range.IsDisplay = model.IsDisplay;
                            range.IsUpdatedOn = u.CurrentIndianTime();
                            range.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            range.UpdatedIP = u.GetLocalIPAddress();


                            s.ERExpenseDataRange.Update(range);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewExpenseDateRangeMaster");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ErExpenseDataRange>(model));
            }
        }


        [CheckCookie("UserUUID")]
        public IActionResult DeleteExpenseDateRangeMaster(string uuid)
        {
            try
            {
                ER_ExpenseDataRange MY = s.ERExpenseDataRange.Get().Where(c => c.IsActive == true && c.UUID == uuid &&
                c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies
                ["EnvUUID"].ToString()).OrderByDescending(c => c.ExpenseDataRange_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ERExpenseDataRange.Update(MY);

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
            return RedirectToAction("ViewExpenseDateRangeMaster");
        }

        #endregion


        //#region ViewReportingDesignation

        //[HttpPost]
        //public async Task<IActionResult> GetReportingDesignationData()
        //{
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
        //    var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

        //    var query = _context.ErManageReportDesignations.Where(a => !a.IsDeletedOn.HasValue);

        //    // Filter by search
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        query = query.Where(a =>
        //            a.Uuid.Contains(searchValue) ||
        //            a.DesignationName.Contains(searchValue));
        //    }

        //    var totalRecords = await query.CountAsync();

        //    // Apply sorting
        //    if (sortColumnDirection == "asc")
        //    {
        //        query = sortColumn switch
        //        {
        //            "uuid" => query.OrderBy(a => a.Uuid),
        //            "designationname" => query.OrderBy(a => a.DesignationName),
        //            _ => query.OrderBy(a => a.Uuid)
        //        };
        //    }
        //    else
        //    {
        //        query = sortColumn switch
        //        {
        //            "uuid" => query.OrderByDescending(a => a.Uuid),
        //            "designationname" => query.OrderByDescending(a => a.DesignationName),
        //            _ => query.OrderByDescending(a => a.Uuid)
        //        };
        //    }

        //    var data = await query
        //        .Skip(start)
        //        .Take(length)
        //        .ToListAsync();

        //    var srNo = start + 1;

        //    return Json(new
        //    {
        //        draw,
        //        recordsFiltered = totalRecords,
        //        recordsTotal = totalRecords,
        //        data = data.Select(a => new
        //        {
        //            srno = srNo++,
        //            uuid = $"<a href='/Expense/EditReportingDesignation/{a.Uuid}' class='btnEdit'>{a.Uuid}</a>",
        //            designationname = a.DesignationName,
        //            nooferapproval = a.NoOfErApproval,
        //            noofpafapproval = a.NoOfPafApproval,
        //            status = (bool)a.IsDisplay
        //                ? "<span class='badge bg-success'>Visible</span>"
        //                : "<span class='badge bg-danger'>Hidden</span>",
        //            action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{a.Uuid}'>Delete</button>"
        //        })
        //    });
        //}

        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> ViewReportingDesignation()
        //{
        //    var designations = await _manageReportDesignationService.GetAllManageReportDesignations(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    return View(designations);
        //}

        //[HttpGet]
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> EditReportingDesignation(string Uuid)
        //{
        //    if (Uuid == null)
        //    {
        //        return View(new ErManageReportDesignation());
        //    }

        //    var designation = await _context.ErManageReportDesignations
        //        .FirstOrDefaultAsync(d => d.Uuid == Uuid);

        //    if (designation == null)
        //    {
        //        return RedirectToAction("ViewReportingDesignation");
        //    }

        //    return View("AddReportingDesignation", designation);
        //}

        //[HttpGet]
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> AddReportingDesignation(string Uuid)
        //{
        //    if (Uuid == null)
        //    {
        //        return View(new ErManageReportDesignation());
        //    }

        //    var designation = await _context.ErManageReportDesignations
        //        .FirstOrDefaultAsync(d => d.Uuid == Uuid);

        //    if (designation == null)
        //    {
        //        return RedirectToAction("ViewReportingDesignation");
        //    }

        //    return View(designation);
        //}

        //[HttpPost]
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> AddReportingDesignation(ErManageReportDesignation model)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(model.Uuid)) // Adding new record
        //        {
        //            model.MasterCompanyUuid = Request.Cookies["CmpUUID"].ToString();               // save in database
        //            model.MasterEnvironmentUuid = Request.Cookies["EnvUUID"].ToString();
        //            model.IsActive = true;
        //            string uuid = Guid.NewGuid().ToString("N");
        //            model.Uuid = uuid.Substring(0, 8) + "-" + uuid.Substring(8, 4) + "-" + uuid.Substring(12, 4) + "-" + uuid.Substring(16, 4) + "-" + uuid.Substring(20, 8);

        //            bool isDuplicate = await _context.ErManageReportDesignations
        //                .AnyAsync(d => d.DesignationName == model.DesignationName);

        //            if (isDuplicate)
        //            {
        //                TempData["Message"] = "Reporting Designation already exists!";
        //                return View(model);
        //            }

        //            _manageReportDesignationService.AddManageReportDesignation(model);
        //            TempData["Message"] = "Reporting Designation added successfully!";
        //            TempData["MessageType"] = "success";
        //            return RedirectToAction("ViewReportingDesignation");
        //        }
        //        else // Editing existing record
        //        {
        //            var existingDesignation = await _context.ErManageReportDesignations
        //                .FirstOrDefaultAsync(d => d.Uuid == model.Uuid);

        //            if (existingDesignation == null)
        //            {
        //                return RedirectToAction("ViewReportingDesignation");
        //            }

        //            bool isDuplicate = await _context.ErManageReportDesignations
        //                .AnyAsync(d => d.DesignationName == model.DesignationName && d.Uuid != model.Uuid);

        //            if (isDuplicate)
        //            {
        //                TempData["Message"] = "Reporting Designation already exists!";
        //                return View(model);
        //            }

        //            // Update fields
        //            // Update fields
        //            existingDesignation.DesignationName = model.DesignationName;
        //            existingDesignation.IsDisplay = model.IsDisplay;
        //            existingDesignation.NoOfErApproval = model.NoOfErApproval; // Include this
        //            existingDesignation.NoOfPafApproval = model.NoOfPafApproval; // Include this
        //            existingDesignation.IsUpdatedOn = DateTime.Now;
        //            existingDesignation.IsUpdatedBy = "1";


        //            _context.ErManageReportDesignations.Update(existingDesignation);
        //            await _context.SaveChangesAsync();

        //            TempData["Message"] = "Reporting Designation updated successfully!";
        //            TempData["MessageType"] = "success";
        //            return RedirectToAction("ViewReportingDesignation");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Message"] = $"An error occurred: {ex.Message}";
        //        TempData["MessageType"] = "danger";
        //        return View(model);
        //    }

        //}


        ////public IActionResult EditReportingDesignation(Guid uuid)
        ////{
        ////    var designation = _manageReportDesignationService.GetManageReportDesignationByUUID(uuid);
        ////    if (designation == null) return NotFound();
        ////    return View(designation);
        ////}

        ////[HttpPost]
        ////public IActionResult EditReportingDesignation(ErManageReportDesignation model)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            _manageReportDesignationService.UpdateManageReportDesignation(model);
        ////            TempData["Message"] = "Manage Report Designation updated successfully!";
        ////            TempData["MessageType"] = "success";
        ////            return RedirectToAction("ViewReportingDesignation");
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            TempData["Message"] = ex.Message;
        ////            TempData["MessageType"] = "danger";
        ////        }
        ////    }
        ////    return View(model);
        ////}

        //[HttpPost]
        //public IActionResult DeleteReportingDesignation(Guid uuid)
        //{
        //    try
        //    {
        //        _manageReportDesignationService.DeleteManageReportDesignation(uuid);
        //        TempData["Message"] = "Manage Report Designation deleted successfully!";
        //        TempData["MessageType"] = "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Message"] = ex.Message;
        //        TempData["MessageType"] = "danger";
        //    }
        //    return RedirectToAction("ViewReportingDesignation");
        //}
        //#endregion

        public IActionResult ViewExpenseTypeMaster()
        {
            return View();
        }
        public IActionResult AddExpenseTypeMaster()
        {
            return View();
        }
        public IActionResult EditExpenseTypeMaster()
        {
            return View();
        }
        public IActionResult ViewExpenseSubTypeMaster()
        {
            return View();
        }
        public IActionResult AddExpenseSubTypeMaster()
        {
            return View();
        }


        public IActionResult ViewRemarkTemplate()
        {
            return View();
        }
        public IActionResult AddRemarkTemplate()
        {
            return View();
        }
        public IActionResult EditRemarkTemplate()
        {
            return View();
        }
        public IActionResult ViewSubstituteExpense()
        {
            return View();
        }
        public IActionResult AddSubstituteExpense()
        {
            return View();
        }
        public IActionResult EditSubstituteExpense()
        {
            return View();
        }


        public IActionResult ViewEmployeeMaster()
        {
            return View();
        }
        public IActionResult AddEmployeeMaster()
        {
            return View();
        }
        public IActionResult EditEmployeeMaster()
        {
            return View();
        }

        public IActionResult ViewWorkFlowInstanceMaster()
        {
            return View();
        }
        public IActionResult AddWorkFlowInstanceMaster()
        {
            return View();
        }



        public IActionResult ViewCostCategoryDetail()
        {
            return View();
        }
        public IActionResult AddCostCategoryMaster()
        {
            return View();
        }

        public IActionResult ViewCostCityDetail()
        {
            return View();
        }
        public IActionResult AddCostCityDetail()
        {
            return View();
        }

        public IActionResult ViewExpenseLimitMaster()
        {
            return View();
        }
        public IActionResult AddExpenseLimitMaster()
        {
            return View();
        }

        public IActionResult ViewExpenseEligibilityMaster()
        {
            return View();
        }
        public IActionResult AddExpenseEligibilityMaster()
        {
            return View();
        }


        public IActionResult AddTimeZoneMaster()
        {
            return View();
        }


        //#region Currency Master

        //[HttpPost]
        //public async Task<IActionResult> GetExpenseCurrencyMasterData()
        //{
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
        //    var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

        //    var query = _context.ErsetupCurrencies.Where(c => c.IsActive == true && c.MasterCompanyUuid == Request.Cookies["CmpUUID"]
        //    .ToString() && c.MasterEnvironmentUuid == Request.Cookies["EnvUUID"].ToString()).AsQueryable();



        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        query = query.Where(i =>

        //            i.CurrencyName.Contains(searchValue) ||
        //            i.CurrencyShortName.ToString().Contains(searchValue));
        //    }

        //    var totalRecords = query.Count();


        //    if (sortColumnDirection == "asc")
        //    {
        //        query = sortColumn switch
        //        {
        //            //"id" => query.OrderBy(c => c.UnitId),
        //            "uuid" => query.OrderBy(c => c.Uuid),
        //            "currencyname" => query.OrderBy(c => c.CurrencyName),
        //            "currencyshortname" => query.OrderBy(c => c.CurrencyShortName),
        //            "symbol" => query.OrderByDescending(c => c.CurrencySymbol),
        //            "status" => query.OrderBy(c => c.IsDisplay),
        //            _ => query.OrderBy(i => i.CurrencyId)
        //        };
        //    }
        //    else
        //    {
        //        query = sortColumn switch
        //        {
        //            //"id" => query.OrderByDescending(c => c.UnitId),
        //            "uuid" => query.OrderByDescending(c => c.Uuid),
        //            "currencyname" => query.OrderByDescending(c => c.CurrencyName),
        //            "currencyshortname" => query.OrderByDescending(c => c.CurrencyShortName),
        //            "symbol" => query.OrderByDescending(c => c.CurrencySymbol),
        //            "status" => query.OrderByDescending(c => c.IsDisplay),
        //            _ => query.OrderByDescending(i => i.CurrencyId)
        //        };
        //    }

        //    var data = query
        //    .Skip(start)
        //    .Take(length).Where(x => x.IsActive == true)
        //    .ToList();

        //    var srNo = start + 1;

        //    return Json(new
        //    {
        //        draw = draw,
        //        recordsFiltered = totalRecords,
        //        recordsTotal = totalRecords,
        //        data = data.Select(menu => new
        //        {
        //            srno = srNo++,
        //            uuid = $"<a href='/Expense/MasterEditExpenseCurrencyMaster/{menu.Uuid}' class='btnEdit'>{menu.Uuid}</a>",
        //            currencyname = menu.CurrencyName,
        //            currencyshortname = menu.CurrencyShortName,
        //            symbol = menu.CurrencySymbol,
        //            status = (bool)menu.IsDisplay
        //                 ? "<span class='badge bg-success'>Visible</span>"
        //                 : "<span class='badge bg-danger'>Hidden</span>",
        //            action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + menu.Uuid + "'>Delete</button>"
        //        })
        //    });


        //}


        //[CheckCookie("UserUUID")]
        //public IActionResult MasterViewExpenseCurrencyMaster()
        //{
        //    //var Unit = _currency.GetAllCurrency(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()); ;
        //    var currencies = _currency.GetAllCurrency(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    return View(currencies);
        //}
        //[HttpGet]
        //[CheckCookie("UserUUID")]
        //public IActionResult MasterEditExpenseCurrencyMaster(string Uuid)
        //{
        //    var countries = _viewCountry.GetAllCountries(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result
        //                    .Where(s => s.IsActive == true)
        //                    .ToList();
        //    ViewBag.Country = countries.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid,
        //        Text = c.CountryName
        //    }).ToList();
        //    if (Uuid == null)
        //    {

        //        return View(new ErsetupCurrency());
        //    }
        //    var unit = _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == Uuid);
        //    if (unit == null)
        //    {
        //        return RedirectToAction("MasterViewExpenseCurrencyMaster");
        //    }

        //    return View("MasterAddExpenseCurrencyMaster", unit);
        //}
        //[HttpGet]
        //[CheckCookie("UserUUID")]
        //public IActionResult MasterAddExpenseCurrencyMaster(string Uuid)
        //{
        //    if (Uuid == null)
        //    {
        //        var countries = _viewCountry.GetAllCountries(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result
        //                    .Where(s => s.IsActive == true)
        //                    .ToList();
        //        ViewBag.Country = countries.Select(c => new SelectListItem
        //        {
        //            Value = c.Uuid,
        //            Text = c.CountryName
        //        }).ToList();
        //        return View(new ErsetupCurrency());
        //    }
        //    var unit = _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == Uuid);
        //    if (unit == null)
        //    {
        //        return RedirectToAction("MasterViewExpenseCurrencyMaster");
        //    }
        //    return View("MasterAddExpenseCurrencyMaster", unit);
        //}
        //[HttpPost]
        //[CheckCookie("UserUUID")]
        //public IActionResult MasterAddExpenseCurrencyMaster(ErsetupCurrency currency)
        //{
        //    try
        //    {

        //        if (string.IsNullOrEmpty(currency.Uuid))
        //        {
        //            currency.MasterCompanyUuid = Request.Cookies["CmpUUID"].ToString();
        //            currency.MasterEnvironmentUuid = Request.Cookies["EnvUUID"].ToString();

        //            // Save in Database
        //            var countries = _viewCountry.GetAllCountries(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result
        //          .Where(s => s.IsActive == true)
        //          .ToList();
        //            ViewBag.Country = countries.Select(c => new SelectListItem
        //            {
        //                Value = c.Uuid,
        //                Text = c.CountryName
        //            }).ToList();
        //            currency.IsActive = true;
        //            string uuid = Guid.NewGuid().ToString("N");
        //            currency.Uuid = uuid.Substring(0, 8) + "-" + uuid.Substring(8, 4)
        //                + "-" + uuid.Substring(12, 4) + "-" + uuid.Substring(16, 4)
        //                + "-" + uuid.Substring(20, 8);

        //            bool isDuplicate = _context.ErsetupCurrencies
        //               .Any(c => c.CurrencyName == currency.CurrencyName || c.CurrencyShortName == currency.CurrencyShortName);
        //            if (isDuplicate)
        //            {
        //                TempData["Message"] = "Data already exists!";
        //                return View(currency);
        //            }

        //            _currency.AddCurrency(currency);
        //            TempData["Message"] = "Data Inserted Successfully!";
        //            TempData["MessageType"] = "success";
        //            return RedirectToAction("MasterViewExpenseCurrencyMaster");
        //        }
        //        else
        //        {
        //            currency.MasterCompanyUuid = Request.Cookies["CmpUUID"].ToString();
        //            currency.MasterEnvironmentUuid = Request.Cookies["EnvUUID"].ToString();
        //            // Edit operation
        //            var year = _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == currency.Uuid);
        //            if (year == null)
        //            {
        //                return RedirectToAction("MasterViewExpenseCurrencyMaster");
        //            }

        //            bool isDuplicate = _context.ErsetupCurrencies
        //                .Any(c => c.CurrencyName == currency.CurrencyName && c.CurrencyShortName == currency.CurrencyShortName && c.Uuid != currency.Uuid);

        //            if (isDuplicate)
        //            {
        //                TempData["Message"] = "Data already exists!";
        //                return View(currency);
        //            }
        //            // Update the existing record
        //            year.CurrencyName = currency.CurrencyName;
        //            year.Uuid = currency.Uuid;
        //            year.CurrencySymbol = currency.CurrencySymbol;
        //            year.IsDefault = currency.IsDefault;
        //            year.CurrencyShortName = currency.CurrencyShortName;
        //            year.IsDisplay = currency.IsDisplay;
        //            year.IsUpdatedOn = DateTime.Now;
        //            year.IsUpdatedBy = "1";

        //            _context.ErsetupCurrencies.Update(year);
        //            _context.SaveChanges();

        //            TempData["Message"] = "Data Updated Successfully!";
        //            TempData["MessageType"] = "success";

        //            return RedirectToAction("MasterViewExpenseCurrencyMaster");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Message"] = $"An error occurred: {ex.Message}";
        //        TempData["MessageType"] = "danger";
        //        return View(currency);
        //    }
        //}
        //public IActionResult DeleteExpenseCurrencyMaster(Guid Uuid)
        //{
        //    var result = _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == Uuid.ToString());
        //    if (result != null)
        //    {
        //        result.IsDeletedOn = DateTime.Now;
        //        result.IsDeletedBy = "1";
        //        result.IsActive = false;
        //        _context.ErsetupCurrencies.Update(result);
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("MasterViewExpenseCurrencyMaster");
        //}

        //#endregion


        public IActionResult ViewSoleExpenses()
        {
            return View();
        }
        public IActionResult AddSoleExpense()
        {
            return View();
        }
        public IActionResult EditSoleExpense()
        {
            return View();
        }
        public IActionResult ViewTripExpense()
        {
            return View();
        }
        public IActionResult AddTripexpense()
        {
            return View();
        }
        public IActionResult EditTripexpense()
        {
            return View();
        }
        public IActionResult SoleExpenseApproval()
        {
            return View();
        }
        public IActionResult Myapprovedexpenses()
        {
            return View();
        }
        public IActionResult Pendingapprovals()
        {
            return View();
        }
        public IActionResult Myrejectedexpenses()
        {
            return View();
        }
        public IActionResult Approvedbyme()
        {
            return View();
        }
        public IActionResult TripExpenseApproval()
        {
            return View();
        }
        public IActionResult Mytripapprovedexpenses()
        {
            return View();
        }
        public IActionResult Pendingtripapprovals()
        {
            return View();
        }
        public IActionResult Mytriprejectedexpenses()
        {
            return View();
        }
        public IActionResult Tripapprovedbyme()
        {
            return View();
        }


    }
}
