using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBTool
{
    public  class Salary
    {
        /// <summary>
        /// 薪資解密
        /// </summary>
        /// <param name="ENCODE_TYPE"></param>
        /// <param name="VALT"></param>
        /// <returns></returns>
        public static decimal ENCODE(int ENCODE_TYPE, decimal VAL)
        {
            string[] vals = VAL.ToString().Split(new char[] { '.' });
            decimal VALT = Convert.ToDecimal(vals[0]);
            decimal VALD = Convert.ToDecimal((vals.Length == 2) ? "0." + vals[1] : "0");
            
            string LCFLAG = (VALT < 0) ? "-" : "+";
            VALT = Math.Abs(VALT);
            string VALSTR = VALT.ToString().Trim();
            string STR1 = "3761532470658472653034873";
            string LL = "";
            int VALLEN = 0;
            int STARTPOS = 0;
            switch (ENCODE_TYPE)
            {
                //Encrypt
                case 1:
                    VALLEN = VALSTR.Length;
                    STARTPOS = 0;
                    for (int I = 1; I <= VALLEN; I++)
                    {
                        STARTPOS = STARTPOS + int.Parse(VALSTR.Substring((I - 1 >= 0) ? I - 1 : 0, 1));
                        STARTPOS = STARTPOS % 10;
                    }
                    for (int I = 1; I <= VALLEN; I++)
                    {
                        int YY = 0;
                        int index = STARTPOS + I - 1 - 1;
                        if (index >= 0) YY = int.Parse(STR1.Substring(index, 1));
                        int WW = int.Parse(VALSTR.Substring((I - 1 >= 0) ? I - 1 : 0, 1)) + YY;
                        int iTmp = Math.Abs(WW) % 10;
                        LL += iTmp.ToString();
                    }
                    LL += VALLEN.ToString() + STARTPOS.ToString();
                    break;
                //Decryp                        
                case 2:
                    string AA = VALSTR.Trim().Substring((VALSTR.Trim().Length - 2 >= 0) ? VALSTR.Trim().Length - 2 : 0, 2);
                    STARTPOS = int.Parse(AA.Substring((AA.Length - 1 >= 0) ? AA.Length - 1 : 0, 1));
                    VALLEN = int.Parse(AA.Substring(0, 1));
                    VALSTR = VALSTR.Substring(0, VALSTR.Length - 2).PadLeft(VALLEN, '0');
                    for (int I = 1; I <= VALLEN; I++)
                    {
                        int ZZ = int.Parse(VALSTR.Substring((I - 1 >= 0) ? I - 1 : 0, 1));
                        int index = STARTPOS + I - 1 - 1;
                        int YY = 0;
                        if (index >= 0) YY = int.Parse(STR1.Substring(index, 1));
                        int WW = ZZ - YY;
                        WW = (WW < 0) ? 10 + WW : WW;
                        int iTmp = Math.Abs(WW) % 10;
                        LL += iTmp.ToString();
                    }
                    break;
            }
            if (LL.Length == 0) LL = "0";
            LL = LCFLAG + LL;

            decimal ret = decimal.Parse(LL);
            if (VALD > 0) ret += LCFLAG == "-" ? VALD * -1 : VALD;
            return ret;
        }
    }
}