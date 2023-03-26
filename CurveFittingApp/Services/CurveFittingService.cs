using CurveFittingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;


namespace CurveFittingApp.Services
{
    public class CurveFittingService : ICurveFittingService
    {
        public Tuple<double, double> FitLinear(IEnumerable<DataPointModel> data)
        {
            var xData = data.Select(p => p.X).ToArray();
            var yData = data.Select(p => p.Y).ToArray();
            (double A, double B) lineFit = Fit.Line(xData, yData);
            return new Tuple<double, double>(lineFit.A, lineFit.B);
        }

        public Tuple<double, double> FitExponential(IEnumerable<DataPointModel> data)
        {
            var xData = data.Select(p => p.X).ToArray();
            var yData = data.Select(p => Math.Log(p.Y)).ToArray();
            (double A, double B) lineFit = Fit.Line(xData, yData);
            return new Tuple<double, double>(Math.Exp(lineFit.A), lineFit.B);
        }

        public Tuple<double, double> FitPowerFunction(IEnumerable<DataPointModel> data)
        {
            var xData = data.Select(p => Math.Log(p.X)).ToArray();
            var yData = data.Select(p => Math.Log(p.Y)).ToArray();
            (double A, double B) lineFit = Fit.Line(xData, yData);
            return new Tuple<double, double>(Math.Exp(lineFit.A), lineFit.B);
        }
    }

}
