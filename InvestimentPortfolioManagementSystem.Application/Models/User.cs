using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestimentPortfolioManagementSystem.Application.Models.Enums;

namespace InvestimentPortfolioManagementSystem.Application.Models
{
    public class User : Entity
    {
        public User()
        {
            RegistrationDate = DateTime.Now;
            IsActive = true;
        }

        [Required]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]

        public UserTypeEnum UserType { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        /* EF Relations */
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
