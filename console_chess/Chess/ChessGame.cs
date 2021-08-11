using board;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public bool EndGame { get; private set; }
        private int Turn;
        private Color ActualPlayer;

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
