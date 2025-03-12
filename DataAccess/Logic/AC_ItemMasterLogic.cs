using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_ItemMasterLogic : AC_ItemMasterRepository
    {
        private AppDbContext _context;
        public AC_ItemMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<ItemMasterModel> GetItemMasterModel()
        {
            List<ItemMasterModel> SSM = (from p in _context.AC_ItemGroup.Where(q => q.IsActive == true)
                                    join r in _context.AC_ItemMaster.Where(t => t.IsActive == true)
                                    on p.UUID equals r.ItemGroup_UUID
                                    select new ItemMasterModel
                                    {
                                        CM = r,
                                        SM = p,
                                        Group_Name = p.Group_Name
                                    }).ToList();
            return SSM;
        }
    }
}
