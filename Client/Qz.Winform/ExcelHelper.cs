using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
    /// Excel 助手类。
    /// </summary>
    public class ExcelHelper
    {
        private void InitializeWorkbook(HSSFWorkbook hssfworkbook, string headerText)
        {
            hssfworkbook = new HSSFWorkbook();

            //创建一个文档摘要信息实体。
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "ZLDC"; //公司名称
            hssfworkbook.DocumentSummaryInformation = dsi;

            //创建一个摘要信息实体。
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "zldc";
            si.Title = headerText;
            si.Subject = headerText;
            si.CreateDateTime = DateTime.Now;
            hssfworkbook.SummaryInformation = si;
        }

        private static MemoryStream WriteToStream(HSSFWorkbook hssfworkbook)
        {
            //Write the stream data of workbook to the root directory
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }

        //Export(DataTable table, string headerText, string sheetName, string[] columnName, string[] columnTitle)
        /// <summary>
        /// 向客户端输出文件。
        /// </summary>
        /// <param name="table">数据表。</param>
        /// <param name="headerText">头部文本。</param>
        /// <param name="sheetName"></param>
        /// <param name="columnName">数据列名称。</param>
        /// <param name="columnTitle">表标题。</param>
        /// <param name="fileName">文件名称。</param>
        public static void Write(DataTable table, string headerText, string sheetName, string[] columnName, string[] columnTitle, string fileName)
        {
            //HttpContext context = HttpContext.Current;
            //context.Response.ContentType = "application/vnd.ms-excel";
            //context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(fileName, Encoding.UTF8)));
            //context.Response.Clear();
            //HSSFWorkbook hssfworkbook = GenerateData(table, headerText, sheetName, columnName, columnTitle);
            //context.Response.BinaryWrite(WriteToStream(hssfworkbook).GetBuffer());
            //context.Response.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="headerText"></param>
        /// <param name="sheetName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnTitle"></param>
        /// <returns></returns>
        public static HSSFWorkbook GenerateData(DataTable table, string headerText, string sheetName, string[] columnName, string[] columnTitle)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet(sheetName);

            #region 设置文件属性信息

            //创建一个文档摘要信息实体。
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Weilog Team"; //公司名称
            hssfworkbook.DocumentSummaryInformation = dsi;

            //创建一个摘要信息实体。
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = " Weilog 系统";
            si.Title = headerText;
            si.Subject = headerText;
            si.CreateDateTime = DateTime.Now;
            hssfworkbook.SummaryInformation = si;

            #endregion

            ICellStyle dateStyle = hssfworkbook.CreateCellStyle();
            IDataFormat format = hssfworkbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            #region 取得列宽

            int[] colWidth = new int[columnName.Length];
            for (int i = 0; i < columnName.Length; i++)
            {
                colWidth[i] = Encoding.GetEncoding(936).GetBytes(columnTitle[i]).Length;
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < columnName.Length; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(table.Rows[i][columnName[j]].ToString()).Length;
                    if (intTemp > colWidth[j])
                    {
                        colWidth[j] = intTemp;
                    }
                }
            }

            #endregion

            int rowIndex = 0;
            foreach (DataRow row in table.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = hssfworkbook.CreateSheet(sheetName + ((int)rowIndex / 65535).ToString());
                    }

                    #region 表头及样式
                    //if (!string.IsNullOrEmpty(headerText))
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(headerText);

                        ICellStyle headStyle = hssfworkbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        IFont font = hssfworkbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;
                        //sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1)); 
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
                    }
                    #endregion

                    #region 列头及样式
                    {
                        //HSSFRow headerRow = sheet.CreateRow(1); 
                        IRow headerRow;
                        //if (!string.IsNullOrEmpty(headerText))
                        //{
                        //    headerRow = sheet.CreateRow(0);
                        //}
                        //else
                        //{
                        headerRow = sheet.CreateRow(1);
                        //}
                        ICellStyle headStyle = hssfworkbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        IFont font = hssfworkbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        for (int i = 0; i < columnName.Length; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(columnTitle[i]);
                            headerRow.GetCell(i).CellStyle = headStyle;
                            //设置列宽 
                            if ((colWidth[i] + 1) * 256 > 30000)
                            {
                                sheet.SetColumnWidth(i, 10000);
                            }
                            else
                            {
                                sheet.SetColumnWidth(i, (colWidth[i] + 1) * 256);
                            }
                        }
                        /* 
                        foreach (DataColumn column in dtSource.Columns) 
                        { 
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName); 
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle; 
   
                            //设置列宽    
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256); 
                        } 
                         * */
                    }
                    #endregion
                    //if (!string.IsNullOrEmpty(headerText))
                    //{
                    //    rowIndex = 1;
                    //}
                    //else
                    //{
                    rowIndex = 2;
                    //}

                }
                #endregion

                #region 填充数据

                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int i = 0; i < columnName.Length; i++)
                {
                    ICell newCell = dataRow.CreateCell(i);

                    string drValue = row[columnName[i]].ToString();

                    switch (table.Columns[columnName[i]].DataType.ToString())
                    {
                        case "System.String"://字符串类型   
                            if (drValue.ToUpper() == "TRUE")
                                newCell.SetCellValue("是");
                            else if (drValue.ToUpper() == "FALSE")
                                newCell.SetCellValue("否");
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型    
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示    
                            break;
                        case "System.Boolean"://布尔型    
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            if (boolV)
                                newCell.SetCellValue("是");
                            else
                                newCell.SetCellValue("否");
                            break;
                        case "System.Int16"://整型    
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型    
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理    
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }

                #endregion

                rowIndex++;
            }

            return hssfworkbook;
        }

    }

    public class NPOIExcelHelper
    {
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void DataTableToExcel(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = DataTableToExcel(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// 多个DataTable 导出到Excel文件
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="header"></param>
        /// <param name="path"></param>
        public static void DataTableToExcel(Dictionary<string, DataTable> dic, string header, string path)
        {
            using (MemoryStream ms = DataTableToExcel(dic, header))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// 创建多个工作表 
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static MemoryStream DataTableToExcel(Dictionary<string, DataTable> dic, string header) 
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "ZLDC";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "ZLDC"; //填加xls文件作者信息
                si.ApplicationName = "1.0.0.1"; //填加xls文件创建程序信息
                si.LastAuthor = "ZLDC"; //填加xls文件最后保存者信息
                si.Comments = "ZLDC"; //填加xls文件作者信息
                si.Title = "数据导出"; //填加xls文件标题信息
                si.Subject = "数据导出";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            foreach (var item in dic)
            {
                CreateSheet(workbook, item.Value, item.Key, header);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                
                return ms;
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream DataTableToExcel(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "ZLDC";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "ZLDC"; //填加xls文件作者信息
                si.ApplicationName = "1.0.0.1"; //填加xls文件创建程序信息
                si.LastAuthor = "ZLDC"; //填加xls文件最后保存者信息
                si.Comments = "ZLDC"; //填加xls文件作者信息
                si.Title = "数据导出"; //填加xls文件标题信息
                si.Subject = "数据导出";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            // 创建工作表 
            CreateSheet(workbook, dtSource,"sheet1",strHeaderText);

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                // sheet.Dispose();
                // workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        /// <summary>
        /// 创建工作表 
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private static HSSFSheet CreateSheet(HSSFWorkbook workBook, DataTable dt, string name = "sheet1", string header = "")
        {
            HSSFSheet sheet = (HSSFSheet)workBook.CreateSheet(name);

            HSSFCellStyle dateStyle = (HSSFCellStyle)workBook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workBook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dt.Columns.Count];
            foreach (DataColumn item in dt.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dt.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workBook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        if (!string.IsNullOrEmpty(header))
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(header);

                            HSSFCellStyle headStyle = (HSSFCellStyle)workBook.CreateCellStyle();
                            //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                            HSSFFont font = (HSSFFont)workBook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            font.FontName = "微软雅黑";
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                            //headerRow.Dispose();
                        }
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workBook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workBook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        font.FontName = "微软雅黑";
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dt.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                HSSFCellStyle cellStyle = (HSSFCellStyle)workBook.CreateCellStyle();
                HSSFFont cellFont = (HSSFFont)workBook.CreateFont();
                cellFont.FontName = "微软雅黑";
                cellStyle.SetFont(cellFont);
                foreach (DataColumn column in dt.Columns)
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                    newCell.CellStyle = cellStyle;
                }
                #endregion

                rowIndex++;
            }

            return sheet;
        }


        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="myDgv">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream DataGridViewToExcel(DataGridView myDgv, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[myDgv.Columns.Count];
            foreach (DataGridViewColumn item in myDgv.Columns)
            {
                arrColWidth[item.Index] = Encoding.GetEncoding(936).GetBytes(item.HeaderText.ToString()).Length;
            }
            for (int i = 0; i < myDgv.Rows.Count; i++)
            {
                for (int j = 0; j < myDgv.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(myDgv.Rows[i].Cells[j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataGridViewRow row in myDgv.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataGridViewColumn column in myDgv.Columns)
                        {
                            headerRow.CreateCell(column.Index).SetCellValue(column.HeaderText);
                            headerRow.GetCell(column.Index).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Index, (arrColWidth[column.Index] + 1) * 256);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                if (row.Index > 0)
                {
                    foreach (DataGridViewColumn column in myDgv.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Index);

                        string drValue = myDgv[column.Index, row.Index - 1].Value.ToString();

                        switch (column.ValueType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                }
                else
                { rowIndex--; }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }


        /// <summary>
        /// DataGridView导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTGridview</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void DataGridViewToExcel(DataGridView myDgv, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = DataGridViewToExcel(myDgv, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

    }
}