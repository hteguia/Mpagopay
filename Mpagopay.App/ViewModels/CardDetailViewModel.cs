using System.ComponentModel.DataAnnotations;

namespace Mpagopay.App.ViewModels
{
    public class CardDetailViewModel
    {
        public long CardId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;
    }
}
