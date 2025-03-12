using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataAccess.Logic;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Logic;

namespace DataAccess
{
    public class Service : IService
    {


        ER_RemarkTagMasterLogic _ERRemarkTagMaster;
        public ER_RemarkTagMasterLogic ERRemarkTagMaster
        {
            get
            {
                if (_ERRemarkTagMaster == null)
                {
                    _ERRemarkTagMaster = new ER_RemarkTagMasterLogic(new AppDbContext());
                }
                return _ERRemarkTagMaster;
            }
        }

        AC_AllowanceTypeLogic _ACAllowanceType;
        public AC_AllowanceTypeLogic ACAllowanceType
        {
            get
            {
                if (_ACAllowanceType == null)
                {
                    _ACAllowanceType = new AC_AllowanceTypeLogic(new AppDbContext());
                }
                return _ACAllowanceType;
            }
        }

        AC_ITEmployeeParameterLogic _ACITEmployeeParameterMaster;
        public AC_ITEmployeeParameterLogic ACITEmployeeParameterMaster
        {
            get
            {
                if (_ACITEmployeeParameterMaster == null)
                {
                    _ACITEmployeeParameterMaster = new AC_ITEmployeeParameterLogic(new AppDbContext());
                }
                return _ACITEmployeeParameterMaster;
            }
        }

        AC_ITSubdeductionLogic _ACITSubdeductionMaster;
        public AC_ITSubdeductionLogic ACITSubdeductionMaster
        {
            get
            {
                if (_ACITSubdeductionMaster == null)
                {
                    _ACITSubdeductionMaster = new AC_ITSubdeductionLogic(new AppDbContext());
                }
                return _ACITSubdeductionMaster;
            }
        }


        AC_ITDeductionLogic _ACITDeductionMaster;
        public AC_ITDeductionLogic ACITDeductionMaster
        {
            get
            {
                if (_ACITDeductionMaster == null)
                {
                    _ACITDeductionMaster = new AC_ITDeductionLogic(new AppDbContext());
                }
                return _ACITDeductionMaster;
            }
        }

        AC_IncomeTaxMasterLogic _ACIncomeTaxMaster;
        public AC_IncomeTaxMasterLogic ACIncomeTaxMaster
        {
            get
            {
                if (_ACIncomeTaxMaster == null)
                {
                    _ACIncomeTaxMaster = new AC_IncomeTaxMasterLogic(new AppDbContext());
                }
                return _ACIncomeTaxMaster;
            }
        }

       



        AC_CustomerMasterLogic _ACCustomerMaster;
        public AC_CustomerMasterLogic ACCustomerMaster
        {
            get
            {
                if (_ACCustomerMaster == null)
                {
                    _ACCustomerMaster = new AC_CustomerMasterLogic(new AppDbContext());
                }
                return _ACCustomerMaster;
            }
        }

        AC_FinancialYearLogic _ACFinancialYear;
        public AC_FinancialYearLogic ACFinancialYear
        {
            get
            {
                if (_ACFinancialYear == null)
                {
                    _ACFinancialYear = new AC_FinancialYearLogic(new AppDbContext());
                }
                return _ACFinancialYear;
            }
        }

        AC_InvoiceSubTypeMasterLogic _ACInvoiceSubTypeMaster;
        public AC_InvoiceSubTypeMasterLogic ACInvoiceSubTypeMaster
        {
            get
            {
                if (_ACInvoiceSubTypeMaster == null)
                {
                    _ACInvoiceSubTypeMaster = new AC_InvoiceSubTypeMasterLogic(new AppDbContext());
                }
                return _ACInvoiceSubTypeMaster;
            }
        }

        AC_InvoiceTypeCodeMasterLogic _ACInvoiceTypeCodeMaster;
        public AC_InvoiceTypeCodeMasterLogic ACInvoiceTypeCodeMaster
        {
            get
            {
                if (_ACInvoiceTypeCodeMaster == null)
                {
                    _ACInvoiceTypeCodeMaster = new AC_InvoiceTypeCodeMasterLogic(new AppDbContext());
                }
                return _ACInvoiceTypeCodeMaster;
            }
        }

        AC_InvoiceTypeMasterLogic _ACInvoiceTypeMaster;
        public AC_InvoiceTypeMasterLogic ACInvoiceTypeMaster
        {
            get
            {
                if (_ACInvoiceTypeMaster == null)
                {
                    _ACInvoiceTypeMaster = new AC_InvoiceTypeMasterLogic(new AppDbContext());
                }
                return _ACInvoiceTypeMaster;
            }
        }

        AC_ItemGroupLogic _ACItemGroup;
        public AC_ItemGroupLogic ACItemGroup
        {
            get
            {
                if (_ACItemGroup == null)
                {
                    _ACItemGroup = new AC_ItemGroupLogic(new AppDbContext());
                }
                return _ACItemGroup;
            }
        }

        AC_ItemMasterLogic _ACItemMaster;
        public AC_ItemMasterLogic ACItemMaster
        {
            get
            {
                if (_ACItemMaster == null)
                {
                    _ACItemMaster = new AC_ItemMasterLogic(new AppDbContext());
                }
                return _ACItemMaster;
            }
        }

        AC_LanguageLogic _ACLanguage;
        public AC_LanguageLogic ACLanguage
        {
            get
            {
                if (_ACLanguage == null)
                {
                    _ACLanguage = new AC_LanguageLogic(new AppDbContext());
                }
                return _ACLanguage;
            }
        }

