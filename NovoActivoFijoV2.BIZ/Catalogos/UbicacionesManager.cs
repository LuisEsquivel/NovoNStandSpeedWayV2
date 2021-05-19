using System;
using System.Collections.Generic;
using System.Text;

namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;

    public class UbicacionesManager : GenericManager<Ubicaciones>, IUbicacionesManager
    {
        public UbicacionesManager(IGenericRepository<Ubicaciones> repository) : base(repository)
        {
        }
    }
}
