namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        int rightSteps = (width - 3) / (height - 2);
        int downSteps = (height - 3) / (width - 2);

        if (width > height)
            MoveRightFirst(robot, height - 2, rightSteps);
        else
            MoveDownFirst(robot, width - 2, downSteps);
    }

    private static void MoveRightFirst(Robot robot, int iterations, int steps)
    {
        for (int i = 0; i < iterations; i++)
        {
            MoveRight(robot, steps);
            if (!robot.Finished) robot.MoveTo(Direction.Down);
        }
    }

    private static void MoveDownFirst(Robot robot, int iterations, int steps)
    {
        for (int i = 0; i < iterations; i++)
        {
            MoveDown(robot, steps);
            if (!robot.Finished) robot.MoveTo(Direction.Right);
        }
    }

    private static void MoveRight(Robot robot, int steps)
    {
        for (int i = 0; i < steps; i++)
            robot.MoveTo(Direction.Right);
    }

    private static void MoveDown(Robot robot, int steps)
    {
        for (int i = 0; i < steps; i++)
            robot.MoveTo(Direction.Down);
    }
}