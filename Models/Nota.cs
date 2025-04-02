using System.ComponentModel.DataAnnotations;

namespace NotaAPI.Models;

public class Nota
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Aluno { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Disciplina { get; set; } = null!;

    [Required, Range(0, 10)]
    public decimal Valor { get; set; }

    [Required]
    public DateTime DataLancamento { get; set; } = DateTime.UtcNow;
}
