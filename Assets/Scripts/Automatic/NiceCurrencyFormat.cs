using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NiceCurrencyFormat : MonoBehaviour
{
    private class SuffixPair
    {
        public int DecimalValue { get; set; }
        public string SuffixName { get; set; }
    }

    private static List<SuffixPair> _suffixPairList = new List<SuffixPair>();

    private static void FillList()
    {
        if (_suffixPairList.Count == 0)
        {
            _suffixPairList.Add(new SuffixPair { DecimalValue = 357, SuffixName = "eq" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 354, SuffixName = "ep" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 351, SuffixName = "eo" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 348, SuffixName = "en" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 345, SuffixName = "em" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 342, SuffixName = "el" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 339, SuffixName = "ek" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 336, SuffixName = "ej" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 333, SuffixName = "ei" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 330, SuffixName = "eh" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 327, SuffixName = "eg" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 324, SuffixName = "ef" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 321, SuffixName = "ee" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 318, SuffixName = "ed" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 315, SuffixName = "ec" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 312, SuffixName = "eb" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 309, SuffixName = "ea" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 306, SuffixName = "dt" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 303, SuffixName = "ds" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 300, SuffixName = "dr" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 297, SuffixName = "dq" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 294, SuffixName = "dp" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 291, SuffixName = "do" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 288, SuffixName = "dn" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 285, SuffixName = "dm" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 282, SuffixName = "dl" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 279, SuffixName = "dk" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 276, SuffixName = "dj" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 273, SuffixName = "di" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 270, SuffixName = "dh" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 267, SuffixName = "dg" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 264, SuffixName = "df" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 261, SuffixName = "de" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 258, SuffixName = "dd" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 255, SuffixName = "dc" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 252, SuffixName = "db" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 249, SuffixName = "da" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 246, SuffixName = "cz" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 243, SuffixName = "cy" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 240, SuffixName = "cx" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 237, SuffixName = "cw" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 234, SuffixName = "cv" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 231, SuffixName = "cu" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 228, SuffixName = "ct" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 225, SuffixName = "cs" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 222, SuffixName = "cr" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 219, SuffixName = "cq" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 216, SuffixName = "cp" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 213, SuffixName = "co" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 210, SuffixName = "cn" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 207, SuffixName = "cm" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 204, SuffixName = "cl" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 201, SuffixName = "ck" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 198, SuffixName = "cj" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 195, SuffixName = "ci" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 192, SuffixName = "ch" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 189, SuffixName = "cg" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 186, SuffixName = "cf" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 183, SuffixName = "ce" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 180, SuffixName = "cd" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 177, SuffixName = "cc" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 174, SuffixName = "cb" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 171, SuffixName = "ca" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 168, SuffixName = "bz" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 165, SuffixName = "by" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 162, SuffixName = "bx" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 159, SuffixName = "bw" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 156, SuffixName = "bv" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 153, SuffixName = "bu" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 150, SuffixName = "bt" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 147, SuffixName = "bs" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 144, SuffixName = "br" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 141, SuffixName = "bq" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 138, SuffixName = "bp" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 135, SuffixName = "bo" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 132, SuffixName = "bn" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 129, SuffixName = "bm" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 126, SuffixName = "bl" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 123, SuffixName = "bk" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 120, SuffixName = "bj" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 117, SuffixName = "bi" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 114, SuffixName = "bh" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 111, SuffixName = "bg" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 108, SuffixName = "bf" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 105, SuffixName = "be" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 102, SuffixName = "bd" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 99, SuffixName = "bc" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 96, SuffixName = "bb" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 93, SuffixName = "ba" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 90, SuffixName = "az" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 87, SuffixName = "ay" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 84, SuffixName = "ax" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 81, SuffixName = "aw" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 78, SuffixName = "av" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 75, SuffixName = "au" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 72, SuffixName = "at" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 69, SuffixName = "as" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 66, SuffixName = "ar" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 63, SuffixName = "aq" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 60, SuffixName = "ap" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 57, SuffixName = "ao" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 54, SuffixName = "an" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 51, SuffixName = "am" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 48, SuffixName = "al" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 45, SuffixName = "ak" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 42, SuffixName = "aj" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 39, SuffixName = "ai" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 36, SuffixName = "ah" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 33, SuffixName = "ag" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 30, SuffixName = "af" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 27, SuffixName = "ae" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 24, SuffixName = "ad" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 21, SuffixName = "ac" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 18, SuffixName = "ab" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 15, SuffixName = "aa" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 12, SuffixName = "T" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 9, SuffixName = "B" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 6, SuffixName = "M" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 3, SuffixName = "K" });
            _suffixPairList.Add(new SuffixPair { DecimalValue = 0, SuffixName = "" });
        }
    }

    public static string ConvertToString(double valueToConvert, int round = 2)
    {
        FillList();
        valueToConvert = Math.Round(valueToConvert, round);
        string converted;

        var suffixValue = _suffixPairList.FirstOrDefault(x => Math.Log10(valueToConvert) >= x.DecimalValue);

        if (suffixValue != null)
        {
            if (valueToConvert >= 1000)
            {
                double result = valueToConvert / double.Parse("1e" + suffixValue.DecimalValue);
                string roundd = result >= 10 ? "" : "F1"; // = 4.0k = 40k = 400k 
                int rounddd = result >= 10 ? 0 : 1; // = 4.0k = 40k = 400k
                result = Math.Round(result, rounddd); // the number is rounded, and the fractional part is not trimmed!
                converted = result.ToString(roundd) + "" + suffixValue.SuffixName;
            }
            else
            {
                if (valueToConvert >= 10) // all that is less than 10, the necessary comma is seconds in updates // we remove 12.34 and 122.34 // only 12 and 122
                    valueToConvert = Math.Round(valueToConvert, 0);
                double intVal = Convert.ToDouble(valueToConvert); // Convert.ToDouble for 5.7
                converted = intVal.ToString();
            }
        }
        else // -> // valueToConvert  <= 1 = 0.1 0.2 0.35
        {
            return valueToConvert.ToString();
        }

        return converted;
    }


    public static string ConvertToStringTwoNumbers(double valueToConvert, int round = 2) // 1.25 // 10.25 // 100.25
    {
        FillList();
        valueToConvert = Math.Round(valueToConvert, round);
        string converted;

        var suffixValue = _suffixPairList.FirstOrDefault(x => Math.Log10(valueToConvert) >= x.DecimalValue);

        if (suffixValue != null)
        {
            if (valueToConvert >= 1000)
            {
                double result = valueToConvert / double.Parse("1e" + suffixValue.DecimalValue);
                string roundd = "F2"; 
                converted = result.ToString(roundd) + "" + suffixValue.SuffixName;
            }
            else
            {
                if (valueToConvert >= 10) // all that is less than 10, the necessary comma is seconds in updates // we remove 12.34 and 122.34 // only 12 and 122
                    valueToConvert = Math.Round(valueToConvert, 0);
                double intVal = Convert.ToDouble(valueToConvert); // Convert.ToDouble for 5.7
                converted = intVal.ToString();
            }
        }
        else // -> // valueToConvert  <= 1 = 0.1 0.2 0.35
        {
            return valueToConvert.ToString();
        }

        return converted;
    }

}