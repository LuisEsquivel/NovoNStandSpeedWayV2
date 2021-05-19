namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;

    public class CentroCostosManager : GenericManager<CentroCostos>, ICentroCostosManager
    {
        public CentroCostosManager(IGenericRepository<CentroCostos> repository) : base(repository)
        {
        }
    }
}
