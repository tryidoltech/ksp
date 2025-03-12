﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_GenderRepository : Repository<Master_Gender>
    {
        private AppDbContext _context;
        public Master_GenderRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Gender Update(Master_Gender obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
