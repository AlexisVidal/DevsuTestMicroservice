namespace MicroserviceOne.Models
{
    public class Cliente
    {
        public int PersonaId { get; set; } // PK y FK
        public string Contrasena { get; set; }
        public bool Estado { get; set; }

        public Persona Persona { get; set; } // 1:1 con Persona
    }
}
