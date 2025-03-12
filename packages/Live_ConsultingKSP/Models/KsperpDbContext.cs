using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Models;

public partial class KsperpDbContext : DbContext
{
    public KsperpDbContext()
    {
    }

    public KsperpDbContext(DbContextOptions<KsperpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcAllowanceType> AcAllowanceTypes { get; set; }

    public virtual DbSet<AcCustomerMaster> AcCustomerMasters { get; set; }

    public virtual DbSet<AcFinancialYear> AcFinancialYears { get; set; }

    public virtual DbSet<AcInvoiceSubTypeMaster> AcInvoiceSubTypeMasters { get; set; }

    public virtual DbSet<AcInvoiceTypeCodeMaster> AcInvoiceTypeCodeMasters { get; set; }

    public virtual DbSet<AcInvoiceTypeMaster> AcInvoiceTypeMasters { get; set; }

    public virtual DbSet<AcItemGroup> AcItemGroups { get; set; }

    public virtual DbSet<AcItemMaster> AcItemMasters { get; set; }

    public virtual DbSet<AcLanguage> AcLanguages { get; set; }

    public virtual DbSet<AcModeOfPayment> AcModeOfPayments { get; set; }

    public virtual DbSet<AcModeOfTransport> AcModeOfTransports { get; set; }

    public virtual DbSet<AcNomenClature> AcNomenClatures { get; set; }

    public virtual DbSet<AcPaymentMeansCode> AcPaymentMeansCodes { get; set; }

    public virtual DbSet<AcPaymentStatus> AcPaymentStatuses { get; set; }

    public virtual DbSet<AcTaxDatum> AcTaxData { get; set; }

    public virtual DbSet<AcTaxGroup> AcTaxGroups { get; set; }

    public virtual DbSet<AcTaxcode> AcTaxcodes { get; set; }

    public virtual DbSet<AcTermsOfPayment> AcTermsOfPayments { get; set; }

    public virtual DbSet<AcUnit> AcUnits { get; set; }

    public virtual DbSet<AcVendorMaster> AcVendorMasters { get; set; }

    public virtual DbSet<AcsalesManageInquiry> AcsalesManageInquiries { get; set; }

    public virtual DbSet<BdCommunicationHistory> BdCommunicationHistories { get; set; }

    public virtual DbSet<BdReasearchCommunicationStatus> BdReasearchCommunicationStatuses { get; set; }

    public virtual DbSet<BdResearchAudience> BdResearchAudiences { get; set; }

    public virtual DbSet<BdResearchChannelType> BdResearchChannelTypes { get; set; }

    public virtual DbSet<BdResearchCommunicationMode> BdResearchCommunicationModes { get; set; }

    public virtual DbSet<CompanySetupEmailCredential> CompanySetupEmailCredentials { get; set; }

    public virtual DbSet<CompanySetupEmailTemplate> CompanySetupEmailTemplates { get; set; }

    public virtual DbSet<ErExpenseDataRange> ErExpenseDataRanges { get; set; }

    public virtual DbSet<ErExpenseSubType> ErExpenseSubTypes { get; set; }

    public virtual DbSet<ErExpenseSubUnit> ErExpenseSubUnits { get; set; }

    public virtual DbSet<ErExpenseType> ErExpenseTypes { get; set; }

    public virtual DbSet<ErExpenseUnit> ErExpenseUnits { get; set; }

    public virtual DbSet<ErHeadDesignation> ErHeadDesignations { get; set; }

    public virtual DbSet<ErManageReportDesignation> ErManageReportDesignations { get; set; }

    public virtual DbSet<ErRemarkTemplate> ErRemarkTemplates { get; set; }

    public virtual DbSet<ErSubstituteExpense> ErSubstituteExpenses { get; set; }

    public virtual DbSet<ErsetupCostCategory> ErsetupCostCategories { get; set; }

    public virtual DbSet<ErsetupCostCity> ErsetupCostCities { get; set; }

    public virtual DbSet<ErsetupCurrency> ErsetupCurrencies { get; set; }

    public virtual DbSet<ErsetupExpenseEligibility> ErsetupExpenseEligibilities { get; set; }

    public virtual DbSet<ErsetupExpenseLimit> ErsetupExpenseLimits { get; set; }

    public virtual DbSet<ErsetupWorkFlowInstance> ErsetupWorkFlowInstances { get; set; }

    public virtual DbSet<HraAttendenceReport> HraAttendenceReports { get; set; }

    public virtual DbSet<HraDocumentNamingMaster> HraDocumentNamingMasters { get; set; }

    public virtual DbSet<HraDocumentType> HraDocumentTypes { get; set; }

    public virtual DbSet<HraEmployeeType> HraEmployeeTypes { get; set; }

    public virtual DbSet<HraLeaveTypeMaster> HraLeaveTypeMasters { get; set; }

    public virtual DbSet<HraManageLeave> HraManageLeaves { get; set; }

    public virtual DbSet<HraNominee> HraNominees { get; set; }

    public virtual DbSet<HraPaySlipCategory> HraPaySlipCategories { get; set; }

    public virtual DbSet<HraProfessionalTax> HraProfessionalTaxes { get; set; }

    public virtual DbSet<HraShift> HraShifts { get; set; }

    public virtual DbSet<HraTaxRegime> HraTaxRegimes { get; set; }

    public virtual DbSet<HraWeekDay> HraWeekDays { get; set; }

    public virtual DbSet<HraemployeeLeaveAuthorization> HraemployeeLeaveAuthorizations { get; set; }

    public virtual DbSet<HraemployeeSalarySalaryParameter> HraemployeeSalarySalaryParameters { get; set; }

    public virtual DbSet<HraemployeeSalarySalaryPaySlip> HraemployeeSalarySalaryPaySlips { get; set; }

    public virtual DbSet<HraemployeeWeekOff> HraemployeeWeekOffs { get; set; }

    public virtual DbSet<HraincomeIncomeTax> HraincomeIncomeTaxes { get; set; }

    public virtual DbSet<HraincomeItdeduction> HraincomeItdeductions { get; set; }

    public virtual DbSet<HraincomeItemployeeParameter> HraincomeItemployeeParameters { get; set; }

    public virtual DbSet<IncomeItsubDeduction> IncomeItsubDeductions { get; set; }

    public virtual DbSet<MaAuthAc> MaAuthAcs { get; set; }

    public virtual DbSet<MasterAsset> MasterAssets { get; set; }

    public virtual DbSet<MasterBankActype> MasterBankActypes { get; set; }

    public virtual DbSet<MasterBanktype> MasterBanktypes { get; set; }

    public virtual DbSet<MasterBloodGroup> MasterBloodGroups { get; set; }

    public virtual DbSet<MasterCategorTag> MasterCategorTags { get; set; }

    public virtual DbSet<MasterCategory> MasterCategories { get; set; }

    public virtual DbSet<MasterCity> MasterCities { get; set; }

    public virtual DbSet<MasterCompany> MasterCompanies { get; set; }

    public virtual DbSet<MasterCompanyBranch> MasterCompanyBranches { get; set; }

    public virtual DbSet<MasterCompanyType> MasterCompanyTypes { get; set; }

    public virtual DbSet<MasterCountry> MasterCountries { get; set; }

    public virtual DbSet<MasterDepartment> MasterDepartments { get; set; }

    public virtual DbSet<MasterDesignation> MasterDesignations { get; set; }

    public virtual DbSet<MasterDocumentCategory> MasterDocumentCategories { get; set; }

    public virtual DbSet<MasterDocumentCategoryTag> MasterDocumentCategoryTags { get; set; }

    public virtual DbSet<MasterDocumentGrop> MasterDocumentGrops { get; set; }

    public virtual DbSet<MasterEmployee> MasterEmployees { get; set; }

    public virtual DbSet<MasterEmployeeDocumentSetup> MasterEmployeeDocumentSetups { get; set; }

    public virtual DbSet<MasterEmployeeHrSetup> MasterEmployeeHrSetups { get; set; }

    public virtual DbSet<MasterEmployeeLeaveAuthorisation> MasterEmployeeLeaveAuthorisations { get; set; }

    public virtual DbSet<MasterEnvironment> MasterEnvironments { get; set; }

    public virtual DbSet<MasterGender> MasterGenders { get; set; }

    public virtual DbSet<MasterHonorific> MasterHonorifics { get; set; }

    public virtual DbSet<MasterIndustry> MasterIndustries { get; set; }

    public virtual DbSet<MasterManageBankDetail> MasterManageBankDetails { get; set; }

    public virtual DbSet<MasterManageDocument> MasterManageDocuments { get; set; }

    public virtual DbSet<MasterMarital> MasterMaritals { get; set; }

    public virtual DbSet<MasterMenu> MasterMenus { get; set; }

    public virtual DbSet<MasterNationality> MasterNationalities { get; set; }

    public virtual DbSet<MasterService> MasterServices { get; set; }

    public virtual DbSet<MasterState> MasterStates { get; set; }

    public virtual DbSet<MasterUserMenuRight> MasterUserMenuRights { get; set; }

    public virtual DbSet<MasterUserRole> MasterUserRoles { get; set; }

    public virtual DbSet<MasterYear> MasterYears { get; set; }

    public virtual DbSet<ProjectCreateProject> ProjectCreateProjects { get; set; }

    public virtual DbSet<ProjectCreateProjectPhase> ProjectCreateProjectPhases { get; set; }

    public virtual DbSet<ProjectCreateProjectStep> ProjectCreateProjectSteps { get; set; }

    public virtual DbSet<ProjectManageResourceCosting> ProjectManageResourceCostings { get; set; }

    public virtual DbSet<ProjectManageTaskTimeLine> ProjectManageTaskTimeLines { get; set; }

    public virtual DbSet<ProjectProjectCrendential> ProjectProjectCrendentials { get; set; }

    public virtual DbSet<ProjectProjectDocument> ProjectProjectDocuments { get; set; }

    public virtual DbSet<ProjectProjectMeeting> ProjectProjectMeetings { get; set; }

    public virtual DbSet<ProjectProjectMom> ProjectProjectMoms { get; set; }

    public virtual DbSet<ProjectProjectResource> ProjectProjectResources { get; set; }

    public virtual DbSet<ProjectProjectStatus> ProjectProjectStatuses { get; set; }

    public virtual DbSet<ProjectProjectTask> ProjectProjectTasks { get; set; }

    public virtual DbSet<ResearchManageFilterDatum> ResearchManageFilterData { get; set; }

    public virtual DbSet<ResearchManageResearchGroup> ResearchManageResearchGroups { get; set; }

    public virtual DbSet<ResearchUploadResearchDatum> ResearchUploadResearchData { get; set; }

    public virtual DbSet<SalesManagePositiveLead> SalesManagePositiveLeads { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=190.92.175.250; Database=ksperp_db; User Id=sampusr; Password=ksperp123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcAllowanceType>(entity =>
        {
            entity.HasKey(e => e.AllowanceTypeId);

            entity.ToTable("AC_AllowanceType");

            entity.Property(e => e.AllowanceTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AllowanceType_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Mode).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Remark).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcCustomerMaster>(entity =>
        {
            entity.HasKey(e => e.CustomerMasterId);

            entity.ToTable("AC_CustomerMaster");

            entity.Property(e => e.CustomerMasterId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CustomerMaster_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BillingBuildingNo)
                .HasMaxLength(50)
                .HasColumnName("Billing_Building_No");
            entity.Property(e => e.BillingCityUuid)
                .HasMaxLength(50)
                .HasColumnName("Billing_City_UUID");
            entity.Property(e => e.BillingCountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Billing_Country_UUID");
            entity.Property(e => e.BillingPostalCode)
                .HasMaxLength(50)
                .HasColumnName("Billing_Postal_Code");
            entity.Property(e => e.BillingStateUuid)
                .HasMaxLength(50)
                .HasColumnName("Billing_State_UUID");
            entity.Property(e => e.BillingStreet1)
                .HasMaxLength(50)
                .HasColumnName("Billing_Street_1");
            entity.Property(e => e.BillingStreet2)
                .HasMaxLength(50)
                .HasColumnName("Billing_Street_2");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("Company_Name");
            entity.Property(e => e.CompanyRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Company_Registration_No");
            entity.Property(e => e.CurrencyUuid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Currency_UUID");
            entity.Property(e => e.CustomerEmailId)
                .HasMaxLength(50)
                .HasColumnName("CustomerEmail_Id");
            entity.Property(e => e.CustomerMasterName)
                .HasMaxLength(50)
                .HasColumnName("CustomerMaster_Name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LandlineNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Landline_No");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MobileNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Mobile_No");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ShippingBuildingNo)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Building_No");
            entity.Property(e => e.ShippingCityUuid)
                .HasMaxLength(50)
                .HasColumnName("Shipping_City_UUID");
            entity.Property(e => e.ShippingCountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Country_UUID");
            entity.Property(e => e.ShippingPostalCode)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Postal_Code");
            entity.Property(e => e.ShippingStateUuid)
                .HasMaxLength(50)
                .HasColumnName("Shipping_State_UUID");
            entity.Property(e => e.ShippingStreet1)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Street_1");
            entity.Property(e => e.ShippingStreet2)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Street_2");
            entity.Property(e => e.TaxNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Tax_No");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcFinancialYear>(entity =>
        {
            entity.HasKey(e => e.FinancialYearId);

            entity.ToTable("AC_FinancialYear");

            entity.Property(e => e.FinancialYearId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("FinancialYear_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_date");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_date");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcInvoiceSubTypeMaster>(entity =>
        {
            entity.HasKey(e => e.InvoiceSubTypeId);

            entity.ToTable("AC_InvoiceSubTypeMaster");

            entity.Property(e => e.InvoiceSubTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("InvoiceSubType_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.InvoiceTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("Invoice_Type_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcInvoiceTypeCodeMaster>(entity =>
        {
            entity.HasKey(e => e.InvoiceTypeCode).HasName("PK_AC_InvoiceTypeCode");

            entity.ToTable("AC_InvoiceTypeCodeMaster");

            entity.Property(e => e.InvoiceTypeCode)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("InvoiceType_Code");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.InvoiceSubTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("InvoiceSubType_UUID");
            entity.Property(e => e.InvoiceTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("InvoiceType_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcInvoiceTypeMaster>(entity =>
        {
            entity.HasKey(e => e.InvoiceTypeId);

            entity.ToTable("AC_InvoiceTypeMaster");

            entity.Property(e => e.InvoiceTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("InvoiceType_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.InvoiceType)
                .HasMaxLength(50)
                .HasColumnName("Invoice_Type");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcItemGroup>(entity =>
        {
            entity.HasKey(e => e.ItemGroupId).HasName("PK_AC_ItemGroupMaster");

            entity.ToTable("AC_ItemGroup");

            entity.Property(e => e.ItemGroupId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ItemGroup_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.GroupCode)
                .HasMaxLength(50)
                .HasColumnName("Group_Code");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .HasColumnName("Group_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Mode).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcItemMaster>(entity =>
        {
            entity.HasKey(e => e.ItemMasterId);

            entity.ToTable("AC_ItemMaster");

            entity.Property(e => e.ItemMasterId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ItemMaster_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .HasColumnName("Item_Code");
            entity.Property(e => e.ItemGroupUuid)
                .HasMaxLength(50)
                .HasColumnName("ItemGroup_UUID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("Item_Name");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Mode).HasMaxLength(50);
            entity.Property(e => e.PackUnitUuid)
                .HasMaxLength(50)
                .HasColumnName("Pack_Unit_UUID");
            entity.Property(e => e.PurchaseUnitUuid)
                .HasMaxLength(50)
                .HasColumnName("Purchase_Unit_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SalesUnitUuid)
                .HasMaxLength(50)
                .HasColumnName("Sales_Unit_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcLanguage>(entity =>
        {
            entity.HasKey(e => e.LanguageId);

            entity.ToTable("AC_Language");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Language_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(50)
                .HasColumnName("Language_Name");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcModeOfPayment>(entity =>
        {
            entity.HasKey(e => e.ModeOfPaymentId);

            entity.ToTable("AC_ModeOfPayment");

            entity.Property(e => e.ModeOfPaymentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ModeOfPayment_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcModeOfTransport>(entity =>
        {
            entity.HasKey(e => e.ModeOfTransportId);

            entity.ToTable("AC_ModeOfTransport");

            entity.Property(e => e.ModeOfTransportId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ModeOfTransport_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcNomenClature>(entity =>
        {
            entity.HasKey(e => e.NomenClatureId);

            entity.ToTable("AC_NomenClature");

            entity.Property(e => e.NomenClatureId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("NomenClature_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("DocumentType_UUID");
            entity.Property(e => e.FinancialYearUuid)
                .HasMaxLength(50)
                .HasColumnName("FinancialYear_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Prefix).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartNo)
                .HasColumnType("datetime")
                .HasColumnName("Start_No");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcPaymentMeansCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AC_PaymentMeans Code");

            entity.ToTable("AC_PaymentMeansCode");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.PaymentMeansCodeName)
                .HasMaxLength(50)
                .HasColumnName("PaymentMeansCode_Name");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.UsageInEn16931)
                .HasMaxLength(50)
                .HasColumnName("Usage_in_EN16931");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcPaymentStatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId);

            entity.ToTable("AC_PaymentStatus");

            entity.Property(e => e.PaymentStatusId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PaymentStatus_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Mode).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcTaxDatum>(entity =>
        {
            entity.HasKey(e => e.TaxDataId);

            entity.ToTable("AC_TaxData");

            entity.Property(e => e.TaxDataId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TaxData_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Amount).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.FromDate)
                .HasColumnType("datetime")
                .HasColumnName("From_Date");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsReserveCharged).HasColumnName("IsReserve_Charged");
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TaxCodeUuid)
                .HasMaxLength(50)
                .HasColumnName("TaxCode_UUID");
            entity.Property(e => e.TaxGroupUuid)
                .HasMaxLength(50)
                .HasColumnName("TaxGroup_UUID");
            entity.Property(e => e.ToDate)
                .HasColumnType("datetime")
                .HasColumnName("To_Date");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcTaxGroup>(entity =>
        {
            entity.HasKey(e => e.TaxGroupId);

            entity.ToTable("AC_TaxGroup");

            entity.Property(e => e.TaxGroupId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TaxGroup_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TaxGroupName)
                .HasMaxLength(50)
                .HasColumnName("TaxGroup_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcTaxcode>(entity =>
        {
            entity.HasKey(e => e.TaxCodeId);

            entity.ToTable("AC_Taxcode");

            entity.Property(e => e.TaxCodeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TaxCode_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(50)
                .HasColumnName("Tax_Code");
            entity.Property(e => e.TaxGroupUuid)
                .HasMaxLength(50)
                .HasColumnName("TaxGroup_UUID");
            entity.Property(e => e.TaxName)
                .HasMaxLength(50)
                .HasColumnName("Tax_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcTermsOfPayment>(entity =>
        {
            entity.HasKey(e => e.TermsOfPaymentId);

            entity.ToTable("AC_TermsOfPayment");

            entity.Property(e => e.TermsOfPaymentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TermsOfPayment_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK_Master_Unit");

            entity.ToTable("AC_Unit");

            entity.Property(e => e.UnitId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Unit_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Mode).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .HasColumnName("Unit_Name");
            entity.Property(e => e.UnitShortName)
                .HasMaxLength(50)
                .HasColumnName("Unit_ShortName");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<AcVendorMaster>(entity =>
        {
            entity.HasKey(e => e.VendorMasterId);

            entity.ToTable("AC_VendorMaster");

            entity.Property(e => e.VendorMasterId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("VendorMaster_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BillingBuildingNo)
                .HasMaxLength(50)
                .HasColumnName("Billing_Building_No");
            entity.Property(e => e.BillingCityUuid)
                .HasMaxLength(50)
                .HasColumnName("Billing_City_UUID");
            entity.Property(e => e.BillingCountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Billing_Country_UUID");
            entity.Property(e => e.BillingPostalCode)
                .HasMaxLength(50)
                .HasColumnName("Billing_Postal_Code");
            entity.Property(e => e.BillingStateUuid)
                .HasMaxLength(50)
                .HasColumnName("Billing_State_UUID");
            entity.Property(e => e.BillingStreet1)
                .HasMaxLength(50)
                .HasColumnName("Billing_Street_1");
            entity.Property(e => e.BillingStreet2)
                .HasMaxLength(50)
                .HasColumnName("Billing_Street_2");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("Company_Name");
            entity.Property(e => e.CrNo)
                .HasMaxLength(50)
                .HasColumnName("CR_No");
            entity.Property(e => e.CurrencyUuid)
                .HasMaxLength(50)
                .HasColumnName("Currency_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LandlineNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Landline_No");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MobileNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Mobile_No");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ShippingBuildingNo)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Building_No");
            entity.Property(e => e.ShippingCityUuid)
                .HasMaxLength(50)
                .HasColumnName("Shipping_City_UUID");
            entity.Property(e => e.ShippingCountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Country_UUID");
            entity.Property(e => e.ShippingPostalCode)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Postal_Code");
            entity.Property(e => e.ShippingStateUuid)
                .HasMaxLength(50)
                .HasColumnName("Shipping_State_UUID");
            entity.Property(e => e.ShippingStreet1)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Street_1");
            entity.Property(e => e.ShippingStreet2)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Street_2");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.VendorEmailId)
                .HasMaxLength(50)
                .HasColumnName("VendorEmail_Id");
            entity.Property(e => e.VendorMasterName)
                .HasMaxLength(50)
                .HasColumnName("VendorMaster_Name");
        });

        modelBuilder.Entity<AcsalesManageInquiry>(entity =>
        {
            entity.ToTable("ACSales_ManageInquiry");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.CurrencyUuid)
                .HasMaxLength(50)
                .HasColumnName("Currency_UUID");
            entity.Property(e => e.CustomerUuid)
                .HasMaxLength(50)
                .HasColumnName("Customer_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.InquiryNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Inquiry_No");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItemNameUuid)
                .HasMaxLength(50)
                .HasColumnName("ItemName_UUID");
            entity.Property(e => e.ItemTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ItemType_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.OrderDate).HasColumnName("Order_Date");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(25)
                .HasColumnName("Project_Name");
            entity.Property(e => e.Quantity).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RequestedDeliveryDate).HasColumnName("RequestedDelivery_Date");
            entity.Property(e => e.UnitUuid)
                .HasMaxLength(50)
                .HasColumnName("Unit_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<BdCommunicationHistory>(entity =>
        {
            entity.HasKey(e => e.CommunicationHistoryId);

            entity.ToTable("BD_CommunicationHistory");

            entity.Property(e => e.CommunicationHistoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CommunicationHistory_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(50)
                .HasColumnName("Color_Code");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.HistoryTypeName)
                .HasMaxLength(50)
                .HasColumnName("History_Type_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDataRequired).HasColumnName("Is_Data_Required");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<BdReasearchCommunicationStatus>(entity =>
        {
            entity.HasKey(e => e.ReasearchCommunicationStatusId);

            entity.ToTable("BD_ReasearchCommunicationStatus");

            entity.Property(e => e.ReasearchCommunicationStatusId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ReasearchCommunicationStatus_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(50)
                .HasColumnName("Color_Code");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.HistoryTypeStatusName)
                .HasMaxLength(50)
                .HasColumnName("HistoryType_Status_Name");
            entity.Property(e => e.HistoryUuid)
                .HasMaxLength(50)
                .HasColumnName("History_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<BdResearchAudience>(entity =>
        {
            entity.HasKey(e => e.ResearchAudienceId).HasName("PK_Master_ResearchAudience");

            entity.ToTable("BD_ResearchAudience");

            entity.Property(e => e.ResearchAudienceId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ResearchAudience_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ResearchAudienceName)
                .HasMaxLength(50)
                .HasColumnName("ResearchAudience_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<BdResearchChannelType>(entity =>
        {
            entity.HasKey(e => e.ResearchChannelId).HasName("PK_Master_ResearchChannelType");

            entity.ToTable("BD_ResearchChannelType");

            entity.Property(e => e.ResearchChannelId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ResearchChannel_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(50)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ResearchChannelName)
                .HasMaxLength(50)
                .HasColumnName("ResearchChannel_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<BdResearchCommunicationMode>(entity =>
        {
            entity.HasKey(e => e.ResearchCommunicationModeId).HasName("PK_Master_ResearchCommunicationMode");

            entity.ToTable("BD_ResearchCommunicationMode");

            entity.Property(e => e.ResearchCommunicationModeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ResearchCommunicationMode_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ResearchCommunicationModeName)
                .HasMaxLength(50)
                .HasColumnName("ResearchCommunicationMode_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<CompanySetupEmailCredential>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CompanySetup_EmailCredential");

            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .HasColumnName("Email_Address");
            entity.Property(e => e.HostServiceProvider)
                .HasMaxLength(50)
                .HasColumnName("Host_ServiceProvider");
            entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Port).HasMaxLength(20);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Smtp)
                .HasMaxLength(30)
                .HasColumnName("SMTP");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<CompanySetupEmailTemplate>(entity =>
        {
            entity.ToTable("CompanySetup_EmailTemplate");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EmailCredentialUuid)
                .HasMaxLength(50)
                .HasColumnName("EmailCredential_UUID");
            entity.Property(e => e.EmailSubject)
                .HasMaxLength(50)
                .HasColumnName("Email_Subject");
            entity.Property(e => e.EmailTemplateName)
                .HasMaxLength(50)
                .HasColumnName("EmailTemplate_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Port).HasMaxLength(20);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErExpenseDataRange>(entity =>
        {
            entity.HasKey(e => e.ExpenseDataRangeId);

            entity.ToTable("ER_ExpenseDataRange");

            entity.Property(e => e.ExpenseDataRangeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseDataRange_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Days).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErExpenseSubType>(entity =>
        {
            entity.HasKey(e => e.ExpenseSubTypeId);

            entity.ToTable("ER_ExpenseSubType");

            entity.Property(e => e.ExpenseSubTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseSubType_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseApplicable)
                .HasMaxLength(50)
                .HasColumnName("Expense_Applicable");
            entity.Property(e => e.ExpenseSubTypeName)
                .HasMaxLength(50)
                .HasColumnName("ExpenseSubType_Name");
            entity.Property(e => e.ExpenseTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UnitTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("UnitType_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErExpenseSubUnit>(entity =>
        {
            entity.HasKey(e => e.ExpenseSubUnitId);

            entity.ToTable("ER_ExpenseSubUnit");

            entity.Property(e => e.ExpenseSubUnitId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseSubUnit_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseSubTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseSubType_UUID");
            entity.Property(e => e.ExpenseTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SubUnitTypeName)
                .HasMaxLength(50)
                .HasColumnName("Sub_Unit_Type_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErExpenseType>(entity =>
        {
            entity.HasKey(e => e.ExpenseTypeId);

            entity.ToTable("ER_ExpenseType");

            entity.Property(e => e.ExpenseTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseType_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseApplicable)
                .HasMaxLength(50)
                .HasColumnName("Expense_Applicable");
            entity.Property(e => e.ExpenseTypeName)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UnitTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("UnitType_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErExpenseUnit>(entity =>
        {
            entity.HasKey(e => e.ExpenseUnitId);

            entity.ToTable("ER_ExpenseUnit");

            entity.Property(e => e.ExpenseUnitId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseUnit_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .HasColumnName("Unit_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErHeadDesignation>(entity =>
        {
            entity.HasKey(e => e.HeadDesignationId);

            entity.ToTable("ER_HeadDesignation");

            entity.Property(e => e.HeadDesignationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("HeadDesignation_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DesignationName)
                .HasMaxLength(50)
                .HasColumnName("Designation_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErManageReportDesignation>(entity =>
        {
            entity.HasKey(e => e.ManageReportDesignationId);

            entity.ToTable("ER_ManageReportDesignation");

            entity.Property(e => e.ManageReportDesignationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ManageReportDesignation_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DesignationName)
                .HasMaxLength(50)
                .HasColumnName("Designation_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.NoOfErApproval)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("NoOfER_Approval");
            entity.Property(e => e.NoOfPafApproval)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("NoOfPAF_Approval");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErRemarkTemplate>(entity =>
        {
            entity.HasKey(e => e.RemarkTemplateId);

            entity.ToTable("ER_RemarkTemplate");

            entity.Property(e => e.RemarkTemplateId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RemarkTemplate_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseSubTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseSubType_UUID");
            entity.Property(e => e.ExpenseTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RemarkString)
                .HasColumnType("text")
                .HasColumnName("Remark_String");
            entity.Property(e => e.RemarkTagUuid)
                .HasMaxLength(50)
                .HasColumnName("RemarkTag_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErSubstituteExpense>(entity =>
        {
            entity.HasKey(e => e.SubstituteExpenseId);

            entity.ToTable("ER_SubstituteExpense");

            entity.Property(e => e.SubstituteExpenseId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("SubstituteExpense_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseTypePrimaryUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseTypePrimary_UUID");
            entity.Property(e => e.ExpenseTypeSecondaryUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseTypeSecondary_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErsetupCostCategory>(entity =>
        {
            entity.HasKey(e => e.CostCategoryId);

            entity.ToTable("ERSetup_CostCategory");

            entity.Property(e => e.CostCategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CostCategory_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CostCategoryName)
                .HasMaxLength(50)
                .HasColumnName("CostCategory_Name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("Designation_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErsetupCostCity>(entity =>
        {
            entity.HasKey(e => e.CostCityCategoryId);

            entity.ToTable("ERSetup_CostCity");

            entity.Property(e => e.CostCityCategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CostCityCategory_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CityUuid)
                .HasMaxLength(50)
                .HasColumnName("City_UUID");
            entity.Property(e => e.CostCityCategoryName)
                .HasMaxLength(50)
                .HasColumnName("Cost_CityCategory_Name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErsetupCurrency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId);

            entity.ToTable("ERSetup_Currency");

            entity.Property(e => e.CurrencyId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Currency_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .HasColumnName("Currency_Name");
            entity.Property(e => e.CurrencyShortName)
                .HasMaxLength(50)
                .HasColumnName("Currency_ShortName");
            entity.Property(e => e.CurrencySymbol)
                .HasMaxLength(50)
                .HasColumnName("Currency_Symbol");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErsetupExpenseEligibility>(entity =>
        {
            entity.HasKey(e => e.ExpenseEligibilityId);

            entity.ToTable("ERSetup_ExpenseEligibility");

            entity.Property(e => e.ExpenseEligibilityId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseEligibility_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.ExpenseAvailablity)
                .HasMaxLength(50)
                .HasColumnName("Expense_Availablity");
            entity.Property(e => e.ExpenseSubTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseSubType_UUID");
            entity.Property(e => e.ExpenseTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErsetupExpenseLimit>(entity =>
        {
            entity.HasKey(e => e.ExpenseLimitId);

            entity.ToTable("ERSetup_ExpenseLimit");

            entity.Property(e => e.ExpenseLimitId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ExpenseLimit_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CostCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("CostCategory_UUID");
            entity.Property(e => e.CostCityCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("CostCityCategory_UUID");
            entity.Property(e => e.CountOn)
                .HasMaxLength(50)
                .HasColumnName("Count_On");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseAllocationType)
                .HasMaxLength(50)
                .HasColumnName("Expense_Allocation_Type");
            entity.Property(e => e.ExpenseApplicableType)
                .HasMaxLength(50)
                .HasColumnName("Expense_Applicable_Type");
            entity.Property(e => e.ExpenseSubTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseSubType_UUID");
            entity.Property(e => e.ExpenseTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsRemakableMandatory).HasColumnName("IsRemakable_Mandatory");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LimitAmount)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Limit_Amount");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MaxAmount)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Max_Amount");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ErsetupWorkFlowInstance>(entity =>
        {
            entity.HasKey(e => e.WorkFlowInstanceId);

            entity.ToTable("ERSetup_WorkFlowInstance");

            entity.Property(e => e.WorkFlowInstanceId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("WorkFlowInstance_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.ExpenseSubTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseSubType_UUID");
            entity.Property(e => e.ExpenseTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("ExpenseType_UUID");
            entity.Property(e => e.InstanceName)
                .HasMaxLength(50)
                .HasColumnName("Instance_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.OriginatorDesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("OriginatorDesignation_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ReportDesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("ReportDesignation_UUID");
            entity.Property(e => e.ReportingSubstituteDesignation)
                .HasMaxLength(50)
                .HasColumnName("ReportingSubstitute_Designation");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.WorkFlowInstanceType)
                .HasMaxLength(50)
                .HasColumnName("WorkFlow_InstanceType");
        });

        modelBuilder.Entity<HraAttendenceReport>(entity =>
        {
            entity.ToTable("HRA_AttendenceReport");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.AttendenceType).HasMaxLength(50);
            entity.Property(e => e.DayType).HasMaxLength(15);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.Format).HasMaxLength(30);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraDocumentNamingMaster>(entity =>
        {
            entity.ToTable("HRA_DocumentNamingMaster");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(30)
                .HasColumnName("Document_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsValidityApplicable).HasColumnName("IsValidity_Applicable");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraDocumentType>(entity =>
        {
            entity.ToTable("HRA_DocumentType");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentTypeName)
                .HasMaxLength(50)
                .HasColumnName("DocumentType_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraEmployeeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HRA_EmployeeName");

            entity.ToTable("HRA_EmployeeType");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DurationType)
                .HasMaxLength(50)
                .HasColumnName("Duration_Type");
            entity.Property(e => e.EmployeeDuration)
                .HasMaxLength(50)
                .HasColumnName("Employee_Duration");
            entity.Property(e => e.EmployeeTypeName)
                .HasMaxLength(50)
                .HasColumnName("EmployeeType_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraLeaveTypeMaster>(entity =>
        {
            entity.ToTable("HRA_LeaveTypeMaster");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LeaveTypeName)
                .HasMaxLength(30)
                .HasColumnName("LeaveType_Name");
            entity.Property(e => e.LeaveTypeShortName)
                .HasMaxLength(30)
                .HasColumnName("LeaveType_ShortName");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraManageLeave>(entity =>
        {
            entity.ToTable("HRA_ManageLeave");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ActionTakenBy)
                .HasMaxLength(30)
                .HasColumnName("Action_TakenBy");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.AppliedBy)
                .HasMaxLength(20)
                .HasColumnName("Applied_By");
            entity.Property(e => e.AppliedDate).HasColumnName("Applied_Date");
            entity.Property(e => e.ContactNumber)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Contact_Number");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LeaveEndDate).HasColumnName("Leave_EndDate");
            entity.Property(e => e.LeaveStartDate).HasColumnName("Leave_StartDate");
            entity.Property(e => e.LeaveTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("LeaveType_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Reason).HasColumnType("text");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraNominee>(entity =>
        {
            entity.HasKey(e => e.NomineeId);

            entity.ToTable("HRA_Nominee");

            entity.Property(e => e.NomineeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Nominee_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.NomineeName)
                .HasMaxLength(50)
                .HasColumnName("Nominee_Name");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraPaySlipCategory>(entity =>
        {
            entity.HasKey(e => e.PaySlipCategoryId);

            entity.ToTable("HRA_PaySlipCategory");

            entity.Property(e => e.PaySlipCategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PaySlipCategory_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.PayslipCategory)
                .HasMaxLength(50)
                .HasColumnName("Payslip_Category");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraProfessionalTax>(entity =>
        {
            entity.HasKey(e => e.ProfessionalTaxId);

            entity.ToTable("HRA_ProfessionalTax");

            entity.Property(e => e.ProfessionalTaxId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ProfessionalTax_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MonthlyIncomeFrom)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Monthly_Income_From");
            entity.Property(e => e.MonthlyIncomeTo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Monthly_Income_To");
            entity.Property(e => e.ProfessionalTaxName)
                .HasMaxLength(50)
                .HasColumnName("ProfessionalTax_Name");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Rupees).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StateUuid)
                .HasMaxLength(50)
                .HasColumnName("State_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraShift>(entity =>
        {
            entity.HasKey(e => e.ShiftId);

            entity.ToTable("HRA_Shift");

            entity.Property(e => e.ShiftId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Shift_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EndTime).HasColumnName("End_Time");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LunchTime)
                .HasMaxLength(50)
                .HasColumnName("Lunch_Time");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ShiftName)
                .HasMaxLength(50)
                .HasColumnName("Shift_Name");
            entity.Property(e => e.ShiftPrefix)
                .HasMaxLength(50)
                .HasColumnName("Shift_Prefix");
            entity.Property(e => e.StartTime).HasColumnName("Start_Time");
            entity.Property(e => e.TotalWorkingHours)
                .HasMaxLength(50)
                .HasColumnName("TotalWorking_Hours");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraTaxRegime>(entity =>
        {
            entity.HasKey(e => e.TaxRegimeId);

            entity.ToTable("HRA_TaxRegime");

            entity.Property(e => e.TaxRegimeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TaxRegime_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TaxRegime)
                .HasMaxLength(50)
                .HasColumnName("Tax_Regime");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraWeekDay>(entity =>
        {
            entity.HasKey(e => e.WeekDaysId);

            entity.ToTable("HRA_WeekDay");

            entity.Property(e => e.WeekDaysId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("WeekDays_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.WeekDayName)
                .HasMaxLength(50)
                .HasColumnName("WeekDay_Name");
        });

        modelBuilder.Entity<HraemployeeLeaveAuthorization>(entity =>
        {
            entity.HasKey(e => e.LeaveAuthorizationId);

            entity.ToTable("HRAEmployee_LeaveAuthorization");

            entity.Property(e => e.LeaveAuthorizationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("LeaveAuthorization_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Company_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LeaveTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("LeaveType_UUID");
            entity.Property(e => e.MasterCompanyBranchUuid)
                .HasMaxLength(50)
                .HasColumnName("MasterCompanyBranch_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.NumberOfAllowLeave)
                .HasMaxLength(50)
                .HasColumnName("NumberOfAllow_leave");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_Date");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraemployeeSalarySalaryParameter>(entity =>
        {
            entity.HasKey(e => e.ParameterId);

            entity.ToTable("HRAEmployeeSalary_SalaryParameter");

            entity.Property(e => e.ParameterId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Parameter_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Advance).HasMaxLength(50);
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Company_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.Incentive).HasMaxLength(50);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Loan).HasMaxLength(50);
            entity.Property(e => e.MasterCompanyBranchUuid)
                .HasMaxLength(50)
                .HasColumnName("MasterCompanyBranch_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MiscRecovery)
                .HasMaxLength(50)
                .HasColumnName("Misc_Recovery");
            entity.Property(e => e.Month).HasMaxLength(50);
            entity.Property(e => e.PayslipCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("PayslipCategory_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraemployeeSalarySalaryPaySlip>(entity =>
        {
            entity.HasKey(e => e.PaySlipId);

            entity.ToTable("HRAEmployeeSalary_SalaryPaySlip");

            entity.Property(e => e.PaySlipId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PaySlip_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Company_UUID");
            entity.Property(e => e.Date).HasMaxLength(50);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyBranchUuid)
                .HasMaxLength(50)
                .HasColumnName("MasterCompanyBranch_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.PayslipCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("PayslipCategory_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraemployeeWeekOff>(entity =>
        {
            entity.HasKey(e => e.WeekOffId);

            entity.ToTable("HRAEmployee_WeekOff");

            entity.Property(e => e.WeekOffId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("WeekOff_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Company_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyBranchUuid)
                .HasMaxLength(50)
                .HasColumnName("MasterCompanyBranch_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.WeekDayUuid)
                .HasMaxLength(50)
                .HasColumnName("WeekDay_UUID");
        });

        modelBuilder.Entity<HraincomeIncomeTax>(entity =>
        {
            entity.HasKey(e => e.IncomeMasterId);

            entity.ToTable("HRAIncome_IncomeTax");

            entity.Property(e => e.IncomeMasterId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("IncomeMaster_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.GenderUuid)
                .HasMaxLength(50)
                .HasColumnName("Gender_UUID");
            entity.Property(e => e.IncomeTaxName)
                .HasMaxLength(50)
                .HasColumnName("IncomeTax_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Percentage).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TaxRegimeUuid)
                .HasMaxLength(50)
                .HasColumnName("TaxRegime_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.YearlyIncomeFrom)
                .HasMaxLength(50)
                .HasColumnName("YearlyIncome_From");
            entity.Property(e => e.YearlyIncomeTo)
                .HasMaxLength(50)
                .HasColumnName("YearlyIncome_To");
        });

        modelBuilder.Entity<HraincomeItdeduction>(entity =>
        {
            entity.HasKey(e => e.ItdeductionId).HasName("PK_HRAIncome_ITSubDeduction");

            entity.ToTable("HRAIncome_ITDeduction");

            entity.Property(e => e.ItdeductionId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ITDeduction_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.FromDeduction)
                .HasMaxLength(50)
                .HasColumnName("From_Deduction");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItdeductionName)
                .HasMaxLength(50)
                .HasColumnName("ITDeduction_Name");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SerialNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Serial_No");
            entity.Property(e => e.ToDeduction)
                .HasMaxLength(50)
                .HasColumnName("To_Deduction");
            entity.Property(e => e.ToSeniorDeduction)
                .HasMaxLength(50)
                .HasColumnName("ToSenior_Deduction");
            entity.Property(e => e.ToSuperSeniorDeduction)
                .HasMaxLength(50)
                .HasColumnName("ToSuperSenior_Deduction");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<HraincomeItemployeeParameter>(entity =>
        {
            entity.HasKey(e => e.ItemployeeParameterId);

            entity.ToTable("HRAIncome_ITEmployeeParameter");

            entity.Property(e => e.ItemployeeParameterId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ITEmployeeParameter_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CompanyBranchUuid)
                .HasMaxLength(50)
                .HasColumnName("CompanyBranch_UUID");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Company_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.FinancialYearUuid)
                .HasMaxLength(50)
                .HasColumnName("FinancialYear_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.Value).HasColumnType("numeric(18, 0)");
        });

        modelBuilder.Entity<IncomeItsubDeduction>(entity =>
        {
            entity.HasKey(e => e.ItsubDeductionId);

            entity.ToTable("Income_ITSubDeduction");

            entity.Property(e => e.ItsubDeductionId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ITSubDeduction_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.FromDeduction)
                .HasMaxLength(50)
                .HasColumnName("From_Deduction");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItdeductionUuid)
                .HasMaxLength(50)
                .HasColumnName("ITDeduction_UUID");
            entity.Property(e => e.ItsubDeductionName)
                .HasMaxLength(50)
                .HasColumnName("ITSubDeduction_Name");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SerialNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Serial_No");
            entity.Property(e => e.ToDeduction)
                .HasMaxLength(50)
                .HasColumnName("To_Deduction");
            entity.Property(e => e.ToSeniorDeduction)
                .HasMaxLength(50)
                .HasColumnName("ToSenior_Deduction");
            entity.Property(e => e.ToSuperSeniorDeduction)
                .HasMaxLength(50)
                .HasColumnName("ToSuperSenior_Deduction");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MaAuthAc>(entity =>
        {
            entity.ToTable("MA_AuthAC");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.TknExpiryEnd)
                .HasColumnType("datetime")
                .HasColumnName("Tkn_Expiry_End");
            entity.Property(e => e.TknExpiryStart)
                .HasColumnType("datetime")
                .HasColumnName("Tkn_Expiry_Start");
            entity.Property(e => e.Token).HasColumnType("text");
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Uuid).HasMaxLength(40);
        });

        modelBuilder.Entity<MasterAsset>(entity =>
        {
            entity.HasKey(e => e.AssetId);

            entity.ToTable("Master_Asset");

            entity.Property(e => e.AssetId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Asset_id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.AssetName)
                .HasMaxLength(50)
                .HasColumnName("Asset_name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterBankActype>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PK__master_B__0D44FA1C21B6055D");

            entity.ToTable("Master_BankACtype");

            entity.Property(e => e.BankId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Bank_id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BankAccontType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bank_AccontType");
            entity.Property(e => e.BankAccountStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bank_Account_Status");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterBanktype>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PK__master_B__0D44FA1C03317E3D");

            entity.ToTable("Master_Banktype");

            entity.Property(e => e.BankId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Bank_id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .HasColumnName("Bank_name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterBloodGroup>(entity =>
        {
            entity.HasKey(e => e.BloodGroupId).HasName("PK_master_BloodGroup");

            entity.ToTable("Master_BloodGroup");

            entity.Property(e => e.BloodGroupId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BloodGroup_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BloodGroupName)
                .HasMaxLength(50)
                .HasColumnName("BloodGroup_name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterCategorTag>(entity =>
        {
            entity.HasKey(e => e.CategoryTagId);

            entity.ToTable("Master_CategorTag");

            entity.Property(e => e.CategoryTagId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CategoryTag_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CategoryTagName)
                .HasMaxLength(50)
                .HasColumnName("Category_Tag_Name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("Master_Category");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Category_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterCity>(entity =>
        {
            entity.ToTable("Master_City");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(50)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .HasColumnName("City_Name");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(50)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RecordsNo).HasColumnName("Records_No");
            entity.Property(e => e.StateUuid)
                .HasMaxLength(50)
                .HasColumnName("State_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(50)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__MasterCo__2D971CACF820348A");

            entity.ToTable("MasterCompany");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Address1).HasMaxLength(100);
            entity.Property(e => e.Address2).HasMaxLength(100);
            entity.Property(e => e.AlternateMobileNumber)
                .HasMaxLength(50)
                .HasColumnName("Alternate_Mobile_Number");
            entity.Property(e => e.AlternatePhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Alternate_PhoneNumber");
            entity.Property(e => e.CityNameUuid)
                .HasMaxLength(50)
                .HasColumnName("CityName_UUID");
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.CompanyShortName)
                .HasMaxLength(50)
                .HasColumnName("Company_ShortName");
            entity.Property(e => e.ContactPersonNameSales)
                .HasMaxLength(50)
                .HasColumnName("ContactPersonName_Sales");
            entity.Property(e => e.ContactPersonNameSupport)
                .HasMaxLength(50)
                .HasColumnName("ContactPersonName_Support");
            entity.Property(e => e.CountryNameUuid)
                .HasMaxLength(50)
                .HasColumnName("CountryName_UUID");
            entity.Property(e => e.DateOfEstablishment).HasColumnName("DateOf_Establishment");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmailIdPersonal)
                .HasMaxLength(50)
                .HasColumnName("Email_Id_Personal");
            entity.Property(e => e.EmailIdSales)
                .HasMaxLength(50)
                .HasColumnName("Email_Id_Sales");
            entity.Property(e => e.EmailIdSupport)
                .HasMaxLength(50)
                .HasColumnName("Email_Id_Support");
            entity.Property(e => e.GstinNumber)
                .HasMaxLength(50)
                .HasColumnName("GSTIN_Number");
            entity.Property(e => e.IsAdddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Logo).HasMaxLength(80);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(50)
                .HasColumnName("Mobile_Number");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Signature).HasMaxLength(80);
            entity.Property(e => e.Stamp).HasMaxLength(80);
            entity.Property(e => e.StateNameUuid)
                .HasMaxLength(50)
                .HasColumnName("StateName_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Url1)
                .HasMaxLength(70)
                .HasColumnName("URL1");
            entity.Property(e => e.Url2)
                .HasMaxLength(70)
                .HasColumnName("URL2");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterCompanyBranch>(entity =>
        {
            entity.HasKey(e => e.CompanyBranchId).HasName("PK_master_CompanyBranch");

            entity.ToTable("Master_CompanyBranch");

            entity.Property(e => e.CompanyBranchId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CompanyBranch_id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BranchAddress)
                .HasMaxLength(50)
                .HasColumnName("Branch_Address");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .HasColumnName("Branch_Name");
            entity.Property(e => e.CityUuid)
                .HasMaxLength(50)
                .HasColumnName("City_UUID");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Company_UUID");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StateUuid)
                .HasMaxLength(50)
                .HasColumnName("State_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterCompanyType>(entity =>
        {
            entity.HasKey(e => e.CompanyTypeId).HasName("PK_master_CompanyType");

            entity.ToTable("Master_CompanyType");

            entity.Property(e => e.CompanyTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CompanyType_id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CompanyType)
                .HasMaxLength(50)
                .HasColumnName("Company_Type");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__MasterCo__10D1609FA4BFED47");

            entity.ToTable("MasterCountry");

            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.CountryShortName).HasMaxLength(50);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);

            entity.ToTable("Master_Department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Department_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("Department_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterDesignation>(entity =>
        {
            entity.HasKey(e => e.DesignationId);

            entity.ToTable("Master_Designation");

            entity.Property(e => e.DesignationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Designation_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DesignationName)
                .HasMaxLength(50)
                .HasColumnName("Designation_Name");
            entity.Property(e => e.DesignationShortName)
                .HasMaxLength(20)
                .HasColumnName("Designation_ShortName");
            entity.Property(e => e.IsAdddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterDocumentCategory>(entity =>
        {
            entity.HasKey(e => e.DocumentId);

            entity.ToTable("Master_DocumentCategory");

            entity.Property(e => e.DocumentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Document_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentCategoryName)
                .HasMaxLength(50)
                .HasColumnName("DocumentCategory_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterDocumentCategoryTag>(entity =>
        {
            entity.HasKey(e => e.DocumentCategoryId);

            entity.ToTable("Master_DocumentCategoryTag");

            entity.Property(e => e.DocumentCategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("DocumentCategory_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentCategoryTag).HasMaxLength(50);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterDocumentGrop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__master_D__3213E83F1DE57479");

            entity.ToTable("Master_DocumentGrop");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(100)
                .HasColumnName("Document_Name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterEmployee>(entity =>
        {
            entity.ToTable("Master_Employee");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.AltMobile).HasMaxLength(50);
            entity.Property(e => e.BuildingNo).HasMaxLength(30);
            entity.Property(e => e.CompanyContactNo).HasMaxLength(70);
            entity.Property(e => e.CompanyEmail).HasMaxLength(70);
            entity.Property(e => e.CurrentBuildingNo).HasMaxLength(30);
            entity.Property(e => e.CurrentLandmark).HasMaxLength(50);
            entity.Property(e => e.CurrentMasterCityName)
                .HasMaxLength(50)
                .HasColumnName("CurrentMaster_City_Name");
            entity.Property(e => e.CurrentMasterCityUuid)
                .HasMaxLength(50)
                .HasColumnName("CurrentMaster_City_UUID");
            entity.Property(e => e.CurrentMasterCountryName)
                .HasMaxLength(50)
                .HasColumnName("CurrentMaster_Country_Name");
            entity.Property(e => e.CurrentMasterCountryUuid)
                .HasMaxLength(50)
                .HasColumnName("CurrentMaster_Country_UUID");
            entity.Property(e => e.CurrentMasterStateName)
                .HasMaxLength(50)
                .HasColumnName("CurrentMaster_State_Name");
            entity.Property(e => e.CurrentMasterStateUuid)
                .HasMaxLength(50)
                .HasColumnName("CurrentMaster_State_UUID");
            entity.Property(e => e.CurrentPostCode).HasMaxLength(15);
            entity.Property(e => e.CurrentStreetAddress).HasMaxLength(70);
            entity.Property(e => e.CurrentStreetAddress2).HasMaxLength(70);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeCode).HasMaxLength(30);
            entity.Property(e => e.ExpLimitDesignation).HasMaxLength(50);
            entity.Property(e => e.ExpWorkflowDesignation).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Landline).HasMaxLength(50);
            entity.Property(e => e.Landmark).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MasterBloodGroupUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_BloodGroup_UUID");
            entity.Property(e => e.MasterCityName)
                .HasMaxLength(50)
                .HasColumnName("Master_City_Name");
            entity.Property(e => e.MasterCityUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_City_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterCountryName)
                .HasMaxLength(50)
                .HasColumnName("Master_Country_Name");
            entity.Property(e => e.MasterCountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Country_UUID");
            entity.Property(e => e.MasterDepartmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Department_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MasterGenderUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Gender_UUID");
            entity.Property(e => e.MasterPrefixUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Prefix_UUID");
            entity.Property(e => e.MasterRolesUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Roles_UUID");
            entity.Property(e => e.MasterStateName)
                .HasMaxLength(50)
                .HasColumnName("Master_State_Name");
            entity.Property(e => e.MasterStateUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_State_UUID");
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PersonalEmail).HasMaxLength(70);
            entity.Property(e => e.PostCode).HasMaxLength(15);
            entity.Property(e => e.ProfilePic).HasMaxLength(80);
            entity.Property(e => e.ReportingDesignation).HasMaxLength(50);
            entity.Property(e => e.StreetAddress).HasMaxLength(70);
            entity.Property(e => e.StreetAddress2).HasMaxLength(70);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterEmployeeDocumentSetup>(entity =>
        {
            entity.ToTable("Master_Employee_DocumentSetup");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentFile)
                .HasMaxLength(80)
                .HasColumnName("Document_File");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(50)
                .HasColumnName("Document_Name");
            entity.Property(e => e.DocumentTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("Document_Type_UUID");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterEmployeeHrSetup>(entity =>
        {
            entity.ToTable("Master_Employee_HR_Setup");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BpCode)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BP_Code");
            entity.Property(e => e.DateOfJoining).HasColumnName("Date_of_Joining");
            entity.Property(e => e.DateOfReliving).HasColumnName("Date_of_Reliving");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.EmployeementTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employeement_Type_UUID");
            entity.Property(e => e.EsicNo)
                .HasMaxLength(50)
                .HasColumnName("ESIC_No");
            entity.Property(e => e.HrDesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("Hr_Designation_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsEmployeeRelived).HasColumnName("Is_Employee_Relived");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LunchEndTime).HasColumnName("Lunch_End_Time");
            entity.Property(e => e.LunchStartTime).HasColumnName("Lunch_Start_Time");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.PayslipCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("Payslip_Category_UUID");
            entity.Property(e => e.PfNo)
                .HasMaxLength(50)
                .HasColumnName("PF_No");
            entity.Property(e => e.ReasonOfLeave)
                .HasMaxLength(50)
                .HasColumnName("Reason_of_Leave");
            entity.Property(e => e.Shift).HasMaxLength(50);
            entity.Property(e => e.ShiftEndTime).HasColumnName("Shift_End_Time");
            entity.Property(e => e.ShiftStartTime).HasColumnName("Shift_Start_Time");
            entity.Property(e => e.TaxRegime)
                .HasMaxLength(50)
                .HasColumnName("Tax_Regime");
            entity.Property(e => e.UanNo)
                .HasMaxLength(50)
                .HasColumnName("UAN_No");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterEmployeeLeaveAuthorisation>(entity =>
        {
            entity.ToTable("Master_Employee_leaveAuthorisation");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BranchNameUuid)
                .HasMaxLength(50)
                .HasColumnName("Branch_Name_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LeaveTypeUuid)
                .HasMaxLength(50)
                .HasColumnName("Leave_Type_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.NoOfLeaveAllowed)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("No_Of_Leave_Allowed");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterEnvironment>(entity =>
        {
            entity.HasKey(e => e.EnvironmentId).HasName("PK__MasterEn__4B909A498AFD29BC");

            entity.ToTable("MasterEnvironment");

            entity.Property(e => e.EnvironmentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EnvironmentName).HasMaxLength(255);
            entity.Property(e => e.IsAdddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__master_G__3213E83F0EA330E9");

            entity.ToTable("Master_Gender");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.GenderName)
                .HasMaxLength(100)
                .HasColumnName("Gender_name");
            entity.Property(e => e.GenderSymbol)
                .HasMaxLength(50)
                .HasColumnName("Gender_Symbol");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterHonorific>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__master_H__3213E83F0AD2A005");

            entity.ToTable("Master_Honorific");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.HonorificName)
                .HasMaxLength(50)
                .HasColumnName("Honorific_name");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterIndustry>(entity =>
        {
            entity.HasKey(e => e.IndustryId);

            entity.ToTable("Master_Industry");

            entity.Property(e => e.IndustryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Industry_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IndustrySector)
                .HasMaxLength(50)
                .HasColumnName("Industry_Sector");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterManageBankDetail>(entity =>
        {
            entity.ToTable("Master_ManageBankDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AccountUuid)
                .HasMaxLength(50)
                .HasColumnName("Account_UUID");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BankAcNumber)
                .HasMaxLength(15)
                .HasColumnName("BankAC_Number");
            entity.Property(e => e.BankUuid)
                .HasMaxLength(50)
                .HasColumnName("Bank_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.IfscCode)
                .HasMaxLength(15)
                .HasColumnName("IFSC_Code");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.OpeningDate).HasColumnName("Opening_Date");
            entity.Property(e => e.PrintedNameOnPassbook)
                .HasMaxLength(30)
                .HasColumnName("Printed_NameOn_Passbook");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterManageDocument>(entity =>
        {
            entity.ToTable("Master_ManageDocument");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DocumentTitile)
                .HasMaxLength(20)
                .HasColumnName("Document_Titile");
            entity.Property(e => e.DocumentUpload)
                .HasMaxLength(50)
                .HasColumnName("Document_Upload");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LibraryCategoryTagUuid)
                .HasMaxLength(50)
                .HasColumnName("LibraryCategoryTag_UUID");
            entity.Property(e => e.LibraryCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("LibraryCategory_UUID");
            entity.Property(e => e.LibraryDocumentCategoryTagUuid)
                .HasMaxLength(50)
                .HasColumnName("LibraryDocumentCategoryTag_UUID");
            entity.Property(e => e.LibraryDocumentCategoryUuid)
                .HasMaxLength(50)
                .HasColumnName("LibraryDocumentCategory_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.NextReminderDate).HasColumnName("NextReminder_Date");
            entity.Property(e => e.NextRenewableDate).HasColumnName("Next_RenewableDate");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RemindMeBeforeDays)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RemindMeBefore_Days");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.YearUuid)
                .HasMaxLength(50)
                .HasColumnName("Year_UUID");
        });

        modelBuilder.Entity<MasterMarital>(entity =>
        {
            entity.HasKey(e => e.MaritalId).HasName("PK_master_Marital");

            entity.ToTable("Master_Marital");

            entity.Property(e => e.MaritalId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Marital_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("MasterMenu");

            entity.Property(e => e.MenuId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAdddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MainParentUuid)
                .HasMaxLength(50)
                .HasColumnName("MainParentUUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MenuIcon).HasMaxLength(50);
            entity.Property(e => e.MenuLevel).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.MenuName).HasMaxLength(60);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Sequence).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SubParentUuid)
                .HasMaxLength(50)
                .HasColumnName("SubParentUUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Url).HasMaxLength(255);
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterNationality>(entity =>
        {
            entity.HasKey(e => e.NationalityId).HasName("PK_master_Nationality");

            entity.ToTable("Master_Nationality");

            entity.Property(e => e.NationalityId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Nationality_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.NationalityName)
                .HasMaxLength(50)
                .HasColumnName("Nationality_Name");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterService>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.ToTable("Master_Service");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Service_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .HasColumnName("Service_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterState>(entity =>
        {
            entity.ToTable("Master_State");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .HasColumnName("State_Name");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<MasterUserMenuRight>(entity =>
        {
            entity.HasKey(e => e.UserMenuRightsId).HasName("PK__MasterUs__6A7A04FBD46ADE3E");

            entity.ToTable("MasterUserMenuRight");

            entity.Property(e => e.UserMenuRightsId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CompanyUuid).HasMaxLength(50);
            entity.Property(e => e.EnvironmentUuid).HasMaxLength(50);
            entity.Property(e => e.IsAdddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MenuUuid).HasMaxLength(50);
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UserRoleUuid).HasMaxLength(50);
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterUserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__MasterUs__3D978A3530C4C2D8");

            entity.ToTable("MasterUserRole");

            entity.Property(e => e.UserRoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAdddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.UserRoleName).HasMaxLength(50);
            entity.Property(e => e.Uuid).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterYear>(entity =>
        {
            entity.HasKey(e => e.YearId).HasName("PK__master_Y__A14C9DC07F60ED59");

            entity.ToTable("Master_Year");

            entity.Property(e => e.YearId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Year_id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeleteOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdateBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid).HasMaxLength(50);
            entity.Property(e => e.YearName)
                .HasMaxLength(50)
                .HasColumnName("Year_name");
        });

        modelBuilder.Entity<ProjectCreateProject>(entity =>
        {
            entity.ToTable("Project_CreateProject");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CustomerUuid)
                .HasMaxLength(50)
                .HasColumnName("Customer_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.ExpectedTotalHours)
                .HasMaxLength(50)
                .HasColumnName("Expected_Total_Hours");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectCost)
                .HasMaxLength(50)
                .HasColumnName("Project_Cost");
            entity.Property(e => e.ProjectDescription)
                .HasColumnType("text")
                .HasColumnName("Project_Description");
            entity.Property(e => e.ProjectTitle)
                .HasMaxLength(50)
                .HasColumnName("Project_Title");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectCreateProjectPhase>(entity =>
        {
            entity.ToTable("Project_CreateProjectPhase");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.InvoiceUuid)
                .HasMaxLength(50)
                .HasColumnName("Invoice_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsProjectPhaseLinkedWithInvoice).HasColumnName("Is_Project_Phase_Linked_with_Invoice");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.PhaseCost)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Phase_Cost");
            entity.Property(e => e.PhaseExpectedHours)
                .HasMaxLength(50)
                .HasColumnName("Phase_Expected_Hours");
            entity.Property(e => e.PhaseName)
                .HasMaxLength(50)
                .HasColumnName("Phase_Name");
            entity.Property(e => e.PhaseProgress).HasMaxLength(50);
            entity.Property(e => e.ProjectDescription)
                .HasColumnType("text")
                .HasColumnName("Project_Description");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.SelectTeamMember)
                .HasMaxLength(50)
                .HasColumnName("Select_Team_Member");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectCreateProjectStep>(entity =>
        {
            entity.ToTable("Project_CreateProjectStep");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectPhaseUuid)
                .HasMaxLength(50)
                .HasColumnName("ProjectPhase_UUID");
            entity.Property(e => e.ProjectStepName)
                .HasMaxLength(50)
                .HasColumnName("Project_Step_Name");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SelectTeamMember)
                .HasMaxLength(50)
                .HasColumnName("Select_Team_Member");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.StepCost)
                .HasMaxLength(50)
                .HasColumnName("Step_Cost");
            entity.Property(e => e.TotalHours)
                .HasMaxLength(50)
                .HasColumnName("Total_Hours");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectManageResourceCosting>(entity =>
        {
            entity.ToTable("Project_ManageResourceCosting");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.Hours).HasMaxLength(50);
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.Month).HasMaxLength(50);
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.Rate).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Total).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectManageTaskTimeLine>(entity =>
        {
            entity.ToTable("Project_ManageTaskTimeLine");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectPhaseUuid)
                .HasMaxLength(50)
                .HasColumnName("ProjectPhase_UUID");
            entity.Property(e => e.ProjectTaskUuid)
                .HasMaxLength(50)
                .HasColumnName("ProjectTask_UUID");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartDateEndDate).HasColumnName("StartDate_EndDate");
            entity.Property(e => e.TaskTimeLineTitle)
                .HasMaxLength(50)
                .HasColumnName("Task_Time_Line_Title");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectCrendential>(entity =>
        {
            entity.ToTable("Project_ProjectCrendentials");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CredentialsDetails)
                .HasColumnType("text")
                .HasColumnName("Credentials_Details");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectDocument>(entity =>
        {
            entity.ToTable("Project_ProjectDocument");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectDocument)
                .HasMaxLength(80)
                .HasColumnName("Project_Document");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectMeeting>(entity =>
        {
            entity.ToTable("Project_ProjectMeeting");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.AttendeesUuid)
                .HasMaxLength(50)
                .HasColumnName("Attendees_UUID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("Company_Name");
            entity.Property(e => e.ContactPersonName)
                .HasMaxLength(50)
                .HasColumnName("Contact_Person_Name");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IndustrySectorUuid)
                .HasMaxLength(50)
                .HasColumnName("IndustrySector_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MeetingDate).HasColumnName("Meeting_Date");
            entity.Property(e => e.MeetingDocument)
                .HasMaxLength(80)
                .HasColumnName("Meeting_Document");
            entity.Property(e => e.MeetingTime).HasColumnName("Meeting_Time");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectMom>(entity =>
        {
            entity.ToTable("Project_ProjectMOM");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.AttendeesFromOurCompanyName)
                .HasMaxLength(256)
                .HasColumnName("Attendees_from_Our_Company_Name");
            entity.Property(e => e.AttendeesFromOurCompanyUuid)
                .HasMaxLength(256)
                .HasColumnName("Attendees_from_Our_Company_UUID");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAnyFurtherMeetingSchedule).HasColumnName("Is_Any_Further_Meeting_Schedule");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MeetingAjenda)
                .HasColumnType("text")
                .HasColumnName("Meeting_Ajenda");
            entity.Property(e => e.MeetingAttendeesFromClientSide)
                .HasMaxLength(50)
                .HasColumnName("Meeting_Attendees_from_Client_Side");
            entity.Property(e => e.MeetingDate).HasColumnName("Meeting_Date");
            entity.Property(e => e.MeetingDocument)
                .HasMaxLength(80)
                .HasColumnName("Meeting_Document");
            entity.Property(e => e.MeetingTime).HasColumnName("Meeting_Time");
            entity.Property(e => e.MeetingType)
                .HasMaxLength(50)
                .HasColumnName("Meeting_Type");
            entity.Property(e => e.NextMeetingDate).HasColumnName("Next_Meeting_Date");
            entity.Property(e => e.NextMeetingTime).HasColumnName("Next_Meeting_Time");
            entity.Property(e => e.NextMeetingType)
                .HasMaxLength(50)
                .HasColumnName("Next_Meeting_Type");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectResource>(entity =>
        {
            entity.ToTable("Project_ProjectResources");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("Designation_UUID");
            entity.Property(e => e.EmployeeUuid)
                .HasMaxLength(50)
                .HasColumnName("Employee_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.Rate).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectStatus>(entity =>
        {
            entity.ToTable("Project_ProjectStatus");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StatusTitle)
                .HasMaxLength(50)
                .HasColumnName("Status_Title");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ProjectProjectTask>(entity =>
        {
            entity.ToTable("Project_ProjectTask");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.ExpectedTaskHours)
                .HasMaxLength(50)
                .HasColumnName("Expected_Task_Hours");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ProjectPhaseUuid)
                .HasMaxLength(50)
                .HasColumnName("ProjectPhase_UUID");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(50)
                .HasColumnName("Project_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.TaskTitle)
                .HasMaxLength(50)
                .HasColumnName("Task_Title");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ResearchManageFilterDatum>(entity =>
        {
            entity.HasKey(e => e.ManageFilterDataId).HasName("PK_Reasearch_ManageFilterData");

            entity.ToTable("Research_ManageFilterData");

            entity.Property(e => e.ManageFilterDataId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ManageFilterData_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CommunicationHistoryUuid)
                .HasMaxLength(50)
                .HasColumnName("CommunicationHistory_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ReasearchCommunicationStatusUuid)
                .HasMaxLength(50)
                .HasColumnName("ReasearchCommunicationStatus_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Remark).HasColumnType("text");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.UploadFiles)
                .HasMaxLength(200)
                .HasColumnName("Upload_Files");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ResearchManageResearchGroup>(entity =>
        {
            entity.HasKey(e => e.ManageResearchGroupId);

            entity.ToTable("Research_ManageResearchGroup");

            entity.Property(e => e.ManageResearchGroupId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ManageResearchGroup_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.BdTeamMember).HasColumnName("BD_TeamMember");
            entity.Property(e => e.BddesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("BDDesignation_UUID");
            entity.Property(e => e.CityUuid)
                .HasMaxLength(50)
                .HasColumnName("City_UUID");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.DataFilterationTeammember).HasColumnName("DataFilteration_Teammember");
            entity.Property(e => e.DataMiningTeamMember).HasColumnName("DataMining_TeamMember");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.DesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("Designation_UUID");
            entity.Property(e => e.FilterationDatacount)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Filteration_Datacount");
            entity.Property(e => e.FilterationDesignationUuid)
                .HasMaxLength(50)
                .HasColumnName("FilterationDesignation_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ResearchAudienceUuid)
                .HasMaxLength(50)
                .HasColumnName("ResearchAudience_UUID");
            entity.Property(e => e.ResearchChannelUuid)
                .HasMaxLength(50)
                .HasColumnName("ResearchChannel_UUID");
            entity.Property(e => e.ResearchDataCount)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ResearchData_Count");
            entity.Property(e => e.ResearchGroupName)
                .HasMaxLength(50)
                .HasColumnName("ResearchGroup_Name");
            entity.Property(e => e.StateUuid)
                .HasMaxLength(50)
                .HasColumnName("State_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        modelBuilder.Entity<ResearchUploadResearchDatum>(entity =>
        {
            entity.HasKey(e => e.UploadResearchDataId);

            entity.ToTable("Research_UploadResearchData");

            entity.Property(e => e.UploadResearchDataId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UploadResearchData_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.AlternateEmail)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Alternate_Email");
            entity.Property(e => e.AlternateMobileNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Alternate_MobileNo");
            entity.Property(e => e.CityUuid)
                .HasMaxLength(50)
                .HasColumnName("City_UUID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("Company_Name");
            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .HasColumnName("Contact_Name");
            entity.Property(e => e.CountryUuid)
                .HasMaxLength(50)
                .HasColumnName("Country_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.EmailId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Email_Id");
            entity.Property(e => e.GstNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("GST_No");
            entity.Property(e => e.IndustryUuid)
                .HasMaxLength(50)
                .HasColumnName("Industry_UUID");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LandLineNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("LandLine_No");
            entity.Property(e => e.ManageResearchGroupUuid)
                .HasMaxLength(50)
                .HasColumnName("ManageResearchGroup_UUID");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.MobileNo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Mobile_No");
            entity.Property(e => e.PinCode).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ResearchAudienceUuid)
                .HasMaxLength(50)
                .HasColumnName("ResearchAudience_UUID");
            entity.Property(e => e.ResearchChannelUuid)
                .HasMaxLength(50)
                .HasColumnName("ResearchChannel_UUID");
            entity.Property(e => e.StateUuid)
                .HasMaxLength(50)
                .HasColumnName("State_UUID");
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.UploadDataFiles)
                .HasMaxLength(200)
                .HasColumnName("Upload_DataFiles");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
            entity.Property(e => e.Website).HasMaxLength(100);
        });

        modelBuilder.Entity<SalesManagePositiveLead>(entity =>
        {
            entity.HasKey(e => e.ManagePositiveLeadId);

            entity.ToTable("Sales_ManagePositiveLead");

            entity.Property(e => e.ManagePositiveLeadId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ManagePositiveLead_Id");
            entity.Property(e => e.AddedIp)
                .HasMaxLength(30)
                .HasColumnName("AddedIP");
            entity.Property(e => e.CommunicationHistoryUuid)
                .HasMaxLength(50)
                .HasColumnName("CommunicationHistory_UUID");
            entity.Property(e => e.DeletedIp)
                .HasMaxLength(30)
                .HasColumnName("DeletedIP");
            entity.Property(e => e.IsAddedBy).HasMaxLength(50);
            entity.Property(e => e.IsAddedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.IsUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.MasterCompanyUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Company_UUID");
            entity.Property(e => e.MasterEnvironmentUuid)
                .HasMaxLength(50)
                .HasColumnName("Master_Environment_UUID");
            entity.Property(e => e.ReasearchCommunicationStatusUuid)
                .HasMaxLength(50)
                .HasColumnName("ReasearchCommunicationStatus_UUID");
            entity.Property(e => e.RecordNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Remark).HasMaxLength(100);
            entity.Property(e => e.UpdatedIp)
                .HasMaxLength(30)
                .HasColumnName("UpdatedIP");
            entity.Property(e => e.UploadFiles)
                .HasMaxLength(200)
                .HasColumnName("Upload_Files");
            entity.Property(e => e.Uuid)
                .HasMaxLength(50)
                .HasColumnName("UUID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
