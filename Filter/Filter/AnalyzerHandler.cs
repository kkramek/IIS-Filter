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
            SVMController controler = new SVMController();

            return controler.Classyfi(request.RawUrl.ToString());
        }


        public HttpRequest Request
        {
            get => request;
            set => request = value;
        }
    }
}
