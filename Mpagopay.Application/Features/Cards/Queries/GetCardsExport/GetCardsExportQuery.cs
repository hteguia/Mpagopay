using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Cards.Queries.GetCardsExport
{
    public class GetCardsExportQuery : IRequest<CardExportFileVm>
    {
    }
}
