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
        public Piece EnPassantVulnerable { get; private set; }
        public bool Check { get; set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        
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

            // # Special: Castling
            
            // Small Castling
            if (piece is King && destination.Column == (origin.Column + 2))
            {
                Position towerOrigin = new Position(origin.Line, origin.Column + 3);
                Position towerDestination = new Position(origin.Line, origin.Column + 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncMoves();
                Board.AddPiece(tower, towerDestination);
            }

            // Big Castling
            if (piece is King && destination.Column == (origin.Column - 2))
            {
                Position towerOrigin = new Position(origin.Line, origin.Column - 4);
                Position towerDestination = new Position(origin.Line, origin.Column - 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncMoves();
                Board.AddPiece(tower, towerDestination);
            }

            // # Special: En Passant

            if(piece is Pawn)
            {
                if(origin.Column != destination.Column && captured == null)
                {
                    Position enPassantCaptured;
                    if(piece.Color == Color.white)
                    {
                        enPassantCaptured = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        enPassantCaptured = new Position(destination.Line - 1, destination.Column);
                    }

                    captured = Board.RemovePiece(enPassantCaptured);
                    Captured.Add(captured);
                }
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

            // # Special: Castling
            
            // Small Castling
            if (piece is King && destination.Column == (origin.Column + 2))
            {
                Position towerOrigin = new Position(origin.Line, origin.Column + 3);
                Position towerDestination = new Position(origin.Line, origin.Column + 1);
                Piece tower = Board.RemovePiece(towerDestination);
                tower.DecMoves();
                Board.AddPiece(tower, towerOrigin);
            }

            // Big Castling
            if (piece is King && destination.Column == (origin.Column - 2))
            {
                Position towerOrigin = new Position(origin.Line, origin.Column - 4);
                Position towerDestination = new Position(origin.Line, origin.Column - 1);
                Piece tower = Board.RemovePiece(towerDestination);
                tower.DecMoves();
                Board.AddPiece(tower, towerOrigin);
            }

            // # Special: En Passant

            if(piece is Pawn)
            {
                if (origin.Column != origin.Column && captured == EnPassantVulnerable)
                {
                    Piece enPassantCaptured = Board.RemovePiece(destination);
                    Position returnPosition;
                    if(piece.Color == Color.white)
                    {
                        returnPosition = new Position(3, destination.Column);
                    }
                    else
                    {
                        returnPosition = new Position(4, destination.Column);
                    }

                    Board.AddPiece(enPassantCaptured, returnPosition);

                }

            }

        }

        public void DoMove(Position origin, Position destination)
        {
            Piece captured = MovePiece(origin, destination);

            if (IsCheck(ActualPlayer))
            {
                UndoMove(origin, destination, captured);
                throw new BoardException("Check!");
            }

            Piece piece = Board.Piece(destination);
            // # Special: Promotion

            if(piece is Pawn)
            {
                if((piece.Color == Color.white && destination.Line == 0) || (piece.Color == Color.black && destination.Line == 7))
                {
                    piece = Board.RemovePiece(destination);
                    Pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.AddPiece(queen, destination);
                    Pieces.Add(queen);
                }
            }

            if (IsCheck(Oposite(ActualPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if(IsMate(Oposite(ActualPlayer)))
            {
                EndGame = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            // # Special: En Passant

            if (piece is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                EnPassantVulnerable = piece;
            }

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
            PutNewPiece('a', 8, new Rook(Board, Color.black));
            PutNewPiece('b', 8, new Knight(Board, Color.black));
            PutNewPiece('c', 8, new Bishop(Board, Color.black));
            PutNewPiece('d', 8, new Queen(Board, Color.black));
            PutNewPiece('e', 8, new King(Board, Color.black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.black));
            PutNewPiece('g', 8, new Knight(Board, Color.black));
            PutNewPiece('h', 8, new Rook(Board, Color.black));
            PutNewPiece('a', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('b', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('c', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('d', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('e', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('f', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('g', 7, new Pawn(Board, Color.black, this));
            PutNewPiece('h', 7, new Pawn(Board, Color.black, this));

            PutNewPiece('a', 1, new Rook(Board, Color.white));
            PutNewPiece('b', 1, new Knight(Board, Color.white));
            PutNewPiece('c', 1, new Bishop(Board, Color.white));
            PutNewPiece('d', 1, new Queen(Board, Color.white));
            PutNewPiece('e', 1, new King(Board, Color.white, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.white));
            PutNewPiece('g', 1, new Knight(Board, Color.white));
            PutNewPiece('h', 1, new Rook(Board, Color.white));
            PutNewPiece('a', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('b', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('c', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('d', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('e', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('f', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('g', 2, new Pawn(Board, Color.white, this));
            PutNewPiece('h', 2, new Pawn(Board, Color.white, this));

        }

    }
}
