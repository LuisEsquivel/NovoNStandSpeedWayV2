using COMMON.Entidades;
using FluentValidation;

namespace COMMON.Validadores
{
    public class ActivosValidator : GenericValidator<Activos>
    {
        public ActivosValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().MaximumLength(200);
        }

    }
}
