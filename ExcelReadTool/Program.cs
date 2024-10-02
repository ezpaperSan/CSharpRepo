using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelReadTool
{
    internal class Program
    {
        public static void Main()
        {
            string filePath = "path/to/your/excel/file.xlsx";

            try
            {
                DataTable dataTable = ReadExcelFile(filePath);

                // 輸出欄位名稱
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    Console.Write(dataTable.Columns[col].ColumnName + "\t");
                }
                Console.WriteLine();

                // 輸出數據
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        Console.Write(row[col] + "\t");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static DataTable ReadExcelFile(string filePath)
        {
            IWorkbook workbook;
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.EndsWith(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (filePath.EndsWith(".xls"))
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
                else
                {
                    throw new Exception("Unsupported file format.");
                }
            }

            var worksheet = workbook.GetSheetAt(0);

            var dataTable = new DataTable();

            // 創建欄位
            for (int col = 0; col < worksheet.GetRow(0).LastCellNum; col++)
            {
                dataTable.Columns.Add(worksheet.GetRow(0).GetCell(col).StringCellValue);
            }

            // 填充數據
            for (int row = 1; row < worksheet.LastRowNum; row++)
            {
                var dataRow = dataTable.NewRow();
                var rowData = worksheet.GetRow(row);
                for (int col = 0; col < rowData.LastCellNum; col++)
                {
                    dataRow[col] = rowData.GetCell(col).ToString();
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}