        AC_ModeOfPaymentLogic _ACModeOfPayment;
        public AC_ModeOfPaymentLogic ACModeOfPayment
        {
            get
            {
                if (_ACModeOfPayment == null)
                {
                    _ACModeOfPayment = new AC_ModeOfPaymentLogic(new AppDbContext());
                }
                return _ACModeOfPayment;
            }
        }

        AC_ModeOfTransportLogic _ACModeOfTransport;
        public AC_ModeOfTransportLogic ACModeOfTransport
        {
            get
            {
                if (_ACModeOfTransport == null)
                {
                    _ACModeOfTransport = new AC_ModeOfTransportLogic(new AppDbContext());
                }
                return _ACModeOfTransport;
            }
        }

        AC_NomenClatureLogic _ACNomenClature;
        public AC_NomenClatureLogic ACNomenClature
        {
            get
            {
                if (_ACNomenClature == null)
                {
                    _ACNomenClature = new AC_NomenClatureLogic(new AppDbContext());
                }
                return _ACNomenClature;
            }
        }

        AC_PaymentMeansCodeLogic _ACPaymentMeansCode;
        public AC_PaymentMeansCodeLogic ACPaymentMeansCode
        {
            get
            {
                if (_ACPaymentMeansCode == null)
                {
                    _ACPaymentMeansCode = new AC_PaymentMeansCodeLogic(new AppDbContext());
                }
                return _ACPaymentMeansCode;
            }
        }

        AC_PaymentStatusLogic _ACPaymentStatus;
        public AC_PaymentStatusLogic ACPaymentStatus
        {
            get
            {
                if (_ACPaymentStatus == null)
                {
                    _ACPaymentStatus = new AC_PaymentStatusLogic(new AppDbContext());
                }
                return _ACPaymentStatus;
            }
        }

        AC_TaxcodeLogic _ACTaxcode;
        public AC_TaxcodeLogic ACTaxcode
        {
            get
            {
                if (_ACTaxcode == null)
                {
                    _ACTaxcode = new AC_TaxcodeLogic(new AppDbContext());
                }
                return _ACTaxcode;
            }
        }


        AC_TaxDataLogic _ACTaxDataLogic;
        public AC_TaxDataLogic ACTaxData
        {
            get
            {
                if (_ACTaxDataLogic == null)
                {
                    _ACTaxDataLogic = new AC_TaxDataLogic(new AppDbContext());
                }
                return _ACTaxDataLogic;
            }
        }

        AC_TaxGroupLogic _ACTaxGroup;
        public AC_TaxGroupLogic ACTaxGroup
        {
            get
            {
                if (_ACTaxGroup == null)
                {
                    _ACTaxGroup = new AC_TaxGroupLogic(new AppDbContext());
                }
                return _ACTaxGroup;
            }
        }

        AC_TermsOfPaymentLogic _ACTermsOfPayment;
        public AC_TermsOfPaymentLogic ACTermsOfPayment
        {
            get
            {
                if (_ACTermsOfPayment == null)
                {
                    _ACTermsOfPayment = new AC_TermsOfPaymentLogic(new AppDbContext());
                }
                return _ACTermsOfPayment;
            }
        }

        AC_UnitLogic _ACUnit;
        public AC_UnitLogic ACUnit
        {
            get
            {
                if (_ACUnit == null)
                {
                    _ACUnit = new AC_UnitLogic(new AppDbContext());
                }
                return _ACUnit;
            }
        }

        AC_VendorMasterLogic _ACVendorMaster;
        public AC_VendorMasterLogic ACVendorMaster
        {
            get
            {
                if (_ACVendorMaster == null)
                {
                    _ACVendorMaster = new AC_VendorMasterLogic(new AppDbContext());
                }
                return _ACVendorMaster;
            }
        }

        ACSales_ManageInquiryLogic _ACSalesManageInquiry;
        public ACSales_ManageInquiryLogic ACSalesManageInquiry
        {
            get
            {
                if (_ACSalesManageInquiry == null)
                {
                    _ACSalesManageInquiry = new ACSales_ManageInquiryLogic(new AppDbContext());
                }
                return _ACSalesManageInquiry;
            }
        }

        BD_CommunicationHistoryLogic _BDCommunicationHistory;
        public BD_CommunicationHistoryLogic BDCommunicationHistory
        {
            get
            {
                if (_BDCommunicationHistory == null)
                {
                    _BDCommunicationHistory = new BD_CommunicationHistoryLogic(new AppDbContext());
                }
                return _BDCommunicationHistory;
            }
        }

        BD_ReasearchCommunicationStatusLogic _BDReasearchCommunicationStatus;
        public BD_ReasearchCommunicationStatusLogic BDReasearchCommunicationStatus
        {
            get
            {
                if (_BDReasearchCommunicationStatus == null)
                {
                    _BDReasearchCommunicationStatus = new BD_ReasearchCommunicationStatusLogic(new AppDbContext());
                }
                return _BDReasearchCommunicationStatus;
            }
        }

        BD_ResearchAudienceLogic _BDResearchAudience;
        public BD_ResearchAudienceLogic BDResearchAudience
        {
            get
            {
                if (_BDResearchAudience == null)
                {
                    _BDResearchAudience = new BD_ResearchAudienceLogic(new AppDbContext());
                }
                return _BDResearchAudience;
            }
        }

        BD_ResearchChannelTypeLogic _BDResearchChannelType;
        public BD_ResearchChannelTypeLogic BDResearchChannelType
        {
            get
            {
                if (_BDResearchChannelType == null)
                {
                    _BDResearchChannelType = new BD_ResearchChannelTypeLogic(new AppDbContext());
                }
                return _BDResearchChannelType;
            }
        }

