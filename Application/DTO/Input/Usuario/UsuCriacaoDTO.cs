﻿using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Usuario
{
    public class UsuCriacaoDTO
    {
        public string UsuNome { get; set; }
        public string UsuEmail { get; set; }
        public string UsuTelefone { get; set; }
        public string UsuSenha { get; set; }
        public UsuarioTipo UsuTipo { get; set; }
        public Guid EmpresaId { get; set; }
    }
}
