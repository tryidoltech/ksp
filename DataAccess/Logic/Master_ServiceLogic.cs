﻿using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_ServiceLogic : Master_ServiceRepository
    {
        private AppDbContext _context;
        public Master_ServiceLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
