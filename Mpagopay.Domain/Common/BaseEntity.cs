using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Domain.Common
{
    public class BaseEntity
    {
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
