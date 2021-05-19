

namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;
    public class UbicacionesValidator: GenericValidator<Ubicaciones>
    {
        public UbicacionesValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().MaximumLength(200);
        }
        
    }
}
