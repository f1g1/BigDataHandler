using AutoMapper;
using BigDataHandler.Dtos;
using BigDataHandler.Models;
using Newtonsoft.Json;

namespace BigDataHandler.Mapper
{
    public class BigDataProfile : Profile
    {
        public BigDataProfile()
        {
            CreateMap<DataStamp, DtoDataStamp>()
                .ForMember(x => x.Values, d => d.MapFrom(y => JsonConvert.DeserializeObject<object>(y.Values)))
                .ForMember(x => x.Location, d => d.MapFrom(y => JsonConvert.DeserializeObject<object>(y.Location)));
            CreateMap<DtoDataStamp, DataStamp>()
                .ForMember(x => x.Values, d => d.MapFrom(y => JsonConvert.SerializeObject(y.Values)))
                .ForMember(x => x.Location, d => d.MapFrom(y => JsonConvert.SerializeObject(y.Location)));

            CreateMap<CartesianStatisticalFeatures, FeatureExtraction.CartesianStatisticalFeatures>().ReverseMap();

        }
    }
}
