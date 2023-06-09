using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities.VirtualCard;

namespace Mpagopay.Application.Features.Cards.Queries.GetCardsExport
{
    public class GetCardExportQueryHandler : IRequestHandler<GetCardsExportQuery, CardExportFileVm>
    {
        private readonly IAsyncRepository<Card> _cardRepository;
        private IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetCardExportQueryHandler(IAsyncRepository<Card> cardRepository, IMapper mapper, ICsvExporter csvExporter)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _csvExporter = csvExporter;
        }

        public async Task<CardExportFileVm> Handle(GetCardsExportQuery request, CancellationToken cancellationToken)
        {
            var allCards = _mapper.Map<List<CardExportDto>>((await _cardRepository.ListAllAsync()).OrderBy(p => p.CreateDate));

            var fileData = _csvExporter.ExportCardsToCsv(allCards);

            var cardExportFileDto = new CardExportFileVm()
            {
                ContentType = "test/csv",
                Data = fileData,
                CardExportFileName = $"{Guid.NewGuid()}.csv"
            };

            return cardExportFileDto;
        }
    }
}
