using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface ILanguageservices
    {
        void AddLanguage(AcLanguage acLanguage);
        List<AcLanguage> GetAllLanguage(string cmpuuid, string envuuid);
        AcLanguage GetByLanguage(Guid uuid);
        void UpdateLanguage(AcLanguage language);
        void DeleteLanguage(Guid uuid);
    }
}
