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
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Live_ConsultingKSP.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public LibraryController(IMapper mapper)
        {
            _mapper = mapper;
        }


        public IActionResult DocumentTreeView()
        {
            return View();
        }

        public IActionResult ManageLibrarySearch()
        {
            return View();
        }
        public IActionResult ViewRenewableDocumentsListReminder()
        {
            return View();
        }




    }
}
