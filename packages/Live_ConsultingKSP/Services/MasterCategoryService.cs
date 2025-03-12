using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class MasterCategoryService : IMasterCategoryService
    {
        private readonly KsperpDbContext _context;

        public MasterCategoryService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddCategory(MasterCategory category)
        {
            if (_context.MasterCategories.Any(c => c.CategoryName == category.CategoryName))
                throw new Exception("Category already exists!");

            category.Uuid = Guid.NewGuid().ToString();
            category.IsActive = true;
            category.IsAddedOn = DateTime.Now;
            category.IsAddedBy = "1";

            _context.MasterCategories.Add(category);
            _context.SaveChanges();
        }

        public List<MasterCategory> GetAllCategories(string cmpuuid, string envuuid)
        {
            return _context.MasterCategories
                .Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid) // Only active categories
                .OrderByDescending(c => c.CategoryId)
                .ToList();
        }


        public MasterCategory GetCategoryByUUID(Guid uuid)
        {
            return _context.MasterCategories.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateCategory(MasterCategory category)
        {
            var existingCategory = _context.MasterCategories.FirstOrDefault(c => c.Uuid == category.Uuid);
            if (existingCategory == null) throw new Exception("Category not found!");

            if (_context.MasterCategories.Any(c => c.CategoryName == category.CategoryName && c.Uuid != category.Uuid))
                throw new Exception("Category name already exists!");

            // Update the specific fields
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.IsDisplay = category.IsDisplay;
            existingCategory.IsUpdatedOn = DateTime.Now;
            existingCategory.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteCategory(Guid uuid)
        {
            var category = _context.MasterCategories.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (category == null) throw new Exception("Category not found!");

            category.IsActive = false; 
            category.IsDeletedOn = DateTime.Now;
            category.IsDeletedBy = "1"; 

            _context.SaveChanges();
        }


    }
}


