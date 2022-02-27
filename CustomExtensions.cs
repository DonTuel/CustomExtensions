using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using System.Runtime.CompilerServices;

namespace CustomExtensions
{
    public static class StringExtension
    {
        public static bool ToBool(this string value)
        {
            if (value != null)
            {
                switch (value.ToLower())
                {
                    case "true":
                        return true;
                    case "false":
                        return false;
                    default:
                        break;
                }
            }
            return false;
        }
        public static bool ContainsAnyOf(this string value, params string[] inValue)
        {
            foreach (string str in inValue)
            {
                if (value.Contains(str)) { return true; }
            }

            return false;
        }
        public static int FindAnyOf(this string value, params string[] inValue)
        {
            for (int i = 0; i < inValue.Length; i++)
            {
                if (value.Contains(inValue[i])) return i;
            }

            return -1;
        }
        public static int IndexAnyOf(this string value, params string[] inValue)
        {
            return value.IndexAnyOf(0, inValue);
        }
        public static int IndexAnyOf(this string value, int start, params string[] inValue)
        {
            foreach (string str in inValue)
            {
                if (value.IndexOf(str, start) >= 0)
                {
                    return value.IndexOf(str, start);
                }
            }

            return -1;
        }
        public static int LastIndexAnyOf(this string value, params string[] inValue)
        {
            foreach (string str in inValue)
            {
                if (value.Contains(str))
                {
                    return value.LastIndexOf(str);
                }
            }

            return -1;
        }
        public static int LastIndexAnyOf(this string value, params char[] inValue)
        {
            foreach (char chr in inValue)
            {
                if (value.Contains(chr))
                {
                    return value.LastIndexOf(chr);
                }
            }

            return -1;
        }
        public static int Count(this string value, char chr)
        {
            int iCnt = 0;
            int id1 = value.IndexOf(chr);
            if (id1 > -1)
            {
                do
                {
                    iCnt++;
                    id1 = value.IndexOf(chr, id1 + 1);
                } while (id1 > -1);
            }

            return iCnt;
        }
        public static bool IsNumeric(this string value, int start, int length)
        {
            if (length < 1) { return false; }

            string str = value.Substring(start, length).Trim();

            if (str.Length < 1) { return false; }

            char sign = ' ';
            char dec = ' ';
            char dig = ' ';

            foreach (char chr in str.ToCharArray())
            {
                switch (chr)
                {
                    case '-':
                    case '+':
                        if (sign != ' ') { return false; }
                        if (dig != ' ') { return false; }
                        sign = chr;
                        break;

                    case '.':
                        if (dec != ' ') { return false; }
                        dec = chr;
                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        dig = chr;
                        break;

                    default:
                        return false;
                }
            }

            return true;
        }
        public static bool IsNumeric(this string value, int start)
        {
            return value.IsNumeric(start, value.Length - start);
        }
        public static bool IsNumeric(this string value)
        {
            return value.IsNumeric(0, value.Length);
        }
        public static bool IsNumeric(this char value)
        {
            return value.ToString().IsNumeric();
        }
        public static bool IsNumberOnly(this string value, int start, int length)
        {
            if (length < 1) { return false; }

            string str = value.Substring(start, length).Trim();

            if (str.Length < 1) { return false; }

            foreach (char chr in str.ToCharArray())
            {
                switch (chr)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;

                    default:
                        return false;
                }
            }

