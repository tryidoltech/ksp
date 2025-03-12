using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class MasterIndustryServices : IMasterIndustryServices
    {
        private readonly KsperpDbContext _context;

        public MasterIndustryServices(KsperpDbContext context)
        {
            _context = context;
        }

        // Get all active industry sectors
        public async Task<List<MasterIndustry>> GetAllIndustriesAsync(string cmpuuid,string envuuid)
        {
            return await _context.MasterIndustries
                .Where(i => (bool)i.IsActive && i.MasterCompanyUuid==cmpuuid && i.MasterEnvironmentUuid==envuuid)
                .OrderByDescending(i => i.IndustryId)
                .ToListAsync();
        }

        // Get industry by UUID
        public async Task<MasterIndustry> GetIndustryByUuidAsync(Guid uuid)
        {
            // Convert Guid to string
            string uuidString = uuid.ToString();

            // Fetch the industry from the database
            return await _context.MasterIndustries
                .FirstOrDefaultAsync(i => i.Uuid == uuidString);
        }


        // Add a new industry sector
        public async Task AddIndustryAsync(MasterIndustry model)
        {
            bool isDuplicate = _context.MasterIndustries
                .Any(i => i.IndustrySector == model.IndustrySector);

            if (isDuplicate)
            {
                throw new Exception("Industry Sector Already Exists!");
            }

            model.IsActive = true;
            model.Uuid = Guid.NewGuid().ToString();
            model.IsAddedOn = DateTime.Now;
            model.IsAddedBy = "1";

            _context.MasterIndustries.Add(model);
            await _context.SaveChangesAsync();
        }

        // Update an existing industry sector
        public async Task UpdateIndustryAsync(MasterIndustry model)
        {
            var existingIndustry = await _context.MasterIndustries
                .FirstOrDefaultAsync(i => i.Uuid == model.Uuid);

            if (existingIndustry != null)
            {
                bool isDuplicate = _context.MasterIndustries.Any(i =>
                    i.IndustrySector == model.IndustrySector &&
                    i.Uuid != model.Uuid);

                if (isDuplicate)
                {
                    throw new Exception("Industry Sector Already Exists!");
                }

                existingIndustry.IndustrySector = model.IndustrySector;
                existingIndustry.IsDisplay = model.IsDisplay;
                existingIndustry.IsUpdatedOn = DateTime.Now;
                existingIndustry.IsUpdatedBy = "1";

                await _context.SaveChangesAsync();
            }
        }

        // Soft delete an industry sector
        public async Task DeleteIndustryAsync(string uuid)
        {
            var industry = await _context.MasterIndustries
                .FirstOrDefaultAsync(i => i.Uuid == uuid);

            if (industry != null)
            {
                industry.IsDeletedOn = DateTime.Now;
                industry.IsDeletedBy = "1";
                industry.IsActive = false;

                _context.MasterIndustries.Update(industry);
                await _context.SaveChangesAsync();
            }
        }



       
    }
}