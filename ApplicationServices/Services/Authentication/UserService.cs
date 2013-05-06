using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Core.Services.Authentication;
using Core.Models.Authentication;
using Core.Data;

namespace ApplicationServices.Services.Authentication
{
    /// <summary>
    /// Service used for managing user's authentication related operations.
    /// </summary>
    public class UserService : IUserService
    {
        #region Static Methods

        /// <summary>
        /// Encrypt a text
        /// </summary>
        /// <param name="text">text to encrypt</param>
        /// <returns>encrypted password</returns>
        private static string EncryptPassword(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        /// <summary>
        /// Create a new password
        /// </summary>
        /// <returns>string of length of 12 with 3 non alphanumeric characters</returns>
        private static string CreatePassword()
        {
            return Membership.GeneratePassword(8, 2);
        }

        #endregion Static Methods

        #region Fields

        private readonly IGenericRepository<UserExample> _userRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IGenericRepository<UserExample> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Authenticates user's identity.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserExample AutenticateUser(string userName, string password)
        {
            UserExample user = _userRepository.Get(u => u.UserName == userName)
                                       .FirstOrDefault();

            if (user != null && EncryptPassword(password) == user.EncryptedPassword)
            {
                return user;
            }

            return null;
        }
        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserExample GetUserById(int id)
        {
            return _userRepository.GetByID(id);
        }
        /// <summary>
        /// Verify if user's entered password is the same as the existing one.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordToCheck"></param>
        /// <returns></returns>
        public bool VerifyUserPassword(int userId, string passwordToCheck)
        {
            UserExample user = _userRepository.GetByID(userId);

            if (user != null)
            {
                return EncryptPassword(passwordToCheck) == user.EncryptedPassword;
            }

            return false;
        }
        /// <summary>
        /// Change user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(int userId, string oldPassword, string newPassword)
        {
            if (VerifyUserPassword(userId, oldPassword))
            {
                UserExample user = _userRepository.GetByID(userId);

                if (user != null)
                {
                    user.EncryptedPassword = EncryptPassword(newPassword);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Returns new user's id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int RegisterUser(UserExample user)
        {
            UserExample existingUser = _userRepository.Get(u => u.UserName == user.UserName)
                                               .FirstOrDefault();

            if (existingUser == null)
            {
                int result = _userRepository.Insert(user);
                _userRepository.SaveChanges();

                return result;
            }

            return -1;
        }

        #endregion Public Methods
    }
}
