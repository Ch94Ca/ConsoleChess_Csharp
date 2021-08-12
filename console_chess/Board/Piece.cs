namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Moves { get; protected set; }
        public Board Board { get; set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Moves = 0;
            Board = board;
        }

        public void IncMoves()
        {
            Moves++;
        }

        public void DecMoves()
        {
            Moves--;
        }

        public bool IsValidMoves()
        {
            bool[,] validMoves = ValidMoves();

            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (validMoves[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool CanMoveto(Position destination)
        {
            return ValidMoves()[destination.Line, destination.Column];
        }

        public abstract bool[,] ValidMoves();

    }
}
