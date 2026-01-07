using System;
using BattleshipZTP.GameAssets;

namespace BattleshipZTP.Networking;

public class SimpleAI : BaseAI
{
    public override Point GetNextMove(int width, int height, IBattleBoard board)
    {
        if (_targetsToHit.Count > 0 && _rnd.NextDouble() < 0.7)
        {
            while (_targetsToHit.Count > 0)
            {
                Point target = _targetsToHit.Dequeue();
                if (!ContainsPoint(_alreadyShot, target) && !IsAdjacentToSunkShip(target, board))
                {
                    _alreadyShot.Add(target);
                    return target;
                }
            }
        }

        Point randomPoint;
        do
        {
            randomPoint = new Point(_rnd.Next(width), _rnd.Next(height));
        } while (
            ContainsPoint(_alreadyShot, randomPoint) ||
            IsAdjacentToSunkShip(randomPoint, board)
        );
        _alreadyShot.Add(randomPoint);
        return randomPoint;
    }
}