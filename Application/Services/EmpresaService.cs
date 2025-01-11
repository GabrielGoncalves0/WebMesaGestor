using WebMesaGestor.Application.DTO.Input;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<EmpresaOutputDTO>> ListarEmpresas()
        {
            IEnumerable<Empresa> empresas = await _empresaRepository.ListarEmpresas();
            return EmpresaMap.MapEmpresa(empresas);
        }

        public async Task<EmpresaOutputDTO> EmpresaPorId(Guid id)
        {
            Empresa empresa = await _empresaRepository.EmpresaPorId(id);
            return EmpresaMap.MapEmpresa(empresa);
        }

        public async Task<Empresa> CriarEmpresa(EmpresaInputDTO empresa)
        {
            Empresa map = EmpresaMap.MapEmpresa(empresa);
            Empresa retorno = await _empresaRepository.CriarEmpresa(map);
            return retorno;
        }

        public async Task<EmpresaOutputDTO> AtualizarEmpresa(EmpresaInputDTO empresa)
        {
            Empresa retorno = EmpresaMap.MapEmpresa(empresa);
            retorno = await _empresaRepository.AtualizarEmpresa(retorno);
            return EmpresaMap.MapEmpresa(retorno);
        }

        public async Task<EmpresaOutputDTO> DeletarEmpresa(Guid id)
        {
            Empresa retorno = await _empresaRepository.DeletarEmpresa(id);
            return EmpresaMap.MapEmpresa(retorno);
        }
    }
}
