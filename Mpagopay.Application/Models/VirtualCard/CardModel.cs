using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Models.VirtualCard
{
    public class CardModel
    {
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
        public string Expires { get; set; } = string.Empty;
    }
}