        BD_ResearchCommunicationModeLogic _BDResearchCommunicationMode;
        public BD_ResearchCommunicationModeLogic BDResearchCommunicationMode
        {
            get
            {
                if (_BDResearchCommunicationMode == null)
                {
                    _BDResearchCommunicationMode = new BD_ResearchCommunicationModeLogic(new AppDbContext());
                }
                return _BDResearchCommunicationMode;
            }
        }

        CompanySetup_EmailCredentialLogic _CompanySetupEmailCredential;
        public CompanySetup_EmailCredentialLogic CompanySetupEmailCredential
        {
            get
            {
                if (_CompanySetupEmailCredential == null)
                {
                    _CompanySetupEmailCredential = new CompanySetup_EmailCredentialLogic(new AppDbContext());
                }
                return _CompanySetupEmailCredential;
            }
        }

        CompanySetup_EmailTemplateLogic _CompanySetupEmailTemplate;
        public CompanySetup_EmailTemplateLogic CompanySetupEmailTemplate
        {
            get
            {
                if (_CompanySetupEmailTemplate == null)
                {
                    _CompanySetupEmailTemplate = new CompanySetup_EmailTemplateLogic(new AppDbContext());
                }
                return _CompanySetupEmailTemplate;
            }
        }

        ER_ExpenseDataRangeLogic _ERExpenseDataRange;
        public ER_ExpenseDataRangeLogic ERExpenseDataRange
        {
            get
            {
                if (_ERExpenseDataRange == null)
                {
                    _ERExpenseDataRange = new ER_ExpenseDataRangeLogic(new AppDbContext());
                }
                return _ERExpenseDataRange;
            }
        }

        ER_ExpenseSubTypeLogic _ERExpenseSubType;
        public ER_ExpenseSubTypeLogic ERExpenseSubType
        {
            get
            {
                if (_ERExpenseSubType == null)
                {
                    _ERExpenseSubType = new ER_ExpenseSubTypeLogic(new AppDbContext());
                }
                return _ERExpenseSubType;
            }
        }

        ER_ExpenseSubUnitLogic _ERExpenseSubUnit;
        public ER_ExpenseSubUnitLogic ERExpenseSubUnit
        {
            get
            {
                if (_ERExpenseSubUnit == null)
                {
                    _ERExpenseSubUnit = new ER_ExpenseSubUnitLogic(new AppDbContext());
                }
                return _ERExpenseSubUnit;
            }
        }

        ER_ExpenseTypeLogic _ERExpenseType;
        public ER_ExpenseTypeLogic ERExpenseType
        {
            get
            {
                if (_ERExpenseType == null)
                {
                    _ERExpenseType = new ER_ExpenseTypeLogic(new AppDbContext());
                }
                return _ERExpenseType;
            }
        }

        ER_ExpenseUnitLogic _ERExpenseUnit;
        public ER_ExpenseUnitLogic ERExpenseUnit
        {
            get
            {
                if (_ERExpenseUnit == null)
                {
                    _ERExpenseUnit = new ER_ExpenseUnitLogic(new AppDbContext());
                }
                return _ERExpenseUnit;
            }
        }

        ER_HeadDesignationLogic _ERHeadDesignation;
        public ER_HeadDesignationLogic ERHeadDesignation
        {
            get
            {
                if (_ERHeadDesignation == null)
                {
                    _ERHeadDesignation = new ER_HeadDesignationLogic(new AppDbContext());
                }
                return _ERHeadDesignation;
            }
        }

        ER_ManageReportDesignationLogic _ERManageReportDesignation;
        public ER_ManageReportDesignationLogic ERManageReportDesignation
        {
            get
            {
                if (_ERManageReportDesignation == null)
                {
                    _ERManageReportDesignation = new ER_ManageReportDesignationLogic(new AppDbContext());
                }
                return _ERManageReportDesignation;
            }
        }

        ER_RemarkTemplateLogic _ERRemarkTemplate;
        public ER_RemarkTemplateLogic ERRemarkTemplate
        {
            get
            {
                if (_ERRemarkTemplate == null)
                {
                    _ERRemarkTemplate = new ER_RemarkTemplateLogic(new AppDbContext());
                }
                return _ERRemarkTemplate;
            }
        }

        ER_SubstituteExpenseLogic _ERSubstituteExpense;
        public ER_SubstituteExpenseLogic ERSubstituteExpense
        {
            get
            {
                if (_ERSubstituteExpense == null)
                {
                    _ERSubstituteExpense = new ER_SubstituteExpenseLogic(new AppDbContext());
                }
                return _ERSubstituteExpense;
            }
        }

        ERSetup_CostCategoryLogic _ERSetupCostCategory;
        public ERSetup_CostCategoryLogic ERSetupCostCategory
        {
            get
            {
                if (_ERSetupCostCategory == null)
                {
                    _ERSetupCostCategory = new ERSetup_CostCategoryLogic(new AppDbContext());
                }
                return _ERSetupCostCategory;
            }
        }

        ERSetup_CostCityLogic _ERSetupCostCity;
        public ERSetup_CostCityLogic ERSetupCostCity
        {
            get
            {
                if (_ERSetupCostCity == null)
                {
                    _ERSetupCostCity = new ERSetup_CostCityLogic(new AppDbContext());
                }
                return _ERSetupCostCity;
            }
        }

        ERSetup_CurrencyLogic _ERSetupCurrency;
        public ERSetup_CurrencyLogic ERSetupCurrency
        {
            get
            {
                if (_ERSetupCurrency == null)
                {
                    _ERSetupCurrency = new ERSetup_CurrencyLogic(new AppDbContext());
                }
                return _ERSetupCurrency;
            }
        }

