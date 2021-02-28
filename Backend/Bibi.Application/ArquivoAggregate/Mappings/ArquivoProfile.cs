using AutoMapper;
using Bibi.Application.ArquivoAggregate.Dto;
using Bibi.Domain;

namespace Bibi.Application.ArquivoAggregate.Mappings
{
    public class ArquivoProfile : Profile
    {
        public ArquivoProfile()
        {
            CreateMap<Arquivo, ArquivoOuputDto>();
        }

    }
}