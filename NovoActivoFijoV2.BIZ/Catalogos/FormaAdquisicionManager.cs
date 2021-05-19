namespace BIZ.Catalogos
{
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using COMMON.Interfaces;
    public class FormaAdquisicionManager: GenericManager<FormaAdquisicion>, IFormaAdquisicionManager
    {
        public FormaAdquisicionManager(IGenericRepository<FormaAdquisicion> repository) : base(repository)
        {

        }


    }
}
