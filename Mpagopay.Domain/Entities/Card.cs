using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities
{
    public class Card : BaseEntity
    {   
        public long CardId { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(19), Required]
        public string Number { get; set; } = string.Empty;
        [MaxLength(3), Required]
        public string Cvv { get; set; } = string.Empty;
        [MaxLength(7), Required]
        public string Expires { get; set; } = string.Empty;
    }
}
