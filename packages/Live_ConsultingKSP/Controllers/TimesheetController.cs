using Microsoft.AspNetCore.Mvc;
using System;
using KSPLogin.Models;
using System.Net.Sockets;
using System.Net;
using DataAccess;
using DataAccess.Entities;
using System.Data;
using AutoMapper;

namespace Live_ConsultingKSP.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public TimesheetController(IMapper mapper)
        {
            _mapper = mapper;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [CheckCookie("UserUUID")]
        public IActionResult ManageTimesheet()
        {
            string CmpUUID = Request.Cookies["CmpUUID"].ToString();
            string EnvUUID = Request.Cookies["EnvUUID"].ToString();
            string UserUUID = Request.Cookies["UserUUID"].ToString();



            // Get the current date
            DateTime today = DateTime.Now;

            // Determine the start and end of the week (Monday to Sunday)
            int diff = today.DayOfWeek == DayOfWeek.Sunday ? -6 : (int)DayOfWeek.Monday - (int)today.DayOfWeek;

            DateTime startOfWeek = today.AddDays(diff); // Start of the week (Monday)
            DateTime endOfWeek = startOfWeek.AddDays(6); // End of the week (Sunday)


            string CurrentDay = today.DayOfWeek.ToString();
            string CurrentDate = today.ToString("yyyy-MM-dd");
            string StartOfWeek = startOfWeek.ToString("yyyy-MM-dd");
            string EndOfWeek = endOfWeek.ToString("yyyy-MM-dd");


            return View();
        }
        public IActionResult ManageTimeSheetApproval()
        {
            return View();
        }
        public IActionResult ManageMyWorkList()
        {
            return View();
        }

    }
}
