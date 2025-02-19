using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllMembersAsync();
        Task<ApplicationUser> GetMemberByIdAsync(string id);
    }
}
