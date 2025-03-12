using  Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class MasterDocumentService : IMasterDocumentService
    {
        private readonly KsperpDbContext _context;

        public MasterDocumentService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddDocumentCategory(MasterDocumentCategory category)
        {
            if (_context.MasterDocumentCategories.Any(c => c.DocumentCategoryName == category.DocumentCategoryName))
                throw new Exception("Document category already exists!");

            category.Uuid = Guid.NewGuid().ToString();
            category.IsActive = true;
            category.IsAddedOn = DateTime.Now;
            category.IsAddedBy = "1";

            _context.MasterDocumentCategories.Add(category);
            _context.SaveChanges();
        }

        public List<MasterDocumentCategory> GetAllDocumentCategories(string cmpuuid, string envuuid)
        {
            return _context.MasterDocumentCategories
                .Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid) // Only active categories
                .OrderByDescending(c => c.DocumentId)
                .ToList();
        }

        public MasterDocumentCategory GetDocumentCategoryByUUID(Guid uuid)
        {
            return _context.MasterDocumentCategories.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }
        public void UpdateDocumentCategory(MasterDocumentCategory category)
        {
            var existingCategory = _context.MasterDocumentCategories.FirstOrDefault(c => c.Uuid == category.Uuid);
            if (existingCategory == null)
                throw new Exception("Document category not found!");

            if (_context.MasterDocumentCategories.Any(c => c.DocumentCategoryName == category.DocumentCategoryName && c.Uuid != category.Uuid))
                throw new Exception("Document category name already exists!");

            existingCategory.DocumentCategoryName = category.DocumentCategoryName;
            existingCategory.IsDisplay = category.IsDisplay;
            existingCategory.IsUpdatedOn = DateTime.Now; // Ensure this is set to the current date/time.
            existingCategory.IsUpdatedBy = "1"; // Example: You should replace '1' with the actual user ID.

            _context.SaveChanges();
        }



        public void DeleteDocumentCategory(Guid uuid)
        {
            var category = _context.MasterDocumentCategories.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (category == null) throw new Exception("Document category not found!");

            category.IsActive = false;
            category.IsDeletedOn = DateTime.Now;
            category.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
