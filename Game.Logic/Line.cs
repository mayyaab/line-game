using System;
using System.Collections.Generic;

namespace Game.Logic
{
    public class Line
    {
        //интерфейс класса - method/fields/enum etc. only publuc (то что могут использовать клиенты класса)
        //public вначале
        public enum Direction
        {
            Horizontal,
            Vertical,
            Descending,
            Ascending
        }

        //не должно быть публичных филдов
        //IList>>ICollection
        public IList<Position> Positions { get; }

        public Line(IList<Position> positions)
        {
            if (positions == null)
            {
                throw new ArgumentNullException(nameof(positions));
            }

            Positions = positions;
        }
    }
}
