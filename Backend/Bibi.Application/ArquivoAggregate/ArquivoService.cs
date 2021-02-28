using System.Collections.Generic;
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

        public async Task<ArquivoOuputDto> AnalisarArquivo(string fileString, string fileName)
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
            var result = await _arquivoRepository.ObterTodos();

            return _mapper.Map<IEnumerable<ArquivoOuputDto>>(result);
        }
    }
}