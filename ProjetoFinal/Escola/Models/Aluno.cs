using System.ComponentModel.DataAnnotations.Schema;


namespace Escola.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public int TurmaID { get; set; }
        public int? TotalFaltas { get; set; }

        internal virtual Turma? Turma { get;  }
        
    }
}
