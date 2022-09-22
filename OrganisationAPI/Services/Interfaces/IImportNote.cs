using OrganisationAPI.Models.ImportModels;

namespace OrganisationAPI.Services.Interfaces
{
    public interface IImportNote
    {
        public IEnumerable<ImportLetter> CreateImportLetters(string[][] parameters);
    }
}
