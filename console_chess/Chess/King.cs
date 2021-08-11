using board;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

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

            return matrix;

        }

    }
}
