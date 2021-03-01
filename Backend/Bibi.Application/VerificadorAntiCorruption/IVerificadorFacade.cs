using System.Threading.Tasks;
using Bibi.Application.VirusTotal;

namespace Bibi.Application.VerificadorAntiCorruption
{
    public interface IVerificadorFacade
    {
        Task<UploadResponse> UploadArquivo(string fileString);
        Task<ReportResponse> ObterRelatorio(string resourceId);
    }
}