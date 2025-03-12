﻿using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_Employee_EducationDetailsLogic : Master_Employee_EducationDetailsRepository
    {
        private AppDbContext _context;
        public Master_Employee_EducationDetailsLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}