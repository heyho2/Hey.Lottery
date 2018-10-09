using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Huach.Framework.Helper
{
    public static class ValidateCode
    {
        public static Image CreateImage(string validateCode, int letterWidth = 200, int letterHeight = 60)
        {
            Bitmap image = new Bitmap(letterWidth, letterHeight);
            Graphics g = Graphics.FromImage(image);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, letterWidth, letterHeight);
            //生成随机生成器
            Random random = new Random();
            //清空图片背景色
            g.Clear(Color.White);
            
            Font font = new Font(FontFamily.GenericSerif, (int)(letterHeight * 0.6), (FontStyle.Bold | FontStyle.Italic));

            var xindex = letterWidth / validateCode.Length + (letterWidth / validateCode.Length - letterHeight * 0.8) / 2;
            for (int x = 0; x < validateCode.Length; x++)
            {
                string letter = validateCode.Substring(x, 1);
                g.DrawString(letter, font, new SolidBrush(Color.Black), (float)(x * xindex), random.Next(0, 15));
            }
            //画图片的干扰线
            Pen linePen = new Pen(new SolidBrush(GetRandomColor()), (float)(letterWidth / 100));
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(linePen, new Point(x1, y1), new Point(x2, y2));
            }
            return image;
        }

        private static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            int int_Red = RandomNum_First.Next(210);
            int int_Green = RandomNum_Sencond.Next(180);
            int int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
        /// <summary>
        /// 生成随机的字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomCode(int codeLength = 4)
        {
            string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            //添加随机的五个字母
            for (int x = 0; x < codeLength; x++)
            {
                string letter = letters.Substring(r.Next(0, letters.Length - 1), 1);
                sb.Append(letter);
            }
            return sb.ToString();
        }
    }
}
