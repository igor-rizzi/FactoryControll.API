namespace FactoryControll.Application.Models
{
    public class EmailMessageDto
    {
        public string Para { get; set; } = default!;
        public string Assunto { get; set; } = default!;
        public string CorpoHtml { get; set; } = default!;
    }
}
