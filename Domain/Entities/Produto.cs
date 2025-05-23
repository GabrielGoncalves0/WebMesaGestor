﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMesaGestor.Domain.Entities
{
    public class Produto
    {
        public Produto()
        {
        }
        public Guid Id { get; set; }
        [Range(0, 999999)]
        public int ProCodigo { get; set; }
        [StringLength(100)]
        public string ProDescricao { get; set; }
        [StringLength(30)]
        public string ProUnidade { get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public  decimal ProPreco { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid? CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public Guid? SetorId { get; set; }
        public virtual Setor? Setor { get; set; }
    }
}