        ERSetup_ExpenseEligibilityLogic _ERSetupExpenseEligibility;
        public ERSetup_ExpenseEligibilityLogic ERSetupExpenseEligibility
        {
            get
            {
                if (_ERSetupExpenseEligibility == null)
                {
                    _ERSetupExpenseEligibility = new ERSetup_ExpenseEligibilityLogic(new AppDbContext());
                }
                return _ERSetupExpenseEligibility;
            }
        }

        ERSetup_ExpenseLimitLogic _ERSetupExpenseLimit;
        public ERSetup_ExpenseLimitLogic ERSetupExpenseLimit
        {
            get
            {
                if (_ERSetupExpenseLimit == null)
                {
                    _ERSetupExpenseLimit = new ERSetup_ExpenseLimitLogic(new AppDbContext());
                }
                return _ERSetupExpenseLimit;
            }
        }

        ERSetup_WorkFlowInstanceLogic _ERSetupWorkFlowInstance;
        public ERSetup_WorkFlowInstanceLogic ERSetupWorkFlowInstance
        {
            get
            {
                if (_ERSetupWorkFlowInstance == null)
                {
                    _ERSetupWorkFlowInstance = new ERSetup_WorkFlowInstanceLogic(new AppDbContext());
                }
                return _ERSetupWorkFlowInstance;
            }
        }

        HRA_AttendenceReportLogic _HRAAttendenceReport;
        public HRA_AttendenceReportLogic HRAAttendenceReport
        {
            get
            {
                if (_HRAAttendenceReport == null)
                {
                    _HRAAttendenceReport = new HRA_AttendenceReportLogic(new AppDbContext());
                }
                return _HRAAttendenceReport;
            }
        }

        HRA_DocumentNamingMasterLogic _HRADocumentNamingMaster;
        public HRA_DocumentNamingMasterLogic HRADocumentNamingMaster
        {
            get
            {
                if (_HRADocumentNamingMaster == null)
                {
                    _HRADocumentNamingMaster = new HRA_DocumentNamingMasterLogic(new AppDbContext());
                }
                return _HRADocumentNamingMaster;
            }
        }

        HRA_DocumentTypeLogic _HRADocumentType;
        public HRA_DocumentTypeLogic HRADocumentType
        {
            get
            {
                if (_HRADocumentType == null)
                {
                    _HRADocumentType = new HRA_DocumentTypeLogic(new AppDbContext());
                }
                return _HRADocumentType;
            }
        }

        HRA_EmployeeTypeLogic _HRAEmployeeType;
        public HRA_EmployeeTypeLogic HRAEmployeeType
        {
            get
            {
                if (_HRAEmployeeType == null)
                {
                    _HRAEmployeeType = new HRA_EmployeeTypeLogic(new AppDbContext());
                }
                return _HRAEmployeeType;
            }
        }

        HRA_LeaveTypeMasterLogic _HRALeaveTypeMaster;
        public HRA_LeaveTypeMasterLogic HRALeaveTypeMaster
        {
            get
            {
                if (_HRALeaveTypeMaster == null)
                {
                    _HRALeaveTypeMaster = new HRA_LeaveTypeMasterLogic(new AppDbContext());
                }
                return _HRALeaveTypeMaster;
            }
        }

        HRA_ManageLeaveLogic _HRAManageLeave;
        public HRA_ManageLeaveLogic HRAManageLeave
        {
            get
            {
                if (_HRAManageLeave == null)
                {
                    _HRAManageLeave = new HRA_ManageLeaveLogic(new AppDbContext());
                }
                return _HRAManageLeave;
            }
        }

        HRA_NomineeLogic _HRANominee;
        public HRA_NomineeLogic HRANominee
        {
            get
            {
                if (_HRANominee == null)
                {
                    _HRANominee = new HRA_NomineeLogic(new AppDbContext());
                }
                return _HRANominee;
            }
        }

        HRA_PaySlipCategoryLogic _HRAPaySlipCategory;
        public HRA_PaySlipCategoryLogic HRAPaySlipCategory
        {
            get
            {
                if (_HRAPaySlipCategory == null)
                {
                    _HRAPaySlipCategory = new HRA_PaySlipCategoryLogic(new AppDbContext());
                }
                return _HRAPaySlipCategory;
            }
        }

        HRA_ProfessionalTaxLogic _HRAProfessionalTax;
        public HRA_ProfessionalTaxLogic HRAProfessionalTax
        {
            get
            {
                if (_HRAProfessionalTax == null)
                {
                    _HRAProfessionalTax = new HRA_ProfessionalTaxLogic(new AppDbContext());
                }
                return _HRAProfessionalTax;
            }
        }

        HRA_ShiftLogic _HRAShift;
        public HRA_ShiftLogic HRAShift
        {
            get
            {
                if (_HRAShift == null)
                {
                    _HRAShift = new HRA_ShiftLogic(new AppDbContext());
                }
                return _HRAShift;
            }
        }

        HRA_TaxRegimeLogic _HRATaxRegime;
        public HRA_TaxRegimeLogic HRATaxRegime
        {
            get
            {
                if (_HRATaxRegime == null)
                {
                    _HRATaxRegime = new HRA_TaxRegimeLogic(new AppDbContext());
                }
                return _HRATaxRegime;
            }
        }

        HRA_WeekDayLogic _HRAWeekDay;
        public HRA_WeekDayLogic HRAWeekDay
        {
            get
            {
                if (_HRAWeekDay == null)
                {
                    _HRAWeekDay = new HRA_WeekDayLogic(new AppDbContext());
                }
                return _HRAWeekDay;
            }
        }

