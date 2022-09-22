using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace OrganisationAPI.Services
{
    public class XLSXParserService : Interfaces.IXLSXParser
    {
        public string[][] ParseXLSXToCSV(IFormFile file)
        {
            string xlsxFilePath = file.FileName;
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
            string path = AppDomain.CurrentDomain.BaseDirectory.Substring(0,AppDomain.CurrentDomain.BaseDirectory.Length-17); //TODO не лучшее решение, знаю что существует WWWROOT, но он вроде как не для этого
            Excel.Workbook workbook = app.Workbooks.Open(path + xlsxFilePath);
            if (File.Exists(path + csvFilePath))
            {
                File.Delete(path + csvFilePath);
            }
            workbook.SaveAs(path + csvFilePath, Excel.XlFileFormat.xlCSVUTF8);
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
