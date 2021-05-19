namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using BIZ;
    using COMMON.Interfaces.Catalogos;

    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : GenericApiController<Roles>
    {
        readonly IRolesManager rolesManager;
        public RolesController() : base(FabricManager.RolesManager())
        {
            rolesManager = (IRolesManager)base.GetManager;
        }
    }
}
