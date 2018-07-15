using GrainInterfaces;
using System.Threading.Tasks;
using System;

namespace GrainCollection
{
    class HelloGrain : Orleans.Grain, IHello
    {
        private readonly string Key = "key1";
        public Task<string> SayHello(string msg)
        {
            return Task.FromResult(string.Format("You said {0}, I say: Hello!", msg));
        }

        public Task<string> SayHelloInReverse(string text)
        {
            var r = ReverseString(text);
            //RequestContext.Set(Key, r);
            return Task.FromResult($"Reverse: " + r);
        }

        public Task<string> FromContext()
        {
           // var val = RequestContext.Get(Key) ?? "No Key";

            return Task.FromResult(Key);
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
