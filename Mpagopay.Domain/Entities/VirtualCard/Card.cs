using System.ComponentModel.DataAnnotations;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities.VirtualCard
{
    public class Card : BaseEntity
    {
        public long CardId { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; }
        [MaxLength(19), Required]
        public string Number { get; set; }
        [MaxLength(3), Required]
        public string Cvv { get; set; }
        [MaxLength(7), Required]
        public string Expires { get; set; }
    }
}
