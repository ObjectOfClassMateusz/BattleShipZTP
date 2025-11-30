namespace BattleshipZTP;

public abstract class BaseShip : IShip
{
    protected int size { get; private set; } 
    private List<Point> hits;
    private List<Point> placement;
    
    public BaseShip(int size, List<Point> initialPlacement)
    {
        this.size = size;
        this.placement = initialPlacement;
        this.hits = new List<Point>();
    }

    public int GetSize()
    {
        return size;
    }
    
    public HitResult TakeHit(Point coords)
    {
        if (!placement.Contains(coords))
        {
            return HitResult.Miss; // jesli statek nie zajmuje tych koordynatow, to pudlo
        }
        
        if (hits.Contains(coords))
        {
            return HitResult.AlreadyHit; // jesli statek juz byl trafiony w tych koordynatach
        }
        hits.Add(coords); 
        if (IsSunk())
        {
            return HitResult.Sunk; // jesli statek jest zatopiony
        }
        return HitResult.Hit; // jesli statek jest trafiony ale nie zatopiony
    }

    public bool IsSunk()
    {
        return hits.Count == size; // jesli liczba trafien jest rowna rozmiarowi statku, to statek jest zatopiony
    }
}