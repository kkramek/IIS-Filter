using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Filter
{
    class SVMDataManager
    {
        private string negativeDataSetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\NegativeDataSet.txt");
        private string positiveDataSetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\PositiveDataSet.txt");

        private List<string> requestText;
        private double[] classValue;
        private List<string> vocabulary;

        public List<string> RequestText
        {
            get { return requestText; }
            set { requestText = value; }
        }
        public double[] ClassValue
        {
            get { return classValue; }
            set { classValue = value; }
        }

        public List<string> Vocabulary
        {
            get { return vocabulary; }
            set { vocabulary = value; }
        }

        public SVMDataManager()
        {
            int negativeRowNumber;
            int positiveRowNumber;

            this.requestText = ReadDataSetFromFile(this.negativeDataSetPath).ToList();
            negativeRowNumber = this.requestText.Count;
            this.requestText.AddRange(ReadDataSetFromFile(this.positiveDataSetPath));
            positiveRowNumber = this.requestText.Count - negativeRowNumber;

            SetClassValues(negativeRowNumber, positiveRowNumber);

            this.vocabulary = requestText.SelectMany(GetWords).Distinct().OrderBy(word => word).ToList();

        }

        public void SetClassValues(int negativeRowNumber, int positiveRowNumber)
        {
            this.classValue = new double[negativeRowNumber + positiveRowNumber];


            for (int i = 0; i < negativeRowNumber + 1; ++i)
            {
                this.classValue[i] = -1;
            }

            for (int i = negativeRowNumber + 1; i < negativeRowNumber + positiveRowNumber; ++i)
            {
                this.classValue[i] = 1;
            }

        }

        public IEnumerable<String> ReadDataSetFromFile(string path)
        {
            return File.ReadLines(path);
        }

        private static IEnumerable<string> GetWords(string requestText)
        {
            string result = requestText; //SeparateNonAlphanumeric(requestText);//

            return result.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string SeparateNonAlphanumeric(string line)
        {
            string result = "";

            for (int i = 0; i < line.Length; i++)
            {
                if (Char.IsLetter(line[i]) || Char.IsNumber(line[i]))
                {
                    result += line[i];
                }
                else
                {
                    result += ' ';
                    result += line[i];
                    result += ' ';
                }

            }

            return result;
        }
    }
}
