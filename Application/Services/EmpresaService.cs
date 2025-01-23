using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Application.DTO.Input.Empresa;
using WebMesaGestor.Utils;

namespace WebMesaGestor.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Response<IEnumerable<EmpOutputDTO>>> ListarEmpresas()
        {
            Response<IEnumerable<EmpOutputDTO>> resposta = new Response<IEnumerable<EmpOutputDTO>>();
            try
            {
                IEnumerable<Empresa> empresas = await _empresaRepository.ListarEmpresas();
                if (empresas == null || !empresas.Any())
                {
                    resposta.Mensagem = "Nenhuma empresa encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = EmpresaMap.MapEmpresa(empresas);
                resposta.Mensagem = "Empresas listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }   

        public async Task<Response<EmpOutputDTO>> EmpresaPorId(Guid id)
        {
            Response<EmpOutputDTO> resposta = new Response<EmpOutputDTO>();
            try
            {
                Empresa empresa = await _empresaRepository.EmpresaPorId(id);

                if (empresa == null)
                {
                    resposta.Mensagem = "Empresa não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = EmpresaMap.MapEmpresa(empresa);
                resposta.Mensagem = "Empresa encontrada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<EmpOutputDTO>> CriarEmpresa(EmpCriacaoDTO empresa)
        {
            Response<EmpOutputDTO> resposta = new Response<EmpOutputDTO>();
            try
            {
                ValidarUsuarioCriacao(empresa);
                Empresa map = EmpresaMap.MapEmpresa(empresa);
                Empresa retorno = await _empresaRepository.CriarEmpresa(map);

                resposta.Dados = EmpresaMap.MapEmpresa(retorno);
                resposta.Mensagem = "Empresa criada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<EmpOutputDTO>> AtualizarEmpresa(EmpEdicaoDTO empresa)
        {
            Response<EmpOutputDTO> resposta = new Response<EmpOutputDTO>();
            try
            {
                ValidarUsuarioEdicao(empresa);
                Empresa buscarEmpresa = await _empresaRepository.EmpresaPorId(empresa.Id);
                AtualizarDadosEmpresa(buscarEmpresa, empresa);
                Empresa retorno = await _empresaRepository.AtualizarEmpresa(buscarEmpresa);

                resposta.Dados = EmpresaMap.MapEmpresa(retorno);
                resposta.Mensagem = "Empresa atualizada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<EmpOutputDTO>> DeletarEmpresa(Guid id)
        {
            Response<EmpOutputDTO> resposta = new Response<EmpOutputDTO>();
            try
            {
                Empresa empresa = await _empresaRepository.EmpresaPorId(id);
                if (empresa == null)
                {
                    resposta.Mensagem = "Empresa não encontrada para deleção.";
                    resposta.Status = false;
                    return resposta;
                }
                Empresa retorno = await _empresaRepository.DeletarEmpresa(id);

                resposta.Dados = EmpresaMap.MapEmpresa(retorno);
                resposta.Mensagem = "Empresa deletada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        private void ValidarUsuarioCriacao(EmpCriacaoDTO empresa)
        {
            ValidadorUtils.ValidarMaximo(empresa.EmpNome, 50, "Nome deve conter no máximo 50 caracteres");
            ValidadorUtils.ValidarMinimo(empresa.EmpNome, 3, "Nome deve conter no minimo 3 caracteres");
            ValidadorUtils.ValidarMaximo(empresa.EmpCnpj, 18, "Nome deve conter no máximo 18 caracteres");
            ValidadorUtils.ValidarMinimo(empresa.EmpCnpj, 14, "Nome deve conter no minimo 14 caracteres");
            ValidadorUtils.ValidarCnpj(empresa.EmpCnpj, "Digite um CNPJ Valido");
        }
        private void ValidarUsuarioEdicao(EmpEdicaoDTO empresa)
        {
            ValidadorUtils.ValidarMaximo(empresa.EmpNome, 50, "Nome deve conter no máximo 50 caracteres");
            ValidadorUtils.ValidarMinimo(empresa.EmpNome, 3, "Nome deve conter no minimo 3 caracteres");
            ValidadorUtils.ValidarMaximo(empresa.EmpCnpj, 18, "Nome deve conter no máximo 18 caracteres");
            ValidadorUtils.ValidarMinimo(empresa.EmpCnpj, 14, "Nome deve conter no minimo 14 caracteres");
            ValidadorUtils.ValidarCnpj(empresa.EmpCnpj, "Digite um CNPJ Valido");
        }

        private void AtualizarDadosEmpresa(Empresa empresaExistente, EmpEdicaoDTO empresa)
        {
            empresaExistente.EmpNome = empresa.EmpNome;
            empresaExistente.EmpCnpj = empresa.EmpCnpj;
        }
    }
}
