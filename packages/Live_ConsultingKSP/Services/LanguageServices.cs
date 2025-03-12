using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;

namespace Live_ConsultingKSP.Services
{
    public class LanguageServices : ILanguageservices
    {
        private readonly KsperpDbContext _context;

        public LanguageServices(KsperpDbContext context)
        {
            _context = context;
        }
        public void AddLanguage(AcLanguage acLanguage)
        {
            bool isDuplicate = _context.AcLanguages
          .Any(c => c.LanguageName == acLanguage.LanguageName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            acLanguage.IsDisplay = acLanguage.IsDisplay;
            acLanguage.Uuid = Guid.NewGuid().ToString();
            acLanguage.IsActive = true;
            acLanguage.IsAddedOn = DateTime.Now;
            acLanguage.IsAddedBy = "1";
            _context.AcLanguages.Add(acLanguage);
            _context.SaveChanges();
        }

        public void DeleteLanguage(Guid uuid)
        {
            var result = _context.AcLanguages.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                _context.AcLanguages.Update(result);
                _context.SaveChanges();
            }
        }

        public List<AcLanguage> GetAllLanguage(string cmpuuid, string envuuid)
        {
            return _context.AcLanguages.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.LanguageId).ToList();
        }

        public AcLanguage GetByLanguage(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public void UpdateLanguage(AcLanguage language)
        {
            bool isDuplicate = _context.AcLanguages
       .Any(c => c.LanguageName == language.LanguageName
                 && c.Uuid != language.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = _context.AcLanguages.FirstOrDefault(c => c.Uuid == language.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.LanguageName = language.LanguageName;
                existingEnvironment.IsDisplay = language.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                _context.SaveChanges();
            }
        }
    }
}
