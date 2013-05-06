using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Models.Authentication;

namespace Core.Services.Authentication
{
    /// <summary>
    /// Service used for managing user's authentication related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates user's identity.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User AutenticateUser(string userName, string password);
        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);
        /// <summary>
        /// Verify if user's entered password is the same as the existing one.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordToCheck"></param>
        /// <returns></returns>
        bool VerifyUserPassword(int userId, string passwordToCheck);
        /// <summary>
        /// Change user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangeUserPassword(int userId, string oldPassword, string newPassword);
        /// <summary>
        /// Returns new user's id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int RegisterUser(User user);
    }
}
