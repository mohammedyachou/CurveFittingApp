using CurveFittingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveFittingApp.Services
{
    public interface ICurveFittingService
    {
        Tuple<double, double> FitLinear(IEnumerable<DataPointModel> data);
        Tuple<double, double> FitExponential(IEnumerable<DataPointModel> data);
        Tuple<double, double> FitPowerFunction(IEnumerable<DataPointModel> data);
    }
}
