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
                    PrintPiece(board.Piece(i, j));
                }

                Console.WriteLine();

            }

            Console.WriteLine("  a b c d e f g h");

        }

        public static void PrintBoard(Board board, bool[,] validPositions)
        {
            ConsoleColor originalColor = Console.BackgroundColor;
            ConsoleColor validPositionColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++)
                {

                    if (validPositions[i,j])
                    {
                        Console.BackgroundColor = validPositionColor;
                    }
                    else
                    {
                        Console.BackgroundColor = originalColor;
                    }

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalColor;
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
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (piece.Color == Color.white)
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

                Console.Write(" ");

            }
        }

    }
}
