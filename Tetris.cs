using System;
using System.Collections.Generic;
using System.Timers;
using System.Diagnostics;

namespace Tetris_v2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // ----------- PROJET C# : Tetris ------------

            // Construction du GameBoard
            GameBoard gb = new GameBoard();
            gb.Build();
            gb.landed = gb.FillLanded();
            gb.ShowLanded();

            // Initialisation des tetrominos

            Dictionary<int, Tetromino> GamePieces = new Dictionary<int, Tetromino>();

            // ------ I -------
            Tetromino I = new Tetromino(gb);

            I.rotations = new int[2][][];

            I.rotations[0] = new int[4][];
            I.rotations[0][0] = new int[1] { 1 };
            I.rotations[0][1] = new int[1] { 1 };
            I.rotations[0][2] = new int[1] { 1 };
            I.rotations[0][3] = new int[1] { 1 };

            I.rotations[1] = new int[1][];
            I.rotations[1][0] = new int[4] { 1, 1, 1, 1 };

            I.colour = ConsoleColor.Cyan;

            GamePieces.Add(1, I);

            // ------ J -------
            Tetromino J = new Tetromino(gb);

            J.rotations = new int[4][][];

            J.rotations[0] = new int[3][];
            J.rotations[0][0] = new int[2] { 0, 1 };
            J.rotations[0][1] = new int[2] { 0, 1 };
            J.rotations[0][2] = new int[2] { 1, 1 };

            J.rotations[1] = new int[2][];
            J.rotations[1][0] = new int[3] { 1, 0, 0 };
            J.rotations[1][1] = new int[3] { 1, 1, 1 };

            J.rotations[2] = new int[3][];
            J.rotations[2][0] = new int[2] { 1, 1 };
            J.rotations[2][1] = new int[2] { 1, 0 };
            J.rotations[2][2] = new int[2] { 1, 0 };

            J.rotations[3] = new int[2][];
            J.rotations[3][0] = new int[3] { 1, 1, 1 };
            J.rotations[3][1] = new int[3] { 0, 0, 1 };

            J.colour = ConsoleColor.Blue;

            GamePieces.Add(2, J);

            // ------ L -------
            Tetromino L = new Tetromino(gb);

            L.rotations = new int[4][][];

            L.rotations[0] = new int[3][];
            L.rotations[0][0] = new int[2] { 1, 0 };
            L.rotations[0][1] = new int[2] { 1, 0 };
            L.rotations[0][2] = new int[2] { 1, 1 };

            L.rotations[1] = new int[2][];
            L.rotations[1][0] = new int[3] { 1, 1, 1 };
            L.rotations[1][1] = new int[3] { 1, 0, 0 };

            L.rotations[2] = new int[3][];
            L.rotations[2][0] = new int[2] { 1, 1 };
            L.rotations[2][1] = new int[2] { 0, 1 };
            L.rotations[2][2] = new int[2] { 0, 1 };

            L.rotations[3] = new int[2][];
            L.rotations[3][0] = new int[3] { 0, 0, 1 };
            L.rotations[3][1] = new int[3] { 1, 1, 1 };

            L.colour = ConsoleColor.DarkYellow;

            GamePieces.Add(3, L);

            // ------ O -------
            Tetromino O = new Tetromino(gb);

            O.rotations = new int[1][][];

            O.rotations[0] = new int[2][];
            O.rotations[0][0] = new int[2] { 1, 1 };
            O.rotations[0][1] = new int[2] { 1, 1 };

            O.colour = ConsoleColor.Yellow;

            GamePieces.Add(4, O);

            // ------ S -------
            Tetromino S = new Tetromino(gb);

            S.rotations = new int[2][][];

            S.rotations[0] = new int[2][];
            S.rotations[0][0] = new int[3] { 0, 1, 1 };
            S.rotations[0][1] = new int[3] { 1, 1, 0 };

            S.rotations[1] = new int[3][];
            S.rotations[1][0] = new int[2] { 1, 0 };
            S.rotations[1][1] = new int[2] { 1, 1 };
            S.rotations[1][2] = new int[2] { 0, 1 };

            S.colour = ConsoleColor.Green;

            GamePieces.Add(5, S);

            // ------ T -------
            Tetromino T = new Tetromino(gb);

            T.rotations = new int[4][][];

            T.rotations[0] = new int[2][];
            T.rotations[0][0] = new int[3] { 1, 1, 1 };
            T.rotations[0][1] = new int[3] { 0, 1, 0 };

            T.rotations[1] = new int[3][];
            T.rotations[1][0] = new int[2] { 0, 1 };
            T.rotations[1][1] = new int[2] { 1, 1 };
            T.rotations[1][2] = new int[2] { 0, 1 };

            T.rotations[2] = new int[2][];
            T.rotations[2][0] = new int[3] { 0, 1, 0 };
            T.rotations[2][1] = new int[3] { 1, 1, 1 };

            T.rotations[3] = new int[3][];
            T.rotations[3][0] = new int[2] { 1, 0 };
            T.rotations[3][1] = new int[2] { 1, 1 };
            T.rotations[3][2] = new int[2] { 1, 0 };

            T.colour = ConsoleColor.Magenta;

            GamePieces.Add(6, T);

            // ------- Z ------
            Tetromino Z = new Tetromino(gb);

            Z.rotations = new int[2][][];

            Z.rotations[0] = new int[2][];
            Z.rotations[0][0] = new int[3] { 1, 1, 0 };
            Z.rotations[0][1] = new int[3] { 0, 1, 1 };

            Z.rotations[1] = new int[3][];
            Z.rotations[1][0] = new int[2] { 0, 1 };
            Z.rotations[1][1] = new int[2] { 1, 1 };
            Z.rotations[1][2] = new int[2] { 1, 0 };

            Z.colour = ConsoleColor.Red;

            GamePieces.Add(7, Z);

            // ------ Lancement du jeu ----------

            T.TopLeft = new int[2] { 0, 0 };
            T.potentialTopLeft = new int[2];
            T.potentialTopLeft[0] = T.TopLeft[0];
            T.potentialTopLeft[1] = T.TopLeft[1];

            bool Over = false;

            //TimeSpan down = TimeSpan.FromMilliseconds(500);
            //DateTime start_time;

            Stopwatch goDown = new Stopwatch();

            ConsoleKey input;

            while(!Over)
            {
                // Choisir le tetromino
                Random Choser = new Random();
                int WhichPiece = Choser.Next(1, 8);

                // Initialiser le tetromino choisi
                GamePieces[WhichPiece].Initialize(gb);


                // Jouer la pìece choisie
                while (GamePieces[WhichPiece].land == false)
                {
                    // Input joueur
                    goDown.Reset();
                    goDown.Start();

                    if (goDown.ElapsedMilliseconds > 500)
                    {
                        goDown.Stop();
						input = ConsoleKey.DownArrow;
                        GamePieces[WhichPiece].GetInput(gb, input);
                    }
                    else
                    {
                        input = Console.ReadKey().Key;
                        GamePieces[WhichPiece].GetInput(gb, input);
                    }

                    // Jouer
                    GamePieces[WhichPiece].DidILand(gb);
                }

                gb.UpdateLanded(GamePieces[WhichPiece]);
                gb.ShowLanded();

                // Vérifier si le jeu est fini
				Over = gb.IsGameOver();
				
            }

        }
    }
}
