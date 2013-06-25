using System;
using System.Collections.Generic;
using Core.Models.Budget;
using System.Linq.Expressions;
using Core.Models.Authentication;

namespace Core.Services.Budget
{
    public interface IHouseHoldService
    {
        void RequestHouseHold(HouseHold houseHold);
        void ApproveHouseHoldRequest(int houseHoldId);
        void RemoveUserFromHouseHold(int houseHoldId);
        List<HouseHold> GetAllApprovedHouseHoldsByUserId(int userId);
        List<UserProfile> GetAllNotApprovedHouseHoldRequestsByUserId(int userId);
        List<UserProfile> GetAllHouseHoldMembersByUserId(int userId);
        List<UserProfile> GetUserProfilesInHouseHold(int houseHoldId);
    }
}
