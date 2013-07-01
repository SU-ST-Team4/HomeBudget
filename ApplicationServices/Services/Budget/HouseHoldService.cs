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
    /// A class which manipulates HouseHold related operations
    /// </summary>
    public class HouseHoldService : IHouseHoldService
    {
        #region Fields

        private readonly IGenericRepository<HouseHold> _houseHoldRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="houseHoldRepository"></param>
        public HouseHoldService(IGenericRepository<HouseHold> houseHoldRepository)
        {
            if (houseHoldRepository == null)
            {
                throw new ArgumentNullException("houseHoldRepository");
            }

            _houseHoldRepository = houseHoldRepository;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Request new household.
        /// </summary>
        /// <param name="houseHold"></param>
        public void RequestHouseHold(HouseHold houseHold)
        {
            _houseHoldRepository.Insert(houseHold);
            _houseHoldRepository.SaveChanges();
        }
        /// <summary>
        /// Approves houseHold request.
        /// </summary>
        /// <param name="houseHoldId"></param>
        public void ApproveHouseHoldRequest(int houseHoldId)
        {
            HouseHold houseHold = _houseHoldRepository.GetByID(houseHoldId);
            houseHold.IsApproved = true;
            houseHold.ApproveDate = DateTime.Now;
            _houseHoldRepository.Update(houseHold);
            _houseHoldRepository.SaveChanges();
        }
        /// <summary>
        /// Remove User from HouseHold.
        /// </summary>
        /// <param name="houseHoldId"></param>
        public void RemoveUserFromHouseHold(int houseHoldId)
        {
            _houseHoldRepository.Delete(houseHoldId);
            _houseHoldRepository.SaveChanges();
        }
        /// <summary>
        /// Get All Approved households by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<HouseHold> GetAllApprovedHouseHoldsByUserId(int userId)
        {
            return _houseHoldRepository.Get(h => (h.First.UserId == userId || h.Second.UserId == userId) && 
                                                  h.IsApproved.HasValue && h.IsApproved == true)
                                       .ToList();
        }
        /// <summary>
        /// Get All members in the user's household.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserProfile> GetAllHouseHoldMembersByUserId(int userId)
        {
            List<HouseHold> houseHolds = GetAllApprovedHouseHoldsByUserId(userId);
            List<UserProfile> result = new List<UserProfile>();

            foreach (HouseHold houseHold in houseHolds)
            {
                if (houseHold.First.UserId != userId)
                {
                    result.Add(houseHold.First);
                }
                else
                {
                    result.Add(houseHold.Second);
                }
            }

            return result;

        }
        /// <summary>
        /// Het all not approved household requests for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<HouseHold> GetAllNotApprovedHouseHoldRequestsByUserId(int userId)
        {
            return _houseHoldRepository.Get(h => h.Second.UserId == userId && !h.IsApproved.HasValue)
                                       .ToList();
        }
        /// <summary>
        /// Get user profiles in a household.
        /// </summary>
        /// <param name="houseHoldId"></param>
        /// <returns></returns>
        public List<UserProfile> GetUserProfilesInHouseHold(int houseHoldId)
        {
            HouseHold houseHold = _houseHoldRepository.GetByID(houseHoldId);
            return new List<UserProfile>() { houseHold.First, houseHold.Second };
        }

        #endregion Public Methods
    }
}
