using System;
using System.Collections.Generic;

namespace StructBenchmarking;

public class Experiments
{
    public static ChartData BuildDate
        (
        IBenchmark benchmark, int repetitionsCount,
        string title,
        Func<int, ITask> createStructTask,
        Func<int, ITask> createClassTask
        )
    {
        var classesTimes = new List<ExperimentResult>();
        var structuresTimes = new List<ExperimentResult>();

        foreach (var c in Constants.FieldCounts)
        {
            var structArray = createStructTask(c);
            var classesArray = createClassTask(c);

            structuresTimes.Add(new ExperimentResult(c, benchmark.MeasureDurationInMs(structArray, repetitionsCount)));
            classesTimes.Add(new ExperimentResult(c, benchmark.MeasureDurationInMs(classesArray, repetitionsCount)));
        }

        return new ChartData
        {
            Title = title,
            ClassPoints = classesTimes,
            StructPoints = structuresTimes,
        };
    }

    public static ChartData BuildChartDataForArrayCreation(
        IBenchmark benchmark, int repetitionsCount)
    {
        return BuildDate
             (
            benchmark,
             repetitionsCount,
             "Create array",
             fieldCount => new StructArrayCreationTask(fieldCount),
             fieldCount => new ClassArrayCreationTask(fieldCount)
             );
    }

    public static ChartData BuildChartDataForMethodCall(
        IBenchmark benchmark, int repetitionsCount)
    {
        return BuildDate
             (
            benchmark,
             repetitionsCount,
             "Call method with argument",
             fieldCount => new MethodCallWithStructArgumentTask(fieldCount),
             fieldCount => new MethodCallWithClassArgumentTask(fieldCount)
             );
    }
}