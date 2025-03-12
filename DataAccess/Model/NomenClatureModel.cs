using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class NomenClatureModel
    {
        public AC_FinancialYear CM { get; set; }
        public AC_NomenClature CTY { get; set; }

        public string Title { get; set; }
        public string UUID { get; set; }

        public NomenClatureModel()
        {
            CM = new AC_FinancialYear();
            CTY = new AC_NomenClature();
        }
    }
}
