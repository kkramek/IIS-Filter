using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Filter
{
    class AnalyzerHandler : IAnalyzer
    {
        private HttpRequest request;

        public AnalyzerHandler(HttpRequest request)
        {
            this.request = request;
        }

        public bool IsRequestClean()
        {



            return true;
        }


        public HttpRequest Request
        {
            get => request;
            set => request = value;
        }
    }
}
