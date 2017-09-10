using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;



namespace CoffeeShop.Model.ModelEntity
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole,
    CustomUserClaim>
    {
        [MaxLength(256)]
        public string FullName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }
        public int? WardID { set; get; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
        UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
        [ForeignKey("WardID")]
        public virtual Ward Ward { set; get; }
        public virtual IEnumerable<MaterialLog> MaterialLog { set; get; }
        public virtual IEnumerable<Order> Orders { set; get; }
    }

    public class CustomUserRole : IdentityUserRole<int>
    {

        public CustomUserRole() : base()
        {

        }

      

    }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
        [StringLength(250)]
        public string Description { set; get; }

    }


}