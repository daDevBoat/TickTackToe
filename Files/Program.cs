using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;

namespace tickTackToe
{
    public static class Storage
    {
        public static bool done = false;
        public static int roundNum = 1;
        public static int[] lastMove = { 3, 3 };
        public static string[][] board =
            {
                new string[] {"-", "-", "-"},
                new string[] {"-", "-", "-"},
                new string[] {"-", "-", "-"}
            };

    }

    class Program
    {
        static string CheckWin()
        {

            foreach (string[] arg in Storage.board)
            {
                if (arg[0] == arg[1] && arg[0] == arg[2] && arg[0] != "-")
                {
                    return arg[0];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (Storage.board[0][i] == Storage.board[1][i] && Storage.board[1][i] == Storage.board[2][i] && Storage.board[0][i] != "-")
                {
                    return Storage.board[0][i];
                }
            }
            if ((Storage.board[0][0] == Storage.board[1][1] && Storage.board[0][0] == Storage.board[2][2]) && Storage.board[1][1] != "-" || (Storage.board[0][2] == Storage.board[1][1] && Storage.board[0][2] == Storage.board[2][0]) && Storage.board[1][1] != "-")
            {
                return Storage.board[1][1];
            }
            else
            {
                return null;
            }


        }

        static void spillerPlassering()
        {
            Console.WriteLine("Hvor vil du plassere brikken?");
            string input = Console.ReadLine();
            string[] splitInput = input.Split(",", 2);
            int pos1 = int.Parse(splitInput[1]);
            int pos2 = int.Parse(splitInput[0]);

            if (Storage.board[pos1][pos2] == "-")
            {
                Storage.board[pos1][pos2] = "X";
                Storage.lastMove[0] = pos1;
                Storage.lastMove[1] = pos2;
            }
            else
            {
                Console.WriteLine("Denne plassen er opptatt, velg en annen rute");
                spillerPlassering();
            }
        }

        static int[] CheckAlmostWin(string[][] board ,string playerType, int numOfW)
        {
            int winPoss = 0;
            for (int y = 0; y < board.Length; y++)
            {
                string[] row = board[y];
                int count = 0;
                int[] pos = { 3, 3 };
                bool hypCheck = false;
                if (!row.Contains("-"))
                {
                    continue;
                }
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == playerType)
                    {
                        count++;
                    }
                    else if (row[i] == "-")
                    {
                        hypCheck = true;
                        pos[0] = i;
                        pos[1] = y;
                    }
                    if (count == 2 && i == 2 && hypCheck)
                    {
                        if (numOfW == 1)
                        { 
                            return pos; 
                        }
                        else
                        {
                            winPoss++;
                        }
                    }

                }
            }

            for (int x = 0; x < 3; x++)
            {
                int count = 0;
                int[] pos = { 3, 3 };
                bool hypCheck = false;
                for (int y = 0; y < 3; y++)
                {
                    if (board[y][x] == playerType)
                    {
                        count++;
                    }
                    else if (board[y][x] == "-")
                    {
                        hypCheck = true;
                        pos[0] = x;
                        pos[1] = y;

                    }
                    if (count == 2 && y == 2 && hypCheck)
                    {
                        if (numOfW == 1)
                        {
                            return pos;
                        }
                        else
                        {
                            winPoss++;
                        }

                    }


                }
            }
            int count2 = 0;
            int[] pos3 = { 3, 3 };
            bool hypCheck3 = false;
            for (int i = 0; i < 3; i++)
            {

                if (board[i][i] == playerType)
                {
                    count2++;
                }
                else if (board[i][i] == "-")
                {
                    hypCheck3 = true;
                    pos3[0] = i;
                    pos3[1] = i;

                }
                if (count2 == 2 && i == 2 && hypCheck3)
                {
                    if (numOfW == 1)
                    {
                        return pos3;
                    }
                    else
                    {
                        winPoss++;
                    }

                }
            }
            int count3 = 0;
            int[] pos2 = { 3, 3 };
            bool hypCheck2 = false;

            for (int i = 0; i < 3; i++)
            {

                if (board[i][2 - i] == playerType)
                {
                    count3++;
                }
                else if (board[i][2 - i] == "-")
                {
                    pos2[0] = 2 - i;
                    pos2[1] = i;
                    hypCheck2 = true;

                }
                if (count3 == 2 && i == 2 && hypCheck2)
                {
                    if (numOfW == 1)
                    {
                        return pos2;
                    }
                    else
                    {
                        winPoss++;
                    }
                }

            }

