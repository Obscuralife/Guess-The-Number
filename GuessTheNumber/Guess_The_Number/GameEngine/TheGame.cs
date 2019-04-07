using Guess_The_Number.DataBase;
using Guess_The_Number.Game_Engine;
using Guess_The_Number.PlayerStuff;
using System;

namespace Guess_The_Number.GameEngine
{
    class TheGame
    {
        public JsonDataBase DataBase { get; private set; }
        public UsersValidator UserContext { get; private set; }
        public GameHandler Handler { get; private set; }

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
                        Start();
                        break;
                    }
                case ConsoleKey.N:
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
        }

        private void Again(Action action)
        {
            Console.WriteLine($"{action.Method.Name} again? (Y/N)" );
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
        }

        private void TryToLogIn()
        {
            ColorEngine.Green();
            Console.WriteLine("\nEnter the name");
            Console.Write(">> ");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the password");
            Console.Write(">> ");
            var password = Console.ReadLine();

            var oldUser = new UserAccount(name, password);
            UserContext = new UsersValidator(oldUser);

            if (!UserContext.IsValid())
            {
                Console.WriteLine("Try Again to Log In");
                Again(TryToLogIn);
            }
            else if (!DataBase.IsDbContains(oldUser))
            {
                ColorEngine.Red();
                Console.WriteLine("The username or password is incorrect. Press any button to start over");
                Again(TryToLogIn);
            }

            ColorEngine.Yellow();
            Console.WriteLine($"Welcome {name}");
            Console.WriteLine("\nLet's start to play");
            ColorEngine.White();
        }

        private void TryToLogUp()
        {
            ColorEngine.Green();

            Console.WriteLine("\nEnter the name");
            Console.Write(">> ");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the password");
            Console.Write(">> ");
            var password = Console.ReadLine();

            var newUser = new UserAccount(name, password);
            UserContext = new UsersValidator(newUser);

            if (!UserContext.IsValid())
            {
                Console.WriteLine("Try Again to Create account");
                Again(TryToLogUp);
            }

            bool isSuccessfully = DataBase.CreateNewUserAccount(newUser);
            if (!isSuccessfully)
            {
                ColorEngine.Red();
                Console.WriteLine("This account already exists. Press any button to start over");
                Console.ReadKey();
                Again(TryToLogUp);
            }
            ColorEngine.Yellow();
            Console.WriteLine($"Welcome {name}");
            Console.WriteLine("\nLet's start to play");
            ColorEngine.White();
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

