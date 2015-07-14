﻿using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Utils.NPOI.SS.Formula.Eval;
using System.Globalization;

namespace Zephyr.Utils.NPOI.SS.Formula.Functions
{
    public class CHAR : Fixed1ArgFunction
    {
        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
        {
            int arg;
            try
            {
                arg = TextFunction.EvaluateIntArg(arg0, srcRowIndex, srcColumnIndex);
                if (arg < 0 || arg >= 256)
                {
                    throw new EvaluationException(ErrorEval.VALUE_INVALID);
                }

            }
            catch (EvaluationException e)
            {
                return e.GetErrorEval();
            }
            return new StringEval(Convert.ToString((char)arg, CultureInfo.CurrentCulture));
        }
    }
}
