using System.ComponentModel.DataAnnotations;

namespace Asp.netCoreDemo.Entities
{
    public class AddCustomerRequest
    {
		public string? CustomerName { get; set; }

		public string? Phone { get; set; }
		public string? Address { get; set; }
	}
}
