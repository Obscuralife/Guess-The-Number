using Guess_The_Number.DataBase;
using Guess_The_Number.Game_Engine;
using Guess_The_Number.PlayerStuff;
using System;

namespace Guess_The_Number.GameEngine
{
    class TheGame
    {
        public static JsonDataBase DataBase { get; private set; }
        public UsersValidator Validator { get; private set; }
        public GameHandler Handler { get; private set; }
        public UserAccount CurrentUserAccount { get; private set; }

        public TheGame()
        {
            DataBase = JsonDataBase.GetDataBase();
            Handler = new GameHandler();
        }
        public void Start()
        {
            Console.Clear();
            Greetings();

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.U:
                    {
                        TryToLogUp();
                        Handler.TryToGuess();
                        break;
                    }
                case ConsoleKey.I:
                    {
                        TryToLogIn();
                        Handler.TryToGuess();
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Start();
                        break;
                    }
            }

            ColorEngine.Yellow();
            Console.WriteLine("Again?(Y/N)");
            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Y:
                    {
                        Console.Clear();
                        new TheGame().Start();
                        break;
                    }
                case ConsoleKey.N:
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
        }

        private bool Again(Action action)
        {
            var actionName = (action.Method.Name == "TryToLogIn") ? "Log in" : "Create account";
            Console.WriteLine($"{actionName} again? (Y/N)");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    {
                        action.Invoke();
                        break;
                    }
                case ConsoleKey.N:
                    {
                        Start();
                        break;
                    }
                default: Again(action); break;
            }
            return true;
        }

        private UserAccount GetAccountWithData()
        {
            ColorEngine.Green();
            Console.WriteLine("Enter the name");
            Console.Write(">> ");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the password");
            Console.Write(">> ");
            var password = Console.ReadLine();

            return new UserAccount(name, password);
        }

        private void TryToLogIn()
        {
            ColorEngine.Yellow();
            Console.WriteLine();
            Console.WriteLine(new string('*', 10) + "Login:" + new string('*', 10));

            CurrentUserAccount = GetAccountWithData();
            Validator = new UsersValidator(CurrentUserAccount);

            if (Validator.IsValid())
            {
                if (!DataBase.IsDbContains(CurrentUserAccount))
                {
                    ColorEngine.Red();
                    Console.WriteLine("This account already exists.");
                    Again(TryToLogIn);
                }
                else
                {
                    ColorEngine.Yellow();
                    Console.Write($"Welcome {CurrentUserAccount.Name}...");
                    Console.WriteLine("\tLet's start to play");
                    Console.WriteLine();
                }
            }
            else
            {
                Again(TryToLogIn);
            }
        }

        private void TryToLogUp()
        {
            ColorEngine.Yellow();
            Console.WriteLine();
            Console.WriteLine(new string('*', 10) + "Create account:" + new string('*', 10));

            CurrentUserAccount = GetAccountWithData();
            Validator = new UsersValidator(CurrentUserAccount);

            if (Validator.IsValid())
            {
                bool ifCreationIsSuccessfully = DataBase.CreateNewUserAccount(CurrentUserAccount);
                if (ifCreationIsSuccessfully)
                {
                    ColorEngine.Yellow();
                    Console.Write($"Welcome {CurrentUserAccount.Name}...");
                    Console.WriteLine("\tLet's start to play");
                    Console.WriteLine();
                }
                else
                {
                    ColorEngine.Red();
                    Console.WriteLine("This account already exists.");
                    Again(TryToLogUp);
                }
            }
            else
            {
                Again(TryToLogUp);
            }
        }

        private void Greetings()
        {
            ColorEngine.Yellow();
            Console.WriteLine("Greetings you in the game 'Guess the number'");
            Console.WriteLine("Press 'U' to Create new account");
            Console.WriteLine("Press 'I' to Log in");
        }
    }
}

