﻿using System;
using board;
using application;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Display.PrintBoard(board);

        }
    }
}
