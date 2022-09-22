namespace OrganisationAPI.Services.Interfaces
{
    public interface IXLSXParser
    {
        public string[][] ParseXLSXToCSV(IFormFile file);
    }
}
