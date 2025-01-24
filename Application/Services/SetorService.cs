using WebMesaGestor.Application.DTO.Input.Setor;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Utils;

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
                if (setors == null)
                {
                    resposta.Mensagem = "Nenhuma setor encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
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
                if (setor == null)
                {
                    resposta.Mensagem = "Setor não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
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
                ValidarSetorCriacao(setor);
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
                ValidarSetorEdicao(setor);
                Setor buscarSetor = await _setorRepository.SetorPorId(setor.Id);
                if (buscarSetor == null)
                {
                    resposta.Mensagem = "Setor não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosSetor(buscarSetor, setor);
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
                Setor setor = await _setorRepository.SetorPorId(id);
                if (setor == null)
                {
                    resposta.Mensagem = "Setor não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
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

        private void ValidarSetorCriacao(SetCriacaoDTO setor)
        {
            ValidadorUtils.ValidarMaximo(setor.SetDesc, 100, "Setor deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarSeVazioOuNulo(setor.SetDesc, "Setor é obrigatório");
            if (!Enum.IsDefined(typeof(SetorStatus), setor.SetStatus))
            {
                throw new Exception("Status do setor é obrigatório");
            }
        }

        private void ValidarSetorEdicao(SetEdicaoDTO setor)
        {
            ValidadorUtils.ValidarMaximo(setor.SetDesc, 100, "Setor deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarSeVazioOuNulo(setor.SetDesc, "Setor é obrigatório");
            if (!Enum.IsDefined(typeof(SetorStatus), setor.SetStatus))
            {
                throw new Exception("Status do setor é obrigatório");
            }
        }

        private void AtualizarDadosSetor(Setor setorExistente, SetEdicaoDTO setor)
        {
            setorExistente.SetDesc = setor.SetDesc;
            setorExistente.SetStatus = setor.SetStatus;
        }
    }
}
