using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Models
{
    public class Product : Entity
    {
        public Product()
        {
            ExpirationDate = DateTime.Now.AddYears(1);
            RegistrationDate = DateTime.Now;
            IsActive = true;
            IsAvailableForSell = true;
        }

        public Guid? OwnerId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Value { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsAvailableForSell { get; set; }

        /* EF Relations */
        public User Owner { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
