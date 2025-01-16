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

        public async Task<IEnumerable<EmpOutputDTO>> ListarEmpresas()
        {
            IEnumerable<Empresa> empresas = await _empresaRepository.ListarEmpresas();
            return EmpresaMap.MapEmpresa(empresas);
        }   

        public async Task<EmpOutputDTO> EmpresaPorId(Guid id)
        {
            Empresa empresa = await _empresaRepository.EmpresaPorId(id);
            return EmpresaMap.MapEmpresa(empresa);
        }

        public async Task<EmpOutputDTO> CriarEmpresa(EmpCriacaoDTO empresa)
        {
            Empresa map = EmpresaMap.MapEmpresa(empresa);
            Empresa retorno = await _empresaRepository.CriarEmpresa(map);
            return EmpresaMap.MapEmpresa(retorno);
        }

        public async Task<EmpOutputDTO> AtualizarEmpresa(EmpEdicaoDTO empresa)
        {
            Empresa buscarEmpresa = await _empresaRepository.EmpresaPorId(empresa.Id);

            buscarEmpresa.EmpNome = empresa.EmpNome;
            buscarEmpresa.EmpCnpj = empresa.EmpCnpj;

            Empresa retorno = await _empresaRepository.AtualizarEmpresa(buscarEmpresa);
            return EmpresaMap.MapEmpresa(retorno);
        }

        public async Task<EmpOutputDTO> DeletarEmpresa(Guid id)
        {
            Empresa retorno = await _empresaRepository.DeletarEmpresa(id);
            return EmpresaMap.MapEmpresa(retorno);
        }
    }
}
