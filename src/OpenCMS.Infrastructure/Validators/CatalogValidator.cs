using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Domain.Models.InputModels;

namespace OpenCMS.Infrastructure.Validators
{
    public class CatalogValidator: AbstractValidator<CreateCatalogInputModel>
    {
        public CatalogValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            When(x => x.IBuyThisItem == true, () =>
            {
                RuleFor(x => x.IncomeAccount)
                    .NotNull()
                    .WithMessage("Income Account should not empty");
            });
            When(x => x.ISellThisItem == true, () =>
            {
                RuleFor(x => x.SalesAccount)
                    .NotNull()
                    .WithMessage("Sales Account should not empty");
            });
            When(x => x.IInventoryThisItem== true, () =>
            {
                RuleFor(x => x.InventoryAccount)
                    .NotNull()
                    .WithMessage("Inventory Account should not empty");
            });

        }
    }
}
