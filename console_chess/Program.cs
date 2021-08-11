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

                while(!game.EndGame)
                {
                    Console.Clear();
                    Display.PrintBoard(game.Board);

                    Console.Write("\n- Origin: ");
                    Position origin = Display.ReadChessPosition().ToPosition();

                    bool[,] validPositions = game.Board.Piece(origin).ValidMoves();

                    Console.Clear();
                    Display.PrintBoard(game.Board, validPositions);

                    Console.Write("\n- Destination: ");
                    Position destination = Display.ReadChessPosition().ToPosition();

                    game.MovePiece(origin, destination);

                }

            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Display.PrintBoard(game.Board);

        }
    }
}
