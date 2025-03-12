using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Services
{
    public interface IBdResearchAudienceService
    {
        void AddResearchAudience(BdResearchAudience audience);
        List<BdResearchAudience> GetAllResearchAudiences(string cmpuuid, string envuuid);
        BdResearchAudience GetResearchAudienceByUUID(string uuid);
        void UpdateResearchAudience(BdResearchAudience audience);
        void DeleteResearchAudience(string uuid);
    }
}
