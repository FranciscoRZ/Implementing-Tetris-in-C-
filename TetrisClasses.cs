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

        // constructeur
        public GameBoard()
        {
        }
    }

    public class Tetromino
    {
        // attributs
        public int[,] shape;
        public int[] TopLeft;
        public int[] potentialTopLeft;
        public int[][,] rotations;
        public ConsoleColor colour;
        public int indicator = 0;
        public int[][] landed;

        // méthodes
        public void Show()
        {
            Console.ForegroundColor = this.colour;
            Console.SetCursorPosition(this.TopLeft[0], this.TopLeft[1]);

            for (int i = 0; i <= this.rotations[this.indicator].GetUpperBound(0); i++)
            {
                for (int j = 0; j <= this.rotations[this.indicator].GetUpperBound(1); j++)
                {
                    Console.SetCursorPosition(this.TopLeft[0] + i, this.TopLeft[1] + j);
                    Console.WriteLine(this.rotations[this.indicator][i, j]);
                }

            }
        }

        public void Clear()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i <= this.rotations[this.indicator].GetUpperBound(0); i++)
            {
                for (int j = 0; j <= this.rotations[this.indicator].GetUpperBound(1); j++)
                {
                    Console.SetCursorPosition(this.TopLeft[0] + i, this.TopLeft[1] + j);
                    Console.WriteLine(0);
                }
            }
        }

        public bool CanIShow()
        {
            bool show = true;

            for (int i = 0; i <= this.rotations[this.indicator].GetUpperBound(0); i++) // boucle sur les colonnes
            {
                for (int j = 0; j <= this.rotations[this.indicator].GetUpperBound(1); j++) // boucle sur les lignes
                {
                    int impact = 0;
                    try
                    {
                        impact = this.landed[this.potentialTopLeft[0] + j][this.potentialTopLeft[1] + i];    // cas où on ne sort pas de la grille
                    }
                    catch(IndexOutOfRangeException)
                    {
                        impact = 1; // cas où on sort de la grille
                    }


                    if ((this.rotations[this.indicator][i,j] == 1) && (impact == 1))
                    {
                        show = false;
                    }
                }
            }

            return show;
        }


        public void GetInput()
        {
            ConsoleKey input = Console.ReadKey().Key;

            if (input == ConsoleKey.Spacebar)
            {
                // afficher la rotation si possible
                if (this.indicator + 1 < this.rotations.Length)
                {
                    this.indicator = this.indicator + 1;
                    this.potentialTopLeft = this.TopLeft;
                    if (this.CanIShow())
                    {
                        this.Clear();
                        this.Show();
                    }
                }
                else
                {
                    this.indicator = 0;
                    this.potentialTopLeft = this.TopLeft;
                    if (this.CanIShow())
                    {
						this.Clear();
                        this.Show();
                    }
                }
            }
            if (input == ConsoleKey.RightArrow)
            {  
                // afficher le tetromino à la colonne de droite si possible
                if (this.TopLeft[0] + 1 < 75)
                {
                    this.potentialTopLeft[0] = this.TopLeft[0] + 1;
                    if (this.CanIShow())
                    {
                        this.Clear();
                        this.TopLeft = this.potentialTopLeft;
                        this.Show();
                    }
                }
            }
            if (input == ConsoleKey.LeftArrow)
            {
                // afficher le tetromino à la colonne de gauche si possible
                if (this.TopLeft[0] - 1 > 5)
                {
                    this.potentialTopLeft[0] = this.TopLeft[0] - 1;
                    if (this.CanIShow())
                    {
                        this.Clear();
                        this.TopLeft = this.potentialTopLeft;
                        this.Show();
                    }
                }
            }
            if (input == ConsoleKey.DownArrow)
            {
                // accélerer la descente
                if (this.TopLeft[1] + 1 < 45)
                {
                    this.potentialTopLeft[1] = this.TopLeft[1] + 1;
                    if (this.CanIShow()) 
                    {
						this.Clear();
                        this.TopLeft = this.potentialTopLeft;
                        this.Show();
                    }
                }
            }
        }

        // constructeurs
        public Tetromino()
        {
                    
        }
    }
}
