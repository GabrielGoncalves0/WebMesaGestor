using WebMesaGestor.Application.DTO.Input.Setor;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class SetorMap
    {
        public static SetOutputDTO MapSetor(this Setor setor)
        {
            return new SetOutputDTO
            {
                Id = setor.Id,
                SetDesc = setor.SetDesc,
                SetStatus = setor.SetStatus,
                CriacaoData = setor.CriacaoData
            };
        }

        public static IEnumerable<SetOutputDTO> MapSetor(this IEnumerable<Setor> setor)
        {
            return setor.Select(x => x.MapSetor()).ToList();
        }

        public static Setor MapSetor(this SetCriacaoDTO setor)
        {
            return new Setor
            {
                Id = Guid.NewGuid(),
                SetDesc = setor.SetDesc,
                SetStatus = setor.SetStatus,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Setor> MapSetor(this IEnumerable<SetCriacaoDTO> setor)
        {
            return setor.Select(x => x.MapSetor()).ToList();
        }

        public static Setor MapSetor(this SetEdicaoDTO setor)
        {
            return new Setor
            {
                SetDesc = setor.SetDesc,
                SetStatus = setor.SetStatus
            };
        }

        public static IEnumerable<Setor> MapSetor(this IEnumerable<SetEdicaoDTO> setor)
        {
            return setor.Select(x => x.MapSetor()).ToList();
        }
    }
}
