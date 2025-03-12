﻿using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_ModeOfPaymentLogic : AC_ModeOfPaymentRepository
    {
        private AppDbContext _context;
        public AC_ModeOfPaymentLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
