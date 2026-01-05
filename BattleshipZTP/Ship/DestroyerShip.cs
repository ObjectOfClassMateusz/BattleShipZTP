namespace BattleshipZTP;

public class DestroyerShip : BaseShip
{
    private const int DestroyerSize = 2;
    public DestroyerShip(List<Point> initialPlacement) 
        : base(DestroyerSize, initialPlacement)
    {
        StreamReader file = new StreamReader($"data/ships/classic/destroyer/model.txt");
        string reader = file.ReadLine();
        _body.Add((reader, 0));
        _name = "Destroyer";
    }
}