using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class MenuRecordsModel
    {
        public string Url               { get; set; }
        public string MenuName          { get; set; }
        public string MenuIcon          { get; set; }
        public decimal? MenuLevel         { get; set; }
        public string MainParentUuid    { get; set; }
        public string SubParentUuid     { get; set; }
        public decimal? Sequence          { get; set; }
        public bool? IsParent          { get; set; }
        public string Uuid              { get; set; }
        public string UserRoleUuid      { get; set; }
        public bool? IsRead            { get; set; }
        public bool? IsWrite           { get; set; }
        public bool? IsEdit            { get; set; }
        public bool? IsDelete { get; set; }
       
      
    }
}
