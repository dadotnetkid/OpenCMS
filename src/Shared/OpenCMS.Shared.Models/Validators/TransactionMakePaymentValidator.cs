using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;

namespace OpenCMS.Shared.Validators
{
    public class TransactionMakePaymentValidator : BaseAbstractValidator<PaymentsModel>
    {
        public TransactionMakePaymentValidator()
        {
            When(x => x.PaymentMethod == (int)PaymentMethod.Cash, () =>
           {
               RuleFor(x => x.PaymentAmount)
                   .NotNull()
                   .WithMessage("Payment Amount is required");
               When(x => x.Term == (int)PaymentTerm.Full, () =>
               {
                   RuleFor(x => x.PaymentAmount)
                       .Equal(x => x.Transaction.TotalAmount)
                       .WithMessage("Payment Amount is not exact");
               });
           });
            When(x => x.PaymentMethod == (int)PaymentMethod.Check, () =>
           {
               RuleFor(x => x.CheckNumber)
                   .NotNull()
                   .WithMessage("Check number must not empty");
               RuleFor(x => x.CheckAmount)
                   .NotNull()
                   .WithMessage("Check amount must not empty");
               RuleFor(x => x.Bank)
                   .NotNull()
                   .WithMessage("Bank must not empty");
               When(x => x.Term == (int)PaymentTerm.Full, () =>
               {
                   RuleFor(x => x.CheckAmount)
                       .Equal(x => x.Transaction.TotalAmount)
                       .WithMessage("Payment Amount is not exact");
               });
           });
            When(x => x.PaymentMethod == (int)PaymentMethod.Credit || x.PaymentMethod == (int)PaymentMethod.Debit, () =>
             {
                 RuleFor(x => x.PaymentAmount)
                     .NotNull()
                     .WithMessage("Payment Amount is required")
                     .NotEmpty()
                     .WithMessage("Payment Amount is required");
                 RuleFor(x=>x.TransactionNumber)
                     .NotNull()
                     .WithMessage("Terminal/Transaction receipt number is required");
                 When(x => x.Term == (int)PaymentTerm.Full, () =>
                 {
                     RuleFor(x => x.CheckAmount)
                         .Equal(x => x.Transaction.TotalAmount)
                         .WithMessage("Payment Amount is not exact");
                 });
             });
        }
    }
}
