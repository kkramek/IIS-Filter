using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    class SVMController
    {
        public void Train()
        {
            SVMDataManager data = new SVMDataManager();

            var problemBuilder = new SVMProblemBuilder();
            var problem = problemBuilder.CreateProblem(data.RequestText, data.ClassValue, data.Vocabulary.ToList());

            const double C = 0.5;
            var model = new C_SVC(problem, KernelHelper.LinearKernel(), C); // Train is called automatically here
            var accuracy = model.GetCrossValidationAccuracy(100);

            Console.Clear();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Accuracy of the model is {0:P}", accuracy);

            model.Export(string.Format(@"C:\Users\kramek\Desktop\WAF\Result\model_{0}_accuracy.model", accuracy));
            System.IO.File.WriteAllLines(string.Format(@"C:\Users\kramek\Desktop\WAF\Result\model_{0}_vocabulary.txt", accuracy), data.Vocabulary);

        }

        public bool Classyfi(string request)
        {
            SVMDataManager data = new SVMDataManager(); 

            var newX = SVMProblemBuilder.CreateNode(request, data.Vocabulary);
            //var predictedYProb = model.PredictProbabilities(newX);
            var predictedY = model.Predict(newX);

            Console.WriteLine("The prediction is {0}. Time is {1}.", _predictionDictionary[(int)predictedY], stopWatch.ElapsedMilliseconds);
            Console.WriteLine(new string('=', 50));

            return true;
        }
    }
}
