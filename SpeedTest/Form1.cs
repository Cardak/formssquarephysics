using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Click(object sender,EventArgs e)
        {
            
        }

        private void populategameobjects()
        {
            GameObjects temp = new GameObjects();
            temp.xspeed = 0;
            temp.yspeed = 0;
            temp.height = 80;
            temp.width = 40;
            temp.x = 50;
            temp.y = 40;
            temp.show = true;
            temp.mass = 1;
            gameobjects.Add(temp);
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            populategameobjects();
        }

        private void clearDrawIt()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle screen = new System.Drawing.Rectangle(
               0, 0, this.Size.Width, this.Size.Height);
            graphics.FillRectangle(System.Drawing.Brushes.Magenta, screen);
            //graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);

        }

        private void DrawIt(Rectangle rectangle)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle( x, y, 150, 150);
            System.Drawing.Rectangle screen = new System.Drawing.Rectangle(
               0, 0, this.Size.Width, this.Size.Height);
            graphics.FillRectangle(System.Drawing.Brushes.Magenta, screen);
            graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GravityApplication();
            MovementApplication();
            DrawGameObjects();
            
        }

        private bool onground;

        private void MovementApplication()
        {
            for(int i = 0;i < gameobjects.Count;i++)
            {
                int newx = gameobjects[i].x + gameobjects[i].xspeed;
                int newy = gameobjects[i].y + gameobjects[i].yspeed;
                if(newx < this.Size.Width - gameobjects[i].width && newx > 0)
                {
                    gameobjects[i].x = newx;
                }
                if (newy < this.Size.Height - gameobjects[i].height && newy > 0)
                {
                    gameobjects[i].y = newy;
                    onground = true;
                }
                else
                {
                    onground = false;
                }

            }
        }

        private void GravityApplication()
        {
            gameobjects[0].yspeed += gameobjects[0].mass;
        }

        private void DrawGameObjects()
        {
            clearDrawIt();
            for (int i = 0; i < gameobjects.Count; i++)
            {
                if (gameobjects[i].show)
                {
                    DrawIt(gameobjects[i].sprite());
                }
            }
        }
        List<GameObjects> gameobjects = new List<GameObjects>();

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char input = ((KeyPressEventArgs)e).KeyChar;
            switch (input)
            {
                case 'w':
                    if (onground)
                    {
                        gameobjects[0].yspeed = -10;
                    }
                    break;
                case 'a':
                    gameobjects[0].xspeed -= 1;
                    break;
                case 's':
                    gameobjects[0].yspeed = 10;
                    break;
                case 'd':
                    gameobjects[0].xspeed += 1;
                    break;
                default:
                    gameobjects[0].xspeed = 0;
                    break;

            }
        }
    }



    class GameObjects
    {
        public int height;
        public int width;
        public int x;
        public int y;
        public int xspeed;
        public int yspeed;
        public Rectangle sprite(){
            Rectangle temp = new Rectangle(new Point(x, y), new Size(width, height));
            return temp;

        }
        public bool show;
        public int mass;

    }
}
