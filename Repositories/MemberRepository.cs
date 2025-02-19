using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public MemberRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllMembersAsync()
        {
            // Adjust filter as needed
            return await Task.Run(() => _userManager.Users.ToList());
        }
        public async Task<ApplicationUser> GetMemberByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID '{id}' not found.");
            }
            return user;
        }
    }
}
