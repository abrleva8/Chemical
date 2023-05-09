using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Helpers;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace Don_tKnowHowToNameThis {
    public class Plot {
        private ObservableCollection<ObservablePoint> _values;
        public ObservableCollection<ISeries> Series { get; }
        public Axis[] XAxes { get; }
        public Axis[] YAxes { get; }
        public DrawMarginFrame Frame { get; set; } = new() {
            Stroke = new SolidColorPaint {
                Color = new (0, 0, 0)
            }
        };
        public Plot(IReadOnlyList<double>? x, IReadOnlyList<double>? y, string yAxisTitle, string serieName) {
            _values = new ObservableCollection<ObservablePoint>();
            Series = new ObservableCollection<ISeries>();
            LineSeries<ObservablePoint> serie = new();
            for (int i = 0; i < x.Count; i++) {
                _values.Add(new ObservablePoint(Math.Round(x[i],2), Math.Round(y[i], 2)));
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
            serie.Values = _values;
            serie.Fill = null;
            serie.GeometrySize = 3;
            serie.Name = serieName;
            serie.TooltipLabelFormatter = (chartPoint) => $"{YAxes[0].Name}: {chartPoint.PrimaryValue}, {XAxes[0].Name}: {chartPoint.SecondaryValue}";
            Series.Add(serie);
        }
    }
}