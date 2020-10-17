using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    // Typ för minröjspelet. 
    struct MineSweeper
    {
        private Board board;
        private bool quit;


        // Konstruktor som initierare ett nytt spel med en slumpmässig spelplan.
        public MineSweeper(string[] args)
        {

            board = new Board(args);
            quit = false;

        }

        // Läs ett nytt kommando från användaren med giltig syntax och 
        // ett känt kommandotecken.
        static private string ReadCommand() 
        {

            while (true)
            {
                System.Console.Write("> ");
                string inpuT = System.Console.ReadLine();
                if(Console.IsInputRedirected)
                {
                    System.Console.WriteLine(inpuT);
                }
                if (inpuT.Length == 1 && Char.IsLetter(inpuT[0]))
                {
                    // giltig syntax
                }
                else if (inpuT.Length == 4 && Char.IsLetter(inpuT[0]) && inpuT[1] == ' ' && inpuT[2] >= 'a' && inpuT[2] <= 'j' && char.IsDigit(inpuT[3]))
                {
                    // giltigt syntax
                }
                else if(inpuT.Length <= 4 && inpuT.Length > 1) 
                {
                    
                    Console.WriteLine("syntax error.");
                    continue;
                }

                return inpuT;
            }
        }
        private void inputcommand(string inpuT)
        {
            if (inpuT[0] == 'f' && inpuT.Length == 4)
            {
                int col = inpuT[2] - 97; // 97 = A 
                int row = inpuT[3] - 48; //  48 = 0 
                board.TryFlag(row, col);
            }

            if (inpuT[0] == 'r' && inpuT.Length == 4)
            {
               
                int col = inpuT[2] - 97; // 97 = A 
                int row = inpuT[3] - 48; //  48 = 0 
                board.TrySweep(row, col);
                
            }
            
            if (inpuT[0] == 'q')
            {

                quit = true;

            }else if (inpuT[0] != 'r' && inpuT[0] != 'f' && inpuT.Length != 4 ) 
            {
                System.Console.WriteLine("unknown Command");
           
            }


        }



        // Kör spelet efter initering. Metoden returnerar när spelet tar 
        // slut genom att något av följande händer:
        // - Spelaren avslutade spelet med kommandot 'q'.
        // - Spelaren förlorade spelet genom att röja en minerad ruta. 
        // - Spelaren vann spelet genom att alla ej minerade rutor är röjda.
        public void Run() 
        {


            

            while (!(quit || board.PlayerWon || board.Gameover))
            {


                board.Print();
                string input = ReadCommand();
                inputcommand(input);
                if(board.Gameover)
                {
                    board.Print();
                    System.Console.WriteLine("GAME OVER! ");
                    Environment.Exit(1);
                }
                else if (board.PlayerWon)
                {
                    board.Print();
                    System.Console.WriteLine("well DONE!");
                    Environment.Exit(0);
                }
                

                // Skriv klart spelloopen här
                

            }

        }
    }
}
