// Class Library .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace Filter
{
    class SyncModule : IHttpModule
    {
        public delegate void MyEventHandler(Object s, EventArgs e);
        private MyEventHandler _eventHandler = null;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(OnBeginRequest);
        }

        public void OnBeginRequest(Object s, EventArgs e)
        {
            HttpApplication app = s as HttpApplication;
            app.Context.Response.Write("Hello from OnBeginRequest in custom module.<br>");
            if (_eventHandler != null)
                _eventHandler(this, null);
        }

        public event MyEventHandler MyEvent
        {
            add { _eventHandler += value; }
            remove { _eventHandler -= value; }
        }
    }

}
