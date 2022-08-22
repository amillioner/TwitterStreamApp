//using System;
//using System.Collections.Generic;

//namespace TwitterStreamApp.Redis
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");
//        }
//    }
//    public class Solution
//    {
//        public IList<string> TopKFrequent(string[] words, int k)
//        {
//            var frequencyMap = new Dictionary<string, int>();
//            foreach (var word in words)
//            {
//                frequencyMap.TryGetValue(word, out int count);
//                frequencyMap[word] = count + 1;
//            }

//            var sortedWords = new SortedSet<string>(
//                frequencyMap.Keys,
//                new WordAndFrequencyComparer(frequencyMap));

//            var result = new List<string>();
//            foreach (var word in sortedWords)
//            {
//                if (result.Count == k)
//                {
//                    break;
//                }
//                result.Add(word);
//            }
//            return result;
//        }

//        private class WordAndFrequencyComparer : IComparer<string>
//        {
//            private readonly IDictionary<string, int> _frequencyMap;

//            public WordAndFrequencyComparer(IDictionary<string, int> frequencyMap)
//            {
//                _frequencyMap = frequencyMap;
//            }

//            public int Compare(string left, string right)
//            {
//                int frequencyComparison = _frequencyMap[right].CompareTo(_frequencyMap[left]);
//                if (frequencyComparison == 0)
//                {
//                    return String.Compare(left, right, StringComparison.Ordinal);
//                }
//                return frequencyComparison;
//            }
//        }
//    }
//}
