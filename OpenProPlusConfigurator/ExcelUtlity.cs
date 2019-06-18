using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    public class ExcelUtlity
    {
        public bool WriteDataTableToExcel(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReportType)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application(); // Start Excel and get Application object.
                excel.Visible = false; // for making Excel visible
                excel.DisplayAlerts = false;
                excelworkBook = excel.Workbooks.Add(Type.Missing); // Creation a new Workbook 
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;  // Workk sheet
                excelSheet.Name = worksheetName;
                excelSheet.Cells[1, 1] = ReportType;
           
                System.Drawing.ColorConverter cc = new System.Drawing.ColorConverter();

                if (dataTable.Columns.Count == 14)
                {
                    excelSheet.get_Range("A1", "N1").Merge(Type.Missing);
                    excelSheet.get_Range("A1", "N1").Font.Size = 20;
                    excelSheet.get_Range("A1", "N1").Font.Bold = true;
                    excelSheet.get_Range("A1", "N1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excelSheet.get_Range("A1", "N1").VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excelCellrange = excelSheet.get_Range("A1", "N1");
                    
                    excelCellrange.RowHeight = 25;
                    int rowcount = 2;  // loop through each row and add values to our sheet
                    foreach (DataRow datarow in dataTable.Rows)
                    {
                        rowcount += 1;
                        for (int i = 1; i <= dataTable.Columns.Count; i++)
                        {
                            if (rowcount == 3) // on the first iteration we add the column headers
                            {
                                excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                                excelCellrange.RowHeight = 25;
                                excelSheet.Cells.Font.Color = System.Drawing.Color.Black;
                            }
                            excelCellrange = excelSheet.Range[excelSheet.Cells[2, i], excelSheet.Cells[2, dataTable.Columns.Count]];
                            FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);
                            excelSheet.Cells[1, i].Interior.Color = System.Drawing.ColorTranslator.ToOle((System.Drawing.Color)cc.ConvertFromString("#F0F8FF")); // First Cell 
                            excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();
                            excelCellrange.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            if (rowcount > 3)  //for alternate rows
                            {
                                if (i == dataTable.Columns.Count)
                                {
                                    if (rowcount % 2 == 0)
                                    {
                                        excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                        FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                    }
                                }
                            }
                        }
                    }
                    excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];  // now we resize the columns
                    excelCellrange.EntireColumn.AutoFit();
                    Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;
                    //Namrata: 09/12/2017
                    excelSheet.EnableAutoFilter = true;   //Enable Auto-filter. 
                    Microsoft.Office.Interop.Excel.Range range = excelSheet.get_Range("A1", "N1");  //Create the range.
                    range.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);    //Auto-filter the range.
                    excelSheet.get_Range("A1", "N1").EntireColumn.AutoFit(); //Auto-fit the second column.
                }
                if (dataTable.Columns.Count == 21)
                {
                    excelSheet.get_Range("A1", "U1").Merge(Type.Missing);
                    excelSheet.get_Range("A1", "U1").Font.Size = 20;
                    excelSheet.get_Range("A1", "U1").Font.Bold = true;
                    excelSheet.get_Range("A1", "U1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excelSheet.get_Range("A1", "U1").VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excelCellrange = excelSheet.get_Range("A1", "U1");
                    excelCellrange.RowHeight = 25;
                    int rowcount = 2;  // loop through each row and add values to our sheet
                    foreach (DataRow datarow in dataTable.Rows)
                    {
                        rowcount += 1;
                        for (int i = 1; i <= dataTable.Columns.Count; i++)
                        {
                            if (rowcount == 3) // on the first iteration we add the column headers
                            {
                                excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                                excelCellrange.RowHeight = 25;
                                excelSheet.Cells.Font.Color = System.Drawing.Color.Black;
                            }
                            excelCellrange = excelSheet.Range[excelSheet.Cells[2, i], excelSheet.Cells[2, dataTable.Columns.Count]];
                            FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);
                            excelSheet.Cells[1, i].Interior.Color = System.Drawing.ColorTranslator.ToOle((System.Drawing.Color)cc.ConvertFromString("#F0F8FF")); // First Cell 
                            excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                            if (rowcount > 3)  //for alternate rows
                            {
                                if (i == dataTable.Columns.Count)
                                {
                                    if (rowcount % 2 == 0)
                                    {
                                        excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                        FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                    }
                                }
                            }
                        }
                    }
                    excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];  // now we resize the columns
                    excelCellrange.EntireColumn.AutoFit();
                    Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;
                    excelCellrange.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //Namrata: 09/12/2017
                    excelSheet.EnableAutoFilter = true;   //Enable Auto-filter.
                    Microsoft.Office.Interop.Excel.Range range = excelSheet.get_Range("A1", "U1");   //Create the range.
                    range.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);//Auto-filter the range.
                    excelSheet.get_Range("A1", "U1").EntireColumn.AutoFit();  //Auto-fit the second column.
                }
                if (dataTable.Columns.Count == 13)
                {
                    excelSheet.get_Range("A1", "M1").Merge(Type.Missing);
                    excelSheet.get_Range("A1", "M1").Font.Size = 20;
                    excelSheet.get_Range("A1", "M1").Font.Bold = true;
                    excelSheet.get_Range("A1", "M1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excelSheet.get_Range("A1", "M1").VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excelCellrange = excelSheet.get_Range("A1", "M1");
                    excelCellrange.RowHeight = 25;
                    int rowcount = 2;  // loop through each row and add values to our sheet
                    foreach (DataRow datarow in dataTable.Rows)
                    {
                        rowcount += 1;
                        for (int i = 1; i <= dataTable.Columns.Count; i++)
                        {
                            if (rowcount == 3) // on the first iteration we add the column headers
                            {
                                excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                                excelCellrange.RowHeight = 25;
                                excelSheet.Cells.Font.Color = System.Drawing.Color.Black;
                            }
                            excelCellrange = excelSheet.Range[excelSheet.Cells[2, i], excelSheet.Cells[2, dataTable.Columns.Count]];
                            FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);
                            excelSheet.Cells[1, i].Interior.Color = System.Drawing.ColorTranslator.ToOle((System.Drawing.Color)cc.ConvertFromString("#F0F8FF")); // First Cell 
                            excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();
                            if (rowcount > 3)  //for alternate rows
                            {
                                if (i == dataTable.Columns.Count)
                                {
                                    if (rowcount % 2 == 0)
                                    {
                                        excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                        FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                    }
                                }
                            }
                        }
                    }
                    excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];  // now we resize the columns
                    excelCellrange.EntireColumn.AutoFit();
                    Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;
                    excelCellrange.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //Namrata: 09/12/2017
                    excelSheet.EnableAutoFilter = true; //Enable Auto-filter.
                    Microsoft.Office.Interop.Excel.Range range = excelSheet.get_Range("A1", "M1");  //Create the range.
                    range.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);  //Auto-filter the range.
                    excelSheet.get_Range("A1", "M1").EntireColumn.AutoFit();   //Auto-fit the second column.
                }
                excelworkBook.SaveAs(saveAsLocation);  //now save the workbook and exit Excel
                MessageBox.Show("File Exported Successfully", Application.ProductName, MessageBoxButtons.OK,MessageBoxIcon.Information);
                excelworkBook.Close();
                excel.Quit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }
        }
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }
    }
}
