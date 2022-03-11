using Lazarus.Common.Model;
using System.Linq;
using System.Threading.Tasks;
namespace Lazarus.Common.Infrastructure
{
    public interface IJWTService
    {
        public string GenerateJWT(UserCredential user);
    }
}
