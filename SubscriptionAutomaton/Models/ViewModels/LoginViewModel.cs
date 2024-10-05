using System.ComponentModel.DataAnnotations;

namespace SubscriptionAutomaton.Models.ViewModels;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите ваш логин")]
    public string? Login { get; set; }
}
