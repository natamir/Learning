using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using Sample.Wpf.Extensibility.Initializers;

namespace Sample.Wpf.Initializers
{
    internal class BarChartInitializer : IBarChartInitializer
    {
        private const int DefaultYValuesIndex = 0;
        private const int DefaultSeriesIndex = 0;
        private const int AnimationStepCount = 5;

        public void Initialize(Chart barChart, IDictionary<string, float> data)
        {
            float step = data.Values.Max() / AnimationStepCount;
            barChart.Series[DefaultSeriesIndex].Points.DataBindXY(data.Keys, data.Values);
            barChart.Series[DefaultSeriesIndex].Points
                .ToObservable()
                .Do(dataPoint =>
                {
                    double initialValue = data[dataPoint.AxisLabel];
                    dataPoint.YValues[DefaultYValuesIndex] = 0;
                    barChart.Refresh();
                    Observable.Interval(TimeSpan.FromMilliseconds(1))
                        .Take(AnimationStepCount)
                        .ObserveOn(SynchronizationContext.Current)
                        .Subscribe(timer =>
                        {
                            if (dataPoint.YValues[DefaultYValuesIndex] < initialValue)
                            {
                                double remainedValue = initialValue - dataPoint.YValues[DefaultSeriesIndex];
                                dataPoint.YValues[DefaultYValuesIndex] += Math.Min(remainedValue, step);
                                barChart.Refresh();
                            }
                        });
                })
                .RunAsync(CancellationToken.None);
        }

        // public void Initialize(Chart barChart, IDictionary<string, float> data)
        // {
        //    float step = data.Values.Max() / AnimationStepCount;
        //    barChart.Series[DefaultSeriesIndex].Points.DataBindXY(data.Keys, data.Values);
        //    foreach (DataPoint dataPoint in barChart.Series[DefaultSeriesIndex].Points)
        //    {
        //        double initialValue = data[dataPoint.AxisLabel];
        //        dataPoint.YValues[DefaultYValuesIndex] = 0;
        //        barChart.Refresh();
        //        var timer = new Timer { Interval = 1 };
        //        timer.Tick += (o, eventArgs) =>
        //        {
        //            if (dataPoint.YValues[DefaultYValuesIndex] < initialValue)
        //            {
        //                double remainedValue = initialValue - dataPoint.YValues[DefaultSeriesIndex];
        //                dataPoint.YValues[DefaultYValuesIndex] += Math.Min(remainedValue, step);
        //                barChart.Refresh();
        //            }
        //            else
        //            {
        //                timer.Stop();
        //            }
        //        };
        //        timer.Start();
        //    }
        // }
    }
}