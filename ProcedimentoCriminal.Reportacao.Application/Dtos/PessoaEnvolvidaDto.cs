namespace ProcedimentoCriminal.Reportacao.Application.Dtos
{
    public class PessoaEnvolvidaDto
    {
        public string Nome { get; set; }
        public string Envolvimento { get; set; }
        public char Sexo { get; set; }
        public string CPF { get; set; }
        public string Profissao { get; set; }
        public string GravidadeLesoes { get; set; }
        public string RacaCor { get; set; }
    }
}