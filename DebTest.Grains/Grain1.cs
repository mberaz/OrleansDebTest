using System.Threading.Tasks;
using Orleans;
using DebTest.Interface;
using System;

namespace DebTest.Grains
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class Grain1 : Grain, ITestGrain
    {
        public Task<string> SayHello()
        {
            return Task.FromResult("Hello World!");
        }

        public Task<string> SayHello(string texts)
        {
            return Task.FromResult($"Reverse: "+ ReverseString( texts) );
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
