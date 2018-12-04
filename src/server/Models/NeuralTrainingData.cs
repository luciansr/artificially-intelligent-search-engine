using System.Collections.Generic;

namespace Models
{
    public class NeuralTrainingData
    {
        public IEnumerable<List<double>> xs { get; set; }
        public IEnumerable<double> ys { get; set; }
    }
}