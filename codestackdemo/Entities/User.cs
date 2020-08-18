using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codestackdemo.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        [NotMapped]
        public string Token { get; set; }
    }
}
