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
        public void Execute(object data) {
            _nodeServices.InvokeAsync<string>("./Node/neuralNetwork");
        }
    }
}