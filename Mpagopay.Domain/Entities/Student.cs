using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities
{
    public class Student : Person
    {
        public long StudentId { get; set; }
        public string SchoolName { get; set; } 
    }
}
