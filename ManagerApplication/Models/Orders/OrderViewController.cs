using System.ComponentModel.DataAnnotations;

namespace ManagerApplication.Mvc.Models.Orders
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please select a meal plan")]
        public string MealPlan { get; set; }
    }
}
