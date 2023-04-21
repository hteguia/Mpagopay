using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities
{
    public class Pricing : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
