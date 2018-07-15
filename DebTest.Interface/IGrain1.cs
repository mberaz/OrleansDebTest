using System.Threading.Tasks;
using Orleans;

namespace DebTest.Interface
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
    public interface ITestGrain : IGrainWithIntegerKey
    {
        Task<string> SayHello();

        Task<string> SayHello(string texts);
    }
}
