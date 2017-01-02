using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;

namespace UnicodeAsciiConverter
{
    public class Converter
    {
        Dictionary<string, string> ar; Dictionary<string, string> aM;
        Dictionary<string, string> aR; Dictionary<string, string> aS;

        public Converter()
        {
            ar = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\ar.json"));
            aR = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\aRR.json"));
            aM = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\aM.json"));
            aS = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\aS.json"));
        }
        string ci(string w)
        {
            var cY = 0;
            for (var i = 0; i < w.Length; i++)
            {
                if (i < w.Length && ao(w.ElementAt(i)))
                {
                    var j = 1;
                    while (v(w.ElementAt(i - j)))
                    {
                        if (i - j < 0) break;
                        if (i - j <= cY) break;
                        if (D(w.ElementAt(i - j - 1))) j += 2;
                        else break;
                    }
                    var R = w.substring(0, i - j);
                    R += w.ElementAt(i);
                    R += w.substring(i - j, i);
                    R += w.substring(i + 1, w.Length);
                    w = R;
                    cY = i + 1;
                    continue;
                }
                if (i < w.Length - 1 && D(w.ElementAt(i)) && w.ElementAt(i - 1) == 'র' && !D(w.ElementAt(i - 2)))
                {
                    var j = 1;
                    var aZ = 0;
                    while (true)
                    {
                        if (v(w.ElementAt(i + j)) && D(w.ElementAt(i + j + 1))) j += 2;
                        else if (v(w.ElementAt(i + j)) && ao(w.ElementAt(i + j + 1)))
                        {
                            aZ = 1;
                            break;
                        }
                        else break;
                    }
                    var R = w.substring(0, i - 1);
                    R += w.substring(i + j + 1, i + j + aZ + 1);
                    R += w.substring(i + 1, i + j + 1);
                    R += w.ElementAt(i - 1);
                    R += w.ElementAt(i);
                    R += w.substring(i + j + aZ + 1, w.Length);
                    w = R;
                    i += (j + aZ);
                    cY = i + 1;
                    continue;
                }
            }
            return w;
        }
        string fb = "0123456789ABCDEF";

        string fa(char d)
        {
            var h = fb.substr(d & 15, 1);
            while (d > 15)
            {
                d >>= 4;
                h = fb.substr(d & 15, 1) + h;
            }
            while (h.Length < 4) h = "0" + h;
            return h;
        }

        string bF(string line, bool ef)
        {
            var text = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (eu(line.ElementAt(i))) text += line.ElementAt(i);
                else text += "&#" + (ef ? fa(line.ElementAt(i)).ToString() : line.charCodeAt(i).ToString()) + ";";
            }
            return text;
        }
        public string Convert(string line)
        {
            string az = "bangla";
            var G = ar;
            if (az == "bijoy") G = ar;
            else if (az == "somewherein") G = aM;
            else if (az == "boisakhi") G = aR;
            else if (az == "bangsee") G = aS;
            else if (az == "bornosoft")
            {
                return line;
            }
            else if (az == "phonetic")
            {
                return line;
            }
            else if (az == "htmlsafehex")
                return bF(line, true);
            else if (az == "htmlsafedec")
                return bF(line, false);

            line = line.replace("ো", "ো");
            line = line.replace("ৌ", "ৌ");
            line = ci(line);
            foreach (var eI in G)
            {
                line = line.replace(eI.Key, eI.Value);
            }
            return line;
        }

        bool bA(char e)
        {
            if (e == '০' || e == '১' || e == '২' || e == '৩' || e == '৪' || e == '৫' || e == '৬' || e == '৭' || e == '৮' || e == '৯') return true;
            return false;
        }

        bool ao(char e)
        {
            if (e == 'ি' || e == 'ৈ' || e == 'ে') return true;
            return false;
        }

        bool aJ(char e)
        {
            if (e == 'া' || e == 'ো' || e == 'ৌ' || e == 'ৗ' || e == 'ু' || e == 'ূ' || e == 'ী' || e == 'ৃ') return true;
            return false;
        }

        bool ah(char e)
        {
            if (ao(e) || aJ(e)) return true;
            return false;
        }

        bool v(char e)
        {
            if (e == 'ক' || e == 'খ' || e == 'গ' || e == 'ঘ' || e == 'ঙ' || e == 'চ' || e == 'ছ' || e == 'জ' || e == 'ঝ' || e == 'ঞ' || e == 'ট' || e == 'ঠ' || e == 'ড' || e == 'ঢ' || e == 'ণ' || e == 'ত' || e == 'থ' || e == 'দ' || e == 'ধ' || e == 'ন' || e == 'প' || e == 'ফ' || e == 'ব' || e == 'ভ' || e == 'ম' || e == 'শ' || e == 'ষ' || e == 'স' || e == 'হ' || e == 'য' || e == 'র' || e == 'ল' || e == 'য়' || e == 'ং' || e == 'ঃ' || e == 'ঁ' || e == 'ৎ') return true;
            return false;
        }

        bool Q(char e)
        {
            if (e == 'অ' || e == 'আ' || e == 'ই' || e == 'ঈ' || e == 'উ' || e == 'ঊ' || e == 'ঋ' || e == 'ঌ' || e == 'এ' || e == 'ঐ' || e == 'ও' || e == 'ঔ') return true;
            return false;
        }

        bool aF(char e)
        {
            if (e == 'ং' || e == 'ঃ' || e == 'ঁ') return true;
            return false;
        }

        bool bS(string e)
        {
            if (e == "্য" || e == "্র") return true;
            return false;
        }

        bool D(char e)
        {
            if (e == '্') return true;
            return false;
        }

        bool eT(char e)
        {
            if (bA(e) || ah(e) || v(e) || Q(e) || aF(e) || bS(e.ToString()) || D(e)) return true;
            return false;
        }

        bool eu(char e)
        {
            if (e >= 0 && e < 128) return true;
            return false;
        }

        bool cy(string e)
        {
            if (e == " " || e == "	" || e == "\n" || e == "\r") return true;
            return false;
        }

        string cJ(string e)
        {
            var t = "";
            if (e == "া") t = "আ";
            else if (e == "ি") t = "ই";
            else if (e == "ী") t = "ঈ";
            else if (e == "ু") t = "উ";
            else if (e == "ূ") t = "ঊ";
            else if (e == "ৃ") t = "ঋ";
            else if (e == "ে") t = "এ";
            else if (e == "ৈ") t = "ঐ";
            else if (e == "ো") t = "ও";
            else if (e == "ো") t = "ও";
            else if (e == "ৌ") t = "ঔ";
            else if (e == "ৌ") t = "ঔ";
            return t;
        }

        string bc(string e)
        {
            var t = "";
            if (e == "আ") t = "া";
            else if (e == "ই") t = "ি";
            else if (e == "ঈ") t = "ী";
            else if (e == "উ") t = "ু";
            else if (e == "ঊ") t = "ূ";
            else if (e == "ঋ") t = "ৃ";
            else if (e == "এ") t = "ে";
            else if (e == "ঐ") t = "ৈ";
            else if (e == "ও") t = "ো";
            else if (e == "ঔ") t = "ৌ";
            return t;
        }


    }

    public static class Ext
    {
        public static string substring(this string str, int from, int to)
        {
            return str.Substring(from, to - from);
        }

        public static string substr(this string str, int start, int length)
        {
            return str.Substring(start, length);
        }
        public static string replace(this string str, string pattern, string replacement)
        {
            return Regex.Replace(str, pattern, replacement);
        }
        public static int charCodeAt(this string str, int index)
        {
            return str.ElementAt(index);
        }
    }
}
