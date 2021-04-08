namespace Game.Logic
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Position operator +(Position lhs, Position rhs)
        {
            return new Position(lhs.Row + rhs.Row, lhs.Column + rhs.Column);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Row, Column);
        //}

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(Position other)
        {
            return Row == other.Row && Column == other.Column;
        }
    }
}
