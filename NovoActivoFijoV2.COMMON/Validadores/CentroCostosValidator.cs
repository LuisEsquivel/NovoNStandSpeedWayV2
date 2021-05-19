namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;
  
    public class CentroCostosValidator : GenericValidator<CentroCostos>
    {
        public CentroCostosValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().MaximumLength(200);
        }
    }
}
