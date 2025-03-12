using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterMaritalServices : IMasterMaritalServices
    {
        private readonly KsperpDbContext _context;

        public MasterMaritalServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterMarital>> GetAllMaritalAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterMaritals
                .Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.MaritalId)
                .ToListAsync();
        }

        public async Task<MasterMarital?> GetMaritalByUuidAsync(Guid uuid)
        {
            return await _context.MasterMaritals.FirstOrDefaultAsync(y => y.Uuid == uuid.ToString());
        }

        public async Task AddMaritalAsync(MasterMarital marital)
        {
            bool isDuplicate = _context.MasterMaritals.Any(x => x.MaritalStatus == marital.MaritalStatus);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            marital.IsActive = true;
            marital.Uuid = Guid.NewGuid().ToString();
            marital.IsAddedOn = DateTime.Now;
            marital.IsAddedBy = "1"; // Update with the current user ID if required
            _context.MasterMaritals.Add(marital);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMaritalAsync(MasterMarital model)
        {
            var marital = await _context.MasterMaritals.FirstOrDefaultAsync(m => m.Uuid == model.Uuid.ToString());

            if (marital != null)
            {
                marital.MaritalStatus = model.MaritalStatus;
                marital.IsDisplay = model.IsDisplay;
                marital.IsUpdateBy = "1"; // Update with the current user ID if required
                marital.IsUpdatedOn = DateTime.Now;

                _context.MasterMaritals.Update(marital);
                await _context.SaveChangesAsync();
            }
        }

        public void DeleteMarital(Guid uuid)
        {
            var marital = _context.MasterMaritals.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (marital != null)
            {
                marital.IsActive = false;
                marital.IsDeletedBy = "1"; // Update with the current user ID if required
                marital.IsDeleteOn = DateTime.Now;

                _context.MasterMaritals.Update(marital);
                _context.SaveChanges();
            }
        }
    }
}
