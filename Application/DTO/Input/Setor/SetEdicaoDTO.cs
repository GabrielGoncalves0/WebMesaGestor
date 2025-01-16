using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Setor
{
    public class SetEdicaoDTO
    {
        public Guid Id { get; set; }
        public string SetDesc { get; set; }
        public SetorStatus SetStatus { get; set; }
    }
}
