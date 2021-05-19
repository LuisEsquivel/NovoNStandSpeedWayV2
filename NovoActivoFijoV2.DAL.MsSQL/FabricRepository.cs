


using COMMON.Entidades;
using COMMON.Interfaces;
using COMMON.Validadores;

namespace DAL.MsSQL
{

    public static class FabricRepository
    {
        public static IGenericRepository<Apps> Apps()
        {
            return new GenericRepository<Apps>(new AppsValidator());
        }

        public static IGenericRepository<CentroCostos> CentroCostos()
        {
            return new GenericRepository<CentroCostos>(new CentroCostosValidator());
        }

        public static IGenericRepository<FormaAdquisicion> FormaAdquisicion()
        {
            return new GenericRepository<FormaAdquisicion>(new FormaAdquisicionValidator());
        }

        public static IGenericRepository<Roles> Roles()
        {
            return new GenericRepository<Roles>(new RolesValidator());
        }

        public static IGenericRepository<Ubicaciones> Ubicaciones()
        {
            return new GenericRepository<Ubicaciones>(new UbicacionesValidator());
        }

        public static IGenericRepository<ZonasLectura> ZonasLectura()
        {
            return new GenericRepository<ZonasLectura>(new ZonasLecturaValidator());
        }

        public static IGenericRepository<Usuarios> Usuarios()
        {
            return new GenericRepository<Usuarios>(new UsuariosValidator());
        }
    }
}
