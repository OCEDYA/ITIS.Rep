using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        task.Run();
        
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < repetitionCount; i++)
        {
            task.Run();
        }
        stopwatch.Stop();
        
        return stopwatch.ElapsedMilliseconds / (double)repetitionCount;
    }
}

public class StringBuilderTask : ITask
{
    public void Run()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 10000; i++)
        {
            sb.Append('a');
        }
        _ = sb.ToString();
    }
}

public class StringConstructorTask : ITask
{
    public void Run()
    {
        _ = new string('a', 10000);
    }
}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        var stringBuilderTask = new StringBuilderTask();
        var stringConstructorTask = new StringConstructorTask();
        
        int repetitions = 10000;
        double stringBuilderTime = benchmark.MeasureDurationInMs(stringBuilderTask, repetitions);
        double stringConstructorTime = benchmark.MeasureDurationInMs(stringConstructorTask, repetitions);
        
        Assert.That(stringConstructorTime, Is.LessThan(stringBuilderTime));
    }
}
