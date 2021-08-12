using board;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "B";
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

            verifyPosition.SetPosition(Position.Line - 1, Position.Column -1);
            while (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
                if (Board.Piece(verifyPosition) != null && Board.Piece(verifyPosition).Color != Color)
                {
                    break;
                }

                verifyPosition.SetPosition(verifyPosition.Line - 1, verifyPosition.Column - 1);
            }

            verifyPosition.SetPosition(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
                if (Board.Piece(verifyPosition) != null && Board.Piece(verifyPosition).Color != Color)
                {
                    break;
                }

                verifyPosition.SetPosition(verifyPosition.Line - 1, verifyPosition.Column + 1);
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
                if (Board.Piece(verifyPosition) != null && Board.Piece(verifyPosition).Color != Color)
                {
                    break;
                }

                verifyPosition.SetPosition(verifyPosition.Line + 1, verifyPosition.Column + 1);
            }

            verifyPosition.SetPosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(verifyPosition) && CanMove(verifyPosition))
            {
                matrix[verifyPosition.Line, verifyPosition.Column] = true;
                if (Board.Piece(verifyPosition) != null && Board.Piece(verifyPosition).Color != Color)
                {
                    break;
                }

                verifyPosition.SetPosition(verifyPosition.Line + 1, verifyPosition.Column - 1);
            }

            return matrix;
        }
    }
}
