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
using DataAccess.Model;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Live_ConsultingKSP.Controllers
{
    public class CompanySetupController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public CompanySetupController(IMapper mapper)
        {
            _mapper = mapper;
        }

        

      
      


       


       

        #region Manage Rights

        [CheckCookie("UserUUID")]
        public IActionResult ManageRights()
        {
            string navmenu = "";
            var roles = s.MasterUserRole.Get().Where(c => c.IsActive == true).ToList();
            ViewBag.Roles = new SelectList(roles, "Uuid", "UserRoleName");
            List<Master_Menu> menuRecords = s.MasterMenu.Get().Where(c => c.IsActive == true).ToList();

            var Mainmenu = menuRecords.Where(mr => mr.MenuLevel == 1).OrderBy(c => c.Sequence).ToList();
            for (int i = 0; i < Mainmenu.Count; i++)
            {
                if ((bool)Mainmenu[i].IsParent)
                {
                    string check = "";
                    check += "<li class='d-flex align-items-center level-one'><div class='me-4' style='min-width: 150px;'><div class='form-check'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='menu_" + Mainmenu[i].Uuid + "' /><label>" + Mainmenu[i].MenuName + "</label></div></div>";
                    check += "<div class='d-flex'><div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='read_" + Mainmenu[i].Uuid + "'/><label style = 'color:brown'> Read </ label></div>";
                    check += "<div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='write_" + Mainmenu[i].Uuid + "'/><label style = 'color:green'> Write </ label></div>";
                    check += "<div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='edit_" + Mainmenu[i].Uuid + "'/><label style = 'color:blue'> Edit </ label></div>";
                    check += "<div class='form-check'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='delete_" + Mainmenu[i].Uuid + "'/><label style = 'color:red'> Delete </ label></div></div></ li>";

                    navmenu += check;
                    var SubMenu = menuRecords.Where(mr => mr.MenuLevel == 2 && mr.MainParentUUID == Mainmenu[i].Uuid.ToString()).OrderBy(c => c.Sequence).ToList();
                    for (int k = 0; k < SubMenu.Count(); k++)
                    {

                        if ((bool)SubMenu[k].IsParent)
                        {
                            string check1 = "";
                            check1 += "<li class='d-flex align-items-center level-two'><div class='me-4' style='min-width: 150px;'><div class='form-check'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + "' type='checkbox' id='menu_" + SubMenu[k].Uuid + "' /><label>" + SubMenu[k].MenuName + "</label></div></div>";
                            check1 += "<div class='d-flex'><div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + "' type='checkbox' id='read_" + SubMenu[k].Uuid + "'/><label style = 'color:brown'> Read </ label></div>";
                            check1 += "<div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + "' type='checkbox' id='write_" + SubMenu[k].Uuid + "'/><label style = 'color:green'> Write </ label></div>";
                            check1 += "<div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + "' type='checkbox' id='edit_" + SubMenu[k].Uuid + "'/><label style = 'color:blue'> Edit </ label></div>";
                            check1 += "<div class='form-check'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + "' type='checkbox' id='delete_" + SubMenu[k].Uuid + "'/><label style = 'color:red'> Delete </ label></div></div></ li>";

                            navmenu += check1;
                            var SubSubMenu = menuRecords.Where(mr => mr.MenuLevel == 3 && mr.SubParentUUID == SubMenu[k].Uuid && mr.MainParentUUID == Mainmenu[i].Uuid.ToString()).OrderBy(c => c.Sequence).ToList();
                            for (int j = 0; j < SubSubMenu.Count; j++)
                            {
                                string check2 = "";
                                check2 += "<li class='d-flex align-items-center level-three'><div class='me-4' style='min-width: 150px;'><div class='form-check'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + " SSMchk" + SubSubMenu[j].MenuId + "' type='checkbox' id='menu_" + SubSubMenu[j].Uuid + "' /><label>" + SubSubMenu[j].MenuName + "</label></div></div>";
                                check2 += "<div class='d-flex'><div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + " SSMchk" + SubSubMenu[j].MenuId + "' type='checkbox' id='read_" + SubSubMenu[j].Uuid + "'/><label style = 'color:brown'> Read </ label></div>";
                                check2 += "<div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + " SSMchk" + SubSubMenu[j].MenuId + "' type='checkbox' id='write_" + SubSubMenu[j].Uuid + "'/><label style = 'color:green'> Write </ label></div>";
                                check2 += "<div class='form-check me-3'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + " SSMchk" + SubSubMenu[j].MenuId + "' type='checkbox' id='edit_" + SubSubMenu[j].Uuid + "'/><label style = 'color:blue'> Edit </ label></div>";
                                check2 += "<div class='form-check'><input class='form-check-input riht Mchk" + Mainmenu[i].MenuId + " SMchk" + SubMenu[k].MenuId + " SSMchk" + SubSubMenu[j].MenuId + "' type='checkbox' id='delete_" + SubSubMenu[j].Uuid + "'/><label style = 'color:red'> Delete </ label></div></div></ li>";

                                navmenu += check2;

                            }

                        }
                        else
                        {
                            string check1 = "";
                            check1 += "<li class='d-flex align-items-center level-two'><div class='me-4' style='min-width: 150px;'><div class='form-check'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='menu_" + SubMenu[k].Uuid + "' /><label>" + SubMenu[k].MenuName + "</label></div></div>";
                            check1 += "<div class='d-flex'><div class='form-check me-3'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='read_" + SubMenu[k].Uuid + "'/><label style = 'color:brown'> Read </ label></div>";
                            check1 += "<div class='form-check me-3'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='write_" + SubMenu[k].Uuid + "'/><label style = 'color:green'> Write </ label></div>";
                            check1 += "<div class='form-check me-3'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='edit_" + SubMenu[k].Uuid + "'/><label style = 'color:blue'> Edit </ label></div>";
                            check1 += "<div class='form-check'><input class='form-check-input Mchk" + Mainmenu[i].MenuId + "' type='checkbox' id='delete_" + SubMenu[k].Uuid + "'/><label style = 'color:red'> Delete </ label></div></div></ li>";

                            navmenu += check1;
                        }
                    }
                }
                else
                {
                    string check = "";
                    check += "<li class='d-flex align-items-center level-one'><div class='me-4' style='min-width: 150px;'><div class='form-check'><input class='form-check-input' type='checkbox' id='menu_" + Mainmenu[i].Uuid + "' /><label>" + Mainmenu[i].MenuName + "</label></div></div>";
                    check += "<div class='d-flex'><div class='form-check me-3'><input class='form-check-input' type='checkbox' id='read_" + Mainmenu[i].Uuid + "'/><label style = 'color:brown'> Read </ label></div>";
                    check += "<div class='form-check me-3'><input class='form-check-input' type='checkbox' id='write_" + Mainmenu[i].Uuid + "'/><label style = 'color:green'> Write </ label></div>";
                    check += "<div class='form-check me-3'><input class='form-check-input' type='checkbox' id='edit_" + Mainmenu[i].Uuid + "'/><label style = 'color:blue'> Edit </ label></div>";
                    check += "<div class='form-check'><input class='form-check-input' type='checkbox' id='delete_" + Mainmenu[i].Uuid + "'/><label style = 'color:red'> Delete </ label></div></div></ li>";

                    navmenu += check;
                }
            }


            ViewBag.Menus = navmenu;
            return View();
            /* 
             var roles = s.MasterUserRole.Get().Where(c => c.IsActive == true).ToList();
             ViewBag.Roles = new SelectList(roles, "Uuid", "UserRoleName", roleUuid);

             List<Master_User_MenuRight> MUM = s.MasterUserMenuRight.Get()
              .Where(c => c.UserRoleUuid == roleUuid && c.IsActive == true)
              .ToList();

             List<MasterUserMenuRight> MY1 = _mapper.Map<List<MasterUserMenuRight>>(MUM);

             ViewBag.Menus = menus;
             ViewBag.UserMenuRights = MUM;
             return View(MY1);*/
        }


        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult SaveMenuRights([FromBody] RoleRightsViewModel model)
        {

            if (model == null || model.RoleUuid == Guid.Empty)
            {
                return BadRequest("Invalid data.");
            }

            List<Master_User_MenuRight> MUM = s.MasterUserMenuRight.Get()
                .Where(mr => mr.UserRoleUuid == model.RoleUuid.ToString())
                .ToList();
            if (MUM.Count > 0)
            {
                for (int i = 0; i < MUM.Count; i++)
                {
                    s.MasterUserMenuRight.Delete(MUM[i]);
                }
            }


            foreach (var right in model.Rights)
            {
                Master_User_MenuRight MUMR = new Master_User_MenuRight();
                MUMR.Uuid = u.GetUUID();
                MUMR.UserRoleUuid = model.RoleUuid.ToString();
                MUMR.CompanyUuid = "-";
                MUMR.EnvironmentUuid = "-";
                MUMR.MenuUuid = right.Uuid;
                MUMR.IsRead = right.IsRead;
                MUMR.IsWrite = right.IsWrite;
                MUMR.IsEdit = right.IsEdit;
                MUMR.IsDelete = right.IsDelete;
                MUMR.IsActive = true;
                MUMR.IsDisplay = true;
                MUMR.IsAdddedOn = u.CurrentIndianTime();
                MUMR.AddedIP = u.GetLocalIPAddress();
                MUMR.RecordNo = u.GetRecordNo();
                MUMR.IsAddedBy = Request.Cookies["UserUUID"].ToString();

                s.MasterUserMenuRight.Add(MUMR);

            }

            return Ok();
        }


        [HttpGet]
        public IActionResult GetMenuRightslsty(string roleUuid)
        {
            if (roleUuid != "0")
            {

                var menus = s.MasterMenu.Get().Where(c => c.IsActive == true).ToList();


                List<Master_User_MenuRight> rights = s.MasterUserMenuRight.Get().Where(r => r.UserRoleUuid.ToLower() == roleUuid.ToLower() && r.IsActive == true).ToList();
                if (rights.Count > 0)
                {
                    var menuRights = menus.Select(menu => new
                    {
                        uuid = menu.Uuid,
                        menuName = menu.MenuName,
                        isRead = rights.Any(r => r.MenuUuid == menu.Uuid) ? rights.First(r => r.MenuUuid == menu.Uuid).IsRead : false,
                        isWrite = rights.Any(r => r.MenuUuid == menu.Uuid) ? rights.First(r => r.MenuUuid == menu.Uuid).IsWrite : false,
                        isEdit = rights.Any(r => r.MenuUuid == menu.Uuid) ? rights.First(r => r.MenuUuid == menu.Uuid).IsEdit : false,
                        isDelete = rights.Any(r => r.MenuUuid == menu.Uuid) ? rights.First(r => r.MenuUuid == menu.Uuid).IsDelete : false
                    }).ToList();

                    return Json(menuRights);
                }
                else
                {
                    return Json("tst");
                }

            }
            else
            {
                return Json("");
            }

            /*var rights = s.MasterUserMenuRight.Get()
            .Where(r => r.UserRoleUuid == roleUuid && r.IsActive == true)
            .Select(r => new
            {
                uuid = r.MenuUuid,
                isRead = r.IsRead,
                isWrite = r.IsWrite,
                isEdit = r.IsEdit,
                isDelete = r.IsDelete
            })
            .ToList();

            return Json(rights);*/
        }
        #endregion

        #region Login & Logout

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login login)
        {

            Master_Employee user = s.MasterEmployee.Get().Where(c => c.Username == login.UserName
                                                  && c.Password == login.Password
                                                  && c.IsLoginActive == true
                                                  && c.IsActive == true).OrderByDescending(c => c.Id).FirstOrDefault();

            if (user != null)
            {
                CookieOptions options = new CookieOptions
                {
                    Domain = "",
                    Expires = DateTime.Now.AddDays(7),
                    Path = "/",
                    Secure = true,
                    HttpOnly = true,
                    MaxAge = TimeSpan.FromDays(7),
                    IsEssential = true
                };
                Response.Cookies.Append("UserUUID", user.UUID.ToString(), options);
                Response.Cookies.Append("RoleUUID", user.Master_Roles_UUID.ToString(), options);
                return RedirectToAction("dashboardcrm");

            }
            TempData["ErrorMessage"] = "Invalid Username or Password.";
            return View();
        }
        // Login POST
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login login)
        {
            var user = await _MasterEmployeeServices.GetEmployeeForLoginAsync(login.UserName, login.Password);


            if (user != null)
            {
                var employee = await _context.MasterEmployees
            .FirstOrDefaultAsync(u => u.Username == login.UserName && u.Password == login.Password);
                if (employee.IsActive == true && employee.IsLoginActive == true)
                {
                    var userRole = _context.MasterUserRoles.FirstOrDefault(r => r.Uuid == employee.MasterRolesUuid.ToString());

                    var menuRights = _context.MasterUserMenuRights
                        .Where(mr => mr.UserRoleUuid == employee.MasterRolesUuid && mr.IsActive == true)
                        .ToList();
                    CookieOptions options = new CookieOptions
                    {
                        Domain = "", // Set the domain for the cookie
                        Expires = DateTime.Now.AddDays(7), // Set expiration date to 7 days from now
                        Path = "/", // Cookie is available within the entire application
                        Secure = true, // Ensure the cookie is only sent over HTTPS
                        HttpOnly = true, // Prevent client-side scripts from accessing the cookie
                        MaxAge = TimeSpan.FromDays(7), // Another way to set the expiration time
                        IsEssential = true // Indicates the cookie is essential for the application to function
                    };
                    Response.Cookies.Append("UserUUID", employee.Uuid.ToString(), options);
                    Response.Cookies.Append("RoleUUID", employee.MasterRolesUuid.ToString(), options);
                    //Response.Cookies.Append("Role", userRole?.UserRoleName.ToString(), options);
                    return RedirectToAction("dashboardcrm");
                }
            }
            TempData["ErrorMessage"] = "Invalid Username or Password.";
            return View();
        }*/

        //public async Task<IActionResult> Logout()
        //{
        //    Response.Cookies.Delete("UserUUID", null);
        //    Response.Cookies.Delete("RoleUUID", null);
        //    Response.Cookies.Delete("Role", null);
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public IActionResult Logout(string id)
        {
            Response.Cookies.Delete("UserUUID");
            Response.Cookies.Delete("RoleUUID");
            Response.Cookies.Delete("Role");
            Response.Cookies.Delete("CmpUUID");
            Response.Cookies.Delete("EnvUUID");

            return Json("Success");
        }

        [CheckCookie("UserUUID")]
        [HttpGet]
        public IActionResult GetEnvLst(string id)
        {
            CookieOptions options = new CookieOptions
            {
                Domain = "", // Set the domain for the cookie
                Expires = DateTime.Now.AddDays(7), // Set expiration date to 7 days from now
                Path = "/", // Cookie is available within the entire application
                Secure = true, // Ensure the cookie is only sent over HTTPS
                HttpOnly = true, // Prevent client-side scripts from accessing the cookie
                MaxAge = TimeSpan.FromDays(7), // Another way to set the expiration time
                IsEssential = true // Indicates the cookie is essential for the application to function
            };
            Master_Employee ME = s.MasterEmployee.Get().Where(c => c.IsActive == true && c.UUID == Request.Cookies["UserUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            string[] array = ME.Allowed_Environment_UUID.ToLower().Split(',');
            if (id != "0")
            {
                Response.Cookies.Append("EnvUUID", id, options);

                var items = "";


                var Mainmenu = s.MasterEnvironment.Get().Where(c => c.IsActive == true).ToList();
                if (Mainmenu.Count > 0)
                {
                    for (int i = 0; i < Mainmenu.Count; i++)
                    {
                        if (array.Contains(Mainmenu[i].Uuid.ToLower()))
                        {
                            if (Mainmenu[i].Uuid.ToString() == id)
                            {
                                items += "<option value=\"" + Mainmenu[i].Uuid + "\" selected='selected'>" + Mainmenu[i].EnvironmentName + "</option>";
                            }
                            else
                            {
                                items += "<option value=\"" + Mainmenu[i].Uuid + "\" >" + Mainmenu[i].EnvironmentName + "</option>";
                            }
                        }

                    }
                }

                return Json(items);
            }
            else
            {


                var items = "";

                var Mainmenu = s.MasterEnvironment.Get().Where(c => c.IsActive == true).ToList();
                if (Mainmenu.Count > 0)
                {
                    if (Request.Cookies["EnvUUID"] != null)
                    {
                        for (int i = 0; i < Mainmenu.Count; i++)
                        {
                            if (array.Contains(Mainmenu[i].Uuid.ToLower()))
                            {
                                if (Mainmenu[i].Uuid.ToString() == Request.Cookies["EnvUUID"].ToString())
                                {
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" selected='selected'>" + Mainmenu[i].EnvironmentName + "</option>";
                                }
                                else
                                {
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" >" + Mainmenu[i].EnvironmentName + "</option>";
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Mainmenu.Count; i++)
                        {
                            if (array.Contains(Mainmenu[i].Uuid.ToLower()))
                            {
                                if (i == 0)
                                {
                                    Response.Cookies.Append("EnvUUID", Mainmenu[i].Uuid, options);
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" selected='selected'>" + Mainmenu[i].EnvironmentName + "</option>";
                                }
                                else
                                {
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" >" + Mainmenu[i].EnvironmentName + "</option>";
                                }
                            }
                        }
                    }

                }

                return Json(items);
            }
        }

        [CheckCookie("UserUUID")]
        [HttpGet]
        public IActionResult GetCompanyLst(string id)
        {
            CookieOptions options = new CookieOptions
            {
                Domain = "", // Set the domain for the cookie
                Expires = DateTime.Now.AddDays(7), // Set expiration date to 7 days from now
                Path = "/", // Cookie is available within the entire application
                Secure = true, // Ensure the cookie is only sent over HTTPS
                HttpOnly = true, // Prevent client-side scripts from accessing the cookie
                MaxAge = TimeSpan.FromDays(7), // Another way to set the expiration time
                IsEssential = true // Indicates the cookie is essential for the application to function
            };
            Master_Employee ME = s.MasterEmployee.Get().Where(c => c.IsActive == true && c.UUID == Request.Cookies["UserUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            string[] array = ME.Allowed_Company_UUID.ToLower().Split(',');
            if (id != "0")
            {
                Response.Cookies.Append("CmpUUID", id, options);

                var items = "";

                var Mainmenu = s.MasterCompany.Get().Where(c => c.IsActive == true).ToList();
                if (Mainmenu.Count > 0)
                {
                    for (int i = 0; i < Mainmenu.Count; i++)
                    {
                        if (array.Contains(Mainmenu[i].Uuid.ToLower()))
                        {
                            if (Mainmenu[i].Uuid.ToString() == id)
                            {
                                items += "<option value=\"" + Mainmenu[i].Uuid + "\" selected='selected'>" + Mainmenu[i].CompanyName + "</option>";
                            }
                            else
                            {
                                items += "<option value=\"" + Mainmenu[i].Uuid + "\" >" + Mainmenu[i].CompanyName + "</option>";
                            }
                        }
                    }
                }

                return Json(items);
            }
            else
            {


                var items = "";

                var Mainmenu = s.MasterCompany.Get().Where(c => c.IsActive == true).ToList();
                if (Mainmenu.Count > 0)
                {
                    if (Request.Cookies["CmpUUID"] != null)
                    {
                        for (int i = 0; i < Mainmenu.Count; i++)
                        {
                            if (array.Contains(Mainmenu[i].Uuid.ToLower()))
                            {
                                if (Mainmenu[i].Uuid.ToString() == Request.Cookies["CmpUUID"].ToString())
                                {
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" selected='selected'>" + Mainmenu[i].CompanyName + "</option>";
                                }
                                else
                                {
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" >" + Mainmenu[i].CompanyName + "</option>";
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Mainmenu.Count; i++)
                        {
                            if (array.Contains(Mainmenu[i].Uuid.ToLower()))
                            {
                                if (i == 0)
                                {
                                    Response.Cookies.Append("CmpUUID", Mainmenu[i].Uuid, options);

                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" selected='selected'>" + Mainmenu[i].CompanyName + "</option>";
                                }
                                else
                                {
                                    items += "<option value=\"" + Mainmenu[i].Uuid + "\" >" + Mainmenu[i].CompanyName + "</option>";
                                }
                            }
                        }
                    }

                }

                return Json(items);
            }

        }




        [CheckCookie("UserUUID")]
        [HttpGet]
        public IActionResult GetMenu(int id)
        {
            string? UserUUID = Request.Cookies["UserUUID"].ToString();
            string? RoleUUID = Request.Cookies["RoleUUID"].ToString();
            //string? Role = Request.Cookies["Role"].ToString();

            //var user = _context.MasterUsers.FirstOrDefaultAsync(u => u.Uuid.ToString()== UserUUID);

            //var userRole = _context.MasterUserRoles.FirstOrDefault(r => r.Uuid == user.RoleUuid);

            //var menuRights = _context.MasterUserMenuRights
            //    .Where(mr => mr.UserRoleUuid.ToString() == RoleUUID && mr.IsActive)
            //    .ToList();

            //var menu = _context.MasterMenus
            //    .Where(mr => mr.IsDisplay && mr.IsActive)
            //    .ToList();


            var menuRecords = s.MasterUserMenuRight.getMenuRecordsModel(RoleUUID);


            //var Mainmenu = menuRecords.Where(mr => mr.MenuLevel==1).ToList();
            //var SubMenu = menuRecords.Where(mr => mr.MenuLevel == 2).ToList();
            //var SubSubMenu = menuRecords.Where(mr => mr.MenuLevel == 3).ToList();

            string navmenu = "";
            //StringBuilder SBMenu = new StringBuilder();

            //if (menuRecords.Count() > 0)
            //{
            var Mainmenu = menuRecords.Where(mr => mr.MenuLevel == 1).OrderBy(c => c.Sequence).ToList();
            for (int i = 0; i < Mainmenu.Count; i++)
            {
                if ((bool)Mainmenu[i].IsParent)
                {
                    navmenu += "<li class='nav-item'><a href='#" + Mainmenu[i].Uuid + "' class='nav-link' data-bs-toggle='collapse' role='button' aria-expanded='false' aria-controls='" + Mainmenu[i].Uuid + "' data-key='t-projects'>" + Mainmenu[i].MenuName + "</a>";

                    navmenu += "<div class='collapse menu-dropdown' id='" + Mainmenu[i].Uuid + "'><ul class='nav nav-sm flex-column'>";

                    var SubMenu = menuRecords.Where(mr => mr.MenuLevel == 2 && mr.MainParentUuid == Mainmenu[i].Uuid.ToString()).OrderBy(c => c.Sequence).ToList();
                    for (int k = 0; k < SubMenu.Count; k++)
                    {
                        if ((bool)SubMenu[k].IsParent)
                        {
                            navmenu += "<li class='nav-item'><a href='#" + SubMenu[k].Uuid + "' class='nav-link' data-bs-toggle='collapse' role='button' aria-expanded='false' aria-controls='" + SubMenu[k].Uuid + "' data-key='t-projects'>" + SubMenu[k].MenuName + "</a>";

                            navmenu += "<div class='collapse menu-dropdown' id='" + SubMenu[k].Uuid + "'><ul class='nav nav-sm flex-column'>";

                            var SubSubMenu = menuRecords.Where(mr => mr.MenuLevel == 3 && mr.SubParentUuid == SubMenu[k].Uuid && mr.MainParentUuid == Mainmenu[i].Uuid.ToString()).OrderBy(c => c.Sequence).ToList();
                            for (int j = 0; j < SubSubMenu.Count; j++)
                            {

                                navmenu += "<li class='nav-item'><a href='" + SubSubMenu[j].Url + "' class='nav-link' data-key='t-mailbox'>" + SubSubMenu[j].MenuName + "</a></li>";

                            }
                            navmenu += "</ul></div></li>";
                        }
                        else
                        {
                            navmenu += "<li class='nav-item'><a href='" + SubMenu[k].Url + "' class='nav-link'  role='button' aria-expanded='false' aria-controls='" + SubMenu[k].Uuid + "' data-key='t-projects'>" + SubMenu[k].MenuName + "</a></li>";


                        }


                    }

                    navmenu += "</ul></div></li>";
                }
                else
                {
                    navmenu += "<li class='nav-item'><a href='#" + Mainmenu[i].Uuid + "' class='nav-link' role='button' aria-expanded='false' aria-controls='" + Mainmenu[i].Uuid + "' data-key='t-projects'>" + Mainmenu[i].MenuName + "</a></li>";


                }


            }

            //}





            var response = new
            {
                navmenu = navmenu,
                menuRecords = menuRecords
            };


            return Json(response);
        }

        [CheckCookie("UserUUID")]
        [HttpGet]
        public IActionResult GetMenuRights(int id)
        {
            string? UserUUID = Request.Cookies["UserUUID"].ToString();
            string? RoleUUID = Request.Cookies["RoleUUID"].ToString();
            //string? Role = Request.Cookies["Role"].ToString();

            //var user = _context.MasterUsers.FirstOrDefaultAsync(u => u.Uuid.ToString()== UserUUID);

            //var userRole = _context.MasterUserRoles.FirstOrDefault(r => r.Uuid == user.RoleUuid);

            //var menuRights = _context.MasterUserMenuRights
            //    .Where(mr => mr.UserRoleUuid.ToString() == RoleUUID && mr.IsActive)
            //    .ToList();

            //var menu = _context.MasterMenus
            //    .Where(mr => mr.IsDisplay && mr.IsActive)
            //    .ToList();

            var menuRecords = s.MasterUserMenuRight.getMenuRecordsModel(RoleUUID);
            //var menuRecords = from menu in _context.MasterMenus
            //                  join menuRight in _context.MasterUserMenuRights
            //                  on menu.Uuid.ToString() equals menuRight.MenuUuid
            //                  where menuRight.UserRoleUuid.ToString() == RoleUUID
            //                  select new
            //                  {
            //                      Url = menu.Url,
            //                      MenuName = menu.MenuName,
            //                      MenuIcon = menu.MenuIcon,
            //                      MenuLevel = menu.MenuLevel,
            //                      MainParentUuid = menu.MainParentUuid,
            //                      SubParentUuid = menu.SubParentUuid,
            //                      Sequence = menu.Sequence,
            //                      IsParent = menu.IsParent,
            //                      Uuid = menu.Uuid,
            //                      UserRoleUuid = menuRight.UserRoleUuid,
            //                      IsRead = menuRight.IsRead,
            //                      IsWrite = menuRight.IsWrite,
            //                      IsEdit = menuRight.IsEdit,
            //                      IsDelete = menuRight.IsDelete

            //                  };


            //var Mainmenu = menuRecords.Where(mr => mr.MenuLevel==1).ToList();
            //var SubMenu = menuRecords.Where(mr => mr.MenuLevel == 2).ToList();
            //var SubSubMenu = menuRecords.Where(mr => mr.MenuLevel == 3).ToList();

            string navmenu = "";
            //StringBuilder SBMenu = new StringBuilder();

            //if (menuRecords.Count() > 0)
            //{
            var Mainmenu = menuRecords.Where(mr => mr.MenuLevel == 1).OrderBy(c => c.Sequence).ToList();
            for (int i = 0; i < Mainmenu.Count; i++)
            {
                if ((bool)Mainmenu[i].IsParent)
                {
                    navmenu += "<li class='nav-item'><a href='#" + Mainmenu[i].Uuid + "' class='nav-link' data-bs-toggle='collapse' role='button' aria-expanded='false' aria-controls='" + Mainmenu[i].Uuid + "' data-key='t-projects'>" + Mainmenu[i].MenuName + "</a>";

                    navmenu += "<div class='collapse menu-dropdown' id='" + Mainmenu[i].Uuid + "'><ul class='nav nav-sm flex-column'>";

                    var SubMenu = menuRecords.Where(mr => mr.MenuLevel == 2 && mr.MainParentUuid == Mainmenu[i].Uuid.ToString()).OrderBy(c => c.Sequence).ToList();
                    for (int k = 0; k < SubMenu.Count; k++)
                    {
                        if ((bool)SubMenu[k].IsParent)
                        {
                            navmenu += "<li class='nav-item'><a href='#" + SubMenu[k].Uuid + "' class='nav-link' data-bs-toggle='collapse' role='button' aria-expanded='false' aria-controls='" + SubMenu[k].Uuid + "' data-key='t-projects'>" + SubMenu[k].MenuName + "</a>";

                            navmenu += "<div class='collapse menu-dropdown' id='" + SubMenu[k].Uuid + "'><ul class='nav nav-sm flex-column'>";

                            var SubSubMenu = menuRecords.Where(mr => mr.MenuLevel == 3 && mr.SubParentUuid == SubMenu[k].Uuid && mr.MainParentUuid == Mainmenu[i].Uuid.ToString()).OrderBy(c => c.Sequence).ToList();
                            for (int j = 0; j < SubSubMenu.Count; j++)
                            {

                                navmenu += "<li class='nav-item'><a href='" + SubSubMenu[j].Url + "' class='nav-link' data-key='t-mailbox'>" + SubSubMenu[j].MenuName + "</a></li>";

                            }
                            navmenu += "</ul></div></li>";
                        }
                        else
                        {
                            navmenu += "<li class='nav-item'><a href='#" + SubMenu[k].Uuid + "' class='nav-link'  role='button' aria-expanded='false' aria-controls='" + SubMenu[k].Uuid + "' data-key='t-projects'>" + SubMenu[k].MenuName + "</a></li>";


                        }


                    }

                    navmenu += "</ul></div></li>";
                }
                else
                {
                    navmenu += "<li class='nav-item'><a href='#" + Mainmenu[i].Uuid + "' class='nav-link' role='button' aria-expanded='false' aria-controls='" + Mainmenu[i].Uuid + "' data-key='t-projects'>" + Mainmenu[i].MenuName + "</a></li>";


                }


            }

            var response = new
            {
                navmenu = navmenu,
                menuRecords = menuRecords
            };


            return Json(response);
        }

        #endregion

      
        [CheckCookie("UserUUID")]
        public IActionResult dashboardcrm()
        {
            return View();
        }




    }
}
