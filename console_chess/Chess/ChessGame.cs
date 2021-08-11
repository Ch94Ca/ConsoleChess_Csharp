using System;
using board;
using exceptions;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public bool EndGame { get; private set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.white;
            EndGame = false;
            PutPieces();
        }

        public void MovePiece(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncMoves();
            Piece captured = Board.RemovePiece(destination);
            Board.AddPiece(piece, destination);

        }

        public void DoMove(Position origin, Position destination)
        {
            MovePiece(origin, destination);
            Turn++;
            ChangePlayer();

        }

        public void CheckOriginPosition(Position origin)
        {
            if(Board.Piece(origin) == null)
            {
                throw new BoardException("The is no piece in this position.");
            }

            if(Board.Piece(origin).Color != ActualPlayer)
            {
                if(ActualPlayer == Color.white)
                {
                    throw new BoardException("White's Turn!");
                }
                else
                {
                    throw new BoardException("Black's Turn!");
                }
            }

            if(!Board.Piece(origin).IsValidMoves())
            {
                throw new BoardException("There is no valid moves for this piece.");
            }

        }
        public void CheckDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveto(destination))
            {
                throw new BoardException("Invalid destination.");
            }
        }

        private void ChangePlayer()
        {
            if(ActualPlayer == Color.white)
            {
                ActualPlayer = Color.black;
            }
            else
            {
                ActualPlayer = Color.white;
            }
        }

        private void PutPieces()
        {
            Board.AddPiece(new Rook(Board, Color.white), new ChessPosition('c', 1).ToPosition());
            Board.AddPiece(new Rook(Board, Color.white), new ChessPosition('c', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.white), new ChessPosition('d', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.white), new ChessPosition('e', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.white), new ChessPosition('e', 1).ToPosition());
            Board.AddPiece(new King(Board, Color.white), new ChessPosition('d', 1).ToPosition());

            Board.AddPiece(new Rook(Board, Color.black), new ChessPosition('c', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.black), new ChessPosition('c', 8).ToPosition());
            Board.AddPiece(new Rook(Board, Color.black), new ChessPosition('d', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.black), new ChessPosition('e', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.black), new ChessPosition('e', 8).ToPosition());
            Board.AddPiece(new King(Board, Color.black), new ChessPosition('d', 8).ToPosition());
        }

    }
}
