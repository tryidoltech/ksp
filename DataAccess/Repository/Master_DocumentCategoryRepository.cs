﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_DocumentCategoryRepository : Repository<Master_DocumentCategory>
    {
        private AppDbContext _context;
        public Master_DocumentCategoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_DocumentCategory Update(Master_DocumentCategory obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
