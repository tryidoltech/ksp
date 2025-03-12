using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class BdResearchAudienceService : IBdResearchAudienceService
    {
        private readonly KsperpDbContext _context;

        public BdResearchAudienceService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddResearchAudience(BdResearchAudience audience)
        {
            if (_context.BdResearchAudiences.Any(a => a.ResearchAudienceName == audience.ResearchAudienceName))
                throw new Exception("Audience already exists!");

            audience.Uuid = Guid.NewGuid().ToString();
            audience.IsActive = true;
            audience.IsAddedOn = DateTime.Now;
            audience.IsAddedBy = "1";

            _context.BdResearchAudiences.Add(audience);
            _context.SaveChanges();
        }

        public List<BdResearchAudience> GetAllResearchAudiences(string cmpuuid, string envuuid)
        {
            return _context.BdResearchAudiences
                .Where(a => (bool)a.IsActive && a.MasterCompanyUuid == cmpuuid && a.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(a => a.ResearchAudienceId)
                .ToList();
        }

        public BdResearchAudience GetResearchAudienceByUUID(string uuid)
        {
            return _context.BdResearchAudiences.FirstOrDefault(a => a.Uuid == uuid);
        }

        public void UpdateResearchAudience(BdResearchAudience audience)
        {
            var existingAudience = _context.BdResearchAudiences.FirstOrDefault(a => a.Uuid == audience.Uuid);
            if (existingAudience == null) throw new Exception("Audience not found!");

            if (_context.BdResearchAudiences.Any(a => a.ResearchAudienceName == audience.ResearchAudienceName && a.Uuid != audience.Uuid))
                throw new Exception("Audience name already exists!");

            existingAudience.ResearchAudienceName = audience.ResearchAudienceName;
            existingAudience.IsDisplay = audience.IsDisplay;
            existingAudience.IsUpdatedOn = DateTime.Now;
            existingAudience.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteResearchAudience(string uuid)
        {
            var audience = _context.BdResearchAudiences.FirstOrDefault(a => a.Uuid == uuid);
            if (audience == null) throw new Exception("Audience not found!");

            audience.IsActive = false;
            audience.IsDeletedOn = DateTime.Now;
            audience.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }

}
