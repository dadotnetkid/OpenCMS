using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OpenCMS.Domain.Entities;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Infrastructure.Mapper
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            MapCatalog();
            MapCardFile();
            MapSales();
            MapChartOfAccount();
        }

        private void MapChartOfAccount()
        {
            CreateMap<Accounts, AccountModel>();
            CreateMap<Classifications, ClassificationModel>();
        }

        private void MapSales()
        {
            CreateMap<Transactions, TransactionModel>();
            CreateMap<TransactionModel,Transactions>();
            CreateMap<TransactionItems, TransactionItemModel>();
            CreateMap<TransactionItemModel,TransactionItems>();
        }

        private void MapCardFile()
        {
            CreateMap<CardFiles, CardFilesModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<CardFilesModel, CardFiles>();
        }

        private void MapCatalog()
        {
            CreateMap<CatalogSellingDetails, CatalogSellingDetailsModel>();
            CreateMap<CatalogBuyingDetails, CatalogBuyingDetailsModel>();
            CreateMap<CatalogBuyingDetailsModel,CatalogBuyingDetails>();
            CreateMap<Catalogs, CatalogModel>()
                .ForMember(dest => dest.BuyingDetails, opt => opt.MapFrom(src => src.CatalogBuyingDetails.FirstOrDefault(x => x.IsActive)))
                .ForMember(dest => dest.PreviousBuyingDetails, opt => opt.MapFrom(src => src.CatalogBuyingDetails.OrderBy(x => x.Id).FirstOrDefault()))
                .ForMember(dest => dest.SellingDetails, opt => opt.MapFrom(src => src.CatalogSellingDetails.FirstOrDefault()));
            CreateMap<CatalogModel, Catalogs>();
        }
    }
}
