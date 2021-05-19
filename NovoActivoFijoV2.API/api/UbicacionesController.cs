namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using BIZ;
    using COMMON.Interfaces.Catalogos;

    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : GenericApiController<Ubicaciones>
    {
        readonly IUbicacionesManager ubicacionesManager;
        public UbicacionesController() : base(FabricManager.UbicacionesManager())
        {
            ubicacionesManager = (IUbicacionesManager)base.GetManager;
        }
    }
}
