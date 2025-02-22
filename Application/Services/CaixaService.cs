using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Usuario;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;
using WebMesaGestor.Infra.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMesaGestor.Application.Services
{
    public class CaixaService
    {
        private readonly ICaixaRepository _caixaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;


        public CaixaService(IUsuarioRepository usuarioRepository, ICaixaRepository caixaRepository, IMapper mapper)
        {
            _caixaRepository = caixaRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CaixaDTO>> ObterTodosCaixas()
        {
            var caixas = await _caixaRepository.ObterTodosCaixas();
            if (caixas == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<CaixaDTO>>(caixas);
        }

        public async Task<CaixaDTO> ObterCaixaPorId(Guid id)
        {
            var caixa = await _caixaRepository.ObterCaixaPorId(id);
            if (caixa == null)
            {
                return null;
            }
            return _mapper.Map<CaixaDTO>(caixa);
        }

        public async Task<Response<CaixaDTO>> AbrirCaixa(CaiAbrirDTO caixaDTO)
        {
            var caixaAberto = await _caixaRepository.ObterCaixaAberto();
            if (caixaAberto != null)
            {
                return new Response<CaixaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaJaEstaAberto }
                };
            }

            var caixa = _mapper.Map<Caixa>(caixaDTO);
            caixa.CaiStatus = CaixaStatus.Aberto;

            var caixaCriado = await _caixaRepository.AbrirCaixa(caixa);

            var dados = _mapper.Map<CaixaDTO>(caixaCriado);
            return new Response<CaixaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { CaixaMessages.CaixaCriadoComSucesso }
            };
        }

        public async Task<Response<CaixaDTO>> FecharCaixa(CaiFecharDTO caixaDTO)
        {
            var caixa = await _caixaRepository.ObterCaixaPorId(caixaDTO.Id);
            if (caixa == null || caixa.CaiStatus == CaixaStatus.Fechado)
            {
                return new Response<CaixaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaNaoEncontrado }
                };
            }

            caixa.CaiStatus = CaixaStatus.Fechado;
            await _caixaRepository.AtualizarCaixa(caixa);

            var dados = _mapper.Map<CaixaDTO>(caixa);
            return new Response<CaixaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { CaixaMessages.CaixaAtualizadoComSucesso }
            };
        }

        public async Task<Response<CaixaDTO>> AtualizarCaixa(CaiAtualizarDTO caixaDTO)
        {
            var caixa = await _caixaRepository.ObterCaixaPorId(caixaDTO.Id);
            if (caixa == null)
            {
                return new Response<CaixaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaNaoEncontrado }
                };
            }

            _mapper.Map(caixaDTO, caixa);
            await _caixaRepository.AtualizarCaixa(caixa);

            var dados = _mapper.Map<CaixaDTO>(caixa);
            return new Response<CaixaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { CaixaMessages.CaixaAtualizadoComSucesso }
            };
        }

        public async Task<Response<CaixaDTO>> DeletarCaixa(Guid id)
        {
            var caixa = await _caixaRepository.ObterCaixaPorId(id);
            if (caixa == null)
            {
                return new Response<CaixaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaNaoEncontrado }
                };
            }

            var sucessoDelecao = await _caixaRepository.DeletarCaixa(id);
            if (!sucessoDelecao)
            {
                return new Response<CaixaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.ErroAoDeletarCaixa }
                };
            }

            var dados = _mapper.Map<CaixaDTO>(caixa);
            return new Response<CaixaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { CaixaMessages.CaixaDeletadoComSucesso }
            };
        }


        public async Task<Response<CaixaDTO>> ReabrirUltimoCaixa()
        {
            var buscarUltimoCaixa = await _caixaRepository.UltimoCaixa();

            if (buscarUltimoCaixa == null || buscarUltimoCaixa.CaiStatus == CaixaStatus.Aberto)
            {
                return new Response<CaixaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaJaEstaAberto }
                };
            } 

            buscarUltimoCaixa.CaiStatus = CaixaStatus.Aberto;
            await _caixaRepository.AtualizarCaixa(buscarUltimoCaixa);

            var dados = _mapper.Map<CaixaDTO>(buscarUltimoCaixa);
            return new Response<CaixaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { CaixaMessages.CaixaReabertoComSucesso }
            };
        }
    }
}
