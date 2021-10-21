using FluentValidation;
using Orion_DataAcess;
using OrionAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrionAPI.Validators
{
    public class UpdatePackageValidator : AbstractValidator<PackageDTO>
    {
        public UpdatePackageValidator(OrionContext context)
        {
            RuleFor(x => x.Price).Must(x => x > 0).WithMessage("Cena mora biti veća od 0.");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Must((package, name) =>
            {
                return !context.Packages.Any(y => y.Name == name && y.Id != package.Id);
            }).WithMessage("Takav paket već postoji.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Morate da izaberete kategoriju kojoj paket pripada.").DependentRules(() =>
                RuleFor(x => x.CategoryId).Must(x =>
                {
                    return context.Categories.Any(y => y.Id == x);
                }
            ).WithMessage("Ne postoji takva kategorija."));
        }
    }
}
