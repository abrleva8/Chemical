﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Don_tKnowHowToNameThis {
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        public AdminWindow() {
            InitializeComponent();
            GetMaterialsFromBd();
        }

        private void GetMaterialsFromBd() {
            var materialsInfo = DataBaseWorker.GetMaterialsInfo();
            
            var column = new DataGridTextColumn {
                Header = "Тип материала",
                Binding = new Binding("materialType")
            };
            MaterialsDataGrid.Columns.Add(column);
 
            column = new()  {
                Header = "Плотность",
                Binding = new Binding("value")
            };
            MaterialsDataGrid.Columns.Add(column);
            
            column = new()  {
                Header = "Единица измерения",
                Binding = new Binding("unit")
            };
            MaterialsDataGrid.Columns.Add(column);
            
            
            
            foreach (var material in materialsInfo) {
                    MaterialsDataGrid.Items.Add(new DataBaseWorker.MaterialInfo(material.materialType, material.value, material.unit));
            }
        }

        // record Test (
        //     int number,
        //     string xxx
        // );
        // // TODO: поменять запрос, чтобы были видны параметры
        // private void GetMaterialsFromBd() {
        //     var materials = DataBaseWorker.GetMaterials();
        //     var column = new DataGridTextColumn {
        //         Header = "Номер",
        //         Binding = new Binding("number")
        //     };
        //     MaterialsDataGrid.Columns.Add(column);
        //     column = new()
        //     {
        //         Header = "Материал",
        //         Binding = new Binding("xxx")
        //     };
        //     MaterialsDataGrid.Columns.Add(column);
        //     var number = 1;
        //     foreach (var material in materials) {
        //         MaterialsDataGrid.Items.Add(new Test(number++, material));
        //     }
        //     // MaterialsDataGrid.ItemsSource = 
        // }
    }
}
