using RehearsalHub.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Users
{
    public interface IUserService
    {
        Task<UserProfileViewModel?> GetUserProfileAsync(string userId);
    }
}
