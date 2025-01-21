using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.DTO.Input.Setor;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class SetorService
    {
        private readonly ISetorRepository _setorRepository;

        public SetorService(ISetorRepository setorRepository)
        {
            _setorRepository = setorRepository;
        }

        public async Task<Response<IEnumerable<SetOutputDTO>>> ListarSetors()
        {
            Response<IEnumerable<SetOutputDTO>> resposta = new Response<IEnumerable<SetOutputDTO>>();
            try
            {
                IEnumerable<Setor> setors = await _setorRepository.ListarSetors();
                resposta.Dados = SetorMap.MapSetor(setors);
                resposta.Mensagem = "Setores listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<SetOutputDTO> SetorPorId(Guid id)
        {
            Setor setor = await _setorRepository.SetorPorId(id);
            return SetorMap.MapSetor(setor);
        }

        public async Task<SetOutputDTO> CriarSetor(SetCriacaoDTO setor)
        {
            Setor map = SetorMap.MapSetor(setor);
            Setor retorno = await _setorRepository.CriarSetor(map);
            return SetorMap.MapSetor(retorno);
        }

        public async Task<SetOutputDTO> AtualizarSetor(SetEdicaoDTO setor)
        {
            Setor buscarSetor = await _setorRepository.SetorPorId(setor.Id);

            buscarSetor.SetDesc = setor.SetDesc;
            buscarSetor.SetStatus = setor.SetStatus;

            Setor retorno = await _setorRepository.AtualizarSetor(buscarSetor);
            return SetorMap.MapSetor(retorno);
        }

        public async Task<SetOutputDTO> DeletarSetor(Guid id)
        {
            Setor retorno = await _setorRepository.DeletarSetor(id);
            return SetorMap.MapSetor(retorno);
        }
    }
}
