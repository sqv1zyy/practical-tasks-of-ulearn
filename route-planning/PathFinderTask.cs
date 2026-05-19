using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    static Point[] checkP;
    static int[] bestOrder;
    static double minDist;
    static int[] currentRoute;
    static bool[] visited;
    static int allCheckPoint;

    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        checkP = checkpoints;
        allCheckPoint = checkP.Length;
        if (allCheckPoint <= 1) return new int[allCheckPoint > 0 ? 1 : 0]; 

        bestOrder = new int[allCheckPoint];
        currentRoute = new int[allCheckPoint];
        visited = new bool[allCheckPoint];
        minDist = double.MaxValue;

        visited[0] = true;
        currentRoute[0] = 0;

        MakeOrder(1, 0.0);
        return bestOrder;
    }

    static void Evaluate(double dist)
    {
        if (dist < minDist)
        {
            minDist = dist;
            for (int i = 0; i < allCheckPoint; i++)
                bestOrder[i] = currentRoute[i];
        }
    }

    static void MakeOrder(int pos, double dist)
    {
        if (dist >= minDist) return;

        if (pos == allCheckPoint)
        {
            Evaluate(dist);
            return;
        }
        for (int i = 1; i < allCheckPoint; i++)
        {
            if (!visited[i])
            {
                double dx = checkP[currentRoute[pos - 1]].X - checkP[i].X;
                double dy = checkP[currentRoute[pos - 1]].Y - checkP[i].Y;
                double d = Math.Sqrt(dx * dx + dy * dy);

                if (dist + d >= minDist) continue;

                visited[i] = true;
                currentRoute[pos] = i;
                MakeOrder(pos + 1, dist + d);
                visited[i] = false; 
            }
        }
    }
}