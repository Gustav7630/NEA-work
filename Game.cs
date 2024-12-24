using Chess_Adative_AI.Chess_Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_Adative_AI
{
    public partial class Game : Form
    {

        // in game.cs [design] change the paint method to make it repaint as required
        private Tile[,] tiles = new Tile[8,8];
        private Piece[,] pieces= new Piece[8,8];

        private Piece[] black_pieces = new Piece[16];
        private Piece[] white_pieces = new Piece[16];
        private Piece selected_piece;

        private int x_pos_global;
        private int y_pos_global;
        private bool first_time = true;
        bool piece_selected = false;
        bool move_select = false;
        private int selected_x=-1;
        private int selected_y=-1;

        public Game()
        {
            InitializeComponent();
            
        }


        private void draw_board(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color black = Color.FromArgb(255, 0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255, 255);
            Color green = Color.FromArgb(255, 0, 255, 0);
            Color red = Color.FromArgb(255, 255, 0, 0);
            SolidBrush blackBrush = new SolidBrush(black);
            SolidBrush whiteBrush = new SolidBrush(white);
            SolidBrush greenBrush = new SolidBrush(green);
            SolidBrush redBrush = new SolidBrush(red);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush MarkBrush = new SolidBrush(Color.Orange);
            int side_len = 40;
            int adjustment = 0;
            Rectangle tile = new Rectangle(20, 20, 20, 20);
            Rectangle circle = new Rectangle(20, 20, 20, 20);

            //e.Graphics.DrawRectangle(blackPen, square);
            Piece current_piece;
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


                    if (tiles[i, j].Occupied)
                    {
                        circle = new Rectangle(10 + (side_len * (j + 1)), 10 + ((side_len * (i + 1))), side_len / 2, side_len / 2);
                        e.Graphics.FillEllipse(blueBrush, circle);
                        //if (first_time) { 
                        //MessageBox.Show($"The tile {j} {i} has been repainted");
                        //first_time = false;
                        // }
                    }

                    if (tiles[i, j].Moveable)
                    {
                        circle = new Rectangle(10 + (side_len * (j + 1)), 10 + ((side_len * (i + 1))), side_len / 2, side_len / 2);
                        e.Graphics.FillEllipse(greenBrush, circle);
                    }

                    if(tiles[i, j].Marked){
                        circle = new Rectangle(10 + (side_len * (j + 1)), 10 + ((side_len * (i + 1))), 2 * side_len / 3, 3 * side_len / 4);
                        e.Graphics.FillEllipse(MarkBrush, circle);
                    }

                }
                adjustment++;
                adjustment = adjustment % 2;
            }
            for (int i = 0; i < 16; i++)
            {
                current_piece = white_pieces[i];
                circle = new Rectangle(10 + (side_len * (current_piece.X_pos + 1)), 10 + ((side_len * (current_piece.Y_pos + 1))), (side_len / 2) - 2, (side_len / 2) - 2);
                 e.Graphics.FillEllipse(redBrush, circle);

                current_piece = black_pieces[i];
                circle = new Rectangle(10 + (side_len * (current_piece.X_pos + 1)), 10 + ((side_len * (current_piece.Y_pos + 1))), (side_len / 2) - 2, (side_len / 2) - 2);
                e.Graphics.FillEllipse(blueBrush, circle);
            }
        }




        //setting up game

            private void Game_Load(object sender, EventArgs e)
            {

 
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        tiles[i, j] = new Tile(j, i, false, false);
                        // tiles[x,y] correspondes to the position y,x
                        pieces[i, j] = new Knight(j, i, true);
                    }
                }

                
                for (int i = 0; i < 8; i++)
                {
                    black_pieces[i] = new Pawn(i, 1, false);
                    white_pieces[i] = new Pawn(i, 6, true);

                }
                
                Gamesetup();
               //PieceDisplay();
                //for(int i = 0; i < 8; i++)
                //{
                //    tiles[6, i].occupy();
                //    tiles[7,i].occupy();
                //}

            }

           
        private void Gamesetup()
        {
            white_pieces[8] = new Rook(0, 7, true);
            white_pieces[9] = new Knight(1, 7, true);
            white_pieces[10] = new Bishop(2, 7, true);

            white_pieces[11] = new King(3, 7, true);
            white_pieces[12] = new Queen(4, 7, true);
            white_pieces[14] = new Knight(6, 7, true);
            white_pieces[13] = new Bishop(5, 7, true);
            white_pieces[15] = new Rook(7, 7, true);

            black_pieces[8] = new Rook(0, 0, false);
            black_pieces[9] = new Knight(1, 0, false);
            black_pieces[10] = new Bishop(2, 0, false);

            black_pieces[11] = new King(3, 0, false);
            black_pieces[12] = new Queen(4, 0, false);
            black_pieces[14] = new Knight(6, 0, false);
            black_pieces[13] = new Bishop(5, 0, false);
            black_pieces[15] = new Rook(7, 0, false);

        }

        private void PieceDisplay()
        {
            int x = 0;
            int y = 0;
            for(int i = 0;i< 16; i++)
            {
                x = black_pieces[i].X_pos;
                y = black_pieces[i].Y_pos;

                tiles[y,x].occupy();
                
            }
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X-40;
            int y = e.Y-40;
            if(x >= 0 && x <= 320 && y>= 0 && y <= 320 && piece_selected ==false && move_select == false) //sidelength, checks wether on board
            {
               // tiles[(y / 40), (x / 40)].occupy();

               // MessageBox.Show($"Repainting, X, {x / 40} Y,{y / 40}, mouse position {e.X} {e.Y}");
                
                x_pos_global = x / 40;
                y_pos_global = y / 40;

                selected_x = x_pos_global;
                selected_y = y_pos_global;
                if (Find_Piece(x_pos_global, y_pos_global) != null)
                {
                    piece_selected = true;
                    
                }
            }
            //selected_piece.possible_moves()
            if (piece_selected)
            {
                
                selected_piece = Find_Piece(x_pos_global,y_pos_global);
                Move_piece(selected_piece);

              move_select = true;
              piece_selected= false;

               
            }

            else if (move_select)
            {
                //MessageBox.Show("Move has been selected");
              //  MessageBox.Show("Move select");
                if (x >= 0 && x <= 320 && y >= 0 && y <= 320) //sidelength, checks wether on board
                {

                    
                    //MessageBox.Show($"{possible_moves(selected_piece)[0][1]}");

                    List<int[]> func_moves = possible_moves(selected_piece);
                    // List<int[]> meth_moves = selected_piece.possible_moves();

                    // MessageBox.Show()
                    x_pos_global = x / 40;
                    y_pos_global = y / 40;
                    ////TESTING CODE
                    //MessageBox.Show($"{func_moves[0][0]} {func_moves[0][1]} being marked");
                    //MessageBox.Show($"{selected_piece.X_pos} {selected_piece.Y_pos} PIECE LOCATION");
                    //MessageBox.Show($"{x_pos_global} {y_pos_global} DESIRED LOCATION");
                    foreach (int[] move in func_moves)
                    {
                        //tiles[move [1], move [0]].mark();
                       
                        if (move[0] ==x_pos_global && move[1] == y_pos_global)
                        {
                            tiles[selected_y,selected_x].clear();
                            selected_piece.move(x_pos_global, y_pos_global);
                            move_select = false;
                            tiles[y_pos_global,x_pos_global].clear();
                            
                            //MessageBox.Show("Move made");
                        }

                        tiles[move[1],move[0]].clear();
                      //  MessageBox.Show($"{move[0]} {move[1]} attempted to move to");
                        
                    }

                   
                   move_select= false; piece_selected = false;
                }

                else
                {
                    move_select=false;
                    Clear_Board();
                }

            }

            Invalidate();

        }


        private void Move_piece(Piece piece)
        {
            //foreach (var move in piece.possible_moves())
            //{
            //    tiles[move[1], move[0]].move();
            //    //MessageBox.Show($" X val {move[0]} Y val {move[1]}");
            //}

            foreach (var move in possible_moves(piece))
            {
                tiles[move[1], move[0]].move();
                //MessageBox.Show($" X val {move[0]} Y val {move[1]}");
            }
        }

        private void board_paint(object sender, PaintEventArgs e)
        {
            
        }
        


        private List<int[]> possible_moves(Piece piece)
        {
            List<int[]> moves = new List<int[]>();

            // previously did not work when this code removed, made it move from next position rather than intended positon
            int x_pos = piece.X_pos;
            int y_pos = piece.Y_pos;

            if (piece.GetType() == typeof(Knight))
            {
                
                return Knight_moves((Knight)piece);
            }

            else if (piece.GetType() == typeof(Bishop))
            {

                return Bishop_moves((Bishop)piece);

            }

            else if (piece.GetType() == typeof(Rook))
            {

                return Rook_moves((Rook)piece);
            }

            else if (piece.GetType()== typeof(Queen))
            {

                return Queen_moves(piece);
            }

            else if(piece.GetType() == typeof(King))
            {
                int x_val = x_pos_global - 1;
                int y_val = y_pos_global - 1;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (x_val + j < 8 && y_val + i < 8 && x_val + j >= 0 && y_val + i >= 0)
                        {
                            if(Find_Piece(x_val+j, y_val+i) == null) { moves.Add(new int[2] { x_val + j, y_val + i }); }
                            
                        }
                    }
                }


                return moves;
            }

            else if(piece.GetType() == typeof(Pawn))
            {
                Pawn pawn = (Pawn)piece;
                x_pos = pawn.X_pos;
                y_pos = pawn.Y_pos;
                int color_correct = 1;
                if (piece.White) color_correct *= -1;

                if (y_pos< 7 && y_pos  > 0 && Find_Piece(x_pos,y_pos+color_correct) == null)
                {
                    moves.Add(new int[2] { x_pos, y_pos + color_correct });
                    if (!((pawn.Moved)) && Find_Piece(x_pos, y_pos + color_correct+color_correct) == null)
                    {
                        moves.Add(new int[2] { x_pos, y_pos + color_correct + color_correct });
                    }

                }

                //diagonal capture

                if (Find_Piece(x_pos + 1, y_pos + color_correct)!= null) {
                    if(Enemy_check(pawn,Find_Piece(x_pos +1, y_pos + color_correct)))
                    {
                        moves.Add(new int[2] { x_pos + 1, y_pos + color_correct });
                    }
                }

                if (Find_Piece(x_pos - 1, y_pos + color_correct) != null)
                {
                    if (Enemy_check(pawn, Find_Piece(x_pos - 1, y_pos + color_correct)))
                    {
                        moves.Add(new int[2] { x_pos - 1, y_pos + color_correct });
                    }
                }

                return (moves);
            }

            else
            {
                return null;
            }

        }

        private Piece Find_Piece(int x, int y)
        {
            int n = 0;
            foreach (Piece piece in white_pieces) {
                if(piece.X_pos == x && piece.Y_pos == y) { return white_pieces[n]; }
                n++;
            }
            n = 0;
            foreach (Piece piece in black_pieces)
            {
                if (piece.X_pos == x && piece.Y_pos == y) { return black_pieces[n]; }
                n++;
            }

            return null;
        }

        private void Clear_Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tiles[i, j].clear();
                }
            }
            Invalidate();
        }


        private bool Enemy_check(Piece attacker , Piece victim)
        {
            return (!(attacker.White == victim.White));
        }
        private List<int[]> Knight_moves(Knight knight)
        {
            List<int[]> moves = new List<int[]>();
            int x_mod = 2;
            int y_mod = 1;
            int x_pos = knight.X_pos;
            int y_pos = knight.Y_pos;
            for (int j = 0; j < 2; j++)
            {

                for (int i = 0; i < 4; i++)
                {
                    if (x_pos + x_mod < 8 && y_pos + y_mod < 8 && x_pos + x_mod >= 0 && y_pos + y_mod >= 0)
                    {
                        if(Find_Piece(x_pos+x_mod,y_pos+y_mod) == null)
                        {
                            moves.Add(new int[2] { x_pos + x_mod, y_mod + y_pos });
                        }
                        
                    }
                    y_mod *= -1;

                    if (i == 1)
                    {
                        x_mod *= -1;
                    }
                }

                x_mod = 1;
                y_mod = 2;

            }


            return moves;

        }

        private List<int[]> Bishop_moves(Piece bishop)
        {
            List<int[]> moves = new List<int[]>();
            int x_pos = bishop.X_pos;
            int y_pos = bishop.Y_pos;
            //int n = 1;
            //bool flag = true;
            //while (flag)
            //{
            //    flag = false;
            //    if (x_pos + n < 8 && y_pos + n < 8) { moves.Add(new int[2] { x_pos + n, y_pos + n }); flag = true; }
            //    if (x_pos - n >= 0 && y_pos - n >= 0) { moves.Add(new int[2] { x_pos - n, y_pos - n }); flag = true; }
            //    if (x_pos + n < 8 && y_pos - n >= 0) { moves.Add(new int[2] { x_pos + n, y_pos - n }); flag = true; }
            //    if (x_pos - n >= 0 && y_pos + n < 8) { moves.Add(new int[2] { x_pos - n, y_pos + n }); flag = true; }
            //    n++;

            //}
            moves = traverse(x_pos, y_pos, 1, 1).Concat(traverse(x_pos, y_pos, -1, -1)).Concat(traverse(x_pos, y_pos, 1, -1)).Concat(traverse(x_pos, y_pos, -1, 1)).ToList();
            return moves;
        }

        private List<int[]> Rook_moves(Piece rook)
        {
            List<int[]> moves = new List<int[]>();

            int x_pos = rook.X_pos;
            int y_pos = rook.Y_pos;

            moves = traverse(x_pos, y_pos,1,0).Concat(traverse(x_pos, y_pos, 0, 1)).Concat(traverse(x_pos, y_pos, -1, 0)).Concat(traverse(x_pos, y_pos, 0, -1)).ToList();
            

            //int n = 1;
            //bool flag = true;
            //while (flag)
            //{
            //    // add piece detection later
            //    // recursive checks??
            //    flag = false;
            //    if (x_pos + n < 8) { moves.Add(new int[2] { x_pos + n, y_pos }); flag = true; }//right
            //    if (x_pos - n >= 0) { moves.Add(new int[2] { x_pos - n, y_pos }); flag = true; }
            //    if (y_pos - n >= 0) { moves.Add(new int[2] { x_pos, y_pos - n }); flag = true; }
            //    if (y_pos + n < 8) { moves.Add(new int[2] { x_pos, y_pos + n }); flag = true; }
            //    n++;

            //}

            return moves;

        }

        private List<int[]> Queen_moves(Piece queen)
        {
            List<int[]> moves = new List<int[]>();
            //append rook & bishop
            moves = Rook_moves(queen).Concat(Bishop_moves(queen)).ToList();
            


            return moves;
        }

        private List<int[]> traverse(int x_pos, int y_pos, int x_direct, int y_direct, List<int[]> moves = null)
        {
            if(moves== null) { moves = new List<int[]>(); } // because i cannot preset the parameter moves as an empty list i do it here

            if (Find_Piece(x_pos+x_direct,y_pos+y_direct) != null||x_pos + x_direct < 0 || y_pos + y_direct < 0 || x_pos + x_direct > 7 || y_pos + y_direct > 7)
            {
               // MessageBox.Show("BASECASE");
                //MessageBox.Show(x_pos.ToString());
                //MessageBox.Show(y_pos.ToString());
                //MessageBox.Show(x_direct.ToString());
                //MessageBox.Show(x_direct.ToString());
                return moves;
            }

            else
            {
                moves.Add(new int[] { x_pos+x_direct, y_pos+y_direct });
                //MessageBox.Show($"{moves[0][0].ToString()}, {moves[0][1]}");
                
                return traverse(x_pos + x_direct,y_pos+y_direct,x_direct,y_direct,moves);

            }

        }

        private void test_knight_Click(object sender, EventArgs e)
        {
            Knight knight = new Knight(x_pos_global,y_pos_global,false);
            tiles[y_pos_global,x_pos_global].occupy();
            Move_piece(knight);
            MessageBox.Show("Knight moves");
            Invalidate();
        }

        private void Bishop_test_Click(object sender, EventArgs e)
        {
            Bishop bishop = new Bishop(x_pos_global, y_pos_global,false);
            tiles[y_pos_global,x_pos_global].occupy();
            Move_piece(bishop);
            MessageBox.Show("Bishop moves");
            Invalidate();
        }

        private void Rook_test_Click(object sender, EventArgs e)
        {
            Rook rook = new Rook(x_pos_global, y_pos_global, false);
            tiles[y_pos_global, x_pos_global].occupy();
            Move_piece(rook);
            MessageBox.Show("Rook moves");
            Invalidate();
        }

        private void Queen_test_button_Click(object sender, EventArgs e)
        {
            Queen queen = new Queen(x_pos_global, y_pos_global, false);
            tiles[y_pos_global, x_pos_global].occupy();
            Move_piece(queen);
            MessageBox.Show("Queen moves");
            Invalidate();
        }

        private void pawntestButton_Click(object sender, EventArgs e)
        {
            Pawn pawn = new Pawn(x_pos_global, y_pos_global, true);
            pawn.displace();
            tiles[y_pos_global, x_pos_global].occupy();
            Move_piece(pawn);
            MessageBox.Show("pawn moves");

            foreach (int[] move in possible_moves(pawn))
            {
                Console.WriteLine($"{move[0]}, {move[1]}");
            }

            
            Invalidate();
        }

        private void Kingtestbutton_Click(object sender, EventArgs e)
        {
            King king = new King(x_pos_global,y_pos_global,false);
            tiles[y_pos_global, x_pos_global].occupy();
            Move_piece(king);
            MessageBox.Show("king moves");
            Invalidate();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            Clear_Board();
        }
    }
}



