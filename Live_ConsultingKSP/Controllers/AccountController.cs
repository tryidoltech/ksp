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
using DataAccess.Logic;
using Microsoft.IdentityModel.Tokens;

namespace Live_ConsultingKSP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public AccountController(IMapper mapper)
        {
            _mapper = mapper;
        }



        //public IActionResult Index()
        //{
        //    return View();
        //}
       
       

        

       

       

        public IActionResult ViewSalesCustomerInquiry()
        {
            return View();
        }
        public IActionResult AddSalesCustomerInquiry()
        {
            return View();
        }

        public IActionResult ViewSalesOrder()
        {
            return View();
        }
        public IActionResult AddSalesOrder()
        {
            return View();
        }
        public IActionResult PDF_sales_order()
        {
            return View();
        }

        public IActionResult ViewSalePerfomaInvoice()
        {
            return View();
        }
        public IActionResult AddSalesPerfomaInvoice()
        {
            return View();
        }
        public IActionResult ViewSaleInvoice()
        {
            return View();
        }
        public IActionResult PDF_packing_slip()
        {
            return View();
        }

        public IActionResult AddSaleInvoice()
        {
            return View();
        }
        public IActionResult ViewPurchaseInquiry()
        {
            return View();
        }
        public IActionResult AddPurchaseInquiry()
        {
            return View();
        }
        public IActionResult ViewPurchaseQuotation()
        {
            return View();
        }
        public IActionResult AddPurchaseQuotation()
        {
            return View();
        }
        public IActionResult ViewPurchaseOrder()
        {
            return View();
        }
        public IActionResult AddPurchaseOrder()
        {
            return View();
        }
        public IActionResult ViewPurchasePerformaInvoice()
        {
            return View();
        }
        public IActionResult AddPurchasePerformaInvoice()
        {
            return View();
        }
        public IActionResult ViewPurchaseInvoice()
        {
            return View();
        }
        public IActionResult AddPurchaseInvoice()
        {
            return View();
        }

       

       


        


        

      

       



        public IActionResult PDF_commercial_invoice()
        {
            return View();
        }
        public IActionResult PDF_credit_note()
        {
            return View();
        }
        public IActionResult PDF_debit_note()
        {
            return View();
        }
        public IActionResult PDF_delievery_note()
        {
            return View();
        }
        public IActionResult PDF_journal_voucher()
        {
            return View();
        }
        public IActionResult PDF_material_transfer_voucher()
        {
            return View();
        }
        public IActionResult PDF_payment_vaoucher_template()
        {
            return View();
        }
        public IActionResult PDF_proforma_invoice()
        {
            return View();
        }
        public IActionResult PDF_purchase_order()
        {
            return View();
        }
        public IActionResult PDF_sales_day_book()
        {
            return View();
        }
        public IActionResult PDF_SteelBear_Purchase_Order()
        {
            return View();
        }




    }
}
