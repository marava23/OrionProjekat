using FluentValidation;
using Orion_DataAcess;
using OrionAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrionAPI.Validators
{
    public class CreateContractValidator : AbstractValidator<ContractDTO>
    {
        public CreateContractValidator(OrionContext context)
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Mora postojati datum nastanka ugovora.");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Morate uneti dužinu ugovora");
            RuleFor(x => x.Duration).Must(x => x == 12 || x == 24).WithMessage("Dužina ugovora mora biti 12, ili 24 meseca.");
            RuleFor(x => x.Status).NotEmpty().DependentRules(() =>
             {
                 RuleFor(x => x.Status).Must(x => x == 1).WithMessage("Prilikom kreiranja ugovora on mora biti aktivan.");
             });
            RuleFor(x => x.Discount).Must(x => (x > 0 && x < 100) || x == null).WithMessage("Neodgovarajuća vrednost za popust.");
            RuleFor(x => x.FreeMonths).Must(x => (x >= 1 && x <= 6) || x==null).WithMessage("Gratis period može biti od 1 do 6 meseci");
            RuleFor(x => x.PackageIds).NotEmpty().WithMessage("Morate izabrati paket.").DependentRules(() =>
             {
                 RuleFor(x => x.PackageIds).Must(x =>
                 {
                     return !context.Packages.Any(y => y.Id.Equals(x));
                 }).WithMessage("Ne postoji takav paket.");
             });
        }
    }
}
