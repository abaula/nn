using System;
using NeuNet.Neural.Errors;
using NeuNet.Neural.Helpers;
using NeuNet.Neural.Networks;
using NeuNet.Neural.Trainers;
using NeuNet.Neural.Networks.Abstractions;
using NeuNet.Neural.Trainers.Abstractions;

namespace NeuNet.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var numFeatures = 8;
            var numRows = 10000;

            Console.WriteLine("\nGenerating " + numRows + " artificial data items with " + numFeatures + " features");
            var allData = MakeAllData(numFeatures, numRows);

            Console.WriteLine("Creating train (80%) and test (20%) matrices");
            MakeTrainTest(allData, out double[][] trainData, out double[][] testData);

            Console.WriteLine("\nTraining data: \n");
            ShowData(trainData, 3, 2, true);

            Console.WriteLine("\nTest data: \n");
            ShowData(testData, 3, 2, true);

            var network = new LogisticRegressionNetwork(numFeatures);
            var errorFunc = new LogisticRegressionErrorFunc(network);
            var trainer = new LogisticStochasticGradientDescentTrainer(network, errorFunc)
            {
                LearningRate = 0.01,
                Data = trainData
            };

            var trainOptimizer = new LimitTrainOptimizer(trainer)
            {
                MaxEpoch = 1000,
                MinError = double.Epsilon,
                NotifyOnEachNthEpoch = 100
            };

            trainOptimizer.OnEpoch += TrainOptimizer_OnEpoch;
            trainOptimizer.Train();

            Console.WriteLine($"\nTraining completed at {trainOptimizer.CurrentEpoch} epoch.");

            Console.WriteLine("\nBest weights found:");
            ShowVector(network.Weights, 4, true);

            double trainAcc = Accuracy(network, trainData);
            Console.WriteLine("Prediction accuracy on training data = " +
              trainAcc.ToString("F4"));

            double testAcc = Accuracy(network, testData);
            Console.WriteLine("Prediction accuracy on test data = " +
              testAcc.ToString("F4"));

            Console.ReadKey();
        }

        private static void TrainOptimizer_OnEpoch(ITrainOptimizer trainOptimizer, ITrainer trainer)
        {
            var epochError = trainer.GetError();
            Console.Write("epoch = " + trainOptimizer.CurrentEpoch);
            Console.WriteLine("  error = " + epochError.ToString("F4"));
        }

        public static void ShowData(double[][] data, int numRows, int decimals, bool indices)
        {
            int len = data.Length.ToString().Length;
            for (int i = 0; i < numRows; ++i)
            {
                if (indices == true)
                    Console.Write("[" + i.ToString().PadLeft(len) + "]  ");
                for (int j = 0; j < data[i].Length; ++j)
                {
                    double v = data[i][j];
                    if (v >= 0.0)
                        Console.Write(" "); // '+'
                    Console.Write(v.ToString("F" + decimals) + "  ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine(". . .");
            int lastRow = data.Length - 1;
            if (indices == true)
                Console.Write("[" + lastRow.ToString().PadLeft(len) + "]  ");
            for (int j = 0; j < data[lastRow].Length; ++j)
            {
                double v = data[lastRow][j];
                if (v >= 0.0)
                    Console.Write(" "); // '+'
                Console.Write(v.ToString("F" + decimals) + "  ");
            }
            Console.WriteLine("\n");
        }

        public static double Accuracy(ILogisticRegressionNetwork network, double[][] data)
        {
            int numCorrect = 0;
            int numWrong = 0;
            int yIndex = network.NumberOfFeatures;

            for (int i = 0; i < data.Length; ++i)
            {
                var computed = network.Calculate(data[i]);

                int target = data[i][yIndex] > 0.5 ? 1 : 0;
                int result = computed[0] > 0.5 ? 1 : 0;

                if (result == target)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return (double)numCorrect / (numWrong + numCorrect);
        }

        static void MakeTrainTest(double[][] allData,
            out double[][] trainData, out double[][] testData)
        {
            Random rnd = new Random();
            int totRows = allData.Length;
            int numTrainRows = (int)(totRows * 0.80); // 80% hard-coded
            int numTestRows = totRows - numTrainRows;
            trainData = new double[numTrainRows][];
            testData = new double[numTestRows][];

            double[][] copy = new double[allData.Length][]; // ref copy of all data
            for (int i = 0; i < copy.Length; ++i)
                copy[i] = allData[i];

            for (int i = 0; i < copy.Length; ++i) // scramble order
            {
                int r = rnd.Next(i, copy.Length); // use Fisher-Yates
                double[] tmp = copy[r];
                copy[r] = copy[i];
                copy[i] = tmp;
            }
            for (int i = 0; i < numTrainRows; ++i)
                trainData[i] = copy[i];

            for (int i = 0; i < numTestRows; ++i)
                testData[i] = copy[i + numTrainRows];
        } // MakeTrainTest


        static double[][] MakeAllData(int numFeatures, int numRows)
        {
            Random rnd = new Random();
            double[] weights = new double[numFeatures + 1]; // inc. b0

            for (int i = 0; i < weights.Length; i++)
                weights[i] = 20 * rnd.NextDouble() - 10;

            double[][] result = new double[numRows][]; // allocate matrix

            for (int i = 0; i < numRows; i++)
                result[i] = new double[numFeatures + 1]; // Y in last column

            for (int i = 0; i < numRows; i++) // for each row
            {
                double z = weights[0]; // the b0
                for (int j = 0; j < numFeatures; j++) // each feature / column except last
                {
                    double x = 20 * rnd.NextDouble() - 10;
                    result[i][j] = x; // store x
                    double wx = x * weights[j + 1]; // weight * x
                    z += wx; // accumulate to get Y
                }

                var y = MathHelper.Sigmoid(z);

                if (y > 0.5)  // slight bias towards 0
                    result[i][numFeatures] = 1.0; // store y in last column
                else
                    result[i][numFeatures] = 0.0;
            }

            Console.WriteLine("Data generation weights:");
            ShowVector(weights, 4, true);

            return result;
        } // MakeAllData

        static void ShowVector(double[] vector, int decimals, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
                Console.Write(vector[i].ToString("F" + decimals) + " ");
            Console.WriteLine("");
            if (newLine == true)
                Console.WriteLine("");
        }
    }
}
