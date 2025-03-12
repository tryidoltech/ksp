using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class MasterCityServices : IMasterCityServices
    {
        private readonly KsperpDbContext _context;
        public MasterCityServices(KsperpDbContext context)
        {
            _context = context;
        }
        public IEnumerable<MasterCity> GetAllCities(string cmpuuid, string envuuid)
        {
            return _context.MasterCities.Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid
            && c.MasterEnvironmentUuid == envuuid).ToList();
        }
        public void AddCity(MasterCity city)
        {

            bool isDuplicate = _context.MasterCities
                        .Any(c => c.CityName == city.CityName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            city.Uuid ??= Guid.NewGuid().ToString();
            city.IsActive = true;
            city.IsAddedOn = DateTime.Now;
            city.IsAddedBy = "1";
            _context.MasterCities.Add(city);
            _context.SaveChanges();
        }


        /*public List<MasterCity> GetAllCities()
        {
            // Return only active cities
            return _context.MasterCities
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.Id)
                .ToList();
        }*/


        public MasterCity GetCitiesByUUID(Guid uuid)
        {
            return _context.MasterCities.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateCity(MasterCity city)
        {
            bool isDuplicate = _context.MasterCities
        .Any(c => (c.CityName == city.CityName)
                  && c.Uuid != city.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingCity = _context.MasterCities.FirstOrDefault(c => c.Uuid == city.Uuid);
            if (existingCity != null)
            {
                existingCity.CityName = city.CityName;
                existingCity.IsDisplay = city.IsDisplay;
                existingCity.IsUpdatedOn = DateTime.Now;
                existingCity.IsUpdateBy = "1";

                _context.SaveChanges();
            }
        }

        public void DeleteCities(Guid uuid)
        {
            var city = _context.MasterCities.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (city != null)
            {
                city.IsDeleteOn = DateTime.Now;
                city.IsDeletedBy = "1";
                city.IsActive = false;
                _context.MasterCities.Update(city);
                _context.SaveChanges();
            }
        }
    }
}
