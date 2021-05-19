namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;

    public class RolesValidator : GenericValidator<Roles>
    {
        public RolesValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().MaximumLength(200);
        }
    }
}
