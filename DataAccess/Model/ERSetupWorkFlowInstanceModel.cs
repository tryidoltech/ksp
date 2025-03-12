using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class ERSetupWorkFlowInstanceModel
    {

        public ER_ExpenseType CB { get; set; }

        public ER_ManageReportDesignation LT { get; set; }

        public ERSetup_WorkFlowInstance SM { get; set; }

        public string UUID { get; set; }

        public string ExpenseType_Name { get; set; }

        public string Designation_Name { get; set; }



        public ERSetupWorkFlowInstanceModel()
        {

            CB = new ER_ExpenseType();
            LT = new ER_ManageReportDesignation();
            SM = new ERSetup_WorkFlowInstance();

        }

    }
}
