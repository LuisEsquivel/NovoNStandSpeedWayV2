namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using COMMON.Interfaces.Catalogos;
    using BIZ;

    [Route("api/[controller]")]
    [ApiController]
    public class CentroCostosController : GenericApiController<CentroCostos>
    {
        readonly ICentroCostosManager centroCostosManager;
        public CentroCostosController() : base(FabricManager.CentroCostosManager())
        {
            centroCostosManager = (ICentroCostosManager)base.GetManager;
        }
    }
}
