/* ====================================================================
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
==================================================================== */

namespace Zephyr.Utils.NPOI.HSSF.UserModel
{
    using System;
    //using Zephyr.Utils.NPOI.HSSF.Model;
    using Zephyr.Utils.NPOI.HSSF.Record;
    using Zephyr.Utils.NPOI.HSSF.Record.Aggregates;
    using Zephyr.Utils.NPOI.SS;
    using Zephyr.Utils.NPOI.SS.Formula;
    using Zephyr.Utils.NPOI.SS.Formula.PTG;
    using Zephyr.Utils.NPOI.SS.Formula.Udf;
    using Zephyr.Utils.NPOI.SS.UserModel;
   
    /**
     * Internal POI use only
     * 
     * @author Josh Micich
     */
    public class HSSFEvaluationWorkbook : IFormulaRenderingWorkbook, IEvaluationWorkbook, IFormulaParsingWorkbook
    {

        private HSSFWorkbook _uBook;
        private Zephyr.Utils.NPOI.HSSF.Model.InternalWorkbook _iBook;

        public static HSSFEvaluationWorkbook Create(Zephyr.Utils.NPOI.SS.UserModel.IWorkbook book)
        {
            if (book == null)
            {
                return null;
            }
            return new HSSFEvaluationWorkbook((HSSFWorkbook)book);
        }

        private HSSFEvaluationWorkbook(HSSFWorkbook book)
        {
            _uBook = book;
            _iBook = book.Workbook;
        }

        public int GetExternalSheetIndex(String sheetName)
        {
            int sheetIndex = _uBook.GetSheetIndex(sheetName);
            return _iBook.CheckExternSheet(sheetIndex);
        }
        public int GetExternalSheetIndex(String workbookName, String sheetName)
        {
            return _iBook.GetExternalSheetIndex(workbookName, sheetName);
        }
        public ExternalName GetExternalName(int externSheetIndex, int externNameIndex)
        {
            return _iBook.GetExternalName(externSheetIndex, externNameIndex);
        }

        public NameXPtg GetNameXPtg(String name)
        {
            return _iBook.GetNameXPtg(name, _uBook.GetUDFFinder());
        }

        public IEvaluationName GetName(String name,int sheetIndex)
        {
            for (int i = 0; i < _iBook.NumNames; i++)
            {
                NameRecord nr = _iBook.GetNameRecord(i);
                if (nr.SheetNumber == sheetIndex + 1 && name.Equals(nr.NameText, StringComparison.OrdinalIgnoreCase))
                {
                    return new Name(nr, i);
                }
            }
            return sheetIndex == -1 ? null : GetName(name, -1);
        }

        public int GetSheetIndex(IEvaluationSheet evalSheet)
        {
            HSSFSheet sheet = ((HSSFEvaluationSheet)evalSheet).HSSFSheet;
            return _uBook.GetSheetIndex(sheet);
        }
        public int GetSheetIndex(String sheetName)
        {
            return _uBook.GetSheetIndex(sheetName);
        }

        public String GetSheetName(int sheetIndex)
        {
            return _uBook.GetSheetName(sheetIndex);
        }

        public IEvaluationSheet GetSheet(int sheetIndex)
        {
            return new HSSFEvaluationSheet((HSSFSheet)_uBook.GetSheetAt(sheetIndex));
        }
        public int ConvertFromExternSheetIndex(int externSheetIndex)
        {
            return _iBook.GetSheetIndexFromExternSheetIndex(externSheetIndex);
        }

        public ExternalSheet GetExternalSheet(int externSheetIndex)
        {
            return _iBook.GetExternalSheet(externSheetIndex);
        }

        public String ResolveNameXText(NameXPtg n)
        {
            return _iBook.ResolveNameXText(n.SheetRefIndex, n.NameIndex);
        }

        public String GetSheetNameByExternSheet(int externSheetIndex)
        {
            return _iBook.FindSheetNameFromExternSheet(externSheetIndex);
        }
        public String GetNameText(NamePtg namePtg)
        {
            return _iBook.GetNameRecord(namePtg.Index).NameText;
        }
        public IEvaluationName GetName(NamePtg namePtg)
        {
            int ix = namePtg.Index;
            return new Name(_iBook.GetNameRecord(ix), ix);
        }
        public Ptg[] GetFormulaTokens(IEvaluationCell evalCell)
        {
            ICell cell = ((HSSFEvaluationCell)evalCell).HSSFCell;
            //if (false)
            //{
            //    // re-parsing the formula text also works, but is a waste of time
            //    // It is useful from time to time to run all unit tests with this code
            //    // to make sure that all formulas POI can evaluate can also be parsed.
            //    return FormulaParser.Parse(cell.CellFormula, _uBook, FormulaType.CELL, _uBook.GetSheetIndex(cell.Sheet));
            //}
            FormulaRecordAggregate fr = (FormulaRecordAggregate)((HSSFCell)cell).CellValueRecord;
            return fr.FormulaTokens;
        }

        public UDFFinder GetUDFFinder()
        {
            return _uBook.GetUDFFinder();
        }


        private class Name : IEvaluationName
        {

            private NameRecord _nameRecord;
            private int _index;

            public Name(NameRecord nameRecord, int index)
            {
                _nameRecord = nameRecord;
                _index = index;
            }
            public Ptg[] NameDefinition
            {
                get{
                    return _nameRecord.NameDefinition;
                }
            }
            public String NameText
            {
                get{
                    return _nameRecord.NameText;
                }
            }
            public bool HasFormula
            {
                get{
                    return _nameRecord.HasFormula;
                }
            }
            public bool IsFunctionName
            {
                get{
                    return _nameRecord.IsFunctionName;
                }
            }
            public bool IsRange
            {
                get
                {
                    return _nameRecord.HasFormula; // TODO - is this right?
                }
            }
            public NamePtg CreatePtg()
            {
                return new NamePtg(_index);
            }
        }

        public SpreadsheetVersion GetSpreadsheetVersion()
        {
            return SpreadsheetVersion.EXCEL97;
        }
    }
}