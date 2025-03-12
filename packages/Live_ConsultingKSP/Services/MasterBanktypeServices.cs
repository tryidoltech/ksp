using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterBanktypeServices : IMasterBanktypeServices
    {
        private readonly KsperpDbContext _context;

        public MasterBanktypeServices(KsperpDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<MasterBanktype>> GetAllBanktypesAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterBanktypes.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.BankId).ToListAsync();
        }

        public Task<MasterBanktype?> GetBanktypeByUuidAsync(Guid uuid)
        {
            return _context.MasterBanktypes.FirstOrDefaultAsync(b => b.Uuid == uuid.ToString());
        }

        public async Task AddBankTypeAsync(MasterBanktype banktype)
        {
            bool isDuplicate = _context.MasterBanktypes.Any(x => x.BankName == banktype.BankName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            banktype.IsActive = true;
            banktype.Uuid = Guid.NewGuid().ToString();
            banktype.IsAddedOn = DateTime.Now;
            banktype.IsAddedBy = "1";
            _context.MasterBanktypes.Add(banktype);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateBankTypeAsync(MasterBanktype model)
        {
            var banktype = await _context.MasterBanktypes.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());
            if (banktype != null)
            {
                banktype.BankName = model.BankName;
                banktype.IsDisplay = model.IsDisplay;
                banktype.IsUpdateBy = "1";
                banktype.IsUpdatedOn = DateTime.Now;
                _context.MasterBanktypes.Update(banktype);
                await _context.SaveChangesAsync();
            }
        }
        public void Deletebanktype(Guid uuid)
        {
            var banktype = _context.MasterBanktypes.FirstOrDefault(b => b.Uuid == uuid.ToString());
            if (banktype != null)
            {
                banktype.IsActive = false;
                banktype.IsDeletedBy = "1";
                banktype.IsDeleteOn = DateTime.Now;
                _context.MasterBanktypes.Update(banktype);
                _context.SaveChanges();
            }
        }
    }
}
