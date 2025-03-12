using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class MasterCompanyTypeServices : IMasterCompanyTypeServices
    {
        private readonly KsperpDbContext _context;
        public MasterCompanyTypeServices(KsperpDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MasterCompanyType>> GetAllCompanyTypesAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterCompanyTypes.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid
            && x.MasterEnvironmentUuid == envuuid).OrderByDescending(x => x.CompanyTypeId).ToListAsync();
        }

        public async Task<MasterCompanyType?> GetCompanyTypeByUuidAsync(Guid uuid)
        {
            return await _context.MasterCompanyTypes.FirstOrDefaultAsync(c => c.Uuid == uuid.ToString());
        }

        public async Task AddCompanyTypeAsync(MasterCompanyType companyType)
        {
            bool isDuplicate = _context.MasterCompanyTypes.Any(x => x.CompanyType == companyType.CompanyType);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            companyType.IsActive = true;
            companyType.Uuid = Guid.NewGuid().ToString();
            companyType.IsAddedOn = DateTime.Now;
            companyType.IsAddedBy = "1";
            _context.MasterCompanyTypes.Add(companyType);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateCompanyTypeAsync(MasterCompanyType model)
        {
            var companyType = await _context.MasterCompanyTypes.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (companyType != null)
            {
                companyType.CompanyType = model.CompanyType;
                companyType.IsDisplay = model.IsDisplay;
                companyType.IsUpdateBy = "1";
                companyType.IsUpdatedOn = DateTime.Now;
                _context.MasterCompanyTypes.Update(companyType);
                await _context.SaveChangesAsync();

            }
        }
        public void DeleteCompanyType(Guid uuid)
{
            var companyType = _context.MasterCompanyTypes.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (companyType != null)
            {
                companyType.IsActive = false;
                companyType.IsDeletedBy = "1";
                companyType.IsDeleteOn = DateTime.Now;
                _context.MasterCompanyTypes.Update(companyType);
                _context.SaveChanges();
            }
        }
    }
}
