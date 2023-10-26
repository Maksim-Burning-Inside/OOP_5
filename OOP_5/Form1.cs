using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_5
{
    public partial class Form1 : Form
    {
        private Picture _picture;

        public Form1()
        {
            InitializeComponent();
        }

        class Picture
        {
            private readonly List<Shape> _shapes = new List<Shape>();
            private PictureBox pictureBox1;

            public static Random R = new Random();

            public Picture(PictureBox pictureBox1)
            {
                this.pictureBox1 = pictureBox1;
            }

            public void ResetShapes()
            {
                _shapes.Clear();
            }

            public void AddShape(Shape shape)
            {
                _shapes.Add(shape);
            }

            public void DrawPicture()
            {
                _shapes.Sort();
                Point point_min = new Point(0, 0);
                Point point_max = new Point(0, pictureBox1.Width);

                int move = 0;
                if (_shapes.Count > 0) move = pictureBox1.Height / _shapes.Count;
                if (move == 0) move++;

                foreach (Shape shape in _shapes)
                {
                    if (shape is Polygon polygon)
                    {
                        DrawPolygon(polygon, point_min, point_max);
                    }
                    else if (shape is Triangle triangle)
                    {
                        DrawTriangle(triangle, point_min, point_max);
                    }
                    else if (shape is Ellipse ellipse)
                    {
                        DrawEllipse(ellipse, point_min, point_max);
                    }
                    else
                    {
                        throw new Exception("Неизвестная фигура");
                    }
                    point_min.X += move;
                    point_max.X += move;
                }
            }

            private void DrawTriangle(Triangle triangle, Point point_min, Point point_max)
            {
                PointF start_point = new PointF(point_max.X, R.Next(point_min.Y, point_max.Y + 1));
                PointF second_point = new PointF(start_point.X, (float)(start_point.Y + triangle.P1));
                PointF end_point = new PointF((float)(start_point.X + triangle.P2), second_point.Y);

                Graphics G = pictureBox1.CreateGraphics();
                G.DrawPolygon(new Pen(Color.FromArgb(R.Next(0, 250), R.Next(0, 250), R.Next(0, 250)), R.Next(1, 11)), new PointF[3] { start_point, second_point, end_point });
            }

            private void DrawPolygon(Polygon polygon, Point point_min, Point point_max)
            {
                PointF[] points = new PointF[(int)polygon.P2];
                points[0] = new PointF(point_max.X, (float)R.Next(point_min.Y, point_max.Y + 1));

                for (int i = 1; i < polygon.P2; i++)
                {
                    points[i] = new PointF((float)(points[i - 1].X * Math.Cos(Polygon.angle) - points[i - 1].Y * Math.Sin(Polygon.angle)), (float)(points[i - 1].X * Math.Sin(Polygon.angle) - points[i - 1].Y * Math.Cos(Polygon.angle)));
                }

                Graphics G = pictureBox1.CreateGraphics();
                G.DrawPolygon(new Pen(Color.FromArgb(R.Next(0, 250), R.Next(0, 250), R.Next(0, 250)), R.Next(1, 11)), points);
            }

            private void DrawEllipse(Ellipse ellipse, Point point_min, Point point_max)
            {
                PointF start_point = new PointF(R.Next(point_min.Y, point_max.Y + 1), point_max.X);
                Graphics G = pictureBox1.CreateGraphics();
                G.DrawEllipse(new Pen(Color.FromArgb(R.Next(0, 250), R.Next(0, 250), R.Next(0, 250)), R.Next(1, 11)), new RectangleF(start_point, new SizeF((float)ellipse.P1, (float)ellipse.P2)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _picture = new Picture(pictureBox1);
        }

        private void T_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < int.Parse(textBox4.Text); i++)
                {
                    _picture.AddShape(new Triangle(Picture.R.Next(1, 50), Picture.R.Next(1, 50)));
                    label6.Text = (int.Parse(label6.Text) + 1).ToString();
                }
            }
            else
            {
                for (int i = 0; i < int.Parse(textBox4.Text); i++)
                {
                    _picture.AddShape(new Triangle(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
                    label6.Text = (int.Parse(label6.Text) + 1).ToString();
                }
            }
        }

        private void E_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < int.Parse(textBox5.Text); i++)
                {
                    _picture.AddShape(new Ellipse(Picture.R.Next(1, 50), Picture.R.Next(1, 50)));
                    label5.Text = (int.Parse(label5.Text) + 1).ToString();
                }
            }
            else
            {
                for (int i = 0; i < int.Parse(textBox5.Text); i++)
                {
                    _picture.AddShape(new Ellipse(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
                    label5.Text = (int.Parse(label5.Text) + 1).ToString();
                }
            }
        }

        private void P_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < int.Parse(textBox6.Text); i++)
                {
                    _picture.AddShape(new Polygon(Picture.R.Next(3, 50), Picture.R.Next(3, 1000)));
                    label7.Text = (int.Parse(label7.Text) + 1).ToString();
                }
            }
            else
            {
                for (int i = 0; i < int.Parse(textBox6.Text); i++)
                {
                    _picture.AddShape(new Polygon(double.Parse(textBox1.Text), double.Parse(textBox2.Text)));
                    label7.Text = (int.Parse(label7.Text) + 1).ToString();
                }
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            _picture.ResetShapes();
            pictureBox1.Image = null;
            label5.Text = "0";
            label6.Text = "0";
            label7.Text = "0";
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < int.Parse(textBox3.Text); i++)
            {
                _picture.DrawPicture();
            }
        }
    }
}
