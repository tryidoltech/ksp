using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class MasterYearServices : IMasterYearServices
    {
        private readonly Utils _utils;
        private readonly KsperpDbContext _context;
        public MasterYearServices(KsperpDbContext context, Utils utils) 
        {
           
            _context = context;
            _utils = utils;
        }
        public async Task<IEnumerable<MasterYear>> GetAllYearsAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterYears.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid
            && x.MasterEnvironmentUuid == envuuid).OrderByDescending(x => x.YearId).ToListAsync();
        }

        public async Task<MasterYear?> GetYearByUuidAsync(Guid uuid)
        {
            return await _context.MasterYears.FirstOrDefaultAsync(c => c.Uuid == uuid.ToString());
        }

        public async Task AddYearAsync(MasterYear year)
        {
            bool isDuplicate = _context.MasterYears.Any(x => x.YearName == year.YearName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            year.IsActive = true;
            year.Uuid = _utils.GetUUID();
            year.IsAddedOn = _utils.CurrentIndianTime();
            year.IsAddedBy = "1";
            year.AddedIp = _utils.GetLocalIPAddress();
            _context.MasterYears.Add(year);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateYearAsync(MasterYear model)
        {
            var year = await _context.MasterYears.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (year != null)
            {
                year.YearName = model.YearName;
                year.IsDisplay = model.IsDisplay;
                year.IsUpdateBy = "1";
                year.IsUpdatedOn = _utils.CurrentIndianTime();
                year.UpdatedIp = _utils.GetLocalIPAddress();

                _context.MasterYears.Update(year);
                await _context.SaveChangesAsync();

            }
        }
        public void DeletedYear(string uuid)
        {
            var year = _context.MasterYears.FirstOrDefault(c => c.Uuid == uuid);
            if (year != null)
            {
                year.IsActive = false;
                year.IsDeletedBy = "1";
                year.IsDeleteOn = _utils.CurrentIndianTime();
                year.DeletedIp = _utils.GetLocalIPAddress();

                _context.MasterYears.Update(year);
                _context.SaveChanges();
            }
        }
    }
}
