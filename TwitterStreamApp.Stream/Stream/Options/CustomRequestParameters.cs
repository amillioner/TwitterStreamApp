using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.StreamApp.Stream.Stream.Options
{
    public interface ICustomRequestOptions
    {
        List<Tuple<string, string>> CustomQueryOptions { get; }

        string Get { get; }

        void Add(string name, string value);

        void Clear();
    }

    public class CustomRequestOptions : ICustomRequestOptions
    {
        public CustomRequestOptions()
        {
            CustomQueryOptions = new List<Tuple<string, string>>();
        }

        public CustomRequestOptions(ICustomRequestOptions parameters)
        {
            if (parameters?.CustomQueryOptions == null)
            {
                CustomQueryOptions = new List<Tuple<string, string>>();
                return;
            }

            CustomQueryOptions = parameters.CustomQueryOptions;
        }

        public void Add(string name, string value)
        {
            CustomQueryOptions.Add(new Tuple<string, string>(name, value));
        }

        public void Clear()
        {
            CustomQueryOptions.Clear();
        }

        public List<Tuple<string, string>> CustomQueryOptions { get; }

        public string Get
        {
            get
            {
                if (CustomQueryOptions.Count == 0)
                {
                    return string.Empty;
                }

                var queryOptions = new StringBuilder($"{CustomQueryOptions[0].Item1}={CustomQueryOptions[0].Item2}");

                for (int i = 1; i < CustomQueryOptions.Count; ++i)
                {
                    queryOptions.Append($"&{CustomQueryOptions[i].Item1}={CustomQueryOptions[i].Item2}");
                }

                return queryOptions.ToString();
            }
        }
    }
}