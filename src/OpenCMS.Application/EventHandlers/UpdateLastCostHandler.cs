using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Shared.Models;

namespace OpenCMS.Application.EventHandlers
{
    public class UpdateLastCostHandler : 
        IRequestHandler<BuyingDetailsCreateOrUpdate, CatalogBuyingDetails>
    {
        private readonly IRepository<Catalogs, int> _catalogRepo;
        private readonly IRepository<CatalogBuyingDetails, int> _catalogBuyingRepo;
        private readonly IMapper _mapper;

        public UpdateLastCostHandler(IRepository<Catalogs, int> catalogRepo,IRepository<CatalogBuyingDetails,int> catalogBuyingRepo, IMapper mapper)
        {
            _catalogRepo = catalogRepo;
            _catalogBuyingRepo = catalogBuyingRepo;
            _mapper = mapper;
        }
        public async Task<CatalogBuyingDetails> Handle(BuyingDetailsCreateOrUpdate request, CancellationToken cancellationToken)
        {
            var item = request.CatalogBuyingDetails;

            var catalog = _catalogRepo.Find(x => x.Id == item.CatalogId);
            foreach (var i in catalog.CatalogBuyingDetails)
            {
                i.IsActive = false;
            }
            catalog.LastCost = item.StandardCost;
            await _catalogRepo.Update(catalog);
            item.IsActive = true;
            await _catalogBuyingRepo.Update(item);


            return item;
        }
    }
    public class BuyingDetailsCreateOrUpdate : IRequest<CatalogBuyingDetails>
    {
        public CatalogBuyingDetails  CatalogBuyingDetails { get; set; }
    }
}
