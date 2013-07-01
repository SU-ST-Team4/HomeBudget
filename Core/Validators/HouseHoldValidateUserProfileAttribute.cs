using Core.Services.Budget;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.Helpers.Validators
{
    public class HouseHoldValidateUserProfileAttribute : ValidationAttribute
    {
        IHouseHoldService _houseHoldService;
        IUserProfileService _userProfileService;
        public HouseHoldValidateUserProfileAttribute(IHouseHoldService houseHoldService, IUserProfileService userProfileService)
        {
            _houseHoldService = houseHoldService;
            _userProfileService = userProfileService;
        }

        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            return null;
        }
    }
}