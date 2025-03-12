using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class ItemMasterModel
    {
        public AC_ItemMaster CM { get; set; }
        public AC_ItemGroup SM { get; set; }
        public string Group_Name { get; set; }
        public ItemMasterModel()
        {
            CM = new AC_ItemMaster();
            SM = new AC_ItemGroup();

        }

    }
}
