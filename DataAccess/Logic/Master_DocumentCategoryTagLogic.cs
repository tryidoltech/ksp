﻿using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_DocumentCategoryTagLogic : Master_DocumentCategoryTagRepository
    {
        private AppDbContext _context;
        public Master_DocumentCategoryTagLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
