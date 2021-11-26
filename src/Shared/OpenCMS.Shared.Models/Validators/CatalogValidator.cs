using FluentValidation;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Shared.Validators
{
    public class CatalogValidator:BaseAbstractValidator<CatalogModel>
    {
        public CatalogValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required");
            When(x => x.IBuyThisItem, () =>
            {
                RuleFor(x => x.IncomeAcount)
                    .NotNull()
                    .WithMessage("Income Account should not be empty");
            });
            When(x => x.IInventoryThisItem, () =>
            {
                RuleFor(x => x.InventoryAccount)
                    .NotNull()
                    .WithMessage("Inventory Account should not be empty");
            });
            When(x => x.ISellThisItem, () =>
            {
                RuleFor(x => x.SalesAcount)
                    .NotNull()
                    .WithMessage("Sales Account should not be empty");
            });
            
        }
    }
}
