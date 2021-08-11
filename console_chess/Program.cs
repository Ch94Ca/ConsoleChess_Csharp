using System;
using board;
using application;
using chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.AddPiece(new Rook(board, Color.black), new Position(0, 0));
            board.AddPiece(new Rook(board, Color.black), new Position(1, 3));
            board.AddPiece(new King(board, Color.black), new Position(2, 4));

            Display.PrintBoard(board);

        }
    }
}
