using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class ERCurrencyModel
    {
        public Master_Country CM { get; set; }

        public ERSetup_Currency EC { get; set; }
        
        public string CountryName { get; set; }
        public ERCurrencyModel()
        {
            CM = new Master_Country();
            

        }

    }
}
