using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Don_tKnowHowToNameThis {
    internal class ExcelWorker {
        private string _path { get; set; }
        List<double>? zCoords;
        List<double>? temperature;
        List<double>? viscosity;
        List<double>? q;
        Calc _calc = new();

/*
        public ExcelWorker(Calc calc, string fileName) {
            _path = fileName;
            _calc = calc;
        }
*/

        public ExcelWorker(List<double>? zCoord, List<double>? temperature, List<double>? viscosity, List<double>? q, string fileName) {
            zCoords = zCoord;
            this.temperature = temperature;
            this.viscosity = viscosity;
            this.q = q;
            _path = fileName;
        }

        public void SaveToExel() {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage()) {
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                FileInfo file = new FileInfo(_path);
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(file.Name);

                int rowAndColumn = 1;
                worksheet.Cells[rowAndColumn, rowAndColumn].Value = "Координата по длине канала, м";
                worksheet.Cells[rowAndColumn, rowAndColumn + 1].Value = "Температура, °С";
                worksheet.Cells[rowAndColumn, rowAndColumn + 2].Value = "Вязкость, Па*с";
                for (int i = 0; i < zCoords.Count; ++i) {
                    worksheet.Cells[rowAndColumn + i + 1, rowAndColumn].Value = zCoords[i];
                    worksheet.Cells[rowAndColumn + i + 1, rowAndColumn + 1].Value = temperature[i];
                    worksheet.Cells[rowAndColumn + i + 1, rowAndColumn + 2].Value = viscosity[i];
                    worksheet.Cells[rowAndColumn + i + 1, rowAndColumn + 3].Value = q[i];
                }

                int r = rowAndColumn;
                int c = rowAndColumn + 5;
                Dictionary<string, object> inputDatas = new()
                {
                    { "Входные данные",                                                     "" },
                    { "Тип материала",                                                      "" },
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
                    { "Температура продукта, °С",                                           temperature[^1] },
                    { "Вязкость продукта, Па*с",                                            viscosity[^1] },
                };

                foreach (var str in inputDatas) {
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
}