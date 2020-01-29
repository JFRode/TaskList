using System;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Domain
{
    public class Task
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        [MinLength(5)]
        public string Titulo { get; set; }

        public bool Status { get; set; }

        [MaxLength(255)]
        public string Descricao { get; set; }

        public DateTime DataDeCriacao { get; set; }
        public DateTime DataDeEdicao { get; set; }
        public DateTime? DataDeExclusao { get; set; }
        public DateTime? DataDeConclusao { get; set; }
        public bool Excluido { get; set; }
    }
}