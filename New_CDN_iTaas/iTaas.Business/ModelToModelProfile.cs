using AutoMapper;
using iTaas.Models;

namespace iTaas.Business
{
    public class ModelToModelProfile : Profile
    {
        public ModelToModelProfile()
        {
            CreateMap<Agora, MinhaCDN>();
            CreateMap<MinhaCDN, Agora>();
            
        }
    }
}
