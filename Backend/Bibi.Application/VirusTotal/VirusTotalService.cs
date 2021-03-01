using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bibi.Application.VerificadorAntiCorruption;
using Newtonsoft.Json;

namespace Bibi.Application.VirusTotal
{
    public class VirusTotalService : IVerificadorFacade
    {
        private readonly HttpClient _httpClient;

        public VirusTotalService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<UploadResponse> UploadArquivo(string fileString)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apikey", VirusTotalConfig.ApiKey),
                new KeyValuePair<string, string>("file", fileString)
            });

            var response = await _httpClient.PostAsync(VirusTotalConfig.ScanUrl, formContent);
            var contents = (await response.Content.ReadAsStringAsync()).Replace("resource", "resourceId");
            var scanResponse = JsonConvert.DeserializeObject<UploadResponse>(contents);
            return scanResponse;
        }

        public async Task<ReportResponse> ObterRelatorio(string resourceId)
        {
            var url = $"{VirusTotalConfig.ReportUrl}?apikey={VirusTotalConfig.ApiKey}&resource={resourceId}";
            var response = await _httpClient.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();

            var reportResponse = JsonConvert.DeserializeObject<ReportResponse>(contents);

            return reportResponse;

        }
    }
}