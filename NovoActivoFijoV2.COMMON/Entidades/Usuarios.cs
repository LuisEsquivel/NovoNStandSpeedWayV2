namespace COMMON.Entidades
{
    public class Usuarios: BaseDTO
    {
        public string Nombre { get; set; }
        public string RolID { get; set; }
        public bool EsAdmin { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
