using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class MasterCountryServices : IMasterCountryServices
    {
        private readonly KsperpDbContext _context;

        public MasterCountryServices(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddCountry(MasterCountry country)
        {

            bool isDuplicate = _context.MasterCountries
                        .Any(c => c.CountryName == country.CountryName || c.CountryShortName == country.CountryShortName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            country.IsActive = true;
            country.IsAddedOn = DateTime.Now;
            country.IsAddedBy = "1";
            _context.MasterCountries.Add(country);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<MasterCountry>> GetAllCountries(string cmpuuid, string envuuid)
        {
            return _context.MasterCountries.Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid
            && c.MasterEnvironmentUuid == envuuid).OrderByDescending(c => c.CountryId).ToList();
        }


        public MasterCountry GetCountryByUUID(Guid uuid)
        {
            return _context.MasterCountries.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateCountry(MasterCountry country)
        {
            bool isDuplicate = _context.MasterCountries
        .Any(c => (c.CountryName == country.CountryName || c.CountryShortName == country.CountryShortName)
                  && c.Uuid != country.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingCountry = _context.MasterCountries.FirstOrDefault(c => c.Uuid == country.Uuid);
            if (existingCountry != null)
            {
                existingCountry.CountryName = country.CountryName;
                existingCountry.CountryShortName = country.CountryShortName;
                existingCountry.IsDisplay = country.IsDisplay;
                existingCountry.IsUpdatedOn = DateTime.Now;
                existingCountry.IsUpdateBy = "1";

                _context.SaveChanges();
            }
        }

        public void DeleteCountry(Guid uuid)
        {
            var country = _context.MasterCountries.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (country != null)
            {
                country.IsDeleteOn = DateTime.Now;
                country.IsDeletedBy = "1";
                country.IsActive = false;
                _context.MasterCountries.Update(country);
                _context.SaveChanges();
            }
        }

    }
}
