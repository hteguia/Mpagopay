using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Features.Cards.Queries.GetCardsExport;

namespace Mpagopay.Application.Contrats.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportCardsToCsv(List<CardExportDto> cardExportDtos);
    }
}