            return true;
        }
        public static bool IsNumberOnly(this string value, int start)
        {
            return value.IsNumberOnly(start, value.Length - start);
        }
        public static bool IsNumberOnly(this string value)
        {
            return value.IsNumberOnly(0, value.Length);
        }
        public static bool IsNumberOnly(this char value)
        {
            return value.ToString().IsNumberOnly();
        }
        public static bool IsTimeValue(this string value)
        {
            string[] _parts = value.Split(':');

            if (_parts.Length < 3) return false;

            if (!_parts[0].IsNumberOnly()) return false;
            if (!_parts[1].IsNumberOnly()) return false;
            if (!_parts[2].IsNumberOnly()) return false;

            int i;

            i = Convert.ToInt32(_parts[0]);
            if ((i < 0) || (i > 24)) return false;

            i = Convert.ToInt32(_parts[1]);
            if ((i < 0) || (i > 59)) return false;

            i = Convert.ToInt32(_parts[2]);
            if ((i < 0) || (i > 59)) return false;

            return true;
        }
        public static DateTime ToDateTime(this string value)
        {
            if (!value.IsTimeValue()) return new DateTime(1, 1, 1, 0, 0, 0);

            string[] _parts = value.Split(':');
            int hr = Convert.ToInt32(_parts[0]);
            int min = Convert.ToInt32(_parts[1]);
            int sec = Convert.ToInt32(_parts[2]);
            double days = 0;
            if (hr >= 24)
            {
                hr -= 24;
                days = 1;
            }

            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hr, min, sec).AddDays(days);
        }
        public static DateTime ToDateTime(this string value, int year, int month, int day)
        {
            if (!value.IsTimeValue()) return new DateTime(1, 1, 1, 0, 0, 0);

            string[] _parts = value.Split(':');
            int hr = Convert.ToInt32(_parts[0]);
            int min = Convert.ToInt32(_parts[1]);
            int sec = Convert.ToInt32(_parts[2]);

            return new DateTime(year, month, day, hr, min, sec);
        }
        public static char LastChar(this string value)
        {
            if (value.Length == 0) { return ' '; }
            char[] _Chars = value.ToCharArray();
            return _Chars[value.Length - 1];
        }
        public static string NextItem(this string value, params char[] sep)
        {
            char[] specials = sep;
            string str;
            int idx;

            idx = value.IndexOfAny(specials);
            if (idx < 0)
            {
                str = value;
            }
            else
            {
                str = value.Substring(0, idx);
            }

            return str;
        }
        public static string NextItem(this string value, char sep)
        {
            char[] specials = sep.ToString().ToArray();
            char[] specials2 = { '<', '[', '{', '(', '|', '%', '"' };
            string str;
            int idx;

            if (value.Substring(0, 1).IndexOfAny(specials2) > -1)
            {
                // value: %A%=1,%B%=1
                str = value.NextItem();
                // str: %A%
                // should be: %A%=1
                if (str.Length < value.Length)
                {
                    idx = value.IndexOfAny(specials, str.Length);
                    if (idx < 0)
                    {
                        str = value;
                    }
                    else
                    {
                        str = value.Substring(0, idx);
                    }
                }
            }
            else
            {
                idx = value.IndexOfAny(specials);

                if (idx < 0)
                {
                    str = value;
                }
                else
                {
                    str = value.Substring(0, idx);
                }
            }

            return str;
        }
        public static string NextItem(this string value)
        {
            if (value == null || value == "") { return ""; }
            char[] specials = { '<', '[', '{', '(', '|', '%', '"' };
            string str;
            char firstChar = value.Substring(0, 1).ToCharArray()[0];
            int idx;

            switch (firstChar)
            {
                case '"':
                    idx = value.IndexOf('"', 1);
                    idx = value.IndexOfAny(specials, idx);
                    str = ItemSubstring(value, idx);
                    break;

                case '{':
                case '(':
                    int id1;
                    int id2;
                    char sep1 = firstChar;
                    char sep2 = '}';
                    if (sep1 == '(') { sep2 = ')'; }
                    // find the first or only sep2 character
                    id2 = value.IndexOf(sep2) + 1;
                    // see if we have another sep1 character
                    id1 = value.IndexOf(sep1, 1) + 1;
                    // we do not have another sep1 or the sep2 is before the sep1
                    // so we have something similar to:
                    // {Dice~1d3}
                    if ((id1 < 1) || (id2 < id1))
                    {
                        str = value.Substring(0, id2);
                    }
                    else
                    {
                        // we should have one of the following forms:
                        // {Line~{Select~{Dice~1d3},1,left,2,center,right},50}
                        // {Loop~{Dice~1d4},{Cap~[Special]}}
                        // {Cap~{AorAn~[Weight]}} [Hair], with {AorAn~[Style]}

                        // we have a sep1 character before the sep2
                        // so count how many sep1 before a sep2
                        string temp; // = value.Substring(0, id2);
                        // temp2 should contain one of the following:
                        // {Line~{Select~{Dice~1d3}
                        // {Loop~{Dice~1d4}
                        // {Cap~{AorAn~[Weight]}
                        int iCnt1; // = temp.Count(sep1);
                        // set count to zero for first pass
                        int iCnt2;
                        do
                        {
                            if (id2 > value.Length)
                            { throw new System.ArgumentException("Mismatched braces."); }
                            temp = value.Substring(0, id2++);
                            iCnt1 = temp.Count(sep1);
                            iCnt2 = temp.Count(sep2);
                        } while (iCnt1 != iCnt2);
                        str = temp;
                    }
                    break;

                case '<':
                case '[':
                case '|':
                case '%':
                    if (firstChar == '<') { idx = value.IndexOf('>', 1); }
                    else if (firstChar == '[') { idx = value.IndexOf(']', 1); }
                    else if (firstChar == '|') { idx = value.IndexOf('|', 1); }
                    else { idx = value.IndexOf('%', 1); }
                    str = ItemSubstring(value, idx);
                    break;

                default:
                    idx = value.IndexOfAny(specials);
                    if (idx < 0)
                    { str = value; }
                    else
                    { str = value.Substring(0, idx); }
                    break;
            }

            return str;
        }
        public static string ToHostPath(this string value)
        {
            string str = value;

            if (str.StartsWith("/cygdrive/"))
            {
                str = str.Substring(10);
                int ic = str.IndexOf('/');
                str = str.Substring(0, ic) + ":" + str.Substring(ic);
            }

            return str;
        }
        public static byte[] ToByteArray(this string value)
        {
            byte[] ByteArray = new byte[value.Length];

            for (int i = 0; i < value.Length; i++)
            {
                ByteArray[i] = (byte)value[i];
            }

            return ByteArray;
        }
        public static byte[] ToByteArray(this string value, int start, int length)
        {
            return ToByteArray(value.Substring(start, length));
        }
        public static String ByteArrayToString(this string value, params byte[] inValue)
        {
            return value.ByteArrayToString(0, inValue.Length, inValue);
        }
        public static String ByteArrayToString(this string value, int length, params byte[] inValue)
        {
            return value.ByteArrayToString(0, length, inValue);
        }
        public static String ByteArrayToString(this string value, int start, int length, params byte[] inValue)
        {
            if ((start > inValue.Length) || (start < 0)) { return null; }

            return Encoding.Default.GetString(inValue, start, length);
        }
        public static string CharArrayToString(this string value, params char[] inValue)
        {
            return value.CharArrayToString(0, inValue.Length, inValue);
        }
        public static string CharArrayToString(this string value, int length, params char[] inValue)
        {
            return value.CharArrayToString(0, length, inValue);
        }
        public static string CharArrayToString(this string value, int start, int length, params char[] inValue)
        {
            if ((start > inValue.Length) || (start < 0)) { return null; }

            return Encoding.Default.GetString(Encoding.Default.GetBytes(inValue), start, length);
        }
        public static string[] SplitAndTrim(this string value, char sep)
        {
            if (value == null) return null;
            string[] temp = value.Split(sep);
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = temp[i].Trim();
            }
            return temp;
        }
        public static string[] Lines(this string value)
        {
            string[] str = value.Split('\n');
            return str;
        }
        public static string[] Lines(this string value, char sep)
        {
            string[] str = value.Split(sep);
            return str;
        }
        public static List<string> ToList(this string[] value, string[] sTemp)
        {
            List<string> vTemp = new List<string>();
            if (!(value == null))
            {
                foreach (var item in value)
                {
                    vTemp.Add(item);
                }
            }
            return vTemp;
        }
        public static List<string> ToList(this string[] value, string[] sTemp, bool ignoreBlanks)
        {
            if (!ignoreBlanks)
            {
                return ToList(value, sTemp);
            }

            List<string> vTemp = new List<string>();
            if (!(value == null))
            {
                foreach (var item in value)
                {
                    if (!(item == ""))
                    {
                        vTemp.Add(item);
                    }
                }
            }
            return vTemp;
        }
        public static string Unsplit(this string[] value, char delim)
        {
            string outStr = string.Empty;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != "")
                {
                    outStr += value[i] + delim;
                }
            }

            return outStr.TrimEnd(delim);
        }
        private static string ItemSubstring(string value, int idx)
        {
            if (idx < 0)
            {
                return value;
            }
            else
            {
                return value.Substring(0, idx + 1);
            }
        }
    }

    public static class ArrayExtension
    {
        public static string UnSplit(this Array value, char sep)
        {
            return UnSplit(value, sep.ToString());
        }
        public static string UnSplit(this Array value, string sep)
        {
            if (value == null) return null;

            string str = "";

            if (value.Length > 0)
            {
                str = value.GetValue(0).ToString();

                for (int i = 1; i < value.Length; i++)
                {
                    str += sep + value.GetValue(i).ToString();
                }
            }

            return str;
        }
    }

    public static class ArrayListExtension
    {
        public static string UnSplit(this ArrayList value, char sep)
        {
            return UnSplit(value, sep.ToString());
        }
        public static string UnSplit(this ArrayList value, string sep)
        {
            string str = "";

            if (value.Count > 0)
            {
                str = (string)value[0];

                for (int i = 1; i < value.Count; i++)
                {
                    str += sep + (string)value[i];
                }
            }

            return str;
        }
    }

    public static class ListExtension
    {
        public static string UnSplit(this List<string> value, char sep)
        {
            return UnSplit(value, sep.ToString());
        }
        public static string UnSplit(this List<string> value, string sep)
        {
            string str = "";

            if (value.Count > 0)
            {
                str = value[0];

                for (int i = 1; i < value.Count; i++)
                {
                    str += sep + value[i];
                }
            }

            return str;
        }
    }

    public static class ByteExtensions
    {
        public static string BytesToHexString(this byte[] value, int start, int length)
        {
            string ret = "";
            if (length > (value.Length - start)) { length = (value.Length - start); }
            for (int i = 0; i < length; i++)
            {
                ret += value[i + start].ToString("X2");
            }
            return ret;
        }

        public static string BytesToHexString(this byte[] value, int start)
        {
            return BytesToHexString(value, start, value.Length - start);
        }

        public static string BytesToHexString(this byte[] value)
        {
            return BytesToHexString(value, 0, value.Length);
        }

        public static string BytesToString(this byte[] value, int start, int length)
        {
            string ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += value[i + start];
            }
            return ret;
        }

        public static string BytesToString(this byte[] value, int start)
        {
            return BytesToString(value, start, value.Length - start);
        }

        public static string BytesToString(this byte[] value)
        {
            return BytesToString(value, 0, value.Length);
        }

        public static Char[] BytesToChars(this byte[] value, int start, int length)
        {
            Char[] ret = new Char[length];

            if (length > (value.Length - start)) { length = (value.Length - start); }
            for (int i = 0; i < length; i++)
            {
                ret[i] = (Char)value[i + start];
            }
            return ret;
        }

        public static Char[] BytesToChars(this byte[] value, int start)
        {
            return BytesToChars(value, start, value.Length - start);
        }

        public static Char[] BytesToChars(this byte[] value)
        {
            return BytesToChars(value, 0, value.Length);
        }

        public static string CharsToString(this byte[] value, int start, int length)
        {
            string ret = "";
            for (int i = start; i < (length - start); i++)
            {
                ret += (Char)value[i];
            }
            return ret;
        }

        public static string CharsToString(this byte[] value, int start)
        {
            return CharsToString(value, start, value.Length - start);
        }

        public static string CharsToString(this byte[] value)
        {
            return CharsToString(value, 0, value.Length);
        }
    }

    public static class CharExtensions
    {
        public static string CharsToString(this char[] value, int start, int length)
        {
            string ret = "";
            for (int i = start; i < (length - start); i++)
            {
                ret += value[i];
            }
            return ret;
        }

        public static string CharsToString(this char[] value, int start)
        {
            return CharsToString(value, start, value.Length - start);
        }

        public static string CharsToString(this char[] value)
        {
            return CharsToString(value, 0, value.Length);
        }

        public static byte[] CharsToBytes(this char[] value, int start, int length)
        {
            byte[] ret = new byte[length];

            if (length > (value.Length - start)) { length = (value.Length - start); }
            for (int i = 0; i < length; i++)
            {
                ret[i] = (byte)value[i + start];
            }
            return ret;
        }

        public static byte[] CharsToBytes(this char[] value, int start)
        {
            return CharsToBytes(value, start, value.Length - start);
        }

        public static byte[] CharsToBytes(this char[] value)
        {
            return CharsToBytes(value, 0, value.Length);
        }
    }
}
