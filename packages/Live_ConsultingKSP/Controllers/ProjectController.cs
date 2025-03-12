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
using DataAccess.Logic;
using System.Data.Entity;


namespace Live_ConsultingKSP.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public ProjectController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Project Meeting

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetProjectMeetingData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectProjectMeeting.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
             c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();



            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Contact_Person_Name.Contains(searchValue) ||
                    i.Company_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();


            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.UUID),
                    "companyname" => query.OrderBy(i => i.Company_Name),
                    "contactpersonname" => query.OrderBy(i => i.Contact_Person_Name),
                    "meetingdate" => query.OrderBy(i => i.Meeting_Date),
                    "meetingtime" => query.OrderBy(i => i.Meeting_Time),
                    "attendes" => query.OrderBy(i => i.Attendees_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "companyname" => query.OrderByDescending(i => i.Company_Name),
                    "contactpersonname" => query.OrderByDescending(i => i.Contact_Person_Name),
                    "meetingdate" => query.OrderByDescending(i => i.Meeting_Date),
                    "meetingtime" => query.OrderByDescending(i => i.Meeting_Time),
                    "attendes" => query.OrderByDescending(i => i.Attendees_Name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Id)
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
                    uuid = $"<a href='/Project/MasterEditProjectMeeting/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    companyname = i.Company_Name,
                    contactpersonname = i.Contact_Person_Name,
                    meetingdate = i.Meeting_Date,
                    meetingtime = i.Meeting_Time,
                    attendes = i.Attendees_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });

        }

        [CheckCookie("UserUUID")]
        public IActionResult MasterViewProjectMeeting()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult MasterEditProjectMeeting(string UUID)
        {

            if (UUID == null)
            {

                ViewBag.industry = new SelectList(s.MasterIndustry.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Industry_Sector");
                var employees = s.MasterEmployee.Get()
               .Where(e => e.IsActive == true)
               .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
               .ToList();

                ViewBag.employee = new SelectList(employees, "UUID", "FullName");
                return View(new MasterManageBankDetail());
            }

            Project_ProjectMeeting MY = s.ProjectProjectMeeting.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

            if (MY == null)
            {
                return RedirectToAction("MasterViewProjectMeeting");
            }
            else
            {

                ProjectProjectMeeting MY1 = new ProjectProjectMeeting();
                var selectedEmployees = MY.Attendees_Name?.Split(',').Select(x => x.Trim()).ToList();
                ViewBag.industry = new SelectList(s.MasterIndustry.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Industry_Sector");
                ViewBag.EmployeeName = new SelectList(s.MasterEmployee.Get().Where(e => e.IsActive == true)
                        .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName }).ToList(), "UUID", "FullName", selectedEmployees);

                MY1 = _mapper.Map<ProjectProjectMeeting>(MY);
                return View("MasterAddProjectMeeting", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult MasterAddProjectMeeting(string UUID)
        {
            if (UUID == null)
            {
                ProjectProjectMeeting m = new ProjectProjectMeeting();
                m.IsActive = true;
                m.IsDisplay = true;

                ViewBag.industry = new SelectList(s.MasterIndustry.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Industry_Sector");
                var employees = s.MasterEmployee.Get()
                               .Where(e => e.IsActive == true)
                               .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                               .ToList();

                ViewBag.employee = new SelectList(employees, "UUID", "FullName");
                return View(m);
            }
            else
            {
                var ME = s.ProjectProjectMeeting.Get().Where(c => c.UUID == UUID && c.IsActive == true && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).FirstOrDefault();
                if (ME == null)
                {
                    return RedirectToAction("MasterViewProjectMeeting");
                }
                else
                {

                    ViewBag.industry = new SelectList(s.MasterIndustry.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Industry_Sector");
                    var selectedEmployees = ME.Attendees_Name?.Split(',').Select(x => x.Trim()).ToList();
                    ViewBag.employee = new SelectList(s.MasterEmployee.Get().Where(e => e.IsActive == true).Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName }).ToList(), "UUID", "FullName", selectedEmployees);

                    return View(ME);
                }
            }
        }
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddProjectMeeting(ProjectProjectMeeting model, IFormFile MeetingDocumentFile)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    if (MeetingDocumentFile != null)
                    {
                        model.Meeting_Document = HandleMeetingDocumentUpload(MeetingDocumentFile, model);
                    }
                    else if (!string.IsNullOrEmpty(model.UUID))
                    {
                        var existingRecord = s.ProjectProjectMeeting.Get().FirstOrDefault(c => c.UUID == model.UUID);
                        if (existingRecord != null)
                        {
                            model.Meeting_Document = existingRecord.Meeting_Document;
                        }
                    }


                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();


                    var selectedEmployeeUuids = Request.Form["Attendees_Name"].ToString().Split(',').Select(x => x.Trim()).ToList();


                    model.Attendees_Name = string.Join(",", selectedEmployeeUuids);


                    var employeeNames = s.MasterEmployee.Get()
                        .Where(e => selectedEmployeeUuids.Contains(e.UUID))
                        .Select(e => e.FirstName + " " + e.LastName)
                        .ToList();

                    // Join the employee names as a comma-separated string to store them in the model
                    model.Attendees_Name = string.Join(",", employeeNames);

                    if (string.IsNullOrEmpty(model.UUID))
                    {
                        var isDuplicate = s.ProjectProjectMeeting.Get()
                        .FirstOrDefault(c => c.Meeting_Time.ToString() == model.Meeting_Time.ToString() &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.IsActive == true);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.AddedIP = u.GetLocalIPAddress();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();

                            Project_ProjectMeeting project = _mapper.Map<Project_ProjectMeeting>(model);

                            s.ProjectProjectMeeting.Add(project);
                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewProjectMeeting");
                        }

                    }
                    else
                    {

                        var result = s.ProjectProjectMeeting.Get().FirstOrDefault(c => c.UUID == model.UUID);

                        var isDuplicate = s.ProjectProjectMeeting.Get()
                        .FirstOrDefault(c => c.Meeting_Time.ToString() == model.Meeting_Time.ToString() &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.IsActive == true);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            // Update the existing record
                            result.Company_Name = model.Company_Name;
                            result.Contact_Person_Name = model.Contact_Person_Name;
                            result.IndustrySector_UUID = model.IndustrySector_UUID;
                            result.Meeting_Date = model.Meeting_Date;
                            result.Meeting_Time = model.Meeting_Time;
                            result.Meeting_Document = model.Meeting_Document;
                            result.Attendees_UUID = model.Attendees_UUID;
                            result.Attendees_Name = model.Attendees_Name;
                            result.Description = model.Description;
                            result.IsDisplay = model.IsDisplay;
                            result.IsUpdatedOn = u.CurrentIndianTime();
                            result.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            result.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectProjectMeeting.Update(result);
                            //await _context.SaveChangesAsync();

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewProjectMeeting");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }
        }




        private string HandleMeetingDocumentUpload(IFormFile DocumentFile, ProjectProjectMeeting model)
        {
            if (DocumentFile != null)
            {
                if (DocumentFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "The uploaded document should not exceed 1 MB.");
                    return model.Meeting_Document;
                }

                var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
                var fileExtension = Path.GetExtension(DocumentFile.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "Only PDF, Word, and Excel file formats are allowed.");
                    return model.Meeting_Document;
                }

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectMeetingDocuments");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DocumentFile.CopyToAsync(stream);
                }

                return $"/ProjectMOMDocuments/{uniqueFileName}";
            }
            return model.Meeting_Document;
        }


        [CheckCookie("UserUUID")]
        public IActionResult DeleteProjectMeeting(string UUID)
        {
            try
            {
                Project_ProjectMeeting MY = s.ProjectProjectMeeting.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectProjectMeeting.Update(MY);

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
            return RedirectToAction("MasterViewProjectMeeting");
        }



        #endregion

        #region Create Project

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetCreateProjectData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectCreateProject.getCreateprojectSubModel();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                //i.Project_Title.Contains(searchValue) ||
                //i.Customer_UUID.Contains(searchValue) ||
                //i.Employee_UUID.Contains(searchValue));

                i.CTY.Project_Title.Contains(searchValue)).ToList();
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {


                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "title" => query.OrderBy(i => i.CTY.Project_Title).ToList(),
                    "customer" => query.OrderByDescending(i => i.CTY.Customer_UUID).ToList(),
                    "manager" => query.OrderByDescending(i => i.CTY.Employee_UUID).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }
            else
            {
                query = sortColumn switch
                {


                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "title" => query.OrderBy(i => i.CTY.Project_Title).ToList(),
                    "customer" => query.OrderByDescending(i => i.CTY.Customer_UUID).ToList(),
                    "manager" => query.OrderByDescending(i => i.CTY.Employee_UUID).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }
            var data = query
             .Skip(start)
             .Take(length).Where(i => i.CTY.IsActive == true)
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
                    uuid = $"<a href='/Project/EditCreateProject/{i.CTY.UUID}' class='btnEdit' target='_blank'>{i.CTY.UUID}</a>",
                    title = i.CTY.Project_Title,
                    customer = i.CustomerMaster_Name,
                    manager = i.FirstName,
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.CTY.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewCreateProject()
        {

            return View();
        }




        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> EditCreateProject(string UUID)
        {
            if (UUID == null)
            {
                ViewBag.cus = new SelectList(s.ACCustomerMaster.Get().Where(c => c.IsActive == true).ToList(), "UUID", "CustomerMaster_Name");
                ViewBag.emp = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                return View(new ProjectCreateProject());
            }

            Project_CreateProject MY = s.ProjectCreateProject.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies
            ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewCreateProject");
            }
            else
            {
                ViewBag.cus = new SelectList(s.ACCustomerMaster.Get().Where(c => c.IsActive == true).ToList(), "UUID", "CustomerMaster_Name");
                ViewBag.emp = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                ProjectCreateProject MY1 = new ProjectCreateProject();
                MY1 = _mapper.Map<ProjectCreateProject>(MY);
                return View("AddCreateProject", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddCreateProject(string UUID)
        {
            if (UUID == null)
            {
                ProjectCreateProject m = new ProjectCreateProject();
                m.IsActive = true;

                ViewBag.cus = new SelectList(s.ACCustomerMaster.Get().Where(c => c.IsActive == true).ToList(), "UUID", "CustomerMaster_Name");
                ViewBag.emp = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                return View(m);
            }
            else
            {
                var pro = s.ProjectCreateProject.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (pro == null)
                {
                    return RedirectToAction("ViewCreateProject");
                }
                else
                {
                    ViewBag.emp = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                    ViewBag.cus = new SelectList(s.ACCustomerMaster.Get().Where(c => c.IsActive == true).ToList(), "UUID", "CustomerMaster_Name");
                    return View(pro);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddCreateProject(ProjectCreateProject model)
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

                        var duplicateRecord = s.ProjectCreateProject.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Project_Title == model.Project_Title &&
                                c.Customer_UUID == model.Customer_UUID &&
                                c.Employee_UUID == model.Employee_UUID &&
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

                            Project_CreateProject p = _mapper.Map<Project_CreateProject>(model);
                            s.ProjectCreateProject.Add(p);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewCreateProject");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ProjectCreateProject.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ProjectCreateProject.Get()
                             .FirstOrDefault(c =>
                                 c.IsActive == true &&
                                 c.Project_Title == model.Project_Title &&
                                 c.Customer_UUID == model.Customer_UUID &&
                                 c.Employee_UUID == model.Employee_UUID &&
                                 c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ProjectCreateProject>(model));
                        }
                        else
                        {
                            existingRecord.Project_Title = model.Project_Title;
                            existingRecord.Customer_UUID = model.Customer_UUID;
                            existingRecord.Employee_UUID = model.Employee_UUID;
                            existingRecord.Project_Description = model.Project_Description;
                            existingRecord.Start_Date = model.Start_Date;
                            existingRecord.End_Date = model.End_Date;
                            existingRecord.Expected_Total_Hours = model.Expected_Total_Hours;
                            existingRecord.Project_Cost = model.Project_Cost;


                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectCreateProject.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewCreateProject");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ProjectCreateProject>(model));
            }


        }
        [CheckCookie("UserUUID")]
        public IActionResult DeleteCreateProject(string uuid)
        {
            try
            {
                Project_CreateProject MY = s.ProjectCreateProject.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectCreateProject.Update(MY);

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
            return RedirectToAction("ViewCreateProject");
        }


        #endregion


        #region Meeting MOM

        [HttpPost]

        public async Task<IActionResult> GetProjectMeetingMOMData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectProjectMOM.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
            .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Company.Contains(searchValue) ||
                    i.Attendees_from_Our_Company_Name.Contains(searchValue) ||
                    i.Meeting_Attendees_from_Client_Side.Contains(searchValue));



            }

            var totalRecords =  query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "companyname" => query.OrderBy(i => i.Company),
                    "meetingdate" => query.OrderBy(i => i.Meeting_Date),
                    "meetingtime" => query.OrderBy(i => i.Meeting_Time),
                    "attendesclientside" => query.OrderBy(i => i.Meeting_Attendees_from_Client_Side),
                    "attendescompanyside" => query.OrderBy(i => i.Attendees_from_Our_Company_Name),
                    "furthermeetingschedule" => query.OrderBy(i => i.Is_Any_Further_Meeting_Schedule),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "companyname" => query.OrderByDescending(i => i.Company),
                    "meetingdate" => query.OrderByDescending(i => i.Meeting_Date),
                    "meetingtime" => query.OrderByDescending(i => i.Meeting_Time),
                    "attendesclientside" => query.OrderByDescending(i => i.Meeting_Attendees_from_Client_Side),
                    "attendescompanyside" => query.OrderByDescending(i => i.Attendees_from_Our_Company_Name),
                    "furthermeetingschedule" => query.OrderByDescending(i => i.Is_Any_Further_Meeting_Schedule),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Id)
                };
            }

            var data =  query
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
                    uuid = $"<a href='/Project/EditMeetingMOM/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    companyname = i.Company,
                    meetingdate = i.Meeting_Date,
                    meetingtime = i.Meeting_Time,
                    attendesclientside = i.Meeting_Attendees_from_Client_Side,
                    attendescompanyside = i.Attendees_from_Our_Company_Name,
                    furthermeetingschedule = (bool)i.Is_Any_Further_Meeting_Schedule
                         ? "<span class='badge bg-success'>Yes</span>"
                         : "<span class='badge bg-danger'>No</span>",
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        public IActionResult ViewMeetingMOM()
        {
            //var model = _MeetingMOM.GetAllProjectMeetingMOMAsync(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
            return View(/*model*/);
        }
        [HttpGet]
        [CheckCookie("UserUUID")]

        public IActionResult EditMeetingMOM(string UUID)
        {
            if (UUID == null)
            {
                var employees = s.MasterEmployee.Get()
                               .Where(e => e.IsActive == true)
                               .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                               .ToList();

                ViewBag.EmployeeName = new SelectList(employees, "UUID", "FullName");

                return View(new ProjectProjectMom());
            }
            else
            {
                Project_ProjectMOM MY = s.ProjectProjectMOM.Get().Where(c => c.UUID == UUID && c.IsActive == true &&
                c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .OrderByDescending(c => c.Id).FirstOrDefault();

                if (MY == null)

                {
                    return RedirectToAction("ViewMeetingMOM");
                }
                else
                {
                    ProjectProjectMom MY1 = new ProjectProjectMom();
                    MY1 = _mapper.Map<ProjectProjectMom>(MY);
                    var selectedEmployees = MY.Attendees_from_Our_Company_Name?.Split(',').Select(x => x.Trim()).ToList();
                    ViewBag.EmployeeName = new SelectList(s.MasterEmployee.Get()
                        .Where(e => e.IsActive == true)
                        .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                        .ToList(), "UUID", "FullName", selectedEmployees);


                    return View("AddMeetingMOM", MY1);
                }
            }
          
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public IActionResult AddMeetingMOM(string UUID)
        {
            if (UUID == null)
            {
                ProjectProjectMom m = new ProjectProjectMom();
                m.IsActive = true;
                m.IsDisplay = true;

                var employees = s.MasterEmployee.Get()
                               .Where(e => e.IsActive == true)
                               .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                               .ToList();

                ViewBag.EmployeeName = new SelectList(employees, "UUID", "FullName");
                return View(m);
            }
            else 
            {
                var proj = s.ProjectProjectMOM.Get().Where(c => c.UUID == UUID && c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).FirstOrDefault();

                if (proj == null)

                {
                    return RedirectToAction("ViewMeetingMOM");
                }
                else
                { 
                var selectedEmployees = proj.Attendees_from_Our_Company_Name?.Split(',').Select(x => x.Trim()).ToList();
                    ViewBag.EmployeeName = new SelectList(s.MasterEmployee.Get()
                        .Where(e => e.IsActive == true)
                        .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                        .ToList(), "UUID", "FullName", selectedEmployees);


                    return View(proj);
                }
            }


        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddMeetingMOM(ProjectProjectMom model, IFormFile MeetingDocumentFile)
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
                    if (MeetingDocumentFile != null)
                    {
                        model.Meeting_Document = await HandleMeetingDocumentUpload(MeetingDocumentFile, model);
                    }
                    else if (!string.IsNullOrEmpty(model.UUID))
                    {
                        var existingRecord = s.ProjectProjectMOM.Get().FirstOrDefault(c => c.UUID == model.UUID);
                        if (existingRecord != null)
                        {
                            model.Meeting_Document = existingRecord.Meeting_Document;
                        }
                    }


                   
                    var selectedEmployeeUuids = Request.Form["Attendees_from_Our_Company_Name"].ToString().Split(',').Select(x => x.Trim()).ToList();


                    model.Attendees_from_Our_Company_UUID = string.Join(",", selectedEmployeeUuids);

                    // Store names as a comma-separated string
                    /*model.Attendees_from_Our_Company_Name = string.Join(",", selectedEmployeeUuids.Select(uuid =>
                        s.MasterEmployee.Get().FirstOrDefault(e => e.UUID == uuid)?.FirstName + " " +
                        s.MasterEmployee.Get().FirstOrDefault(e => e.UUID == uuid)?.LastName));*/
                    var employeeNames = s.MasterEmployee.Get()
                        .Where(e => selectedEmployeeUuids.Contains(e.UUID))
                        .Select(e => e.FirstName + " " + e.LastName)
                        .ToList();

                    // Join the employee names as a comma-separated string to store them in the model
                    model.Attendees_from_Our_Company_Name = string.Join(",", employeeNames);



                    if (string.IsNullOrEmpty(model.UUID))
                    {
                        var isDuplicate = s.ProjectProjectMOM.Get()
                        .FirstOrDefault(c => c.Company == model.Company &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.IsActive == true);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.AddedIP = u.GetLocalIPAddress();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            /*bool isDuplicate = s.ProjectProjectMOM.Get()
                               .Any(c => c.Company == model.Company && c.IsActive == true);*/
                            Project_ProjectMOM project = _mapper.Map<Project_ProjectMOM>(model);

                            s.ProjectProjectMOM.Add(project);
                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewMeetingMOM");
                        }

                    }
                    else
                    {

                        var result = s.ProjectProjectMOM.Get().FirstOrDefault(c => c.UUID == model.UUID);

                        var isDuplicate = s.ProjectProjectMOM.Get()
                          .FirstOrDefault(c =>
                              c.IsActive == true &&
                              c.Company == model.Company &&
                              c.Master_Company_UUID == model.Master_Company_UUID &&
                              c.Master_Environment_UUID == model.Master_Environment_UUID &&
                              c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            // Update the existing record
                            result.Company = model.Company;
                            result.Meeting_Date = model.Meeting_Date;
                            result.Meeting_Time = model.Meeting_Time;
                            result.Meeting_Type = model.Meeting_Type;
                            result.Attendees_from_Our_Company_UUID = model.Attendees_from_Our_Company_UUID;
                            result.Attendees_from_Our_Company_Name = model.Attendees_from_Our_Company_Name;
                            result.Meeting_Ajenda = model.Meeting_Ajenda;
                            result.Meeting_Document = model.Meeting_Document;
                            result.IsDisplay = model.IsDisplay;
                            result.IsUpdatedOn = u.CurrentIndianTime();
                            result.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            result.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectProjectMOM.Update(result);
                            //await _context.SaveChangesAsync();

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewMeetingMOM");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }

        }

        private async Task<string> HandleMeetingDocumentUpload(IFormFile MeetingDocumentFile, ProjectProjectMom model)
        {
            if (MeetingDocumentFile != null)
            {
                if (MeetingDocumentFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "The uploaded document should not exceed 1 MB.");
                    return model.Meeting_Document;
                }

                var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
                var fileExtension = Path.GetExtension(MeetingDocumentFile.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "Only PDF, Word, and Excel file formats are allowed.");
                    return model.Meeting_Document;
                }

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectMOMDocuments");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = u.GetUUID() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await MeetingDocumentFile.CopyToAsync(stream);
                }

                return $"/ProjectMOMDocuments/{uniqueFileName}";
            }

            return model.Meeting_Document;
        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteMOM(string uuid)
        {
            try
            {
                Project_ProjectMOM MY = s.ProjectProjectMOM.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectProjectMOM.Update(MY);

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

            return RedirectToAction("ViewMeetingMOM");
        }


        #endregion

        #region ViewProjectCredentials

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetProjectCredentialsData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "5");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectProjectCrendentials.getProjectCrendentialubModel();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                 i.CTY.Title.Contains(searchValue)).ToList();




            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "project" => query.OrderBy(i => i.CTY.Project_UUID).ToList(),
                    "title" => query.OrderByDescending(i => i.CTY.Title).ToList(),
                    "status" => query.OrderByDescending(i => i.CTY.IsDisplay).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "project" => query.OrderBy(i => i.CTY.Project_UUID).ToList(),
                    "title" => query.OrderByDescending(i => i.CTY.Title).ToList(),
                    "status" => query.OrderByDescending(i => i.CTY.IsDisplay).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }


            var data = query
            .Skip(start)
            .Take(length).Where(i => i.CTY.IsActive == true)
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
                    uuid = $"<a href='/Project/EditProjectCredentials/{i.CTY.UUID}' class='btnEdit' target='_blank'>{i.CTY.UUID}</a>",
                    project = i.Project_Title,
                    title = i.CTY.Credentials_Details,
                    status = (bool)i.CTY.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + i.CTY.UUID + "'>Delete</button>"
                })
            });
        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewProjectCredentials()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditProjectCredentials(string UUID)
        {

            if (UUID == null)
            {
                ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                    , "UUID", "Project_Title");
                return View(new ProjectProjectCrendential());

            }
            else
            {
                Project_ProjectCrendentials MY = s.ProjectProjectCrendentials.Get().Where(c => c.UUID == UUID && c.IsActive == true
                && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id)
                .FirstOrDefault();

                if (MY == null)

                {
                    return RedirectToAction("ViewProjectCredentials");
                }
                else
                {

                    ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                    , "UUID", "Project_Title");
                    ProjectProjectCrendential MY1 = new ProjectProjectCrendential();
                    MY1 = _mapper.Map<ProjectProjectCrendential>(MY);
                    return View("AddProjectCredentials", MY1);
                }
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddProjectCredentials(string UUID)
        {
            if (UUID == null)
            {
                ProjectProjectCrendential m = new ProjectProjectCrendential();
                m.IsActive = true;
                m.IsDisplay = true;

                ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                    , "UUID", "Project_Title");
                return View(m);
            }
            else
            {
                var proj = s.ProjectCreateProject.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
        c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (proj == null)

                {
                    return RedirectToAction("ViewProjectCredentials");
                }
                else
                {
                    ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Project_Title");
                    return View(proj);
                }
            }
        }
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddProjectCredentials(ProjectProjectCrendential model)
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

                        var duplicateRecord = s.ProjectProjectCrendentials.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Project_UUID == model.Project_UUID &&
                                c.Title == model.Title &&
                                c.Credentials_Details == model.Credentials_Details &&
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

                            Project_ProjectCrendentials p = _mapper.Map<Project_ProjectCrendentials>(model);
                            s.ProjectProjectCrendentials.Add(p);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectCredentials");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ProjectProjectCrendentials.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ProjectProjectCrendentials.Get()
                       .FirstOrDefault(c =>
                           c.IsActive == true &&
                           c.Project_UUID == model.Project_UUID &&
                           c.Title == model.Title &&
                           c.Credentials_Details == model.Credentials_Details &&
                           c.Master_Company_UUID == model.Master_Company_UUID &&
                           c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ProjectProjectCrendential>(model));
                        }
                        else
                        {
                            existingRecord.Project_UUID = model.Project_UUID;
                            existingRecord.Title = model.Title;
                            existingRecord.Credentials_Details = model.Credentials_Details;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectProjectCrendentials.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectCredentials");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ProjectProjectCrendential>(model));
            }
        }




        [CheckCookie("UserUUID")]
        public IActionResult DeleteProjectCredentials(string uuid)
        {
            try
            {
                Project_ProjectCrendentials MY = s.ProjectProjectCrendentials.Get().Where(c => c.IsActive == true && c.UUID ==
                uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID ==
                Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectProjectCrendentials.Update(MY);

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
            return RedirectToAction("ViewProjectCredentials");
        }





        #endregion


        #region Project Status

        [HttpPost]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> GetProjectStatusData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectProjectStatus.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Status_Title.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "projectstatus" => query.OrderBy(i => i.Status_Title),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "projectstatus" => query.OrderByDescending(i => i.Status_Title),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Id)
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
                    uuid = $"<a href='/Project/EditProjectStatus/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    projectstatus = i.Status_Title,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewProjectStatus()
        {
            return View();
        }



        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> EditProjectStatus(string UUID)
        {
            if (UUID == null)
            {
                return View(new ProjectProjectStatus());
            }

            Project_ProjectStatus MY = s.ProjectProjectStatus.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies
            ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewProjectStatus");
            }
            else
            {
                ProjectProjectStatus MY1 = new ProjectProjectStatus();
                MY1 = _mapper.Map<ProjectProjectStatus>(MY);
                return View("AddProjectStatus", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddProjectStatus(string UUID)
        {
            if (UUID == null)
            {
                ProjectProjectStatus m = new ProjectProjectStatus();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var p = s.ProjectProjectStatus.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
                .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (p == null)
                {
                    return RedirectToAction("ViewProjectStatus");
                }
                else
                {
                    return View(p);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddProjectStatus(ProjectProjectStatus model)
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

                        var duplicateRecord = s.ProjectProjectStatus.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Status_Title == model.Status_Title &&
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

                            Project_ProjectStatus p = _mapper.Map<Project_ProjectStatus>(model);
                            s.ProjectProjectStatus.Add(p);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectStatus");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ProjectProjectStatus.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ProjectProjectStatus.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Status_Title == model.Status_Title &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ProjectProjectStatus>(model));
                        }
                        else
                        {
                            existingRecord.Status_Title = model.Status_Title;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectProjectStatus.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectStatus");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ProjectProjectStatus>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteStatus(string uuid)
        {
            try
            {
                Project_ProjectStatus MY = s.ProjectProjectStatus.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
                .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectProjectStatus.Update(MY);

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
            return RedirectToAction("ViewProjectStatus");

        }


        #endregion

        #region Project Phase


        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetProjectPhase()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectCreateProjectPhase.getprojectphaseSubModel();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.CM.Project_Title.Contains(searchValue) ||
                    i.PM.Phase_Name.Contains(searchValue) ||
                    i.SM.FirstName.Contains(searchValue) ||
                    i.SM.LastName.Contains(searchValue)).ToList();

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.PM.UUID).ToList(),
                    "projecttitle" => query.OrderBy(i => i.CM.Project_Title).ToList(),
                    "phasename" => query.OrderByDescending(i => i.PM.Phase_Name).ToList(),
                    _ => query.OrderBy(i => i.PM.Id).ToList()
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.PM.UUID).ToList(),
                    "projecttitle" => query.OrderByDescending(i => i.CM.Project_Title).ToList(),
                    "phasename" => query.OrderByDescending(i => i.PM.Phase_Name).ToList(),
                    _ => query.OrderByDescending(i => i.PM.Id).ToList()
                };
            }

            var data = query
             .Skip(start)
             .Take(length).Where(i => i.PM.IsActive == true)
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
                    uuid = $"<a href='/Project/EditProjectPhase/{i.PM.UUID}' class='btnEdit' target='_blank'>{i.PM.UUID}</a>",
                    projecttitle = i.Project_Title,
                    phasename = i.PM.Phase_Name,
                    phaselead = i.FirstName,

                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.PM.UUID + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewProjectPhase()
        {
            /*var phaseentities = s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).ToList();

            var phaseModels = phaseentities.Select(c => new ProjectCreateProjectPhase
            {
                UUID = c.UUID,
                Project_UUID = c.Project_UUID,
                Phase_Name = c.Phase_Name,
                Employee_UUID = c.Employee_UUID,                
                IsActive = (bool)c.IsActive,
                IsAddedOn = c.IsAddedOn,
                IsAddedBy = c.IsAddedBy
            }).ToList();*/
            return View(/*phaseModels*/);
        }



        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditProjectPhase(string UUID)
        {
            var employees = s.MasterEmployee.Get()
                               .Where(e => e.IsActive == true)
                               .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                               .ToList();
            if (UUID == null)
            {
                ViewBag.EmployeeName = new SelectList(employees, "UUID", "FullName");
                ViewBag.proj = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Project_Title");
                ViewBag.emp = new SelectList(employees, "UUID", "FullName");
                return View(new ProjectCreateProjectPhase());
            }

            Project_CreateProjectPhase MY = s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewProjectPhase");
            }
            else
            {
                ViewBag.proj = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Project_Title");
                ViewBag.emp = new SelectList(employees, "UUID", "FullName"); 

                var selectedEmployees = MY.Select_Team_Member_Name?.Split(',').Select(x => x.Trim()).ToList();
                ViewBag.EmployeeName = new SelectList(employees, "UUID", "FullName", selectedEmployees);
                ProjectCreateProjectPhase MY1 = new ProjectCreateProjectPhase();
                MY1 = _mapper.Map<ProjectCreateProjectPhase>(MY);

                return View("AddProjectPhase", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddProjectPhase(string UUID)
        {
            if (UUID == null)
            {
                ProjectCreateProjectPhase m = new ProjectCreateProjectPhase();
                m.IsActive = true;
                // m.IsDisplay = true;
                var employees = s.MasterEmployee.Get()
                                .Where(e => e.IsActive == true)
                                .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                                .ToList();

                ViewBag.EmployeeName = new SelectList(employees, "UUID", "FullName");
                ViewBag.proj = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Project_Title");
                ViewBag.emp = new SelectList(employees, "UUID", "FullName");
                return View(m);
            }
            else
            {
                var phase = s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (phase == null)
                {
                    return RedirectToAction("ViewProjectPhase");
                }
                else
                {
                    var selectedEmployees = phase.Select_Team_Member_Name?.Split(',').Select(x => x.Trim()).ToList();
                    ViewBag.EmployeeName = new SelectList(s.MasterEmployee.Get()
                        .Where(e => e.IsActive == true)
                        .Select(e => new { e.UUID, FullName = e.FirstName + " " + e.LastName })
                        .ToList(), "UUID", "FullName", selectedEmployees);
                    ViewBag.proj = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList(), "UUID", "Project_Title");
                    ViewBag.emp = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                    return View(phase);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddProjectPhase(ProjectCreateProjectPhase model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var selectedEmployeeUuids = Request.Form["Select_Team_Member_Name"].ToString().Split(',').Select(x => x.Trim()).ToList();
                    model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                    model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                    model.Select_Team_Member_UUID = string.Join(",", selectedEmployeeUuids);


                    var employeeNames = s.MasterEmployee.Get()
                        .Where(e => selectedEmployeeUuids.Contains(e.UUID))
                        .Select(e => e.FirstName + " " + e.LastName)
                        .ToList();

                    
                    model.Select_Team_Member_Name = string.Join(",", employeeNames);


                    if (string.IsNullOrEmpty(model.UUID))
                    {

                        var duplicateRecord = s.ProjectCreateProjectPhase.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Phase_Name == model.Phase_Name &&
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

                            Project_CreateProjectPhase phase = _mapper.Map<Project_CreateProjectPhase>(model);
                            s.ProjectCreateProjectPhase.Add(phase);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectPhase");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ProjectCreateProjectPhase.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ProjectCreateProjectPhase.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Phase_Name == model.Phase_Name &&
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
                            existingRecord.Phase_Name = model.Phase_Name;
                            existingRecord.Project_UUID = model.Project_UUID;
                            existingRecord.Project_Description = model.Project_Description;
                            existingRecord.Phase_Cost = model.Phase_Cost;
                            existingRecord.Employee_UUID = model.Employee_UUID;
                            existingRecord.Select_Team_Member_UUID = model.Select_Team_Member_UUID;
                            existingRecord.Select_Team_Member_Name = model.Select_Team_Member_Name;
                            existingRecord.Start_Date = model.Start_Date;
                            existingRecord.End_Date = model.End_Date;
                            existingRecord.Phase_Expected_Hours = model.Phase_Expected_Hours;

                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectCreateProjectPhase.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectPhase");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }
        }


        [CheckCookie("UserUUID")]
        public IActionResult DeletePhase(string uuid)
        {
            try
            {
                Project_CreateProjectPhase MY = s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectCreateProjectPhase.Update(MY);

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
            return RedirectToAction("ViewProjectPhase");
        }


        #endregion
        public IActionResult ViewProjectStep()
        {
            return View();
        }
        public IActionResult AddProjectStep()
        {
            return View();
        }

        #region Project Task

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetCreateProjectTaskData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.ProjectProjectTask.getProjectTaskSubModel();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>



                i.Project_Title.Contains(searchValue)).ToList();
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {


                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "project" => query.OrderBy(i => i.CTY.Project_UUID).ToList(),
                    "projectphase" => query.OrderByDescending(i => i.CTY.ProjectPhase_UUID).ToList(),
                    "task" => query.OrderByDescending(i => i.CTY.Task_Title).ToList(),
                    "expectedtaskhours" => query.OrderByDescending(i => i.CTY.Expected_Task_Hours).ToList(),
                    "status" => query.OrderByDescending(i => i.CTY.IsDisplay).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }
            else
            {
                query = sortColumn switch
                {



                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "project" => query.OrderBy(i => i.CTY.Project_UUID).ToList(),
                    "projectphase" => query.OrderByDescending(i => i.CTY.ProjectPhase_UUID).ToList(),
                    "task" => query.OrderByDescending(i => i.CTY.Task_Title).ToList(),
                    "expectedtaskhours" => query.OrderByDescending(i => i.CTY.Expected_Task_Hours).ToList(),
                    "status" => query.OrderByDescending(i => i.CTY.IsDisplay).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }
            var data = query
         .Skip(start)
         .Take(length).Where(i => i.CTY.IsActive == true)
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
                    uuid = $"<a href='/Project/EditProjectTask/{i.CTY.UUID}' class='btnEdit' target='_blank'>{i.CTY.UUID}</a>",
                    project = i.Project_Title,
                    projectphase = i.Phase_Name,
                    task = i.CTY.Task_Title,
                    expectedtaskhours = i.CTY.Expected_Task_Hours,
                    status = (bool)i.CTY.IsDisplay
                     ? "<span class='badge bg-success'>Visible</span>"
                     : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.CTY.UUID + "'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult ViewProjectTask()
        {
            return View();
        }
        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> EditProjectTask(string UUID)
        {

            if (UUID == null)
            {
                ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                    , "UUID", "Project_Title");

                ViewBag.prophase = new SelectList(s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true).ToList()
                   , "UUID", "Phase_Name");
                return View(new ProjectProjectTask());

            }
            else
            {
                Project_ProjectTask MY = s.ProjectProjectTask.Get().Where(c => c.UUID == UUID && c.IsActive == true
                && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id)
                .FirstOrDefault();

                if (MY == null)

                {
                    return RedirectToAction("ViewProjectTask");
                }
                else
                {

                    ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                      , "UUID", "Project_Title");

                    ViewBag.prophase = new SelectList(s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true).ToList()
                       , "UUID", "Phase_Name");

                    ProjectProjectTask MY1 = new ProjectProjectTask();
                    MY1 = _mapper.Map<ProjectProjectTask>(MY);
                    return View("AddProjectTask", MY1);
                }
            }
        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddProjectTask(string UUID)
        {
            if (UUID == null)
            {
                ProjectProjectTask m = new ProjectProjectTask();
                m.IsActive = true;
                m.IsDisplay = true;

                ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                        , "UUID", "Project_Title");

                ViewBag.prophase = new SelectList(s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true).ToList()
                   , "UUID", "Phase_Name");

                return View(m);
            }
            else
            {
                var proj = s.ProjectProjectTask.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
        c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (proj == null)

                {
                    return RedirectToAction("ViewProjectTask");
                }
                else
                {
                    ViewBag.project = new SelectList(s.ProjectCreateProject.Get().Where(c => c.IsActive == true).ToList()
                      , "UUID", "Project_Title");

                    ViewBag.prophase = new SelectList(s.ProjectCreateProjectPhase.Get().Where(c => c.IsActive == true).ToList()
                       , "UUID", "Phase_Name");

                    return View(proj);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddProjectTask(ProjectProjectTask model)
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

                        var duplicateRecord = s.ProjectProjectTask.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Project_UUID == model.Project_UUID &&
                                c.ProjectPhase_UUID == model.ProjectPhase_UUID &&
                                c.Task_Title == model.Task_Title &&
                                 c.Expected_Task_Hours == model.Expected_Task_Hours &&
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

                            Project_ProjectTask p = _mapper.Map<Project_ProjectTask>(model);
                            s.ProjectProjectTask.Add(p);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectTask");
                        }



                    }
                    else
                    {

                        var existingRecord = s.ProjectProjectTask.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.ProjectProjectTask.Get()
                       .FirstOrDefault(c =>
                          c.IsActive == true &&
                                c.Project_UUID == model.Project_UUID &&
                                c.ProjectPhase_UUID == model.ProjectPhase_UUID &&
                                c.Task_Title == model.Task_Title &&
                                 c.Expected_Task_Hours == model.Expected_Task_Hours &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                               c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<ProjectProjectTask>(model));
                        }
                        else
                        {
                            existingRecord.Project_UUID = model.Project_UUID;
                            existingRecord.ProjectPhase_UUID = model.ProjectPhase_UUID;
                            existingRecord.Task_Title = model.Task_Title;
                            existingRecord.Expected_Task_Hours = model.Expected_Task_Hours;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.ProjectProjectTask.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewProjectTask");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<ProjectProjectTask>(model));
            }
        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteprojectTask(string uuid)
        {
            try
            {
                Project_ProjectTask MY = s.ProjectProjectTask.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID ==
                Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending
                (c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.ProjectProjectTask.Update(MY);

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
            return RedirectToAction("ViewProjectTask");
        }

        #endregion

        public IActionResult ManageProjectResource()
        {
            return View();
        }
        public IActionResult ViewTaskTimeLine()
        {
            return View();
        }
        public IActionResult AddTaskTimeLine()
        {
            return View();
        }
        public IActionResult ManageResourceCosting()
        {
            return View();
        }
        public IActionResult ViewProjectClosure()
        {
            return View();
        }
        public IActionResult AddProjectClosure()
        {
            return View();
        }
        public IActionResult ViewProjectPhaseClosure()
        {
            return View();
        }
        public IActionResult AddProjectPhaseClosure()
        {
            return View();
        }
        public IActionResult ViewProjectTaskClosure()
        {
            return View();
        }
        public IActionResult AddProjectTaskClosure()
        {
            return View();
        }

    }
}
