using System;
using Microsoft.AspNetCore.NodeServices;

namespace Services
{
    public class JavascriptExecutor
    {
        private INodeServices _nodeServices;

        public JavascriptExecutor(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }
        public Object Execute(object data) {
            return _nodeServices.InvokeAsync<Object>("./Node/neuralNetwork", data).Result;
        }
    }
}