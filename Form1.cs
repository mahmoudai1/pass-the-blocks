using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{

    public partial class Form1 : Form
    {
        Bitmap off;

        class Hero
        {
            public int X;
            public int Y;
            public int W;
            public int H;
        }

        class Shape
        {
            public int X;
            public int Y;
            public int W;
            public int H;
        }

        class Obstacle
        {
            public int X;
            public int Y;
            public int W;
            public int H;
        }

        Timer t = new Timer();
        int ctTimer = 0;
        bool isLose = false;
        int ctZoomIn = 0, ctZoomOut = 0;
        bool iDrag = false;
        int prevX, prevY;
        int vSpeed = 30;
        int vObstacles = 8;

        List<Hero> L1 = new List<Hero>();
        List<Shape> L2 = new List<Shape>();
        List<Obstacle> L3 = new List<Obstacle>();
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyUp += Form1_KeyUp;
            this.Text = " Assignment 11B";
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Start();
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void T_Tick(object sender, EventArgs e)
        {

            if (ctTimer % 9 == 0)
            {
                vObstacles += 2;
                if (vObstacles >= 65)
                {
                    vObstacles = 65;
                }
            }
            if(vObstacles < 8)
            {
                vObstacles = 8;
            }

            if (vObstacles >= 65)
            {
                if (ctTimer % 5 == 0)
                {
                    label5.Text = "";
                }
                else
                {
                    label5.Text = "Squares Speed = " + vObstacles;
                }
            }
            else
            {
                label5.Text = "Squares Speed = " + vObstacles;
            }

            if (!isLose)
            {
                MoveObstacles();
            }
            ctTimer++;
            DrawDouble(this.CreateGraphics());
        }

        void MoveObstacles()
        {
            int f = 1;

            for (int i = 0; i < L3.Count; i++)
            {
                for (int k = 0; k < vObstacles; k++)
                {
                    if (L3[i].X <= L2[2].X + L2[2].W && L3[i].Y + L3[i].H < L2[9].Y)
                    {
                        if (L3[i].Y + L3[i].H + 1 >= L1[0].Y && L3[i].Y + L3[i].H <= L1[0].Y + L1[0].H && L1[0].X + L1[0].W > L3[i].X && L1[0].X < L3[i].X + L3[i].W)
                        {
                            isLose = true;
                            f = 0;
                            MessageBox.Show("You Lost!, the game has ended.");
                            break;
                        }
                        L3[i].Y++;
                    }
                }

                if (f == 0)
                {
                    break;
                }

                for (int k = 0; k < vObstacles; k++)
                {
                    if (L3[i].X + L3[i].W < L2[10].X && L3[i].Y + L3[i].H >= L2[9].Y)
                    {
                        if (L3[i].X + L3[i].W + 1 >= L1[0].X && L3[i].X + L3[i].W <= L1[0].X + L1[0].W && L1[0].Y + L1[0].H > L3[i].Y && L1[0].Y < L3[i].Y + L3[i].H)
                        {
                            isLose = true;
                            f = 0;
                            MessageBox.Show("You Lost!, the game has ended.");
                            break;
                        }
                        L3[i].X++;
                    }
                }

                if (f == 0)
                {
                    break;
                }

                for (int k = 0; k < vObstacles; k++)
                {
                    if (L3[i].X + L3[i].W >= L2[10].X && L3[i].Y > L2[3].Y)
                    {
                        if (L3[i].Y - 1 <= L1[0].Y + L1[0].H && L3[i].Y >= L1[0].Y && L1[0].X + L1[0].W > L3[i].X && L1[0].X < L3[i].X + L3[i].W)
                        {
                            isLose = true;
                            f = 0;
                            MessageBox.Show("You Lost!, the game has ended.");
                            break;
                        }
                        L3[i].Y--;
                    }
                }

                if (f == 0)
                {
                    break;
                }

                for (int k = 0; k < vObstacles; k++)
                {
                    if (L3[i].X > L2[2].X && L3[i].Y <= L2[3].Y)
                    {
                        if (L3[i].X - 1 <= L1[0].X + L1[0].W && L3[i].X >= L1[0].X && L1[0].Y + L1[0].H > L3[i].Y && L1[0].Y < L3[i].Y + L3[i].H)
                        {
                            isLose = true;
                            f = 0;
                            MessageBox.Show("You Lost!, the game has ended.");
                            break;
                        }
                        L3[i].X--;
                    }
                }

                if (f == 0)
                {
                    break;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isLose)
            {
                MessageBox.Show("You Lost!, the game has ended.");
            }
            else
            {

                if (e.KeyCode == Keys.Right)
                {
                    int f = 1;
                    for (int k = 0; k < vSpeed; k++)
                    {
                        for (int i = 0; i < L2.Count; i++)
                        {
                            if (L2[i].X >= L1[0].X && L2[i].X <= L1[0].X + L1[0].W &&
                               (L1[0].Y) < L2[i].Y + L2[i].H && (L1[0].Y + L1[0].H) > L2[i].Y)
                            {
                                f = 0;
                                break;
                            }
                        }

                        for (int i = 0; i < L3.Count; i++)
                        {
                            if (L3[i].X >= L1[0].X && L3[i].X <= L1[0].X + L1[0].W &&
                               (L1[0].Y) < L3[i].Y + L3[i].H && (L1[0].Y + L1[0].H) > L3[i].Y)
                            {
                                isLose = true;
                                break;
                            }
                        }

                        if (f == 1 && !isLose)
                        {
                            L1[0].X++;
                        }
                    }

                }

                else if (e.KeyCode == Keys.Left)
                {
                    int f = 1;
                    for (int k = 0; k < vSpeed; k++)
                    {
                        for (int i = 0; i < L2.Count; i++)
                        {
                            if (L2[i].X + L2[i].W >= L1[0].X && L2[i].X + L2[i].W <= L1[0].X + L1[0].W &&
                                (L1[0].Y) < L2[i].Y + L2[i].H && (L1[0].Y + L1[0].H) > L2[i].Y)
                            {
                                f = 0;
                                break;
                            }
                        }

                        for (int i = 0; i < L3.Count; i++)
                        {
                            if (L3[i].X + L3[i].W >= L1[0].X && L3[i].X + L3[i].W <= L1[0].X + L1[0].W &&
                                (L1[0].Y) < L3[i].Y + L3[i].H && (L1[0].Y + L1[0].H) > L3[i].Y)
                            {
                                isLose = true;
                                break;
                            }
                        }

                        if (f == 1 && !isLose)
                        {
                            L1[0].X--;
                        }
                    }


                }

                else if (e.KeyCode == Keys.Down)
                {
                    int f = 1;
                    for (int k = 0; k < vSpeed; k++)
                    {
                        for (int i = 0; i < L2.Count; i++)
                        {
                            if (L2[i].Y >= L1[0].Y && L2[i].Y <= L1[0].Y + L1[0].H &&
                               (L1[0].X) < L2[i].X + L2[i].W && (L1[0].X + L1[0].W) > L2[i].X)
                            {
                                f = 0;
                                break;
                            }
                        }

                        for (int i = 0; i < L3.Count; i++)
                        {
                            if (L3[i].Y >= L1[0].Y && L3[i].Y <= L1[0].Y + L1[0].H &&
                               (L1[0].X) < L3[i].X + L3[i].W && (L1[0].X + L1[0].W) > L3[i].X)
                            {
                                isLose = true;
                                break;
                            }
                        }

                        if (f == 1 && !isLose)
                        {
                            L1[0].Y++;
                        }
                    }


                }

                else if (e.KeyCode == Keys.Up)
                {
                    int f = 1;
                    for (int k = 0; k < vSpeed; k++)
                    {
                        for (int i = 0; i < L2.Count; i++)
                        {
                            if (L2[i].Y + L2[i].H >= L1[0].Y && L2[i].Y + L2[i].H <= L1[0].Y + L1[0].H &&
                                (L1[0].X) < L2[i].X + L2[i].W && (L1[0].X + L1[0].W) > L2[i].X)
                            {
                                f = 0;
                                break;
                            }
                        }

                        for (int i = 0; i < L3.Count; i++)
                        {
                            if (L3[i].Y + L3[i].H >= L1[0].Y && L3[i].Y + L3[i].H <= L1[0].Y + L1[0].H &&
                                (L1[0].X) < L3[i].X + L3[i].W && (L1[0].X + L1[0].W) > L3[i].X)
                            {
                                isLose = true;
                                break;
                            }
                        }

                        if (f == 1 && !isLose)
                        {
                            L1[0].Y--;
                        }
                    }


                }

                if (e.KeyCode == Keys.Space)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (i % 2 == 0)
                        {
                            L2[i].H += 40;

                        }
                        else
                        {
                            L2[i].W += 80;
                        }
                    }

                    L1[0].W += 10;
                    L1[0].H += 10;

                    if (L1[0].X < L2[1].X + (L2[1].W / 2))
                    {
                        L1[0].X -= 80;
                    }
                    else if (L1[0].X >= L2[1].X + (L2[1].W / 2) && L1[0].X < L2[3].X)
                    {
                        L1[0].X -= 20;
                    }
                    else if (L1[0].X < L2[3].X + (L2[3].W / 2) && L1[0].X > L2[3].X)
                    {
                        L1[0].X += 0;
                    }
                    else if (L1[0].X >= L2[3].X + (L2[3].W / 2) && L1[0].X + L1[0].W < L2[3].X + L2[3].W - L2[4].W)
                    {
                        L1[0].X += 70;
                    }
                    else if (L1[0].X < L2[5].X + (L2[5].W / 2) && L1[0].X + L1[0].W >= L2[5].X)
                    {
                        L1[0].X += 100; // not sure
                    }
                    else if (L1[0].X >= L2[5].X + (L2[5].W / 2) && L1[0].X <= L2[5].X + L2[5].W)
                    {
                        L1[0].X += 150;
                    }


                    if (L1[0].Y > L2[0].Y && L1[0].Y + L1[0].H < L2[0].Y + (L2[0].H / 2))
                    {
                        L1[0].Y -= 40;
                    }
                    else if (L1[0].Y + L1[0].H < L2[0].Y + L2[0].H && L1[0].Y + L1[0].H >= L2[0].Y + (L2[0].H / 2))
                    {
                        L1[0].Y -= 10; //-20
                    }
                    else if (L1[0].Y > L2[2].Y && L1[0].Y + L1[0].H < L2[2].Y + (L2[2].H / 2))
                    {
                        L1[0].Y -= 80;
                    }
                    else if (L1[0].Y < L2[2].Y + L2[2].H && L1[0].Y + L1[0].H >= L2[2].Y + (L2[2].H / 2))
                    {
                        L1[0].Y -= 50;
                    }
                    else if (L1[0].Y + L1[0].H > L2[8].Y && L1[0].Y + L1[0].H < L2[8].Y + (L2[8].H / 2))
                    {
                        L1[0].Y += 0;
                    }
                    else if (L1[0].Y < L2[8].Y + L2[8].H && L1[0].Y + L1[0].H >= L2[8].Y + (L2[8].H / 2))
                    {
                        L1[0].Y += 30;
                    }



                    L2[0].X -= 80;
                    L2[1].X -= 80;
                    L2[0].Y -= 40;
                    L2[1].Y -= 40;

                    if (L3[0].Y + (L3[0].H / 2) <= L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) < L2[3].X + (L2[3].W / 2))
                    {
                        L3[0].Y -= 80;
                        L3[0].X += 0;
                        L3[1].Y += 0;
                        L3[1].X += 40;
                    }
                    else if (L3[0].Y + (L3[0].H / 2) > L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) < L2[3].X + (L2[3].W / 2))
                    {
                        L3[0].Y += 0;
                        L3[0].X += 0;
                        L3[1].Y -= 80;
                        L3[1].X += 40;
                    }

                    else if (L3[0].Y + (L3[0].H / 2) <= L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) >= L2[3].X + (L2[3].W / 2))
                    {

                        L3[0].Y -= 80;
                        L3[1].X += 0;
                        L3[1].Y += 0;
                        L3[0].X += 40;
                    }
                    else if (L3[0].Y + (L3[0].H / 2) > L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) >= L2[3].X + (L2[3].W / 2))
                    {

                        L3[0].Y += 0;
                        L3[1].X += 0;
                        L3[1].Y -= 80;
                        L3[0].X += 40;
                    }

                    vSpeed += 5;
                    ctZoomIn++;
                    ctZoomOut--;
                    vObstacles += 3;
                    ReShaping();
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (ctZoomOut < 2)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            if (i % 2 == 0)
                            {
                                L2[i].H -= 40;

                            }
                            else
                            {
                                L2[i].W -= 80;
                            }
                        }

                        L1[0].W -= 10;
                        L1[0].H -= 10;

                        if (L1[0].X < L2[1].X + (L2[1].W / 2))
                        {
                            L1[0].X += 80;
                        }
                        else if (L1[0].X >= L2[1].X + (L2[1].W / 2) && L1[0].X < L2[3].X)
                        {
                            L1[0].X += 20;
                        }
                        else if (L1[0].X < L2[3].X + (L2[3].W / 2) && L1[0].X > L2[3].X)
                        {
                            L1[0].X -= 0;
                        }
                        else if (L1[0].X >= L2[3].X + (L2[3].W / 2) && L1[0].X + L1[0].W < L2[3].X + L2[3].W - L2[4].W)
                        {

                            L1[0].X -= 70;
                        }
                        else if (L1[0].X < L2[5].X + (L2[5].W / 2) && L1[0].X + L1[0].W >= L2[5].X)
                        {
                            L1[0].X -= 100; // not sure
                        }
                        else if (L1[0].X >= L2[5].X + (L2[5].W / 2) && L1[0].X <= L2[5].X + L2[5].W)
                        {
                            L1[0].X -= 150;
                        }
                        else
                        {
                            if (L1[0].X < L2[5].X)
                            {
                                L1[0].X -= 70;
                            }
                            else if (L1[0].X > L2[5].X + (L2[5].W / 2))
                            {
                                L1[0].X -= 150;
                            }
                        }


                        if (L1[0].Y > L2[0].Y && L1[0].Y + L1[0].H < L2[0].Y + (L2[0].H / 2))
                        {

                            L1[0].Y += 40;
                        }
                        else if (L1[0].Y + L1[0].H <= L2[0].Y + L2[0].H && L1[0].Y + L1[0].H >= L2[0].Y + (L2[0].H / 2))
                        {

                            L1[0].Y += 10; //-20
                        }
                        else if (L1[0].Y > L2[2].Y && L1[0].Y + L1[0].H < L2[2].Y + (L2[2].H / 2))
                        {
                            L1[0].Y += 80;
                        }
                        else if (L1[0].Y < L2[2].Y + L2[2].H && L1[0].Y + L1[0].H >= L2[2].Y + (L2[2].H / 2))
                        {
                            L1[0].Y += 50;
                        }
                        else if (L1[0].Y + L1[0].H > L2[8].Y && L1[0].Y + L1[0].H < L2[8].Y + (L2[8].H / 2))
                        {
                            L1[0].Y -= 0;
                        }
                        else if (L1[0].Y < L2[8].Y + L2[8].H && L1[0].Y + L1[0].H >= L2[8].Y + (L2[8].H / 2))
                        {
                            L1[0].Y -= 30;
                        }
                        else
                        {
                            L1[0].Y += 10;
                        }

                        L2[0].X += 80;
                        L2[1].X += 80;
                        L2[0].Y += 40;
                        L2[1].Y += 40;

                        if (L3[0].Y + (L3[0].H / 2) <= L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) < L2[3].X + (L2[3].W / 2))
                        {
                            L3[0].Y += 80;
                            L3[0].X -= 0;
                            L3[1].Y -= 0;
                            L3[1].X -= 40;
                        }
                        else if (L3[0].Y + (L3[0].H / 2) > L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) < L2[3].X + (L2[3].W / 2))
                        {
                            L3[0].Y -= 0;
                            L3[0].X -= 0;
                            L3[1].Y += 80;
                            L3[1].X -= 40;
                        }

                        else if (L3[0].Y + (L3[0].H / 2) <= L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) >= L2[3].X + (L2[3].W / 2))
                        {

                            L3[0].Y += 80;
                            L3[1].X -= 0;
                            L3[1].Y -= 0;
                            L3[0].X -= 40;
                        }
                        else if (L3[0].Y + (L3[0].H / 2) > L2[0].Y + (L2[0].H / 2) && L3[0].X + (L3[0].W / 2) >= L2[3].X + (L2[3].W / 2))
                        {

                            L3[0].Y -= 0;
                            L3[1].X -= 0;
                            L3[1].Y += 80;
                            L3[0].X -= 40;
                        }



                        vSpeed -= 5;
                        ctZoomOut++;
                        ctZoomIn--;
                        vObstacles -= 3;
                        ReShaping();
                    }
                }
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDouble(e.Graphics);
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            iDrag = false;
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            iDrag = true;
            prevX = e.X;
            prevY = e.Y;
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Text = "X = " + e.X + "  Y = " + e.Y;

            if (iDrag)
            {
                int dx = e.X - prevX;
                int dy = e.Y - prevY;
                if (dx < 0)
                {
                    dx *= -1;
                }
                if (dy < 0)
                {
                    dy *= -1;
                }

                if (e.X > prevX)
                {
                    L2[0].X += dx;
                    L2[1].X += dx;
                    L1[0].X += dx;
                    L3[0].X += dx;
                    L3[1].X += dx;
                    prevX = e.X;
                }
                if (e.X < prevX)
                {
                    L2[0].X -= dx;
                    L2[1].X -= dx;
                    L1[0].X -= dx;
                    L3[0].X -= dx;
                    L3[1].X -= dx;
                    prevX = e.X;
                }
                if (e.Y > prevY)
                {
                    L2[0].Y += dy;
                    L2[1].Y += dy;
                    L1[0].Y += dy;
                    L3[0].Y += dy;
                    L3[1].Y += dy;
                    prevY = e.Y;
                }
                if (e.Y < prevY)
                {
                    L2[0].Y -= dy;
                    L2[1].Y -= dy;
                    L1[0].Y -= dy;
                    L3[0].Y -= dy;
                    L3[1].Y -= dy;
                    prevY = e.Y;
                }

                ReShaping();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CreateActors();
        }

        void ReShaping()
        {

            for (int i = 0; i < L2.Count; i++)
            {
                switch (i)
                {
                    case 1:
                        L2[2].X = L2[1].X + L2[1].W;
                        L2[2].Y = L2[1].Y + L2[1].H - (L2[0].H);
                        L2[2].W = L2[0].W;
                        L2[2].H = L2[0].H;
                        break;
                    case 2:
                        L2[3].X = L2[2].X;
                        L2[3].Y = L2[2].Y;
                        L2[3].W = L2[1].W;
                        L2[3].H = L2[1].H;
                        break;
                    case 3:
                        L2[4].X = L2[3].X + L2[3].W;
                        L2[4].Y = L2[3].Y;
                        L2[4].W = L2[0].W;
                        L2[4].H = L2[0].H;
                        break;
                    case 4:
                        L2[5].X = L2[4].X;
                        L2[5].Y = L2[4].Y + L2[4].H - L2[3].H;
                        L2[5].W = L2[1].W;
                        L2[5].H = L2[1].H;
                        break;
                    case 5:
                        L2[6].X = L2[5].X + L2[5].W;
                        L2[6].Y = L2[5].Y;
                        L2[6].W = L2[0].W;
                        L2[6].H = L2[0].H;
                        break;
                    case 6:
                        L2[7].X = L2[0].X;
                        L2[7].Y = L2[0].Y + L2[0].H - L2[1].H;
                        L2[7].W = L2[1].W;
                        L2[7].H = L2[1].H;
                        break;
                    case 7:
                        L2[8].X = L2[7].X + L2[7].W;
                        L2[8].Y = L2[7].Y;
                        L2[8].W = L2[0].W;
                        L2[8].H = L2[0].H;
                        break;
                    case 8:
                        L2[9].X = L2[8].X;
                        L2[9].Y = L2[8].Y + L2[8].H - L2[7].H;
                        L2[9].W = L2[1].W;
                        L2[9].H = L2[1].H;
                        break;
                    case 9:
                        L2[10].X = L2[9].X + L2[9].W;
                        L2[10].Y = L2[8].Y;
                        L2[10].W = L2[0].W;
                        L2[10].H = L2[0].H;
                        break;
                    case 10:
                        L2[11].X = L2[10].X + L2[10].W;
                        L2[11].Y = L2[0].Y + L2[0].H - L2[1].H;
                        L2[11].W = L2[1].W;
                        L2[11].H = L2[1].H;
                        break;
                }

            }

            L3[0].W = L2[3].W / 2;
            L3[0].H = L2[2].H - L2[3].H;

            L3[1].W = L2[3].W / 2;
            L3[1].H = L2[2].H - L2[3].H;


        }

        void CreateActors()
        {

            Hero pnn = new Hero();
            pnn.X = 450;
            pnn.Y = this.ClientSize.Height / 2;
            pnn.W = 50;
            pnn.H = 50;
            L1.Add(pnn);

            int ax = 400;                    //First Controller
            int ay = L1[0].Y - 20;
            int aw = 8;
            int ah = 200;

            for (int i = 0; i < 12; i++)
            {
                Shape pnn2 = new Shape();
                pnn2.X = ax;
                pnn2.Y = ay;
                pnn2.W = aw;
                pnn2.H = ah;
                L2.Add(pnn2);
                switch (i)
                {
                    case 0:
                        ax = L2[0].X;               //Second Controller
                        ay = L2[0].Y;
                        aw = 400;
                        ah = 8;
                        break;
                    case 1:
                        ax = L2[1].X + L2[1].W;
                        ay = L2[1].Y + L2[1].H - (L2[0].H);
                        aw = L2[0].W;
                        ah = L2[0].H;
                        break;
                    case 2:
                        ax = L2[2].X;
                        ay = L2[2].Y;
                        aw = L2[1].W;
                        ah = L2[1].H;
                        break;
                    case 3:
                        ax = L2[3].X + L2[3].W;
                        ay = L2[3].Y;
                        aw = L2[0].W;
                        ah = L2[0].H;
                        break;
                    case 4:
                        ax = L2[4].X;
                        ay = L2[4].Y + L2[4].H - L2[3].H;
                        aw = L2[1].W;
                        ah = L2[1].H;
                        break;
                    case 5:
                        ax = L2[5].X + L2[5].W;
                        ay = L2[5].Y;
                        aw = L2[0].W;
                        ah = L2[0].H;
                        break;
                    case 6:
                        ax = L2[0].X;
                        ay = L2[0].Y + L2[0].H - L2[1].H;
                        aw = L2[1].W;
                        ah = L2[1].H;
                        break;
                    case 7:
                        ax = L2[7].X + L2[7].W;
                        ay = L2[7].Y;
                        aw = L2[0].W;
                        ah = L2[0].H;
                        break;
                    case 8:
                        ax = L2[8].X;
                        ay = L2[8].Y + L2[8].H - L2[7].H;
                        aw = L2[1].W;
                        ah = L2[1].H;
                        break;
                    case 9:
                        ax = L2[9].X + L2[9].W;
                        ay = L2[8].Y;
                        aw = L2[0].W;
                        ah = L2[0].H;
                        break;
                    case 10:
                        ax = L2[10].X + L2[10].W;
                        ay = L2[0].Y + L2[0].H - L2[1].H;
                        aw = L2[1].W;
                        ah = L2[1].H;
                        break;
                }

            }


            int bx = L2[3].X;
            int by = L2[3].Y + L2[3].H;

            for (int i = 0; i < 2; i++)
            {
                Obstacle pnn3 = new Obstacle();
                pnn3.X = bx;
                pnn3.Y = by;
                pnn3.W = L2[3].W / 2;
                pnn3.H = L2[2].H - L2[3].H;
                L3.Add(pnn3);
                bx = L2[9].X + (L2[9].W / 2);
                by = L2[10].Y;
            }
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);

            g.FillEllipse(Brushes.Yellow, L1[0].X, L1[0].Y, L1[0].W, L1[0].H);
            for (int i = 0; i < L2.Count; i++)
            {
                g.FillRectangle(Brushes.White, L2[i].X, L2[i].Y, L2[i].W, L2[i].H);
            }

            for (int i = 0; i < L3.Count; i++)
            {
                g.FillRectangle(Brushes.White, L3[i].X, L3[i].Y, L3[i].W, L3[i].H);
            }
        }

        void DrawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }



    }
}


