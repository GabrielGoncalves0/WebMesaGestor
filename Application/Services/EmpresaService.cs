using System.Globalization;
using System;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Application.DTO.Input.Empresa;

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
                Empresa buscarEmpresa = await _empresaRepository.EmpresaPorId(empresa.Id);
                buscarEmpresa.EmpNome = empresa.EmpNome;
                buscarEmpresa.EmpCnpj = empresa.EmpCnpj;
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
    }
}
