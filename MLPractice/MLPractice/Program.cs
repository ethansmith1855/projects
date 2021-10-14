using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using MLPracticeML.Model;

namespace MLPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load tge nodel
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load
            // Add input data
            var input = new ModelInput();
            input.SentimentText = "This is rudes";

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            Console.WriteLine(result.Prediction);
            Console.ReadKey();
        }
    }
}
