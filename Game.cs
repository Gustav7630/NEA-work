using Chess_Adative_AI.Chess_Pieces;
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

        // in game.cs [design] change the paint method to make it repaint as required
        private Tile[,] tiles = new Tile[8,8];
        private int x_pos;
        private int y_pos;
        public Game()
        {
            InitializeComponent();
            for(int i=0;i<8;i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    tiles[i,j] = new Tile(j,i,false,false);
                }
            }
        }


        private void draw_board(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color black = Color.FromArgb(255, 0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255, 255);
            Color green = Color.FromArgb(255, 0, 255, 0);
            Color red = Color.FromArgb(255,255,0,0);
            SolidBrush blackBrush = new SolidBrush(black);
            SolidBrush whiteBrush = new SolidBrush(white);
            SolidBrush greenBrush= new SolidBrush(green);
            SolidBrush redBrush= new SolidBrush(red);
            SolidBrush blueBrush = new SolidBrush(Color.AliceBlue);
            int side_len = 40;
            int adjustment = 0;
            Rectangle tile = new Rectangle(20, 20, 20, 20);
            Rectangle circle = new Rectangle(20,20,20,20);
            // e.Graphics.DrawRectangle(blackPen, square);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tile = new Rectangle(side_len * (j + 1), (side_len * (i + 1)), side_len, side_len);

                    if ((j + adjustment) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(whiteBrush, tile);
                    }

                    else
                    {
                        e.Graphics.FillRectangle(blackBrush, tile);
                    }

                    if (tiles[i, j].Moveable)
                    {
                        circle = new Rectangle(10+(side_len * (j + 1)),10+( (side_len * (i + 1))), side_len/2, side_len/2);
                        e.Graphics.FillEllipse(greenBrush, circle);
                    }
                    if (tiles[i, j].Occupied)
                    {
                        circle = new Rectangle(10 + (side_len * (j + 1)), 10 + ((side_len * (i + 1))), side_len / 2, side_len / 2);
                        e.Graphics.FillEllipse(redBrush, circle);
                    }



                }
                adjustment++;
                adjustment = adjustment % 2;
            }

        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X-40;
            int y = e.Y-40;
            if(x >= 0 && x <= 320 && y>= 0 && y <= 320) //sidelength, checks wether on board
            {
                tiles[(y / 40), (x / 40)].move();
               // MessageBox.Show($"Repainting, {x / 40} {y / 40}, mouse position {e.X} {e.Y}");
                Invalidate();
                x_pos = x / 40;
                y_pos = y / 40;
            }
            
            
            
        }


        private void move_piece(Piece piece)
        {
            foreach (var move in piece.possible_moves())
            {
                tiles[move[0], move[1]].move(); 
            }
        }

        private void board_paint(object sender, PaintEventArgs e)
        {
            
        }

        private void test_knight_Click(object sender, EventArgs e)
        {
            Knight knight = new Knight(7,7,false);
            tiles[7,7].occupy();
            move_piece(knight);
            MessageBox.Show("Knight moves");
            Invalidate();
        }

        private void Bishop_test_Click(object sender, EventArgs e)
        {
            Bishop bishop = new Bishop(y_pos, x_pos,false);
            tiles[y_pos,x_pos].occupy();
            move_piece(bishop);
            MessageBox.Show("Bishop moves");
            Invalidate();
        }

        private void Rook_test_Click(object sender, EventArgs e)
        {
            Rook rook = new Rook(y_pos, x_pos, false);
            tiles[y_pos, x_pos].occupy();
            move_piece(rook);
            MessageBox.Show("Rook moves");
            Invalidate();
        }

        private void Queen_test_button_Click(object sender, EventArgs e)
        {
            Queen queen = new Queen(y_pos, x_pos, false);
            tiles[y_pos, x_pos].occupy();
            move_piece(queen);
            MessageBox.Show("Queen moves");
            Invalidate();
        }

        private void pawntestButton_Click(object sender, EventArgs e)
        {
            Pawn pawn = new Pawn(y_pos, x_pos, true);
            tiles[y_pos, x_pos].occupy();
            move_piece(pawn);
            MessageBox.Show("pawn moves");

            foreach (int[] move in pawn.possible_moves())
            {
                MessageBox.Show($" X : {move[0]} Y: {move[1]}");
            }

            pawn.displace();
            Invalidate();
        }

        private void Kingtestbutton_Click(object sender, EventArgs e)
        {
            King king = new King(y_pos,x_pos,false);
            tiles[y_pos, x_pos].occupy();
            move_piece(king);
            MessageBox.Show("king moves");
            Invalidate();
        }
    }
}



