using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;
using Live_ConsultingKSP;
using AutoMapper;
using DataAccess.Entities;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.GetConnectionString("AppDbContext");

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddScoped<Utils>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(name: "EditProjectPhase",
                pattern: "Project/EditProjectPhase/{*uuid}",
                defaults: new { controller = "Project", action = "EditProjectPhase" });

app.MapControllerRoute(name: "EditShift",
                pattern: "HRA/EditShift/{*uuid}",
                defaults: new { controller = "HRA", action = "EditShift" });

app.MapControllerRoute(name: "EditPaySlipCategory",
                pattern: "HRA/EditPaySlipCategory/{*uuid}",
                defaults: new { controller = "HRA", action = "EditPaySlipCategory" });

app.MapControllerRoute(name: "EditWeekDay",
                pattern: "HRA/EditWeekDay/{*uuid}",
                defaults: new { controller = "HRA", action = "EditWeekDay" });

app.MapControllerRoute(name: "EditNominee",
                pattern: "HRA/EditNominee/{*uuid}",
                defaults: new { controller = "HRA", action = "EditNominee" });

app.MapControllerRoute(name: "EditTaxRegime",
                pattern: "HRA/EditTaxRegime/{*uuid}",
                defaults: new { controller = "HRA", action = "EditTaxRegime" });
app.MapControllerRoute(name: "MasterEditEmailCredential",
                pattern: "CompanySetup/MasterEditEmailCredential/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditEmailCredential" });

app.MapControllerRoute(name: "MasterEditBankDetails",
                pattern: "CompanySetup/MasterEditBankDetails/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditBankDetails" });

app.MapControllerRoute(name: "EditMenu",
                pattern: "CompanySetup/EditMenu/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditMenu" });
app.MapControllerRoute(name: "MasterEditCityName",
                pattern: "CompanySetup/MasterEditCityName/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditCityName" });

app.MapControllerRoute(name: "MasterEditStateName",
                pattern: "CompanySetup/MasterEditStateName/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditStateName" });

app.MapControllerRoute(name: "EditExpenseUnitMaster",
                pattern: "Expense/EditExpenseUnitMaster/{*uuid}",
                defaults: new { controller = "Expense", action = "EditExpenseUnitMaster" });


app.MapControllerRoute(name: "EditIndustrySector",
                pattern: "CompanySetup/EditIndustrySector/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditIndustrySector" });

app.MapControllerRoute(name: "EditRole",
                pattern: "CompanySetup/EditRole/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditRole" });

app.MapControllerRoute(name: "EditResearchChannelType",
                pattern: "BusinessDevelopment/EditResearchChannelType/{*uuid}",
                defaults: new { controller = "BusinessDevelopment", action = "EditResearchChannelType" });
app.MapControllerRoute(name: "EditResearchAudience",
                pattern: "BusinessDevelopment/EditResearchAudience/{*uuid}",
                defaults: new { controller = "BusinessDevelopment", action = "EditResearchAudience" });
app.MapControllerRoute(name: "EditResearchCommunicationMode",
                pattern: "BusinessDevelopment/EditResearchCommunicationMode/{*uuid}",
                defaults: new { controller = "BusinessDevelopment", action = "EditResearchCommunicationMode" });
app.MapControllerRoute(name: "MasterEditServiceGroup",
                pattern: "CompanySetup/MasterEditServiceGroup/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditServiceGroup" });
app.MapControllerRoute(name: "MasterEditEmployeeDesignation",
                pattern: "CompanySetup/MasterEditEmployeeDesignation/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditEmployeeDesignation" });
app.MapControllerRoute(name: "MasterEditDepartment",
                pattern: "CompanySetup/MasterEditDepartment/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditDepartment" });
app.MapControllerRoute(name: "MasterEditCountryName",
                pattern: "CompanySetup/MasterEditCountryName/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditCountryName" });


app.MapControllerRoute(name: "MasterEditFinancialYear",
                pattern: "Account/MasterEditFinancialYear/{*uuid}",
                defaults: new { controller = "Account", action = "MasterEditFinancialYear" });

app.MapControllerRoute(name: "EditUnitMaster",
                pattern: "Account/EditUnitMaster/{*uuid}",
                defaults: new { controller = "Account", action = "EditUnitMaster" });

app.MapControllerRoute(name: "EditModeofPayment",
                pattern: "Account/EditModeofPayment/{*uuid}",
                defaults: new { controller = "Account", action = "EditModeofPayment" });

app.MapControllerRoute(name: "EditTermsofPayment",
                pattern: "Account/EditTermsofPayment/{*uuid}",
                defaults: new { controller = "Account", action = "EditTermsofPayment" });

app.MapControllerRoute(name: "EditModeofTransport",
                pattern: "Account/EditModeofTransport/{*uuid}",
                defaults: new { controller = "Account", action = "EditModeofTransport" });

app.MapControllerRoute(name: "MasterEditAllowanceType",
                pattern: "Account/MasterEditAllowanceType/{*uuid}",
                defaults: new { controller = "Account", action = "MasterEditAllowanceType" });

app.MapControllerRoute(name: "MasterEditPaymentStatus",
                pattern: "Account/MasterEditPaymentStatus/{*uuid}",
                defaults: new { controller = "Account", action = "MasterEditPaymentStatus" });

app.MapControllerRoute(name: "MasterEditLanguage",
                pattern: "Account/MasterEditLanguage/{*uuid}",
                defaults: new { controller = "Account", action = "MasterEditLanguage" });

app.MapControllerRoute(name: "EditPaymentMeansCode",
                pattern: "Account/EditPaymentMeansCode/{*uuid}",
                defaults: new { controller = "Account", action = "EditPaymentMeansCode" });



