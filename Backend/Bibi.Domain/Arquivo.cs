namespace Bibi.Domain
{
    public class Arquivo
    {
        public Arquivo(string id, string nome, string conteudo, string status)
        {
            this.Id = id;
            this.Nome = nome;
            this.Conteudo = conteudo;
            this.Status = status;

        }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Conteudo { get; set; }
        public string Status { get; set; }
    }
}