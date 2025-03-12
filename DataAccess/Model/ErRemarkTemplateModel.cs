using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Model;

namespace DataAccess.Model
{
    public class ErRemarkTemplateModel
    {
        public ER_ExpenseType CP { get; set; }
        public ER_ExpenseSubType PP { get; set; }
        public ER_RemarkTagMaster PT { get; set; }
        public ER_RemarkTemplate MT { get; set; }

        public string ExpenseType_Name { get; set; }
        public string ExpenseSubType_Name { get; set; }
        public string RemarkTag_Name { get; set; }
        public string UUID { get; set; }
        public string Remark_String { get; set; }

        public ErRemarkTemplateModel()
        {
            CP = new ER_ExpenseType();
            PP = new ER_ExpenseSubType();
            PT = new ER_RemarkTagMaster();
            MT = new ER_RemarkTemplate();
        }
    }
}





