using board;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "H";
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

            verifyPosition.SetPosition(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
            }

            return matrix;

        }

    }
}
