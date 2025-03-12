using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
namespace DataAccess.Logic
{
    public class Master_UserMenuRightLogic : Master_UserMenuRightRepository
    {
        private AppDbContext _context;
        public Master_UserMenuRightLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<MenuRecordsModel> getMenuRecordsModel(string RoleUUID)
        {
            List<MenuRecordsModel> CSM = (from menu in _context.Master_Menu
                              join menuRight in _context.Master_User_MenuRight
                              on menu.Uuid.ToString() equals menuRight.MenuUuid
                              where menuRight.UserRoleUuid.ToString() == RoleUUID
                              select new MenuRecordsModel
                              {
                                  Url = menu.Url,
                                  MenuName = menu.MenuName,
                                  MenuIcon = menu.MenuIcon,
                                  MenuLevel = menu.MenuLevel,
                                  MainParentUuid = menu.MainParentUUID,
                                  SubParentUuid = menu.SubParentUUID,
                                  Sequence = menu.Sequence,
                                  IsParent = menu.IsParent,
                                  Uuid = menu.Uuid,
                                  UserRoleUuid = menuRight.UserRoleUuid,
                                  IsRead = menuRight.IsRead,
                                  IsWrite = menuRight.IsWrite,
                                  IsEdit = menuRight.IsEdit,
                                  IsDelete = menuRight.IsDelete

                              }).ToList();
            return CSM;
        }
    }
}
