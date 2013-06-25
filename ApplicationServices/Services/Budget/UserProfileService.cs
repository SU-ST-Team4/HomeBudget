using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data;
using Core.Models.Budget;
using Core.Services.Budget;
using System.Linq.Expressions;
using Core.Models.Authentication;

namespace ApplicationServices.Services.Budget
{
    /// <summary>
    /// A class which manipulates user profiles.
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        #region Fields

        private readonly IGenericRepository<UserProfile> _userProfileRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userProfileRepository"></param>
        public UserProfileService(IGenericRepository<UserProfile> userProfileRepository)
        {
            if (userProfileRepository == null)
            {
                throw new ArgumentNullException("userProfileRepository");
            }

            _userProfileRepository = userProfileRepository;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Inserts new user.
        /// </summary>
        /// <param name="userProfile"></param>
        public void Insert(UserProfile userProfile)
        {
            _userProfileRepository.Insert(userProfile);
            _userProfileRepository.SaveChanges();
        }
        /// <summary>
        /// Updates new user.
        /// </summary>
        /// <param name="userProfile"></param>
        public void Update(UserProfile userProfile)
        {
            _userProfileRepository.Update(userProfile);
            _userProfileRepository.SaveChanges();
        }
        /// <summary>
        /// Gets User profile by user name.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserProfile GetUserProfileByName(string username)
        {
            IEnumerable<UserProfile> userProfiles = _userProfileRepository.Get(u => u.UserName == username);
            return (userProfiles.Count() > 0) ? userProfiles.First() : null;
        }

        #endregion Public Methods
    }
}
