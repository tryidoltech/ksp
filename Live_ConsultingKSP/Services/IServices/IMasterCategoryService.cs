using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterCategoryService
    {
        void AddCategory(MasterCategory category);
        List<MasterCategory> GetAllCategories(string cmpuuid, string envuuid);
        MasterCategory GetCategoryByUUID(Guid uuid);
        void UpdateCategory(MasterCategory category);
        void DeleteCategory(Guid uuid);
       
    }
}
