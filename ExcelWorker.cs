using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Don_tKnowHowToNameThis {
    internal class ExcelWorker {
        private string Path { get; }
        private readonly List<double>? _zCoords;
        private readonly List<double>? _temperature;
        private readonly List<double>? _viscosity;
        private readonly List<double>? _q;
        private string _currentMaterial;
        private readonly Calc _calc = new();

        // public ExcelWorker(List<double>? zCoord, List<double>? temperature, List<double>? viscosity, List<double>? q, string fileName) {
        //     _zCoords = zCoord;
        //     _temperature = temperature;
        //     _viscosity = viscosity;
        //     _q = q;
        //     Path = fileName;
        // }

        public ExcelWorker(List<double>? zCoords, List<double>? temperature, List<double>? viscosity, List<double>? q, string path, string currentMaterial) {
            _zCoords = zCoords;
            _temperature = temperature;
            _viscosity = viscosity;
            _q = q;
            _currentMaterial = currentMaterial;
            Path = path;
        }

        public void SaveToExel() {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var excelPackage = new ExcelPackage();
            excelPackage.Workbook.Properties.Created = DateTime.Now;

            var file = new FileInfo(Path);
            var worksheet = excelPackage.Workbook.Worksheets.Add(file.Name);

            const int rowAndColumn = 1;
            worksheet.Cells[rowAndColumn, rowAndColumn].Value = "Координата по длине канала, м";
            worksheet.Cells[rowAndColumn, rowAndColumn + 1].Value = "Температура, °С";
            worksheet.Cells[rowAndColumn, rowAndColumn + 2].Value = "Вязкость, Па*с";
            for (var i = 0; i < _zCoords!.Count; ++i) {
                worksheet.Cells[rowAndColumn + i + 1, rowAndColumn].Value = _zCoords[i];
                worksheet.Cells[rowAndColumn + i + 1, rowAndColumn + 1].Value = _temperature![i];
                worksheet.Cells[rowAndColumn + i + 1, rowAndColumn + 2].Value = _viscosity![i];
                worksheet.Cells[rowAndColumn + i + 1, rowAndColumn + 3].Value = _q![i];
            }

            var r = rowAndColumn;
            const int c = rowAndColumn + 5;
            // TODO: поле тип материала взять из интерфейса
            Dictionary<string, object> inputData = new()
            {
                { "Входные данные",                                                     "" },
                { "Тип материала",                                                      _currentMaterial },
                { "", ""},
                { "Геометрические параметры канала:",                                   "" },
                { "Ширина, м",                                                          _calc.W },
                { "Глубина, м",                                                         _calc.H },
                { "Длина, м",                                                           _calc.L },
                { " ", ""},
                { "Параметры свойств материала:",                                       ""},
                { "Плотность, кг/м^3",                                                  Calc.R },
                { "Удельная теплоёмкость, Дж/(кг*°С)",                                  _calc.C },
                { "Температура плавления, °С",                                          _calc.T0 },
                { "  ", ""},
                { "Режимные параметры процесса:",                                       "" },
                { "Скорость крышки, м/с",                                               _calc.Vu },
                { "Температура крышки, °С",                                             _calc.Tu },
                { "   ", ""},
                { "Параметры метода решения уравнений модели:",                         "" },
                { "Шаг расчета по длине канала, м",                                     _calc.Step },
                { "    ", ""},
                { "Эмпирические коэффициенты математической модели:",                   "" },
                { "Коэффициент консистенции при температуре приведения, Па*с^n",        _calc.Mu0 },
                { "Энергия активации вязкого течения материала, Дж/моль",                  _calc.Ea },
                { "Температура приведения, °С",                                         _calc.Tr },
                { "Индекс течения",                                                     _calc.N },
                { "Коэффициент теплоотдачи от крышки канала к материалу, Вт /(м^2*°С)", _calc.AlphaU },
                { "     ", ""},
                { "      ", ""},
                { "Критериальные показатели процесса:",                                 "" },
                { "Производительность, кг/ч",                                           _calc.Q },
                { "Температура продукта, °С",                                           _temperature![^1] },
                { "Вязкость продукта, Па*с",                                            _viscosity![^1] },
            };

            foreach (var str in inputData) {
                worksheet.Cells[r, c].Value = str.Key;
                worksheet.Cells[r++, c + 1].Value = str.Value;
            }

            try {
                excelPackage.SaveAs(file);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Сохранение произошло успешно!");
        }
    }
}