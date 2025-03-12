namespace DataAccess.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
: base("data source=195.250.30.50;initial catalog=ksperp_db;user id=sampusr;password=ksperp123;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<AC_AllowanceType> AC_AllowanceType { get; set; }
        public virtual DbSet<AC_CustomerMaster> AC_CustomerMaster { get; set; }
        public virtual DbSet<AC_FinancialYear> AC_FinancialYear { get; set; }
        public virtual DbSet<AC_IncomeTaxMaster> AC_IncomeTaxMaster { get; set; }
        public virtual DbSet<AC_InvoiceSubTypeMaster> AC_InvoiceSubTypeMaster { get; set; }
        public virtual DbSet<AC_InvoiceTypeCodeMaster> AC_InvoiceTypeCodeMaster { get; set; }
        public virtual DbSet<AC_InvoiceTypeMaster> AC_InvoiceTypeMaster { get; set; }
        public virtual DbSet<AC_ITDeduction> AC_ITDeduction { get; set; }
        public virtual DbSet<AC_ItemGroup> AC_ItemGroup { get; set; }
        public virtual DbSet<AC_ItemMaster> AC_ItemMaster { get; set; }
        public virtual DbSet<AC_ITEmployeeParameter> AC_ITEmployeeParameter { get; set; }
        public virtual DbSet<AC_ITSubdeduction> AC_ITSubdeduction { get; set; }
        public virtual DbSet<AC_Language> AC_Language { get; set; }
        public virtual DbSet<AC_ModeOfPayment> AC_ModeOfPayment { get; set; }
        public virtual DbSet<AC_ModeOfTransport> AC_ModeOfTransport { get; set; }
        public virtual DbSet<AC_NomenClature> AC_NomenClature { get; set; }
        public virtual DbSet<AC_PaymentMeansCode> AC_PaymentMeansCode { get; set; }
        public virtual DbSet<AC_PaymentStatus> AC_PaymentStatus { get; set; }
        public virtual DbSet<AC_Taxcode> AC_Taxcode { get; set; }
        public virtual DbSet<AC_TaxData> AC_TaxData { get; set; }
        public virtual DbSet<AC_TaxGroup> AC_TaxGroup { get; set; }
        public virtual DbSet<AC_TermsOfPayment> AC_TermsOfPayment { get; set; }
        public virtual DbSet<AC_Unit> AC_Unit { get; set; }
        public virtual DbSet<AC_VendorMaster> AC_VendorMaster { get; set; }
        public virtual DbSet<ACSales_ManageInquiry> ACSales_ManageInquiry { get; set; }
        public virtual DbSet<BD_CommunicationHistory> BD_CommunicationHistory { get; set; }
        public virtual DbSet<BD_ReasearchCommunicationStatus> BD_ReasearchCommunicationStatus { get; set; }
        public virtual DbSet<BD_ResearchAudience> BD_ResearchAudience { get; set; }
        public virtual DbSet<BD_ResearchChannelType> BD_ResearchChannelType { get; set; }
        public virtual DbSet<BD_ResearchCommunicationMode> BD_ResearchCommunicationMode { get; set; }
        public virtual DbSet<CompanySetup_EmailCredential> CompanySetup_EmailCredential { get; set; }
        public virtual DbSet<CompanySetup_EmailTemplate> CompanySetup_EmailTemplate { get; set; }
        public virtual DbSet<ER_ExpenseDataRange> ER_ExpenseDataRange { get; set; }
        public virtual DbSet<ER_ExpenseSubType> ER_ExpenseSubType { get; set; }
        public virtual DbSet<ER_ExpenseSubUnit> ER_ExpenseSubUnit { get; set; }
        public virtual DbSet<ER_ExpenseType> ER_ExpenseType { get; set; }
        public virtual DbSet<ER_ExpenseUnit> ER_ExpenseUnit { get; set; }
        public virtual DbSet<ER_HeadDesignation> ER_HeadDesignation { get; set; }
        public virtual DbSet<ER_ManageReportDesignation> ER_ManageReportDesignation { get; set; }
        public virtual DbSet<ER_RemarkTagMaster> ER_RemarkTagMaster { get; set; }
        public virtual DbSet<ER_RemarkTemplate> ER_RemarkTemplate { get; set; }
        public virtual DbSet<ER_SubstituteExpense> ER_SubstituteExpense { get; set; }
        public virtual DbSet<ERSetup_CostCategory> ERSetup_CostCategory { get; set; }
        public virtual DbSet<ERSetup_CostCity> ERSetup_CostCity { get; set; }
        public virtual DbSet<ERSetup_Currency> ERSetup_Currency { get; set; }
        public virtual DbSet<ERSetup_ExpenseEligibility> ERSetup_ExpenseEligibility { get; set; }
        public virtual DbSet<ERSetup_ExpenseLimit> ERSetup_ExpenseLimit { get; set; }
        public virtual DbSet<ERSetup_WorkFlowInstance> ERSetup_WorkFlowInstance { get; set; }
        public virtual DbSet<HRA_AttendenceReport> HRA_AttendenceReport { get; set; }
        public virtual DbSet<HRA_DocumentNamingMaster> HRA_DocumentNamingMaster { get; set; }
        public virtual DbSet<HRA_DocumentType> HRA_DocumentType { get; set; }
        public virtual DbSet<HRA_EmployeeType> HRA_EmployeeType { get; set; }
        public virtual DbSet<HRA_LeaveStatus> HRA_LeaveStatus { get; set; }
        public virtual DbSet<HRA_LeaveTypeMaster> HRA_LeaveTypeMaster { get; set; }
        public virtual DbSet<HRA_ManageLeave> HRA_ManageLeave { get; set; }
        public virtual DbSet<HRA_Nominee> HRA_Nominee { get; set; }
        public virtual DbSet<HRA_PaySlipCategory> HRA_PaySlipCategory { get; set; }
        public virtual DbSet<HRA_ProfessionalTax> HRA_ProfessionalTax { get; set; }
        public virtual DbSet<HRA_Shift> HRA_Shift { get; set; }
        public virtual DbSet<HRA_TaxRegime> HRA_TaxRegime { get; set; }
        public virtual DbSet<HRA_WeekDay> HRA_WeekDay { get; set; }
        public virtual DbSet<HRAEmployee_LeaveAuthorization> HRAEmployee_LeaveAuthorization { get; set; }
        public virtual DbSet<HRAEmployee_WeekOff> HRAEmployee_WeekOff { get; set; }
        public virtual DbSet<HRAEmployeeSalary_SalaryParameter> HRAEmployeeSalary_SalaryParameter { get; set; }
        public virtual DbSet<HRAEmployeeSalary_SalaryPaySlip> HRAEmployeeSalary_SalaryPaySlip { get; set; }
        public virtual DbSet<HRAIncome_IncomeTax> HRAIncome_IncomeTax { get; set; }
        public virtual DbSet<HRAIncome_ITDeduction> HRAIncome_ITDeduction { get; set; }
        public virtual DbSet<HRAIncome_ITEmployeeParameter> HRAIncome_ITEmployeeParameter { get; set; }
        public virtual DbSet<Income_ITSubDeduction> Income_ITSubDeduction { get; set; }
        public virtual DbSet<MA_AuthAC> MA_AuthAC { get; set; }
        public virtual DbSet<Master_Asset> Master_Asset { get; set; }
        public virtual DbSet<Master_BankACtype> Master_BankACtype { get; set; }
        public virtual DbSet<Master_Banktype> Master_Banktype { get; set; }
        public virtual DbSet<Master_BloodGroup> Master_BloodGroup { get; set; }
        public virtual DbSet<Master_CategorTag> Master_CategorTag { get; set; }
        public virtual DbSet<Master_Category> Master_Category { get; set; }
        public virtual DbSet<Master_City> Master_City { get; set; }
        public virtual DbSet<Master_Company> Master_Company { get; set; }
        public virtual DbSet<Master_CompanyBranch> Master_CompanyBranch { get; set; }
        public virtual DbSet<Master_CompanyType> Master_CompanyType { get; set; }
        public virtual DbSet<Master_Country> Master_Country { get; set; }
        public virtual DbSet<Master_Department> Master_Department { get; set; }
        public virtual DbSet<Master_Designation> Master_Designation { get; set; }
        public virtual DbSet<Master_DocumentCategory> Master_DocumentCategory { get; set; }
        public virtual DbSet<Master_DocumentCategoryTag> Master_DocumentCategoryTag { get; set; }
        public virtual DbSet<Master_DocumentGrop> Master_DocumentGrop { get; set; }
        public virtual DbSet<Master_Employee> Master_Employee { get; set; }
        public virtual DbSet<Master_Employee_AssestsDetails> Master_Employee_AssestsDetails { get; set; }
        public virtual DbSet<Master_Employee_DocumentSetup> Master_Employee_DocumentSetup { get; set; }
        public virtual DbSet<Master_Employee_EducationDetails> Master_Employee_EducationDetails { get; set; }
        public virtual DbSet<Master_Employee_ExperienceDetail> Master_Employee_ExperienceDetail { get; set; }
        public virtual DbSet<Master_Employee_HR_Setup> Master_Employee_HR_Setup { get; set; }
        public virtual DbSet<Master_Employee_leaveAuthorisation> Master_Employee_leaveAuthorisation { get; set; }
        public virtual DbSet<Master_Environment> Master_Environment { get; set; }
        public virtual DbSet<Master_Gender> Master_Gender { get; set; }
        public virtual DbSet<Master_Honorific> Master_Honorific { get; set; }
        public virtual DbSet<Master_Industry> Master_Industry { get; set; }
        public virtual DbSet<Master_ManageBankDetail> Master_ManageBankDetail { get; set; }
        public virtual DbSet<Master_ManageDocument> Master_ManageDocument { get; set; }
        public virtual DbSet<Master_Marital> Master_Marital { get; set; }
        public virtual DbSet<Master_Menu> Master_Menu { get; set; }
        public virtual DbSet<Master_Nationality> Master_Nationality { get; set; }
        public virtual DbSet<Master_Service> Master_Service { get; set; }
        public virtual DbSet<Master_State> Master_State { get; set; }
        public virtual DbSet<Master_User_MenuRight> Master_User_MenuRight { get; set; }
        public virtual DbSet<Master_User_Role> Master_User_Role { get; set; }
        public virtual DbSet<Master_Year> Master_Year { get; set; }
        public virtual DbSet<Project_CreateProject> Project_CreateProject { get; set; }
        public virtual DbSet<Project_CreateProjectPhase> Project_CreateProjectPhase { get; set; }
        public virtual DbSet<Project_CreateProjectStep> Project_CreateProjectStep { get; set; }
        public virtual DbSet<Project_ManageResourceCosting> Project_ManageResourceCosting { get; set; }
        public virtual DbSet<Project_ManageTaskTimeLine> Project_ManageTaskTimeLine { get; set; }
        public virtual DbSet<Project_ProjectCrendentials> Project_ProjectCrendentials { get; set; }
        public virtual DbSet<Project_ProjectDocument> Project_ProjectDocument { get; set; }
        public virtual DbSet<Project_ProjectMeeting> Project_ProjectMeeting { get; set; }
        public virtual DbSet<Project_ProjectMOM> Project_ProjectMOM { get; set; }

        public virtual DbSet<Project_ProjectResources> Project_ProjectResources { get; set; }
        public virtual DbSet<Project_ProjectStatus> Project_ProjectStatus { get; set; }
        public virtual DbSet<Project_ProjectTask> Project_ProjectTask { get; set; }
        public virtual DbSet<Research_ManageFilterData> Research_ManageFilterData { get; set; }
        public virtual DbSet<Research_ManageResearchGroup> Research_ManageResearchGroup { get; set; }
        public virtual DbSet<Research_UploadResearchData> Research_UploadResearchData { get; set; }
        public virtual DbSet<Sales_ManagePositiveLead> Sales_ManagePositiveLead { get; set; }
        public virtual DbSet<SoleExpenceDetailMaster> SoleExpenceDetailMasters { get; set; }
        public virtual DbSet<SoleExpenceMaster> SoleExpenceMasters { get; set; }
        public virtual DbSet<Timesheet_Header> Timesheet_Header { get; set; }
        public virtual DbSet<Timesheet_Line> Timesheet_Line { get; set; }
        public virtual DbSet<Website_BannerMaster> Website_BannerMaster { get; set; }
        public virtual DbSet<Website_BlogCategoryMaster> Website_BlogCategoryMaster { get; set; }
        public virtual DbSet<Website_BlogMaster> Website_BlogMaster { get; set; }
        public virtual DbSet<Website_CMSMaster> Website_CMSMaster { get; set; }
        public virtual DbSet<Website_CompanyBasicData> Website_CompanyBasicData { get; set; }
        public virtual DbSet<Website_PhotoGallery> Website_PhotoGallery { get; set; }
        public virtual DbSet<Website_Servicecategory> Website_Servicecategory { get; set; }
        public virtual DbSet<Website_Testimonial> Website_Testimonial { get; set; }
        public virtual DbSet<Website_VideoGallery> Website_VideoGallery { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AC_AllowanceType>()
                .Property(e => e.AllowanceType_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_AllowanceType>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<AC_AllowanceType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_CustomerMaster>()
                .Property(e => e.CustomerMaster_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_CustomerMaster>()
                .Property(e => e.Mobile_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_CustomerMaster>()
                .Property(e => e.Landline_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_CustomerMaster>()
                .Property(e => e.Tax_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_CustomerMaster>()
                .Property(e => e.Currency_UUID)
                .IsUnicode(false);

            modelBuilder.Entity<AC_CustomerMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_FinancialYear>()
                .Property(e => e.FinancialYear_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_FinancialYear>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_IncomeTaxMaster>()
                .Property(e => e.IncomeTax_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_IncomeTaxMaster>()
                .Property(e => e.Yearly_Income_From)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_IncomeTaxMaster>()
                .Property(e => e.Yearly_Income_To)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_IncomeTaxMaster>()
                .Property(e => e.Percentage)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_IncomeTaxMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_InvoiceSubTypeMaster>()
                .Property(e => e.InvoiceSubType_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_InvoiceSubTypeMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_InvoiceTypeCodeMaster>()
                .Property(e => e.InvoiceTypeCode_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_InvoiceTypeCodeMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_InvoiceTypeMaster>()
                .Property(e => e.InvoiceType_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_InvoiceTypeMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITDeduction>()
                .Property(e => e.ITDeduction_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITDeduction>()
                .Property(e => e.Serial_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITDeduction>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ItemGroup>()
                .Property(e => e.ItemGroup_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ItemGroup>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ItemMaster>()
                .Property(e => e.ItemMaster_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ItemMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITEmployeeParameter>()
                .Property(e => e.ITEmployeeParameter_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITEmployeeParameter>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITEmployeeParameter>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITSubdeduction>()
                .Property(e => e.ITSubDeduction_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITSubdeduction>()
                .Property(e => e.Serial_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ITSubdeduction>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_Language>()
                .Property(e => e.Language_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_Language>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ModeOfPayment>()
                .Property(e => e.ModeOfPayment_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ModeOfPayment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AC_ModeOfPayment>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ModeOfTransport>()
                .Property(e => e.ModeOfTransport_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_ModeOfTransport>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AC_ModeOfTransport>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_NomenClature>()
                .Property(e => e.NomenClature_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_NomenClature>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_PaymentMeansCode>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_PaymentMeansCode>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_PaymentStatus>()
                .Property(e => e.PaymentStatus_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_PaymentStatus>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_Taxcode>()
                .Property(e => e.TaxCode_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_Taxcode>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TaxData>()
                .Property(e => e.TaxData_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TaxData>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TaxData>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TaxGroup>()
                .Property(e => e.TaxGroup_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TaxGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AC_TaxGroup>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TermsOfPayment>()
                .Property(e => e.TermsOfPayment_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_TermsOfPayment>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_Unit>()
                .Property(e => e.Unit_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_Unit>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_VendorMaster>()
                .Property(e => e.VendorMaster_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_VendorMaster>()
                .Property(e => e.Mobile_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_VendorMaster>()
                .Property(e => e.Landline_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AC_VendorMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ACSales_ManageInquiry>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ACSales_ManageInquiry>()
                .Property(e => e.Inquiry_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ACSales_ManageInquiry>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ACSales_ManageInquiry>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_CommunicationHistory>()
                .Property(e => e.CommunicationHistory_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_CommunicationHistory>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ReasearchCommunicationStatus>()
                .Property(e => e.ReasearchCommunicationStatus_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ReasearchCommunicationStatus>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ResearchAudience>()
                .Property(e => e.ResearchAudience_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ResearchAudience>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ResearchChannelType>()
                .Property(e => e.ResearchChannel_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ResearchChannelType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ResearchCommunicationMode>()
                .Property(e => e.ResearchCommunicationMode_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BD_ResearchCommunicationMode>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CompanySetup_EmailCredential>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CompanySetup_EmailCredential>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CompanySetup_EmailTemplate>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CompanySetup_EmailTemplate>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CompanySetup_EmailTemplate>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseDataRange>()
                .Property(e => e.ExpenseDataRange_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseDataRange>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseSubType>()
                .Property(e => e.ExpenseSubType_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseSubType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseSubUnit>()
                .Property(e => e.ExpenseSubUnit_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseSubUnit>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseType>()
                .Property(e => e.ExpenseType_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseUnit>()
                .Property(e => e.ExpenseUnit_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ExpenseUnit>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_HeadDesignation>()
                .Property(e => e.HeadDesignation_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_HeadDesignation>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ManageReportDesignation>()
                .Property(e => e.ManageReportDesignation_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ManageReportDesignation>()
                .Property(e => e.NoOfER_Approval)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ManageReportDesignation>()
                .Property(e => e.NoOfPAF_Approval)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_ManageReportDesignation>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_RemarkTagMaster>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_RemarkTagMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_RemarkTemplate>()
                .Property(e => e.RemarkTemplate_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_RemarkTemplate>()
                .Property(e => e.Remark_String)
                .IsUnicode(false);

            modelBuilder.Entity<ER_RemarkTemplate>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_SubstituteExpense>()
                .Property(e => e.SubstituteExpense_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ER_SubstituteExpense>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_CostCategory>()
                .Property(e => e.CostCategory_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_CostCategory>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_CostCity>()
                .Property(e => e.CostCityCategory_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_CostCity>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_Currency>()
                .Property(e => e.Currency_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_Currency>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_ExpenseEligibility>()
                .Property(e => e.ExpenseEligibility_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_ExpenseEligibility>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_ExpenseLimit>()
                .Property(e => e.ExpenseLimit_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_ExpenseLimit>()
                .Property(e => e.Limit_Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_ExpenseLimit>()
                .Property(e => e.Max_Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_ExpenseLimit>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_WorkFlowInstance>()
                .Property(e => e.WorkFlowInstance_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ERSetup_WorkFlowInstance>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_AttendenceReport>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_AttendenceReport>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_DocumentNamingMaster>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_DocumentNamingMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_DocumentType>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_DocumentType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_EmployeeType>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_EmployeeType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_LeaveStatus>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_LeaveStatus>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_LeaveTypeMaster>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_LeaveTypeMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ManageLeave>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ManageLeave>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<HRA_ManageLeave>()
                .Property(e => e.Contact_Number)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ManageLeave>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_Nominee>()
                .Property(e => e.Nominee_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_Nominee>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_PaySlipCategory>()
                .Property(e => e.PaySlipCategory_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_PaySlipCategory>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ProfessionalTax>()
                .Property(e => e.ProfessionalTax_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ProfessionalTax>()
                .Property(e => e.Monthly_Income_From)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ProfessionalTax>()
                .Property(e => e.Monthly_Income_To)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ProfessionalTax>()
                .Property(e => e.Rupees)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_ProfessionalTax>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_Shift>()
                .Property(e => e.Shift_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_Shift>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_TaxRegime>()
                .Property(e => e.TaxRegime_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_TaxRegime>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_WeekDay>()
                .Property(e => e.WeekDays_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRA_WeekDay>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployee_LeaveAuthorization>()
                .Property(e => e.LeaveAuthorization_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployee_LeaveAuthorization>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployee_WeekOff>()
                .Property(e => e.WeekOff_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployee_WeekOff>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployeeSalary_SalaryParameter>()
                .Property(e => e.Parameter_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployeeSalary_SalaryParameter>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployeeSalary_SalaryPaySlip>()
                .Property(e => e.PaySlip_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAEmployeeSalary_SalaryPaySlip>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_IncomeTax>()
                .Property(e => e.IncomeMaster_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_IncomeTax>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_ITDeduction>()
                .Property(e => e.ITDeduction_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_ITDeduction>()
                .Property(e => e.Serial_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_ITDeduction>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_ITEmployeeParameter>()
                .Property(e => e.ITEmployeeParameter_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_ITEmployeeParameter>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HRAIncome_ITEmployeeParameter>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Income_ITSubDeduction>()
                .Property(e => e.ITSubDeduction_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Income_ITSubDeduction>()
                .Property(e => e.Serial_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Income_ITSubDeduction>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MA_AuthAC>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MA_AuthAC>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<Master_Asset>()
                .Property(e => e.Asset_id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Asset>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_BankACtype>()
                .Property(e => e.Bank_id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_BankACtype>()
                .Property(e => e.Bank_AccontType)
                .IsUnicode(false);

            modelBuilder.Entity<Master_BankACtype>()
                .Property(e => e.Bank_Account_Status)
                .IsUnicode(false);

            modelBuilder.Entity<Master_BankACtype>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Banktype>()
                .Property(e => e.Bank_id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Banktype>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_BloodGroup>()
                .Property(e => e.BloodGroup_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_BloodGroup>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_CategorTag>()
                .Property(e => e.CategoryTag_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_CategorTag>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Category>()
                .Property(e => e.Category_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Category>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_City>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_City>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Company>()
                .Property(e => e.CompanyId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Company>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_CompanyBranch>()
                .Property(e => e.CompanyBranch_id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_CompanyBranch>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_CompanyType>()
                .Property(e => e.CompanyType_id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_CompanyType>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Country>()
                .Property(e => e.CountryId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Country>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Department>()
                .Property(e => e.Department_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Department>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Designation>()
                .Property(e => e.Designation_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Designation>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_DocumentCategory>()
                .Property(e => e.Document_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_DocumentCategory>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_DocumentCategoryTag>()
                .Property(e => e.DocumentCategory_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_DocumentCategoryTag>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_DocumentGrop>()
                .Property(e => e.id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_DocumentGrop>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee>()
                .Property(e => e.BP_Code)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_AssestsDetails>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_AssestsDetails>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_DocumentSetup>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_DocumentSetup>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_EducationDetails>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_EducationDetails>()
                .Property(e => e.Percentage_Grade)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_EducationDetails>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_ExperienceDetail>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_ExperienceDetail>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_HR_Setup>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_HR_Setup>()
                .Property(e => e.BP_Code)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_leaveAuthorisation>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_leaveAuthorisation>()
                .Property(e => e.No_Of_Leave_Allowed)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Employee_leaveAuthorisation>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Environment>()
                .Property(e => e.EnvironmentId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Environment>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Gender>()
                .Property(e => e.id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Gender>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Honorific>()
                .Property(e => e.id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Honorific>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Industry>()
                .Property(e => e.Industry_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Industry>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_ManageBankDetail>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_ManageBankDetail>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_ManageDocument>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_ManageDocument>()
                .Property(e => e.RemindMeBefore_Days)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_ManageDocument>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Marital>()
                .Property(e => e.Marital_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Marital>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Menu>()
                .Property(e => e.MenuId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Menu>()
                .Property(e => e.MenuLevel)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Menu>()
                .Property(e => e.Sequence)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Menu>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Nationality>()
                .Property(e => e.Nationality_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Nationality>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Service>()
                .Property(e => e.Service_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Service>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_State>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_State>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_User_MenuRight>()
                .Property(e => e.UserMenuRightsId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_User_MenuRight>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_User_Role>()
                .Property(e => e.UserRoleId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_User_Role>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Year>()
                .Property(e => e.Year_id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Master_Year>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProject>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProject>()
                .Property(e => e.Project_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Project_CreateProject>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProjectPhase>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProjectPhase>()
                .Property(e => e.Project_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Project_CreateProjectPhase>()
                .Property(e => e.Phase_Cost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProjectPhase>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProjectStep>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_CreateProjectStep>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Project_CreateProjectStep>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ManageResourceCosting>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ManageResourceCosting>()
                .Property(e => e.Rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ManageResourceCosting>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ManageTaskTimeLine>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ManageTaskTimeLine>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectCrendentials>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectCrendentials>()
                .Property(e => e.Credentials_Details)
                .IsUnicode(false);

            modelBuilder.Entity<Project_ProjectCrendentials>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectDocument>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectDocument>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectMeeting>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectMeeting>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Project_ProjectMeeting>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectMOM>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectMOM>()
                .Property(e => e.Meeting_Ajenda)
                .IsUnicode(false);

            modelBuilder.Entity<Project_ProjectMOM>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectResources>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectResources>()
                .Property(e => e.Rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectResources>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectStatus>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectStatus>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectTask>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Project_ProjectTask>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_ManageFilterData>()
                .Property(e => e.ManageFilterData_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_ManageFilterData>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Research_ManageFilterData>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_ManageResearchGroup>()
                .Property(e => e.ManageResearchGroup_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_ManageResearchGroup>()
                .Property(e => e.ResearchData_Count)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_ManageResearchGroup>()
                .Property(e => e.Filteration_Datacount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_ManageResearchGroup>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.UploadResearchData_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.GST_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.Email_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.Alternate_Email)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.Mobile_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.Alternate_MobileNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.PinCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.LandLine_No)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Research_UploadResearchData>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sales_ManagePositiveLead>()
                .Property(e => e.ManagePositiveLead_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sales_ManagePositiveLead>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SoleExpenceDetailMaster>()
                .Property(e => e.Quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SoleExpenceDetailMaster>()
                .Property(e => e.UnitCost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SoleExpenceDetailMaster>()
                .Property(e => e.TotalCost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SoleExpenceDetailMaster>()
                .Property(e => e.TaxAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SoleExpenceDetailMaster>()
                .Property(e => e.BillAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Timesheet_Header>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Timesheet_Header>()
                .Property(e => e.Restriction_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Timesheet_Header>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Timesheet_Line>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Timesheet_Line>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Timesheet_Line>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_BannerMaster>()
                .Property(e => e.Banner_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_BannerMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_BlogCategoryMaster>()
                .Property(e => e.BlogCategoryId)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_BlogCategoryMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_BlogMaster>()
                .Property(e => e.Blog_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_BlogMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_CMSMaster>()
                .Property(e => e.CMS_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_CMSMaster>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_CompanyBasicData>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_CompanyBasicData>()
                .Property(e => e.Logo)
                .IsFixedLength();

            modelBuilder.Entity<Website_CompanyBasicData>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_PhotoGallery>()
                .Property(e => e.PhotoGallery_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_PhotoGallery>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_Servicecategory>()
                .Property(e => e.ServiceCategory_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_Servicecategory>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_Testimonial>()
                .Property(e => e.Testimonial_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_Testimonial>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_VideoGallery>()
                .Property(e => e.VideoGallery_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Website_VideoGallery>()
                .Property(e => e.RecordNo)
                .HasPrecision(18, 0);
        }
    }
}
