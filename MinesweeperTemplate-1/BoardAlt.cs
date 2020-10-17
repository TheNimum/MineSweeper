using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{


    // Typ för spelplanen i minröj.
    struct Board
    {

        private bool isGameover, playerwon;
        private Square[,] board;
        private int flagCount, sweepedCount, maxFlagCount;



        // Konstruktor som initaliserar en ny spelplan.
        public Board(string[] args) // Stubbe
        {
            Helper.Initialize(args);
            isGameover = false;
            playerwon = false;


            board = new Square[10, 10];
            for (int row = 0; row < 10; ++row)
            {
                for (int col = 0; col < 10; ++col)
                {
                    board[row, col] = new Square(Helper.BoobyTrapped(row, col));

                }
            }

            for (int row = 0; row < 10; ++row)
            {
                for (int col = 0; col < 10; ++col)
                {

                    if (Helper.BoobyTrapped(row, col + 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row + 1, col))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row - 1, col))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row, col - 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }

                    if (Helper.BoobyTrapped(row + 1, col + 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row - 1, col - 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row - 1, col + 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }
                    if (Helper.BoobyTrapped(row + 1, col - 1))
                    {
                        board[row, col].IncrementCloseMineCount();
                    }

                }
            }

            flagCount = 0;
            sweepedCount = 0;
            maxFlagCount = 25;

        }
        // Enbart läsbar egenskap som säger som spelaren har vunnit spelet.
        public bool PlayerWon => playerwon;


        // Enbart läsbar egenskap som säger om spelaren har förlorat.
        public bool Gameover
        {
            get => isGameover;
            private set
            {
                isGameover = value;
                if (isGameover)
                {
                    for (int r = 0; r < 10; r++)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            board[r, c].GameOver = true;


                        }
                    }

                }

            }
        }

        // Försök röja en ruta. Returnerar false om ogiltigt drag, annars true.
        public bool TrySweep(int row, int col)
        {
            if (board[row, col].TrySweep())
            {
                if (board[row, col].BoobyTrapped)
                {
                    Gameover = true;
                }

                else if (board[row, col].Symbol == (char)GameSymbol.SweepedZeroCloseMine)
                {
                    for (int r = row - 1; r <= row + 1; r++)
                    {
                        for (int c = col - 1; c <= col + 1; c++)
                        {
                            if (r != -1 && r != 10 && c != -1 && c != 10)
                            {
                                if(!board[r,c].IsSweeped)
                                {
                                 TrySweep(r, c);
                                 continue;   
                                }
                                

                            }
                        }
                    }

                }
                sweepedCount++;

                if (sweepedCount == 90 && !board[row,col].IsSweeped) //  Det finns 10 minor av 100 rutor så 90 sweepes för att vinna.
                {
                    playerwon = true;

                }
                return true;
            }
            return false;
        }

        // Försök flagga en ruta. Returnerar false om ogiltigt drag, annars true.
        public bool TryFlag(int row, int col)
        {
            if (board[row, col].TryFlag() && flagCount <= maxFlagCount)
            {
                flagCount++;
                return true;
            }
            else
            {
                Console.WriteLine("out of flags.");
                return false;
            }


        }

        // Skriv ut spelplanen till konsolen.
        public void Print() 
        {
            System.Console.WriteLine("   A B C D E F G H I J");
            System.Console.WriteLine("  +-------------------");
            for (int row = 0; row < 10; row++)
            {
                System.Console.Write(row + "| ");
                for (int col = 0; col < 10; col++)
                {
                    System.Console.Write(board[row, col].Symbol + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
