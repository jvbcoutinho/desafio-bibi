using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Bibi.Application.ArquivoAggregate.Dto;
using Bibi.Application.VerificadorAntiCorruption;
using Bibi.Domain;

namespace Bibi.Application.ArquivoAggregate
{
    public class ArquivoService : IArquivoService
    {

        private readonly IArquivoRepository _arquivoRepository;
        private readonly IMapper _mapper;
        private readonly IVerificadorFacade _verificador;
        private readonly HttpClient _httpClient;

        public ArquivoService(IArquivoRepository arquivoRepository, IMapper mapper, IVerificadorFacade verificador)
        {
            _arquivoRepository = arquivoRepository;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _verificador = verificador;
        }

        public async Task<ArquivoOuputDto> AdicionarArquivo(string fileString, string fileName)
        {
            var uploadResponse = await _verificador.UploadArquivo(fileString);

            var arquivo = new Arquivo(uploadResponse.ResourceId, fileName, EStatus.ANALISE);

            await _arquivoRepository.Criar(arquivo);

            return _mapper.Map<ArquivoOuputDto>(arquivo);
        }

        public async Task<IEnumerable<ArquivoOuputDto>> ObterTodasAnalises()
        {
            var arquivos = await _arquivoRepository.ObterTodos();

            await AtualizarAnalisesPendentes(arquivos);

            return _mapper.Map<IEnumerable<ArquivoOuputDto>>(arquivos);
        }

        private async Task AtualizarAnalisesPendentes(IEnumerable<Arquivo> arquivos)
        {
            foreach (var arquivo in arquivos)
            {
                if (arquivo.Status == EStatus.ANALISE)
                    await AtualizarStatus(arquivo);
            }
        }

        public async Task<ArquivoOuputDto> ObterAnalise(string resourceId)
        {
            var arquivo = await _arquivoRepository.ObterPorResourceId(resourceId);

            var result = await AtualizarStatus(arquivo);

            return _mapper.Map<ArquivoOuputDto>(result);
        }

        private async Task<Arquivo> AtualizarStatus(Arquivo arquivo)
        {
            var reportResponse = await _verificador.ObterRelatorio(arquivo.ResourceId);

            if (reportResponse.Response_code == -2)
                return arquivo;

            if (reportResponse.Positives == 0)
                arquivo.Status = EStatus.SEGURO;
            else if (reportResponse.Positives > 0)
                arquivo.Status = EStatus.INSEGURO;

            await _arquivoRepository.Atualizar(arquivo);

            return arquivo;
        }

        public async Task Delete(string resourceId)
        {
            var arquivo = await _arquivoRepository.ObterPorResourceId(resourceId);

            await _arquivoRepository.Deletar(arquivo);
        }
    }
}