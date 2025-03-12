using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Live_ConsultingKSP.Services
{
    public class MasterCompanyServices : IMasterCompanyService
    {
        private readonly KsperpDbContext _context;
        public MasterCompanyServices(KsperpDbContext context)
        {
            _context = context;
        }
        public async Task<List<MasterCompany>> GetAllCompanyAsync()
        {
            return _context.MasterCompanies.OrderByDescending(x => x.CompanyId).ToList();
        }
        public async Task<MasterCompany?> GetCompanyByUuidAsync(Guid uuid)
        {
            return await _context.MasterCompanies.FirstOrDefaultAsync(c => c.Uuid == uuid.ToString());
        }
        public async Task AddCompanyAsync(MasterCompany company, IFormFile logoFile, IFormFile stampFile, IFormFile signatureFile)
        {
            bool isDuplicate = _context.MasterCompanies.Any(x => x.CompanyName == company.CompanyName
            || x.GstinNumber == company.GstinNumber || x.MobileNumber == company.MobileNumber);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }

            // Validate and save the Logo
            if (logoFile != null)
            {
                if (!IsValidImageFile(logoFile))
                    throw new Exception("Only .jpg and .png files under 2 MB are allowed.");

                company.Logo = await SaveImageAsync(logoFile);
            }

            // Validate and save the Stamp
            if (stampFile != null)
            {
                if (!IsValidImageFile(stampFile))
                    throw new Exception("Only .jpg and .png files under 2 MB are allowed.");

                company.Stamp = await SaveImageAsync(stampFile);
            }

            // Validate and save the Signature
            if (signatureFile != null)
            {
                if (!IsValidImageFile(signatureFile))
                    throw new Exception("Only .jpg and .png files under 2 MB are allowed.");

                company.Signature = await SaveImageAsync(signatureFile);
            }

            company.CompanyName = company.CompanyName;
            company.CompanyShortName = company.CompanyShortName;
            company.GstinNumber = company.GstinNumber;
            company.ContactPersonNameSales = company.ContactPersonNameSales;
            company.ContactPersonNameSupport = company.ContactPersonNameSupport;
            company.DateOfEstablishment = company.DateOfEstablishment;
            company.EmailIdSales = company.EmailIdSales;
            company.EmailIdSupport = company.EmailIdSupport;
            company.EmailIdPersonal = company.EmailIdPersonal;
            company.CountryNameUuid = company.CountryNameUuid;
            company.StateNameUuid = company.StateNameUuid;
            company.CityNameUuid = company.CityNameUuid;
            company.Address1 = company.Address1;
            company.Address2 = company.Address2;
            company.PhoneNumber = company.PhoneNumber;
            company.AlternatePhoneNumber = company.AlternatePhoneNumber;
            company.MobileNumber = company.MobileNumber;
            company.AlternateMobileNumber = company.AlternateMobileNumber;
            company.Url1 = company.Url1;
            company.Url2 = company.Url2;
            company.Logo = company.Logo;
            company.Stamp = company.Stamp;
            company.Signature = company.Signature;
            company.IsDisplay = company.IsDisplay;
            company.IsActive = true;
            //company.Uuid = Guid.NewGuid().ToString();
            company.IsAdddedOn = DateTime.Now;
            company.IsAddedBy = "1";
            _context.MasterCompanies.Add(company);

            await _context.SaveChangesAsync();


        }
        // Helper method to validate image file
        private bool IsValidImageFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return file.Length <= 2 * 1024 * 1024 // File size <= 2 MB
                   && allowedExtensions.Contains(extension);
        }

        // Helper method to save the image
        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/images/" + fileName; // Return the relative path
        }

        public async Task UpdateCompanyAsync(MasterCompany model)
        {
            var company = await _context.MasterCompanies.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (company != null)
            {
                company.CompanyName = model.CompanyName;
                company.CompanyShortName = model.CompanyShortName;
                company.GstinNumber = model.GstinNumber;
                company.ContactPersonNameSales = model.ContactPersonNameSales;
                company.ContactPersonNameSupport = model.ContactPersonNameSupport;
                company.DateOfEstablishment = model.DateOfEstablishment;
                company.EmailIdSales = model.EmailIdSales;
                company.EmailIdSupport = model.EmailIdSupport;
                company.EmailIdPersonal = model.EmailIdPersonal;
                company.CountryNameUuid = model.CountryNameUuid;
                company.StateNameUuid = model.StateNameUuid;
                company.CityNameUuid = model.CityNameUuid;
                company.Address1 = model.Address1;
                company.Address2 = model.Address2;
                company.PhoneNumber = model.PhoneNumber;
                company.AlternatePhoneNumber = model.AlternatePhoneNumber;
                company.MobileNumber = model.MobileNumber;
                company.AlternateMobileNumber = model.AlternateMobileNumber;
                company.Url1 = model.Url1;
                company.Url2 = model.Url2;
                company.Logo = model.Logo;
                company.Stamp = model.Stamp;
                company.Signature = model.Signature;
                company.IsDisplay = model.IsDisplay;
                company.IsUpdatedBy = "1";
                company.IsUpdatedOn = DateTime.Now;
                _context.MasterCompanies.Update(company);
                await _context.SaveChangesAsync();

            }
        }
        public void DeleteCompany(Guid uuid)
        {
            var company = _context.MasterCompanies.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (company != null)
            {
                company.IsActive = false; // Mark as inactive
                company.IsDeletedBy = "1"; // Assume 1 is the current user ID; adjust as needed
                company.IsDeletedOn = DateTime.Now; // Record deletion timestamp
                _context.MasterCompanies.Update(company); // Update the entity
                _context.SaveChanges(); // Save changes
            }
        }

    }
}
