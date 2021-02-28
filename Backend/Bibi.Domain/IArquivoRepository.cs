using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bibi.Domain
{
    public interface IArquivoRepository
    {
        Task Criar(Arquivo arquivo);
        Task Deletar(Arquivo arquivo);
        Task<Arquivo> ObterPorResourceId(string id);
        Task<IEnumerable<Arquivo>> ObterTodos();
    }
}