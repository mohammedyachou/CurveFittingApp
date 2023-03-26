using CurveFittingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveFittingApp.Services
{
    public interface IDataFileService
    {
        List<DataPointModel> LoadDataFromFile(string filePath);
    }
}
