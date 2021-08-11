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
            ChessGame game = null;

            try
            {
                game = new ChessGame();

                while (!game.EndGame)
                {
                    try
                    {
                        Console.Clear();
                        Display.PrintBoard(game.Board);
                        Console.WriteLine($"\n- Turn: {game.Turn} ({game.ActualPlayer})");

                        Console.Write("\n- Origin: ");
                        Position origin = Display.ReadChessPosition().ToPosition();
                        game.CheckOriginPosition(origin);

                        bool[,] validPositions = game.Board.Piece(origin).ValidMoves();

                        Console.Clear();
                        Display.PrintBoard(game.Board, validPositions, origin);
                        Console.WriteLine($"\n- Turn: {game.Turn} ({game.ActualPlayer})");

                        Console.WriteLine($"\n- Origin: {origin.ToChessPosition()} ({game.Board.Piece(origin)})");
                        Console.Write("- Destination: ");
                        Position destination = Display.ReadChessPosition().ToPosition();
                        game.CheckDestinationPosition(origin, destination);

                        game.DoMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine($"\n{e.Message}");
                        Console.ReadLine();
                    }

                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Display.PrintBoard(game.Board);

        }
    }
}
