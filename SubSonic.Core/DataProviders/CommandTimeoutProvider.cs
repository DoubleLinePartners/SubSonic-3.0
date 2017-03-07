using System.Configuration;

namespace SubSonic.DataProviders
{
    public class CommandTimeoutProvider
    {
        private readonly int _timeout;

        public CommandTimeoutProvider()
        {
            _timeout = GetTimeout();
        }

        public int Timeout
        {
            get { return _timeout; }
        }

        private int GetTimeout()
        {
            var timeoutValue = ConfigurationManager.AppSettings.Get("SubSonic.CommandTimeout");
            int timeout;
            if (int.TryParse(timeoutValue, out timeout))
                return timeout;

            const int defaultTimeout = 60;
            return defaultTimeout;
        }
    }
}
