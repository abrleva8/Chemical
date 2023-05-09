using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Don_tKnowHowToNameThis {
    public class Plot {
        public ObservableCollection<ISeries> Series { get; }
        public Axis[] XAxes { get; }
        public Axis[] YAxes { get; }
        
        public DrawMarginFrame Frame { get; set; } = new() {
            Stroke = new SolidColorPaint {
                Color = new (0, 0, 0)
            }
        };
        public Plot(IReadOnlyList<double>? x, IReadOnlyList<double>? y, string yAxisTitle, string serieName) {
            ObservableCollection<ObservablePoint> values = new();
            Series = new();
            LineSeries<ObservablePoint> lineSeries = new();
            for (var i = 0; i < x!.Count; i++) {
                values.Add(new(Math.Round(x[i],2), Math.Round(y![i], 2)));
            }
            XAxes = new Axis[] {
                new() {
                    Name = "Координата по длине канала, м"
                }
            };

            YAxes = new Axis[] {
                new() {
                    Name = yAxisTitle
                }
            };
            lineSeries.Values = values;
            lineSeries.Fill = null;
            lineSeries.GeometrySize = 3;
            lineSeries.Name = serieName;
            lineSeries.TooltipLabelFormatter = (chartPoint) => $"{YAxes[0].Name}: {chartPoint.PrimaryValue}, {XAxes[0].Name}: {chartPoint.SecondaryValue}";
            Series.Add(lineSeries);
        }
    }
}