namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using BIZ;
    using COMMON.Interfaces.Catalogos;

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : GenericApiController<Usuarios>
    {
        readonly IUsuariosManager usuariosManager;
        public UsuariosController() : base(FabricManager.UsuariosManager())
        {
            usuariosManager = (IUsuariosManager)base.GetManager;
        }
    }
}