        HRAEmployee_LeaveAuthorizationLogic _HRAEmployeeLeaveAuthorization;
        public HRAEmployee_LeaveAuthorizationLogic HRAEmployeeLeaveAuthorization
        {
            get
            {
                if (_HRAEmployeeLeaveAuthorization == null)
                {
                    _HRAEmployeeLeaveAuthorization = new HRAEmployee_LeaveAuthorizationLogic(new AppDbContext());
                }
                return _HRAEmployeeLeaveAuthorization;
            }
        }

        HRAEmployee_WeekOffLogic _HRAEmployeeWeekOff;
        public HRAEmployee_WeekOffLogic HRAEmployeeWeekOff
        {
            get
            {
                if (_HRAEmployeeWeekOff == null)
                {
                    _HRAEmployeeWeekOff = new HRAEmployee_WeekOffLogic(new AppDbContext());
                }
                return _HRAEmployeeWeekOff;
            }
        }

        HRAEmployeeSalary_SalaryParameterLogic _HRAEmployeeSalarySalaryParameter;
        public HRAEmployeeSalary_SalaryParameterLogic HRAEmployeeSalarySalaryParameter
        {
            get
            {
                if (_HRAEmployeeSalarySalaryParameter == null)
                {
                    _HRAEmployeeSalarySalaryParameter = new HRAEmployeeSalary_SalaryParameterLogic(new AppDbContext());
                }
                return _HRAEmployeeSalarySalaryParameter;
            }
        }

        HRAEmployeeSalary_SalaryPaySlipLogic _HRAEmployeeSalarySalaryPaySlip;
        public HRAEmployeeSalary_SalaryPaySlipLogic HRAEmployeeSalarySalaryPaySlip
        {
            get
            {
                if (_HRAEmployeeSalarySalaryPaySlip == null)
                {
                    _HRAEmployeeSalarySalaryPaySlip = new HRAEmployeeSalary_SalaryPaySlipLogic(new AppDbContext());
                }
                return _HRAEmployeeSalarySalaryPaySlip;
            }
        }

        HRAIncome_IncomeTaxLogic _HRAIncomeIncomeTax;
        public HRAIncome_IncomeTaxLogic HRAIncomeIncomeTax
        {
            get
            {
                if (_HRAIncomeIncomeTax == null)
                {
                    _HRAIncomeIncomeTax = new HRAIncome_IncomeTaxLogic(new AppDbContext());
                }
                return _HRAIncomeIncomeTax;
            }
        }

        HRAIncome_ITDeductionLogic _HRAIncomeITDeduction;
        public HRAIncome_ITDeductionLogic HRAIncomeITDeduction
        {
            get
            {
                if (_HRAIncomeITDeduction == null)
                {
                    _HRAIncomeITDeduction = new HRAIncome_ITDeductionLogic(new AppDbContext());
                }
                return _HRAIncomeITDeduction;
            }
        }

        HRAIncome_ITEmployeeParameterLogic _HRAIncomeITEmployeeParameter;
        public HRAIncome_ITEmployeeParameterLogic HRAIncomeITEmployeeParameter
        {
            get
            {
                if (_HRAIncomeITEmployeeParameter == null)
                {
                    _HRAIncomeITEmployeeParameter = new HRAIncome_ITEmployeeParameterLogic(new AppDbContext());
                }
                return _HRAIncomeITEmployeeParameter;
            }
        }

        Income_ITSubDeductionLogic _IncomeITSubDeduction;
        public Income_ITSubDeductionLogic IncomeITSubDeduction
        {
            get
            {
                if (_IncomeITSubDeduction == null)
                {
                    _IncomeITSubDeduction = new Income_ITSubDeductionLogic(new AppDbContext());
                }
                return _IncomeITSubDeduction;
            }
        }

        Master_AssetLogic _MasterAsset;
        public Master_AssetLogic MasterAsset
        {
            get
            {
                if (_MasterAsset == null)
                {
                    _MasterAsset = new Master_AssetLogic(new AppDbContext());
                }
                return _MasterAsset;
            }
        }

        Master_BankACtypeLogic _MasterBankACtype;
        public Master_BankACtypeLogic MasterBankACtype
        {
            get
            {
                if (_MasterBankACtype == null)
                {
                    _MasterBankACtype = new Master_BankACtypeLogic(new AppDbContext());
                }
                return _MasterBankACtype;
            }
        }

        Master_BanktypeLogic _MasterBanktype;
        public Master_BanktypeLogic MasterBanktype
        {
            get
            {
                if (_MasterBanktype == null)
                {
                    _MasterBanktype = new Master_BanktypeLogic(new AppDbContext());
                }
                return _MasterBanktype;
            }
        }

        Master_BloodGroupLogic _MasterBloodGroup;
        public Master_BloodGroupLogic MasterBloodGroup
        {
            get
            {
                if (_MasterBloodGroup == null)
                {
                    _MasterBloodGroup = new Master_BloodGroupLogic(new AppDbContext());
                }
                return _MasterBloodGroup;
            }
        }

        Master_CategorTagLogic _MasterCategorTag;
        public Master_CategorTagLogic MasterCategorTag
        {
            get
            {
                if (_MasterCategorTag == null)
                {
                    _MasterCategorTag = new Master_CategorTagLogic(new AppDbContext());
                }
                return _MasterCategorTag;
            }
        }

        Master_CategoryLogic _MasterCategory;
        public Master_CategoryLogic MasterCategory
        {
            get
            {
                if (_MasterCategory == null)
                {
                    _MasterCategory = new Master_CategoryLogic(new AppDbContext());
                }
                return _MasterCategory;
            }
        }

        Master_CityLogic _MasterCity;
        public Master_CityLogic MasterCity
        {
            get
            {
                if (_MasterCity == null)
                {
                    _MasterCity = new Master_CityLogic(new AppDbContext());
                }
                return _MasterCity;
            }
        }

