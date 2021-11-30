using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Shared.Models;

namespace OpenCMS.Shared.Validators
{
    public class TransactionsValidator:BaseAbstractValidator<TransactionModel>
    {
        public TransactionsValidator()
        {
           /* When(x => x.TransactionType == Common.TransactionType.Purchases, () =>
            {
                RuleFor(x => x.CardFileId)
                    .NotEqual(0)
                    .WithMessage($"Vendor should not empty");
            });
            When(x => x.TransactionType == Common.TransactionType.Sales, () =>
            {
                RuleFor(x => x.CardFileId)
                    .NotEqual(0)
                    .WithMessage($"Customer should not empty");
            });*/

            RuleFor(x => x.CardFileId)
                .NotEmpty()
                .WithMessage($"Card File should not empty");
            RuleFor(x => x.HasItems)
                .NotNull()
                .WithMessage("Items should not empty");

        }
    }
}
