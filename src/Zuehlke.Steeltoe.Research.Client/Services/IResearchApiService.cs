using System.Threading.Tasks;

namespace Zuehlke.Steeltoe.Research.Client.Services
{
    public interface IResearchApiService
    {
        Task RequestApiRandomAsync(int count);
    }
}