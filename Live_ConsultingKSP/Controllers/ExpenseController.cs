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
