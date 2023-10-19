using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumingWebApi.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        [DisplayName("CustomerName")]
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }

        public string? Address { get; set; }
    }   
}
