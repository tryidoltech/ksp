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
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public ProjectController(IMapper mapper)
        {
            _mapper = mapper;
        }

     



        #region Project Closure

        /*[HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetProjectClosure()
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
                i.CTY.Project_Title.ToLower().Contains(searchValue.ToLower()) ||
                i.CustomerMaster_Name.ToLower().Contains(searchValue.ToLower()) ||
                i.FirstName.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "des")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "projectname" => query.OrderBy(i => i.CTY.Project_Title).ToList(),
                    "customername" => query.OrderBy(i => i.CustomerMaster_Name).ToList(),
                    "projectmanager" => query.OrderBy(i => i.FirstName).ToList(),
                    //"projectprogress" => query.OrderBy(i => i.).ToList(),                                     
                    "status" => query.OrderBy(i => i.CTY.Project_Status_UUID).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };

            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.CTY.UUID).ToList(),
                    "projectname" => query.OrderByDescending(i => i.CTY.Project_Title).ToList(),
                    "customername" => query.OrderByDescending(i => i.CustomerMaster_Name).ToList(),
                    "projectmanager" => query.OrderByDescending(i => i.FirstName).ToList(),
                    //"projectprogress" => query.OrderByDescending(i => i.CB.City_UUID).ToList(),                    
                    "status" => query.OrderByDescending(i => i.CTY.Project_Status_UUID).ToList(),
                    _ => query.OrderByDescending(i => i.CTY.Id).ToList()
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
                    uuid = $"<a href='/Project/AddProjectClosure/{i.CTY.UUID}' class='btnEdit' target='_blank'>{i.CTY.UUID}</a>",
                    projectname = i.CTY.Project_Title,
                    customername = i.CustomerMaster_Name,
                    projectmanager = i.FirstName,
                    //projectprogress = i.Branch_Address,
                    status = i.CTY.Project_Status_UUID
                      ? "<span class='badge bg-warning'>Not Updated</span>"
                      : "<span class='badge bg-success'>Updated</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.CTY.UUID + "'>Delete</button>"

                })
            });
        }
        public IActionResult ViewProjectClosure()
        {
            return View();
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult AddProjectClosure(string UUID)
        {
            if (UUID == null)
            {            
                return View(new ProjectCreateProject());
            }
            else
            {
                Project_CreateProject PC = s.ProjectCreateProject.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (PC == null)
                {
                    return RedirectToAction("ViewProjectClosure");
                }
                else
                {
                    ProjectCreateProject MY1 = new ProjectCreateProject();
                    MY1 = _mapper.Map<ProjectCreateProject>(PC);
                    return View("MasterAddYear", MY1);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddProjectClosure(ProjectCreateProject model)
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
                    
                    var existingRecord = s.ProjectCreateProject.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                    var duplicateRecord = s.ProjectCreateProject.Get()
                        .FirstOrDefault(c =>
                            c.IsActive == true &&
                            c.Actual_Start_Date == model.Actual_Start_Date &&
                            c.Actual_End_Date == model.Actual_End_Date &&
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
                        existingRecord.Actual_Start_Date = model.Actual_Start_Date;
                        existingRecord.Actual_End_Date = model.Actual_End_Date;
                        existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                        existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                        existingRecord.UpdatedIP = u.GetLocalIPAddress();

                        s.ProjectCreateProject.Update(existingRecord);

                        TempData["Message"] = "Data Updated Successfully!";
                        TempData["MessageType"] = "success";
                        return RedirectToAction("ViewProjectClosure");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }
        }*/

        #endregion

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
