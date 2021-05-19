namespace BIZ
{
    using BIZ.Catalogos;
    using COMMON.Interfaces.Catalogos;
    using DAL.MsSQL;
    public static class FabricManager
    {
        public static IUsuariosManager UsuariosManager() => new UsuariosManager(FabricRepository.Usuarios());
        public static ICentroCostosManager CentroCostosManager() => new CentroCostosManager(FabricRepository.CentroCostos());
        public static IFormaAdquisicionManager FormaAdquisicionManager() => new FormaAdquisicionManager(FabricRepository.FormaAdquisicion());
        public static IRolesManager RolesManager() => new RolesManager(FabricRepository.Roles());
        public static IUbicacionesManager UbicacionesManager() => new UbicacionesManager(FabricRepository.Ubicaciones());
        public static IZonasLecturaManager ZonasLecturaManager() => new ZonasLecturaManager(FabricRepository.ZonasLectura());
        public static IAppsManager AppsManager() => new AppsManager(FabricRepository.Apps());
    }
}
