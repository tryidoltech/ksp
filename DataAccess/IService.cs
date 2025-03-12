using DataAccess.Entities;
using DataAccess.Logic;

//using DataAccess.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IService
    {

        //Master_AdminLoginLogic AdminLoginMaster { get; }
        ER_RemarkTagMasterLogic ERRemarkTagMaster { get; }
        AC_ITEmployeeParameterLogic ACITEmployeeParameterMaster { get; }
        AC_ITSubdeductionLogic ACITSubdeductionMaster { get; }
        AC_ITDeductionLogic ACITDeductionMaster { get; }
        AC_IncomeTaxMasterLogic ACIncomeTaxMaster { get; }
        AC_AllowanceTypeLogic ACAllowanceType { get; }
        AC_CustomerMasterLogic ACCustomerMaster { get; }
        AC_FinancialYearLogic ACFinancialYear { get; }
        AC_InvoiceSubTypeMasterLogic ACInvoiceSubTypeMaster { get; }
        AC_InvoiceTypeCodeMasterLogic ACInvoiceTypeCodeMaster { get; }
        AC_InvoiceTypeMasterLogic ACInvoiceTypeMaster { get; }
        AC_ItemGroupLogic ACItemGroup { get; }
        AC_ItemMasterLogic ACItemMaster { get; }
        AC_LanguageLogic ACLanguage { get; }
        AC_ModeOfPaymentLogic ACModeOfPayment { get; }
        AC_ModeOfTransportLogic ACModeOfTransport { get; }
        AC_NomenClatureLogic ACNomenClature { get; }
        AC_PaymentMeansCodeLogic ACPaymentMeansCode { get; }
        AC_PaymentStatusLogic ACPaymentStatus { get; }
        AC_TaxcodeLogic ACTaxcode { get; }
        AC_TaxDataLogic ACTaxData { get; }
        AC_TaxGroupLogic ACTaxGroup { get; }
        AC_TermsOfPaymentLogic ACTermsOfPayment { get; }
        AC_UnitLogic ACUnit { get; }
        AC_VendorMasterLogic ACVendorMaster { get; }
        ACSales_ManageInquiryLogic ACSalesManageInquiry { get; }
        BD_CommunicationHistoryLogic BDCommunicationHistory { get; }
        BD_ReasearchCommunicationStatusLogic BDReasearchCommunicationStatus { get; }
        BD_ResearchAudienceLogic BDResearchAudience { get; }
        BD_ResearchChannelTypeLogic BDResearchChannelType { get; }
        BD_ResearchCommunicationModeLogic BDResearchCommunicationMode { get; }
        CompanySetup_EmailCredentialLogic CompanySetupEmailCredential { get; }
        CompanySetup_EmailTemplateLogic CompanySetupEmailTemplate { get; }
        ER_ExpenseDataRangeLogic ERExpenseDataRange { get; }
        ER_ExpenseSubTypeLogic ERExpenseSubType { get; }
        ER_ExpenseSubUnitLogic ERExpenseSubUnit { get; }
        ER_ExpenseTypeLogic ERExpenseType { get; }
        ER_ExpenseUnitLogic ERExpenseUnit { get; }
        ER_HeadDesignationLogic ERHeadDesignation { get; }
        ER_ManageReportDesignationLogic ERManageReportDesignation { get; }
        ER_RemarkTemplateLogic ERRemarkTemplate { get; }
        ER_SubstituteExpenseLogic ERSubstituteExpense { get; }
        ERSetup_CostCategoryLogic ERSetupCostCategory { get; }
        ERSetup_CostCityLogic ERSetupCostCity { get; }
        ERSetup_CurrencyLogic ERSetupCurrency { get; }
        ERSetup_ExpenseEligibilityLogic ERSetupExpenseEligibility { get; }
        ERSetup_ExpenseLimitLogic ERSetupExpenseLimit { get; }
        ERSetup_WorkFlowInstanceLogic ERSetupWorkFlowInstance { get; }
        HRA_AttendenceReportLogic HRAAttendenceReport { get; }
        HRA_DocumentNamingMasterLogic HRADocumentNamingMaster { get; }
        HRA_DocumentTypeLogic HRADocumentType { get; }
        HRA_EmployeeTypeLogic HRAEmployeeType { get; }
        HRA_LeaveTypeMasterLogic HRALeaveTypeMaster { get; }
        HRA_ManageLeaveLogic HRAManageLeave { get; }
        HRA_NomineeLogic HRANominee { get; }
        HRA_PaySlipCategoryLogic HRAPaySlipCategory { get; }
        HRA_ProfessionalTaxLogic HRAProfessionalTax { get; }
        HRA_ShiftLogic HRAShift { get; }
        HRA_TaxRegimeLogic HRATaxRegime { get; }
        HRA_WeekDayLogic HRAWeekDay { get; }
        HRAEmployee_LeaveAuthorizationLogic HRAEmployeeLeaveAuthorization { get; }
        HRAEmployee_WeekOffLogic HRAEmployeeWeekOff { get; }
        HRAEmployeeSalary_SalaryParameterLogic HRAEmployeeSalarySalaryParameter { get; }
        HRAEmployeeSalary_SalaryPaySlipLogic HRAEmployeeSalarySalaryPaySlip { get; }
        HRAIncome_IncomeTaxLogic HRAIncomeIncomeTax { get; }
        HRAIncome_ITDeductionLogic HRAIncomeITDeduction { get; }
        HRAIncome_ITEmployeeParameterLogic HRAIncomeITEmployeeParameter { get; }
        Income_ITSubDeductionLogic IncomeITSubDeduction { get; }
        Master_AssetLogic MasterAsset { get; }
        Master_BankACtypeLogic MasterBankACtype { get; }
        Master_BanktypeLogic MasterBanktype { get; }
        Master_BloodGroupLogic MasterBloodGroup { get; }
        Master_CategorTagLogic MasterCategorTag { get; }
        Master_CategoryLogic MasterCategory { get; }
        Master_CityLogic MasterCity { get; }
        Master_CompanyBranchLogic MasterCompanyBranch { get; }
        Master_CompanyTypeLogic MasterCompanyType { get; }
        Master_DepartmentLogic MasterDepartment { get; }
        Master_DesignationLogic MasterDesignation { get; }
        Master_DocumentCategoryLogic MasterDocumentCategory { get; }
        Master_DocumentCategoryTagLogic MasterDocumentCategoryTag { get; }
        Master_DocumentGropLogic MasterDocumentGrop { get; }
        Master_EmployeeLogic MasterEmployee { get; }
        Master_Employee_DocumentSetupLogic MasterEmployeeDocumentSetup { get; }
        Master_Employee_EducationDetailsLogic MasterEmployeeEducationDetails { get; }
        Master_Employee_ExperienceDetailLogic MasterEmployeeExperienceDetail { get; }

        Master_Employee_AssestsDetailsLogic MasterEmployeeAssestsDetails { get; }
        Master_Employee_leaveAuthorisationLogic MasterEmployeeleaveAuthorisation { get; }
        Master_GenderLogic MasterGender { get; }
        Master_HonorificLogic MasterHonorific { get; }
        Master_IndustryLogic MasterIndustry { get; }
        Master_ManageBankDetailLogic MasterManageBankDetail { get; }
        Master_ManageDocumentLogic MasterManageDocument { get; }
        Master_MaritalLogic MasterMarital { get; }
        Master_NationalityLogic MasterNationality { get; }
        Master_ServiceLogic MasterService { get; }
        Master_StateLogic MasterState { get; }
        Master_YearLogic MasterYear { get; }
        Master_CompanyLogic MasterCompany { get; }
        Master_CountryLogic MasterCountry { get; }
        Master_EnvironmentLogic MasterEnvironment { get; }
        Master_MenuLogic MasterMenu { get; }
        Master_UserMenuRightLogic MasterUserMenuRight { get; }
        Master_UserRoleLogic MasterUserRole { get; }
        Project_CreateProjectLogic ProjectCreateProject { get; }
        Project_CreateProjectPhaseLogic ProjectCreateProjectPhase { get; }
        Project_CreateProjectStepLogic ProjectCreateProjectStep { get; }
        Project_ManageResourceCostingLogic ProjectManageResourceCosting { get; }
        Project_ManageTaskTimeLineLogic ProjectManageTaskTimeLine { get; }
        Project_ProjectCrendentialsLogic ProjectProjectCrendentials { get; }
        Project_ProjectDocumentLogic ProjectProjectDocument { get; }
        Project_ProjectMeetingLogic ProjectProjectMeeting { get; }
        Project_ProjectMOMLogic ProjectProjectMOM { get; }

        Project_ProjectResourcesLogic ProjectProjectResources { get; }
        Project_ProjectStatusLogic ProjectProjectStatus { get; }
        Project_ProjectTaskLogic ProjectProjectTask { get; }
        Research_ManageFilterDataLogic ResearchManageFilterData { get; }
        Research_ManageResearchGroupLogic ResearchManageResearchGroup { get; }
        Research_UploadResearchDataLogic ResearchUploadResearchData { get; }
        Sales_ManagePositiveLeadLogic SalesManagePositiveLead { get; }
        Timesheet_LineLogic TimesheetLine { get; }
        Timesheet_HeaderLogic TimesheetHeader { get; }




    }
}
