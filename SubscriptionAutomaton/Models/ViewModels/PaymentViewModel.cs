using System.ComponentModel.DataAnnotations;

namespace SubscriptionAutomaton.Models.ViewModels;

public class PaymentViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Заполните поле")]
    [Range(16, 20)]
    public string? CardNumber;

    public string? CardName;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Заполните поле")]
    [Range(7,7)]
    public string? ExpiryDate;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Заполните поле")]
    [Range(3,3)]
    public string? Cvv;
}