        Master_CompanyBranchLogic _MasterCompanyBranch;
        public Master_CompanyBranchLogic MasterCompanyBranch
        {
            get
            {
                if (_MasterCompanyBranch == null)
                {
                    _MasterCompanyBranch = new Master_CompanyBranchLogic(new AppDbContext());
                }
                return _MasterCompanyBranch;
            }
        }

        Master_CompanyTypeLogic _MasterCompanyType;
        public Master_CompanyTypeLogic MasterCompanyType
        {
            get
            {
                if (_MasterCompanyType == null)
                {
                    _MasterCompanyType = new Master_CompanyTypeLogic(new AppDbContext());
                }
                return _MasterCompanyType;
            }
        }

        Master_DepartmentLogic _MasterDepartmentLogic;
        public Master_DepartmentLogic MasterDepartment
        {
            get
            {
                if (_MasterDepartmentLogic == null)
                {
                    _MasterDepartmentLogic = new Master_DepartmentLogic(new AppDbContext());
                }
                return _MasterDepartmentLogic;
            }
        }

        Master_DesignationLogic _MasterDesignationLogic;
        public Master_DesignationLogic MasterDesignation
        {
            get
            {
                if (_MasterDesignationLogic == null)
                {
                    _MasterDesignationLogic = new Master_DesignationLogic(new AppDbContext());
                }
                return _MasterDesignationLogic;
            }
        }

        Master_DocumentCategoryLogic _MasterDocumentCategory;
        public Master_DocumentCategoryLogic MasterDocumentCategory
        {
            get
            {
                if (_MasterDocumentCategory == null)
                {
                    _MasterDocumentCategory = new Master_DocumentCategoryLogic(new AppDbContext());
                }
                return _MasterDocumentCategory;
            }
        }

        Master_DocumentCategoryTagLogic _MasterDocumentCategoryTag;
        public Master_DocumentCategoryTagLogic MasterDocumentCategoryTag
        {
            get
            {
                if (_MasterDocumentCategoryTag == null)
                {
                    _MasterDocumentCategoryTag = new Master_DocumentCategoryTagLogic(new AppDbContext());
                }
                return _MasterDocumentCategoryTag;
            }
        }

        Master_DocumentGropLogic _MasterDocumentGrop;
        public Master_DocumentGropLogic MasterDocumentGrop
        {
            get
            {
                if (_MasterDocumentGrop == null)
                {
                    _MasterDocumentGrop = new Master_DocumentGropLogic(new AppDbContext());
                }
                return _MasterDocumentGrop;
            }
        }

        Master_EmployeeLogic _MasterEmployee;
        public Master_EmployeeLogic MasterEmployee
        {
            get
            {
                if (_MasterEmployee == null)
                {
                    _MasterEmployee = new Master_EmployeeLogic(new AppDbContext());
                }
                return _MasterEmployee;
            }
        }

        Master_Employee_DocumentSetupLogic _MasterEmployeeDocumentSetup;
        public Master_Employee_DocumentSetupLogic MasterEmployeeDocumentSetup
        {
            get
            {
                if (_MasterEmployeeDocumentSetup == null)
                {
                    _MasterEmployeeDocumentSetup = new Master_Employee_DocumentSetupLogic(new AppDbContext());
                }
                return _MasterEmployeeDocumentSetup;
            }
        }

        Master_Employee_EducationDetailsLogic _MasterEmployeeEducationDetails;
        public Master_Employee_EducationDetailsLogic MasterEmployeeEducationDetails
        {
            get
            {
                if (_MasterEmployeeEducationDetails == null)
                {
                    _MasterEmployeeEducationDetails = new Master_Employee_EducationDetailsLogic(new AppDbContext());
                }
                return _MasterEmployeeEducationDetails;
            }
        }


        Master_Employee_ExperienceDetailLogic _MasterEmployeeExperienceDetail;
        public Master_Employee_ExperienceDetailLogic MasterEmployeeExperienceDetail
        {
            get
            {
                if (_MasterEmployeeExperienceDetail == null)
                {
                    _MasterEmployeeExperienceDetail = new Master_Employee_ExperienceDetailLogic(new AppDbContext());
                }
                return _MasterEmployeeExperienceDetail;
            }
        }


        Master_Employee_AssestsDetailsLogic _MasterEmployeeAssestsDetails;
        public Master_Employee_AssestsDetailsLogic MasterEmployeeAssestsDetails
        {
            get
            {
                if (_MasterEmployeeAssestsDetails == null)
                {
                    _MasterEmployeeAssestsDetails = new Master_Employee_AssestsDetailsLogic(new AppDbContext());
                }
                return _MasterEmployeeAssestsDetails;
            }
        }

        Master_Employee_leaveAuthorisationLogic _MasterEmployeeleaveAuthorisation;
        public Master_Employee_leaveAuthorisationLogic MasterEmployeeleaveAuthorisation
        {
            get
            {
                if (_MasterEmployeeleaveAuthorisation == null)
                {
                    _MasterEmployeeleaveAuthorisation = new Master_Employee_leaveAuthorisationLogic(new AppDbContext());
                }
                return _MasterEmployeeleaveAuthorisation;
            }
        }

        Master_GenderLogic _MasterGender;
        public Master_GenderLogic MasterGender
        {
            get
            {
                if (_MasterGender == null)
                {
                    _MasterGender = new Master_GenderLogic(new AppDbContext());
                }
                return _MasterGender;
            }
        }

