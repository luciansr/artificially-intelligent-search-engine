using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.NodeServices;
using Models;
using Newtonsoft.Json.Linq;

namespace Services
{
    public class JavascriptExecutor
    {
        private INodeServices _nodeServices;

        public JavascriptExecutor(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        public Object Execute(object data)
        {
            return _nodeServices.InvokeAsync<Object>("./Node/neuralNetwork", data).Result;
        }
        public string Fit(string query, NeuralTrainingData data)
        {
            var result = _nodeServices.InvokeAsync<string>("./Node/fit", query, data).Result;
            return result;
        }

        public List<int> Predict(string query, NeuralTestData data)
        {
            var result = _nodeServices.InvokeAsync<List<int>>("./Node/predict", query, data).Result;
            return result;
        }
    }
}