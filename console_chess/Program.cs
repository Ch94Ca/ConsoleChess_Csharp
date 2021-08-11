using System;
using board;
using application;
using chess;
using exceptions;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            try
            {
                board.AddPiece(new Rook(board, Color.black), new Position(0, 0));
                board.AddPiece(new Rook(board, Color.black), new Position(1, 3));
                board.AddPiece(new King(board, Color.black), new Position(2, 4));

                board.AddPiece(new Rook(board, Color.white), new Position(3, 5));
                board.AddPiece(new Rook(board, Color.white), new Position(1, 4));
                board.AddPiece(new King(board, Color.white), new Position(4, 4));

            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Display.PrintBoard(board);

        }
    }
}
