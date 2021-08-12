using System;
using System.Collections.Generic ;
using board;
using chess;

namespace application
{
    class Display
    {
        public static void PrintGame(ChessGame game)
        {
            PrintBoard(game.Board);
            Console.WriteLine();
            PrintCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine($"\n- Turn: {game.Turn} ({game.ActualPlayer})");

            if(game.Check)
            {   
                Console.WriteLine("\nCheck!");
            }

        }

        public static void PrintCapturedPieces(ChessGame game)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White's: ");
            PrintSet(game.GetCapturedPieces(Color.white));
            Console.WriteLine();
            Console.Write("Black's: ");
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(game.GetCapturedPieces(Color.black));
            Console.ForegroundColor = originalColor;
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[ ");
            foreach (Piece piece in set)
            {
                Console.Write($"{piece} ");
            }
            Console.Write("]");
        }

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

        public static void PrintBoard(Board board, bool[,] validPositions, Position origin)
        {
            ConsoleColor originaBGlColor = Console.BackgroundColor;
            ConsoleColor originaFGlColor = Console.ForegroundColor;
            ConsoleColor validPositionColor = ConsoleColor.DarkGray;
            ConsoleColor movedPieceBGColor = ConsoleColor.Gray;
            ConsoleColor movedPieceFGColor = ConsoleColor.Black;

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
                        Console.BackgroundColor = originaBGlColor;
                    }

                    if(origin.Line == i && origin.Column == j)
                    {
                        Console.BackgroundColor = movedPieceBGColor;
                        Console.ForegroundColor = movedPieceFGColor;
                    }

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originaBGlColor;
                    Console.ForegroundColor = originaFGlColor;
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