app.MapControllerRoute(name: "MasterEditCompanyName",
                pattern: "Companysetup/MasterEditCompanyName/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditCompanyName" });

app.MapControllerRoute(name: "MasterEditEnvironment",
                pattern: "Companysetup/MasterEditEnvironment/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditEnvironment" });


app.MapControllerRoute(name: "MasterEditCompanyType",
                pattern: "Companysetup/MasterEditCompanyType/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditCompanyType" });

app.MapControllerRoute(name: "MasterEditYear",
                pattern: "Companysetup/MasterEditYear/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditYear" });

app.MapControllerRoute(name: "MasterEditBankType",
                pattern: "Companysetup/MasterEditBankType/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditBankType" });

app.MapControllerRoute(name: "MasterEditBankAccountType",
                pattern: "Companysetup/MasterEditBankAccountType/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditBankAccountType" });

app.MapControllerRoute(name: "MasterEditHonorific",
                pattern: "Companysetup/MasterEditHonorific/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditHonorific" });

app.MapControllerRoute(name: "MasterEditGender",
                pattern: "Companysetup/MasterEditGender/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditGender" });


app.MapControllerRoute(name: "EditBloodGroup",
                pattern: "Companysetup/EditBloodGroup/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditBloodGroup" });

app.MapControllerRoute(name: "EditMaritalStatus",
                pattern: "Companysetup/EditMaritalStatus/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditMaritalStatus" });

app.MapControllerRoute(name: "EditNationality",
                pattern: "Companysetup/EditNationality/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditNationality" });

app.MapControllerRoute(name: "MasterEditDocumentType",
                pattern: "Companysetup/MasterEditDocumentType/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "MasterEditDocumentType" });

app.MapControllerRoute(name: "EditAssets",
                pattern: "Companysetup/EditAssets/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditAssets" });

app.MapControllerRoute(name: "EditLibraryCategory",
                pattern: "Library/EditLibraryCategory/{*uuid}",
                defaults: new { controller = "Library", action = "EditLibraryCategory" });

app.MapControllerRoute(name: "EditLibraryCategoryTag",
                pattern: "Library/EditLibraryCategoryTag/{*uuid}",
                defaults: new { controller = "Library", action = "EditLibraryCategoryTag" });

app.MapControllerRoute(name: "EditDocumentCategory",
                pattern: "Library/EditDocumentCategory/{*uuid}",
                defaults: new { controller = "Library", action = "EditDocumentCategory" });

app.MapControllerRoute(name: "EditDocumentCategoryTag",
                pattern: "Library/EditDocumentCategoryTag/{*uuid}",
                defaults: new { controller = "Library", action = "EditDocumentCategoryTag" });

app.MapControllerRoute(name: "EditHeadDesignation",
                pattern: "Expense/EditHeadDesignation/{*uuid}",
                defaults: new { controller = "Expense", action = "EditHeadDesignation" });
app.MapControllerRoute(name: "EditReportingDesignation",
                pattern: "Expense/EditReportingDesignation/{*uuid}",
                defaults: new { controller = "Expense", action = "EditReportingDesignation" });

app.MapControllerRoute(name: "EditExpenseDateRangeMaster",
                pattern: "Expense/EditExpenseDateRangeMaster/{*uuid}",
                defaults: new { controller = "Expense", action = "EditExpenseDateRangeMaster" });
app.MapControllerRoute(name: "EditEmployeeGeneral",
                pattern: "CompanySetup/EditEmployeeGeneral/{*uuid}",
                defaults: new { controller = "CompanySetup", action = "EditEmployeeGeneral" });
app.MapControllerRoute(name: "MasterEditExpenseCurrencyMaster",
                pattern: "Expense/MasterEditExpenseCurrencyMaster/{*uuid}",
                defaults: new { controller = "Expense", action = "MasterEditExpenseCurrencyMaster" });
app.MapControllerRoute(name: "EditProjectStatus",
                pattern: "Project/EditProjectStatus/{*uuid}",
                defaults: new { controller = "Project", action = "EditProjectStatus" });

app.MapControllerRoute(name: "EditMeetingMOM",
                pattern: "Project/EditMeetingMOM/{*uuid}",
                defaults: new { controller = "Project", action = "EditMeetingMOM" });
app.MapControllerRoute(name: "EditCreateProject",
                pattern: "Project/EditCreateProject/{*uuid}",
                defaults: new { controller = "Project", action = "EditCreateProject" });
app.MapControllerRoute(name: "EditProjectCredentials",
                pattern: "Project/EditProjectCredentials/{*uuid}",
                defaults: new { controller = "Project", action = "EditProjectCredentials" });
app.MapControllerRoute(name: "MasterEditProjectMeeting",
                pattern: "Project/MasterEditProjectMeeting/{*uuid}",
                defaults: new { controller = "Project", action = "MasterEditProjectMeeting" });

app.MapControllerRoute(name: "EditProjectTask",
                pattern: "Project/EditProjectTask/{*uuid}",
                defaults: new { controller = "Project", action = "EditProjectTask" });


app.MapControllerRoute(name: "EditLeaveType",
                pattern: "HRA/EditLeaveType/{*uuid}",
                defaults: new { controller = "HRA", action = "EditLeaveType" });


app.MapControllerRoute(name: "EditDocumentNaming",
                pattern: "HRA/EditDocumentNaming/{*uuid}",
                defaults: new { controller = "HRA", action = "EditDocumentNaming" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CompanySetup}/{action=Index}/{id?}");

app.Run();
