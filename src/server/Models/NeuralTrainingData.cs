using System.Collections.Generic;

namespace Models
{
    public class NeuralTrainingData
    {
        public IEnumerable<List<int>> xs { get; set; }
        public IEnumerable<int> ys { get; set; }
    }
}