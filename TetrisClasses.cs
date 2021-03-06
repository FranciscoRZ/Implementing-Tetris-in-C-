using System;
using System.Linq;

namespace Tetris_v2
{
    public class GameBoard
    {
        // attributs
        public int[][] landed;
        public int[][] grid;

        // méthodes

        // méthode construisant les bordures
        public void Build()
        {
            Console.SetWindowSize(50, 150);

            // écriture du titre du jeu
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(37, 2);
            Console.WriteLine("TETRIS");

            // construction des bordures verticales
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < 40; i ++)
            {
                Console.SetCursorPosition(4, 5 + i);
                Console.WriteLine("@");
                Console.SetCursorPosition(5, 5 + i);
                Console.WriteLine("@");
                Console.SetCursorPosition(75, 5 + i);
                Console.WriteLine("@");
                Console.SetCursorPosition(76, 5 + i);
                Console.WriteLine("@");
            }
            // construction des lignes horizontales
            for (int i = 0; i < 73; i++)
            {
                Console.SetCursorPosition(4 + i, 4);
                Console.WriteLine("@");
                Console.SetCursorPosition(4 + i, 45);
                Console.WriteLine("@");
                Console.SetCursorPosition(4 + i, 46);
                Console.WriteLine("@");
                Console.SetCursorPosition(4 + i, 47);
                Console.WriteLine("@");
            }
        }

        public int[][] FillLanded()
        {
            int[][] landed1 = new int[40][];
            for (int i = 0; i < 40; i++)
            {
                landed1[i] = new int[69];
                for (int j = 0; j < 69; j++)
                {
                    landed1[i][j] = 0;
                }
            }
            return landed1;
        }

        public void ShowLanded()
        {
            for (int i = 0; i < this.landed.Length; i++)
            {
                for (int j = 0; j < this.landed[i].Length; j++)
                {
                    Console.SetCursorPosition(6 + j, 5 + i);
                    if (this.landed[i][j] == 1) 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(this.landed[i][j]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(this.landed[i][j]);
                    }
                }
            }
        }

        public void UpdateLanded(Tetromino T)
        {
            for (int i = 0; i < T.rotations[T.indicator].Length; i++)
            {
                for (int j = 0; j < T.rotations[T.indicator][i].Length; j++)
                {
                    if (this.landed[T.TopLeft[1] + i][T.TopLeft[0] + j] != 1)
                    {
                        this.landed[T.TopLeft[1] + i][T.TopLeft[0] + j] = T.rotations[T.indicator][i][j];    
                    }
                }
            }
        }

        //public void ClearLines()

        public bool IsLineFull(int line)
        {
            bool full = false;

            int val = landed[line].Sum();
            if (val == landed[line].Length)
            {
                full = true;    
            }

            return full;
        }

        public bool IsGameOver()
        {
            bool Over = false;

            if (IsLineFull(0))
            {
                Over = true;
            }

            return Over;
        }


