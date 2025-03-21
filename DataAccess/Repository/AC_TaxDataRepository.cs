﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_TaxDataRepository : Repository<AC_TaxData>
    {
        private AppDbContext _context;
        public AC_TaxDataRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_TaxData Update(AC_TaxData obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
