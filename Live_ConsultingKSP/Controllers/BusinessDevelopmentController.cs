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
    public class BusinessDevelopmentController : Controller
    {

        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public BusinessDevelopmentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        
        public IActionResult ViewResearchGroup()
        {
            return View();
        }
        public IActionResult AddResearchGroup()
        {
            return View();
        }
        public IActionResult EditResearchGroup()
        {
            return View();
        }
        public IActionResult ViewResearchGrpData()
        {
            return View();
        }
        public IActionResult AddMiningData()
        {
            return View();
        }

        public IActionResult ViewDataFiltration()
        {
            return View();
        }
        public IActionResult ManageFiltrationData()
        {
            return View();
        }



        public IActionResult ManagePositiveLead()
        {
            return View();
        }
        public IActionResult UpdatePositiveData()
        {
            return View();
        }

    }
}
