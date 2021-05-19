namespace COMMON.Entidades
{
    using System;
    public abstract class BaseDTO : IDisposable
    {
        private bool isDisposed;

        public string Id { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string UsuarioRegistroID { get; set; }

        public string UsuarioModificaID { get; set; }

        public bool EstaActivo { get; set; }

        public void Dispose()
        {
            if (isDisposed)
            {
                isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
