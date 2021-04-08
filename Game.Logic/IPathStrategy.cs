using System.Collections.Generic;

namespace Game.Logic
{
    public interface IPathStrategy
    {
        IList<Position> GetPath(Position source, Position destination, bool[,] visited);
    }
}
