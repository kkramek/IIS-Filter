using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Filter
{
    class SVMController
    {
        private static Dictionary<int, string> _predictionDictionary = new Dictionary<int, string> { { -1, "SqlInjection" }, { 1, "SafeRequest" } };
        private double accuracy;


        public void Train()
        {
            SVMDataManager data = new SVMDataManager();

            var problemBuilder = new SVMProblemBuilder();
            var problem = problemBuilder.CreateProblem(data.RequestText, data.ClassValue, data.Vocabulary.ToList());

            const double C = 0.5;
            C_SVC model = new C_SVC(problem, KernelHelper.LinearKernel(), C); // Train is called automatically here
            accuracy = model.GetCrossValidationAccuracy(100);

            model.Export(string.Format(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"bin\model_{0}_accuracy.model", accuracy))));
            System.IO.File.WriteAllLines(string.Format(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"bin\model_{0}_vocabulary.txt", accuracy))), data.Vocabulary);

        }

        public bool Classyfi(string request)
        {
            SVMDataManager data = new SVMDataManager();
            C_SVC model = new C_SVC(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"model.model"));

            request = request.Replace("&", " ");
            request = request.Replace("+", " ");
            request = request.Replace("%20", " ");
            request = request.Replace("/", " ");
            request = request.Replace("?", " ");
            request = request.Replace("Filter", " ");
            request = request.Replace("Test.aspx", " ");

            var newX = SVMProblemBuilder.CreateNode(request, data.Vocabulary);
            //var predictedYProb = model.PredictProbabilities(newX);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var predictedY = model.Predict(newX);

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"log.txt"), string.Format("The prediction is {0}. Time is {1}.", _predictionDictionary[(int)predictedY], stopWatch.ElapsedMilliseconds));

            if(predictedY == -1)
            {
                return false;

            } else
            {
                return true;
            }
            
        }
    }
}
