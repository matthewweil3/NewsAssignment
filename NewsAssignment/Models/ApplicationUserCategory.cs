using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAssignment.Models
{
    public class ApplicationUserCategory
    {
        public int ID { get; set; }

      
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
