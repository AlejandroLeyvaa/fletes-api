using Fletes.Models.DTOs;
using Fletes.Models;
using AutoMapper;
using Fletes.Mapper;

namespace Fletes.Services
{
    public class FreightServices
    {
        private readonly FreightMappers _freightMappers;

        public FreightServices(FreightMappers freightMappers)
        {
            _freightMappers = freightMappers;

        }

        public Freight createFreight(FreightDTO freightDTO)
        {
            return _freightMappers.ConvertFreightDtoToFreight(freightDTO);
        }
    }
}
