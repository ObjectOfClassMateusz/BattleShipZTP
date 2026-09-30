namespace BattleshipZTP.Commands;

/**
 * @brief Interface for creating commands for ships
 */
public interface ICommand
{
    void Execute(List<(int x, int y)> coords);
    List<(string text, int offset)> GetBody();
    void SetBody(List<(string text, int offset)> body);
    bool PlaceCondition(int x , int y);
}