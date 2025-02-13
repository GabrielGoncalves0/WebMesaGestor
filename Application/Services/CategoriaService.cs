using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.DTO.Input.Categoria;
using WebMesaGestor.Application.Validations.Categoria;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> ObterTodasCategorias()
        {
            var categorias = await _categoriaRepository.ObterTodasCategorias();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task<CategoriaDTO> ObterCategoriaPorId(Guid id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(id);

            if (categoria == null)
            {
                return null;
            }

            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<Response<CategoriaDTO>> CriarCategoria(CatCriacaoDTO categoriaDTO)
        {
            var validationResult = new CategoriaCriacaoValidator().Validate(categoriaDTO);
            if (!validationResult.IsValid)
            {
                return new Response<CategoriaDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepository.CriarCategoria(categoria);

            return new Response<CategoriaDTO>
            {
                Sucesso = true,
                Id = categoria.Id,
                Data = _mapper.Map<CategoriaDTO>(categoria)
            };
        }

        public async Task<Response<CategoriaDTO>> AtualizarCategoria(CatEdicaoDTO categoriaDTO)
        {
            var validationResult = new CategoriaEdicaoValidator().Validate(categoriaDTO);
            if (!validationResult.IsValid)
            {
                return new Response<CategoriaDTO>()
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var categoriaExistente = await _categoriaRepository.ObterCategoriaPorId(categoriaDTO.Id);
            if (categoriaExistente == null)
            {
                return new Response<CategoriaDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { CategoriaMessages.CategoriaNaoEncontrada }
                };
            }

            var categoria = _mapper.Map(categoriaDTO, categoriaExistente);
            await _categoriaRepository.AtualizarCategoria(categoria);

            return new Response<CategoriaDTO>()
            {
                Sucesso = true,
                Id = categoria.Id,
                Data = _mapper.Map<CategoriaDTO>(categoria)
            };
        }

        public async Task<Response<CategoriaDTO>> DeletarCategoria(Guid id)
        {
            var categoriaExistente = await _categoriaRepository.ObterCategoriaPorId(id);

            if (categoriaExistente == null)
            {
                return new Response<CategoriaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CategoriaMessages.CategoriaNaoEncontrada }
                };
            }

            var sucessoDelecao = await _categoriaRepository.DeletarCategoria(id);
            if (!sucessoDelecao)
            {
                return new Response<CategoriaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CategoriaMessages.ErroAoDeletarCategoria }
                };
            }

            var dados = _mapper.Map<CategoriaDTO>(categoriaExistente);
            return new Response<CategoriaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { CategoriaMessages.CategoriaDeletadaComSucesso }
            };
        }
    }
}
