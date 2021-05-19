
namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;

    public class ZonasLecturaManager : GenericManager<ZonasLectura>, IZonasLecturaManager
    {
        public ZonasLecturaManager(IGenericRepository<ZonasLectura> repository) : base(repository)
        {
        }
    }
}
