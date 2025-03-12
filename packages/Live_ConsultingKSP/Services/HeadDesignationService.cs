using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class HeadDesignationService : IHeadDesignationService
    {
        private readonly KsperpDbContext _context;

        public HeadDesignationService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddHeadDesignation(ErHeadDesignation designation)
        {
            if (_context.ErHeadDesignations.Any(hd => hd.DesignationName == designation.DesignationName))
                throw new Exception("Head Designation already exists!");

            designation.IsActive = true;
            designation.IsAddedOn = DateTime.Now;
            designation.IsAddedBy = "1";

            _context.ErHeadDesignations.Add(designation);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ErHeadDesignation>> GetAllHeadDesignations(string cmpuuid, string envuuid)
        {
            return await _context.ErHeadDesignations
                .Where(hd => hd.IsActive == true && hd.MasterCompanyUuid == cmpuuid && hd.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(hd => hd.HeadDesignationId)
                .ToListAsync();
        }



        public ErHeadDesignation GetHeadDesignationByUUID(string uuid)
        {
            return _context.ErHeadDesignations.FirstOrDefault(hd => hd.Uuid == uuid.ToString());
        }

        public void UpdateHeadDesignation(ErHeadDesignation designation)
        {
            var existingDesignation = _context.ErHeadDesignations.FirstOrDefault(hd => hd.Uuid == designation.Uuid);
            if (existingDesignation == null) throw new Exception("Head Designation not found!");

            if (_context.ErHeadDesignations.Any(hd => hd.DesignationName == designation.DesignationName && hd.Uuid != designation.Uuid))
                throw new Exception("Head Designation already exists!");

            existingDesignation.DesignationName = designation.DesignationName;
            existingDesignation.IsDisplay = designation.IsDisplay;
            existingDesignation.IsUpdatedOn = DateTime.Now;
            existingDesignation.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteHeadDesignation(string uuid)
        {
            var designation = _context.ErHeadDesignations.FirstOrDefault(hd => hd.Uuid == uuid.ToString());
            if (designation == null) throw new Exception("Head Designation not found!");

            designation.IsActive = false;
            designation.IsDeletedOn = DateTime.Now;
            designation.IsDeletedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteHeadDesignation(Guid uuid)
        {
            throw new NotImplementedException();
        }
    }
}
