using System;
using System.Collections.Generic;
using System.Numerics;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kurs2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static char[] alfabet = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е','Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', ' ' };
        private string CodeDecode(string text, int k)
        {
            const string sad = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            var fullAlfabet = sad + sad.ToLower() + " ";
            var n = fullAlfabet.Length;
            var retVal = " ";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (n+index + k) % n;
                    retVal += fullAlfabet[codeIndex];
                }
            }
            return retVal;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string codemess = textBox1.Text;
            int key = Convert.ToInt32(textBox3.Text);
            textBox2.Text = CodeDecode(codemess, key);
            textBox2.Text = textBox2.Text.Remove(0, 1);


        }
        private void button2_Click(object sender, EventArgs e)
        {
            string codemess = textBox2.Text;
            int key = Convert.ToInt32(textBox3.Text);
            textBox2.Text = CodeDecode(codemess, -key);
            textBox2.Text = textBox2.Text.Remove(0, 1);
        }


        //RSA//
        private void button4_Click(object sender, EventArgs e)
        {
            int n = (int)(Convert.ToInt64(textp.Text) * Convert.ToInt64(textq.Text));
            int f = (int)((Convert.ToInt64(textp.Text) - 1) * (Convert.ToInt64(textq.Text) - 1));
            textn.Text = Convert.ToString(n);
            textf.Text = Convert.ToString(f);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int f = Convert.ToInt32(textf.Text);
            int d = f-3;
            for (int i = 2; i <= f; i++)
                if ((f % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 2;
                }

            textd.Text = Convert.ToString(d);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            long f = Convert.ToInt64(textf.Text);
            long d = Convert.ToInt64(textd.Text);
            long e1 = 2;
            while (true)
            {
                if (((e1 * d) - 1) % f == 0)
                    break;
                else
                    e1++;
            }
            texte.Text = Convert.ToString(e1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<string> result = new List<string>();
            BigInteger res;
            string s = (txt1.Text);
            long e1 = Convert.ToInt64(texte.Text);
            long n = Convert.ToInt64(textn.Text);
            for (int i = 0; i < s.Length; i++)
            {
                int bu = Array.IndexOf(alfabet, s[i]);
                res = new BigInteger(bu);
                res = BigInteger.Pow(res, (int)e1);
                BigInteger n_ = new BigInteger((int)n);
                res = res % n_;               
                result.Add(res.ToString());
            }
            txt2.Text += String.Join(Environment.NewLine, result);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<string> input = new List<string>(txt2.Lines);
            BigInteger bi;
            long n = Convert.ToInt64(textn.Text);
            string result = "";
            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)Convert.ToInt64(textd.Text));
                BigInteger n_ = new BigInteger((int)n);
                bi = bi % n_;
                int index = Convert.ToInt32(bi.ToString());
                result += alfabet[index].ToString();
            }
            txt3.Text = String.Join(Environment.NewLine, result);
        }

        //hesh//
        private void button9_Click(object sender, EventArgs e)
        {

            txt2.Clear();
            if (txt1.Text != "")
            {
                string text = txt1.Text;
                int textlength = text.Length;
                 string alfabet = "-абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
                int h = 4;

                for (int i = 0; i < textlength; i++)
                {
                    int index = alfabet.IndexOf(text[i]);
                    double vskobke = Convert.ToDouble(h + index);
                    double sqr = 2;

                    h = (Convert.ToInt32(Math.Pow(vskobke, sqr)) % Convert.ToInt32(textn.Text));
                    txt2.Text += "(" + vskobke + ")^2 mod " + textn.Text + " = " + h + "\r\n";
                }
               txthesh.Text =Convert.ToString(h);
            }
        }
        //podpis//

        private void button10_Click(object sender, EventArgs e)
        {
            long hash = Convert.ToInt64(txthesh.Text);
            long n = Convert.ToInt64(textn.Text);

            BigInteger bi;

            bi = new BigInteger(Convert.ToDouble(hash));
            bi = BigInteger.Pow(bi, (int)Convert.ToInt64(textd.Text));

            BigInteger n_ = new BigInteger((int)n);

            bi = bi % n_;

           txticp.Text = bi.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            long n = Convert.ToInt64(textn.Text);

            BigInteger bi;

            bi = new BigInteger(Convert.ToInt64(txticp.Text));
            bi = BigInteger.Pow(bi, (int)Convert.ToInt64(texte.Text));

            BigInteger n_ = new BigInteger((int)n);

            bi = bi % n_;

            txtproverka.Text = bi.ToString();
        }
        //gost//
        private void button3_Click(object sender, EventArgs e)
        {
            textres.Clear();
            string text = textgost.Text;
            string L0 = textgost.Text.Substring(0, 4);
            string R0 = textgost.Text.Substring(4, 4);
            string key = textgost2.Text.Substring(0, 4);
            
           textres.Text += "Результат выполнения прграммы:\r\n";

            byte[] R1 = Encoding.GetEncoding(1251).GetBytes(R0);
            byte[] key1 = Encoding.GetEncoding(1251).GetBytes(key);
            byte[] L1 = Encoding.GetEncoding(1251).GetBytes(L0);
            byte[] text1 = Encoding.GetEncoding(1251).GetBytes(text);

            textres.Text += "Исходный текст в двоичном виде -   " + string.Join(" ", text1.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))) + "\r\n";
            textres.Text += "Значение блока L0 -   " + string.Join(" ", L1.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))) + "\r\n";
            textres.Text += "Значение блока R0 -   " + string.Join(" ", R1.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))) + "\r\n";
            textres.Text += "Значение ключа  -   " + string.Join(" ", key1.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))) + "\r\n";
    
            byte[] SymMass = new byte[R1.Length];
            byte[] Mass = new byte[R1.Length];
            byte[] sdvigMass = new byte[R1.Length];
            byte[] newsdvig = new byte[R1.Length];
            byte[] SdvigSumm = new byte[R1.Length];
            for (int i = 3; i > -1; i--)
            {

                if (i != 3)
                {
                    if (checkByte(Convert.ToString(Convert.ToInt64(R1[i + 1]), 2), Convert.ToString(Convert.ToInt64(key1[i + 1]), 2)) == true)
                    {
                        Mass[i] = Convert.ToByte(Obrez(R1[i]));
                        SymMass[i] = (byte)(Mass[i] + key1[i]);
                    }
                    else
                    {
                        Mass[i] = R1[i];
                        SymMass[i] = (byte)(Mass[i] + key1[i]);
                    }
                }
                else
                {
                    Mass[i] = R1[i];
                    SymMass[i] = (byte)(Mass[i] + key1[i]);
                }

            }

         
            
            string newsumm = "";
            for (int i = 0; i < 4; i++)
            {       
                newsumm += Convert.ToString(Convert.ToInt64(SymMass[i]), 2) + " ";
            }
            textres.Text += "Результат суммирования -   " + newsumm + "\r\n";
            //string s1 = "00100111001111101110000001011111";          
            string sdvigRsult = sdvig(newsumm);
            textres.Text += "Результат сдвига на 11 бит -   " + sdvigRsult.Substring(0, 8) + " " + sdvigRsult.Substring(8, 8) + " " + sdvigRsult.Substring(16, 8) + " " + sdvigRsult.Substring(24, 8) + " " + "\r\n";

            int numOfBytes = (int)Math.Ceiling(sdvigRsult.Length / 8m);

                       
            for (int i = 0; i < numOfBytes; i++)
            {
                sdvigMass[i] = Convert.ToByte(sdvigRsult.Substring(i * 8, 8), 2);
            }
            string summa = "";
            string afterSdvigResult = "";
            string afterSdvigResult1 = "";
            for (int p = 0; p < 4; p++)
            {          
                 
                 newsdvig[p] = sdvigMass[p];
                 SdvigSumm[p] = (byte)(newsdvig[p] ^ L1[p]);
                summa = Convert.ToString(Convert.ToInt64(SdvigSumm[p]), 2);
                afterSdvigResult1 = Convert.ToString(Convert.ToInt64(SdvigSumm[p]), 2);
                if (summa.Length == 1)
                    afterSdvigResult1 = "0000000" + afterSdvigResult1;
                if (summa.Length == 2)
                    afterSdvigResult1 = "000000" + afterSdvigResult1;
                if (summa.Length == 3)
                    afterSdvigResult1 = "00000" + afterSdvigResult1;
                if (summa.Length == 4)
                    afterSdvigResult1 = "0000" + afterSdvigResult1;
                if (summa.Length == 5)
                    afterSdvigResult1 = "000" + afterSdvigResult1;
                if (summa.Length == 6)
                    afterSdvigResult1 = "00" + afterSdvigResult1;
                if (summa.Length == 7)
                    afterSdvigResult1 = "0" + afterSdvigResult1;
                afterSdvigResult += afterSdvigResult1 + " ";
            }
            
           string d1 = "";     
            
            textres.Text += "Результат сложения - " + afterSdvigResult + "\r\n";
        }
        bool checkByte(string s1, string s2)
        {
            if (s1.Length == 8 && s2.Length == 8)
            {
                if (s1[0] == '1' && s2[0] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        string Obrez(byte Cut)
        {
            string cutstr = Convert.ToString(Convert.ToInt64(Cut + 1), 10);
            if (cutstr == "256") cutstr = "0";
            return cutstr;
        }
        string sdvig(string str)
        {
            string neArr = str;
            neArr = neArr.Replace(" ", "");
            int LeghthStr = neArr.Length;
            char[] cstr = neArr.ToCharArray();
            char[] buff1=  new char [11];
            char[] buff2 = new char[1];
            string s;
        for(int x=0;x<11;x++)
            {
                buff1[x] = cstr[x];
            }
            buff2[0] = cstr[31];
            StringBuilder sb = new StringBuilder();
            foreach (char ch in buff1)
                sb.Append(ch);
             s = sb.ToString();
            for (int i = 0; i < 11; i++)
            {
                for (int f = 1; f < LeghthStr; f++)
                {
                    if (f == 1)
                    {
                        char buff = cstr[0];
                        cstr[0] = cstr[1];
                        cstr[LeghthStr - 1] = buff;
                    }
                    else
                    {
                        char buff = cstr[f - 1];
                        cstr[f - 1] = cstr[f];
                        cstr[f - 2] = buff;
                    }
                    
                }
            }
            string result = new string(cstr);            
            result = result.Substring(0, 21);
            result += s;
            char[] q = result.ToArray();
            q[20] = buff2[0];
            string d = new string(q);
            return d;
        }
        //des
        private void button12_Click(object sender, EventArgs e)
        {
            string text = textdes.Text.Substring(0, 8);
            string key = txtdes.Text.Substring(0, 7);

            List<byte> byteList = new List<byte>(Encoding.GetEncoding(1251).GetBytes(key));

            byte[] textb = Encoding.GetEncoding(1251).GetBytes(text);
            byte[] keyb = Encoding.GetEncoding(1251).GetBytes(key);

            string resultP = "";
            for (int i = 0; i < 8; i++)
            {
                resultP += Convert.ToString(Convert.ToInt64(textb[i]), 2) + " ";
            }    
            textdes1.Text += "Исходный текст - " + resultP + "\r\n";
            resultP = PereStanovka(resultP);
            textdes1.Text += "Результат перестановки - " + resultP + "\r\n";

            string L0 = resultP.Substring(0, 32);
            string R0 = resultP.Substring(32, 32);
            textdes1.Text += "Блок L0 " + L0 + "\r\n" + "Блок R0 = " + R0 + "\r\n"+ "\r\n";

            string SixBitText = "";

            for (int t = 0; t < 8; t++)
            {
                char[] strArr = R0.ToCharArray();
                if (t == 0)
                {
                    string buff = R0.Substring(0, 4);
                    SixBitText += strArr[R0.Length - 1] + buff + strArr[4] + " ";
                }
                else
                {
                    if (t == 7)
                    {
                        string buff = R0.Substring(t * 4, 4);
                        SixBitText += strArr[(t * 4) - 1] + buff + strArr[0] + " ";
                    }
                    else
                    {
                        string buff = R0.Substring(t * 4, 4);
                        SixBitText += strArr[(t * 4) - 1] + buff + strArr[t * 4 + 4] + " ";
                    }
                }
            }
            textdes1.Text += "Расширенный блок -  " + SixBitText + "\r\n";

            string keyNormTxt = "";
            for (int u = 0; u < 7; u++)
            {
                keyNormTxt += Convert.ToString(Convert.ToInt64(keyb[u]), 2) + " ";
            }

            string KeyCut = "";
            textdes1.Text += "Ключ -  " + keyNormTxt + "\r\n";
            for (int i = 0; i < 7; i++)
            {
                if (i != 6)
                {
                    KeyCut += CutByte(keyb[i], 7) + "   ";
                }
                else
                {
                    KeyCut += CutByte(keyb[i], 6) + "   ";
                }
            }

            textdes1.Text += "Уменьшенный ключ  -  " + KeyCut + "\r\n";

            KeyCut = KeyCut.Trim();
            KeyCut = KeyCut.Replace(" ", "");

             string KeySixBit = "";

             for (int h = 0; h < 8; h++)
             {
                 KeySixBit += KeyCut.Substring(h * 6, 6) + " ";
             }
             textdes1.Text += "Ключ по 6  " + KeySixBit + "\r\n";

            //Суммирование
            KeySixBit = KeyCut.Trim();
            KeySixBit = KeyCut.Replace(" ", "");
            SixBitText = SixBitText.Trim();
            SixBitText = SixBitText.Replace(" ", "");
            byte[] R0B = new byte[SixBitText.Length / 6];
            byte[] KEYBB = new byte[KeyCut.Length / 6];
            byte[] sd= new byte[KeyCut.Length / 6];

            for (int i = 0; i < 8; i++)
            {
                R0B[i] = Convert.ToByte(SixBitText.Substring(i * 6, 6), 2);
                KEYBB[i] = Convert.ToByte(KeySixBit.Substring(i * 6, 6), 2);
            }
            string resukt1 = "";
            string resukt2 = "";
            string resukt3 = "";
            byte[] ResultSym = new byte[KeySixBit.Length / 6];
            for (int p = 0; p <8; p++)
                
            {
                {
                    sd[p] = Convert.ToByte(R0B[p]);
                    ResultSym[p] = (byte)(sd[p] ^ KEYBB[p]);
                    resukt1 = Convert.ToString(Convert.ToInt64(ResultSym[p]), 2) ;
                    resukt2 = Convert.ToString(Convert.ToInt64(ResultSym[p]), 2) ;
                   
                    if (resukt1.Length == 1)
                        resukt2 = "00000" + resukt2;
                    if (resukt1.Length == 2)
                        resukt2 = "0000" + resukt2;
                    if (resukt1.Length == 3)
                        resukt2 = "000" + resukt2;
                    if (resukt1.Length == 4)
                        resukt2 = "00" + resukt2;
                    if (resukt1.Length == 5)
                        resukt2 = "0" + resukt2;
                    
                    resukt3 += resukt2+" ";
                }
                
            }
            textdes1.Text += "Результат суммирования -  " + resukt3 + "\r\n";         
            string SBox = "0001 1110 1110 0101 0001 0000 0011 1110";
            textdes1.Text += "Замена s-box - 0001 1110 1110 0101 0001 0000 0011 1110" + "\r\n";
            string ResultSixPer = PereStanovkaSix(SBox);
            textdes1.Text += "Результат перестановки 2 -  " + ResultSixPer + "\r\n";
            string ForFinal = L0 + ResultSixPer;
            textdes1.Text += "Обхединение блоков L0 и R0  -" + ForFinal + "\r\n";
            string AfterFinalPere = PereFinal(ForFinal);
            string FinalFormat = "";
            for (int h = 0; h < 8; h++)
            {
                FinalFormat += AfterFinalPere.Substring(h * 8, 8) + " ";
            }
            textdes1.Text += "Итоговая перестановка   " + FinalFormat + "\r\n";
        }

        string CutByte(byte Cut, int Index)
        {
            string cutstr = Convert.ToString(Convert.ToInt64(Cut), 2);
            return cutstr.Substring(0, Index);
        }

        string PereStanovka(string str)
        {
            str = str.Replace(" ", "");
            char[] arrChar = str.ToCharArray();
            char[] zamenChar = new char[64];
            int[] IndeX = new int[64] {  58 , 50 , 42 , 34 , 26 , 18 , 10 , 2 ,
                                         60 , 52 , 44 , 36 , 28 , 20 , 12 , 4 ,
                                         62 , 54 , 46 , 38 , 30 , 22 , 14 , 6 ,
                                         64 , 56 , 48 , 40 , 32 , 24 , 16 , 8 ,
                                         57 , 49 , 41 , 33 , 25 , 17 , 9 , 1 ,
                                         59 , 51 , 43 , 35 , 27 , 19 , 11 , 3 ,
                                         61 , 53 , 45 , 37 , 29 , 21 , 13 , 5 ,
                                         63 , 55 , 47 , 39 , 31 , 23 , 15 , 7};
            for (int i = 0; i < 64; i++)
            {
                zamenChar[i] = arrChar[IndeX[i]-1];
            }
            string result = new string(zamenChar);
            return result;
        }

        string PereStanovkaSix(string str)
        {
            str = str.Trim();
            str = str.Replace(" ", "");
            char[] arrChar = str.ToCharArray();
            char[] zamenChar = new char[str.Length];
            int[] IndeX = new int[32] {  16, 7, 20, 21, 29, 12, 28, 17,
                                            1, 15, 23, 26, 5, 18, 31, 10,
                                            2, 8, 24, 14, 32, 27, 3, 9,
                                            19, 13, 30, 6, 22, 11, 4, 25};
            for (int i = 0; i < 32; i++)
            {
                zamenChar[i] = arrChar[IndeX[i] - 1];
            }
            string result = new string(zamenChar);
            return result;
        }

        string PereFinal(string str)
        {
            str = str.Trim();
            str = str.Replace(" ", "");
            char[] arrChar = str.ToCharArray();
            char[] zamenChar = new char[str.Length];
            int[] IndeX = new int[64] {  40, 8, 48, 16, 56, 24, 64, 32,
                                        39, 7, 47, 15, 55, 23, 63, 31,
                                        38, 6, 46, 14, 54, 22, 62, 30,
                                        37, 5, 45, 13, 53, 21, 61, 29,
                                        36, 4, 44, 12, 52, 20, 60, 28,
                                        35, 3, 43, 11, 51, 19, 59, 27,
                                        34, 2, 42, 10, 50, 18, 58, 26,
                                        33, 1, 41, 9, 49, 17, 57, 25};
            for (int i = 0; i < 64; i++)
            {
                zamenChar[i] = arrChar[IndeX[i] - 1];
            }
            string result = new string(zamenChar);
            return result;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
    }






   

   
           
    

