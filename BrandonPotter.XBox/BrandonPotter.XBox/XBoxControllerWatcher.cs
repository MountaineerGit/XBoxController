using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandonPotter.XBox
{
    public delegate void ControllerEvent(XBoxController controller);
    public class XBoxControllerWatcher : IDisposable
    {
        public event ControllerEvent ControllerConnected;
        public event ControllerEvent ControllerDisconnected;

        public event ControllerEvent ControllerStateChange;

        Dictionary<int, bool> _connectionStates = new Dictionary<int, bool>();
        private bool _connection_state = false;
        private bool _stopWatching = false;
        public XBoxControllerWatcher()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o => WatcherLoop());
        }

        private void WatcherLoop()
        {
            System.Threading.Thread.Sleep(10);

            while (!_stopWatching)
            {
                try
                {
                    DetectStates();
                }
                catch { }

                System.Threading.Thread.Sleep(50);
            }
        }

        private void DetectStates()
        {
            var xbc = XBoxController.GetController();
            if(xbc != null)
            {
                if (!_connection_state)
                {
                    _connection_state = xbc.IsConnected;
                    if (xbc.IsConnected)
                    {
                        FireConnected(xbc);
                    }
                }

                if (_connection_state == false && xbc.IsConnected)
                {
                    _connection_state = true;
                    FireConnected(xbc);
                }
                else if (_connection_state == true && xbc.IsConnected == false)
                {
                    _connection_state = false;
                    FireDisconnected(xbc);
                }

                if (xbc.statechange)
                {
                    //Console.WriteLine(xbc.ToString());
                    FireStateChange(xbc);
                }
            }
        }

        private void FireConnected(XBoxController xbc)
        {
            if (ControllerConnected != null)
            {
                ControllerConnected(xbc);
            }
        }

        private void FireDisconnected(XBoxController xbc)
        {
            if (ControllerDisconnected != null)
            {
                ControllerDisconnected(xbc);
            }
        }

        private void FireStateChange(XBoxController xbc)
        {
            ControllerStateChange?.Invoke(xbc);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _stopWatching = true;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~XBoxControllerWatcher() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
