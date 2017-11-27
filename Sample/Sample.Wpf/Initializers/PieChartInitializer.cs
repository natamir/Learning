using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using Sample.Wpf.Extensibility.Initializers;

namespace Sample.Wpf.Initializers
{
    internal class PieChartInitializer : IPieChartInitializer
    {
        private const int DefaultYValuesIndex = 0;
        private const int DefaultSeriesIndex = 0;
        private const int AnimationStepCount = 5;

        public void Initialize(Chart pieChart, IDictionary<string, float> data)
        {
            float step = data.Values.Sum() / AnimationStepCount;
            pieChart.Series[DefaultSeriesIndex].Points.DataBindXY(data.Keys, data.Values);
            IObservable<long> previousDataPointObservable = null;
            foreach (DataPoint dataPoint in pieChart.Series[DefaultSeriesIndex].Points)
            {
                double initialValue = data[dataPoint.AxisLabel];
                dataPoint.YValues[DefaultYValuesIndex] = 0;
                previousDataPointObservable = CreateObservable(pieChart, dataPoint, initialValue, step, previousDataPointObservable);
            }
            previousDataPointObservable?.RunAsync(CancellationToken.None);
        }

        private static IObservable<long> CreateObservable(Chart pieChart, DataPoint dataPoint, double initialValue, float step, IObservable<long> previousDataPointObservable)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(1))
                .ObserveOn(SynchronizationContext.Current)
                .Take(AnimationStepCount)
                .Do(
                    timer =>
                    {
                        if (dataPoint.YValues[DefaultYValuesIndex] < initialValue)
                        {
                            double remainedValue = initialValue - dataPoint.YValues[DefaultSeriesIndex];
                            dataPoint.YValues[DefaultYValuesIndex] += Math.Min(remainedValue, step);
                            pieChart.Refresh();
                        }
                    },
                    () => previousDataPointObservable?.RunAsync(CancellationToken.None));
        }

        // public void Initialize(Chart pieChart, IDictionary<string, float> data)
        // {
        //    float step = data.Values.Sum() / AnimationStepCount;
        //    pieChart.Series[DefaultSeriesIndex].Points.DataBindXY(data.Keys, data.Values);
        //    Timer previousTimer = null;
        //    foreach (DataPoint dataPoint in pieChart.Series[DefaultSeriesIndex].Points)
        //    {
        //        double initialValue = data[dataPoint.AxisLabel];
        //        dataPoint.YValues[DefaultYValuesIndex] = 0;
        //        previousTimer = CreateTimer(pieChart, dataPoint, initialValue, step, previousTimer);
        //    }
        //    previousTimer?.Start();
        // }

        // private static Timer CreateTimer(Chart pieChart, DataPoint dataPoint, double initialValue, float step, Timer previousTimer)
        // {
        //    var timer = new Timer { Interval = 1 };
        //    timer.Tick += (o, eventArgs) =>
        //    {
        //        if (dataPoint.YValues[DefaultYValuesIndex] < initialValue)
        //        {
        //            double remainedValue = initialValue - dataPoint.YValues[DefaultYValuesIndex];
        //            dataPoint.YValues[DefaultYValuesIndex] += Math.Min(remainedValue, step);
        //            pieChart.Refresh();
        //        }
        //        else
        //        {
        //            timer.Stop();
        //            previousTimer?.Start();
        //        }
        //    };
        //    return timer;
        // }
    }
}