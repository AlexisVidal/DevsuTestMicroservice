namespace MicroserviceTwo.Dto
{
    public class CuentaClienteDto
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

        // Datos de la persona (recibidos de MicroserviceOne)
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
