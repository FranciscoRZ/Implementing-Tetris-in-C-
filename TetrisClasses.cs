using System;
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
            int[] arr = new int[69];

            for (int j = 0; j < 69; j++)
            {
                arr[j] = 0;
            }

            for (int i = 0; i < 40; i++)
            {
                landed1[i] = arr;    
            }
            return landed1;
        }

        public void ShowLanded()
        {
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < this.landed.Length; i++)
            {
                for (int j = 0; j < this.landed[1].Length; j++)
                {
                    Console.SetCursorPosition(6 + j, 5 + i);
                    Console.WriteLine(this.landed[i][j]);
                }
            }
        }

        //public void ClearLines()
        //{
            
        //}

        //public bool IsGameOver()
        //{
            
        //}

        //public bool IsLineFull()
        //{
            
        //}


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

        public bool CanIShow()
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
						val = this.landed[line][col];
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

        public void DidILand()
        {

            // récupérer l'indice de la dernière lignée de notre tetromino affiché
            int n = this.rotations[this.indicator].Length - 1;

            // vérifier si on est à la dernière ligne
            if (this.TopLeft[1] == 40)
            {
                this.land = true;
                return;
            }
            // vérifier s'il y a eut impact
            for (int i = 0; i < this.rotations[this.indicator][n].Length; i++)
            {
                if (this.rotations[this.indicator][n][i] == 1 && this.landed[this.TopLeft[1] + n][i] == 1)
                {
                    this.land = true;       
                }
            }

            return;
        }

        public void GetInput()
        {
            ConsoleKey input = Console.ReadKey().Key;

            if (input == ConsoleKey.Spacebar)
            {
                // afficher la rotation si possible
                if (this.indicator + 1 < this.rotations.Length)
                {
                    this.potentialTopLeft[0] = this.TopLeft[0];
                    this.potentialTopLeft[1] = this.TopLeft[1];
                    this.potentialIndicator = this.indicator + 1;
                    if (this.CanIShow())
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
                    if (this.CanIShow())
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
                    if (this.CanIShow())
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
                    if (this.CanIShow())
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
                // accélerer la descente
                if (this.TopLeft[1] + 1 < 40)
                {
                    // vérifier si on est arrivé en bas
                    this.DidILand();
                    if (!this.land)
                    {
                        this.potentialTopLeft[1] = this.TopLeft[1] + 1;
                        this.potentialIndicator = this.indicator;
                        if (this.CanIShow())
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

        // constructeurs
        public Tetromino(GameBoard gb)
        {
            this.landed = gb.landed;
        }
    }
}
