using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Neunet.Images
{
    [Serializable]
    internal class HistoryDictionary : Dictionary<object, List<float>>
    {

        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Number of categories (vertical).
        /// </summary>
        public int CategoryCount { get { return Count; } }

        /// <summary>
        /// Number of ticks (horizontal).
        /// </summary>
        public int TickCount { get; private set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <param name="key">The iteration engine.</param>
        /// <param name="t">The tick.</param>
        /// <returns>The value.</returns>
        public float this[object key, int t]
        {
            get { return this[key][t]; }
            set { this[key][t] = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public HistoryDictionary()
        {
            TickCount = 0;
        }

        protected HistoryDictionary(SerializationInfo info, StreamingContext context): base(info, context)
        {
            throw new NotImplementedException();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            throw new NotImplementedException();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region HistoryDictionary

        public void ClearItems()
        {
            Clear();
            TickCount = 0;
        }

        /// <summary>
        /// Get the value at the last tick of category j.
        /// </summary>
        /// <param name="key">Category</param>
        /// <returns>The value.</returns>
        public float GetLastValue(object key)
        {
            int t = TickCount;
            return t > 0 ? this[key][t - 1] : float.NaN;
        }

        /// <summary>
        /// Set the value at the last tick of category j.
        /// </summary>
        /// <param name="engine">Category</param>
        /// <param name="value">The value.</param>
        public void SetLastValue(object key, float value)
        {
            int t = TickCount;
            if (t > 0) this[key][t - 1] = value;
        }

        /// <summary>
        /// Adds a new tick to the end of all categories, initializes the new tick values with NaN, and returns the index of the new tick.
        /// </summary>
        /// <returns>The index of the new tick.</returns>
        public void AddOneTick()
        {
            foreach (KeyValuePair<object, List<float>> keyValuePair in this)
            {
                List<float> currentValues = keyValuePair.Value;
                currentValues.Add(float.NaN);
            }
            TickCount++;
        }

        /// <summary>
        /// Adds a new category to the end of the list, initializes the new float array with values NaN, and returns the index of the new category.
        /// </summary>
        /// <returns>The index of the new category.</returns>
        public void AddOneCategory(object key)
        {
            List<float> newValues = new List<float>(TickCount);
            for (int t = 0; t < TickCount; t++)
                newValues.Add(float.NaN);
            Add(key, newValues);
        }

        #endregion
    }
}
