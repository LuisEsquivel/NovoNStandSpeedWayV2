namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using BIZ;
    using COMMON.Interfaces.Catalogos;

    [Route("api/[controller]")]
    [ApiController]
    public class FormaAdquisicionController : GenericApiController<FormaAdquisicion>
    {
        readonly IFormaAdquisicionManager formaAdquisicionManager;
        public FormaAdquisicionController() : base(FabricManager.FormaAdquisicionManager())
        {
            formaAdquisicionManager = (IFormaAdquisicionManager)base.GetManager;
        }
    }
}
