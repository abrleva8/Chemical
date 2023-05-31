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
    /// Логика взаимодействия для ChangeMaterialWindow.xaml
    /// </summary>
    public partial class ChangeMaterialWindow : Window {
        public ChangeMaterialWindow() {
            InitializeComponent();
            ID_materialComboBox.Items.Clear();
            DataBaseWorker.GetParametersId().ForEach(material => ID_materialComboBox.Items.Add(material));
        }
    }
}
