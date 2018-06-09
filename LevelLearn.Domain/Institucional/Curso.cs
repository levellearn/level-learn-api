namespace LevelLearn.Domain.Institucional
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public int InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}
