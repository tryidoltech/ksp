﻿using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ER_HeadDesignationLogic : ER_HeadDesignationRepository
    {
        private AppDbContext _context;
        public ER_HeadDesignationLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
