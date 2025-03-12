using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterHonorificServices : IMasterHonorificServices
    {
        private readonly KsperpDbContext _context;

        public MasterHonorificServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterHonorific>> GetAllHonorificsAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterHonorifics.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<MasterHonorific?> GetHonorificByUuidAsync(Guid uuid)
        {
            return await _context.MasterHonorifics.FirstOrDefaultAsync(y => y.Uuid == uuid.ToString());
        }

        public async Task AddNewHonorificAsync(MasterHonorific honorific)
        {
            bool isDuplicate = _context.MasterHonorifics.Any(x => x.HonorificName == honorific.HonorificName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            honorific.IsActive = true;
            honorific.Uuid = Guid.NewGuid().ToString();
            honorific.IsAddedOn = DateTime.Now;
            honorific.IsAddedBy = "1";
            _context.MasterHonorifics.Add(honorific);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateHonorificAsync(MasterHonorific model)
        {
            var honorific = await _context.MasterHonorifics.FirstOrDefaultAsync(h => h.Uuid == model.Uuid.ToString());
            if (honorific != null)
            {
                honorific.HonorificName = model.HonorificName;
                honorific.IsDisplay = model.IsDisplay;
                honorific.IsUpdateBy = "1";
                honorific.IsUpdatedOn = DateTime.Now;
                _context.MasterHonorifics.Update(honorific);
                await _context.SaveChangesAsync();
            }
        }
        public void Deletehonorific(Guid uuid)
        {
            var honorific = _context.MasterHonorifics.FirstOrDefault(h => h.Uuid == uuid.ToString());
            if (honorific != null)
            {
                honorific.IsActive = false;
                honorific.IsDeletedBy = "1";
                honorific.IsDeleteOn = DateTime.Now;
                _context.MasterHonorifics.Update(honorific);
                _context.SaveChanges();
            }
        }
    }
}
