using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterGenderServices : IMasterGenderServicescs
    {
        private readonly KsperpDbContext _context;

        public MasterGenderServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterGender>> GetAllGendersAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterGenders.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<MasterGender?> GetGenderByUuidAsync(Guid uuid)
        {
            return await _context.MasterGenders.FirstOrDefaultAsync(g => g.Uuid == uuid.ToString());
        }

        public async Task AddGenderAsync(MasterGender gender)
        {
            bool isDuplicate = _context.MasterGenders.Any(x => x.GenderName == gender.GenderName && x.GenderSymbol == gender.GenderSymbol);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            gender.IsActive = true;
            gender.Uuid = Guid.NewGuid().ToString();
            gender.IsAddedOn = DateTime.Now;
            gender.IsAddedBy = "1";
            _context.MasterGenders.Add(gender);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateGenderAsync(MasterGender model)
        {
            var gender = await _context.MasterGenders.FirstOrDefaultAsync(g => g.Uuid == model.Uuid);
            if (gender != null)
            {
                gender.GenderName = model.GenderName;
                gender.GenderSymbol = model.GenderSymbol;
                gender.IsDisplay = model.IsDisplay;
                gender.IsUpdateBy = "1";
                gender.IsUpdatedOn = DateTime.Now;
                _context.MasterGenders.Update(gender);
                await _context.SaveChangesAsync();
            }
            _context.MasterGenders.Update(gender);
            await _context.SaveChangesAsync();
        }
        public void Deletegender(Guid uuid)
        {
            var gender = _context.MasterGenders.FirstOrDefault(g => g.Uuid == uuid.ToString());
            if (gender != null)
            {
                gender.IsActive = false;
                gender.IsDeletedBy = "1";
                gender.IsDeleteOn = DateTime.Now;
                _context.MasterGenders.Update(gender);
                _context.SaveChanges();
            }
        }
    }
}
