//using System.Drawing;
//using WebMesaGestor.Application.Common;
//using WebMesaGestor.Application.DTO.Input.Caixa;
//using WebMesaGestor.Application.DTO.Input.USuario;
//using WebMesaGestor.Application.DTO.Output;
//using WebMesaGestor.Application.Map;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;
//using WebMesaGestor.Infra.Repositories;
//using WebMesaGestor.Utils;

//namespace WebMesaGestor.Application.Services
//{
//    public class CaixaService
//    {
//        private readonly ICaixaRepository _caixaRepository; 
//        private readonly IUsuarioRepository _usuarioRepository;


//        public CaixaService(IUsuarioRepository usuarioRepository, ICaixaRepository caixaRepository)
//        {
//            _caixaRepository = caixaRepository;
//            _usuarioRepository = usuarioRepository;
//        }

//        public async Task<Response<IEnumerable<CaiOutputDTO>>> ListarCaixas()
//        {
//            Response<IEnumerable<CaiOutputDTO>> resposta = new Response<IEnumerable<CaiOutputDTO>>();
//            try
//            {
//                IEnumerable<Caixa> caixas = await _caixaRepository.ListarCaixas();
//                foreach (var caixa in caixas)
//                {
//                    if (caixa.UsuarioId != null)
//                    {
//                        caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
//                    }
//                }

//                await PreencherCaixa(caixas);
//                resposta.Dados = CaixaMap.MapCaixa(caixas);
//                resposta.Mensagem = "Caixas listados com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CaiOutputDTO>> CaixaPorId(Guid id)
//        {
//            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
//            try
//            {
//                Caixa caixa = await _caixaRepository.CaixaPorId(id);
//                if (caixa.UsuarioId != null)
//                {
//                    caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
//                }

//                await PreencherCaixa(caixa);
//                resposta.Dados = CaixaMap.MapCaixa(caixa);
//                resposta.Mensagem = "Caixas listados com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CaiOutputDTO>> AbrirCaixa(CaiAbrirDTO caixa)
//        {
//            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
//            try
//            {
//                ValidarCaixaAbertura(caixa);
//                await ValidarUsuario(caixa.UsuarioId);
//                Caixa map = CaixaMap.MapCaixa(caixa);
//                Caixa retorno = await _caixaRepository.AbrirCaixa(map);
//                await PreencherCaixa(retorno);

//                resposta.Dados = CaixaMap.MapCaixa(retorno);
//                resposta.Mensagem = "Caixas listados com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CaiOutputDTO>> FecharCaixa(CaiFecharDTO caixa)
//        {
//            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
//            try
//            {
//                Caixa buscarCaixa = await _caixaRepository.CaixaPorId(caixa.Id);
//                AtualizarDadosFechamendo(buscarCaixa, caixa);
//                Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarCaixa);

//                resposta.Dados = CaixaMap.MapCaixa(retorno);
//                resposta.Mensagem = "Caixas fechado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CaiOutputDTO>> AtualizarCaixa(CaiAtualizarDTO caixa)
//        {
//            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
//            try
//            {
//                Caixa buscarCaixa = await _caixaRepository.CaixaPorId(caixa.Id);
//                if (buscarCaixa == null)
//                {
//                    resposta.Mensagem = "Caixa não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                AtualizarDadosAtualizacao(buscarCaixa, caixa);

//                Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarCaixa);

//                resposta.Dados = CaixaMap.MapCaixa(retorno);
//                resposta.Mensagem = "Caixa atualizado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CaiOutputDTO>> DeletarCaixa(Guid id)
//        {
//            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();

//            try
//            {
//                Caixa retorno = await _caixaRepository.DeletarCaixa(id);

//                resposta.Dados = CaixaMap.MapCaixa(retorno);
//                resposta.Mensagem = "Caixa deletado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CaiOutputDTO>> ReabrirUltimoCaixa()
//        {
//            Response<CaiOutputDTO> resposta = new Response<CaiOutputDTO>();
//            Caixa buscarUltimoCaixa = await _caixaRepository.UltimoCaixa();
//            if (buscarUltimoCaixa == null)
//            {
//                resposta.Mensagem = "Caixa não encontrado.";
//                resposta.Status = false;
//                return resposta;
//            }
//            buscarUltimoCaixa.CaiStatus = CaixaStatus.Aberto;
//            Caixa retorno = await _caixaRepository.AtualizarCaixa(buscarUltimoCaixa);
//            resposta.Dados = CaixaMap.MapCaixa(retorno);
//            resposta.Mensagem = "Caixa reaberto com sucesso";
//            return resposta;
//        }

//        //Metodos auxiliares

//        private async Task PreencherCaixa(IEnumerable<Caixa> caixas)
//        {
//            foreach (var caixa in caixas)
//            {
//                if (caixa.UsuarioId != null)
//                {
//                    caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
//                }
//            }
//        }

//        private async Task PreencherCaixa(Caixa caixa)
//        {
//            if (caixa.UsuarioId != null)
//            {
//                caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
//            }
//        }

//        private async Task ValidarUsuario(Guid? usuarioId)
//        {
//            if (usuarioId == null || usuarioId == Guid.Empty)
//            {
//                throw new Exception("Usuario é obrigatório");
//            }

//            var usuario = await _usuarioRepository.UsuarioPorId((Guid)usuarioId);

//            if (usuario == null)
//            {
//                throw new Exception("Usuario não encontrado");
//            }
//        }

//        private void ValidarCaixaAbertura(CaiAbrirDTO caixa)
//        {
//            ValidadorUtils.ValidarDecimalSeVazio(caixa.CaiValInicial, "Valor é obrigatório");
//            ValidadorUtils.ValidarMaximo(caixa.CaiValInicial, 9999999, "Valor deve ser menor que 9999999");
//            ValidadorUtils.ValidarMinimo(caixa.CaiValInicial, 0, "Valor deve ser maior que 0");
//        }

//        private void ValidarCaixaFechamento(CaiFecharDTO caixa)
//        {
//            ValidadorUtils.ValidarDecimalSeVazio(caixa.CaiValFechamento, "Valor é obrigatório");
//            ValidadorUtils.ValidarMaximo(caixa.CaiValFechamento, 9999999, "Valor deve ser menor que 9999999");
//            ValidadorUtils.ValidarMinimo(caixa.CaiValFechamento, 0, "Valor deve ser maior que 0");
//        }

//        private void AtualizarDadosFechamendo(Caixa caixaExistente, CaiFecharDTO caixa)
//        {
//            if (caixa.CaiValFechamento != null)
//            {
//                caixaExistente.CaiValFechamento = caixa.CaiValFechamento;
//            }
//            caixaExistente.CaiStatus = CaixaStatus.Fechado;
//            caixaExistente.FechamentoData = DateTime.UtcNow;
//        }

//        private void AtualizarDadosAtualizacao(Caixa caixaExistente, CaiAtualizarDTO caixa)
//        {
//            if(caixa.CaiValTotal != null)
//            {
//                caixaExistente.CaiValTotal = caixa.CaiValTotal;
//            }
//            if (caixa.CaiStatus != null)
//            {
//                caixaExistente.CaiStatus = caixa.CaiStatus;
//            }
//        }
//    }
//}
