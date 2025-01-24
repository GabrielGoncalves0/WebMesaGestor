﻿using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Transacao
{

    public class TraEdicaoDTO
    {
        public Guid Id { get; set; }
        public string TraDescricao { get; set; }
        public decimal TraValor { get; set; }
        public TranStatus TransacaoStatus { get; set; }
        public Guid? UsuarioId { get; set; }
        public Guid? CaixaId { get; set; }
        public Guid? PedidoId { get; set; }
    }
}
