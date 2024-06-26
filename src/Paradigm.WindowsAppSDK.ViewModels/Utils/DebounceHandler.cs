namespace Paradigm.WindowsAppSDK.ViewModels.Utils
{
    public sealed class DebounceHandler
    {
        #region Singleton

        /// <summary>
        /// The internal instance
        /// </summary>
        private static readonly Lazy<DebounceHandler> InternalInstance = new Lazy<DebounceHandler>(() => new DebounceHandler(), true);

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DebounceHandler Instance => InternalInstance.Value;

        #endregion

        #region Properties

        /// <summary>
        /// The timers dictionary
        /// </summary>
        private readonly IDictionary<string, System.Timers.Timer> _timersDictionary;

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="DebounceHandler"/> class from being created.
        /// </summary>
        private DebounceHandler()
        {
            _timersDictionary = new Dictionary<string, System.Timers.Timer>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Debounces the provided action.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <param name="operationId">The operation identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="intervalMilliseconds">The interval milliseconds.</param>
        public void Debounce(object consumer, string operationId, Action action, double intervalMilliseconds)
        {
            var key = GetDebouncerKey(consumer, operationId);

            System.Timers.Timer debounceTimer;

            if (_timersDictionary.ContainsKey(key))
            {
                debounceTimer = _timersDictionary[key];
                debounceTimer.Dispose();
            }

            debounceTimer = new System.Timers.Timer(intervalMilliseconds);
            debounceTimer.AutoReset = false;
            debounceTimer.Elapsed += (s, e) =>
            {
                try
                {
                    action();
                }
                finally
                {
                    debounceTimer.Dispose();
                }
            };

            _timersDictionary[key] = debounceTimer;
            debounceTimer.Start();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the debouncer key.
        /// </summary>
        /// <param name="consumer">The consumer.</param>
        /// <param name="operationId">The operation identifier.</param>
        /// <returns></returns>
        private string GetDebouncerKey(object consumer, string operationId) => $"{consumer.GetHashCode()}-{operationId}";

        #endregion
    }
}