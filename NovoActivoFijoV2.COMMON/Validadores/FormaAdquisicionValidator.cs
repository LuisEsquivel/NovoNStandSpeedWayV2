

namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;

    public class FormaAdquisicionValidator: GenericValidator<FormaAdquisicion>
    {
        public FormaAdquisicionValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().MaximumLength(200);
        }
        
    }
}
