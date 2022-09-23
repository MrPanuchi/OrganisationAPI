using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace OrganisationAPI.Services
{
    public class XLSXParserService : Interfaces.IXLSXParser
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public XLSXParserService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public string[][] ParseXLSXToCSV(IFormFile file)
        {
            string xlsxFilePath = _appEnvironment.WebRootPath + "\\" + file.FileName;
            string csvFilePath = xlsxFilePath.Substring(0, xlsxFilePath.Length - 5) + ".csv";

            DownloadFile(file, xlsxFilePath);
            ConvertXLSXToCSV(xlsxFilePath, csvFilePath);
            string data = CSVToString(csvFilePath);
            string[][] result = SplitData(data);

            return result;
        }
        private void DownloadFile(IFormFile file, string xlsxFilePath)
        {
            using (Stream fileStream = new FileStream(xlsxFilePath, FileMode.OpenOrCreate))
            {
                file.CopyTo(fileStream);
            }
        }
        private void ConvertXLSXToCSV(string xlsxFilePath, string csvFilePath)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(xlsxFilePath);
            if (File.Exists(csvFilePath))
            {
                File.Delete(csvFilePath);
            }
            workbook.SaveAs(csvFilePath, Excel.XlFileFormat.xlCSVUTF8);
            workbook.Close();
        }
        private string CSVToString(string csvFilePath)
        {
            return File.ReadAllText(csvFilePath, Encoding.UTF8);
        }
        private string[][] SplitData(string data)
        {
            List<string[]> strings = new List<string[]>();
            string[] firstSplit = data.Split("\r\n");
            foreach (string fs in firstSplit)
            {
                strings.Add(fs.Split(','));
            }
            return strings.ToArray();
        }
    }
}
