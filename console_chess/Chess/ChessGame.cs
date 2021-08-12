using System;
using System.Collections.Generic;
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
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.white;
            EndGame = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PutPieces();
        }

        public Piece MovePiece(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncMoves();
            Piece captured = Board.RemovePiece(destination);
            Board.AddPiece(piece, destination);

            if (captured != null)
            {
                Captured.Add(captured);
            }

            return captured;

        }

        public void UndoMove(Position origin, Position destination, Piece captured)
        {
            Piece piece = Board.RemovePiece(destination);
            piece.DecMoves();

            if (captured != null)
            {
                Board.AddPiece(captured, destination);
                Captured.Remove(captured);
            }

            Board.AddPiece(piece, origin);

        }

        public void DoMove(Position origin, Position destination)
        {
            Piece captured = MovePiece(origin, destination);

            if (IsCheck(ActualPlayer))
            {
                UndoMove(origin, destination, captured);
                throw new BoardException("Check!");
            }

            if (IsCheck(Oposite(ActualPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if(IsCheck(Oposite(ActualPlayer)))
            {
                EndGame = true;
            }

            Turn++;
            ChangePlayer();

        }

        public void CheckOriginPosition(Position origin)
        {
            if (Board.Piece(origin) == null)
            {
                throw new BoardException("The is no piece in this position.");
            }

            if (Board.Piece(origin).Color != ActualPlayer)
            {
                if (ActualPlayer == Color.white)
                {
                    throw new BoardException("White's Turn!");
                }
                else
                {
                    throw new BoardException("Black's Turn!");
                }
            }

            if (!Board.Piece(origin).IsValidMoves())
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
            if (ActualPlayer == Color.white)
            {
                ActualPlayer = Color.black;
            }
            else
            {
                ActualPlayer = Color.white;
            }
        }

        public HashSet<Piece> GetCapturedPieces(Color color)
        {
            HashSet<Piece> output = new HashSet<Piece>();

            foreach (Piece piece in Captured)
            {
                if (piece.Color == color)
                {
                    output.Add(piece);
                }
            }

            return output;

        }

        public HashSet<Piece> GetPiecesInGame(Color color)
        {
            HashSet<Piece> output = new HashSet<Piece>();

            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    output.Add(piece);
                }
            }

            output.ExceptWith(GetCapturedPieces(color));

            return output;
        }
        private Color Oposite(Color color)
        {
            if (color == Color.white)
            {
                return Color.black;
            }
            else
            {
                return Color.white;
            }
        }

        private Piece GetKing(Color color)
        {
            Piece king = null;

            foreach (Piece piece in GetPiecesInGame(color))
            {
                if (piece is King)
                {
                    king = piece;
                    break;
                }
            }

            return king;

        }

        public bool IsCheck(Color color)
        {
            Piece king = GetKing(color);

            foreach (Piece piece in GetPiecesInGame(Oposite(color)))
            {
                bool[,] moves = piece.ValidMoves();
                if (moves[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }

            return false;

        }

        public bool IsMate(Color color)
        {
            if (!IsCheck(color))
            {
                return false;
            }

            foreach(Piece piece in GetPiecesInGame(color))
            {
                bool[,] moves = piece.ValidMoves();

                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if(moves[i,j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece captured = MovePiece(origin, destination);
                            bool isMate = IsCheck(color);
                            UndoMove(origin, destination, captured);
                            if(!IsCheck(color))
                            {
                                return false;
                            }
                        }
                    }
                }

            }

            return true;

        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.AddPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('c', 1, new Rook(Board, Color.white));
            PutNewPiece('c', 2, new Rook(Board, Color.white));
            PutNewPiece('d', 2, new Rook(Board, Color.white));
            PutNewPiece('e', 2, new Rook(Board, Color.white));
            PutNewPiece('e', 1, new Rook(Board, Color.white));
            PutNewPiece('d', 1, new King(Board, Color.white));

            PutNewPiece('c', 7, new Rook(Board, Color.black));
            PutNewPiece('c', 8, new Rook(Board, Color.black));
            PutNewPiece('d', 7, new Rook(Board, Color.black));
            PutNewPiece('e', 7, new Rook(Board, Color.black));
            PutNewPiece('e', 8, new Rook(Board, Color.black));
            PutNewPiece('d', 8, new King(Board, Color.black));

        }

    }
}
