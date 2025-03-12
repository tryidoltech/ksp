using Live_ConsultingKSP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Live_ConsultingKSP.Services
{
    public class MasterMenuServices : IMasterMenuServices
    {
        private readonly KsperpDbContext context;

        public MasterMenuServices(KsperpDbContext context)
        {
            this.context = context;
        }
        public void AddMenu(Models.MasterMenu masterMenu)
        {
            bool isDuplicate = context.MasterMenus
                       .Any(c => c.MenuName == masterMenu.MenuName && c.MenuIcon == masterMenu.MenuIcon && c.Sequence == masterMenu.Sequence && c.Url == masterMenu.Url);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }

            
            masterMenu.IsDisplay = masterMenu.IsDisplay;
            masterMenu.Uuid = Guid.NewGuid().ToString();
            masterMenu.IsActive = true;
            masterMenu.IsAdddedOn = DateTime.Now;
            masterMenu.IsAddedBy = "1";
            context.MasterMenus.Add(masterMenu);
            context.SaveChanges();
        }

        public void DeleteMenu(Guid uuid)
        {
            var result = context.MasterMenus.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeletedOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                context.MasterMenus.Update(result);
                context.SaveChanges();
            }
        }

        public List<Models.MasterMenu> GetAllMenu(string cmpuuid, string envuuid)
        {

            return context.MasterMenus.Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid).
                OrderByDescending(c => c.MenuId).ToList();
        }

        public Models.MasterMenu GetByMenu(Guid uuid)
        {
            return context.MasterMenus.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateMenu(Models.MasterMenu master)
        {
            bool isDuplicate = context.MasterMenus
        .Any(c => (c.MenuName == master.MenuName)
                  && c.Uuid != master.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = context.MasterMenus.FirstOrDefault(c => c.Uuid == master.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.MenuName = master.Uuid;
                existingEnvironment.IsDisplay = master.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdatedBy = "1";

                context.SaveChanges();
            }
        }



    }
}
