using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Shared.Models;

namespace OpenCMS.Web.Infrastructure.Validators
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
