
using System;
using System.Collections.Generic;
using System.Text;

namespace COMMON.Validadores
{
    using COMMON.Entidades;
    using FluentValidation;
    public class UsuariosValidator: GenericValidator<Usuarios>
    {
        public UsuariosValidator()
        {
            RuleFor(x => x.EsAdmin).NotNull();
            RuleFor(x => x.Nombre).NotNull();
            RuleFor(x => x.RolID).NotNull();
            RuleFor(x => x.Usuario).NotNull();
            RuleFor(x => x.Password).NotNull();

        }
    }
}
