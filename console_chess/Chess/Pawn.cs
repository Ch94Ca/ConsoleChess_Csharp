using board;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool EnemyIn(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool FreePosition(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] ValidMoves()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position verifyPosition = new Position(0, 0);

            if (Color == Color.white)
            {
                verifyPosition.SetPosition(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(verifyPosition) && FreePosition(verifyPosition))
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

                verifyPosition.SetPosition(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(verifyPosition) && FreePosition(verifyPosition) && Moves == 0)
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

                verifyPosition.SetPosition(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(verifyPosition) && EnemyIn(verifyPosition))
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

                verifyPosition.SetPosition(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(verifyPosition) && EnemyIn(verifyPosition))
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

            }
            else
            {
                verifyPosition.SetPosition(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(verifyPosition) && FreePosition(verifyPosition))
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

                verifyPosition.SetPosition(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(verifyPosition) && FreePosition(verifyPosition) && Moves == 0)
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

                verifyPosition.SetPosition(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(verifyPosition) && EnemyIn(verifyPosition))
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }

                verifyPosition.SetPosition(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(verifyPosition) && EnemyIn(verifyPosition))
                {
                    matrix[verifyPosition.Line, verifyPosition.Column] = true;
                }
            }

            return matrix;

        }
    }
}