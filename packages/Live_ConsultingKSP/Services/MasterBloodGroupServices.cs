using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterBloodGroupServices : IMasterBloodGroupServices
    {
        private readonly KsperpDbContext _context;

        public MasterBloodGroupServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterBloodGroup>> GetAllBloodGroupsAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterBloodGroups.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.BloodGroupId).ToListAsync();
        }
        public async Task<MasterBloodGroup?> GetBloodGroupByUuidAsync(Guid uuid)
        {
            return await _context.MasterBloodGroups.FirstOrDefaultAsync(y => y.Uuid == uuid.ToString());
        }
        public async Task AddBloodGroupAsync(MasterBloodGroup bloodGroup)
        {
            bool isDuplicate = _context.MasterBloodGroups.Any(x => x.BloodGroupName == bloodGroup.BloodGroupName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            bloodGroup.IsActive = true;
            bloodGroup.Uuid = Guid.NewGuid().ToString();
            bloodGroup.IsAddedOn = DateTime.Now;
            bloodGroup.IsAddedBy = "1";
            _context.MasterBloodGroups.Add(bloodGroup);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateBloodGroupAsync(MasterBloodGroup model)
        {
            var bloodGroup = await _context.MasterBloodGroups.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (bloodGroup != null)
            {
                bloodGroup.BloodGroupName = model.BloodGroupName;
                bloodGroup.IsDisplay = model.IsDisplay;
                bloodGroup.IsUpdateBy = "1";
                bloodGroup.IsUpdatedOn = DateTime.Now;
                _context.MasterBloodGroups.Update(bloodGroup);
                await _context.SaveChangesAsync();

            }
        }
        public void DeleteBloodGroup(Guid uuid)
        {
            var bloodGroup = _context.MasterBloodGroups.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (bloodGroup != null)
            {
                bloodGroup.IsActive = false;
                bloodGroup.IsDeletedBy = "1";
                bloodGroup.IsDeleteOn = DateTime.Now;
                _context.MasterBloodGroups.Update(bloodGroup);
                _context.SaveChanges();
            }
        }
    }
}
