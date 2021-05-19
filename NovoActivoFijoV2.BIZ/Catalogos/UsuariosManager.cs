namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;

    public class UsuariosManager : GenericManager<Usuarios>, IUsuariosManager
    {
        public UsuariosManager(IGenericRepository<Usuarios> repository) : base(repository)
        {
        }
    }
}
