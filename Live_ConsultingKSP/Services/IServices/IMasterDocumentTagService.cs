using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterDocumentTagService
    {
        void AddDocumentCategoryTag(MasterDocumentCategoryTag tag);
        List<MasterDocumentCategoryTag> GetAllDocumentCategoryTags(string cmpuuid, string envuuid);
        MasterDocumentCategoryTag GetDocumentCategoryTagByUUID(Guid uuid);
        void UpdateDocumentCategoryTag(MasterDocumentCategoryTag tag);
        void DeleteDocumentCategoryTag(Guid uuid);
    }
}
