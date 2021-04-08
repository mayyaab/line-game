using System;
using System.Collections.Generic;

namespace Game.Logic
{
    public class GetPathOriginal : IPathStrategy
    {
        private readonly Field _field = new Field();

        public IList<Position> GetPath(Position source, Position destination, bool[,] visited)
        {
            return GetPathWave(source, destination);
        }

        public IList<Position> GetPathWave(Position source, Position destination)
        {
            int[,] visited = new int[_field.Height, _field.Width];
            int step = 0;
            var position = new Tuple<Position, int>(source, step);
            var visitedPositions = new Queue<Tuple<Position, int>>();
            visitedPositions.Enqueue(position);
            visited[source.Row, source.Column] = step;

            while (visitedPositions.Count != 0)
            {
                var dequeue = visitedPositions.Dequeue();

                if (dequeue.Item1 == destination)
                {
                    visited[dequeue.Item1.Row, dequeue.Item1.Column] = dequeue.Item2;
                    var pathBack = GetPathBack(source, destination, visited);
                    pathBack.Reverse();
                    return pathBack;
                }

                visited[dequeue.Item1.Row, dequeue.Item1.Column] = dequeue.Item2;
                var neighbors = _field.GetNeighbors(dequeue.Item1);

                foreach (var neighbor in neighbors)
                {
                    if (visited[neighbor.Row, neighbor.Column] == 0 && _field.GetBallColorAt(neighbor.Row, neighbor.Column) == BallColor.Empty)
                    {
                        var newStep = dequeue.Item2;
                        visitedPositions.Enqueue(new Tuple<Position, int>(neighbor, newStep + 1));
                    }
                }
            }
            return null;
        }

        private List<Position> GetPathBack(Position source, Position destination, int[,] visited)
        {
            var listBack = new List<Position>();
            while (destination != source)
            {
                var neighbors = _field.GetNeighbors(destination);
                foreach (var neighbor in neighbors)
                {
                    if (neighbor == source)
                    {
                        listBack.Add(neighbor);
                        return listBack;
                    }
                    if (visited[neighbor.Row, neighbor.Column] == visited[destination.Row, destination.Column] - 1)
                    {
                        listBack.Add(neighbor);
                        destination = neighbor;
                        break;
                    }
                }
            }
            return null;
        }
    }
}
