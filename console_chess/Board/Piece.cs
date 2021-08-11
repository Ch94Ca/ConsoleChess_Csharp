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

        public abstract bool[,] ValidMoves();
    }
}