            if (numOfW != 1)
            {
                return new int[] {winPoss};
            }

            int[] test = { 3, 3 };
            return test;

        }

        static void Showboard()
        {
            Console.WriteLine("    " + "   " + 0 + "   " + 1 + "   " + 2 + "  ");
            Console.WriteLine("     -------------");
            int i = 0;
            foreach (string[] rows in Storage.board)
            {

                Console.WriteLine("   " + i + " | " + rows[0] + " | " + rows[1] + " | " + rows[2] + " |");
                Console.WriteLine("     -------------");
                i++;
            }
        }

        static void FakeAI()
        {
            /*
            Console.WriteLine("\nTEST");
            Showboard();
            Console.WriteLine("TESTDONE\n");
            */

            int[][] cornersPos =
                   {

                        new int[] {0, 0},
                        new int[] {0, 2},
                        new int[] {2, 0},
                        new int[] {2, 2}

                    };

            if (Storage.roundNum == 1) //egne trekk første trekk, fordi det er få muligheter og vi har funnet ut hva som er best.
            {

                if (Storage.board[1][1] == "X") //spiller plasser i midten
                {
                    Random rdm = new Random();
                    int corner = rdm.Next(0, 4);
                    Storage.board[cornersPos[corner][1]][cornersPos[corner][0]] = "O";
                }
                else if (Storage.board[1][0] == "X" || Storage.board[0][1] == "X" || Storage.board[2][1] == "X" || Storage.board[1][2] == "X")   // Hvis spiller setter brikke på siden
                {
                    if (Storage.lastMove[0] == 1)   // lasMove[0] = y
                    {
                        Storage.board[1][2 - Storage.lastMove[1]] = "O";
                    }
                    else
                    {
                        Storage.board[2 - Storage.lastMove[0]][1] = "O";
                    }
                }
                else //spiller plasser i et hjørne
                {
                    Storage.board[1][1] = "O"; //vi plasserer i midten
                }
            }
            else //alle andre runder enn første runde.
                 //vi må se om vi selv kan vinne for vi vil da plassere der, hvis ikke: hvis spilleren holder på å vinne, vil vi plassere der for å blokke. tilslutt har vi strategi for hvis vi ser det er måter spilleren utføre en gaffel manøver som vi må blokke for et trekk før.
            {
                int[] attack = CheckAlmostWin(Storage.board,"O", 1); //først hvis vi selv holder på å vinne og gir tilbake koordinater.

                if (!(attack[0] == attack[1] && attack[0] == 3)) //må sjekke om det ikke er 3,3. dette er standard verdien, som egentlig betyr vi ikke kan vinne
                {
                    Storage.board[attack[1]][attack[0]] = "O"; //plassere for å vinne
                    Showboard();

                }
                else //3,3 altså vi kan ikke vinne
                {
                    int[] defence = CheckAlmostWin(Storage.board,"X", 1); //sjekker så for om motstanderen kan vinne
                    if (!(defence[0] == defence[1] && defence[0] == 3)) //ser igjen om denne er 3,3 som ville betyd at standard verdien sendes tilbake, altså motstanndere kan vinne.
                    {
                        Storage.board[defence[1]][defence[0]] = "O"; //plasser for å hindre at motstanderen vinner.
                    }
                    else //ingen kan vinne i neste trekk.
                    {
                        if ((Storage.board[0][0] == "X" && Storage.board[2][2] == "X") || (Storage.board[0][2] == "X" && Storage.board[2][0] == "X")) //vi ser om motstanderen har plassesert i to motsatte hjørner.  //stor L
                        {
                            int[][] sidePos =
                            {
                                    new int[] {0, 1},
                                    new int[] {1, 0},
                                    new int[] {1, 2},
                                    new int[] {2, 1}

                                };

                            Random rdm = new Random();
                            int corner = rdm.Next(0, 4);

                            //Storage.board[sidePos[corner][1]][sidePos[corner][0]] = "O";
                            if (sidePos[corner][1] == 1)
                            {
                                if (Storage.board[sidePos[corner][1]][sidePos[corner][0]] == "-" && Storage.board[sidePos[corner][1]][2 - sidePos[corner][0]] == "-" && Storage.board[1][1] == "X")
                                {
                                    Storage.board[sidePos[corner][1]][sidePos[corner][0]] = "O"; //velge et tilfeldig side, sånn at vi vi får to på rad og motstander må blokke.
                                    return;
                                }
                                else
                                {
                                    if (Storage.board[0][1] == "-" && Storage.board[2][1] == "-")
                                    {
                                        Storage.board[rdm.Next(0, 1) * 2][1] = "O";
                                        return;
                                    }


                                }

                            }
                            else if (sidePos[corner][0] == 1)
                            {
                                if (Storage.board[sidePos[corner][1]][sidePos[corner][0]] == "-" && Storage.board[2 - sidePos[corner][1]][sidePos[corner][0]] == "-" && Storage.board[1][1] == "X")
                                {
                                    Storage.board[sidePos[corner][1]][sidePos[corner][0]] = "O"; //velge et tilfeldig side, sånn at vi vi får to på rad og motstander må blokke.
                                    return;
                                }
                                else
                                {
                                    if (Storage.board[1][2] == "-" && Storage.board[1][0] == "-")
                                    {
                                        Storage.board[1][rdm.Next(0, 1) * 2] = "O";
                                        return;
                                    }


                                }
                            }

                            //resten vil løses automatisk. enten uagjort, eller hvis motstanderen gjør noe dumt.
                        }
                        for (int i = 0; i < cornersPos.Length; i++) // Sjekker for l
                        {

                            if (!(Storage.board[cornersPos[i][1]][cornersPos[i][0]] == "-"))
                            {
                                //Console.WriteLine("break");
                                continue;
                            }
                            if (Storage.board[1][cornersPos[i][0]] == "X" && Storage.board[cornersPos[i][1]][1] == "X" && Storage.board[2 - cornersPos[i][1]][cornersPos[i][0]] == "-" && Storage.board[cornersPos[i][1]][2 - cornersPos[i][0]] == "-")
                            {
                                Storage.board[cornersPos[i][1]][cornersPos[i][0]] = "O";
                                return;
                            }
                        }
                        //Hest her
                        for (int y = 0; y < Storage.board.Length; y++)
                        {
                            for (int x = 0; x < Storage.board.Length; x++)
                            {

                                if (Storage.board[y][x] == "-")
                                {
                                    string[][] tempBoard = 
                                    {
                                        new string[] {"-", "-", "-"},
                                        new string[] {"-", "-", "-"},
                                        new string[] {"-", "-", "-"}
                                    };


                                    for (int j = 0; j < Storage.board.Length; j++)
                                    {
                                        for (int h = 0; h < Storage.board.Length; h++)
                                        {
                                            tempBoard[j][h] = Storage.board[j][h];
                                        }
                                    }
                                    //string[][] tempBoard = Storage.board.Clone() as string[][];
                                    
                                    tempBoard[y][x] = "X";



                                    if (CheckAlmostWin(tempBoard, "X", 3)[0] > 1) 
                                    {
                                        Storage.board[y][x] = "O";
                                        return;
                                    }
                                }
                            }
                        }

                        //Strategisk flytt for å vinne
                        int[] bestPos = { 3, 3 };
                        int highestPoss = -1;
                        for (int y = 0; y < Storage.board.Length; y++)
                        {
                            for (int x = 0; x < Storage.board.Length; x++)
                            {

                                if (Storage.board[y][x] == "-")
                                {
                                    string[][] tempBoard =
                                    {
                                        new string[] {"-", "-", "-"},
                                        new string[] {"-", "-", "-"},
                                        new string[] {"-", "-", "-"}
                                    };


                                    for (int j = 0; j < Storage.board.Length; j++)
                                    {
                                        for (int h = 0; h < Storage.board.Length; h++)
                                        {
                                            tempBoard[j][h] = Storage.board[j][h];
                                        }
                                    }

                                    tempBoard[y][x] = "O";



                                    if (CheckAlmostWin(tempBoard, "O", 3)[0] > highestPoss)
                                    {
                                        bestPos[0] = y;
                                        bestPos[1] = x;
                                        highestPoss = CheckAlmostWin(tempBoard, "O", 3)[0];
                                    }
                                }
                            }
                        }

                        if(bestPos[0] == 3)
                        {
                            Showboard();
                            Console.WriteLine("Du spilte uavgjort mot monsteret, seier er umulig mohahahahahaha!");
                            Storage.done = true;
                        }
                        else
                        {
                            Storage.board[bestPos[0]][bestPos[1]] = "O";
                        }

                    }
                }
            }
            /*
            1 Sjekke om den kan vinne
            2 Forsvare motstander akkurat nå
            3 Strategiw

            Strategi:

            - Starter i midten
            Da velger vi et tilfeldig hjørne
            Hvis spiller plasserer på siden utløses 2 || Hvis spiller plassere i hjørnet som ikke er motsatt av vårt hjørne, plasser der    


            - Starter i hjørnet
            Må plassrere i midten og så side || side rett ved siden av X-en og så videre...

            - Starter på siden
            Setter på den motsatte siden
            Pass på hest-movement

            Generell algorithme:
            - Sjekke for L, l, T og hest

            */
        }

