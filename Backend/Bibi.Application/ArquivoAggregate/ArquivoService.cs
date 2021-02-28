using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Bibi.Application.ArquivoAggregate.Dto;
using Bibi.Application.VirusTotal;
using Bibi.Domain;
using Newtonsoft.Json;

namespace Bibi.Application.ArquivoAggregate
{
    public class ArquivoService : IArquivoService
    {

        private readonly IArquivoRepository _arquivoRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public ArquivoService(IArquivoRepository arquivoRepository, IMapper mapper)
        {
            _arquivoRepository = arquivoRepository;
            _mapper = mapper;
            _httpClient = new HttpClient();
        }

        public async Task<ArquivoOuputDto> AdicionarArquivo(string fileString, string fileName)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apikey", VirusTotalConfig.ApiKey),
                new KeyValuePair<string, string>("file", fileString)
            });

            var response = await _httpClient.PostAsync(VirusTotalConfig.ScanUrl, formContent);
            var contents = await response.Content.ReadAsStringAsync();
            var scanResponse = JsonConvert.DeserializeObject<ScanResponse>(contents);

            var arquivo = new Arquivo(scanResponse.Resource, fileName, EStatus.ANALISE);

            await _arquivoRepository.Criar(arquivo);

            return _mapper.Map<ArquivoOuputDto>(arquivo);
        }

        public async Task<IEnumerable<ArquivoOuputDto>> ObterTodasAnalises()
        {
            var analises = await _arquivoRepository.ObterTodos();

            await AtualizarAnalisesPendentes(analises);

            return _mapper.Map<IEnumerable<ArquivoOuputDto>>(analises);
        }

        private async Task AtualizarAnalisesPendentes(IEnumerable<Arquivo> analises)
        {
            foreach (var analise in analises)
            {
                if (analise.Status == EStatus.ANALISE)
                    await ObterAnalise(analise.ResourceId);
            }
        }

        public async Task<ArquivoOuputDto> ObterAnalise(string resourceId)
        {
            var url = $"{VirusTotalConfig.ReportUrl}?apikey={VirusTotalConfig.ApiKey}&resource={resourceId}";
            var response = await _httpClient.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();
            var arquivo = await _arquivoRepository.ObterPorResourceId(resourceId);

            if (!contents.Contains("positives"))
                return _mapper.Map<ArquivoOuputDto>(arquivo);

            var reportResponse = JsonConvert.DeserializeObject<ReportResponse>(contents);

            if (reportResponse.Positives == 0)
                arquivo.Status = EStatus.SEGURO;
            else if (reportResponse.Positives > 0)
                arquivo.Status = EStatus.INSEGURO;
            else
                return _mapper.Map<ArquivoOuputDto>(arquivo);

            await _arquivoRepository.Atualizar(arquivo);
            return _mapper.Map<ArquivoOuputDto>(arquivo);

        }
    }
}