using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class GroupService : IGroupService
    {
        private readonly KsperpDbContext _context;

        public GroupService(KsperpDbContext context)
        {
            _context = context;
        }

        public MasterService GetGroupByUuid(Guid uuid)
        {
            return _context.MasterServices.FirstOrDefault(g => g.Uuid == uuid.ToString());
        }

        public void AddGroup(MasterService group)
        {
            if (_context.MasterServices.Any(g => g.ServiceName == group.ServiceName))
                throw new Exception("Group already exists!");

            group.Uuid = Guid.NewGuid().ToString();
            group.IsActive = true;
            group.IsAddedOn = DateTime.Now;
            group.IsAddedBy = "1";

            _context.MasterServices.Add(group);
            _context.SaveChanges();
        }

        public List<MasterService> GetAllGroups(string cmpuuid, string envuuid)
        {
            return _context.MasterServices
                .Where(g => (bool)g.IsActive && g.MasterCompanyUuid == cmpuuid && g.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(g => g.ServiceId)
                .ToList();
        }

        public void UpdateGroup(MasterService group)
        {
            var existingGroup = _context.MasterServices.FirstOrDefault(g => g.Uuid == group.Uuid);
            if (existingGroup == null) throw new Exception("Group not found!");

            if (_context.MasterServices.Any(g => g.ServiceName == group.ServiceName && g.Uuid != group.Uuid))
                throw new Exception("Group name already exists!");

            existingGroup.ServiceName = group.ServiceName;
            existingGroup.IsDisplay = group.IsDisplay;
            existingGroup.IsUpdatedOn = DateTime.Now;
            existingGroup.IsUpdatedBy = "1";

            _context.SaveChanges();
        }
        public void DeleteGroup(Guid uuid)
        {
            var group = _context.MasterServices.FirstOrDefault(g => g.Uuid == uuid.ToString());
            if (group == null)
                throw new Exception("Service Group not found!");

            // Perform a soft delete by marking it as inactive
            group.IsActive = false;
            group.IsDeletedOn = DateTime.Now;
            group.IsDeletedBy = "1"; // Replace with the actual user ID if available

            _context.MasterServices.Update(group);
            _context.SaveChanges();
        }


    }
}
