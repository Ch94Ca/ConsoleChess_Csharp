using System;
using board;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p = new Position(2, 3);
            Console.WriteLine(p);

            Board board = new Board(8, 8);

        }
    }
}
