using GuessTheNumber.DataBase;
using GuessTheNumber.PlayerStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber.GameEngine
{
    static class Engine
    {
        private static Random random = new Random();
        private static int attempts;

        public static void TryToGuess()
        {
            var minValue = 0;
            var maxValue = random.Next(10, 21);
            var expectedNumber = random.Next(minValue, maxValue + 1);
            attempts = (int)Math.Log(maxValue, 2);
            Console.WriteLine($"Try to guess a number from 0 to {maxValue}. You have {attempts} attempts");

            var history = new PlayerHistory();
            while (attempts > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter the estimated number");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(">>> ");

                if (int.TryParse(Console.ReadLine(), out int estimatedNumber))
                {
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (expectedNumber > estimatedNumber)
                    {
                        Console.WriteLine($"\tYour entered number LESS than expected. You have {attempts} attempts");
                        history.AddAction($"entered number LESS than expected");
                    }
                    else if (expectedNumber < estimatedNumber)
                    {
                        Console.WriteLine($"\tYour entered number BIGGER than expected. You have {attempts} attempts");
                        history.AddAction($"entered number Bigger than expected");
                    }
                    else
                    {
                        Console.WriteLine("\tCongratulations, you won!");
                        history.AddAction($"Won!!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("YOU'VE GOT TO ENTER A N_U_M_B_E_R");
                }
                if (attempts == 0)
                {
                    Console.WriteLine("You loose");
                    history.AddAction($"\tLoooooseee :=(  -- Expected number is {expectedNumber} --");
                }
            }
            Console.WriteLine($"Expected number is: {expectedNumber}");
            JsonDataBase.AddHistoryToCurrentAccount(history);
        }
    }
}
