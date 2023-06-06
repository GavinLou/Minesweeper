using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 踩地雷.Properties
{
     class 格子 : PictureBox
    {
        public 格子(int x ,int y ,int z)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point( x , y );
            this.Size = new Size( 32 , 32 );
            this.Padding = new Padding(352, 0, 64*z, 0);
            this.Image = Properties.Resources.tiles;
            this.SizeMode = PictureBoxSizeMode.CenterImage;
        }
    }
}
