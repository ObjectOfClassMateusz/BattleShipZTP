using BattleshipZTP.GameAssets;

namespace BattleshipZTP.Networking;

public interface IAI
{
    Point GetNextMove(int width, int height, IBattleBoard board);
    void AddTargetNeighbors(Point hitPoint, int width, int height);
    void ClearTargets();
}