        // constructeur
        public GameBoard()
        {
        }
    }

    public class Tetromino : GameBoard
    {
        // attributs

        public int[] TopLeft;
        public int[] potentialTopLeft;
        public int[][][] rotations;
        public ConsoleColor colour;
        public int indicator = 0;
        public int potentialIndicator = 0;
        public bool land;

        // méthodes

        public void Show()
        {
            Console.ForegroundColor = this.colour;
            Console.SetCursorPosition(this.TopLeft[0], this.TopLeft[1]);

            for (int i = 0; i < this.rotations[this.indicator].Length; i++)
            {
                for (int j = 0; j < this.rotations[this.indicator][i].Length; j++)
                {
                    Console.SetCursorPosition(this.TopLeft[0] + j + 6, this.TopLeft[1] + i + 5);

                    if (this.rotations[this.indicator][i][j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(this.rotations[this.indicator][i][j]);
                    }
                    else
                    {
                        Console.ForegroundColor = this.colour;
                        Console.WriteLine(this.rotations[this.indicator][i][j]);
                    }
                }
            }
        }

        public void Clear()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < this.rotations[this.indicator].Length; i++)
            {
                for (int j = 0; j < this.rotations[this.indicator][i].Length; j++)
                {
                    Console.SetCursorPosition(this.TopLeft[0] + 6 + j, this.TopLeft[1] + 5 + i);
                    Console.WriteLine(0);
                }
            }
        }

        public bool CanIShow(GameBoard gb)
        {

            bool show = true;

            int line;
            int col;
            int val;

            for (int i = 0; i < this.rotations[this.potentialIndicator].Length; i++)
            {
                for (int j = 0; j < this.rotations[this.potentialIndicator][i].Length; j++)
                {
                    line = this.potentialTopLeft[1] + i;
                    col = this.potentialTopLeft[0] + j;
                    try
                    {
						val = gb.landed[line][col];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // cas où l'on sort de la grille
                        val = 1;
                    }

                    if (val == 1)
                    {
                        if (this.rotations[this.potentialIndicator][i][j] == 1)
                        {
                            show = false;
                        }
                    }
                }
            }

            return show;
        }

        public void DidILand(GameBoard gb)
        {
            // vérifier si on est à la dernière ligne
            if (this.TopLeft[1] + this.rotations[this.indicator].Length == this.landed.Length)
            {
                this.land = true;
                return;
            }
            // vérifier s'il y a eut impact
            for (int i = 0; i < this.rotations[this.indicator].Length; i++)
            {
                for (int j = 0; j < this.rotations[this.indicator][i].Length; j++)
                {
                    if (this.rotations[this.indicator][i][j] == 1 && gb.landed[this.TopLeft[1] + i + 1][this.TopLeft[0] + j] == 1)
                    {
                        this.land = true;
                    }
                }
            }
        }

        public void GetInput(GameBoard gb, ConsoleKey input)
        {
            //ConsoleKey input = Console.ReadKey().Key;

            if (input == ConsoleKey.Spacebar)
            {
                // afficher la rotation si possible
                if (this.indicator + 1 < this.rotations.Length)
                {
                    this.potentialTopLeft[0] = this.TopLeft[0];
                    this.potentialTopLeft[1] = this.TopLeft[1];
                    this.potentialIndicator = this.indicator + 1;
                    if (this.CanIShow(gb))
                    {
                        this.Clear();
						this.indicator = this.potentialIndicator;
                        this.Show();
                    }
                }
                else
                {
                    this.potentialTopLeft[0] = this.TopLeft[0];
                    this.potentialTopLeft[1] = this.TopLeft[1];
                    this.potentialIndicator = 0;
                    if (this.CanIShow(gb))
                    {
                        this.Clear();
						this.indicator = this.potentialIndicator;
                        this.Show();
                    }
                }
            }
            if (input == ConsoleKey.RightArrow)
            {  
                // afficher le tetromino à la colonne de droite si possible
                if (this.TopLeft[0] + 1 < 69)
                {
                    this.potentialTopLeft[0] = this.TopLeft[0] + 1;
                    this.potentialIndicator = this.indicator;
                    if (this.CanIShow(gb))
                    {
                        this.Clear();
                        this.TopLeft[0] = this.potentialTopLeft[0];
                        this.Show();
                    }
                    else
                    {
                        this.potentialTopLeft[0] = this.TopLeft[0];
                    }
                }
            }
            if (input == ConsoleKey.LeftArrow)
            {
                // afficher le tetromino à la colonne de gauche si possible
                if (this.TopLeft[0] > 0)
                {
                    this.potentialTopLeft[0] = this.TopLeft[0] - 1;
                    this.potentialIndicator = this.indicator;
                    if (this.CanIShow(gb))
                    {
                        this.Clear();
                        this.TopLeft[0] = this.potentialTopLeft[0];
                        this.Show();
                    }
                    else
                    {
                        this.potentialTopLeft[0] = this.TopLeft[0];
                    }
                }
            }
            if (input == ConsoleKey.DownArrow)
            {
                // descendre
                if (this.TopLeft[1] + 1 < 40)
                {
                    // vérifier si on est arrivé en bas
                    this.DidILand(gb);
                    if (!this.land)
                    {
                        this.potentialTopLeft[1] = this.TopLeft[1] + 1;
                        this.potentialIndicator = this.indicator;
                        if (this.CanIShow(gb))
                        {
                            this.Clear();
                            this.TopLeft[1] = this.potentialTopLeft[1];
                            this.Show();
                        }
                        else
                        {
                            this.potentialTopLeft[1] = this.TopLeft[1];
                        }    
                    }

                }
            }
        }

        public void Initialize(GameBoard gb)
        {
            // initialiser la position où il va apparaître
            Random choose = new Random();
            int Col = choose.Next(0, 69);

            this.TopLeft = new int[2] { Col, 0 };
            this.potentialTopLeft = new int[2] { Col, 0 };
            this.land = false;

            this.DidILand(gb);

            while (this.land) 
            {
                Col = choose.Next(0, 69);
                this.TopLeft[0] = Col;
                this.potentialTopLeft[0] = Col;
                this.DidILand(gb);
            }

            // initialiser la rotation
            this.indicator = 0;
            this.potentialIndicator = 0;

        }


        // constructeurs
        public Tetromino(GameBoard gb)
        {
            this.landed = gb.landed;
        }
    }

    public class GameMaster : GameBoard
    {
        public void Choose()
        {
            
        }
    }

}
