namespace WindowsClient.Utils
{
    public static class SingleInstanceManager
    {
        private const string MutexName = "Global\\ArqanumAppMutex";

        public static bool IsFirstInstance(out Mutex mutex)
        {
            bool createdNew;
            mutex = new Mutex(true, MutexName, out createdNew);
            return createdNew;
        }
    }
}
