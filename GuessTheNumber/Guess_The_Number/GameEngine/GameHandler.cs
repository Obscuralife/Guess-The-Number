using Guess_The_Number.DataBase;
using Guess_The_Number.GameEngine;
using Guess_The_Number.PlayerStuff;
using System;

namespace Guess_The_Number.Game_Engine
{
    class GameHandler
    {
        public Random Random { get; private set; }
        public int Attempts { get; private set; }
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }
        public int ExpectedNumber { get; private set; }
        public bool IsWon { get; private set; }

        public GameHandler()
        {
            this.Random = new Random();
            this.MinValue = 0;
            this.MaxValue = Random.Next(10, 21);
            this.Attempts = (int)Math.Log(MaxValue, 2);
            this.ExpectedNumber = Random.Next(MinValue, MaxValue + 1);
            IsWon = false;
        }

        public void TryToGuess()
        {
            ColorEngine.White();
            Console.WriteLine($"Try to guess a number from 0 to {MaxValue}. You have {Attempts} attempts");

            var history = new PlayerHistory();
            while (Attempts > 0 && !IsWon)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter the estimated number");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(">>> ");

                if (int.TryParse(Console.ReadLine(), out int estimatedNumber))
                {
                    if (estimatedNumber > MaxValue || estimatedNumber < MinValue)
                    {
                        ColorEngine.Red();
                        Console.WriteLine("Out of range");
                        continue;
                    }
                    Attempts--;
                    ColorEngine.White();

                    switch (estimatedNumber.CompareTo(ExpectedNumber))
                    {
                        case (int)Equality.Bigger:
                            {
                                history.AddAction($"Entered number Bigger({estimatedNumber}) than expected({ExpectedNumber})");
                                Console.WriteLine($"\tYour entered number BIGGER than expected. You have {Attempts} attempts");
                                break;
                            }
                        case (int)Equality.Less:
                            {
                                history.AddAction($"Entered number({estimatedNumber}) LESS than expected({ExpectedNumber})");
                                Console.WriteLine($"\tYour entered number LESS than expected. You have {Attempts} attempts");
                                break;
                            }
                        case (int)Equality.Equals:
                            {
                                IsWon = true;
                                history.AddAction($"You Won!!");
                                Console.WriteLine("\tCongratulations, you won!");
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    ColorEngine.Red();
                    Console.WriteLine("YOU'VE GOT TO ENTER A N_U_M_B_E_R");
                }
            }
            if (!IsWon)
            {
                Console.WriteLine($"Expected number is: {ExpectedNumber}. You loose");
                history.AddAction($"Loooooseee :=(");
            }
            Console.WriteLine();

            TheGame.DataBase.AddHistoryToCurrentAccount(history);
        }
    }

    public enum Equality
    {
        Less = -1,
        Equals = 0,
        Bigger = 1
    }
}
