using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Kids.Utility.Captcha
{
    public sealed class CaptchaImage
    {
        private string familyName;
        private int height;
        private readonly Random random;
        private readonly string text;
        private int width;

     
        public CaptchaImage(string s, int width, int height, string familyName)
        {
            random = new Random();
            text = s;
            SetDimensions(width, height);
            SetFamilyName(familyName);
            GenerateImage();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Image.Dispose();
            }
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        private void GenerateImage()
        {
            Font font;
            Bitmap bitmap = new Bitmap(width, height, (PixelFormat)0x26200a);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = (SmoothingMode)4;
            Rectangle rectangle = new Rectangle(0, 0, width, height);
            HatchBrush brush = new HatchBrush((HatchStyle)0x22, Color.LightGray, Color.White);
            graphics.FillRectangle(brush, rectangle);
            float num = rectangle.Height + 1;
            do
            {
                num--;
                font = new Font(familyName, num, FontStyle.Bold);
            }
            while (graphics.MeasureString(text, font).Width > rectangle.Width);
            StringFormat format = new StringFormat();
            format.Alignment = (StringAlignment)1;
            format.LineAlignment = (StringAlignment)1;
            GraphicsPath path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rectangle, format);
            const float num2 = 4f;
            PointF[] tfArray = new[] {
                                         new PointF((random.Next(rectangle.Width)) / num2,(random.Next(rectangle.Height)) / num2),
                                         new PointF(rectangle.Width - ((random.Next(rectangle.Width)) / num2),(random.Next(rectangle.Height) / num2)),
                                         new PointF((random.Next(rectangle.Width)) / num2,rectangle.Height - ((random.Next(rectangle.Height)) / num2)),
                                         new PointF(rectangle.Width - ((random.Next(rectangle.Width)) / num2),rectangle.Height - ((random.Next(rectangle.Height)) / num2)) 
                                     };
            Matrix matrix = new Matrix();
            matrix.Translate(0f, 0f);
            path.Warp(tfArray, rectangle, matrix, 0, 0f);
            brush = new HatchBrush((HatchStyle)0x23, Color.LightGray, Color.DarkGray);
            graphics.FillPath(brush, path);
            int num3 = Math.Max(rectangle.Width, rectangle.Height);
            for (int i = 0; i < (((rectangle.Width * rectangle.Height)) / 30f); i++)
            {
                int num5 = random.Next(rectangle.Width);
                int num6 = random.Next(rectangle.Height);
                int num7 = random.Next(num3 / 50);
                int num8 = random.Next(num3 / 50);
                graphics.FillEllipse(brush, num5, num6, num7, num8);
            }
            font.Dispose();
            brush.Dispose();
            graphics.Dispose();
            Image = bitmap;
        }

        private void SetDimensions(int p_width, int p_height)
        {
            if (p_width <= 0)
            {
                throw new ArgumentOutOfRangeException("p_width", p_width, "Argument out of range, must be greater than zero.");
            }
            if (p_height <= 0)
            {
                throw new ArgumentOutOfRangeException("p_height", p_height, "Argument out of range, must be greater than zero.");
            }
            width = p_width;
            height = p_height;
        }

        private void SetFamilyName(string p_familyName)
        {
            try
            {
                Font font = new Font(p_familyName, 12f);
                familyName = p_familyName;
                font.Dispose();
            }
            catch (Exception)
            {
                familyName = FontFamily.GenericSerif.Name;
            }
        }

        //public int Height
        //{
        //    get
        //    {
        //        return height;
        //    }
        //}

        public Bitmap Image { get; private set; }

        //public string Text
        //{
        //    get
        //    {
        //        return text;
        //    }
        //}

        //public int Width
        //{
        //    get
        //    {
        //        return width;
        //    }
        //}
    }
}