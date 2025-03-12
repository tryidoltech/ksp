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
    public class CompanySetupController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public CompanySetupController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region CompanyName

        [HttpPost]
        public async Task<IActionResult> GetMasterCompanyData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterCompany.Get().Where(c => c.IsActive == true).AsQueryable();



            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(c =>
                   c.CompanyName.Contains(searchValue) ||
                   c.ContactPersonName_Sales.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "companyName" => query.OrderBy(c => c.CompanyName),
                    "gstinNumber" => query.OrderBy(c => c.GSTIN_Number),
                    "contactname" => query.OrderBy(c => c.ContactPersonName_Sales),
                    "mobileNumber" => query.OrderBy(c => c.Mobile_Number),
                    "emailIdSales" => query.OrderBy(c => c.Email_Id_Sales),
                    "signature" => query.OrderBy(c => c.Signature),
                    "isDisplay" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(c => c.CompanyId),
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "companyName" => query.OrderByDescending(c => c.CompanyName),
                    "gstinNumber" => query.OrderByDescending(c => c.GSTIN_Number),
                    "contactname" => query.OrderByDescending(c => c.ContactPersonName_Sales),
                    "mobileNumber" => query.OrderByDescending(c => c.Mobile_Number),
                    "emailIdSales" => query.OrderByDescending(c => c.Email_Id_Sales),
                    "signature" => query.OrderByDescending(c => c.Signature),
                    "isDisplay" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(c => c.CompanyId),
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
                data = data.Select(c => new
                {
                    srno = srNo++,
                    uuid = $"<a href='/CompanySetup/MasterEditCompanyName/{c.Uuid}' class='btnEdit' target='_blank'>{c.Uuid}</a>",
                    companyName = c.CompanyName,
                    gstinNumber = c.GSTIN_Number,
                    contactname = c.ContactPersonName_Sales,
                    mobileNumber = c.Mobile_Number,
                    emailIdSales = c.Email_Id_Sales,
                    signature = Url.Content($"~{c.Signature}"),
                    isDisplay = (bool)c.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = $"<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='{c.Uuid}'>Delete</button>"
                })
            });
        }





        public async Task<IActionResult> MasterViewCompanyName()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MasterEditCompanyName(string uuid)
        {
            Master_Company company;


            ViewBag.GeneratedUuid = uuid ?? u.GetUUID();

            if (string.IsNullOrEmpty(uuid))
            {
                company = new Master_Company
                {
                    Uuid = ViewBag.GeneratedUuid
                };
            }
            else
            {
                company = s.MasterCompany.Get().Where(c => c.Uuid == uuid).FirstOrDefault();
                if (company == null) return NotFound();
            }

            var countries = s.MasterCountry.Get().Where(c => c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString() && c.IsActive == true).ToList();
            ViewBag.Country = countries.Select(c => new SelectListItem
            {
                Value = c.UUID,
                Text = c.CountryName
            }).ToList();

            MasterCompany MY1 = new MasterCompany();
            MY1 = _mapper.Map<MasterCompany>(company);

            return View("MasterAddCompanyName", MY1);
        }

        [HttpGet]

        public async Task<IActionResult> MasterAddCompanyName(string uuid)
        {

            Master_Company company;


            ViewBag.GeneratedUuid = uuid ?? u.GetUUID();

            if (string.IsNullOrEmpty(uuid))
            {
                company = new Master_Company
                {
                    Uuid = ViewBag.GeneratedUuid
                };
            }
            else
            {

                company = s.MasterCompany.Get().Where(c => c.Uuid == uuid).FirstOrDefault();
                if (company == null) return NotFound();
            }


            var countries = s.MasterCountry.Get().Where(c => c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString() && c.IsActive == true).ToList();
            ViewBag.Country = countries.Select(c => new SelectListItem
            {
                Value = c.UUID,
                Text = c.CountryName
            }).ToList();


            MasterCompany MY1 = new MasterCompany();
            MY1 = _mapper.Map<MasterCompany>(company);

            return View(MY1);
        }

        [HttpPost]
        public async Task<IActionResult> MasterAddCompanyName(MasterCompany company, IFormFile logoFile, IFormFile stampFile, IFormFile signatureFile)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }
            if (string.IsNullOrEmpty(company.Uuid) || !s.MasterCompany.Get().Any(c => c.Uuid == company.Uuid))
            {
                company.Uuid = u.GetUUID();
                company.IsActive = true;
                company.AddedIP = u.GetLocalIPAddress();
                company.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                company.IsAdddedOn = u.CurrentIndianTime();

            }
            else
            {
                // If editing, retrieve the existing company
                var existingCompany = s.MasterCompany.Get().FirstOrDefault(c => c.IsActive == true &&
                                     c.CompanyName == company.CompanyName && c.Uuid != company.Uuid);
                if (existingCompany != null)
                {
                    // Update existing company properties
                    existingCompany.CompanyName = company.CompanyName;
                    existingCompany.Company_ShortName = company.Company_ShortName;
                    existingCompany.GSTIN_Number = company.GSTIN_Number;
                    existingCompany.ContactPersonName_Sales = company.ContactPersonName_Sales;
                    existingCompany.ContactPersonName_Support = company.ContactPersonName_Support;
                    existingCompany.DateOf_Establishment = company.DateOf_Establishment.HasValue ? company.DateOf_Establishment.Value.ToDateTime(TimeOnly.MinValue) : null;
                    existingCompany.Email_Id_Sales = company.Email_Id_Sales;
                    existingCompany.Email_Id_Support = company.Email_Id_Support;
                    existingCompany.Email_Id_Personal = company.Email_Id_Personal;
                    existingCompany.CountryName_UUID = company.CountryName_UUID;
                    existingCompany.StateName_UUID = company.StateName_UUID;
                    existingCompany.CityName_UUID = company.CityName_UUID;
                    existingCompany.Address1 = company.Address1;
                    existingCompany.Address2 = company.Address2;
                    existingCompany.Phone_Number = company.Phone_Number;
                    existingCompany.Alternate_PhoneNumber = company.Alternate_PhoneNumber;
                    existingCompany.Mobile_Number = company.Mobile_Number;
                    existingCompany.Alternate_Mobile_Number = company.Alternate_Mobile_Number;
                    existingCompany.URL1 = company.URL1;
                    existingCompany.URL2 = company.URL2;
                    existingCompany.IsDisplay = company.IsDisplay;
                    existingCompany.IsActive = true;
                    existingCompany.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                    existingCompany.IsUpdatedOn = u.CurrentIndianTime();

                    _mapper.Map(company, existingCompany);
                    company = _mapper.Map<MasterCompany>(existingCompany);
                }
            }

            // Define the folder to store the uploaded files
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Process and save logoFile
            if (logoFile != null && logoFile.Length > 0)
            {
                string uniqueLogoFileName = u.GetUUID() + Path.GetExtension(logoFile.FileName);
                string logoFilePath = Path.Combine(uploadsFolder, uniqueLogoFileName);

                using (var stream = new FileStream(logoFilePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(stream);
                }

                // Save the relative path to the database
                company.Logo = $"/images/{uniqueLogoFileName}";
            }

            // Process and save stampFile
            if (stampFile != null && stampFile.Length > 0)
            {
                string uniqueStampFileName = u.GetUUID() + Path.GetExtension(stampFile.FileName);
                string stampFilePath = Path.Combine(uploadsFolder, uniqueStampFileName);

                using (var stream = new FileStream(stampFilePath, FileMode.Create))
                {
                    await stampFile.CopyToAsync(stream);
                }

                // Save the relative path to the database
                company.Stamp = $"/images/{uniqueStampFileName}";
            }

            // Process and save signatureFile
            if (signatureFile != null && signatureFile.Length > 0)
            {
                string uniqueSignatureFileName = u.GetUUID() + Path.GetExtension(signatureFile.FileName);
                string signatureFilePath = Path.Combine(uploadsFolder, uniqueSignatureFileName);

                using (var stream = new FileStream(signatureFilePath, FileMode.Create))
                {
                    await signatureFile.CopyToAsync(stream);
                }

                // Save the relative path to the database
                company.Signature = $"/images/{uniqueSignatureFileName}";
            }

            // Save the company record to the database
            if (string.IsNullOrEmpty(company.Uuid))
            {
                var companyEntity = _mapper.Map<Master_Company>(company);
                s.MasterCompany.Add(companyEntity);
            }
            else
            {
                var companyEntity = _mapper.Map<Master_Company>(company); // Convert to entity
                s.MasterCompany.Update(companyEntity);

            }

            TempData["Message"] = "Data saved successfully!";
            TempData["MessageType"] = "success";
            return RedirectToAction("MasterViewCompanyName");
        }



        public IActionResult DeleteCompany(string Uuid)
        {
            try
            {
                Master_Company MC = s.MasterCompany.Get().Where(c => c.IsActive == true && c.Uuid == Uuid).FirstOrDefault();
                MC.IsActive = false;
                MC.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                MC.IsDeletedOn = u.CurrentIndianTime();
                MC.DeletedIP = u.GetLocalIPAddress();
                s.MasterCompany.Update(MC);
                TempData["Message"] = "Data Deleted Successfully!";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("MasterViewCompanyName"); // Redirect to view
        }

        #endregion


        #region Master Company Type

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetMasterCompanyTypeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterCompanyType.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].
            ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CompanyType_id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Company_Type.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "companytype" => query.OrderBy(i => i.Company_Type),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.CompanyType_id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "companytype" => query.OrderByDescending(i => i.Company_Type),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.CompanyType_id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditCompanyType/{i.UUID}' class='btnEdit'target='_blank'>{i.UUID}</a>",
                    companytype = i.Company_Type,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewCompanyType()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditCompanyType(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterCompanyType());
            }

            Master_CompanyType MY = s.MasterCompanyType.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies
            ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CompanyType_id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewCompanyType");
            }
            else
            {
                MasterCompanyType MY1 = new MasterCompanyType();
                MY1 = _mapper.Map<MasterCompanyType>(MY);
                return View("MasterAddCompanyType", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddCompanyType(string UUID)
        {
            if (UUID == null)
            {
                MasterCompanyType m = new MasterCompanyType();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var companytype = s.MasterCompanyType.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CompanyType_id).FirstOrDefault();

                if (companytype == null)
                {
                    return RedirectToAction("MasterViewCompanyType");
                }
                else
                {
                    return View(companytype);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddCompanyType(MasterCompanyType model)
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
                        var isDuplicate = s.MasterCompanyType.Get()
                        .FirstOrDefault(c => c.IsActive == true &&
                                   c.Company_Type == model.Company_Type &&
                                   c.Master_Company_UUID == model.Master_Company_UUID &&
                                   c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                   c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterCompanyType>(model));
                        }
                        else
                        {


                            model.IsActive = true;
                            model.AddedIP = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();


                            Master_CompanyType MY = new Master_CompanyType();
                            MY = _mapper.Map<Master_CompanyType>(model);
                            s.MasterCompanyType.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewCompanyType");
                        }
                    }
                    else
                    {
                        var existingRecord = s.MasterCompanyType.Get()
                           .FirstOrDefault(c => c.UUID == model.UUID);

                        var isDuplicate = s.MasterCompanyType.Get()
                         .FirstOrDefault(c => c.IsActive == true &&
                                  c.Company_Type == model.Company_Type &&
                                  c.Master_Company_UUID == model.Master_Company_UUID &&
                                  c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                  c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterCompanyType>(model));
                        }
                        else
                        {
                            existingRecord.Company_Type = model.Company_Type;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();


                            s.MasterCompanyType.Update(existingRecord);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("MasterViewCompanyType");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterCompanyType>(model));
            }


        }
        [CheckCookie("UserUUID")]
        public IActionResult DeleteCompanyType(String uuid)
        {
            try
            {
                Master_CompanyType MY = s.MasterCompanyType.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CompanyType_id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterCompanyType.Update(MY);

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
            return RedirectToAction("MasterViewCompanyType");
        }



        #endregion


        #region Master Environment

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetMasterEnvironmentData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterEnvironment.Get().Where(c => c.IsActive == true).OrderByDescending(c => c.EnvironmentId).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.EnvironmentName.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.Uuid),
                    "environmentname" => query.OrderBy(i => i.EnvironmentName),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.EnvironmentId)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.Uuid),
                    "environmentname" => query.OrderByDescending(i => i.EnvironmentName),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.EnvironmentId)
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
                    uuid = $"<a href='/CompanySetup/MasterEditEnvironment/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    environmentname = i.EnvironmentName,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewEnvironment()
        {
            return View();
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditEnvironment(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterEnvironment());
            }

            Master_Environment MY = s.MasterEnvironment.Get().Where(c => c.IsActive == true && c.Uuid == UUID
            .ToString()).OrderByDescending(c => c.EnvironmentId).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewEnvironment");
            }
            else
            {
                MasterEnvironment MY1 = new MasterEnvironment();
                MY1 = _mapper.Map<MasterEnvironment>(MY);
                return View("MasterAddEnvironment", MY1);
            }
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddEnvironment(string UUID)
        {
            if (UUID == null)
            {
                MasterEnvironment m = new MasterEnvironment();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var environmentname = s.MasterEnvironment.Get().Where(c => c.IsActive == true && c.Uuid == UUID
                .ToString()).OrderByDescending(c => c.EnvironmentId).FirstOrDefault();

                if (environmentname == null)
                {
                    return RedirectToAction("MasterViewEnvironment");
                }
                else
                {
                    return View(environmentname);
                }

            }
        }
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddEnvironment(MasterEnvironment model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterEnvironment.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.EnvironmentName == model.EnvironmentName);
                               

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
                            model.IsAdddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.Uuid = u.GetUUID();

                            Master_Environment environmentname = _mapper.Map<Master_Environment>(model);
                            s.MasterEnvironment.Add(environmentname);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewEnvironment");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterEnvironment.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterEnvironment.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.EnvironmentName == model.EnvironmentName &&
                               
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterEnvironment>(model));
                        }
                        else
                        {
                            existingRecord.EnvironmentName = model.EnvironmentName;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterEnvironment.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewEnvironment");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterEnvironment>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteEnvironment(string uuid)
        {
            try
            {
                Master_Environment MY = s.MasterEnvironment.Get().Where(c => c.IsActive == true && c.Uuid == uuid
                .ToString()).OrderByDescending(c => c.EnvironmentId).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterEnvironment.Update(MY);

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
            return RedirectToAction("MasterViewEnvironment");
        }



        #endregion


        #region Master Year

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetMasterYearData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterYear.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Year_id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Year_name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.Uuid),
                    "yearname" => query.OrderBy(i => i.Year_name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Year_id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.Uuid),
                    "yearname" => query.OrderByDescending(i => i.Year_name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Year_id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditYear/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    yearname = i.Year_name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewYear()
        {
            return View();
        }



        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditYear(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterYear());
            }

            Master_Year MY = s.MasterYear.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Year_id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewYear");
            }
            else
            {
                MasterYear MY1 = new MasterYear();
                MY1 = _mapper.Map<MasterYear>(MY);
                return View("MasterAddYear", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> MasterAddYear(string UUID)
        {
            if (UUID == null)
            {
                MasterYear m = new MasterYear();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var year = s.MasterYear.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Year_id).FirstOrDefault();

                if (year == null)
                {
                    return RedirectToAction("MasterViewYear");
                }
                else
                {
                    return View(year);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddYear(MasterYear model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterYear.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Year_name == model.Year_name &&
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
                            model.AddedIp = u.GetLocalIPAddress();
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                            model.RecordNo = u.GetRecordNo();
                            model.Uuid = u.GetUUID();

                            Master_Year newYear = _mapper.Map<Master_Year>(model);
                            s.MasterYear.Add(newYear);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewYear");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterYear.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterYear.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Year_name == model.Year_name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterYear>(model));
                        }
                        else
                        {
                            existingRecord.Year_name = model.Year_name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterYear.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewYear");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterYear>(model));
            }
        }


        [CheckCookie("UserUUID")]
        public IActionResult Deleteyear(string uuid)
        {
            try
            {
                Master_Year MY = s.MasterYear.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Year_id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterYear.Update(MY);

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
            return RedirectToAction("MasterViewYear");
        }


        #endregion


        #region Master Bank Type

        [HttpPost]

        public async Task<IActionResult> GetMasterBankTypeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterBanktype.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                     i.Bank_name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.Uuid),
                    "bankname" => query.OrderBy(i => i.Bank_name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Bank_id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.Uuid),
                    "bankname" => query.OrderByDescending(i => i.Bank_name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Bank_id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditBankType/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    bankname = i.Bank_name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewBankType()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditBankType(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterBanktype());
            }
            Master_Banktype MY = s.MasterBanktype.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
            .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).FirstOrDefault();
          
            if (MY == null)
            {
                return RedirectToAction("MasterViewBankType");
            }
            else
            {
                MasterBanktype MY1 = new MasterBanktype();
                MY1 = _mapper.Map<MasterBanktype>(MY);
                return View("MasterAddBankType", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddBankType(string UUID)
        {
            if (UUID == null)
            {

                MasterBanktype m = new MasterBanktype();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var bank = s.MasterBanktype.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).FirstOrDefault();
                if (bank == null)
                {
                    return RedirectToAction("MasterViewBankType");
                }
                else
                {
                    return View(bank);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddBankType(MasterBanktype model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterBanktype.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Bank_name == model.Bank_name &&
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
                            model.Uuid = u.GetUUID();

                            Master_Banktype bank = _mapper.Map<Master_Banktype>(model);
                            s.MasterBanktype.Add(bank);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewBankType");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterBanktype.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterBanktype.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Bank_name == model.Bank_name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterBanktype>(model));
                        }
                        else
                        {
                            existingRecord.Bank_name = model.Bank_name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterBanktype.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewBankType");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterYear>(model));
            }


        }
        public IActionResult Deletebanktype(string uuid)
        {
            try
            {
                Master_Banktype MY = s.MasterBanktype.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterBanktype.Update(MY);

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
            return RedirectToAction("MasterViewBankType");
        }


        #endregion



        #region Master BankAccountType

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetMasterBankAccountTypeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterBankACtype.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.Bank_AccontType.Contains(searchValue) ||
                i.Bank_Account_Status.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(c => c.Uuid),
                    "bankactype" => query.OrderBy(c => c.Bank_AccontType),
                    "bankacstatus" => query.OrderBy(c => c.Bank_Account_Status),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.Bank_id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "bankactype" => query.OrderByDescending(c => c.Bank_AccontType),
                    "bankacstatus" => query.OrderByDescending(c => c.Bank_Account_Status),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.Bank_id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditBankAccountType/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    bankactype = i.Bank_AccontType,
                    bankacstatus = i.Bank_Account_Status,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewBankAccountType()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditBankAccountType(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterBankActype());
            }

            Master_BankACtype MY = s.MasterBankACtype.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == 
            Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                .OrderByDescending(c => c.Bank_id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewBankAccountType");
            }
            else
            {
                MasterBankActype MY1 = new MasterBankActype();
                MY1 = _mapper.Map<MasterBankActype>(MY);
                return View("MasterAddBankAccounttype", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddBankAccounttype(string UUID)
        {
            if (UUID == null)
            {
                MasterBankActype m = new MasterBankActype();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var Bankactype = s.MasterBankACtype.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID
                == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).FirstOrDefault();

                if (Bankactype == null)
                {
                    return RedirectToAction("MasterViewBankAccountType");
                }
                else
                {
                    return View(Bankactype);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddBankAccounttype(MasterBankActype model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterBankACtype.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                               (c.Bank_AccontType == model.Bank_AccontType || c.Bank_Account_Status == model.Bank_Account_Status) &&
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
                            model.Uuid = u.GetUUID();

                            Master_BankACtype Bankactype = _mapper.Map<Master_BankACtype>(model);
                            s.MasterBankACtype.Add(Bankactype);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewBankAccountType");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterBankACtype.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterBankACtype.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                               (c.Bank_AccontType == model.Bank_AccontType || c.Bank_Account_Status == model.Bank_Account_Status) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterBankActype>(model));
                        }
                        else
                        {
                            existingRecord.Bank_AccontType = model.Bank_AccontType;
                            existingRecord.Bank_Account_Status = model.Bank_Account_Status;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterBankACtype.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewBankAccountType");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterBankActype>(model));
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteBankAcType(string uuid)
        {
            try
            {
                Master_BankACtype MY = s.MasterBankACtype.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Bank_id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterBankACtype.Update(MY);

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
            return RedirectToAction("MasterViewBankAccountType");
        }

        #endregion


        #region Bank Detail

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetBankDetail()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterManageBankDetail.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();




            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Printed_NameOn_Passbook.Contains(searchValue) ||
                    i.Bank_UUID.Contains(searchValue) ||
                    i.Employee_UUID.Contains(searchValue));
            }

            var totalRecords = query.Count();


            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.UUID),
                    "employeename" => query.OrderBy(i => i.Employee_UUID),
                    "bankname" => query.OrderBy(i => i.Bank_UUID),
                    "accountname" => query.OrderBy(i => i.Account_UUID),
                    "name" => query.OrderBy(i => i.Printed_NameOn_Passbook),
                    "acnumber" => query.OrderBy(i => i.BankAC_Number),
                    "ifsc" => query.OrderBy(i => i.IFSC_Code),
                    "opendate" => query.OrderBy(i => i.Opening_Date),
                    "remark" => query.OrderBy(i => i.Remark),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "employeename" => query.OrderByDescending(i => i.Employee_UUID),
                    "bankname" => query.OrderByDescending(i => i.Bank_UUID),
                    "accountname" => query.OrderByDescending(i => i.Account_UUID),
                    "name" => query.OrderByDescending(i => i.Printed_NameOn_Passbook),
                    "acnumber" => query.OrderByDescending(i => i.BankAC_Number),
                    "ifsc" => query.OrderByDescending(i => i.IFSC_Code),
                    "opendate" => query.OrderByDescending(i => i.Opening_Date),
                    "remark" => query.OrderByDescending(i => i.Remark),
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
                    uuid = $"<a href='/CompanySetup/MasterEditBankDetails/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    employeename = i.Employee_UUID,
                    bankname = i.Bank_UUID,
                    accountname = i.Account_UUID,
                    acnumber = i.BankAC_Number,
                    ifsc = i.IFSC_Code,
                    opendate = i.Opening_Date,
                    remark = i.Remark,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });
        }

        public IActionResult MasterViewBankDetail()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult MasterEditBankDetails(string UUID)
        {
            if (UUID == null)
            {
                ViewBag.employee = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                ViewBag.bankname = new SelectList(s.MasterBanktype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_name");
                ViewBag.actype = new SelectList(s.MasterBankACtype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_AccontType");
                return View(new MasterManageBankDetail());
            }

            Master_ManageBankDetail MY = s.MasterManageBankDetail.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewBankDetail");
            }
            else
            {
                ViewBag.employee = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                ViewBag.bankname = new SelectList(s.MasterBanktype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_name");
                ViewBag.actype = new SelectList(s.MasterBankACtype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_AccontType");
                MasterManageBankDetail MY1 = new MasterManageBankDetail();
                MY1 = _mapper.Map<MasterManageBankDetail>(MY);
                return View("MasterAddBankDetails", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddBankDetails(string UUID)
        {
            if (UUID == null)
            {
                MasterManageBankDetail m = new MasterManageBankDetail();
                m.IsActive = true;
                m.IsDisplay = true;
                ViewBag.employee = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                ViewBag.bankname = new SelectList(s.MasterBanktype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_name");
                ViewBag.actype = new SelectList(s.MasterBankACtype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_AccontType");
                return View(m);
            }
            else
            {
                var manageBankDetail = s.MasterManageBankDetail.Get().Where(c => c.UUID == UUID && c.IsActive == true && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).FirstOrDefault();
                if (manageBankDetail == null)
                {
                    return RedirectToAction("MasterViewBankDetail");
                }
                else
                {
                    ViewBag.employee = new SelectList(s.MasterEmployee.Get().Where(c => c.IsActive == true).ToList(), "UUID", "FirstName");
                    ViewBag.bankname = new SelectList(s.MasterBanktype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_name");
                    ViewBag.actype = new SelectList(s.MasterBankACtype.Get().Where(c => c.IsActive == true).ToList(), "Uuid", "Bank_AccontType");
                    return View(manageBankDetail);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddBankDetails(MasterManageBankDetail model)
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

                        var isDuplicate = s.MasterManageBankDetail.Get()
                            .FirstOrDefault(c => c.IsActive == true &&
                                         c.BankAC_Number == model.BankAC_Number &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                            model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                            model.IsActive = true;
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.AddedIP = u.GetLocalIPAddress();
                            model.UUID = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();

                            Master_ManageBankDetail manageBankDetail = _mapper.Map<Master_ManageBankDetail>(model);
                            s.MasterManageBankDetail.Add(manageBankDetail);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewBankDetail");
                        }
                    }
                    else
                    {

                        var existingState = s.MasterManageBankDetail.Get().FirstOrDefault(s => s.UUID == model.UUID);

                        var isDuplicate = s.MasterManageBankDetail.Get()
                            .FirstOrDefault(c => c.IsActive == true &&
                                         c.BankAC_Number == model.BankAC_Number &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            existingState.Employee_UUID = model.Employee_UUID;
                            existingState.Bank_UUID = model.Bank_UUID;
                            existingState.Account_UUID = model.Account_UUID;
                            existingState.Printed_NameOn_Passbook = model.Printed_NameOn_Passbook;
                            existingState.BankAC_Number = model.BankAC_Number;
                            existingState.IFSC_Code = model.IFSC_Code;
                            existingState.Opening_Date = model.Opening_Date;
                            existingState.Remark = model.Remark;
                            existingState.IsDisplay = model.IsDisplay;
                            existingState.IsUpdatedOn = u.CurrentIndianTime();
                            existingState.IsUpdatedBy = Request.Cookies["UserUUID"];
                            s.MasterManageBankDetail.Update(existingState);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                        }
                    }
                }

                return RedirectToAction("MasterViewBankDetail");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult DeleteBankDetails(string uuid)
        {
            try
            {
                Master_ManageBankDetail MY = s.MasterManageBankDetail.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterManageBankDetail.Update(MY);

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
            return RedirectToAction("MasterViewBankDetail");
        }

        #endregion



        #region Honorific

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetHonorificData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterHonorific.Get().Where(c => c.IsActive == true && c.Master_Company_UUID ==
            Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID ==
            Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Honorific_name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "honorificname" => query.OrderBy(c => c.Honorific_name),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "honorificname" => query.OrderByDescending(c => c.Honorific_name),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditHonorific/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    honorificname = i.Honorific_name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewHonorific()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> MasterEditHonorific(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterHonorific());
            }

            Master_Honorific MY = s.MasterHonorific.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID ==
            Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewHonorific");
            }
            else
            {
                MasterHonorific MY1 = new MasterHonorific();
                MY1 = _mapper.Map<MasterHonorific>(MY);
                return View("MasterAddHonorific", MY1);
            }


        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> MasterAddHonorific(string UUID)
        {
            if (UUID == null)
            {
                MasterHonorific m = new MasterHonorific();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {

                var Honorific = s.MasterHonorific.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID ==
                Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
                if (Honorific == null)
                {
                    return RedirectToAction("MasterViewHonorific");
                }
                else
                {
                    return View(Honorific);
                }

            }
        }


        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddHonorific(MasterHonorific model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterHonorific.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Honorific_name == model.Honorific_name &&
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
                            model.Uuid = u.GetUUID();

                            Master_Honorific Honorific = _mapper.Map<Master_Honorific>(model);
                            s.MasterHonorific.Add(Honorific);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewHonorific");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterHonorific.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterHonorific.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Honorific_name == model.Honorific_name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterHonorific>(model));
                        }
                        else
                        {
                            existingRecord.Honorific_name = model.Honorific_name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterHonorific.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewHonorific");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterHonorific>(model));
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult Deletehonorific(string uuid)
        {
            try
            {
                Master_Honorific MY = s.MasterHonorific.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID
                == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterHonorific.Update(MY);

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
            return RedirectToAction("MasterViewHonorific");
        }


        #endregion


        #region Gender

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetGenderData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterGender.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.Gender_name.Contains(searchValue) ||
                i.Gender_Symbol.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(c => c.Uuid),
                    "gendername" => query.OrderBy(c => c.Gender_name),
                    "gendersymbol" => query.OrderBy(c => c.Gender_Symbol),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "gendername" => query.OrderByDescending(c => c.Gender_name),
                    "gendersymbol" => query.OrderByDescending(c => c.Gender_Symbol),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditGender/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    gendername = i.Gender_name,
                    gendersymbol = i.Gender_Symbol,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });




        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewGender()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditGender(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterGender());
            }

            Master_Gender MY = s.MasterGender.Get().Where(c => c.IsActive == true && c.Uuid == UUID
            && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewGender");
            }
            else
            {
                MasterGender MY1 = new MasterGender();
                MY1 = _mapper.Map<MasterGender>(MY);
                return View("MasterAddGender", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddGender(string UUID)
        {
            if (UUID == null)
            {
                MasterGender m = new MasterGender();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var gender = s.MasterGender.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();

                if (gender == null)
                {
                    return RedirectToAction("MasterViewGender");
                }
                else
                {
                    return View(gender);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddGender(MasterGender model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterGender.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                               (c.Gender_name == model.Gender_name || c.Gender_Symbol == model.Gender_Symbol) &&
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
                            model.Uuid = u.GetUUID();

                            Master_Gender gender = _mapper.Map<Master_Gender>(model);
                            s.MasterGender.Add(gender);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewGender");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterGender.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterGender.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                               (c.Gender_name == model.Gender_name || c.Gender_Symbol == model.Gender_Symbol) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterGender>(model));
                        }
                        else
                        {
                            existingRecord.Gender_name = model.Gender_name;
                            existingRecord.Gender_Symbol = model.Gender_Symbol;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterGender.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewGender");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterGender>(model));
            }

        }


        [CheckCookie("UserUUID")]
        public IActionResult Deletegender(string uuid)
        {
            try
            {
                Master_Gender MY = s.MasterGender.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterGender.Update(MY);

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
            return RedirectToAction("MasterViewGender");
        }


        #endregion


        #region Blood Group

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetBloodGroupData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterBloodGroup.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.BloodGroup_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.BloodGroup_name.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.Uuid),
                    "bloodgroupname" => query.OrderBy(i => i.BloodGroup_name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.BloodGroup_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.Uuid),
                    "bloodgroupname" => query.OrderByDescending(i => i.BloodGroup_name),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.BloodGroup_Id)
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
                    uuid = $"<a href='/CompanySetup/EditBloodGroup/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    bloodgroupname = i.BloodGroup_name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewBloodGroup()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> EditBloodGroup(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterBloodGroup());
            }

            Master_BloodGroup MY = s.MasterBloodGroup.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.BloodGroup_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewBloodGroup");
            }
            else
            {
                MasterBloodGroup MY1 = new MasterBloodGroup();
                MY1 = _mapper.Map<MasterBloodGroup>(MY);
                return View("AddBloodGroup", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddBloodGroup(string UUID)
        {
            if (UUID == null)
            {
                MasterBloodGroup m = new MasterBloodGroup();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var bloodGroup = s.MasterBloodGroup.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.BloodGroup_Id).FirstOrDefault();

                if (bloodGroup == null)
                {
                    return RedirectToAction("ViewBloodGroup");
                }
                else
                {
                    return View(bloodGroup);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddBloodGroup(MasterBloodGroup model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterBloodGroup.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.BloodGroup_name == model.BloodGroup_name &&
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
                            model.Uuid = u.GetUUID();

                            Master_BloodGroup bloodGroup = _mapper.Map<Master_BloodGroup>(model);
                            s.MasterBloodGroup.Add(bloodGroup);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewBloodGroup");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterBloodGroup.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterBloodGroup.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.BloodGroup_name == model.BloodGroup_name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterBloodGroup>(model));
                        }
                        else
                        {
                            existingRecord.BloodGroup_name = model.BloodGroup_name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterBloodGroup.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewBloodGroup");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterBloodGroup>(model));
            }


        }


        [CheckCookie("UserUUID")]
        public IActionResult DeleteBloodGroup(string uuid)
        {
            try
            {
                Master_BloodGroup MY = s.MasterBloodGroup.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID ==
                Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.BloodGroup_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterBloodGroup.Update(MY);

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
            return RedirectToAction("ViewBloodGroup");
        }



        #endregion


        #region Marital Status

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetMaritalData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterMarital.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Marital_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.Marital_Status.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "maritailstatus" => query.OrderBy(c => c.Marital_Status),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.Marital_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "maritailstatus" => query.OrderByDescending(c => c.Marital_Status),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.Marital_Id)
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
                    uuid = $"<a href='/CompanySetup/EditMaritalStatus/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    maritailstatus = i.Marital_Status,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });




        }


        // GET: MasterMarital/ViewMaritalStatus
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewMaritalStatus()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditMaritalStatus(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterMarital());
            }

            Master_Marital MY = s.MasterMarital.Get().Where(c => c.IsActive == true && c.Uuid == UUID
            && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Marital_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewMaritalStatus");
            }
            else
            {
                MasterMarital MY1 = new MasterMarital();
                MY1 = _mapper.Map<MasterMarital>(MY);
                return View("AddMaritalStatus", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddMaritalStatus(string UUID)
        {
            if (UUID == null)
            {
                MasterMarital m = new MasterMarital();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var marital = s.MasterMarital.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Marital_Id).FirstOrDefault();

                if (marital == null)
                {
                    return RedirectToAction("ViewMaritalStatus");
                }
                else
                {
                    return View(marital);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddMaritalStatus(MasterMarital model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterMarital.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Marital_Status == model.Marital_Status &&
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
                            model.Uuid = u.GetUUID();

                            Master_Marital marital = _mapper.Map<Master_Marital>(model);
                            s.MasterMarital.Add(marital);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewMaritalStatus");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterMarital.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterMarital.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Marital_Status == model.Marital_Status &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterMarital>(model));
                        }
                        else
                        {
                            existingRecord.Marital_Status = model.Marital_Status;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterMarital.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewMaritalStatus");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterMarital>(model));
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteMarital(string uuid)
        {
            try
            {
                Master_Marital MY = s.MasterMarital.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Marital_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterMarital.Update(MY);

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
            return RedirectToAction("ViewMaritalStatus");
        }

        #endregion


        #region Nationality

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetNationalityData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterNationality.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nationality_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.Nationality_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "nationalityname" => query.OrderBy(c => c.Nationality_Name),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.Nationality_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "nationalityname" => query.OrderByDescending(c => c.Nationality_Name),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.Nationality_Id)
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
                    uuid = $"<a href='/CompanySetup/EditNationality/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    nationalityname = i.Nationality_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });

        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewNationality()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditNationality(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterNationality());
            }

            Master_Nationality MY = s.MasterNationality.Get().Where(c => c.IsActive == true && c.Uuid == UUID
            && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nationality_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewNationality");
            }
            else
            {
                MasterNationality MY1 = new MasterNationality();
                MY1 = _mapper.Map<MasterNationality>(MY);
                return View("AddNationality", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddNationality(string UUID)
        {
            if (UUID == null)
            {
                MasterNationality m = new MasterNationality();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var nationality = s.MasterNationality.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nationality_Id).FirstOrDefault();

                if (nationality == null)
                {
                    return RedirectToAction("ViewNationality");
                }
                else
                {
                    return View(nationality);
                }

            }
        }


        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddNationality(MasterNationality model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterNationality.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Nationality_Name == model.Nationality_Name &&
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
                            model.Uuid = u.GetUUID();

                            Master_Nationality nationality = _mapper.Map<Master_Nationality>(model);
                            s.MasterNationality.Add(nationality);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewNationality");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterNationality.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterNationality.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Nationality_Name == model.Nationality_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterNationality>(model));
                        }
                        else
                        {
                            existingRecord.Nationality_Name = model.Nationality_Name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterNationality.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewNationality");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterNationality>(model));
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteNationality(string uuid)
        {
            try
            {
                Master_Nationality MY = s.MasterNationality.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Nationality_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterNationality.Update(MY);

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
            return RedirectToAction("ViewNationality");
        }

        #endregion


        #region DocumentType

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetDocumentTypeData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterDocumentGrop.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.Document_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "documentname" => query.OrderBy(c => c.Document_Name),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "documentname" => query.OrderByDescending(c => c.Document_Name),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.id)
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
                    uuid = $"<a href='/CompanySetup/MasterEditDocumentType/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    documentname = i.Document_Name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });

        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewDocumentType()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditDocumentType(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterNationality());
            }

            Master_DocumentGrop MY = s.MasterDocumentGrop.Get().Where(c => c.IsActive == true && c.Uuid == UUID
            && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewDocumentType");
            }
            else
            {
                MasterDocumentGrop MY1 = new MasterDocumentGrop();
                MY1 = _mapper.Map<MasterDocumentGrop>(MY);
                return View("MasterAddDocumentType", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddDocumentType(string UUID)
        {
            if (UUID == null)
            {
                MasterDocumentGrop m = new MasterDocumentGrop();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var dtype = s.MasterDocumentGrop.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();

                if (dtype == null)
                {
                    return RedirectToAction("MasterViewDocumentType");
                }
                else
                {
                    return View(dtype);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddDocumentType(MasterDocumentGrop model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterDocumentGrop.Get()
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
                            model.RecordNo = u.GetRecordNo();
                            model.Uuid = u.GetUUID();

                            Master_DocumentGrop dtype = _mapper.Map<Master_DocumentGrop>(model);
                            s.MasterDocumentGrop.Add(dtype);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewDocumentType");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterDocumentGrop.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterDocumentGrop.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Document_Name == model.Document_Name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterDocumentGrop>(model));
                        }
                        else
                        {
                            existingRecord.Document_Name = model.Document_Name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterDocumentGrop.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewDocumentType");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterDocumentGrop>(model));
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteDocumentType(string uuid)
        {
            try
            {
                Master_DocumentGrop MY = s.MasterDocumentGrop.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterDocumentGrop.Update(MY);

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
            return RedirectToAction("MasterViewDocumentType");
        }




        #endregion


        #region Master Asset

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetMasterAssetData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterAsset.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Asset_id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.Asset_name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "assetname" => query.OrderBy(c => c.Asset_name),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.Asset_id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "assetname" => query.OrderByDescending(c => c.Asset_name),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(i => i.Asset_id)
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
                    uuid = $"<a href='/CompanySetup/EditAssets/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    assetname = i.Asset_name,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });

        }


        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewAssets()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditAssets(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterAsset());
            }

            Master_Asset MY = s.MasterAsset.Get().Where(c => c.IsActive == true && c.Uuid == UUID
            && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Asset_id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewAssets");
            }
            else
            {
                MasterAsset MY1 = new MasterAsset();
                MY1 = _mapper.Map<MasterAsset>(MY);
                return View("AddAssets", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddAssets(string UUID)
        {
            if (UUID == null)
            {
                MasterAsset m = new MasterAsset();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var asset = s.MasterAsset.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Asset_id).FirstOrDefault();

                if (asset == null)
                {
                    return RedirectToAction("ViewAssets");
                }
                else
                {
                    return View(asset);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddAssets(MasterAsset model)
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


                    if (string.IsNullOrEmpty(model.Uuid))
                    {

                        var duplicateRecord = s.MasterAsset.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Asset_name == model.Asset_name &&
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
                            model.Uuid = u.GetUUID();

                            Master_Asset asset = _mapper.Map<Master_Asset>(model);
                            s.MasterAsset.Add(asset);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewAssets");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterAsset.Get()
                            .FirstOrDefault(c => c.Uuid == model.Uuid);



                        var duplicateRecord = s.MasterAsset.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Asset_name == model.Asset_name &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.Uuid != model.Uuid);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterAsset>(model));
                        }
                        else
                        {
                            existingRecord.Asset_name = model.Asset_name;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterAsset.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewAssets");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterAsset>(model));
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteAssets(string uuid)
        {
            try
            {
                Master_Asset MY = s.MasterAsset.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Asset_id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterAsset.Update(MY);

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
            return RedirectToAction("ViewAssets");
        }




        #endregion


        #region Industry Sector

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetIndustryData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterIndustry.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Industry_Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Industry_Sector.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.UUID),
                    "industrysector" => query.OrderBy(i => i.Industry_Sector),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Industry_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "industrysector" => query.OrderByDescending(i => i.Industry_Sector),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.Industry_Id)
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
                    uuid = $"<a href='/CompanySetup/EditIndustrySector/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    industrysector = i.Industry_Sector,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });


        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> ViewIndustrySector()
        {
            return View();
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditIndustrySector(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterIndustry());
            }

            Master_Industry MY = s.MasterIndustry.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
            .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Industry_Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("ViewIndustrySector");
            }
            else
            {
                MasterIndustry MY1 = new MasterIndustry();
                MY1 = _mapper.Map<MasterIndustry>(MY);
                return View("AddIndustrySector", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]

        public async Task<IActionResult> AddIndustrySector(string UUID)
        {
            if (UUID == null)
            {
                MasterIndustry m = new MasterIndustry();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var industry = s.MasterIndustry.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
                .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Industry_Id).FirstOrDefault();

                if (industry == null)
                {
                    return RedirectToAction("ViewIndustrySector");
                }
                else
                {
                    return View(industry);
                }

            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddIndustrySector(MasterIndustry model)
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

                        var duplicateRecord = s.MasterIndustry.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Industry_Sector == model.Industry_Sector &&
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

                            Master_Industry industry = _mapper.Map<Master_Industry>(model);
                            s.MasterIndustry.Add(industry);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewIndustrySector");
                        }



                    }
                    else
                    {

                        var existingRecord = s.MasterIndustry.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.MasterIndustry.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                c.Industry_Sector == model.Industry_Sector &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<MasterIndustry>(model));
                        }
                        else
                        {
                            existingRecord.Industry_Sector = model.Industry_Sector;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterIndustry.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewIndustrySector");
                        }


                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterIndustry>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> DeleteIndustrySector(string uuid)
        {
            try
            {
                Master_Industry MY = s.MasterIndustry.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
                .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Industry_Id).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();

                    s.MasterIndustry.Update(MY);

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
            return RedirectToAction("ViewIndustrySector");
        }

        #endregion


        #region Group Service 


        [HttpPost]
        public async Task<IActionResult> GetServiceGroupData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterService.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Service_Name).AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(a =>
                    a.Service_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(a => a.UUID),
                    "servicename" => query.OrderBy(a => a.Service_Name),
                    "status" => query.OrderBy(a => a.IsDisplay),
                    _ => query.OrderBy(a => a.Service_Name)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(a => a.UUID),
                    "servicename" => query.OrderByDescending(a => a.Service_Name),
                    "status" => query.OrderByDescending(a => a.IsDisplay),
                    _ => query.OrderByDescending(a => a.Service_Name)
                };
            }

            var data = query
                .Skip(start)
                .Take(length).Where(a => a.IsActive == true)
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
                    uuid = $"<a href='/CompanySetup/MasterEditServiceGroup/{a.UUID}' class='btnEdit' target='_blank'>{a.UUID}</a>",
                    servicename = a.Service_Name,
                    status = (bool)a.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + a.UUID + "'>Delete</button>"
                })
            });
        }
        [CheckCookie("UserUUID")]
        public IActionResult MasterViewServiceGroup()
        {
            return View();
        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditServiceGroup(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterService());
            }


            Master_Service MY = s.MasterService
                .Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Service_Id).FirstOrDefault();

            if (MY == null)
            {
                return RedirectToAction("MasterViewServiceGroup");
            }
            else
            {
                MasterService MY1 = new MasterService();
                MY1 = _mapper.Map<MasterService>(MY);
                return View("MasterAddServiceGroup", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddServiceGroup(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterService());
            }
            else
            {

                var serviceGroup = s.MasterService.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Service_Id).FirstOrDefault();

                if (serviceGroup == null)
                {
                    return RedirectToAction("MasterViewServiceGroup");
                }
                else
                {
                    return View(serviceGroup);
                }

            }
        }
        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddServiceGroup(MasterService model)
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

                        var service = s.MasterService.Get().Where(c => c.IsActive == true && c.Service_Name == model.Service_Name && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Service_Id).FirstOrDefault();
                        if (service != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }

                        else
                        {
                            Master_Service MY = new Master_Service();
                            MY = _mapper.Map<Master_Service>(model);
                            s.MasterService.Add(MY);
                            TempData["Message"] = "Service Group added successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewServiceGroup");
                        }
                    }
                    else
                    {

                        var existingService = s.MasterService.Get().FirstOrDefault(c => c.UUID == model.UUID);

                        //if (existingService == null)
                        //{
                        //    TempData["Message"] = "Record not found!";
                        //    TempData["MessageType"] = "danger";
                        //    return RedirectToAction("MasterViewServiceGroup");
                        //}

                        var isDuplicate = s.MasterService.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.Service_Name == model.Service_Name &&
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
                            existingService.Service_Name = model.Service_Name;
                            existingService.IsDisplay = model.IsDisplay;
                            existingService.IsUpdatedOn = u.CurrentIndianTime();
                            existingService.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingService.UpdatedIP = u.GetLocalIPAddress();


                            s.MasterService.Update(existingService);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("MasterViewServiceGroup");
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


        [HttpPost]

        public IActionResult MasterDeleteServiceGroup(string uuid)
        {
            try
            {

                Master_Service MY = s.MasterService.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Service_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterService.Update(MY);
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

            return RedirectToAction("MasterViewServiceGroup");
        }



        #endregion


        #region Designation

        [HttpPost]
        public async Task<IActionResult> GetDesignationData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterDesignation.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Designation_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(a =>
                    a.Designation_Name.Contains(searchValue) ||
                    a.Designation_ShortName.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(a => a.UUID),
                    "designationname" => query.OrderBy(a => a.Designation_Name),
                    "designationshortname" => query.OrderBy(a => a.Designation_ShortName),


                    "status" => query.OrderBy(a => a.IsDisplay),
                    _ => query.OrderBy(a => a.Designation_Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(a => a.UUID),
                    "designationname" => query.OrderByDescending(a => a.Designation_Name),
                    "designationshortname" => query.OrderByDescending(a => a.Designation_ShortName),

                    "status" => query.OrderByDescending(a => a.IsDisplay),
                    _ => query.OrderByDescending(a => a.Designation_Id)
                };
            }

            var data = query
                .Skip(start)
                .Take(length)
                .Where(a => a.IsActive == true)
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
                    uuid = $"<a href='/CompanySetup/MasterEditEmployeeDesignation/{a.UUID}' class='btnEdit' target='_blank'>{a.UUID}</a>",
                    designationname = a.Designation_Name,
                    designationshortname = a.Designation_ShortName,

                    status = (bool)a.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + a.UUID + "'>Delete</button>"
                })
            });
        }
        [CheckCookie("UserUUID")]
        public IActionResult MasterViewEmployeeDesignation()
        {
            return View();
        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditEmployeeDesignation(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterDesignation());
            }
            Master_Designation MY = s.MasterDesignation.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Designation_Id).FirstOrDefault();

            if (MY == null)
            {
                return RedirectToAction("MasterViewEmployeeDesignation");
            }
            else
            {
                MasterDesignation MY1 = new MasterDesignation();
                MY1 = _mapper.Map<MasterDesignation>(MY);
                return View("MasterAddEmployeeDesignation", MY1);
            }

        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddEmployeeDesignation(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterDesignation());
            }

            var designation = s.MasterDesignation.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Designation_Id).FirstOrDefault();


            if (designation == null)
            {
                return RedirectToAction("MasterViewEmployeeDesignation");
            }
            else
            {
                return View(designation);
            }


        }


        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddEmployeeDesignation(MasterDesignation model)
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
                    if (string.IsNullOrEmpty(model.UUID)) // Adding new record
                    {
                        model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                        model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                        model.IsActive = true;
                        model.AddedIP = u.GetLocalIPAddress();
                        model.IsAdddedOn = u.CurrentIndianTime();
                        model.RecordNo = u.GetRecordNo();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.UUID = u.GetUUID();

                        var degisnation = s.MasterDesignation.Get().Where(c => c.IsActive == true && c.Designation_Name == model.Designation_Name && c.Designation_ShortName == model.Designation_ShortName && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Designation_Id).FirstOrDefault();

                        if (degisnation != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }

                        else
                        {
                            Master_Designation MY = new Master_Designation();
                            MY = _mapper.Map<Master_Designation>(model);
                            s.MasterDesignation.Add(MY);
                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("MasterViewEmployeeDesignation");
                        }
                    }
                    else // Editing existing record
                    {

                        var existingDesignation = s.MasterDesignation.Get().FirstOrDefault(c => c.UUID == model.UUID);

                        //if (existingDesignation == null)
                        //{
                        //    TempData["Message"] = "Record not found!";
                        //    TempData["MessageType"] = "danger";
                        //    return RedirectToAction("MasterViewEmployeeDesignation");
                        //}

                        var isDuplicate = s.MasterDesignation.Get()
                   .FirstOrDefault(c => c.IsActive == true &&
                                        c.Designation_Name == model.Designation_Name &&
                                        c.Master_Company_UUID == model.Master_Company_UUID &&
                                        c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                        c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }

                        // Update fields
                        else
                        {
                            existingDesignation.Designation_Name = model.Designation_Name;
                            existingDesignation.Designation_ShortName = model.Designation_ShortName;

                            existingDesignation.IsDisplay = model.IsDisplay;
                            existingDesignation.IsUpdatedOn = u.CurrentIndianTime();
                            existingDesignation.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingDesignation.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterDesignation.Update(existingDesignation);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewEmployeeDesignation");
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



        [HttpPost]
        public IActionResult MasterDeleteDesignation(string uuid)
        {
            try
            {
                Master_Designation MY = s.MasterDesignation.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Designation_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterDesignation.Update(MY);

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
            return RedirectToAction("MasterViewEmployeeDesignation"); // Reload the view
        }


        #endregion


        #region Department

        [HttpPost]
        public async Task<IActionResult> GetDepartmentData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterDepartment.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Department_Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(a =>
                    a.Department_Name.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(a => a.UUID),
                    "departmentname" => query.OrderBy(a => a.Department_Name),
                    "status" => query.OrderBy(a => a.IsDisplay),
                    _ => query.OrderBy(a => a.Department_Name)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(a => a.UUID),
                    "departmentname" => query.OrderByDescending(a => a.Department_Name),
                    "status" => query.OrderByDescending(a => a.IsDisplay),
                    _ => query.OrderByDescending(a => a.Department_Name)
                };
            }

            var data = query
                .Skip(start)
                .Take(length)
                .Where(a => a.IsActive == true)
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
                    uuid = $"<a href='/CompanySetup/MasterEditDepartment/{a.UUID}' class='btnEdit' target='_blank'>{a.UUID}</a>",
                    departmentname = a.Department_Name,
                    status = (bool)a.IsDisplay
                        ? "<span class='badge bg-success'>Visible</span>"
                        : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + a.UUID + "'>Delete</button>"
                })
            });
        }
        [CheckCookie("UserUUID")]
        public IActionResult MasterViewDepartment()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditDepartment(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterDepartment());
            }

            Master_Department MY = s.MasterDepartment.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Department_Id).FirstOrDefault();


            if (MY == null)
            {
                return RedirectToAction("MasterViewDepartment");
            }
            else
            {
                MasterDepartment MY1 = new MasterDepartment();
                MY1 = _mapper.Map<MasterDepartment>(MY);

                return View("MasterAddDepartment", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddDepartment(string Uuid)
        {
            if (Uuid == null)
            {
                return View(new MasterDepartment());
            }

            // Fetch the record for editing
            var department = s.MasterDepartment.Get().Where(c => c.IsActive == true && c.UUID == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Department_Id).FirstOrDefault();

            if (department == null)
            {
                return RedirectToAction("MasterViewDepartment");
            }
            else
            {
                return View(department);
            }

        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddDepartment(MasterDepartment model)
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
                        var dept = s.MasterDepartment.Get().Where(c => c.IsActive == true && c.Department_Name == model.Department_Name && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Department_Id).FirstOrDefault();

                        if (dept != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            Master_Department MY = new Master_Department();
                            MY = _mapper.Map<Master_Department>(model);
                            s.MasterDepartment.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewDepartment");
                        }
                    }
                    else // Editing existing record
                    {

                        var existingDepartment = s.MasterDepartment.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);

                        if (existingDepartment == null)
                        {
                            TempData["Message"] = "Record not found!";
                            TempData["MessageType"] = "danger";
                            return RedirectToAction("MasterViewDepartment");
                        }

                        var isDuplicate = s.MasterDepartment.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.Department_Name == model.Department_Name &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }

                        // Update fields
                        else
                        {
                            existingDepartment.Department_Name = model.Department_Name;
                            existingDepartment.IsDisplay = model.IsDisplay;
                            existingDepartment.IsUpdatedOn = u.CurrentIndianTime();
                            existingDepartment.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            existingDepartment.UpdatedIP = u.GetLocalIPAddress();

                            s.MasterDepartment.Update(existingDepartment);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("MasterViewDepartment");
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


        [HttpPost]
        public IActionResult MasterDeleteDepartment(String uuid)
        {
            try
            {
                Master_Department MY = s.MasterDepartment.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Department_Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterDepartment.Update(MY);

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
            return RedirectToAction("MasterViewDepartment");
        }

        #endregion



        #region Country

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetCountry()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterCountry.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CountryId).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.CountryName.Contains(searchValue) ||
                i.CountryShortName.Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.UUID),
                    "countryname" => query.OrderBy(i => i.CountryName),
                    "countryshortname" => query.OrderBy(i => i.CountryShortName),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.CountryId)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "countryname" => query.OrderByDescending(i => i.CountryName),
                    "countryshortname" => query.OrderByDescending(i => i.CountryShortName),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.CountryId)
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
                    uuid = $"<a href='/CompanySetup/MasterEditCountryName/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    countryname = i.CountryName,
                    countryshortname = i.CountryShortName,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });

        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewCountryName()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditCountryName(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterCountry());
            }

            Master_Country MY = s.MasterCountry.Get().Where(c => c.IsActive == true && c.UUID == UUID
            && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CountryId).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewCountryName");
            }
            else
            {
                MasterCountry MY1 = new MasterCountry();
                MY1 = _mapper.Map<MasterCountry>(MY);
                return View("MasterAddCountryName", MY1);
            }

        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddCountryName(string UUID)
        {
            if (UUID == null)
            {
                MasterCountry m = new MasterCountry();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var Bankactype = s.MasterCountry.Get().Where(c => c.IsActive == true && c.UUID == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CountryId).FirstOrDefault();

                if (Bankactype == null)
                {
                    return RedirectToAction("MasterViewCountryName");
                }
                else
                {
                    return View(Bankactype);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddCountryName(MasterCountry model)
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

                        model.IsActive = true;
                        model.AddedIP = u.GetLocalIPAddress();
                        model.IsAddedOn = u.CurrentIndianTime();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.RecordNo = u.GetRecordNo();
                        model.UUID = u.GetUUID();
                        var Count = s.MasterCountry.Get().Where(c => c.IsActive == true && c.CountryName == model.CountryName && c.CountryShortName == model.CountryShortName && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                        && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CountryId).FirstOrDefault();


                        if (Count != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            Master_Country MY = new Master_Country();
                            MY = _mapper.Map<Master_Country>(model);
                            s.MasterCountry.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewCountryName");
                        }
                    }
                    else
                    {
                        model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                        model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                        model.UpdatedIP = u.GetLocalIPAddress();
                        model.IsUpdatedOn = u.CurrentIndianTime();
                        model.IsUpdateBy = Request.Cookies["UserUUID"].ToString();

                        var Coun = s.MasterCountry.Get().FirstOrDefault(c => c.UUID == model.UUID);
                        //if (year == null)
                        //{
                        //    TempData["Message"] = "Record not found!";
                        //    TempData["MessageType"] = "danger";
                        //    return RedirectToAction("MasterViewCountryName");
                        //}

                        var isDuplicate = s.MasterCountry.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.CountryName == model.CountryName &&
                                         c.CountryShortName == model.CountryShortName &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.UUID != model.UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<Master_Country>(model));
                        }

                        Coun.CountryName = model.CountryName;
                        Coun.CountryShortName = model.CountryShortName;
                        Coun.IsDisplay = model.IsDisplay;
                        Coun.IsUpdatedOn = u.CurrentIndianTime();
                        Coun.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                        Coun.UpdatedIP = u.GetLocalIPAddress();


                        s.MasterCountry.Update(Coun);
                        TempData["Message"] = "Data Updated Successfully!";
                        TempData["MessageType"] = "success";

                        return RedirectToAction("MasterViewCountryName");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<MasterCountry>(model));
            }

        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteCountry(string uuid)
        {
            try
            {
                Master_Country MY = s.MasterCountry.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
                && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.CountryId).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.DeletedIP = u.GetLocalIPAddress();
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    s.MasterCountry.Update(MY);

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
            return RedirectToAction("MasterViewCountryName");
        }

        #endregion


        #region State
        [HttpPost]
        public async Task<IActionResult> GetState()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();


            var query = s.MasterState.getStateActiveSubModel();



            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.SM.State_Name.Contains(searchValue)).ToList();
            }

            var totalRecords = query.Count();


            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.SM.Id).ToList(),
                    "countryname" => query.OrderBy(i => i.CountryName).ToList(),
                    "statename" => query.OrderBy(i => i.SM.State_Name).ToList(),
                    "status" => query.OrderByDescending(i => i.SM.IsDisplay).ToList(),
                    _ => query.OrderBy(i => i.SM.Id).ToList()
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.SM.Id).ToList(),
                    "countryname" => query.OrderByDescending(i => i.CountryName).ToList(),
                    "statename" => query.OrderByDescending(i => i.SM.State_Name).ToList(),
                    "status" => query.OrderByDescending(i => i.SM.IsDisplay).ToList(),
                    _ => query.OrderByDescending(i => i.SM.Id).ToList()
                };
            }

            var data = query
           .Skip(start)
           .Take(length)
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
                    uuid = $"<a href='/CompanySetup/MasterEditStateName/{i.SM.UUID}' class='btnEdit' target='_blank'>{i.SM.UUID}</a>",
                    countryname = i.CountryName,
                    statename = i.SM.State_Name,
                    status = (bool)i.SM.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn' data-uuid='" + i.SM.UUID + "'>Delete</button>"
                })
            });


        }
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterViewStateName()
        {
            var stateEntities = s.MasterState.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).ToList();

            var stateModels = stateEntities.Select(s => new MasterState
            {
                UUID = s.UUID,
                State_Name = s.State_Name,
                Country_UUID = s.Country_UUID,
                IsDisplay = (bool)s.IsDisplay,
                IsActive = (bool)s.IsActive,
                IsAddedOn = s.IsAddedOn,
                IsAddedBy = s.IsAddedBy
            }).ToList();

            return View(stateModels);

        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditStateName(string UUID)
        {
            if (UUID == null)
            {

                ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName");
                return View(new MasterState());
            }
            else
            {
                Master_State MY = s.MasterState.Get().Where(c => c.UUID == UUID && c.IsActive == true && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending
                (c => c.Id).FirstOrDefault();

                if (MY == null)
                {
                    return RedirectToAction("MasterViewStateName");
                }
                else
                {
                    MasterState MY1 = new MasterState();
                    MY1 = _mapper.Map<MasterState>(MY);
                    ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName");
                    return View("MasterAddStateName", MY1);
                }
            }
        }



        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddStateName(string UUID)
        {
            if (UUID == null)
            {
                MasterState m = new MasterState();
                m.IsActive = true;
                m.IsDisplay = true;

                ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName");
                return View(m);
            }
            else
            {
                var state = s.MasterCountry.Get().Where(c => c.UUID == UUID && c.IsActive == true && c.Master_Company_UUID == Request.Cookies
                ["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).FirstOrDefault();
                if (state == null)
                {
                    return RedirectToAction("MasterViewStateName");
                }
                else
                {
                    ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), state.CountryName);
                    return View(state);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddStateName(MasterState model)
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

                        var isDuplicate = s.MasterState.Get()
                            .FirstOrDefault(c => c.IsActive == true &&
                                         c.State_Name == model.State_Name &&
                                         c.Country_UUID == model.Country_UUID &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                            model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                            model.IsActive = true;
                            model.IsAddedOn = u.CurrentIndianTime();
                            model.AddedIP = u.GetLocalIPAddress();
                            model.RecordNo = u.GetRecordNo();
                            model.UUID = u.GetUUID();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();

                            var entity = new Master_State
                            {
                                UUID = model.UUID,
                                State_Name = model.State_Name,
                                Country_UUID = model.Country_UUID,
                                IsDisplay = model.IsDisplay,
                                IsActive = model.IsActive,
                                IsAddedOn = model.IsAddedOn,
                                IsAddedBy = model.IsAddedBy,
                                Master_Company_UUID = model.Master_Company_UUID,
                                Master_Environment_UUID = model.Master_Environment_UUID


                            };


                            /*Master_State MY = new Master_State();
                            MY = _mapper.Map<Master_State>(model);
                            s.MasterState.Add(MY);*/

                            s.MasterState.Add(entity);
                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewStateName");
                        }
                    }
                    else
                    {

                        var existingState = s.MasterState.Get().FirstOrDefault(s => s.UUID == model.UUID);
                        //if (existingState == null)
                        //{
                        //    TempData["Message"] = "Record not found!";
                        //    TempData["MessageType"] = "danger";
                        //    return RedirectToAction("MasterViewStateName");
                        //}
                        var isDuplicate = s.MasterState.Get()
                          .FirstOrDefault(c => c.IsActive == true &&
                                               c.State_Name == model.State_Name &&
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
                            existingState.State_Name = model.State_Name;
                            existingState.Country_UUID = model.Country_UUID;
                            existingState.IsDisplay = model.IsDisplay;
                            existingState.IsUpdatedOn = u.CurrentIndianTime();
                            existingState.IsUpdateBy = Request.Cookies["UserUUID"];
                            s.MasterState.Update(existingState);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                        }
                    }
                }

                return RedirectToAction("MasterViewStateName");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult DeleteStates(string uuid)
        {
            try
            {
                Master_State MY = s.MasterState.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterState.Update(MY);

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
            return RedirectToAction("MasterViewStateName");
        }

        #endregion


        #region City

        [HttpPost]
        public async Task<IActionResult> GetCity()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterCity.getCityMasterSubModel();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>


                    i.CTY.City_Name.Contains(searchValue)).ToList();
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.CTY.UUID).ToList(),
                    "countryname" => query.OrderBy(i => i.CountryName).ToList(),
                    "statename" => query.OrderBy(i => i.StateName).ToList(),
                    "cityname" => query.OrderBy(i => i.CTY.City_Name).ToList(),
                    "status" => query.OrderByDescending(i => i.CTY.IsDisplay).ToList(),
                    _ => query.OrderBy(i => i.CTY.Id).ToList()
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.CTY.UUID).ToList(),
                    "countryname" => query.OrderByDescending(i => i.CountryName).ToList(),
                    "statename" => query.OrderByDescending(i => i.StateName).ToList(),
                    "cityname" => query.OrderByDescending(i => i.CTY.City_Name).ToList(),
                    "status" => query.OrderByDescending(i => i.CTY.IsDisplay).ToList(),
                    _ => query.OrderByDescending(i => i.CTY.Id).ToList()
                };
            }

            var data = query
            .Skip(start)
            .Take(length)
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
                    uuid = $"<a href='/CompanySetup/MasterEditCityName/{i.CTY.UUID}' class='btnEdit' target='_blank'>{i.CTY.UUID}</a>",
                    countryname = i.CountryName,
                    statename = i.StateName,
                    cityname = i.CTY.City_Name,
                    status = i.CTY.IsDisplay.HasValue && i.CTY.IsDisplay.Value
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn' data-uuid='" + i.CTY.UUID + "'>Delete</button>"
                })
            });


        }
        [CheckCookie("UserUUID")]
        public IActionResult MasterViewCityName()

        {
            var cityEntities = s.MasterCity.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).ToList();

            var cityModels = cityEntities.Select(c => new MasterCity
            {
                UUID = c.UUID,
                City_Name = c.City_Name,
                State_UUID = c.State_UUID,
                Country_UUID = c.Country_UUID,
                IsDisplay = (bool)c.IsDisplay,
                IsActive = (bool)c.IsActive,
                IsAddedOn = c.IsAddedOn,
                IsAddedBy = c.IsAddedBy
            }).ToList();
            return View(cityModels);

        }
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterEditCityName(string UUID)
        {
            if (UUID == null)
            {

                ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName");
                ViewBag.States = new SelectList(s.MasterState.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "State_Name");

                return View(new MasterCity());
            }
            else
            {
                Master_City MY = s.MasterCity.Get().Where(c => c.UUID == UUID && c.IsActive == true &&
                c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .OrderByDescending(c => c.Id).FirstOrDefault();

                if (MY == null)
                {
                    return RedirectToAction("MasterViewCityName");
                }
                else
                {
                    MasterCity MY1 = new MasterCity();
                    MY1 = _mapper.Map<MasterCity>(MY);
                    ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName");
                    ViewBag.States = new SelectList(s.MasterState.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "State_Name");

                    return View("MasterAddCityName", MY1);
                }
            }
        }

        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddCityName(string UUID)
        {
            if (UUID == null)
            {
                MasterCity m = new MasterCity();
                m.IsActive = true;
                m.IsDisplay = true;
                ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName");
                ViewBag.States = new SelectList(s.MasterState.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "State_Name");

                return View(m);
            }
            else
            {
                var city = s.MasterCity.Get().Where(c => c.UUID == UUID && c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).FirstOrDefault();
                if (city == null)
                {
                    return RedirectToAction("MasterViewCityName");
                }
                else
                {
                    ViewBag.Countries = new SelectList(s.MasterCountry.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "CountryName", city.Country_UUID);
                    ViewBag.States = new SelectList(s.MasterState.Get().Where(c => (bool)c.IsActive).ToList(), "UUID", "State_Name", city.Country_UUID);

                    return View(city);
                }
            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddCityName(MasterCity model)
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

                        var existingCity = s.MasterCity.Get()
                        .FirstOrDefault(c => c.City_Name == model.City_Name &&
                                         c.State_UUID == model.State_UUID &&
                                         c.Country_UUID == model.Country_UUID &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.IsActive == true);
                        if (existingCity != null)
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
                            var entity = new Master_City
                            {
                                UUID = model.UUID,
                                City_Name = model.City_Name,
                                State_UUID = model.State_UUID,
                                Country_UUID = model.Country_UUID,
                                IsDisplay = model.IsDisplay,
                                IsActive = model.IsActive,
                                IsAddedOn = model.IsAddedOn,
                                IsAddedBy = model.IsAddedBy,
                                Master_Company_UUID = model.Master_Company_UUID,
                                Master_Environment_UUID = model.Master_Environment_UUID


                            };
                            /*Master_City MY = new Master_City();
                                MY = _mapper.Map<Master_City>(model);
                                //s.MasterCity.Add(MY);*/

                            s.MasterCity.Add(entity);
                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewCityName");
                        }



                    }
                    else
                    {

                        var existingCity = s.MasterCity.Get().FirstOrDefault(c => c.UUID == model.UUID);
                        //if (existingCity == null)
                        //{
                        //    TempData["Message"] = "Record not found!";
                        //    TempData["MessageType"] = "danger";
                        //    return RedirectToAction("MasterViewCityName");
                        //}
                        var isDuplicate = s.MasterCity.Get()
                           .FirstOrDefault(c => c.IsActive == true &&
                                                c.City_Name == model.City_Name &&
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
                            existingCity.City_Name = model.City_Name;
                            existingCity.State_UUID = model.State_UUID;
                            existingCity.Country_UUID = model.Country_UUID;
                            existingCity.IsDisplay = model.IsDisplay;
                            existingCity.IsUpdatedOn = u.CurrentIndianTime();
                            existingCity.IsUpdateBy = Request.Cookies["UserUUID"];
                            s.MasterCity.Update(existingCity);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                        }

                    }

                }
                return RedirectToAction("MasterViewCityName");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(model);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult GetStatesByCountry(string countryUuid)
        {
            /*if (string.IsNullOrEmpty(countryUuid))
            {
                return Json(new { success = true, states = new List<object>() }); 
            }*/
            var states = s.MasterState.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString() && c.Country_UUID == countryUuid)

                .Select(s => new SelectListItem
                {
                    Value = s.UUID.ToString(),
                    Text = s.State_Name ?? "Unknown"
                })
                .ToList();

            return Json(states);
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult GetCitiesByState(string stateUuid)
        {
            var cities = s.MasterCity.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString() && c.State_UUID == stateUuid)

                .Select(c => new SelectListItem
                {
                    Value = c.UUID.ToString(), // Ensure Uuid is not null
                    Text = c.City_Name ?? "Unknown" // Ensure CityName is not null
                })
                .ToList();

            return Json(cities);
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult DeleteCity(string uuid)
        {
            try
            {
                Master_City MY = s.MasterCity.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterCity.Update(MY);

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
            return RedirectToAction("MasterViewCityName");
        }
        #endregion

        //#region Employee Master

        //[HttpPost]
        //public async Task<IActionResult> GetEmployee()
        //{
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
        //    var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

        //    var query = _context.MasterEmployees.Where(c => c.IsActive == true && c.MasterCompanyUuid == Request.Cookies["CmpUUID"]
        //    .ToString() && c.MasterEnvironmentUuid == Request.Cookies["EnvUUID"].ToString()).AsQueryable();

        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        query = query.Where(i =>

        //            i.FirstName.Contains(searchValue) ||
        //             i.LastName.Contains(searchValue) ||
        //             i.PersonalEmail.Contains(searchValue) ||
        //             i.EmployeeCode.Contains(searchValue) ||
        //            i.Uuid.ToString().Contains(searchValue));
        //    }

        //    var totalRecords = await query.CountAsync();

        //    if (sortColumnDirection == "asc")
        //    {
        //        query = sortColumn switch
        //        {

        //            "uuid" => query.OrderBy(i => i.Uuid),
        //            "employeecode" => query.OrderBy(i => i.EmployeeCode),
        //            "employeename" => query.OrderBy(i => i.FirstName),
        //            "personalemailid" => query.OrderBy(i => i.PersonalEmail),
        //            "mobileno" => query.OrderBy(i => i.Mobile),
        //            "designation" => query.OrderBy(i => i.ExpLimitDesignation),
        //            "status" => query.OrderByDescending(i => i.IsDisplay),
        //            _ => query.OrderBy(i => i.Id)
        //        };
        //    }
        //    else
        //    {
        //        query = sortColumn switch
        //        {

        //            "uuid" => query.OrderByDescending(i => i.Uuid),
        //            "employeecode" => query.OrderByDescending(i => i.EmployeeCode),
        //            "employeename" => query.OrderByDescending(i => i.FirstName),
        //            "personalemailid" => query.OrderByDescending(i => i.PersonalEmail),
        //            "mobileno" => query.OrderByDescending(i => i.Mobile),
        //            "designation" => query.OrderByDescending(i => i.ExpLimitDesignation),
        //            "status" => query.OrderByDescending(i => i.IsDisplay),
        //            _ => query.OrderByDescending(i => i.Id)
        //        };
        //    }

        //    var data = await query
        //     .Skip(start)
        //     .Take(length).Where(i => i.IsActive == true)
        //     .ToListAsync();
        //    var srNo = start + 1;

        //    return Json(new
        //    {
        //        draw = draw,
        //        recordsFiltered = totalRecords,
        //        recordsTotal = totalRecords,
        //        data = data.Select(i => new
        //        {
        //            srno = srNo++,
        //            uuid = $"<a href='/CompanySetup/EditEmployeeGeneral/{i.Uuid}'>{i.Uuid}</a>",
        //            employeecode = i.EmployeeCode,
        //            employeename = i.FirstName + i.LastName,
        //            personalemailid = i.PersonalEmail,
        //            mobileno = i.Mobile,
        //            designation = i.ExpLimitDesignation,
        //            status = (bool) i.IsDisplay
        //                 ? "<span class='badge bg-success'>Visible</span>"
        //                 : "<span class='badge bg-danger'>Hidden</span>",
        //            action = "<button class='btn btn-danger btn-sm delete-btn' data-uuid='" + i.Uuid + "'>Delete</button>"
        //        })
        //    });


        //}
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> ViewEmployee()
        //{
        //    var employees = await _MasterEmployeeServices.GetAllEmployees(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    return View(employees);
        //}

        //[HttpPost]
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> GetEmployees()
        //{
        //    var employees = await _MasterEmployeeServices.GetAllEmployees(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    return Json(new { data = employees });
        //}

        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> AddEmployeeGeneral(string uuid)
        //{
        //    var model = new MasterEmployee();
        //    if (!string.IsNullOrEmpty(uuid))
        //    {
        //        model = _MasterEmployeeServices.GetEmployeeByUUID(uuid).Result;

        //        if (model == null)
        //        {
        //            return NotFound();
        //        }
        //        if (!string.IsNullOrEmpty(model.MasterCountryUuid))
        //        {
        //            var states = _MasterState.GetAllStates(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString())
        //                .Where(s => s.CountryUuid == model.MasterCountryUuid && s.IsActive == true)
        //                .Select(s => new SelectListItem
        //                {
        //                    Value = s.Uuid.ToString(),
        //                    Text = s.StateName
        //                })
        //                .ToList();
        //            ViewBag.States = states;
        //        }

        //        if (!string.IsNullOrEmpty(model.MasterStateUuid))
        //        {
        //            var cities = _MasterCity.GetAllCities(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString())
        //                .Where(c => c.StateUuid == model.MasterStateUuid && c.IsActive == true)
        //                .Select(c => new SelectListItem
        //                {
        //                    Value = c.Uuid.ToString(),
        //                    Text = c.CityName
        //                })
        //                .ToList();
        //            ViewBag.Cities = cities;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.States = new List<SelectListItem>(); 
        //        ViewBag.Cities = new List<SelectListItem>();
        //    }

        //    ViewBag.GeneratedUuid = uuid ?? Guid.NewGuid().ToString("D");

        //    PopulateDropdowns();

        //    return View(model);
        //}

        //[CheckCookie("UserUUID")]
        //public IActionResult EditEmployeeGeneral(string uuid)
        //{
        //    var model = new MasterEmployee();

        //    if (!string.IsNullOrEmpty(uuid))
        //    {
        //        model = _MasterEmployeeServices.GetEmployeeByUUID(uuid).Result;

        //        if (model == null)
        //        {
        //            return NotFound();
        //        }
        //        if (!string.IsNullOrEmpty(model.MasterCountryUuid))
        //        {
        //            var states = _MasterState.GetAllStates(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString())
        //                .Where(s => s.CountryUuid == model.MasterCountryUuid && s.IsActive == true)
        //                .Select(s => new SelectListItem
        //                {
        //                    Value = s.Uuid.ToString(),
        //                    Text = s.StateName
        //                })
        //                .ToList();
        //            ViewBag.States = states;
        //        }

        //        if (!string.IsNullOrEmpty(model.MasterStateUuid))
        //        {
        //            var cities = _MasterCity.GetAllCities(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString())
        //                .Where(c => c.StateUuid == model.MasterStateUuid && c.IsActive ==  true)
        //                .Select(c => new SelectListItem
        //                {
        //                    Value = c.Uuid.ToString(),
        //                    Text = c.CityName
        //                })
        //                .ToList();
        //            ViewBag.Cities = cities;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.States = new List<SelectListItem>();
        //        ViewBag.Cities = new List<SelectListItem>(); 
        //    }

        //    ViewBag.GeneratedUuid = uuid ?? Guid.NewGuid().ToString("D");
        //    PopulateDropdowns();

        //    return View("AddEmployeeGeneral", model);
        //}


        //[HttpPost]
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> AddEmployeeGeneral(MasterEmployee employee, IFormFile ProfilePicFile)
        //{
        //    if (ProfilePicFile != null)
        //    {
        //        employee.ProfilePic = await HandleProfilePicUpload(ProfilePicFile, employee);
        //    }
        //    else
        //    {
        //        var existingEmployee = await _MasterEmployeeServices.GetEmployeeByUUID(employee.Uuid);
        //        if (existingEmployee != null)
        //        {
        //            employee.ProfilePic = existingEmployee.ProfilePic; 
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(employee.MasterCountryUuid))
        //    {
        //        var country = _viewCountry.GetCountryByUUID(new Guid(employee.MasterCountryUuid));
        //        if (country != null)
        //        {
        //            employee.MasterCountryName = country.CountryName; 
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(employee.MasterStateUuid))
        //    {
        //        var state = _MasterState.GetStatesByUUID(new Guid(employee.MasterStateUuid));
        //        if (state != null)
        //        {
        //            employee.MasterStateName = state.StateName; 
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(employee.MasterCityUuid))
        //    {
        //        var city = _MasterCity.GetCitiesByUUID(new Guid(employee.MasterCityUuid));
        //        if (city != null)
        //        {
        //            employee.MasterCityName = city.CityName; 
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(employee.CurrentMasterCountryUuid))
        //    {
        //        var currentCountry = _viewCountry.GetCountryByUUID(new Guid(employee.CurrentMasterCountryUuid));
        //        if (currentCountry != null)
        //        {
        //            employee.CurrentMasterCountryName = currentCountry.CountryName; 
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(employee.CurrentMasterStateUuid))
        //    {
        //        var currentState = _MasterState.GetStatesByUUID(new Guid(employee.CurrentMasterStateUuid));
        //        if (currentState != null)
        //        {
        //            employee.CurrentMasterStateName = currentState.StateName; 
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(employee.CurrentMasterCityUuid))
        //    {
        //        var currentCity = _MasterCity.GetCitiesByUUID(new Guid(employee.CurrentMasterCityUuid));
        //        if (currentCity != null)
        //        {
        //            employee.CurrentMasterCityName = currentCity.CityName; 
        //        }
        //    }

        //    bool exists = await _MasterEmployeeServices.Exists(employee.Uuid);

        //    if (exists)
        //    {
        //        employee.MasterCompanyUuid = Request.Cookies["CmpUUID"].ToString();
        //        employee.MasterEnvironmentUuid = Request.Cookies["EnvUUID"].ToString();
        //        // Update existing employee
        //        var existingEmployee = await _MasterEmployeeServices.GetEmployeeByUUID(employee.Uuid);
        //        if (existingEmployee != null)
        //        {
        //            UpdateEmployeeProperties(existingEmployee, employee);
        //            await _MasterEmployeeServices.UpdateEmployee(existingEmployee);
        //        }
        //    }
        //    else
        //    {
        //        employee.MasterCompanyUuid = Request.Cookies["CmpUUID"].ToString();
        //        employee.MasterEnvironmentUuid = Request.Cookies["EnvUUID"].ToString();
        //        // Add new employee
        //        await _MasterEmployeeServices.AddEmployee(employee);
        //    }

        //    return RedirectToAction("ViewEmployee");
        //}




        //private async Task<string> HandleProfilePicUpload(IFormFile ProfilePicFile, MasterEmployee employee)
        //{
        //    if (ProfilePicFile != null)
        //    {
        //        if (ProfilePicFile.Length > 1048576)
        //        {
        //            ModelState.AddModelError("", "The uploaded image should not exceed 1 MB.");
        //            return employee.ProfilePic; ;
        //        }

        //        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        //        var fileExtension = Path.GetExtension(ProfilePicFile.FileName).ToLower();

        //        if (!allowedExtensions.Contains(fileExtension))
        //        {
        //            ModelState.AddModelError("", "Only jpg, jpeg, and png file formats are allowed.");
        //            return employee.ProfilePic; ;
        //        }

        //        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        //        if (!Directory.Exists(uploadsFolder))
        //        {
        //            Directory.CreateDirectory(uploadsFolder);
        //        }
        //        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicFile.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await ProfilePicFile.CopyToAsync(stream);
        //        }

        //        return $"/images/{uniqueFileName}";
        //    }

        //    return employee.ProfilePic; ;
        //}

        //private void UpdateEmployeeProperties(MasterEmployee existingEmployee, MasterEmployee employee)
        //{

        //    existingEmployee.ProfilePic = employee.ProfilePic;
        //    existingEmployee.MasterPrefixUuid = employee.MasterPrefixUuid;
        //    existingEmployee.FirstName = employee.FirstName;
        //    existingEmployee.MiddleName = employee.MiddleName;
        //    existingEmployee.LastName = employee.LastName;
        //    existingEmployee.MasterBloodGroupUuid = employee.MasterBloodGroupUuid;
        //    existingEmployee.MasterDepartmentUuid = employee.MasterDepartmentUuid;
        //    existingEmployee.MasterGenderUuid = employee.MasterGenderUuid;
        //    existingEmployee.EmployeeCode = employee.EmployeeCode;
        //    existingEmployee.ExpLimitDesignation = employee.ExpLimitDesignation;
        //    existingEmployee.PersonalEmail = employee.PersonalEmail;
        //    existingEmployee.Mobile = employee.Mobile;
        //    existingEmployee.ExpWorkflowDesignation = employee.ExpWorkflowDesignation;
        //    existingEmployee.ReportingDesignation = employee.ReportingDesignation;
        //    existingEmployee.MasterRolesUuid = employee.MasterRolesUuid;
        //    existingEmployee.Username = employee.Username;
        //    existingEmployee.Password = employee.Password;
        //    existingEmployee.IsLoginActive = employee.IsLoginActive;
        //    existingEmployee.Landline = employee.Landline;
        //}

        //private void PopulateDropdowns()
        //{
        //    var honorifics = _MasterHonorificServices.GetAllHonorificsAsync(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result;
        //    ViewBag.Prefix = honorifics.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.HonorificName
        //    }).ToList();

        //    var bloodGroups = _MasterBloodGroupServices.GetAllBloodGroupsAsync(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result;
        //    ViewBag.BloodGrp = bloodGroups.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.BloodGroupName
        //    }).ToList();

        //    var departments = _department.GetAllDepartments(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    ViewBag.Department = departments.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.DepartmentName
        //    }).ToList();

        //    var genders = _MasterGenderServices.GetAllGendersAsync(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result;
        //    ViewBag.Gender = genders.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.GenderName
        //    }).ToList();

        //    var roles = _role.GetAllRoles(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    ViewBag.Role = roles.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.UserRoleName
        //    }).ToList();

        //    var limitDesignations = _headDesignationService.GetAllHeadDesignations(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    ViewBag.LimitDesignation = limitDesignations.Result.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.DesignationName
        //    }).ToList();

        //    var workflowDesignations = _headDesignationService.GetAllHeadDesignations(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    ViewBag.Workflow = workflowDesignations.Result.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.DesignationName
        //    }).ToList();

        //    var reportingDesignations = _manageReportDesignationService.GetAllManageReportDesignations(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
        //    ViewBag.ReportDesignation = reportingDesignations.Result.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.DesignationName
        //    }).ToList();

        //    var countries = _viewCountry.GetAllCountries(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString()).Result;
        //    ViewBag.Country = countries.Select(c => new SelectListItem
        //    {
        //        Value = c.Uuid.ToString(),
        //        Text = c.CountryName
        //    }).ToList();
        //}




        //[HttpPost]
        //[CheckCookie("UserUUID")]
        //public async Task<IActionResult> DeleteEmployee(Guid uuid)
        //{
        //    await _MasterEmployeeServices.DeleteEmployee(uuid.ToString());
        //    return RedirectToAction("ViewEmployee");
        //}

        //#endregion



        //#region HRSetup
        //public async Task<List<SelectListItem>> GetDocumentTypes()
        //{
        //    var documentTypes = await _context.MasterDocumentCategories
        //        .Where(d => (bool)d.IsActive)
        //        .Select(d => new SelectListItem
        //        {
        //            Value = d.Uuid,
        //            Text = d.DocumentCategoryName
        //        })
        //        .ToListAsync();

        //    return documentTypes ?? new List<SelectListItem>();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddEmployeeHr(EmployeeSetupViewModel model, IFormFile DocumentFile)
        //{
        //    if (DocumentFile != null)
        //    {
        //        if (DocumentFile.Length > 2 * 1024 * 1024)
        //        {
        //            ModelState.AddModelError("", "The uploaded document should not exceed 2 MB.");
        //        }

        //        var allowedExtensions = new[] { ".pdf", ".xls", ".xlsx", ".jpg", ".jpeg", ".png" };
        //        var fileExtension = Path.GetExtension(DocumentFile.FileName).ToLower();

        //        if (!allowedExtensions.Contains(fileExtension))
        //        {
        //            ModelState.AddModelError("", "Only PDF, Excel, JPG, PNG, and JPEG files are allowed.");
        //        }


        //        if (ModelState.IsValid)
        //        {
        //            string filePath = await HandleDocumentUpload(DocumentFile);

        //            model.DocumentSetup.DocumentFile = filePath;
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {


        //        model.DocumentSetup.Uuid = model.DocumentSetup.Uuid ?? Guid.NewGuid().ToString();
        //        model.DocumentSetup.IsActive = true;
        //        model.DocumentSetup.IsAddedOn = DateTime.Now;
        //        model.DocumentSetup.IsAddedBy = "1";


        //        await _context.MasterEmployeeDocumentSetups.AddAsync(model.DocumentSetup);
        //        await _context.SaveChangesAsync();

        //        model.DocumentSetup = new MasterEmployeeDocumentSetup();
        //    }

        //    var documents = await _context.MasterEmployeeDocumentSetups
        //         .Join(_context.MasterDocumentCategories,
        //               doc => doc.DocumentTypeUuid,
        //               category => category.Uuid,
        //               (doc, category) => new MasterEmployeeDocumentSetup
        //               {
        //                   Id = doc.Id,
        //                   DocumentName = doc.DocumentName,
        //                   DocumentTypeUuid = category.DocumentCategoryName
        //               })
        //         .ToListAsync();

        //    model.DocumentSetupList = documents;

        //    ViewBag.DocumentTypes = await GetDocumentTypes();

        //    return View(model);
        //}

        //private async Task<string> HandleDocumentUpload(IFormFile documentFile)
        //{
        //    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/employeeGeneral");
        //    if (!Directory.Exists(uploadFolder))
        //    {
        //        Directory.CreateDirectory(uploadFolder);
        //    }

        //    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(documentFile.FileName);
        //    string filePath = Path.Combine(uploadFolder, uniqueFileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await documentFile.CopyToAsync(stream);
        //    }

        //    return $"/employeeGeneral/{uniqueFileName}";
        //}

        //public IActionResult AddEmployeeHr()
        //{
        //    var model = new EmployeeSetupViewModel
        //    {
        //        HrSetup = new MasterEmployeeHrSetup(),
        //        DocumentSetup = new MasterEmployeeDocumentSetup(),
        //        LeaveAuthorisation = new MasterEmployeeLeaveAuthorisation(),
        //        DocumentSetupList = new List<MasterEmployeeDocumentSetup>()
        //    };


        //    ViewBag.DocumentTypes = GetDocumentTypes().Result;

        //    return View(model);

        //}
        //public IActionResult AddEmployeeLibrary()
        //{
        //    return View();
        //}
        //#endregion



        //#region EmailTemplate
        //public IActionResult MasterViewEmailTemplate()
        //{
        //    return View();
        //}
        //public IActionResult MasterAddEmailTemplate()
        //{
        //    return View();
        //}
        //public IActionResult MasterEditEmailTemplate()
        //{
        //    return View();
        //}
        //#endregion


        #region Email Credentials

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> GetEmailCredentialData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.CompanySetupEmailCredential.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.Email_Address.Contains(searchValue) ||
                    i.Host_ServiceProvider.Contains(searchValue) ||
                    i.Port.Contains(searchValue));

            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(i => i.UUID),
                    "emailaddress" => query.OrderBy(i => i.Email_Address),
                    "password" => query.OrderBy(i => i.Password),
                    "host" => query.OrderBy(i => i.Host_ServiceProvider),
                    "smtp" => query.OrderBy(i => i.SMTP),
                    "port" => query.OrderBy(i => i.Port),
                    "status" => query.OrderBy(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.Id)
                };
            }
            else
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderByDescending(i => i.UUID),
                    "emailaddress" => query.OrderByDescending(i => i.Email_Address),
                    "password" => query.OrderByDescending(i => i.Password),
                    "host" => query.OrderByDescending(i => i.Host_ServiceProvider),
                    "smtp" => query.OrderByDescending(i => i.SMTP),
                    "port" => query.OrderByDescending(i => i.Port),
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
                    uuid = $"<a href='/CompanySetup/MasterEditEmailCredential/{i.UUID}' class='btnEdit' target='_blank'>{i.UUID}</a>",
                    emailaddress = i.Email_Address,
                    password = i.Password,
                    host = i.Host_ServiceProvider,
                    smtp = i.SMTP,
                    port = i.Port,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.UUID + "'>Delete</button>"
                })
            });
        }

        [CheckCookie("UserUUID")]
        public IActionResult MasterViewEmailCredential()
        {
            return View();
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult MasterEditEmailCredential(string UUID)
        {
            if (UUID == null)
            {
                return View(new CompanySetupEmailCredential());
            }

            CompanySetup_EmailCredential MY = s.CompanySetupEmailCredential.Get().Where(c => c.IsActive == true && c.UUID == UUID &&
            c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
            c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewEmailCredential");
            }
            else
            {
                CompanySetupEmailCredential MY1 = new CompanySetupEmailCredential();
                MY1 = _mapper.Map<CompanySetupEmailCredential>(MY);
                return View("MasterAddEmailCredential", MY1);
            }
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult MasterAddEmailCredential(string UUID)
        {
            if (UUID == null)
            {
                CompanySetupEmailCredential m = new CompanySetupEmailCredential();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var emailCredential = s.CompanySetupEmailCredential.Get().Where(c => c.IsActive == true && c.UUID == UUID &&
                c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();

                if (emailCredential == null)
                {
                    return RedirectToAction("MasterViewEmailCredential");
                }
                else
                {
                    return View(emailCredential);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> MasterAddEmailCredential(CompanySetupEmailCredential model)
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

                        var duplicateRecord = s.CompanySetupEmailCredential.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                (c.Email_Address == model.Email_Address || c.Host_ServiceProvider == model.Host_ServiceProvider) &&
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

                            CompanySetup_EmailCredential unit = _mapper.Map<CompanySetup_EmailCredential>(model);
                            s.CompanySetupEmailCredential.Add(unit);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewEmailCredential");
                        }



                    }
                    else
                    {

                        var existingRecord = s.CompanySetupEmailCredential.Get()
                            .FirstOrDefault(c => c.UUID == model.UUID);



                        var duplicateRecord = s.CompanySetupEmailCredential.Get()
                            .FirstOrDefault(c =>
                                c.IsActive == true &&
                                (c.Email_Address == model.Email_Address || c.Host_ServiceProvider == model.Host_ServiceProvider) &&
                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                c.UUID != model.UUID);

                        if (duplicateRecord != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(_mapper.Map<CompanySetupEmailCredential>(model));
                        }
                        else
                        {
                            existingRecord.Email_Address = model.Email_Address;
                            existingRecord.Password = model.Password;
                            existingRecord.Host_ServiceProvider = model.Host_ServiceProvider;
                            existingRecord.SMTP = model.SMTP;
                            existingRecord.Port = model.Port;
                            existingRecord.IsDisplay = model.IsDisplay;
                            existingRecord.IsUpdatedOn = u.CurrentIndianTime();
                            existingRecord.IsUpdateBy = Request.Cookies["UserUUID"].ToString();
                            existingRecord.UpdatedIP = u.GetLocalIPAddress();

                            s.CompanySetupEmailCredential.Update(existingRecord);

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("MasterViewEmailCredential");
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "danger";
                return View(_mapper.Map<CompanySetupEmailCredential>(model));
            }
        }

        [CheckCookie("UserUUID")]
        public IActionResult DeleteEmailCredentialMaster(string uuid)
        {
            try
            {
                CompanySetup_EmailCredential MY = s.CompanySetupEmailCredential.Get().Where(c => c.IsActive == true && c.UUID == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.Id).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeleteOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.CompanySetupEmailCredential.Update(MY);

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
            return RedirectToAction("MasterViewEmailCredential");
        }

        #endregion


        #region Role

        [HttpPost]
        public async Task<IActionResult> GetRole()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterUserRole.Get()
                .Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UserRoleId).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>

                    i.UserRoleName.Contains(searchValue) ||
                    i.Uuid.ToString().Contains(searchValue));
            }

            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderBy(i => i.Uuid),
                    "rolename" => query.OrderBy(i => i.UserRoleName),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderBy(i => i.UserRoleId)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(i => i.Uuid),
                    "rolename" => query.OrderByDescending(i => i.UserRoleName),
                    "status" => query.OrderByDescending(i => i.IsDisplay),
                    _ => query.OrderByDescending(i => i.UserRoleId)
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
                    uuid = $"<a href='/CompanySetup/EditRole/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    rolename = i.UserRoleName,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }

        // View User Role
        [CheckCookie("UserUUID")]
        public IActionResult ViewRole()
        {
            //var role = _role.GetAllRoles(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
            return View();

        }
        // Add User Role
        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> EditRole(string UUID)
        {
            if (UUID == null)
            {
                return View(new MasterUserRole());
            }

            Master_User_Role MY = s.MasterUserRole.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UserRoleId).FirstOrDefault();
            if (MY == null)
            {
                return RedirectToAction("MasterViewYear");
            }
            else
            {
                MasterUserRole MY1 = new MasterUserRole();
                MY1 = _mapper.Map<MasterUserRole>(MY);
                return View("AddRole", MY1);
            }

        }


        [HttpGet]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddRole(string UUID)
        {
            if (UUID == null)
            {
                MasterUserRole m = new MasterUserRole();
                m.IsActive = true;
                m.IsDisplay = true;
                return View(m);
            }
            else
            {
                var role = s.MasterUserRole.Get().Where(c => c.IsActive == true && c.Uuid == UUID && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UserRoleId).FirstOrDefault();

                if (role == null)
                {
                    return RedirectToAction("ViewRole");
                }
                else
                {
                    return View(role);
                }

            }
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public async Task<IActionResult> AddRole(MasterUserRole model)
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

                    if (string.IsNullOrEmpty(model.Uuid))
                    {
                        model.Master_Company_UUID = Request.Cookies["CmpUUID"].ToString();
                        model.Master_Environment_UUID = Request.Cookies["EnvUUID"].ToString();
                        model.IsActive = true;
                        model.AddedIP = u.GetLocalIPAddress();
                        model.IsAdddedOn = u.CurrentIndianTime();
                        model.IsAddedBy = Request.Cookies["UserUUID"].ToString();
                        model.RecordNo = u.GetRecordNo();
                        model.Uuid = u.GetUUID();
                        var role = s.MasterUserRole.Get().Where(c => c.IsActive == true && c.UserRoleName == model.UserRoleName && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UserRoleId).FirstOrDefault();


                        if (role != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            Master_User_Role MY = new Master_User_Role();
                            MY = _mapper.Map<Master_User_Role>(model);
                            s.MasterUserRole.Add(MY);

                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewRole");
                        }
                    }
                    else
                    {


                        var role = s.MasterUserRole.Get().FirstOrDefault(c => c.Uuid == model.Uuid);
                        //if (role == null)
                        //{
                        //    TempData["Message"] = "Record not found!";
                        //    TempData["MessageType"] = "danger";
                        //    return RedirectToAction("ViewRole");
                        //}

                        var isDuplicate = s.MasterUserRole.Get()
                    .FirstOrDefault(c => c.IsActive == true &&
                                         c.UserRoleName == model.UserRoleName &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.Uuid != model.Uuid);

                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }

                        else
                        {
                            role.UserRoleName = model.UserRoleName;
                            role.IsDisplay = model.IsDisplay;
                            role.IsUpdatedOn = u.CurrentIndianTime();
                            role.IsUpdatedBy = Request.Cookies["UserUUID"].ToString();
                            role.UpdatedIP = u.GetLocalIPAddress();


                            s.MasterUserRole.Update(role);
                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";

                            return RedirectToAction("ViewRole");
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

        // Delete User Role
        [HttpPost]
        public IActionResult DeleteRole(string uuid)
        {
            try
            {
                Master_User_Role MY = s.MasterUserRole.Get().Where(c => c.IsActive == true && c.Uuid == uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.UserRoleId).FirstOrDefault();
                if (MY != null)
                {
                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterUserRole.Update(MY);

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
            return RedirectToAction("ViewRole");
        }

        #endregion


        #region Menu Master

        [HttpPost]
        public async Task<IActionResult> GetMenuData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
            var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

            var query = s.MasterMenu.Get().Where(c => c.IsActive == true && c.Master_Company_UUID == Request.Cookies["CmpUUID"]
            .ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(i =>
                i.MenuName.Contains(searchValue));

            }



            var totalRecords = query.Count();

            if (sortColumnDirection == "asc")
            {
                query = sortColumn switch
                {
                    "uuid" => query.OrderBy(c => c.Uuid),
                    "menuname" => query.OrderBy(c => c.MenuName),
                    "menuicon" => query.OrderBy(c => c.MenuIcon),
                    "url" => query.OrderBy(c => c.Url),
                    "status" => query.OrderBy(c => c.IsDisplay),
                    _ => query.OrderBy(i => i.MenuId)
                };
            }
            else
            {
                query = sortColumn switch
                {

                    "uuid" => query.OrderByDescending(c => c.Uuid),
                    "menuname" => query.OrderByDescending(c => c.MenuName),
                    "menuicon" => query.OrderByDescending(c => c.MenuIcon),
                    "url" => query.OrderByDescending(c => c.Url),
                    "status" => query.OrderByDescending(c => c.IsDisplay),
                    _ => query.OrderByDescending(c => c.MenuId),
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
                    uuid = $"<a href='/CompanySetup/EditMenu/{i.Uuid}' class='btnEdit' target='_blank'>{i.Uuid}</a>",
                    menuname = i.MenuName,
                    menuicon = i.MenuIcon,
                    url = i.Url,
                    status = (bool)i.IsDisplay
                         ? "<span class='badge bg-success'>Visible</span>"
                         : "<span class='badge bg-danger'>Hidden</span>",
                    action = "<button class='btn btn-danger btn-sm delete-btn btnDelete ' data-uuid='" + i.Uuid + "'>Delete</button>"
                })
            });


        }


        [CheckCookie("UserUUID")]
        public IActionResult ViewMenu()
        {
            //var Menu = _imenu.GetAllMenu(Request.Cookies["CmpUUID"].ToString(), Request.Cookies["EnvUUID"].ToString());
            return View();
        }


        [HttpGet]
        public IActionResult GetParents()
        {
            var parents = s.MasterMenu.Get()
                .Where(x => x.IsParent == true && x.MenuLevel == 1)
                .Select(x => new { uuid = x.Uuid, menuName = x.MenuName })
                .ToList();

            return Json(parents);
        }

        [HttpGet]
        [CheckCookie("UserUUID")]
        public IActionResult EditMenu(string Uuid)
        {
            if (string.IsNullOrEmpty(Uuid))
            {
                var menus = s.MasterMenu.Get().Where(x => x.IsParent == true && x.MenuLevel == 1).ToList();
                var masters = s.MasterMenu.Get().Where(x => x.IsParent == true && x.MenuLevel == 2).ToList();
                var subMenus = s.MasterMenu.Get().Where(x => x.MenuLevel == 3).ToList();
                ViewBag.MenuName = new SelectList(menus, "Uuid", "MenuName");
                ViewBag.MenuN = new SelectList(masters, "Uuid", "MenuName");
                ViewBag.Menu = new SelectList(subMenus, "Uuid", "MenuName");
                return View(new MasterMenu());
            }
            else
            {
                // Retrieve Cookie Values Safely
                string cmpUUID = Request.Cookies["CmpUUID"];
                string envUUID = Request.Cookies["EnvUUID"];

                if (string.IsNullOrEmpty(cmpUUID) || string.IsNullOrEmpty(envUUID))
                {
                    return RedirectToAction("ViewMenu");
                }

                // Retrieve the Master_Menu record
                Master_Menu MY = s.MasterMenu.Get()
                    .Where(c => c.Uuid == Uuid && c.IsActive == true &&
                                c.Master_Company_UUID == cmpUUID &&
                                c.Master_Environment_UUID == envUUID)
                    .OrderByDescending(c => c.MenuId)
                    .FirstOrDefault();

                if (MY == null)
                {
                    return RedirectToAction("ViewMenu");
                }
                else
                {
                    MasterMenu MY1 = _mapper.Map<MasterMenu>(MY);

                    var menus = s.MasterMenu.Get().Where(x => x.IsParent == true && x.MenuLevel == 1).ToList();
                    var masters = s.MasterMenu.Get().Where(x => x.IsParent == true && x.MenuLevel == 2).ToList();
                    var subMenus = s.MasterMenu.Get().Where(x => x.MenuLevel == 3).ToList();
                    ViewBag.MenuName = new SelectList(menus, "Uuid", "MenuName");
                    ViewBag.MenuN = new SelectList(masters, "Uuid", "MenuName"); 
                    ViewBag.Menu = new SelectList(subMenus, "Uuid", "MenuName");
                    MY1.SubParentUUID = MY.SubParentUUID;
                    ViewBag.SelectedMenuLevel = MY1.MenuLevel;

                    return View("AddMenu", MY1);
                }
            }


        }

        [CheckCookie("UserUUID")]
        public IActionResult AddMenu(string Uuid)
        {
            if (Uuid == null)
            {
                MasterMenu m = new MasterMenu();
                m.IsActive = true;
                m.IsDisplay = true;
                var menus = s.MasterMenu.Get().Where(x => (bool)(x.IsParent = true && x.MenuLevel == 1)).ToList();
                var masters = s.MasterMenu.Get().Where(x => (bool)(x.IsParent = true && x.MenuLevel == 2)).ToList();
                ViewBag.MenuName = new SelectList(menus, "Uuid", "MenuName");
                ViewBag.MenuN = new SelectList(masters, "Uuid", "MenuName");
                return View(m);
            }
            else
            {
                var menu = s.MasterMenu.Get().Where(c => c.Uuid == Uuid && c.IsActive == true &&
                c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() &&
                c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                    .FirstOrDefault();
                if (menu == null)
                {
                    return RedirectToAction("ViewMenu");
                }
                else
                {
                    var menus = s.MasterMenu.Get().Where(x => (bool)(x.IsParent = true && x.MenuLevel == 1)).ToList();
                    var masters = s.MasterMenu.Get().Where(x => (bool)(x.IsParent = true && x.MenuLevel == 2)).ToList();
                    ViewBag.MenuName = new SelectList(menus, "Uuid", "MenuName");
                    ViewBag.MenuN = new SelectList(masters, "Uuid", "MenuName");
                    return View(menu);
                }

            }
        }



        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult AddMenu(MasterMenu model)
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

                    if (string.IsNullOrEmpty(model.Uuid))
                    {
                        var isduplicate = s.MasterMenu.Get()
                        .FirstOrDefault(c => c.MenuName == model.MenuName &&
                                         c.MainParentUUID == model.MainParentUUID &&
                                         c.SubParentUUID == model.SubParentUUID &&
                                         c.Master_Company_UUID == model.Master_Company_UUID &&
                                         c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                         c.IsActive == true);
                        if (isduplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            model.IsActive = true;
                            model.IsAdddedOn = u.CurrentIndianTime();
                            model.AddedIP = u.GetLocalIPAddress();
                            model.Uuid = u.GetUUID();
                            model.RecordNo = u.GetRecordNo();
                            model.IsAddedBy = Request.Cookies["UserUUID"].ToString();

                            /*                            bool isDuplicate = _context.MasterMenus
                                                           .Any(c => c.MenuName == model.MenuName);
                                                        if (isDuplicate)
                                                        {
                                                            TempData["Message"] = "Data already exists!";
                                                            return View(model);
                                                        }
                            */
                            Master_Menu MY = _mapper.Map<Master_Menu>(model);
                            MY = _mapper.Map<Master_Menu>(model);
                            s.MasterMenu.Add(MY);
                            TempData["Message"] = "Data Inserted Successfully!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("ViewMenu");
                        }

                    }
                    else
                    {

                        // Edit operation
                        var menu = s.MasterMenu.Get().FirstOrDefault(c => c.Uuid == model.Uuid);

                        var isDuplicate = s.MasterMenu.Get()
                           .FirstOrDefault(c => c.IsActive == true &&
                                                c.MenuName == model.MenuName &&
                                                c.MainParentUUID == model.MainParentUUID &&
                                                c.SubParentUUID == model.SubParentUUID &&
                                                c.Master_Company_UUID == model.Master_Company_UUID &&
                                                c.Master_Environment_UUID == model.Master_Environment_UUID &&
                                                c.Uuid != model.Uuid);
                        /* if (year == null)
                         {
                             return RedirectToAction("ViewMenu");
                         }

                         bool isDuplicate = _context.MasterMenus
                             .Any(c => c.MenuName == model.MenuName && c.Uuid != model.Uuid);
 */
                        if (isDuplicate != null)
                        {
                            TempData["Message"] = "Data already exists!";
                            TempData["MessageType"] = "danger";
                            return View(model);
                        }
                        else
                        {
                            // Update the existing record
                            menu.MenuName = model.MenuName;
                            menu.MainParentUUID = model.MainParentUUID;
                            menu.SubParentUUID = model.SubParentUUID;
                            menu.Uuid = model.Uuid;
                            menu.MenuIcon = model.MenuIcon;
                            menu.Url = model.Url;
                            menu.IsDisplay = model.IsDisplay;
                            menu.IsUpdatedOn = DateTime.Now;
                            menu.IsUpdatedBy = "1";

                            s.MasterMenu.Update(menu);
                            // _context.SaveChanges();

                            TempData["Message"] = "Data Updated Successfully!";
                            TempData["MessageType"] = "success";
                        }
                        return RedirectToAction("ViewMenu");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An error occurred: {ex.Message}";
                TempData["MessageType"] = "danger";
                return View(model);
            }

        }


        public JsonResult GetMainParentDrp(int id)
        {
            var secondList = s.MasterMenu.Get().Where(c => c.IsParent == true &&
            c.MenuLevel == 1 && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())

                                  .Select(m => new SelectListItem
                                  {
                                      Text = m.MenuName,
                                      Value = m.Uuid
                                  })
                                  .ToList();


            return Json(secondList);
        }
        public JsonResult GetSubParentDrp(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Invalid Main Parent ID" });
            }
            var subParents = s.MasterMenu.Get().Where(c => c.IsParent == true && c.MenuLevel == 2 && c.MainParentUUID == id && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString()
            && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString())
                               .Select(m => new SelectListItem
                               {
                                   Text = m.MenuName,
                                   Value = m.Uuid
                               })
                               .ToList();

            return Json(subParents);
        }

        [HttpPost]
        [CheckCookie("UserUUID")]
        public IActionResult DeleteMenu(string Uuid)
        {
            //var result = _context.MasterMenus.FirstOrDefault(c => c.Uuid == Uuid.ToString());
            //if (result != null)
            //{
            //    result.IsDeletedOn = DateTime.Now;
            //    result.IsDeletedBy = "1";
            //    result.IsActive = false;
            //    _context.MasterMenus.Update(result);
            //    _context.SaveChanges();
            //}


            try
            {
                Master_Menu MY = s.MasterMenu.Get().Where(c => c.IsActive == true && c.Uuid == Uuid && c.Master_Company_UUID == Request.Cookies["CmpUUID"].ToString() && c.Master_Environment_UUID == Request.Cookies["EnvUUID"].ToString()).OrderByDescending(c => c.MenuId).FirstOrDefault();
                if (MY != null)
                {

                    MY.IsActive = false;
                    MY.IsDeletedOn = u.CurrentIndianTime();
                    MY.IsDeletedBy = Request.Cookies["UserUUID"].ToString();
                    MY.DeletedIP = u.GetLocalIPAddress();
                    s.MasterMenu.Update(MY);

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
            return RedirectToAction("ViewMenu");
        }


        #endregion

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
            if (id != "0")
            {
                Response.Cookies.Append("EnvUUID", id, options);

                var items = "";

                var Mainmenu = s.MasterEnvironment.Get().Where(c => c.IsActive == true).ToList();
                if (Mainmenu.Count > 0)
                {
                    for (int i = 0; i < Mainmenu.Count; i++)
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
                    else
                    {
                        for (int i = 0; i < Mainmenu.Count; i++)
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
            if (id != "0")
            {
                Response.Cookies.Append("CmpUUID", id, options);

                var items = "";

                var Mainmenu = s.MasterCompany.Get().Where(c => c.IsActive == true).ToList();
                if (Mainmenu.Count > 0)
                {
                    for (int i = 0; i < Mainmenu.Count; i++)
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
                    else
                    {
                        for (int i = 0; i < Mainmenu.Count; i++)
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
