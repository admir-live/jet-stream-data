using System.Buffers;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SearchValuesBenchmark
{
    [MemoryDiagnoser]
    public class SearchPerformanceBenchmark
    {
        private readonly string _simplePath = "Owner.Name.Title";
        private readonly string _longString = $"{new string('x', 1000)}0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz{new string('x', 1000)}";
        private readonly char[] _alphaNumericArray = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        private readonly SearchValues<char> _searchValuesAlphaNumeric;

        public SearchPerformanceBenchmark()
        {
            _searchValuesAlphaNumeric = SearchValues.Create(_alphaNumericArray);
        }

        [Benchmark]
        public int IndexOfFirstAlphaNumericSimple()
        {
            return _simplePath.IndexOfAny(_alphaNumericArray);
        }

        [Benchmark]
        public int IndexOfFirstAlphaNumericLong()
        {
            return _longString.IndexOfAny(_alphaNumericArray);
        }

        [Benchmark]
        public int IndexOfSearchValuesLong()
        {
            return _longString.AsSpan().IndexOfAny(_searchValuesAlphaNumeric);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SearchPerformanceBenchmark>();
        }
    }
}
