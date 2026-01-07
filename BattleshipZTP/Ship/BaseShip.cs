namespace BattleshipZTP;

public abstract class BaseShip : IShip
{
    protected int size { get; private set; } 
    private List<Point> hits;
    private List<Point> placement;
    protected readonly List<(string text, int offset)> _body;
    protected (ConsoleColor foreground, ConsoleColor background) _colors;
    protected string _name;
    public int Size => size;

    public BaseShip(int size, List<Point> initialPlacement)
    {
        this.size = size;
        this.placement = initialPlacement;
        this.hits = new List<Point>();
        _body = new List<(string, int)>();
        _colors = (ConsoleColor.White, ConsoleColor.Black);
    }

    public string Name() => _name;

    public List<(string text,int offset)> GetBody()
    {
        return _body;
    }
    public void SetBody(List<(string text, int offset)> body)
    {   
        _body.Clear();
        foreach (var b in body) 
        { 
            _body.Add(b);
        }
    }
    public (ConsoleColor foreground, ConsoleColor background) GetColors()
    {
        return _colors;
    }
    public int GetSize()
    {
        return size;
    }

    public void Locate(List<(int x, int y)> coords)
    {
        List<Point> points = new List<Point>();
        foreach (var c in coords) {
            Point point = new Point();
            point.X = c.x;
            point.Y = c.y;
            points.Add(point);
        }
        placement = points;
    }
    
    public HitResult TakeHit(Point coords)
    {
        if (!placement.Contains(coords))
        {
            return HitResult.Miss;
        }
        
        hits.Add(coords); 
        if (IsSunk())
        {
            return HitResult.HitAndSunk;
        }
        return HitResult.Hit;
    }

    public bool IsSunk()
    {
        return hits.Distinct().Count() == size;
        
    }
}