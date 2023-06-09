using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities.Base
{
    public class Country : BaseEntity
    {
        public long CountryId { get; set; }
        public string Name { get; set; }
        public string CodeIso2 { get; set; }
        public string CodeIso3 { get; set; }
    }
}
