namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;

    
    public abstract class GenericValidator<T> : AbstractValidator<T> where T : BaseDTO
    {
        public GenericValidator()
        {
            RuleFor(x => x.Id).NotNull().MaximumLength(50);
            RuleFor(x => x.FechaAlta).NotNull();
            RuleFor(x => x.UsuarioRegistroID).NotNull().MaximumLength(50);
            RuleFor(x => x.UsuarioModificaID).NotNull().MaximumLength(50);
            RuleFor(x => x.FechaModificacion).NotNull();
            RuleFor(x => x.EstaActivo).NotNull();
        }
    }
}
