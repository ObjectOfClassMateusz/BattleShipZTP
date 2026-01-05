namespace BattleshipZTP.Networking;

public class SimpleAI
{
    private Random _rnd = new Random();
    private Point? _lastHit = null;
    private List<Point> _targetsToCheck = new List<Point>();

    public Point GetNextMove(int boardWidth, int boardHeight)
    {
        // Jeśli mamy pola do sprawdzenia wokół trafienia (Hunt Mode)
        if (_targetsToCheck.Count > 0)
        {
            Point next = _targetsToCheck[0];
            _targetsToCheck.RemoveAt(0);
            return next;
        }

        // Losowy strzał (Seek Mode)
        return new Point(_rnd.Next(0, boardWidth), _rnd.Next(0, boardHeight));
    }

    public void ReportResult(Point p, HitResult result)
    {
        if (result == HitResult.Hit)
        {
            _lastHit = p;
            // Dodaj sąsiednie pola do sprawdzenia
            _targetsToCheck.Add(new Point(p.X + 1, p.Y));
            _targetsToCheck.Add(new Point(p.X - 1, p.Y));
            _targetsToCheck.Add(new Point(p.X, p.Y + 1));
            _targetsToCheck.Add(new Point(p.X, p.Y - 1));
        }
        else if (result == HitResult.HitAndSunk)
        {
            _targetsToCheck.Clear(); // Statek zatopiony, wracamy do losowania
        }
    }
}