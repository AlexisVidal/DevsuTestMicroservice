namespace MicroserviceTwo.Models
{
    public class Cuenta
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public int PersonaId { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
    }
}
