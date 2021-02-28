using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bibi.Application.ArquivoAggregate.Dto;

namespace Bibi.Application.ArquivoAggregate
{
    public interface IArquivoService
    {
        Task<ArquivoOuputDto> AdicionarArquivo(string fileString, string fileName);
        Task<IEnumerable<ArquivoOuputDto>> ObterTodasAnalises();
        Task<ArquivoOuputDto> ObterAnalise(string resourceId);
    }
}