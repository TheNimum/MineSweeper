using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    enum GameSymbol
    {
        Flagged = 'F',
        NotSweeped = 'X',
        SweepedZeroCloseMine = '.'
    }

    enum GameOverSymbol
    {
        ExplodedMine = 'M',
        FlaggedMine = '#',
        Mine = 'm',
        MisplacedFlag = '!'
    }
    // Typ för en ruta på spelplanen.
    struct Square
    {

        private int closeMineCount; // Antal minor på intilliggande rutor
        private bool flagged, boobyTrapped, sweeped;
        private char symbol;

        // Konstruktor som initierar en ny ruta på spelplanen.
        public Square(bool isBoobyTrapped)
        {
            closeMineCount = 0;
            flagged = false;
            this.boobyTrapped = isBoobyTrapped;
            sweeped = false;
            symbol = 'X';
        }
        private int CloseMineCount
        {
            get { return closeMineCount; }
            set { closeMineCount = value; }
        }


        // Enbart läsbar egenskap som säger om rutan är flaggad för tillfället.
        public bool IsFlagged => flagged;

        // Enbart läsbar egenskap som säger om rutan är minerad.
        public bool BoobyTrapped => boobyTrapped;

        // Enbart läsbar egenskap som säger om rutan har blivit röjd. 
        public bool IsSweeped => sweeped;

        // Enbart läsbar egenskap som är symbolen som representerar rutan just nu 
        // om spelplanen skall ritas ut.
        public char Symbol => symbol;

        // Enbart skrivbar egenskap som tilldelas true för alla rutor om spelaren 
        // röjer en minerad ruta 
        public bool GameOver
        {
            set
            {
                if (value)
                {
                    if (flagged && boobyTrapped)
                    {
                        symbol = (char)GameOverSymbol.FlaggedMine;
                    }
                    if (sweeped && boobyTrapped)
                    {
                        symbol = (char)GameOverSymbol.ExplodedMine;
                    }
                    if (boobyTrapped && !sweeped && !flagged)
                    {
                        symbol = (char)GameOverSymbol.Mine;
                    }
                    if (flagged && !boobyTrapped)
                    {
                        symbol = (char)GameOverSymbol.MisplacedFlag;
                    }
                }

            }
        }

        // Öka räknaren av minor på intilliggande rutor med 1.
        public void IncrementCloseMineCount()
        {
            CloseMineCount += 1;

        }



        // Försök att flagga rutan. Returnerar false om ogiltigt drag, annars true.
        public bool TryFlag()
        {
            if (sweeped)
            {
                System.Console.WriteLine("can't flag this position.");
                return false;
            }
            else
            {
                if (flagged)
                {
                    symbol = (char)GameSymbol.NotSweeped; // denna tar bort flaggan om du plaserar på en redan flaggad position. 
                }
                else
                {
                    symbol = (char)GameSymbol.Flagged; // flaggar en position som är inte ockuperad. 
                }
                flagged = !flagged; // reversar flaggan.
                return true;
            }

        }


        // Försök röja rutan. Returnerar false om ogiltigt drag, annars true.
        public bool TrySweep()
        {
            if (!sweeped && !flagged)
            {
                sweeped = true;
                if (closeMineCount == 0)
                {
                    symbol = (char)GameSymbol.SweepedZeroCloseMine;
                }
                if (boobyTrapped)
                {

                    GameOver = true;
                }

                else if (closeMineCount > 0)
                {
                    symbol = char.Parse(closeMineCount.ToString());
                }
                return true;
            }else
            {
               System.Console.WriteLine("wrong command");
               return false; 
            }
        
        }

    }
}