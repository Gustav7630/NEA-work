using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_Adative_AI
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }


        private void draw_board(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color black = Color.FromArgb(255, 0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255, 255);
            SolidBrush blackBrush = new SolidBrush(black);
            SolidBrush whiteBrush = new SolidBrush(white);

            int side_len = 40;
            int adjustment = 0;
            Rectangle tile = new Rectangle(20, 20, 20, 20);

            // e.Graphics.DrawRectangle(blackPen, square);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tile = new Rectangle(side_len * (j + 1), (side_len * i + 1), side_len, side_len);

                    if ((j + adjustment) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(whiteBrush, tile);
                    }

                    else
                    {
                        e.Graphics.FillRectangle(blackBrush, tile);
                    }
                }
                adjustment++;
                adjustment = adjustment % 2;
            }

        }
    }
}