        static void Main()
        {
            Console.WriteLine("Ta utgangspunkt i at venstre hjørne er 0, 0. Skriv koordinatene på formen \"x, y\"");
            while (!Storage.done)
            {

                Showboard();
                spillerPlassering();

                string winner = CheckWin();


                if (winner == "X") // Dette er en undøvendig sjekk, spilleren kan aldri vinne hahahahahaha
                {
                    Console.WriteLine("Gratulerer spiller, du vant over monsteret!");
                    Storage.done = true;
                }
                else
                {
                    FakeAI();

                    winner = CheckWin();

                    if (winner == "O")
                    {
                        Console.WriteLine("Du tapte dessverre mot monsteret. Ditt nederlag var uungåelig!");
                        Storage.done = true;
                    }
                    
                }
                Storage.roundNum++;

                /*
                 
                 
                 
                 
                 
                 det er viktig at taktikken vår er basert på at ting skjer i andre trekk. 
                 
                 endringer;
                    stor L
                    lagt til kommentarer

                 
                 

                CODE GRAVEYARD:
                int[][] horsePosChecksRight =
                        {
                            new int[] {1, 2, 3}, //3 da skal den oppover  2 felt
                            new int[] {0, 2, 3},
                            new int[] {0, 1, 4}, // 4 da skal den bortover 2 felt
                            new int[] {0, 0, 4}
                        };
                        int[][] horsePosChecksLeft =
                        {
                            new int[] {1, 2, 3}, //3 da skal den oppover 2 felt
                            new int[] {2, 2, 3},
                            new int[] {2, 1, 4}, //4 da skal den bortover 3 felt
                            new int[] {2, 0, 4}
                        };

                        foreach (int[] ele in horsePosChecksLeft)
                        {
                            if (ele[2] == 3)
                            {
                                if (Storage.board[ele[1]][ele[0]] == "X" && Storage.board[0][ele[0] + 1] == "X"  )
                                {
                                    int leftRow = ele[0] - 1;
                                    if (leftRow == -1)
                                    {
                                        leftRow = 2;
                                    }
                                    if (Storage.board[1][ele[0]] == "-" && Storage.board[0][ele[0]] == "-" ) { }
                                }
                            }
                            else
                            {

                            }

                        }



                //Random move
                        List<int[]> availableSpaces = new();
                        for (int y = 0; y < Storage.board.Length; y++)
                        {
                            for (int x = 0; x < Storage.board[y].Length; x++)
                            {
                                if (Storage.board[y][x] == "-")
                                {
                                    availableSpaces.Add(new int[] { x, y });
                                }
                            }
                        }
                        if (availableSpaces.Count == 0)
                        {
                            Showboard();
                            Console.WriteLine("\nBra spilt, men det ble uavgjort. Bedre lykke neste gang!");
                            Storage.done = true;
                            return;
                     
                        }
                        Random rdmPlace = new Random();
                        int rdmNum = rdmPlace.Next(availableSpaces.Count);
                        Storage.board[availableSpaces[rdmNum][1]][availableSpaces[rdmNum][0]] = "O";
                 
                 
                 */

            }


        }
    }
}





/*
Må gjøres:
- Spiller kan ikke plassere der det allerede er brikker 
- Algoritmen
-
-

farlige trekk:
- stor L
- liten L
- hest
 */
