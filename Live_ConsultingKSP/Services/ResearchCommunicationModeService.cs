using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class ResearchCommunicationModeService : IResearchCommunicationModeService
    {
        private readonly KsperpDbContext _context;

        public ResearchCommunicationModeService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddCommunicationMode(BdResearchCommunicationMode mode)
        {
            if (_context.BdResearchCommunicationModes.Any(m => m.ResearchCommunicationModeName == mode.ResearchCommunicationModeName))
                throw new Exception("Communication mode already exists!");

            mode.Uuid = Guid.NewGuid().ToString();
            mode.IsActive = true;
            mode.IsAddedOn = DateTime.Now;
            mode.IsAddedBy = "1";

            _context.BdResearchCommunicationModes.Add(mode);
            _context.SaveChanges();
        }

        public List<BdResearchCommunicationMode> GetAllCommunicationModes(string cmpuuid, string envuuid)
        {
            return _context.BdResearchCommunicationModes
                .Where(m => (bool)m.IsActive && m.MasterCompanyUuid == cmpuuid && m.MasterEnvironmentUuid == envuuid) // Only active modes
                .OrderByDescending(m => m.ResearchCommunicationModeId)
                .ToList();
        }

        public BdResearchCommunicationMode GetCommunicationModeByUUID(Guid uuid)
        {
            return _context.BdResearchCommunicationModes.FirstOrDefault(m => m.Uuid == uuid.ToString());
        }

        public void UpdateCommunicationMode(BdResearchCommunicationMode mode)
        {
            var existingMode = _context.BdResearchCommunicationModes.FirstOrDefault(m => m.Uuid == mode.Uuid);
            if (existingMode == null) throw new Exception("Communication mode not found!");

            if (_context.BdResearchCommunicationModes.Any(m => m.ResearchCommunicationModeName == mode.ResearchCommunicationModeName && m.Uuid != mode.Uuid))
                throw new Exception("Communication mode name already exists!");

            // Update the specific fields
            existingMode.ResearchCommunicationModeName = mode.ResearchCommunicationModeName;
            existingMode.IsDisplay = mode.IsDisplay;
            existingMode.IsUpdatedOn = DateTime.Now;
            existingMode.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteCommunicationMode(Guid uuid)
        {
            var mode = _context.BdResearchCommunicationModes.FirstOrDefault(m => m.Uuid == uuid.ToString());
            if (mode == null) throw new Exception("Communication mode not found!");

            mode.IsActive = false;
            mode.IsDeletedOn = DateTime.Now;
            mode.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}