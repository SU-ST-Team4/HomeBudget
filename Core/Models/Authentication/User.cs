using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System;

namespace Core.Models.Authentication
{
    /// <summary>
    /// Represents an user in the system.
    /// </summary>
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage="UserName is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(64)]
        public string EncryptedPassword { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
