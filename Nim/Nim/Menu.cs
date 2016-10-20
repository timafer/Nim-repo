using Nim.CpuVsCpu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Nim.Players;

namespace Nim
{
    public class Menu
    {
        int repeat = 0;
        public static int[] board = new int[3] { 3, 5, 7 };
        LearnCPU learn = new LearnCPU(board,"Player 2");

        public Menu()
        {
        }
        public void Load()
        {
            if (YesNoQues("Do you want load up a saved learning cpu y / n"))
            {
                DeSeralize();
            }
            Select();
        }
        public void Select()
        {
            int i = ChoseInt("Please Select Game mode \n 1:P v P \n 2:P v Cpu \n 3:Cpu v Cpu \n 4:Cpu v Smart Cpu \n 5:P v Smart Cpu",5);
            if (i == 4)
            {
                MultiPlay();
            }
            else
            {
                UseSelection(i);
            }
        }
        public void MultiPlay()
        {
            bool goodinput = false;
            while (!goodinput)
            {
                Console.WriteLine("How many games do you want to play:");
                string s = Console.ReadLine();
                bool b = int.TryParse(s, out repeat);
                if (!b || repeat < 1)
                {
                    Console.Clear();
                    Console.WriteLine("ERROR:Invalid input. Please Enter A Number Above 0");
                }
                else
                {
                    goodinput = true;
                }
            }
            UseSelection(4);
        }
        public void UseSelection(int selection)
        {
            int sleepcounter = 0;
            switch (selection)
            {
                case 1:
                    Game g=new Game(new UserPlayer(board,"Player 1"), new UserPlayer(board, "Player 2"),board);
                    g.Start(sleepcounter);
                    reset();
                    break;
                case 2:
                    Game g1 = new Game(new UserPlayer(board, "Player 1"), new RandCpu(board, "Player 2"),board);
                    g1.Start(sleepcounter);
                    reset();
                    break;
                case 3:
                    Game g2 = new Game(new RandCpu(board, "Player 1"), new RandCpu(board, "Player 2"),board);
                    g2.Start(sleepcounter);
                    reset();
                    break;
                case 4:
                    Game g3 = new Game(new RandCpu(board, "Player 1"), learn,board);
                    for (int i = 0; i < repeat; i++)
                    {
                        g3.Start(sleepcounter);
                        reset();
                    }
                    showStats(g3.player1.wincount, learn.wincount);
                    break;
                case 5:
                    Game g4 = new Game(new UserPlayer(board, "Player 1"), learn,board);
                    g4.Start(sleepcounter);
                    reset();
                    break;
            }
            PostQuestions(selection);
        }


        public void PostQuestions(int selection)
        {
            if (YesNoQues("Do you want to play again y/n"))
            {
                if (selection == 4)
                {
                    MultiPlay();
                }
                else
                {
                    UseSelection(selection);
                }          
            }
            else {
                if (YesNoQues("Do you want to make a diffrent selection y/n"))
                {
                    Select();
                }
                else
                {
                    if (YesNoQues("Do you want to see the states y/n"))
                    {
                        states();
                    }
                    if (YesNoQues("Do you want to save the learning cpu y/n"))
                    {
                        Seralize();
                    }
                }
            }
        }
        public int ChoseInt(string prompt,int choises)
        {
            int i = 0;
            bool goodinput = false;
            while (!goodinput)
            {
                Console.WriteLine(prompt);
                string s = Console.ReadLine();
                bool b = int.TryParse(s, out i);
                if (!b || i < 1 || i > choises)
                {
                    Console.WriteLine("ERROR:Invalid input. Please Enter A Number Between 1 and "+choises);
                }
                else
                {
                    goodinput = true;
                }
            }
            return i;
        }
        public bool YesNoQues(string prompt)
        {
            bool valid = false;
            bool ans = false;
            while (!valid)
            {
                Console.WriteLine(prompt);
                string s = Console.ReadLine();
                if (char.ToLower(s[0]) == 'y')
                {
                    ans = true;
                    valid = true;
                }
                else if (char.ToLower(s[0]) == 'n')
                {
                    valid = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input");
                }
            }
            return ans;
        }
        private void showStats(int p1, int p2)
        {
            Console.WriteLine("Player 1 won " + p1 + " times.\nPlayer 2 won " + p2 + " times.");
            Console.WriteLine("Stats: \n\nPlayer 1 won " + Math.Truncate(((p1 / (double)(p1 + p2)) * 100)) +
                "% of the games.\nPlayer 2 won " + Math.Truncate(((p2 / (double)(p1 + p2)) * 100)) + "% of the games.");
        }
        public void states()
        {
            foreach (CpuVsCpu.State s in learn.learnedMoves.OrderBy(o => o.ValueOfWorth))
            {
                Console.WriteLine("" + s.StateOfBoard[0] + "," + s.StateOfBoard[1] + "," + s.StateOfBoard[2] + " Next Move:" + s.MoveMade[0] + "," + s.MoveMade[1] + " value:" + s.ValueOfWorth);
            }
        }
        public void Seralize()
        {
            string filepath= "C:\\Users\\Timafer\\Desktop\\learncpu.txt";
            StateStore s = new StateStore(learn.learnedMoves);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, s);
            stream.Close();
        }
        public void DeSeralize()
        {
            string fileselected = "C:\\Users\\Timafer\\Desktop\\learncpu.txt";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileselected, FileMode.Open, FileAccess.Read, FileShare.Read);
            StateStore b = (StateStore)formatter.Deserialize(stream);
            stream.Close();
            learn.learnedMoves = b.learnedMoves;
        }
        public StateStore DeSeralizetest()
        {
            string fileselected = "C:\\Users\\Timafer\\Desktop\\learncpu.txt";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileselected, FileMode.Open, FileAccess.Read, FileShare.Read);
            StateStore b = (StateStore)formatter.Deserialize(stream);
            stream.Close();
            return b;
        }
        public void reset()
        {
            board = new int[] { 3, 5, 7 };
        }

    }
}


