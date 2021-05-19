
using COMMON.Entidades;
using FluentValidation;


namespace COMMON.Validadores
{

    public class AppsValidator : GenericValidator<Apps>
    {
        public AppsValidator()
        {
            RuleFor(x => x.NombreApp).NotNull();
            RuleFor(x => x.Clave).NotNull();
        }
    }
}
