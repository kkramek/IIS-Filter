// Class Library .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace Filter
{
    class WafModule : IHttpModule
    {
        public delegate void WafEventHandler(Object s, EventArgs e);
        private WafEventHandler _wafEventHandler = null;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(OnBeginRequest);
        }

        public void OnBeginRequest(Object sender, EventArgs e)
        {
            if(sender != null && sender is HttpApplication)
            {
                var request = (sender as HttpApplication).Request;
                //var requestParamsString = request.Params.ToString();

                AnalyzerHandler analyzer = new AnalyzerHandler(request);
                
                if(analyzer.IsRequestClean())
                {

                } else
                {
                    throw new HttpException(404, "File Not Found");
                }

            }

            if (_wafEventHandler != null)
                _wafEventHandler(this, null);
        }

        public event WafEventHandler WafEvent
        {
            add { _wafEventHandler += value; }
            remove { _wafEventHandler -= value; }
        }
    }

}
