﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    class Game
    {
        private static void Green() => Console.ForegroundColor = ConsoleColor.Green;
        private static void Yellow() => Console.ForegroundColor = ConsoleColor.Yellow;
        private static void White() => Console.ForegroundColor = ConsoleColor.White;
        private static void Red() => Console.ForegroundColor = ConsoleColor.Red;

        public static void Play()
        {
            Greetings();
            ReadyToPlay();
        }

        private static void Greetings()
        {
            Yellow();
            Console.WriteLine("Greetings you in the game 'Guess the number'");
            Console.WriteLine("Press 'C' to create a new account");
            Console.WriteLine("Press 'S' to sign up");
        }

        private static void ReadyToPlay()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.C:
                    {
                        Green();

                        Console.WriteLine("\nEnter the name");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter the password");
                        var password = Console.ReadLine();

                        bool isSuccessfully = Authorization.SignUp(name, password);
                        if (!isSuccessfully)
                        {
                            Red();
                            Console.WriteLine("This account already exists. Press any botton to start over");
                            Console.ReadKey();
                            Console.Clear();
                            Play();
                        }
                        Yellow();
                        Console.WriteLine($"Welcome {name}");
                        break;
                    }
                case ConsoleKey.S:
                    {
                        Green();

                        Console.WriteLine("\nEnter the name");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter the password");
                        var password = Console.ReadLine();

                        bool isSuccessfully = Authorization.LogIn(name, password);
                        if (!isSuccessfully)
                        {
                            Red();
                            Console.WriteLine("The username or password is incorrect. Press any botton to start over");
                            Console.ReadKey();
                            Console.Clear();
                            Play();
                        }
                        Yellow();
                        Console.WriteLine($"Welcome {name}");
                        break;
                    }
                default:
                    break;
            }
            Console.WriteLine("Again?(Y/N)");
            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Y:
                    {
                        ReadyToPlay();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}