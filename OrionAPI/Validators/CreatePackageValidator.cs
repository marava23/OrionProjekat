using FluentValidation;
using Orion_DataAcess;
using OrionAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrionAPI.Validators
{
    public class CreatePackageValidator : AbstractValidator<PackageDTO>
    {
        public CreatePackageValidator(OrionContext context)
        {
            RuleFor(x => x.Price).Must(x => x > 0).WithMessage("Cena mora biti veća od 0.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Paket mora imati ime.").DependentRules(() =>
                RuleFor(x=> x.Name).Must(x=> {
                    return !context.Packages.Any(y => y.Name == x);
                    }).WithMessage("Takav paket već postoji.")
            );
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Morate da izaberete kategoriju kojoj paket pripada.").DependentRules(()=>
                RuleFor(x=> x.CategoryId).Must(x=>
                {
                    return context.Categories.Any(y => y.Id == x);
                }
            ).WithMessage("Ne postoji takva kategorija."));
        }
    }
}
