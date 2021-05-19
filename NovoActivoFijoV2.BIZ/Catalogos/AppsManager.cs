namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;
    public class AppsManager : GenericManager<Apps>, IAppsManager
    {
        public AppsManager(IGenericRepository<Apps> repository) : base(repository)
        {
        }
    }
}
