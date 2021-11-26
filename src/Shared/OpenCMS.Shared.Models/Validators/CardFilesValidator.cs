using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Shared.Models;

namespace OpenCMS.Shared.Validators
{
    public class CardFilesValidator:BaseAbstractValidator<CardFilesModel>
    {
        public CardFilesValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("First Name is Required");
            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Last Name is Required");
            RuleFor(x=>x.AddressLine1)
                .NotNull()
                .WithMessage("Address Line 1 is Required");
            RuleFor(x => x.CardFileType)
                .NotNull()
                .WithMessage("Card Type is Required");
        }
    }
}
