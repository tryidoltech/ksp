using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;

namespace Live_ConsultingKSP.Services
{
    public class CurrencyServices : ICurrencyServices
    {
        private readonly KsperpDbContext _context;

        public CurrencyServices(KsperpDbContext context)
        {
            _context = context;
        }
        public void AddCurrency(ErsetupCurrency currency)
        {
            bool isDuplicate = _context.ErsetupCurrencies
                     .Any(c => c.CurrencyName == currency.CurrencyName && c.CurrencyShortName == currency.CurrencyShortName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            currency.IsAddedBy = "1";
            currency.IsDisplay = currency.IsDisplay;
            currency.CurrencySymbol = currency.CurrencySymbol;
            currency.IsDefault = currency.IsDefault;
            currency.Uuid = Guid.NewGuid().ToString();
            currency.IsActive = true;
            currency.IsAddedOn = DateTime.Now;
            _context.ErsetupCurrencies.Add(currency);
            _context.SaveChanges();
        }

        public void DeleteCurrency(Guid uuid)
        {
            var result = _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeletedOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                _context.ErsetupCurrencies.Update(result);
                _context.SaveChanges();
            }
        }

        public List<ErsetupCurrency> GetAllCurrency(string cmpuuid, string envuuid)
        {
            return _context.ErsetupCurrencies.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterCompanyUuid == envuuid)
                .OrderByDescending(c => c.CurrencyId).ToList();
        }

        public ErsetupCurrency GetByCurrency(Guid uuid)
        {
            return _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateCurrency(ErsetupCurrency currency)
        {
            bool isDuplicate = _context.ErsetupCurrencies
        .Any(c => c.CurrencyName == currency.CurrencyName && c.CurrencyShortName == currency.CurrencyShortName
                  && c.Uuid != currency.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = _context.ErsetupCurrencies.FirstOrDefault(c => c.Uuid == currency.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.CurrencyName = currency.CurrencyName;
                existingEnvironment.CurrencyShortName = currency.CurrencyShortName;
                existingEnvironment.IsDisplay = currency.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdatedBy = "1";

                _context.SaveChanges();
            }
        }
    }
}
