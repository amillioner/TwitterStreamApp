using System;
using System.Collections.Generic;

namespace Twitter.StreamApp.Common
{
    public class WordAndFrequencyComparer : IComparer<string>
    {
        private readonly IDictionary<string, long> _frequencyMap;

        public WordAndFrequencyComparer(IDictionary<string, long> frequencyMap)
        {
            _frequencyMap = frequencyMap;
        }

        public int Compare(string left, string right)
        {
            int frequencyComparison = _frequencyMap[right].CompareTo(_frequencyMap[left]);
            return frequencyComparison == 0 ?
                string.Compare(left, right, StringComparison.Ordinal)
                : frequencyComparison;
        }
    }
}
