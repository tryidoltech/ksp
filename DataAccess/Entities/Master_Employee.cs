namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_Employee
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string Master_Prefix_UUID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(80)]
        public string ProfilePic { get; set; }

        [StringLength(50)]
        public string Master_BloodGroup_UUID { get; set; }

        [StringLength(50)]
        public string Master_Department_UUID { get; set; }

        [StringLength(50)]
        public string Master_Gender_UUID { get; set; }

        [StringLength(30)]
        public string EmployeeCode { get; set; }

        [StringLength(50)]
        public string ExpLimitDesignation { get; set; }

        [StringLength(50)]
        public string ExpWorkflowDesignation { get; set; }

        [StringLength(50)]
        public string ReportingDesignation { get; set; }

        [StringLength(50)]
        public string Master_Roles_UUID { get; set; }

        [StringLength(256)]
        public string Allowed_Company_UUID { get; set; }

        [StringLength(256)]
        public string Allowed_Company { get; set; }

        [StringLength(256)]
        public string Allowed_Environment_UUID { get; set; }

        [StringLength(256)]
        public string Allowed_Environment { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? IsLoginActive { get; set; }

        [StringLength(70)]
        public string PersonalEmail { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Landline { get; set; }

        [StringLength(50)]
        public string AltMobile { get; set; }

        [StringLength(70)]
        public string CompanyEmail { get; set; }

        [StringLength(70)]
        public string CompanyContactNo { get; set; }

        [StringLength(30)]
        public string BuildingNo { get; set; }

        [StringLength(70)]
        public string StreetAddress { get; set; }

        [StringLength(70)]
        public string StreetAddress2 { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string Master_City_UUID { get; set; }

        [StringLength(50)]
        public string Master_City_Name { get; set; }

        [StringLength(50)]
        public string Master_State_UUID { get; set; }

        [StringLength(50)]
        public string Master_State_Name { get; set; }

        [StringLength(50)]
        public string Master_Country_UUID { get; set; }

        [StringLength(50)]
        public string Master_Country_Name { get; set; }

        [StringLength(30)]
        public string CurrentBuildingNo { get; set; }

        [StringLength(70)]
        public string CurrentStreetAddress { get; set; }

        [StringLength(70)]
        public string CurrentStreetAddress2 { get; set; }

        [StringLength(15)]
        public string CurrentPostCode { get; set; }

        [StringLength(50)]
        public string CurrentMaster_City_UUID { get; set; }

        [StringLength(50)]
        public string CurrentMaster_City_Name { get; set; }

        [StringLength(50)]
        public string CurrentMaster_State_UUID { get; set; }

        [StringLength(50)]
        public string CurrentMaster_State_Name { get; set; }

        [StringLength(50)]
        public string CurrentMaster_Country_UUID { get; set; }

        [StringLength(50)]
        public string CurrentMaster_Country_Name { get; set; }

        [StringLength(50)]
        public string CurrentLandmark { get; set; }

        [StringLength(50)]
        public string Landmark { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public bool? IsLibraryAllowed { get; set; }

        public bool? IsTreeViewSearchAllowed { get; set; }

        public bool? IsDisplay { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? IsAddedOn { get; set; }

        [StringLength(50)]
        public string IsAddedBy { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        [StringLength(50)]
        public string IsUpdatedBy { get; set; }

        public DateTime? IsDeletedOn { get; set; }

        [StringLength(50)]
        public string IsDeletedBy { get; set; }

        [StringLength(30)]
        public string AddedIP { get; set; }

        [StringLength(30)]
        public string UpdatedIP { get; set; }

        [StringLength(30)]
        public string DeletedIP { get; set; }

        public bool? IsCurrentAddressSame { get; set; }

        [StringLength(50)]
        public string Father_Name { get; set; }

        [StringLength(50)]
        public string Father_Occupation { get; set; }

        [StringLength(50)]
        public string Father_MobileNo { get; set; }

        [StringLength(50)]
        public string Mother_Name { get; set; }

        [StringLength(50)]
        public string Mother_Occupation { get; set; }

        [StringLength(50)]
        public string Mother_MobileNo { get; set; }

        [StringLength(50)]
        public string Sibling1_Name { get; set; }

        [StringLength(50)]
        public string Sibling1_Occupation { get; set; }

        [StringLength(50)]
        public string Sibling1_MobileNo { get; set; }

        [StringLength(50)]
        public string Sibling2_Name { get; set; }

        [StringLength(50)]
        public string Sibling2_Occupation { get; set; }

        [StringLength(50)]
        public string Sibling2_MobileNo { get; set; }

        [StringLength(50)]
        public string Employee_Code { get; set; }

        [StringLength(50)]
        public string Payslip_Category_UUID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_of_Joining { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BP_Code { get; set; }

        public bool? IsEmployee_DoneFromPreviousCompany { get; set; }

        public bool? Is_Employee_Relived { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_of_Reliving { get; set; }

        [StringLength(50)]
        public string Reason_of_Leave { get; set; }

        public bool? IsSubmittedAll_Assests { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Submitted_Date { get; set; }

        [StringLength(50)]
        public string Hr_Designation_UUID { get; set; }

        [StringLength(50)]
        public string PF_No { get; set; }

        [StringLength(50)]
        public string ESIC_No { get; set; }

        [StringLength(50)]
        public string UAN_No { get; set; }

        [StringLength(50)]
        public string Tax_Regime_UUID { get; set; }

        [StringLength(50)]
        public string Employeement_Type_UUID { get; set; }

        [StringLength(50)]
        public string ExpenseLimitDesignation_UUID { get; set; }

        [StringLength(50)]
        public string ExpenseWorkflowDesignation_UUID { get; set; }

        [StringLength(50)]
        public string Reporting_Designation { get; set; }

        [StringLength(50)]
        public string Alternate_Reporting_Designation { get; set; }

        [StringLength(50)]
        public string Menu_Role_UUID { get; set; }

        [StringLength(50)]
        public string Shift_UUID { get; set; }

        public TimeSpan? Shift_Start_Time { get; set; }

        public TimeSpan? Shift_End_Time { get; set; }

        public TimeSpan? Lunch_Start_Time { get; set; }

        public TimeSpan? Lunch_End_Time { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecordNo { get; set; }
    }
}
