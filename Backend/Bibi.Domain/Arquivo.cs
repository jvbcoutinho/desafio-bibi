using System;

namespace Bibi.Domain
{
    public class Arquivo
    {
        public Arquivo(string resourceId, string nome, EStatus status)
        {
            this.ResourceId = resourceId;
            this.Nome = nome;
            this.Status = status;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string ResourceId { get; set; }
        public string Nome { get; set; }
        public EStatus Status { get; set; }
    }
}