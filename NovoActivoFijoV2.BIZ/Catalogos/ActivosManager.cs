using COMMON.Entidades;
using COMMON.Interfaces;
using COMMON.Interfaces.Catalogos;


namespace BIZ.Catalogos
{
    public class ActivosManager : GenericManager<Activos>, IActivosManager
    {
        public ActivosManager(IGenericRepository<Activos> repository) : base(repository)
        {
        }
    }
}