        Master_HonorificLogic _MasterHonorific;
        public Master_HonorificLogic MasterHonorific
        {
            get
            {
                if (_MasterHonorific == null)
                {
                    _MasterHonorific = new Master_HonorificLogic(new AppDbContext());
                }
                return _MasterHonorific;
            }
        }

        Master_IndustryLogic _MasterIndustry;
        public Master_IndustryLogic MasterIndustry
        {
            get
            {
                if (_MasterIndustry == null)
                {
                    _MasterIndustry = new Master_IndustryLogic(new AppDbContext());
                }
                return _MasterIndustry;
            }
        }

        Master_ManageBankDetailLogic _MasterManageBankDetail;
        public Master_ManageBankDetailLogic MasterManageBankDetail
        {
            get
            {
                if (_MasterManageBankDetail == null)
                {
                    _MasterManageBankDetail = new Master_ManageBankDetailLogic(new AppDbContext());
                }
                return _MasterManageBankDetail;
            }
        }

        Master_ManageDocumentLogic _MasterManageDocument;
        public Master_ManageDocumentLogic MasterManageDocument
        {
            get
            {
                if (_MasterManageDocument == null)
                {
                    _MasterManageDocument = new Master_ManageDocumentLogic(new AppDbContext());
                }
                return _MasterManageDocument;
            }
        }

        Master_MaritalLogic _MasterMarital;
        public Master_MaritalLogic MasterMarital
        {
            get
            {
                if (_MasterMarital == null)
                {
                    _MasterMarital = new Master_MaritalLogic(new AppDbContext());
                }
                return _MasterMarital;
            }
        }

        Master_NationalityLogic _MasterNationality;
        public Master_NationalityLogic MasterNationality
        {
            get
            {
                if (_MasterNationality == null)
                {
                    _MasterNationality = new Master_NationalityLogic(new AppDbContext());
                }
                return _MasterNationality;
            }
        }

        Master_ServiceLogic _MasterService;
        public Master_ServiceLogic MasterService
        {
            get
            {
                if (_MasterService == null)
                {
                    _MasterService = new Master_ServiceLogic(new AppDbContext());
                }
                return _MasterService;
            }
        }

        Master_StateLogic _MasterState;
        public Master_StateLogic MasterState
        {
            get
            {
                if (_MasterState == null)
                {
                    _MasterState = new Master_StateLogic(new AppDbContext());
                }
                return _MasterState;
            }
        }

        Master_YearLogic _MasterYear;
        public Master_YearLogic MasterYear
        {
            get
            {
                if (_MasterYear == null)
                {
                    _MasterYear = new Master_YearLogic(new AppDbContext());
                }
                return _MasterYear;
            }
        }

        Master_CompanyLogic _MasterCompany;
        public Master_CompanyLogic MasterCompany
        {
            get
            {
                if (_MasterCompany == null)
                {
                    _MasterCompany = new Master_CompanyLogic(new AppDbContext());
                }
                return _MasterCompany;
            }
        }

        Master_CountryLogic _MasterCountry;
        public Master_CountryLogic MasterCountry
        {
            get
            {
                if (_MasterCountry == null)
                {
                    _MasterCountry = new Master_CountryLogic(new AppDbContext());
                }
                return _MasterCountry;
            }
        }

        Master_EnvironmentLogic _MasterEnvironment;
        public Master_EnvironmentLogic MasterEnvironment
        {
            get
            {
                if (_MasterEnvironment == null)
                {
                    _MasterEnvironment = new Master_EnvironmentLogic(new AppDbContext());
                }
                return _MasterEnvironment;
            }
        }

        Master_MenuLogic _MasterMenu;
        public Master_MenuLogic MasterMenu
        {
            get
            {
                if (_MasterMenu == null)
                {
                    _MasterMenu = new Master_MenuLogic(new AppDbContext());
                }
                return _MasterMenu;
            }
        }

        Master_UserMenuRightLogic _MasterUserMenuRight;
        public Master_UserMenuRightLogic MasterUserMenuRight
        {
            get
            {
                if (_MasterUserMenuRight == null)
                {
                    _MasterUserMenuRight = new Master_UserMenuRightLogic(new AppDbContext());
                }
                return _MasterUserMenuRight;
            }
        }

        Master_UserRoleLogic _MasterUserRole;
        public Master_UserRoleLogic MasterUserRole
        {
            get
            {
                if (_MasterUserRole == null)
                {
                    _MasterUserRole = new Master_UserRoleLogic(new AppDbContext());
                }
                return _MasterUserRole;
            }
        }

        Project_CreateProjectLogic _ProjectCreateProject;
        public Project_CreateProjectLogic ProjectCreateProject
        {
            get
            {
                if (_ProjectCreateProject == null)
                {
                    _ProjectCreateProject = new Project_CreateProjectLogic(new AppDbContext());
                }
                return _ProjectCreateProject;
            }
        }

        Project_CreateProjectPhaseLogic _ProjectCreateProjectPhase;
        public Project_CreateProjectPhaseLogic ProjectCreateProjectPhase
        {
            get
            {
                if (_ProjectCreateProjectPhase == null)
                {
                    _ProjectCreateProjectPhase = new Project_CreateProjectPhaseLogic(new AppDbContext());
                }
                return _ProjectCreateProjectPhase;
            }
        }

        Project_CreateProjectStepLogic _ProjectCreateProjectStep;
        public Project_CreateProjectStepLogic ProjectCreateProjectStep
        {
            get
            {
                if (_ProjectCreateProjectStep == null)
                {
                    _ProjectCreateProjectStep = new Project_CreateProjectStepLogic(new AppDbContext());
                }
                return _ProjectCreateProjectStep;
            }
        }

