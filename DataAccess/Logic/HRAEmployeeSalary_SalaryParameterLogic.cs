﻿using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRAEmployeeSalary_SalaryParameterLogic : HRAEmployeeSalary_SalaryParameterRepository
    {
        private AppDbContext _context;
        public HRAEmployeeSalary_SalaryParameterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
