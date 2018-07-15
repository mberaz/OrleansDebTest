using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string msg);

        Task<string> SayHelloInReverse(string texts);

        Task<string> FromContext();
    }
}
