using FluentValidation;
using OpenCMS.Shared.Models;

namespace OpenCMS.Shared.Validators
{
    public class SalesItemsValidators:BaseAbstractValidator<TransactionItemModel>
    {
        public SalesItemsValidators()
        {
            RuleFor(x => x.CatalogId)
                .NotNull()
                .WithMessage("Catalog is Required");
            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Quantity is Required");

        }
    }
}
