using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Services
{
    public interface IGroupService
    {
        MasterService GetGroupByUuid(Guid uuid);
        void AddGroup(MasterService group);
        List<MasterService> GetAllGroups(string cmpuuid, string envuuid);
        void UpdateGroup(MasterService group);
        void DeleteGroup(Guid uuid);
    }
}
