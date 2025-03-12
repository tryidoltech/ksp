using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IHraPaySlipCategoryService
    {
        void AddPaySlipCategory(HraPaySlipCategory paySlipCategory);
        List<HraPaySlipCategory> GetAllPaySlipCategories();
        IQueryable<HraPaySlipCategory> GetAllPaySlipCategoriesQueryable();
        HraPaySlipCategory GetPaySlipCategoryByUUID(string uuid);
        void UpdatePaySlipCategory(HraPaySlipCategory paySlipCategory);
        void DeletePaySlipCategory(string uuid);
    }
}
