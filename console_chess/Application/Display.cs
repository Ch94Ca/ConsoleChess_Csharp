using System;
using board;
using chess;

namespace application
{
    class Display
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();

            }

            Console.WriteLine("  a b c d e f g h");

        }

        public static ChessPosition ReadChessPosition()
        {
            string input = Console.ReadLine();
            char column = input[0];
            int line = int.Parse($"{input[1]}");

            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if(piece.Color == Color.white)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }

    }
}
