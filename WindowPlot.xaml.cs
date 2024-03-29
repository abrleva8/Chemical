﻿using K4os.Compression.LZ4.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Don_tKnowHowToNameThis {
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WindowPlot : Window {


        //public SeriesCollection SeriesCollection { get; set; }
        public String[] Labels { get; set; }

        public WindowPlot(List<double>? zCoord, List<double>? temperature, List<double>? viscocity, string effic, string tem, string vis, TimeSpan timeSpan, long memoryUsed) {
            InitializeComponent();
            eff.Content = effic;
            T.Content = tem;
            visc.Content = vis;
            time.Content = Math.Round(timeSpan.TotalMilliseconds, 3).ToString(CultureInfo.CurrentCulture) ;
            mem.Content = memoryUsed;
            Plot temperatureChart = new Plot(zCoord, temperature, "Температура, °C", "Температура");
            test.DataContext = temperatureChart;

            Plot viscosityChart = new Plot(zCoord, viscocity, "Вязкость, Па * с", "Вязкость");
            test2.DataContext = viscosityChart;
            /*SeriesCollection = new SeriesCollection {
                new LineSeries {
                    Title = "Значение вязкости",
                    Values = new ChartValues<double>(viscocity),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,

                }

            };




            Labels = new String[zCoord.ToArray().Length];
            for (int i = 0; i < Labels.Length; i++) {
                Labels[i] = zCoord[i].ToString();
            }

            //modifying any series values will also animate and update the chart

            DataContext = this;*/
        }

        
    }
}