        Project_ManageResourceCostingLogic _ProjectManageResourceCosting;
        public Project_ManageResourceCostingLogic ProjectManageResourceCosting
        {
            get
            {
                if (_ProjectManageResourceCosting == null)
                {
                    _ProjectManageResourceCosting = new Project_ManageResourceCostingLogic(new AppDbContext());
                }
                return _ProjectManageResourceCosting;
            }
        }

        Project_ManageTaskTimeLineLogic _ProjectManageTaskTimeLine;
        public Project_ManageTaskTimeLineLogic ProjectManageTaskTimeLine
        {
            get
            {
                if (_ProjectManageTaskTimeLine == null)
                {
                    _ProjectManageTaskTimeLine = new Project_ManageTaskTimeLineLogic(new AppDbContext());
                }
                return _ProjectManageTaskTimeLine;
            }
        }

        Project_ProjectCrendentialsLogic _ProjectProjectCrendentials;
        public Project_ProjectCrendentialsLogic ProjectProjectCrendentials
        {
            get
            {
                if (_ProjectProjectCrendentials == null)
                {
                    _ProjectProjectCrendentials = new Project_ProjectCrendentialsLogic(new AppDbContext());
                }
                return _ProjectProjectCrendentials;
            }
        }

        Project_ProjectDocumentLogic _ProjectProjectDocument;
        public Project_ProjectDocumentLogic ProjectProjectDocument
        {
            get
            {
                if (_ProjectProjectDocument == null)
                {
                    _ProjectProjectDocument = new Project_ProjectDocumentLogic(new AppDbContext());
                }
                return _ProjectProjectDocument;
            }
        }

        Project_ProjectMeetingLogic _ProjectProjectMeeting;
        public Project_ProjectMeetingLogic ProjectProjectMeeting
        {
            get
            {
                if (_ProjectProjectMeeting == null)
                {
                    _ProjectProjectMeeting = new Project_ProjectMeetingLogic(new AppDbContext());
                }
                return _ProjectProjectMeeting;
            }
        }

        Project_ProjectMOMLogic _ProjectProjectMOM;
        public Project_ProjectMOMLogic ProjectProjectMOM
        {
            get
            {
                if (_ProjectProjectMOM == null)
                {
                    _ProjectProjectMOM = new Project_ProjectMOMLogic(new AppDbContext());
                }
                return _ProjectProjectMOM;
            }
        }


        Project_ProjectResourcesLogic _ProjectProjectResources;
        public Project_ProjectResourcesLogic ProjectProjectResources
        {
            get
            {
                if (_ProjectProjectResources == null)
                {
                    _ProjectProjectResources = new Project_ProjectResourcesLogic(new AppDbContext());
                }
                return _ProjectProjectResources;
            }
        }

        Project_ProjectStatusLogic _ProjectProjectStatus;
        public Project_ProjectStatusLogic ProjectProjectStatus
        {
            get
            {
                if (_ProjectProjectStatus == null)
                {
                    _ProjectProjectStatus = new Project_ProjectStatusLogic(new AppDbContext());
                }
                return _ProjectProjectStatus;
            }
        }

        Project_ProjectTaskLogic _ProjectProjectTask;
        public Project_ProjectTaskLogic ProjectProjectTask
        {
            get
            {
                if (_ProjectProjectTask == null)
                {
                    _ProjectProjectTask = new Project_ProjectTaskLogic(new AppDbContext());
                }
                return _ProjectProjectTask;
            }
        }

        Research_ManageFilterDataLogic _ResearchManageFilterData;
        public Research_ManageFilterDataLogic ResearchManageFilterData
        {
            get
            {
                if (_ResearchManageFilterData == null)
                {
                    _ResearchManageFilterData = new Research_ManageFilterDataLogic(new AppDbContext());
                }
                return _ResearchManageFilterData;
            }
        }

        Research_ManageResearchGroupLogic _ResearchManageResearchGroup;
        public Research_ManageResearchGroupLogic ResearchManageResearchGroup
        {
            get
            {
                if (_ResearchManageResearchGroup == null)
                {
                    _ResearchManageResearchGroup = new Research_ManageResearchGroupLogic(new AppDbContext());
                }
                return _ResearchManageResearchGroup;
            }
        }

        Research_UploadResearchDataLogic _ResearchUploadResearchData;
        public Research_UploadResearchDataLogic ResearchUploadResearchData
        {
            get
            {
                if (_ResearchUploadResearchData == null)
                {
                    _ResearchUploadResearchData = new Research_UploadResearchDataLogic(new AppDbContext());
                }
                return _ResearchUploadResearchData;
            }
        }

        Sales_ManagePositiveLeadLogic _SalesManagePositiveLead;
        public Sales_ManagePositiveLeadLogic SalesManagePositiveLead
        {
            get
            {
                if (_SalesManagePositiveLead == null)
                {
                    _SalesManagePositiveLead = new Sales_ManagePositiveLeadLogic(new AppDbContext());
                }
                return _SalesManagePositiveLead;
            }
        }

        Timesheet_LineLogic _TimesheetLine;
        public Timesheet_LineLogic TimesheetLine
        {
            get
            {
                if (_TimesheetLine == null)
                {
                    _TimesheetLine = new Timesheet_LineLogic(new AppDbContext());
                }
                return _TimesheetLine;
            }
        }

        Timesheet_HeaderLogic _TimesheetHeader;
        public Timesheet_HeaderLogic TimesheetHeader
        {
            get
            {
                if (_TimesheetHeader == null)
                {
                    _TimesheetHeader = new Timesheet_HeaderLogic(new AppDbContext());
                }
                return _TimesheetHeader;
            }
        }

    }
}
