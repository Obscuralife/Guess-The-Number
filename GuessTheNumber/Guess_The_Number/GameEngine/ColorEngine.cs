using System;

namespace Guess_The_Number.GameEngine
{
    class ColorEngine
    {
        public static void Green() => Console.ForegroundColor = ConsoleColor.Green;
        public static void Yellow() => Console.ForegroundColor = ConsoleColor.Yellow;
        public static void White() => Console.ForegroundColor = ConsoleColor.White;
        public static void Red() => Console.ForegroundColor = ConsoleColor.Red;
    }
}
