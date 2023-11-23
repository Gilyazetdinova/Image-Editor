using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float brightness = (float)numericUpDown1.Value / 100;
            var img = AdjustImage(pictureBox1.Image, brightness - 1f, 1f, 1f, 1f, 1f, 1f);
            pictureBox1.Image = img;
        }

        Bitmap AdjustImage(Image image, float brightness, float gamma, float cRed, float cGreen, float cBlue, float cAlpha)
        {
            //brightness = Значение яркости указывается в виде дельты, то есть 0 - нет изменений, 0.5 - увеличить на 50%, -0.5 - уменьшить на 50% и т.д.
            //cRed,cGreen,cBlue = значение от -1 до 1. Наверное вернее было бы от 0 до 1, но мне было лень разбираться
            float[][] matrix =
            {
                new float[] { cRed, 0, 0, 0, 0 },   // Контраст красного
                new float[] { 0, cGreen, 0, 0, 0 }, // Контраст зеленого
                new float[] { 0, 0, cBlue, 0, 0 },  // Контраст синего
                new float[] { 0, 0, 0, cAlpha, 0 }, // Контраст альфы
                new float[] { brightness, brightness, brightness, 0, 1 } // Яркость
            };

            var atts = new ImageAttributes();
            atts.SetColorMatrix(new ColorMatrix(matrix), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            atts.SetGamma(gamma, ColorAdjustType.Bitmap);

            Bitmap newImage = new Bitmap(image.Width, image.Height);
            using (var gfx = Graphics.FromImage(newImage))
                gfx.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height), 0, 0, newImage.Width, newImage.Height, GraphicsUnit.Pixel, atts);
            return newImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float red = (float)numericUpDown2.Value;
            float green = (float)numericUpDown3.Value;
            float blue = (float)numericUpDown4.Value;
            var img = AdjustImage(pictureBox1.Image, 0, 1f, red / 100, green / 100, blue / 100, 1f);
            pictureBox1.Image = img;
        }
    }
}
