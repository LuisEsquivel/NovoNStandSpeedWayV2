
namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;

    public class ZonasLecturaValidator: GenericValidator<ZonasLectura>
    {
        public ZonasLecturaValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().MaximumLength(200);
            RuleFor(x => x.DireccionIP).NotNull().MaximumLength(100);
            RuleFor(x => x.Modelo).NotNull().MaximumLength(50);

        }
    }
}
