using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Repository
{
    public class MasterRole : IMasterRole
    {
        private readonly KsperpDbContext _context;

        public MasterRole(KsperpDbContext context)
        {
            _context = context;
        }
        public void AddRole(MasterUserRole role)
        {
            bool isDuplicate = _context.MasterUserRoles
                       .Any(c => c.UserRoleName == role.UserRoleName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            role.IsActive = true;
            role.IsAdddedOn = DateTime.Now;
            role.IsAddedBy = "1";
            _context.MasterUserRoles.Add(role);
            _context.SaveChanges();
        }
        public List<MasterUserRole> GetAllRoles(string cmpuuid, string envuuid)
        {
            return _context.MasterUserRoles.Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.UserRoleId).ToList();
        }
        public MasterUserRole GetRoleByUUID(Guid uuid)
        {
            return _context.MasterUserRoles.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateRoles(MasterUserRole role)
        {
            bool isDuplicate = _context.MasterUserRoles
        .Any(c => (c.UserRoleName == role.UserRoleName)
                  && c.Uuid != role.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingrole = _context.MasterUserRoles.FirstOrDefault(c => c.Uuid == role.Uuid);
            if (existingrole != null)
            {
                existingrole.UserRoleName = role.UserRoleName;
                existingrole.IsDisplay = role.IsDisplay;
                existingrole.IsUpdatedOn = DateTime.Now;
                existingrole.IsUpdatedBy = "1";

                _context.SaveChanges();
            }
        }
        public void DeleteRoles(Guid uuid)
        {
            var role = _context.MasterUserRoles.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (role != null)
            {
                role.IsDeletedOn = DateTime.Now;
                role.IsDeletedBy = "1";
                role.IsActive = false;
                _context.MasterUserRoles.Update(role);
                _context.SaveChanges();
            }
        }
    }

}
