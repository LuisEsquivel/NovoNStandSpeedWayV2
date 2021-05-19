using System;
using System.Collections.Generic;
using System.Text;

namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;

    public class RolesManager : GenericManager<Roles>, IRolesManager
    {
        public RolesManager(IGenericRepository<Roles> repository) : base(repository)
        {
        }
    }
}
