﻿namespace WebMesaGestor.Application.DTO.Output
{
    public class EmpOutputDTO
    {
        public Guid? Id { get; set; }
        public string EmpNome { get; set; }
        public string EmpCnpj { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
