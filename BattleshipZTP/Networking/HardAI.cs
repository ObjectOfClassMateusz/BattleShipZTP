using System;
using System.Collections.Generic;
using System.Linq;
using BattleshipZTP.GameAssets;

namespace BattleshipZTP.Networking;

public class HardAI : MediumAI
{
    public override Point GetNextMove(int width, int height, IBattleBoard board)
    {
        while (_targetsToHit.Count > 0)
        {
            Point t = _targetsToHit.Dequeue();
            if (!ContainsPoint(_alreadyShot, t) && !IsAdjacentToSunkShip(t, board))
            {
                _alreadyShot.Add(t);
                return t;
            }
        }

        var largestArea = FindLargestUnshotArea(width, height)
            .Where(p => !IsAdjacentToSunkShip(p, board)) // DODANO: Filtrowanie pól przy wraku
            .ToList();

        var chessboardFields = largestArea
            .Where(p => (p.X + p.Y) % 2 == 0 && !ContainsPoint(_alreadyShot, p))
            .ToList();        
    
        if (chessboardFields.Count > 0)
        {
            var chosen = chessboardFields[_rnd.Next(chessboardFields.Count)];
            _alreadyShot.Add(chosen);
            return chosen;
        }

        Point randomPoint;
        do
        {
            randomPoint = new Point(_rnd.Next(width), _rnd.Next(height));
        } while (ContainsPoint(_alreadyShot, randomPoint) || IsAdjacentToSunkShip(randomPoint, board));

        _alreadyShot.Add(randomPoint);
        return randomPoint;
    }
    
    private List<Point> FindLargestUnshotArea(int width, int height)
    {
        bool[,] shot = new bool[width, height];
        foreach (var p in _alreadyShot)
            if (p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height)
                shot[p.X, p.Y] = true;

        bool[,] visited = new bool[width, height];
        List<Point> largestArea = new();

        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!shot[x, y] && !visited[x, y])
                {
                    List<Point> area = new();
                    Queue<Point> queue = new();
                    queue.Enqueue(new Point(x, y));
                    visited[x, y] = true;

                    while (queue.Count > 0)
                    {
                        var p = queue.Dequeue();
                        area.Add(p);

                        for (int dir = 0; dir < 4; dir++)
                        {
                            int nx = p.X + dx[dir];
                            int ny = p.Y + dy[dir];
                            if (nx >= 0 && nx < width && ny >= 0 && ny < height &&
                                !shot[nx, ny] && !visited[nx, ny])
                            {
                                visited[nx, ny] = true;
                                queue.Enqueue(new Point(nx, ny));
                            }
                        }
                    }

                    if (area.Count > largestArea.Count)
                        largestArea = area;
                }
            }
        }
        return largestArea;
    }
}