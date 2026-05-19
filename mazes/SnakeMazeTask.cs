namespace Mazes;

public static class SnakeMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
        {
            MoveRight(robot, width);
            MoveTwoDown(robot);
            MoveLeft(robot, width);
            if (robot.Finished) break;
            MoveTwoDown(robot);
        }
    }
    
    public static void MoveRight(Robot robot, int width)
    {
        for (int i = 0; i < width - 3; i++)
        {
            robot.MoveTo(Direction.Right);
        }
    }

    public static void MoveLeft(Robot robot, int width)
    {
        for (int i = width - 2; i != 1; i--)
        {
            robot.MoveTo(Direction.Left);
        }
    }

    public static void MoveDown(Robot robot, int height)
    {
        for (int i = 0; i < height - 3; i++)
        {
            robot.MoveTo(Direction.Down);
        }
    }

    public static void MoveTwoDown(Robot robot)
    {
        robot.MoveTo(Direction.Down);
        robot.MoveTo(Direction.Down);
    }
}