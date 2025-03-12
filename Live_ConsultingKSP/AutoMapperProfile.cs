﻿using AutoMapper;
using System;
using DataAccess.Entities;
using Live_ConsultingKSP.Models;
namespace Live_ConsultingKSP
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Master_Employee, MasterEmployee>().ReverseMap();
            CreateMap<ER_RemarkTagMaster, ErRemarkTagMaster>().ReverseMap();
            CreateMap<Master_Employee, MasterEmployeeFamily>().ReverseMap();
            CreateMap<Master_Employee, MasterEmployeeHR>().ReverseMap();
            CreateMap<Master_Year, MasterYear>().ReverseMap();
            CreateMap<AC_AllowanceType, AcAllowanceType>().ReverseMap();
            CreateMap<AC_CustomerMaster, AcCustomerMaster>().ReverseMap();
            CreateMap<AC_FinancialYear, AcFinancialYear>().ReverseMap();
            CreateMap<AC_InvoiceSubTypeMaster, AcInvoiceSubTypeMaster>().ReverseMap();
            CreateMap<AC_InvoiceTypeCodeMaster, AcInvoiceTypeCodeMaster>().ReverseMap();
            CreateMap<AC_InvoiceTypeMaster, AcInvoiceTypeMaster>().ReverseMap();
            CreateMap<AC_ItemGroup, AcItemGroup>().ReverseMap();
            CreateMap<AC_ItemMaster, AcItemMaster>().ReverseMap();
            CreateMap<AC_Language, AcLanguage>().ReverseMap();
            CreateMap<AC_ModeOfPayment, AcModeOfPayment>().ReverseMap(); 
            CreateMap<AC_ModeOfTransport, AcModeOfTransport>().ReverseMap();
            CreateMap<AC_NomenClature, AcNomenClature>().ReverseMap();
            CreateMap<AC_PaymentMeansCode, AcPaymentMeansCode>().ReverseMap();
            CreateMap<AC_PaymentStatus, AcPaymentStatus>().ReverseMap();
            CreateMap<AC_Taxcode, AcTaxcode>().ReverseMap();
            CreateMap<AC_TaxData, AcTaxDatum>().ReverseMap();
            CreateMap<AC_TaxGroup, AcTaxGroup>().ReverseMap();
            CreateMap<AC_TermsOfPayment, AcTermsOfPayment>().ReverseMap();
            CreateMap<AC_Unit, AcUnit>().ReverseMap(); 
            CreateMap<AC_VendorMaster, AcVendorMaster>().ReverseMap();
            CreateMap<ACSales_ManageInquiry, AcsalesManageInquiry>().ReverseMap();
            CreateMap<BD_CommunicationHistory, BdCommunicationHistory>().ReverseMap();
            CreateMap<BD_ReasearchCommunicationStatus, BdReasearchCommunicationStatus>().ReverseMap();
            CreateMap<BD_ResearchAudience, BdResearchAudience>().ReverseMap();
            CreateMap<BD_ResearchChannelType, BdResearchChannelType>().ReverseMap();
            CreateMap<BD_ResearchCommunicationMode, BdResearchCommunicationMode>().ReverseMap();
            CreateMap<CompanySetup_EmailCredential, CompanySetupEmailCredential>().ReverseMap();
            CreateMap<CompanySetup_EmailTemplate, CompanySetupEmailTemplate>().ReverseMap();
            CreateMap<ER_ExpenseDataRange, ErExpenseDataRange>().ReverseMap();
            CreateMap<ER_ExpenseSubType, ErExpenseSubType>().ReverseMap();
            CreateMap<ER_ExpenseSubUnit, ErExpenseSubUnit>().ReverseMap();
            CreateMap<ER_ExpenseType, ErExpenseType>().ReverseMap();
            CreateMap<ER_ExpenseUnit, ErExpenseUnit>().ReverseMap();
            CreateMap<ER_HeadDesignation, ErHeadDesignation>().ReverseMap();
            CreateMap<ER_ManageReportDesignation, ErManageReportDesignation>().ReverseMap();
            CreateMap<ER_RemarkTemplate, ErRemarkTemplate>().ReverseMap();
            CreateMap<ER_SubstituteExpense, ErSubstituteExpense>().ReverseMap();
            CreateMap<ERSetup_CostCategory, ErsetupCostCategory>().ReverseMap();
            CreateMap<ERSetup_CostCity, ErsetupCostCity>().ReverseMap();
            CreateMap<ERSetup_Currency, ErsetupCurrency>().ReverseMap();
            CreateMap<ERSetup_ExpenseEligibility, ErsetupExpenseEligibility>().ReverseMap();
            CreateMap<ERSetup_ExpenseLimit, ErsetupExpenseLimit>().ReverseMap();
            CreateMap<ERSetup_WorkFlowInstance, ErsetupWorkFlowInstance>().ReverseMap();
            CreateMap<HRA_AttendenceReport, HraAttendenceReport>().ReverseMap();
            CreateMap<HRA_DocumentNamingMaster, HraDocumentNamingMaster>().ReverseMap();
            CreateMap<HRA_DocumentType, HraDocumentType>().ReverseMap();
            CreateMap<HRA_EmployeeType, HraEmployeeType>().ReverseMap();
            CreateMap<HRA_LeaveTypeMaster, HraLeaveTypeMaster>().ReverseMap();
            CreateMap<HRA_ManageLeave, HraManageLeave>().ReverseMap();
            CreateMap<HRA_Nominee, HraNominee>().ReverseMap();
            CreateMap<HRA_PaySlipCategory, HraPaySlipCategory>().ReverseMap();
            CreateMap<HRA_ProfessionalTax, HraProfessionalTax>().ReverseMap();
            CreateMap<HRA_Shift, HraShift>().ReverseMap();
            CreateMap<HRA_TaxRegime, HraTaxRegime>().ReverseMap();
            CreateMap<HRA_WeekDay, HraWeekDay>().ReverseMap();
            CreateMap<HRAEmployee_LeaveAuthorization, HraemployeeLeaveAuthorization>().ReverseMap();
            CreateMap<HRAEmployee_WeekOff, HraemployeeWeekOff>().ReverseMap();
            CreateMap<HRAEmployeeSalary_SalaryParameter, HraemployeeSalarySalaryParameter>().ReverseMap();
            CreateMap<HRAEmployeeSalary_SalaryPaySlip, HraemployeeSalarySalaryPaySlip>().ReverseMap();
            CreateMap<HRAIncome_IncomeTax, HraincomeIncomeTax>().ReverseMap();
            CreateMap<HRAIncome_ITDeduction, HraincomeItdeduction>().ReverseMap();
            CreateMap<HRAIncome_ITEmployeeParameter, HraincomeItemployeeParameter>().ReverseMap();
            CreateMap<Income_ITSubDeduction, IncomeItsubDeduction>().ReverseMap();
            CreateMap<Master_Asset, MasterAsset>().ReverseMap();
            CreateMap<Master_BankACtype, MasterBankActype>().ReverseMap();
            CreateMap<Master_Banktype, MasterBanktype>().ReverseMap();
            CreateMap<Master_BloodGroup, MasterBloodGroup>().ReverseMap();
            CreateMap<Master_CategorTag, MasterCategorTag>().ReverseMap();
            CreateMap<Master_Category, MasterCategory>().ReverseMap();
            CreateMap<Master_City, MasterCity>().ReverseMap();
            CreateMap<Master_CompanyBranch, MasterCompanyBranch>().ReverseMap();
            CreateMap<Master_CompanyType, MasterCompanyType>().ReverseMap();
            CreateMap<Master_Department, MasterDepartment>().ReverseMap();
            CreateMap<Master_Designation, MasterDesignation>().ReverseMap();
            CreateMap<Master_DocumentCategory, MasterDocumentCategory>().ReverseMap();
            CreateMap<Master_DocumentCategoryTag, MasterDocumentCategoryTag>().ReverseMap();
            CreateMap<Master_DocumentGrop, MasterDocumentGrop>().ReverseMap();
            CreateMap<Master_Employee_DocumentSetup, MasterEmployeeDocumentSetup>().ReverseMap();            
            CreateMap<Master_Employee_leaveAuthorisation, MasterEmployeeLeaveAuthorisation>().ReverseMap();
            CreateMap<Master_Gender, MasterGender>().ReverseMap();
            CreateMap<Master_Honorific, MasterHonorific>().ReverseMap();
            CreateMap<Master_Industry, MasterIndustry>().ReverseMap();
            CreateMap<Master_ManageBankDetail, MasterManageBankDetail>().ReverseMap();
            CreateMap<Master_ManageDocument, MasterManageDocument>().ReverseMap();
            CreateMap<Master_Marital, MasterMarital>().ReverseMap();
            CreateMap<Master_Nationality, MasterNationality>().ReverseMap();
            CreateMap<Master_Service, MasterService>().ReverseMap();
            CreateMap<Master_State, MasterState>().ReverseMap();
            CreateMap<Project_CreateProject, ProjectCreateProject>().ReverseMap();
            CreateMap<Project_CreateProjectPhase, ProjectCreateProjectPhase>().ReverseMap();
            CreateMap<Project_CreateProjectStep, ProjectCreateProjectStep>().ReverseMap();
            CreateMap<Project_ManageResourceCosting, ProjectManageResourceCosting>().ReverseMap();
            CreateMap<Project_ManageTaskTimeLine, Project_ManageTaskTimeLine>().ReverseMap();
            CreateMap<Project_ProjectCrendentials, ProjectProjectCrendential>().ReverseMap();
            CreateMap<Project_ProjectDocument, ProjectProjectDocument>().ReverseMap();
            CreateMap<Project_ProjectMeeting, ProjectProjectMeeting>().ReverseMap();

            CreateMap<Project_ProjectMOM, ProjectProjectMom>().ReverseMap();

            CreateMap<Project_ProjectResources, ProjectProjectResource>().ReverseMap();
            CreateMap<Project_ProjectStatus, ProjectProjectStatus>().ReverseMap();
            CreateMap<Project_ProjectTask, ProjectProjectTask>().ReverseMap();
            CreateMap<Research_ManageFilterData, ResearchManageFilterDatum>().ReverseMap();
            CreateMap<Research_ManageResearchGroup, ResearchManageResearchGroup>().ReverseMap();
            CreateMap<Research_UploadResearchData, ResearchUploadResearchDatum>().ReverseMap();
            CreateMap<Sales_ManagePositiveLead, SalesManagePositiveLead>().ReverseMap();
            CreateMap<Master_Company, MasterCompany>().ReverseMap();
            CreateMap<Master_Environment, MasterEnvironment>().ReverseMap();
            CreateMap<Master_Menu, MasterMenu>().ReverseMap();
            CreateMap<Master_User_MenuRight, MasterUserMenuRight>().ReverseMap();
            CreateMap<Master_User_Role, MasterUserRole>().ReverseMap();
            CreateMap<Master_Country, MasterCountry>().ReverseMap();
            CreateMap<ProjectManageTaskTimeLine, Project_ManageTaskTimeLine>().ReverseMap();
            CreateMap<AcIncomeTaxMaster,AC_IncomeTaxMaster>().ReverseMap();
            CreateMap<AcITDeduction,AC_ITDeduction>().ReverseMap();
            CreateMap<AcITSubdeduction,AC_ITSubdeduction>().ReverseMap();
            CreateMap<AcITEmployeeParameter,AC_ITEmployeeParameter>().ReverseMap();

            

        }
    }
}
