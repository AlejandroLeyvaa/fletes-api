using AutoMapper;
using Fletes.Models.DTOs;
using Fletes.Models;

namespace Fletes.Mapper
{
    public class FreightMappers
    {
        private readonly IMapper _mapper;

        public FreightMappers(IMapper mapper)
        {
            _mapper = mapper;

        }
        public Freight ConvertFreightDtoToFreight(FreightDTO freightDTO)
        {
            Freight freight = _mapper.Map<Freight>(freightDTO);

            return freight;
        }
    }
}
