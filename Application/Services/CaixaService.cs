using System.Drawing;
using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;
using WebMesaGestor.Utils;

namespace WebMesaGestor.Application.Services
{
    public class CaixaService
    {
        private readonly ICaixaRepository _caixaRepository; 
        private readonly IUsuarioRepository _usuarioRepository;


        public CaixaService(IUsuarioRepository usuarioRepository, ICaixaRepository caixaRepository)
        {
            _caixaRepository = caixaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Response<IEnumerable<CaiOutputDTO>>> ListarCaixas()
        {
            Response<IEnumerable<CaiOutputDTO>> resposta = new Response<IEnumerable<CaiOutputDTO>>();
            try
            {
                IEnumerable<Caixa> caixas = await _caixaRepository.ListarCaixas();
                foreach (var caixa in caixas)
                {
                    if (caixa.UsuarioId != null)
                    {
                        caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
                    }
                }
                resposta.Dados = CaixaMap.MapCaixa(caixas);
                resposta.Mensagem = "Caixas listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<CaiOutputDTO>> CaixaPorId(Guid id)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            try
            {
                Caixa caixa = await _caixaRepository.CaixaPorId(id);
                if (caixa.UsuarioId != null)
                {
                    caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
                }

                resposta.Dados = CaixaMap.MapCaixa(caixa);
                resposta.Mensagem = "Caixas listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<CaiOutputDTO>> AbrirCaixa(CaiAbrirDTO caixa)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            try
            {
                Caixa map = CaixaMap.MapCaixa(caixa);
                Caixa retorno = await _caixaRepository.AbrirCaixa(map);

                resposta.Dados = CaixaMap.MapCaixa(retorno);
                resposta.Mensagem = "Caixas listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<CaiOutputDTO>> FecharCaixa(CaiFecharDTO caixa)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            try
            {
                Caixa buscarCaixa = await _caixaRepository.CaixaPorId(caixa.Id);
                buscarCaixa.CaiValFechamento = caixa.CaiValFechamento;
                Caixa retorno = await _caixaRepository.FecharCaixa(buscarCaixa);

                resposta.Dados = CaixaMap.MapCaixa(retorno);
                resposta.Mensagem = "Caixas listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<CaiOutputDTO>> AtualizarCaixa(CaiAtualizarDTO caixa)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            try
            {
                Caixa buscarCaixa = await _caixaRepository.CaixaPorId(caixa.Id);
                if (buscarCaixa == null)
                {
                    resposta.Mensagem = "Caixa não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                if (caixa.CaiValTotal.HasValue)
                {
                    buscarCaixa.CaiValTotal = caixa.CaiValTotal.Value;
                }

                if (caixa.CaiStatus.HasValue)
                {
                    buscarCaixa.CaiStatus = caixa.CaiStatus.Value;
                }

                Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarCaixa);

                resposta.Dados = CaixaMap.MapCaixa(retorno);
                resposta.Mensagem = "Caixa atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<CaiOutputDTO>> DeletarCaixa(Guid id)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();

            try
            {
                Caixa retorno = await _caixaRepository.DeletarCaixa(id);

                resposta.Dados = CaixaMap.MapCaixa(retorno);
                resposta.Mensagem = "Caixas listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<CaiOutputDTO>> SangriaCaixa(Guid id, decimal valor)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            Caixa buscarCaixa = await _caixaRepository.CaixaPorId(id);
            if (buscarCaixa == null)
            {
                resposta.Mensagem = "Caixa não encontrado.";
                resposta.Status = false;
                return resposta;
            }
            buscarCaixa.CaiValTotal -= valor;
            Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarCaixa);
            resposta.Dados = CaixaMap.MapCaixa(retorno);
            resposta.Mensagem = "Suprimento realizado com sucesso";
            return resposta;
        }

        public async Task<Response<CaiOutputDTO>> SuprimentoCaixa(Guid id, decimal valor)
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            Caixa buscarCaixa = await _caixaRepository.CaixaPorId(id);
            if (buscarCaixa == null)
            {
                resposta.Mensagem = "Caixa não encontrado.";
                resposta.Status = false;
                return resposta;
            }
            buscarCaixa.CaiValTotal += valor;
            Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarCaixa);
            resposta.Dados = CaixaMap.MapCaixa(retorno);
            resposta.Mensagem = "Suprimento realizado com sucesso";
            return resposta;
        }

        public async Task<Response<CaiOutputDTO>> ReabrirUltimoCaixa()
        {
            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
            Caixa buscarUltimoCaixa = await _caixaRepository.UltimoCaixa();
            if (buscarUltimoCaixa == null)
            {
                resposta.Mensagem = "Caixa não encontrado.";
                resposta.Status = false;
                return resposta;
            }
            buscarUltimoCaixa.CaiStatus = CaixaStatus.Aberto;
            Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarUltimoCaixa);
            resposta.Dados = CaixaMap.MapCaixa(retorno);
            resposta.Mensagem = "Caixa reaberto com sucesso";
            return resposta;
        }
    }
}
