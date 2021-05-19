namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using BIZ;
    using COMMON.Interfaces.Catalogos;

    [Route("api/[controller]")]
    [ApiController]
    public class ZonasLecturaController : GenericApiController<ZonasLectura>
    {
        readonly IZonasLecturaManager zonasLecturaManager;
        public ZonasLecturaController() : base(FabricManager.ZonasLecturaManager())
        {
            zonasLecturaManager = (IZonasLecturaManager)base.GetManager;
        }
    }
}
