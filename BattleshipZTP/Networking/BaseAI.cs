using System;
using System.Collections.Generic;
using System.Linq;
using BattleshipZTP.GameAssets;

namespace BattleshipZTP.Networking;

public abstract class BaseAI : IAI
{
    protected Random _rnd = new Random();
    protected List<Point> _currentHits = new List<Point>();
    protected Queue<Point> _targetsToHit = new Queue<Point>();
    protected HashSet<Point> _alreadyShot = new HashSet<Point>();
    protected List<int> _remainingShipSizes = new List<int>();


    protected enum Orientation { Unknown, Horizontal, Vertical }
    protected Orientation _orientation = Orientation.Unknown;

    public abstract Point GetNextMove(int width, int height, IBattleBoard board);

    public virtual void AddTargetNeighbors(Point hitPoint, int width, int height)
    {
        if (!ContainsPoint(_currentHits, hitPoint))
            _currentHits.Add(hitPoint);

        if (_currentHits.Count >= 2)
        {
            var first = _currentHits[0];
            var last = _currentHits.Last();
        
            if (first.X == last.X) _orientation = Orientation.Vertical;
            else if (first.Y == last.Y) _orientation = Orientation.Horizontal;
        }

        _targetsToHit.Clear();

        if (_orientation == Orientation.Vertical)
        {
            int minY = _currentHits.Min(p => p.Y);
            int maxY = _currentHits.Max(p => p.Y);
            int x = _currentHits[0].X;

            EnqueueIfValid(new Point(x, minY - 1), width, height);
            EnqueueIfValid(new Point(x, maxY + 1), width, height);
        }
        else if (_orientation == Orientation.Horizontal)
        {
            int minX = _currentHits.Min(p => p.X);
            int maxX = _currentHits.Max(p => p.X);
            int y = _currentHits[0].Y;

            EnqueueIfValid(new Point(minX - 1, y), width, height);
            EnqueueIfValid(new Point(maxX + 1, y), width, height);
        }
        else
        {
            EnqueueIfValid(new Point(hitPoint.X, hitPoint.Y + 1), width, height);
            EnqueueIfValid(new Point(hitPoint.X, hitPoint.Y - 1), width, height);
            EnqueueIfValid(new Point(hitPoint.X + 1, hitPoint.Y), width, height);
            EnqueueIfValid(new Point(hitPoint.X - 1, hitPoint.Y), width, height);
        }
    }
    
    public virtual void ClearTargets()
    {
        _targetsToHit.Clear();
        _currentHits.Clear();
        _orientation = Orientation.Unknown;
    }

    protected void EnqueueIfValid(Point p, int width, int height)
    {
        if (p.X < 0 || p.X >= width || p.Y < 0 || p.Y >= height) return;
        if (ContainsPoint(_alreadyShot, p)) return;
        if (_targetsToHit.Any(t => t.X == p.X && t.Y == p.Y)) return;
        _targetsToHit.Enqueue(p);
    }
    
    protected bool IsAdjacentToSunkShip(Point p, IBattleBoard board)
    {
        for (int dx = -1; dx <= 1; dx++)
        for (int dy = -1; dy <= 1; dy++)
        {
            int nx = p.X + dx;
            int ny = p.Y + dy;
            var field = board.GetField(nx, ny);
            if (field != null && field.ShipReference != null && field.ShipReference.IsSunk())
                return true;
        }
        return false;
    }

    protected bool ContainsPoint(IEnumerable<Point> collection, Point p)
    {
        return collection.Any(q => q.X == p.X && q.Y == p.Y);
    }
}