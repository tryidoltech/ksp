using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterDocumentService
    {
        void AddDocumentCategory(MasterDocumentCategory category);
        List<MasterDocumentCategory> GetAllDocumentCategories(string cmpuuid, string envuuid);
        MasterDocumentCategory GetDocumentCategoryByUUID(Guid uuid);
        void UpdateDocumentCategory(MasterDocumentCategory category);
        void DeleteDocumentCategory(Guid uuid);
    }
}
