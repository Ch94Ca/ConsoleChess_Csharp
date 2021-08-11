using exceptions;

namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool CheckPiece(Position position)
        {
            ValidPosition(position);
            return Piece(position) != null;
        }

        public void AddPiece(Piece piece, Position position)
        {   
            if(CheckPiece(position))
            {
                throw new BoardException("This position already heave a piece.");
            }

            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if(Piece(position) == null)
            {
                return null;
            }

            Piece aux = Piece(position);
            aux.Position = null;
            Pieces[position.Line, position.Column] = null;
            return aux;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Column < 0 || position.Line >= Lines || position.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void CheckPosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid position");
            }
        }

    }
}
