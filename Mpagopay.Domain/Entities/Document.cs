using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities
{
    public class Document : BaseEntity
    {
        public DocumentType DocumentType { get; set; }
        public decimal Price { get; set; }
    }
}
