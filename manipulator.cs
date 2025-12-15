using System;
using Avalonia;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
    public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
    {
        var elbowX = Math.Cos(shoulder) * UpperArm;
        var elbowY = Math.Sin(shoulder) * UpperArm;
        var elbowPos = new Point(elbowX, elbowY);

        var forearmAngle = shoulder - (Math.PI - elbow);
        var wristX = elbowPos.X + Math.Cos(forearmAngle) * Forearm;
        var wristY = elbowPos.Y + Math.Sin(forearmAngle) * Forearm;
        var wristPos = new Point(wristX, wristY);

        var palmAngle = forearmAngle - (Math.PI - wrist);
        var palmX = wristPos.X + Math.Cos(palmAngle) * Palm;
        var palmY = wristPos.Y + Math.Sin(palmAngle) * Palm;
        var palmEndPos = new Point(palmX, palmY);

        return new[] { elbowPos, wristPos, palmEndPos };
    }
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
    private const double Tolerance = 1e-5;

    [TestCase(0, Math.PI, Math.PI, UpperArm + Forearm + Palm, 0)]
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
    [TestCase(Math.PI / 2, -Math.PI / 2, Math.PI, -Forearm - Palm, UpperArm)]
    [TestCase(Math.PI / 2, -Math.PI / 2, -Math.PI / 2, -Forearm, UpperArm - Palm)]
    public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
        var elbowPos = joints[0];
        var wristPos = joints[1];
        var palmEndPos = joints[2];
        
        Assert.That(palmEndPos.X, Is.EqualTo(palmEndX).Within(Tolerance), "palm endX");
        Assert.That(palmEndPos.Y, Is.EqualTo(palmEndY).Within(Tolerance), "palm endY");
        
        var upperArmLength = Math.Sqrt(elbowPos.X * elbowPos.X + elbowPos.Y * elbowPos.Y);
        Assert.That(upperArmLength, Is.EqualTo(UpperArm).Within(Tolerance), "Upper arm length");
        
        var dx = wristPos.X - elbowPos.X;
        var dy = wristPos.Y - elbowPos.Y;
        var forearmLength = Math.Sqrt(dx * dx + dy * dy);
        Assert.That(forearmLength, Is.EqualTo(Forearm).Within(Tolerance), "Forearm length");
        
        dx = palmEndPos.X - wristPos.X;
        dy = palmEndPos.Y - wristPos.Y;
        var palmLength = Math.Sqrt(dx * dx + dy * dy);
        Assert.That(palmLength, Is.EqualTo(Palm).Within(Tolerance), "Palm length");
    }
}
