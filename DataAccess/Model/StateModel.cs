using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class StateModel
    {
        public Master_Country CM { get; set; }
        public Master_State SM { get; set; }
        public string CountryName { get; set; }
        public StateModel()
        {
            CM = new Master_Country();
            SM = new Master_State();

        }

    }
}
