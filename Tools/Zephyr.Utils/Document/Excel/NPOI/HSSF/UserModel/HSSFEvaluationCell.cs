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
    using System.Text;
    using Zephyr.Utils.NPOI.SS.Formula;

    /// <summary>
    /// HSSF wrapper for a cell under evaluation
    /// @author Josh Micich
    /// </summary>
    public class HSSFEvaluationCell : IEvaluationCell
    {

        private IEvaluationSheet _evalSheet;
        private Zephyr.Utils.NPOI.SS.UserModel.ICell _cell;

        public HSSFEvaluationCell(Zephyr.Utils.NPOI.SS.UserModel.ICell cell, IEvaluationSheet evalSheet)
        {
            _cell = cell;
            _evalSheet = evalSheet;
        }
        public HSSFEvaluationCell(Zephyr.Utils.NPOI.SS.UserModel.ICell cell)
        {
            _cell = cell;
            _evalSheet = new HSSFEvaluationSheet((HSSFSheet)cell.Sheet);
        }
        // Note -  hashCode and equals defined according to underlying cell
        public override int GetHashCode()
        {
            return _cell.GetHashCode();
        }
        public override bool Equals(Object obj)
        {
            Zephyr.Utils.NPOI.SS.UserModel.ICell cellb = ((HSSFEvaluationCell)obj)._cell;
            return _cell.RowIndex == cellb.RowIndex
                && _cell.ColumnIndex == cellb.ColumnIndex
                && _cell.CellFormula == cellb.CellFormula
                && _cell.Sheet == cellb.Sheet;
        }

        public Zephyr.Utils.NPOI.SS.UserModel.ICell HSSFCell
        {
            get
            {
                return _cell;
            }
        }
        public bool BooleanCellValue
        {
            get
            {
                return _cell.BooleanCellValue;
            }
        }
        public Zephyr.Utils.NPOI.SS.UserModel.CellType CellType
        {
            get
            {
                return _cell.CellType;
            }
        }
        public int ColumnIndex
        {
            get
            {
                return _cell.ColumnIndex;
            }
        }
        public int ErrorCellValue
        {
            get
            {
                return _cell.ErrorCellValue;
            }
        }
        public double NumericCellValue
        {
            get
            {
                return _cell.NumericCellValue;
            }
        }
        public int RowIndex
        {
            get
            {
                return _cell.RowIndex;
            }
        }
        public IEvaluationSheet Sheet
        {
            get
            {
                return _evalSheet;
            }
        }
        public String StringCellValue
        {
            get
            {
                return _cell.RichStringCellValue.String;
            }
        }


        public object IdentityKey
        {
            get { return _cell; }
        }
    }
}