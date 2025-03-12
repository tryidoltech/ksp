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
    public class HRAController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public HRAController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult ViewEmployeementtype()
        {
            return View();
        }
        public IActionResult AddEmployeementtype()
        {
            return View();
        }

        #region ViewShift

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetShiftData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();
            var query = s.HRAShift.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Shift_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Shift_Name.Contains(searchValue));

            }



            var totalRecords = query.Count();


            // Apply sorting



            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "shiftname" => query.OrderBy(i => i.Shift_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Shift_Id)
                };
            }

            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "shiftname" => query.OrderBy(i => i.Shift_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Shift_Id)
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
                data = data.Select(sh => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditShift/{sh.UUID}'class='btnEdit' target='_blank'>{sh.UUID}</a>",
                    shiftname = sh.Shift_Name,
                    starttime = sh.Start_Time?.ToString("hh\\:mm"),
                    endtime = sh.End_Time?.ToString("hh\\:mm"),
                    lunchtime = sh.Lunch_Time,
                    totalworkinghours = sh.TotalWorking_Hours,
                    status = (bool)sh.IsDisplay
                ? "<span class='badge bg-success'>Visible</span>"
                : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{sh.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]

        public async Task<IActionResult> ViewShift()
        {
            return View();
        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditShift(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraShift());
            }

            else
            {
                HRA_Shift MY = s.HRAShift.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Shift_Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewShift");
                }
                else
                {
                    HraShift MY1 = new HraShift();
                    MY1 = _mapper.Map<HraShift>(MY);
                    return View("AddShift", MY1);
                }
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddShift(string UUID)
        {
            if (UUID == null)
            {
                HraShift m = new HraShift();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var shift = s.HRAShift.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Shift_Id).FirstOrDefault();

                if (shift == null)
                {
                    return RedirectToAction("ViewShift");
                }
                else
                {
                    return View(shift);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddShift(HraShift model)
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

                        var duplicateRecord = s.HRAShift.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Shift_Name == model.Shift_Name && c.Shift_Prefix == model.Shift_Prefix &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_Shift newshift = _mapper.Map<HRA_Shift>(model);
                            newshift = _mapper.Map<HRA_Shift>(model);
                            s.HRAShift.Add(newshift);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewShift");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRAShift.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRAShift.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Shift_Name == model.Shift_Name && c.Shift_Prefix == model.Shift_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraShift>(model));
                        }
                        else
                        {
                            existingRecord.Shift_Name = model.Shift_Name;
                            existingRecord.Shift_Prefix = model.Shift_Prefix;
                            existingRecord.Start_Time = model.Start_Time;
                            existingRecord.End_Time = model.End_Time;
                            existingRecord.Lunch_Time = model.Lunch_Time;
                            existingRecord.TotalWorking_Hours = model.TotalWorking_Hours;

                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRAShift.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewShift");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraShift>(model));
            }
        }


        [CheckCookie("UserUUID")]
        public IActionResult DeleteShift(string uuid)
        {
            try
            {
                HRA_Shift MY = s.HRAShift.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Shift_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRAShift.Update(MY);

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
            return RedirectToAction("ViewShift");
        }


        #endregion


        #region ViewPaySlipCategory
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetPaySlipCategoryData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.HRAPaySlipCategory.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaySlipCategory_Id).AsQueryable();

            // Filter by search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p =>
                    p.Payslip_Category.Contains(searchValue));
            }
            var totalRecords = query.Count();


            // Apply sorting
            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(p => p.UUID),
                    "payslipcategory" => query.OrderBy(p => p.Payslip_Category),
                    "status" => query.OrderBy(p => p.IsDisplay),
                    _ => query.OrderBy(p => p.PaySlipCategory_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(p => p.UUID),
                    "payslipcategory" => query.OrderByDescending(p => p.Payslip_Category),
                    "status" => query.OrderByDescending(p => p.IsDisplay),
                    _ => query.OrderByDescending(p => p.Payslip_Category)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(p => p.IsActive == true)
                .ToList();

            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(p => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditPaySlipCategory/{p.UUID}'class='btnEdit' target='_blank'>{p.UUID}</a>",


                    payslipcategory = p.Payslip_Category,
                    status = (bool)p.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{p.UUID}'>Delete</button>"
                })
            });
        }
        [CheckCookie("UserUUID")]
        public IActionResult ViewPaySlipCategory()
        {

            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditPaySlipCategory(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraPaySlipCategory());
            }

            else
            {
                HRA_PaySlipCategory MY = s.HRAPaySlipCategory.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaySlipCategory_Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewPaySlipCategory");
                }
                else
                {
                    HraPaySlipCategory MY1 = new HraPaySlipCategory();
                    MY1 = _mapper.Map<HraPaySlipCategory>(MY);
                    return View("AddPaySlipCategory", MY1);
                }
            }

        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddPaySlipCategory(string UUID)
        {
            if (UUID == null)
            {
                HraPaySlipCategory m = new HraPaySlipCategory();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var payslipcat = s.HRAPaySlipCategory.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaySlipCategory_Id).FirstOrDefault();

                if (payslipcat == null)
                {
                    return RedirectToAction("ViewPaySlipCategory");
                }
                else
                {
                    return View(payslipcat);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddPaySlipCategory(HraPaySlipCategory model)
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

                        var duplicateRecord = s.HRAPaySlipCategory.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Payslip_Category == model.Payslip_Category &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_PaySlipCategory payslip = _mapper.Map<HRA_PaySlipCategory>(model);
                            payslip = _mapper.Map<HRA_PaySlipCategory>(model);
                            s.HRAPaySlipCategory.Add(payslip);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewPaySlipCategory");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRAPaySlipCategory.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRAPaySlipCategory.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Payslip_Category == model.Payslip_Category &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraPaySlipCategory>(model));
                        }
                        else
                        {
                            existingRecord.Payslip_Category = model.Payslip_Category;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRAPaySlipCategory.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewPaySlipCategory");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraPaySlipCategory>(model));
            }
        }



        [CheckCookie("UserUUID")]
        public IActionResult DeletePaySlipCategory(string uuid)
        {
            try
            {
                HRA_PaySlipCategory MY = s.HRAPaySlipCategory.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.PaySlipCategory_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRAPaySlipCategory.Update(MY);

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
            return RedirectToAction("ViewPaySlipCategory");
        }
        #endregion

        #region ViewWeekDay
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetWeekDayData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.HRAWeekDay.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.WeekDays_Id).AsQueryable();
            ;

            // Filter by search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(w =>
                    w.WeekDay_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            // Apply sorting
            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(w => w.UUID),
                    "weekdayname" => query.OrderBy(w => w.WeekDay_Name),
                    "status" => query.OrderBy(w => w.IsDisplay),
                    _ => query.OrderBy(w => w.WeekDays_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(w => w.UUID),
                    "weekdayname" => query.OrderByDescending(w => w.WeekDay_Name),
                    "status" => query.OrderByDescending(w => w.IsDisplay),
                    _ => query.OrderByDescending(w => w.WeekDays_Id)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(w => w.IsActive == true)
                .ToList();

            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(w => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditWeekDay/{w.UUID}'class='btnEdit' target='_blank'>{w.UUID}</a>",
                    weekdayname = w.WeekDay_Name,
                    status = (bool)w.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{w.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewWeekDay()
        {

            return View();
        }

        public IActionResult AddWeekDay()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> EditWeekDay(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraWeekDay());
            }

            else
            {
                HRA_WeekDay MY = s.HRAWeekDay.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.WeekDays_Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewWeekDay");
                }
                else
                {
                    HraWeekDay MY1 = new HraWeekDay();
                    MY1 = _mapper.Map<HraWeekDay>(MY);
                    return View("AddWeekDay", MY1);
                }
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddWeekDay(string UUID)
        {
            if (UUID == null)
            {
                HraWeekDay m = new HraWeekDay();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var weekday = s.HRAWeekDay.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.WeekDays_Id).FirstOrDefault();

                if (weekday == null)
                {
                    return RedirectToAction("ViewWeekDay");
                }
                else
                {
                    return View(weekday);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddWeekDay(HraWeekDay model)
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

                        var duplicateRecord = s.HRAWeekDay.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.WeekDay_Name == model.WeekDay_Name &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_WeekDay weekdays = _mapper.Map<HRA_WeekDay>(model);
                            weekdays = _mapper.Map<HRA_WeekDay>(model);
                            s.HRAWeekDay.Add(weekdays);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewWeekDay");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRAWeekDay.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRAWeekDay.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.WeekDay_Name == model.WeekDay_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraWeekDay>(model));
                        }
                        else
                        {
                            existingRecord.WeekDay_Name = model.WeekDay_Name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRAWeekDay.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewWeekDay");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraWeekDay>(model));
            }
        }



        [CheckCookie("UserUUID")]
        public IActionResult DeleteWeekDay(string uuid)
        {
            try
            {
                HRA_WeekDay MY = s.HRAWeekDay.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.WeekDays_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRAWeekDay.Update(MY);

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
            return RedirectToAction("ViewWeekDay");
        }




        #endregion

        #region ViewNominee

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetNomineeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.HRANominee.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nominee_Id).AsQueryable();

            // Filter by search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(n =>

                    n.Nominee_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            // Apply sorting
            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(n => n.UUID),
                    "nomineename" => query.OrderBy(n => n.Nominee_Name),
                    "status" => query.OrderBy(n => n.IsDisplay),
                    _ => query.OrderBy(n => n.Nominee_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(n => n.UUID),
                    "nomineename" => query.OrderByDescending(n => n.Nominee_Name),
                    "status" => query.OrderByDescending(n => n.IsDisplay),
                    _ => query.OrderByDescending(n => n.Nominee_Id)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(n => n.IsActive == true)
                .ToList();

            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(n => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditNominee/{n.UUID}'class='btnEdit' target='_blank'>{n.UUID}</a>",
                    nomineename = n.Nominee_Name,
                    status = (bool)n.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{n.UUID}'>Delete</button>"
                })
            });
        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewNominee()
        {

            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditNominee(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraNominee());
            }

            else
            {
                HRA_Nominee MY = s.HRANominee.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nominee_Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewNominee");
                }
                else
                {
                    HraNominee MY1 = new HraNominee();
                    MY1 = _mapper.Map<HraNominee>(MY);
                    return View("AddNominee", MY1);
                }
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddNominee(string UUID)
        {
            if (UUID == null)
            {
                HraNominee m = new HraNominee();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var nominee = s.HRANominee.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nominee_Id).FirstOrDefault();

                if (nominee == null)
                {
                    return RedirectToAction("ViewNominee");
                }
                else
                {
                    return View(nominee);
                }

            }
        }


        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddNominee(HraNominee model)
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

                        var duplicateRecord = s.HRANominee.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Nominee_Name == model.Nominee_Name &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_Nominee payslip = _mapper.Map<HRA_Nominee>(model);
                            payslip = _mapper.Map<HRA_Nominee>(model);
                            s.HRANominee.Add(payslip);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewNominee");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRANominee.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRANominee.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Nominee_Name == model.Nominee_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraNominee>(model));
                        }
                        else
                        {
                            existingRecord.Nominee_Name = model.Nominee_Name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRANominee.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewNominee");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraNominee>(model));
            }
        }




        [CheckCookie("UserUUID")]
        public IActionResult DeleteNominee(string uuid)
        {
            try
            {
                HRA_Nominee MY = s.HRANominee.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nominee_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRANominee.Update(MY);

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
            return RedirectToAction("ViewNominee");
        }

        #endregion

        #region ViewTaxRegime

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetTaxRegimeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.HRATaxRegime.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TaxRegime_Id).AsQueryable();

            // Filter by search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(tr =>

                    tr.Tax_Regime.Contains(searchValue));
            }

            var totalRecords = query.Count();

            // Apply sorting
            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(tr => tr.UUID),
                    "taxregime" => query.OrderBy(tr => tr.Tax_Regime),
                    "status" => query.OrderBy(tr => tr.IsDisplay),
                    _ => query.OrderBy(tr => tr.TaxRegime_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(tr => tr.UUID),
                    "taxregime" => query.OrderByDescending(tr => tr.Tax_Regime),
                    "status" => query.OrderByDescending(tr => tr.IsDisplay),
                    _ => query.OrderByDescending(tr => tr.TaxRegime_Id)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(tr => tr.IsActive == true)
                .ToList();

            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(tr => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditTaxRegime/{tr.UUID}'class='btnEdit' target='_blank'>{tr.UUID}</a>",
                    taxregime = tr.Tax_Regime,
                    status = (bool)tr.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{tr.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewTaxRegime()
        {

            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditTaxRegime(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraTaxRegime());
            }

            else
            {
                HRA_TaxRegime MY = s.HRATaxRegime.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TaxRegime_Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewTaxRegime");
                }
                else
                {
                    HraTaxRegime MY1 = new HraTaxRegime();
                    MY1 = _mapper.Map<HraTaxRegime>(MY);
                    return View("AddTaxRegime", MY1);
                }
            }

        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddTaxRegime(string UUID)
        {
            if (UUID == null)
            {
                HraTaxRegime m = new HraTaxRegime();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var taxregime = s.HRATaxRegime.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TaxRegime_Id).FirstOrDefault();

                if (taxregime == null)
                {
                    return RedirectToAction("ViewTaxRegime");
                }
                else
                {
                    return View(taxregime);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddTaxRegime(HraTaxRegime model)
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

                        var duplicateRecord = s.HRATaxRegime.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Tax_Regime == model.Tax_Regime &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_TaxRegime regime = _mapper.Map<HRA_TaxRegime>(model);
                            regime = _mapper.Map<HRA_TaxRegime>(model);
                            s.HRATaxRegime.Add(regime);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewTaxRegime");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRATaxRegime.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRATaxRegime.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Tax_Regime == model.Tax_Regime &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraTaxRegime>(model));
                        }
                        else
                        {
                            existingRecord.Tax_Regime = model.Tax_Regime;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRATaxRegime.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewTaxRegime");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraTaxRegime>(model));
            }
        }




        [HttpPost]
        public IActionResult DeleteTaxRegime(string uuid)
        {
            try
            {
                HRA_TaxRegime MY = s.HRATaxRegime.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.TaxRegime_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRATaxRegime.Update(MY);

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
            return RedirectToAction("ViewTaxRegime");
        }
        #endregion
        public IActionResult ViewProfessionalTax()
        {
            return View();
        }
        public IActionResult AddProfessionalTax()
        {
            return View();
        }

        #region LeaveType
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetLeaveType()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.HRALeaveTypeMaster.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();

            // Filter by search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(tr =>

                    tr.LeaveType_Name.Contains(searchValue) || tr.LeaveType_ShortName.Contains(searchValue));
            }

            var totalRecords = query.Count();

            // Apply sorting
            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(tr => tr.UUID),
                    "leavetype" => query.OrderBy(tr => tr.LeaveType_Name),
                    "leaveshortname" => query.OrderBy(tr => tr.LeaveType_ShortName),
                    "status" => query.OrderBy(tr => tr.IsDisplay),
                    _ => query.OrderBy(tr => tr.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(tr => tr.UUID),
                    "leavetype" => query.OrderByDescending(tr => tr.LeaveType_Name),
                    "leaveshortname" => query.OrderByDescending(tr => tr.LeaveType_ShortName),
                    "status" => query.OrderByDescending(tr => tr.IsDisplay),
                    _ => query.OrderByDescending(tr => tr.Id)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(tr => tr.IsActive == true)
                .ToList();

            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(tr => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditLeaveType/{tr.UUID}'class='btnEdit' target='_blank'>{tr.UUID}</a>",
                    leavetype = tr.LeaveType_Name,
                    leaveshortname = tr.LeaveType_ShortName,
                    status = (bool)tr.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{tr.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewLeaveType()
        {

            return View();
        }



        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditLeaveType(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraLeaveTypeMaster());
            }

            else
            {
                HRA_LeaveTypeMaster MY = s.HRALeaveTypeMaster.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewLeaveType");
                }
                else
                {
                    HraLeaveTypeMaster MY1 = new HraLeaveTypeMaster();
                    MY1 = _mapper.Map<HraLeaveTypeMaster>(MY);
                    return View("AddLeaveType", MY1);
                }
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddLeaveType(string UUID)
        {
            if (UUID == null)
            {
                HraLeaveTypeMaster m = new HraLeaveTypeMaster();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var leavetype = s.HRALeaveTypeMaster.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (leavetype == null)
                {
                    return RedirectToAction("ViewLeaveType");
                }
                else
                {
                    return View(leavetype);
                }

            }
        }



        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddLeaveType(HraLeaveTypeMaster model)
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

                        var duplicateRecord = s.HRALeaveTypeMaster.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.LeaveType_Name == model.LeaveType_Name &&
                                c.LeaveType_ShortName == model.LeaveType_ShortName &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_LeaveTypeMaster leave = _mapper.Map<HRA_LeaveTypeMaster>(model);
                            leave = _mapper.Map<HRA_LeaveTypeMaster>(model);
                            s.HRALeaveTypeMaster.Add(leave);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewLeaveType");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRALeaveTypeMaster.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRALeaveTypeMaster.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.LeaveType_ShortName == model.LeaveType_ShortName &&
                                c.LeaveType_Name == model.LeaveType_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraLeaveTypeMaster>(model));
                        }
                        else
                        {
                            existingRecord.LeaveType_Name = model.LeaveType_Name;
                            existingRecord.LeaveType_ShortName = model.LeaveType_ShortName;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRALeaveTypeMaster.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewLeaveType");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraLeaveTypeMaster>(model));
            }
        }

        [HttpPost]
        public IActionResult DeleteLeaveType(string uuid)
        {
            try
            {
                HRA_LeaveTypeMaster MY = s.HRALeaveTypeMaster.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRALeaveTypeMaster.Update(MY);

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
            return RedirectToAction("ViewLeaveType");
        }
        #endregion


        #region Document Naming Master
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetDocumentNamingMatser()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.HRADocumentNamingMaster.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();

            // Filter by search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(tr =>

                    tr.Document_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            // Apply sorting
            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(tr => tr.UUID),
                    "documentnaming" => query.OrderBy(tr => tr.Document_Name),

                    "status" => query.OrderBy(tr => tr.IsDisplay),
                    _ => query.OrderBy(tr => tr.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(tr => tr.UUID),
                    "documentnaming" => query.OrderByDescending(tr => tr.Document_Name),

                    "status" => query.OrderByDescending(tr => tr.IsDisplay),
                    _ => query.OrderByDescending(tr => tr.Id)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(tr => tr.IsActive == true)
                .ToList();

            var srNo = start + 1;

            return Json(new
            {
                draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data.Select(tr => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/HRA/EditDocumentNaming/{tr.UUID}'class='btnEdit' target='_blank'>{tr.UUID}</a>",
                    documentypename = tr.Document_Name,

                    status = (bool)tr.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn' data-uuid='{tr.UUID}'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewDocumentNamingMaster()
        {

            return View();
        }



        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditDocumentNaming(string UUID)
        {
            if (UUID == null)
            {
                return View(new HraDocumentNamingMaster());
            }

            else
            {
                HRA_DocumentNamingMaster MY = s.HRADocumentNamingMaster.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY == null)
                {
                    return RedirectToAction("ViewDocumentNamingMaster");
                }
                else
                {
                    HraDocumentNamingMaster MY1 = new HraDocumentNamingMaster();
                    MY1 = _mapper.Map<HraDocumentNamingMaster>(MY);
                    return View("AddDocumentNamingMaster", MY1);
                }
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddDocumentNamingMaster(string UUID)
        {
            if (UUID == null)
            {
                HraDocumentNamingMaster m = new HraDocumentNamingMaster();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var documentname = s.HRADocumentNamingMaster.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (documentname == null)
                {
                    return RedirectToAction("ViewDocumentNamingMaster");
                }
                else
                {
                    return View(documentname);
                }

            }
        }



        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddDocumentNamingMaster(HraDocumentNamingMaster model)
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

                        var duplicateRecord = s.HRADocumentNamingMaster.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&

                                c.Document_Name == model.Document_Name &&
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
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();


                            HRA_DocumentNamingMaster document = _mapper.Map<HRA_DocumentNamingMaster>(model);
                            document = _mapper.Map<HRA_DocumentNamingMaster>(model);
                            s.HRADocumentNamingMaster.Add(document);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewDocumentType");
                        }


                    }
                    else
                    {

                        var existingRecord = s.HRADocumentNamingMaster.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.HRADocumentNamingMaster.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Document_Name == model.Document_Name &&
                                c.IsValidity_Applicable == model.IsValidity_Applicable &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<HraDocumentNamingMaster>(model));
                        }
                        else
                        {
                            existingRecord.Document_Name = model.Document_Name;
                            existingRecord.IsValidity_Applicable = model.IsValidity_Applicable;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.HRADocumentNamingMaster.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewDocumentNamingMaster");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<HraDocumentNamingMaster>(model));
            }
        }

        [HttpPost]
        public IActionResult DeleteHRDocumentNaming(string uuid)
        {
            try
            {
                HRA_DocumentNamingMaster MY = s.HRADocumentNamingMaster.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.HRADocumentNamingMaster.Update(MY);

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
            return RedirectToAction("ViewDocumentNamingMaster");
        }
        #endregion 


        public IActionResult MasterViewDocumentType()
        {
            return View();
        }
        public IActionResult MasterAddDocumentType()
        {
            return View();
        }
       
        public IActionResult AddDocumentNamingMaster()
        {
            return View();
        }
        public IActionResult ViewNomineeType()
        {
            return View();
        }
        public IActionResult AddNomineeType()
        {
            return View();
        }


        public IActionResult EditEmployeementtype()
        {
            return View();
        }
        public IActionResult ViewEmployeeLeaveAuthorization()
        {
            return View();
        }
        public IActionResult AddEmployeeLeaveAuthorization()
        {
            return View();
        }

        public IActionResult ViewEmployeeRelieving()
        {
            return View();
        }
        public IActionResult ViewEmployeesWeekOff()
        {
            return View();
        }
        public IActionResult AddEmployeesWeekOff()
        {
            return View();
        }

        public IActionResult ViewSalaryParameter()
        {
            return View();
        }
        public IActionResult AddSalaryParameter()
        {
            return View();
        }
        public IActionResult EditSalaryParameter()
        {
            return View();
        }
        public IActionResult SalaryPaySlipGeneration()
        {
            return View();
        }
        public IActionResult ViewIncomeTax()
        {
            return View();
        }
        public IActionResult AddIncomeTax()
        {
            return View();
        }
        public IActionResult EditIncomeTax()
        {
            return View();
        }
        public IActionResult ManageAttendence()
        {
            return View();
        }

        public IActionResult ViewITDeduction()
        {
            return View();
        }
        public IActionResult AddITDeduction()
        {
            return View();
        }

        public IActionResult ViewSubITDeduction()
        {
            return View();
        }
        public IActionResult AddSubITDeduction()
        {
            return View();
        }

        public IActionResult ViewITEmployeeParameter()
        {
            return View();
        }
        public IActionResult AddITEmployeeParameter()
        {
            return View();
        }

        public IActionResult CompanyWiseAttendanceReport()
        {
            return View();
        }
        public IActionResult EmployeeWiseAttendanceReport()
        {
            return View();
        }
        public IActionResult SalarySlipReport()
        {
            return View();
        }

        public IActionResult SalaryStatementReport()
        {
            return View();
        }

        public IActionResult BankStatementReport()
        {
            return View();
        }
        public IActionResult EmployeePayrollReport()
        {
            return View();
        }
        public IActionResult JoiningReport()
        {
            return View();
        }
        public IActionResult FuelStatementReport()
        {
            return View();
        }
        public IActionResult ProfesionalTaxReport()
        {
            return View();
        }
        public IActionResult EmployeeBankDetilsReport()
        {
            return View();
        }
        public IActionResult EmployeePFReport()
        {
            return View();
        }
        public IActionResult ApplyforLeave()
        {
            return View();
        }
        public IActionResult LeaveApproval()
        {
            return View();
        }

        public IActionResult ECRReport()
        {
            return View();
        }








    }
}