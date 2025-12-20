using System.Collections.Generic;

namespace StructBenchmarking
{
    public interface ITaskFactory
    {
        ITask CreateStructTask(int size);
        ITask CreateClassTask(int size);
    }

    public class ArrayCreationTaskFactory : ITaskFactory
    {
        public ITask CreateStructTask(int size)
        {
            return new StructArrayCreationTask(size);
        }

        public ITask CreateClassTask(int size)
        {
            return new ClassArrayCreationTask(size);
        }
    }

    public class MethodCallTaskFactory : ITaskFactory
    {
        public ITask CreateStructTask(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }

        public ITask CreateClassTask(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }
    }

    public class Experiments
    {
        private static ChartData BuildChartData(
            IBenchmark benchmark,
            ITaskFactory factory,
            int repetitionsCount,
            string title)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var fieldCount in Constants.FieldCounts)
            {
                var structTask = factory.CreateStructTask(fieldCount);
                var structTime = benchmark.MeasureDurationInMs(structTask, repetitionsCount);
                structuresTimes.Add(new ExperimentResult(fieldCount, structTime));

                var classTask = factory.CreateClassTask(fieldCount);
                var classTime = benchmark.MeasureDurationInMs(classTask, repetitionsCount);
                classesTimes.Add(new ExperimentResult(fieldCount, classTime));
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
            var factory = new ArrayCreationTaskFactory();
            return BuildChartData(benchmark, factory, repetitionsCount, "Создание массива");
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var factory = new MethodCallTaskFactory();
            return BuildChartData(benchmark, factory, repetitionsCount, "Вызов метода с аргументом");
        }
    }
}
