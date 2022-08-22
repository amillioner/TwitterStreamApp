using System.Threading.Tasks;
using Twitter.StreamApp.Data.Request;

namespace Twitter.StreamApp.Stream.Stream.Interfaces
{
    public interface IFilteredStreamService : IStreamService
    {
        Task<System.IO.Stream> AddRules(AddRulesRequest addRules);
        Task<System.IO.Stream> RetrieveRules();
        Task<System.IO.Stream> DeleteRules(DeleteRulesRequest deleteRules);
    }
}