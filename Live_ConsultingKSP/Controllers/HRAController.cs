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

        public IActionResult ManageAttendence()
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