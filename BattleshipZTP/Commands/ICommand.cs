namespace BattleshipZTP.Commands;

public interface ICommand
{
    void Execute(List<(int x, int y)> coords);
    List<(string text, int offset)> GetBody();
    bool PlaceCondition(int x , int y);
}