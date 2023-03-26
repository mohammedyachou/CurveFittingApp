using CurveFittingApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurveFittingApp.Services
{
    public class DataFileService : IDataFileService
    {
        public List<DataPointModel> LoadDataFromFile(string filePath)
        {
            var dataPoints = new List<DataPointModel>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length == 2 &&
                            double.TryParse(values[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double x) &&
                            double.TryParse(values[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
                        {
                            dataPoints.Add(new DataPointModel(x, y));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "File Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return dataPoints;
        }
        /* public List<DataPointModel> LoadDataFromFile(string filePath)
         {
             var dataPoints = new List<DataPointModel>();

             using (var reader = new StreamReader(filePath))
             {
                 while (!reader.EndOfStream)
                 {
                     var line = reader.ReadLine();
                     var values = line.Split(',');

                     if (values.Length != 2) throw new FormatException("Invalid data format");

                     dataPoints.Add(new DataPointModel
                     {
                         X = double.Parse(values[0]),
                         Y = double.Parse(values[1])
                     });
                 }
             }

             return dataPoints;
         }*/
    }
}
