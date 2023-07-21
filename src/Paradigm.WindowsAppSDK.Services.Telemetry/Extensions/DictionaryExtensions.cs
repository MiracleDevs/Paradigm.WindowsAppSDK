namespace Paradigm.WindowsAppSDK.Services.Telemetry.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Clones the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public static IDictionary<TKey, TValue> CloneDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source)
             where TValue : ICloneable
             where TKey : notnull
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            var cloned = new Dictionary<TKey, TValue>(source.Count);

            foreach (var entry in source)
            {
                cloned.Add(entry.Key, (TValue)entry.Value.Clone());
            }

            return cloned;
        }
    }
}
