using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using OpenCMS.Domain.Entities;
using OpenCMS.Shared.Models;

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
            MapConfiguration();
        }

        private void MapConfiguration()
        {
            CreateMap<ConfigurationManagements, ConfigurationModel>();
            CreateMap<ConfigurationManagements, HashConfigurationModel>()
                .ForMember(x => x.HashConfiguration,
                    src => src.MapFrom(s =>
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(s)))));
            CreateMap<ConfigurationModel, HashConfigurationModel>()
                .ForMember(x => x.HashConfiguration,
                    src => src.MapFrom(s =>
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(s)))));
        }

        private void MapChartOfAccount()
        {
            CreateMap<Accounts, AccountModel>();
            CreateMap<Classifications, ClassificationModel>();
        }

        private void MapSales()
        {
            CreateMap<Transactions, TransactionModel>();
            CreateMap<TransactionModel, Transactions>();
            CreateMap<TransactionItems, TransactionItemModel>();
            CreateMap<TransactionItemModel, TransactionItems>();
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
            CreateMap<CatalogBuyingDetailsModel, CatalogBuyingDetails>();
            CreateMap<Catalogs, CatalogModel>()
                .ForMember(dest => dest.BuyingDetails, opt => opt.MapFrom(src => src.CatalogBuyingDetails.FirstOrDefault(x => x.IsActive)))
                .ForMember(dest => dest.PreviousBuyingDetails, opt => opt.MapFrom(src => src.CatalogBuyingDetails.OrderBy(x => x.Id).FirstOrDefault()))
                .ForMember(dest => dest.SellingDetails, opt => opt.MapFrom(src => src.CatalogSellingDetails.FirstOrDefault()));
            CreateMap<CatalogModel, Catalogs>();
        }
    }
}
