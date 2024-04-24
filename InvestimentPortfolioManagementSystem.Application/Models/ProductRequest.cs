using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestimentPortfolioManagementSystem.Application.Models
{
    public class ProductRequest : Product
    {
        [Required(ErrorMessage = "It is Required to inform the User that is executing the operation")]
        public Guid UserOperationId { get; set; }
    }
}
