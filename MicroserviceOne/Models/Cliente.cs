using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MicroserviceOne.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        [Required]
        [ForeignKey("Establecimiento")]
        public int PersonaId { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
        public Persona Persona { get; set; }
    }
}
