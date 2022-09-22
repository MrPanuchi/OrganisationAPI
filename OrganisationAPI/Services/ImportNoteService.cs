using OrganisationAPI.Models.ImportModels;

namespace OrganisationAPI.Services
{
    public class ImportNoteService : Interfaces.IImportNote
    {
        public IEnumerable<ImportLetter> CreateImportLetters(string[][] parameters)
        {
            List<ImportLetter> result = new List<ImportLetter>();
            foreach (string[] parameter in parameters)
            {
                result.Add(ImportLetter.CreateImportLetter(parameter));
            }
            return result;
        }
    }
}
