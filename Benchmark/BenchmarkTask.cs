using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using StructBenchmarking;

namespace StructBenchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
        GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                        // и как-то повлияет на них.
        task.Run();
        var time = Stopwatch.StartNew();
        for (int i = 0; i < repetitionCount; i++)
        {
            task.Run();
        }
        time.Stop();
        return (double)time.ElapsedMilliseconds / repetitionCount;
    }
}

public class SBuilder : ITask
{
    public void Run()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 10000; i++)
        {
            sb.Append('a');
        }
        var result = sb.ToString();
    }
}

public class SConstructor : ITask
{
    public void Run()
    {
        var result = new string('a', 10000);
    }
}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        int repeatCountConstructor = 20000;
        int repeatCountBuilder = 5000;
        var sBuilder = new SBuilder();
        var sConstructor = new SConstructor();
        var sBTime = benchmark.MeasureDurationInMs(sBuilder, repeatCountBuilder);
        var sCTime = benchmark.MeasureDurationInMs(sConstructor, repeatCountConstructor);
        Assert.That(sCTime, Is.LessThan(sBTime));
    }
}