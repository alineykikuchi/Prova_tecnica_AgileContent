using AutoMapper;

namespace iTaas.Business
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToModelProfile>();
            });
        }
    }
}
