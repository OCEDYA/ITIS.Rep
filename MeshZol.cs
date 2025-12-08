using System;
using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public class Sack : ICreature
{
    private int fallingDistance = 0;
    
    public string GetImageFileName() => "Sack.png";
    public int GetDrawingPriority() => 50;

    public CreatureCommand Act(int x, int y)
    {
        var command = new CreatureCommand();
        
        if (y + 1 >= Game.MapHeight)
        {
            if (fallingDistance > 1)
                command.TransformTo = new Gold();
            fallingDistance = 0;
            return command;
        }
        
        var creatureBelow = Game.Map[x, y + 1];
        
        if (creatureBelow == null)
        {
            command.DeltaY = 1;
            fallingDistance++;
        }
        else if (creatureBelow is Player && fallingDistance > 0)
        {
            command.DeltaY = 1;
            fallingDistance++;
        }
        else
        {
            if (fallingDistance > 1)
                command.TransformTo = new Gold();
            fallingDistance = 0;
        }
        
        return command;
    }

    public bool DeadInConflict(ICreature conflictedObject) => false;
}

public class Gold : ICreature
{
    public string GetImageFileName() => "Gold.png";
    public int GetDrawingPriority() => 50;
    public CreatureCommand Act(int x, int y) => new CreatureCommand();
    public bool DeadInConflict(ICreature conflictedObject)
    {
        if (conflictedObject is Player)
        {
            Game.Scores += 10;
            return true;
        }
        return false;
    }
}

public class Terrain : ICreature
{
    public string GetImageFileName() => "Terrain.png";
    public int GetDrawingPriority() => 100;
    public CreatureCommand Act(int x, int y) => new CreatureCommand();
    public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Player;
}

public class Player : ICreature
{
    public string GetImageFileName() => "Digger.png";
    public int GetDrawingPriority() => 0;
    
    public CreatureCommand Act(int x, int y)
    {
        var command = new CreatureCommand();
        
        if (Game.KeyPressed == Key.Left && x > 0)
        {
            var target = Game.Map[x - 1, y];
            if (!(target is Sack))
                command.DeltaX = -1;
        }
        else if (Game.KeyPressed == Key.Right && x < Game.MapWidth - 1)
        {
            var target = Game.Map[x + 1, y];
            if (!(target is Sack))
                command.DeltaX = 1;
        }
        else if (Game.KeyPressed == Key.Up && y > 0)
        {
            var target = Game.Map[x, y - 1];
            if (!(target is Sack))
                command.DeltaY = -1;
        }
        else if (Game.KeyPressed == Key.Down && y < Game.MapHeight - 1)
        {
            var target = Game.Map[x, y + 1];
            if (!(target is Sack))
                command.DeltaY = 1;
        }
        
        return command;
    }

    public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Sack;
}
