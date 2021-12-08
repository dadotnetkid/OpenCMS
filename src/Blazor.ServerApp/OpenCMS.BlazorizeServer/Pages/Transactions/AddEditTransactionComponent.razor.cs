using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;

namespace OpenCMS.BlazorizeServer.Pages.Transactions
{
    public partial class AddEditTransactionComponent
    {
        [Parameter] public string TransactionType { get; set; }
        [Parameter] public int? TransactionId { get; set; }
        private bool _showDialog;
        private ObservableCollection<TransactionItemModel> _dataSource;
        private TransactionModel _model = new();
        private TransactionItemModel _salesItemModel;
        private List<CardFilesModel> _cardFiles;
        private CardFileType _cardFileType;
        private string _currentPage;
        private TransactionType _transactionType;
        private List<EnumModel> _transactionStatus;
        private bool isLoading = true;
        public AddEditTransactionComponent()
        {
            _dataSource = new();
            _salesItemModel = new();
            _cardFiles = new();

        }
        
        protected override Task OnInitializedAsync()
        {
            _transactionStatus = Enum.GetValues<TransactionStatus>().ToList().Select(x => new EnumModel { Id = (int)x, Text = x.ToString() }).ToList();
            isLoading = false;
            return base.OnInitializedAsync();
        }
        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;
            _transactionType = Enum.Parse<TransactionType>(this.TransactionType, true);
            _cardFileType = _transactionType == OpenCMS.Shared.Common.TransactionType.Sales
                ? CardFileType.Customer
                : CardFileType.Supplier;
            var cardFiles = await CardFileApiClient.GetAll((int)_cardFileType);
            _cardFiles = cardFiles.Data.Items;
            if (TransactionId != null)
            {
                var _sales = await _transactionService.GetById(TransactionId.Value, _transactionType);
                if (_sales is BaseResponse<TransactionModel> item)
                {
                    _model = item.Data;
                }
                var _salesItems = await _transactionService.GetSalesItems(TransactionId.Value);
                if (_salesItems is BaseResponse<List<TransactionItemModel>> salesItems)
                {
                    _dataSource = new ObservableCollection<TransactionItemModel>(salesItems.Data);
                }
            }

            _model.HasItems = _dataSource.Any();
            _currentPage = TransactionId == null ? $"Add {TransactionType[0].ToString().ToUpper() + TransactionType.Substring(1)}" : _model.CardFile.FullName;
            isLoading = false;
            StateHasChanged();
        }

        private void ShowAddEditDialog(TransactionItemModel item)
        {
            item.TransactionType = _transactionType;
            _salesItemModel = item;
            _showDialog = true;
        }

        private async Task DeleteItem(TransactionItemModel context)
        {
            if (context.Id == 0)
            {
                _dataSource.Remove(context);
                return;
            }
            var _salesItems = await _transactionService.DeleteSalesItems(context.TransactionId ?? 0, context.Id);
            if (_salesItems is BaseResponse<List<TransactionItemModel>> salesItems)
            {
                _dataSource = new ObservableCollection<TransactionItemModel>(salesItems.Data);
            }
        }
        private void onDialogClose()
        {
            _showDialog = false;

        }
        private void OnSaleItemSave(TransactionItemModel model)
        {
            if (model.IsNew)
            {
                _dataSource.Remove(model);
                _dataSource.Add(model);
            }
            else
            {
                _dataSource.Remove(model);
                model.IsModified = true;
                _dataSource.Add(model);
            }
            _model.TotalAmount = _dataSource.Sum(x => x.SubTotal);
            _model.HasItems = _dataSource.Any();
            _showDialog = false;
            StateHasChanged();
        }

        private async Task OnValidSubmit()
        {
            _transactionType = Enum.Parse<TransactionType>(this.TransactionType, true);
            _model.TransactionType = _transactionType;
            var res = await _transactionService.CreateOrUpdate(_model, _dataSource);
            if (res.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _navigationManager.NavigateTo($"transactions/{TransactionType}");
            }
        }
    }
}
