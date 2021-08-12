using board;

namespace chess
{
    class King : Piece
    {
        private ChessGame Game;

        public King(Board board, Color color, ChessGame game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece == null || piece.Color != Color;
        }

        private bool CheckTower(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.Moves == 0;
        }

        public override bool[,] ValidMoves()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position verifyPosition = new Position(0, 0);

            verifyPosition.SetPosition(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            // # Special: Castling

            if (Moves == 0 && !Game.Check)
            {
                // # Special: Small Castling

                Position rookPosition = new Position(Position.Line, Position.Column + 3);
                if (CheckTower(rookPosition))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        matrix[Position.Line, Position.Column + 2] = true;
                    }

                }
                
                // # Special: Big Castling
                Position rookPosition2 = new Position(Position.Line, Position.Column - 4);
                if (CheckTower(rookPosition2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        matrix[Position.Line, Position.Column - 2] = true;
                    }
                    
                }
                
            }

            return matrix;

        }

    }
}
