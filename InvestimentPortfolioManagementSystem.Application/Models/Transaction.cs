using InvestimentPortfolioManagementSystem.Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Models
{
    public class Transaction : Entity
    {
        public Transaction() 
        {
            TransactionTimeStamp = DateTime.Now;
        }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public TransactionTypeEnum TransactionType { get; set; }

        public decimal ProductValue { get; set; }

        public DateTime TransactionTimeStamp { get; set; }

        /* EF Relations */
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
