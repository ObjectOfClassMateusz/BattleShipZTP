// csharp
namespace BattleshipZTP.Networking;

public class SimpleAI
{
    private Random _rnd = new Random();
    private List<Point> _currentHits = new List<Point>();
    private Queue<Point> _targetsToHit = new Queue<Point>();
    private HashSet<Point> _alreadyShot = new HashSet<Point>();

    private enum Orientation { Unknown, Horizontal, Vertical }
    private Orientation _orientation = Orientation.Unknown;

    public Point GetNextMove(int width, int height)
    {
        while (_targetsToHit.Count > 0)
        {
            Point target = _targetsToHit.Dequeue();
            if (!ContainsPoint(_alreadyShot, target))
            {
                _alreadyShot.Add(target);
                return target;
            }
        }

        Point randomPoint;
        do
        {
            randomPoint = new Point(_rnd.Next(width), _rnd.Next(height));
        } while (ContainsPoint(_alreadyShot, randomPoint));

        _alreadyShot.Add(randomPoint);
        return randomPoint;
    }

    public void AddTargetNeighbors(Point hitPoint, int width, int height)
    {
        if (!ContainsPoint(_currentHits, hitPoint))
            _currentHits.Add(hitPoint);

        if (_currentHits.Count >= 2 && _orientation == Orientation.Unknown)
        {
            var a = _currentHits[0];
            var b = _currentHits[1];
            if (a.X == b.X) _orientation = Orientation.Vertical;
            else if (a.Y == b.Y) _orientation = Orientation.Horizontal;
        }

        if (_orientation == Orientation.Unknown)
        {
            EnqueueIfValid(new Point(hitPoint.X, hitPoint.Y + 1), width, height);
            EnqueueIfValid(new Point(hitPoint.X, hitPoint.Y - 1), width, height);
            EnqueueIfValid(new Point(hitPoint.X + 1, hitPoint.Y), width, height);
            EnqueueIfValid(new Point(hitPoint.X - 1, hitPoint.Y), width, height);
        }
        else
        {
            if (_orientation == Orientation.Horizontal)
            {
                int minX = _currentHits.Min(p => p.X);
                int maxX = _currentHits.Max(p => p.X);
                int y = _currentHits[0].Y;

                EnqueueIfValid(new Point(maxX + 1, y), width, height);
                EnqueueIfValid(new Point(minX - 1, y), width, height);
            }
            else
            {
                int minY = _currentHits.Min(p => p.Y);
                int maxY = _currentHits.Max(p => p.Y);
                int x = _currentHits[0].X;

                EnqueueIfValid(new Point(x, maxY + 1), width, height);
                EnqueueIfValid(new Point(x, minY - 1), width, height);
            }
        }
    }

    public void ClearTargets()
    {
        _targetsToHit.Clear();
        _currentHits.Clear();
        _orientation = Orientation.Unknown;
    }

    private void EnqueueIfValid(Point p, int width, int height)
    {
        if (p.X < 0 || p.X >= width || p.Y < 0 || p.Y >= height) return;
        if (ContainsPoint(_alreadyShot, p)) return;
        // unikamy duplikatów w kolejce
        if (_targetsToHit.Any(t => t.X == p.X && t.Y == p.Y)) return;
        _targetsToHit.Enqueue(p);
    }

    private bool ContainsPoint(IEnumerable<Point> collection, Point p)
    {
        return collection.Any(q => q.X == p.X && q.Y == p.Y);
    }
}