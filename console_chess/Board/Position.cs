using System.Text;

namespace board
{
    class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public void SetPosition(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public string ToChessPosition()
        {
            return $"{(char)(Column + 97)}{8-Line}";
        }

        public override string ToString()
        {
            return $"{Line}, {Column}";
        }

    }
}
