namespace Instrumentation.Logging
{
    public class KeyedLog
    {
        private readonly string _key;

        public string Key
        {
            get { return _key; }
        }

        protected KeyedLog(string key)
        {
            _key = key;
        }
    }
}
