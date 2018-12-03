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
        public string Fit(NeuralTrainingData data)
        {
            var result = _nodeServices.InvokeAsync<Object>("./Node/fit", data).Result;
            return result as string;
        }

        public List<int> Predict(string savedModel, NeuralTestData data)
        {
            var result = _nodeServices.InvokeAsync<Object>("./Node/predict", savedModel, data).Result;
            List<int> listResult = JObject.FromObject(result).ToObject<List<int>>();
            return listResult;
        }
    }
}