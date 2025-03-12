using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterBankActypeServices : IMasterBankActypeServices
    {
        private readonly KsperpDbContext _context;

        public MasterBankActypeServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterBankActype>> GetAllBankActypesAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterBankActypes.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.BankId).ToListAsync();

        }

        public Task<MasterBankActype?> GetBankActypeByUuidAsync(Guid uuid)
        {
            return _context.MasterBankActypes.FirstOrDefaultAsync(b => b.Uuid == uuid.ToString());
        }

        public async Task AddBankAcTypeAsync(MasterBankActype bankActype)
        {
            bool isDuplicate = _context.MasterBankActypes.Any(x => x.BankAccontType == bankActype.BankAccontType && x.BankAccountStatus == bankActype.BankAccountStatus);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            bankActype.IsActive = true;
            bankActype.Uuid = Guid.NewGuid().ToString();
            bankActype.IsAddedOn = DateTime.Now;
            bankActype.IsAddedBy = "1";
            _context.MasterBankActypes.Add(bankActype);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateBankAcTypeAsync(MasterBankActype model)
        {
            var bankActype = await _context.MasterBankActypes.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());
            if (bankActype != null)
            {
                bankActype.BankAccontType = model.BankAccontType;
                bankActype.BankAccountStatus = model.BankAccountStatus;
                bankActype.IsDisplay = model.IsDisplay;
                bankActype.IsUpdateBy = "1";
                bankActype.IsUpdatedOn = DateTime.Now;
                _context.MasterBankActypes.Update(bankActype);
                await _context.SaveChangesAsync();
            }
        }
        public void Deletebankactype(Guid uuid)
        {
            var bankActype = _context.MasterBankActypes.FirstOrDefault(b => b.Uuid == uuid.ToString());
            if (bankActype != null)
            {
                bankActype.IsActive = false;
                bankActype.IsDeletedBy = "1";
                bankActype.IsDeleteOn = DateTime.Now;
                _context.MasterBankActypes.Update(bankActype);
                _context.SaveChanges();
            }
        }
    }
}
