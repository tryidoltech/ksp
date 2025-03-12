using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterNationalityServices : IMasterNationalityServices
    {
        private readonly KsperpDbContext _context;

        public MasterNationalityServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterNationality>> GetAllNationalitysAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterNationalities.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.NationalityId).ToListAsync();
        }

        public async Task<MasterNationality?> GetNationalityByUuidAsync(Guid uuid)
        {
            return await _context.MasterNationalities.FirstOrDefaultAsync(n => n.Uuid == uuid.ToString());
        }

        public async Task AddNationalityAsync(MasterNationality nationality)
        {
            bool isDuplicate = _context.MasterNationalities.Any(x => x.NationalityName == nationality.NationalityName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            nationality.IsActive = true;
            nationality.Uuid = Guid.NewGuid().ToString();
            nationality.IsAddedOn = DateTime.Now;
            nationality.IsAddedBy = "1";  // Example for user ID, can be dynamic
            _context.MasterNationalities.Add(nationality);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNationalityAsync(MasterNationality model)
        {
            var nationality = await _context.MasterNationalities.FirstOrDefaultAsync(n => n.Uuid == model.ToString());
            if (nationality != null)
            {
                nationality.NationalityName = model.NationalityName;
                nationality.IsDisplay = model.IsDisplay;
                nationality.IsUpdateBy = "1"; // Example for user ID, can be dynamic
                nationality.IsUpdatedOn = DateTime.Now;
                _context.MasterNationalities.Update(nationality);
                await _context.SaveChangesAsync();
            }
        }

        public void DeleteNationality(Guid uuid)
        {
            var nationality = _context.MasterNationalities.FirstOrDefault(n => n.Uuid == uuid.ToString());
            if (nationality != null)
            {
                nationality.IsActive = false;
                nationality.IsDeletedBy = "1"; // Example for user ID, can be dynamic
                nationality.IsDeleteOn = DateTime.Now;
                _context.MasterNationalities.Update(nationality);
                _context.SaveChanges();
            }
        }
    }
}
