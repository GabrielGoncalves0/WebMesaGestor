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

        public async Task<Response<SetOutputDTO>> SetorPorId(Guid id)
        {
            Response<SetOutputDTO> resposta = new Response<SetOutputDTO>();
            try
            {
                Setor setor = await _setorRepository.SetorPorId(id);

                resposta.Dados = SetorMap.MapSetor(setor);
                resposta.Mensagem = "Setor encontrado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<SetOutputDTO>> CriarSetor(SetCriacaoDTO setor)
        {
            Response<SetOutputDTO> resposta = new Response<SetOutputDTO>();
            try
            {
                Setor map = SetorMap.MapSetor(setor);
                Setor retorno = await _setorRepository.CriarSetor(map);

                resposta.Dados = SetorMap.MapSetor(retorno);
                resposta.Mensagem = "Setor criado com sucesso";
                return resposta; 
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<SetOutputDTO>> AtualizarSetor(SetEdicaoDTO setor)
        {
            Response<SetOutputDTO> resposta = new Response<SetOutputDTO>();
            try
            {
                Setor buscarSetor = await _setorRepository.SetorPorId(setor.Id);
                buscarSetor.SetDesc = setor.SetDesc;
                buscarSetor.SetStatus = setor.SetStatus;
                Setor retorno = await _setorRepository.AtualizarSetor(buscarSetor);

                resposta.Dados = SetorMap.MapSetor(retorno);
                resposta.Mensagem = "Setor atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<SetOutputDTO>> DeletarSetor(Guid id)
        {
            Response<SetOutputDTO> resposta = new Response<SetOutputDTO>();
            try
            {
                Setor retorno = await _setorRepository.DeletarSetor(id);
                resposta.Dados = SetorMap.MapSetor(retorno);
                resposta.Mensagem = "Setor deletado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
