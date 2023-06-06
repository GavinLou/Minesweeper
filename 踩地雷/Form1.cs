using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using 踩地雷.Properties;
using System.Collections.Generic;

namespace 踩地雷
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        int[,] grid = new int[30, 15];
        int[,] sgrid = new int[30, 15];
        List<List<格子>> cell = new List<List<格子>>();
        public static Form1 f;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            first = false;
            f = this;
            this.Size = new Size(975, 518);
            for (int i = 0; i < 30; i++)
            {
                List<格子> temp = new List<格子>();
                for (int j = 0; j < 15; j++)
                {
                    格子 back = new 格子(i * 32, j * 32, 10);
                    back.MouseDown += Back_MouseDown;
                    temp.Add(back);
                    this.Controls.Add(back);
                }
                cell.Add(temp);
            }
            int sit, x, y;
            int[] counter = new int[450];
            for (int i = 0; i < 50; i++)
            {
                do
                {
                    sit = random.Next() % 450;
                } while (counter[sit] != 0);
                counter[sit]++;
                y = sit / 30;
                x = sit % 30;
                grid[x, y] = 9;
            }
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int n = 0;
                    if (grid[i, j] == 9) continue;
                    if (i < 29 && grid[i + 1, j] == 9) n++;
                    if (j < 14 && grid[i, j + 1] == 9) n++;
                    if (i > 0 && grid[i - 1, j] == 9) n++;
                    if (j > 0 && grid[i, j - 1] == 9) n++;
                    if (i < 29 && j < 14 && grid[i + 1, j + 1] == 9) n++;
                    if (i > 0 && j > 0 && grid[i - 1, j - 1] == 9) n++;
                    if (i > 0 && j < 14 && grid[i - 1, j + 1] == 9) n++;
                    if (i < 29 && j > 0 && grid[i + 1, j - 1] == 9) n++;
                    grid[i, j] = n;
                }
            }
        }
        bool first = false;
        public void Open(int x, int y)
        {
            cell[x][y].Image = null;
            cell[x][y].Tag = "lock";
            cell[x][y].Padding = new Padding(352, 0, 64 * grid[x, y], 0);
            cell[x][y].Image = Properties.Resources.tiles;
        }
        public void Openblock(int x, int y)
        {
            if (cell[x][y].Tag == "lock")
                return;
            Open(x, y);
            if (grid[x, y] != 0)
                return;
            if (x < 29 && grid[x + 1, y] == 0)
            {
                Openblock(x + 1, y);
            }
            else if(x < 29)
            {
                Open(x + 1, y);
            }
            if (y < 14 && grid[x, y + 1] == 0)
            {
                Openblock(x, y + 1);
            }
            else if (y < 14)
            {
                Open(x, y + 1);
            }
            if (x > 0 && grid[x - 1, y] == 0)
            {
                Openblock(x - 1, y);
            }
            else if (x > 0)
            {
                Open(x - 1, y);
            }
            if (y > 0 && grid[x, y - 1] == 0)
            {
                Openblock(x, y - 1);
            }
            else if (y > 0)
            {
                Open(x, y - 1);
            }
            if (x < 29 && y < 14 && grid[x + 1, y + 1] == 0)
            {
                Openblock(x + 1, y + 1);
            }
            else if (x < 29 && y < 14)
            {
                Open(x + 1, y + 1);
            }
            if (x > 0 && y > 0 && grid[x - 1, y - 1] == 0)
            {
                Openblock(x - 1, y - 1);
            }
            else if (x > 0 && y > 0)
            {
                Open(x - 1, y - 1);
            }
            if (x > 0 && y < 14 && grid[x - 1, y + 1] == 0)
            {
                Openblock(x - 1, y + 1);
            }
            else if (x > 0 && y < 14)
            {
                Open(x - 1, y + 1);
            }
            if (x < 29 && y > 0 && grid[x + 1, y - 1] == 0)
            {
                Openblock(x + 1, y - 1);
            }
            else if (x < 29 && y > 0)
            {
                Open(x + 1, y - 1);
            }
        }
        private void Back_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                格子 tmp = sender as 格子;
                int x = tmp.Location.X / 32, y = tmp.Location.Y / 32;
                if (tmp.Tag == "lock" || tmp.Tag == "flag")
                {
                    return;
                }
                Openblock(x, y);
                int sum = 0;
                if(cell.Sum(z => z.Count(w=>w.Tag == "lock")) == 400)
                {
                    MessageBox.Show("Win");
                }
                if (grid[x, y] == 9)
                {
                    MessageBox.Show("Gameover");
                    Close();
                }
            }
            else
            {
    
                格子 tmp = sender as 格子;
                int x = tmp.Location.X / 32, y = tmp.Location.Y / 32;
                if (tmp.Tag == "lock")
                    return;
                if (tmp.Tag != "flag" )
                {
                    tmp.Image = null;
                    tmp.Tag = "flag";
                    tmp.Padding = new Padding(352, 0, 64 * 11, 0);
                    tmp.Image = Properties.Resources.tiles;
                }
                else
                {
                    tmp.Tag = "";
                    tmp.Image = null;
                    tmp.Padding = new Padding(352, 0, 64 * 10, 0);
                    tmp.Image = Properties.Resources.tiles;
                }
            }
        }
    }
}
