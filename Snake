namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            Move(robot, Direction.Right, width - 3);
            Move(robot, Direction.Down, 2);
            Move(robot, Direction.Left, width - 3);

            if (!robot.Finished)
            {
                Move(robot, Direction.Down, 2);
                MoveOut(robot, width, height);
            }
        }

        private static void Move(Robot robot, Direction direction, int steps)
        {
            for (int i = 0; i < steps; i++)
                robot.MoveTo(direction);
        }
    }
}
