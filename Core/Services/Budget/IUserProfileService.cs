using System;
using System.Collections.Generic;
using Core.Models.Budget;
using System.Linq.Expressions;
using Core.Models.Authentication;

namespace Core.Services.Budget
{
    public interface IUserProfileService
    {
        void Insert(UserProfile userProfile);
        void Update(UserProfile userProfile);
        UserProfile GetUserProfileByName(string username);
    }
}
