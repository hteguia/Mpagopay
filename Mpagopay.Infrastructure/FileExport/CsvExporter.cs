using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Features.Cards.Queries.GetCardsExport;

namespace Mpagopay.Infrastructure.FileExport
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportCardsToCsv(List<CardExportDto> cardExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using(var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWiter = new CsvWriter(streamWriter);
                csvWiter.WriteRecord(cardExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
