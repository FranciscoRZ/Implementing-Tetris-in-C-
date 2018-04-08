using System;

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
            // ------ I -------
            Tetromino I = new Tetromino();
            I.shape = new int[1, 4] { { 1, 1, 1, 1 } };
            I.rotations = new int[2][,];
            I.rotations[0] = new int[1, 4] {
                {1,1,1,1}
            };
            I.rotations[1] = new int[4, 1]{
                {1},
                {1},
                {1},
                {1}
            };
            I.colour = ConsoleColor.Cyan;
            I.landed = gb.landed;
            // ------ J -------
            Tetromino J = new Tetromino();
            J.shape = new int[3, 2] {
                {0,1},
                {0,1},
                {1,1}
            };
            J.rotations = new int[4][,];
            J.rotations[0] = new int[3, 2]{
                {0,1},
                {0,1},
                {1,1}
            };
            J.rotations[1] = new int[2, 3]{
                {1,0,0},
                {1,1,1},
            };
            J.rotations[2] = new int[3, 2]{
                {1,1},
                {1,0},
                {1,0}
            };
            J.rotations[3] = new int[2, 3]{
                {1,1,1},
                {0,0,1},
            };
            J.colour = ConsoleColor.Blue;
            J.landed = gb.landed;
            // ------ L -------
            Tetromino L = new Tetromino();
            L.shape = new int[3, 2] {
                {1,0},
                {1,0},
                {1,1}
            };
            L.rotations = new int[4][,];
            L.rotations[0] = new int[3, 2]{
                {1,0},
                {1,0},
                {1,1}
            };
            L.rotations[1] = new int[2, 3]{
                {1,1,1},
                {1,0,0}
            };
            L.rotations[2] = new int[3, 2]{
                {1,1},
                {0,1},
                {0,1}
            };
            L.rotations[3] = new int[2, 3]{
                {0,0,1},
                {1,1,1}
            };
            L.colour = ConsoleColor.DarkYellow;
            L.landed = gb.landed;

            // ------ O -------
            Tetromino O = new Tetromino();
            O.shape = new int[2, 2]{
                {1,1},
                {1,1}
            };
            O.rotations = new int[1][,];
            O.rotations[0] = new int[2,2]{
                {1,1},
                {1,1}
            };
            O.colour = ConsoleColor.Yellow;
            O.landed = gb.landed;
            // ------ S -------
            Tetromino S = new Tetromino();
            S.shape = new int[2, 3]{
                {0,1,1},
                {1,1,0}
            };
            S.rotations = new int[2][,];
            S.rotations[0] = new int[2,3]{
                {0,1,1},
                {1,1,0}
            };
            S.rotations[1] = new int[3, 2]{
                {1,0},
                {1,1},
                {0,1}
            };
            S.colour = ConsoleColor.Green;
            S.landed = gb.landed;
            // ------ T -------
            Tetromino T = new Tetromino();
            T.shape = new int[2, 3]{
                {1,1,1},
                {0,1,0}
            };
            T.rotations = new int[4][,];
            T.rotations[0] = new int[2,3]{
                {1,1,1},
                {0,1,0}
            };
            T.rotations[1] = new int[3, 2]{
                {0,1},
                {1,1},
                {0,1}
            };
            T.rotations[2] = new int[2, 3]{
                {0,1,0},
                {1,1,1}
            };
            T.rotations[3] = new int[3, 2]{
                {1,0},
                {1,1},
                {1,0}
            };
            T.colour = ConsoleColor.Magenta;
            T.landed = gb.landed;
            // ------- Z ------
            Tetromino Z = new Tetromino();
            Z.shape = new int[2, 3]{
                {1,1,0},
                {0,1,1}
            };
            Z.rotations = new int[2][,];
            Z.rotations[0] = new int[2, 3]{
                {1,1,0},
                {0,1,1}
            };
            Z.rotations[1] = new int[3, 2]{
                {0,1},
                {1,1},
                {1,0}
            };
            Z.colour = ConsoleColor.Red;
            Z.landed = gb.landed;

            I.TopLeft = new int[2] { 6, 5 };
            I.potentialTopLeft = I.TopLeft;

            while(true)
            {
                I.GetInput();
            }

        }
    }
}
