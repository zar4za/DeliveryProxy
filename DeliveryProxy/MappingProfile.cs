using DeliveryProxy.Calculator;
using DeliveryProxy.Calculator.Cdek;
using Mapster;

namespace DeliveryProxy;

public static class MappingProfile
{
    public static void CreateMappers()
    {
        TypeAdapterConfig<ShipmentDto, CdekCalcRequest>
            .NewConfig()
            .Map(
                dest => dest.FromLocation,
                src => new CdekLocation(src.FiasFrom))
            .Map(
                dest => dest.ToLocation,
                src => new CdekLocation(src.FiasDest))
            .Map(
                dest => dest.Packages,
                src => new CdekPackage[]
                {
                    // note that cdek uses integer centimeter so any size after conversion should be ceiled to lowest integer
                    // ex: 105 mm = 10.5 cm, so lowest integer is 11 cm
                    new(src.WeightGrams, 
                        (int)Math.Ceiling(src.SizeMmX / 10f), 
                        (int)Math.Ceiling(src.SizeMmX / 10f), 
                        (int)Math.Ceiling(src.SizeMmX / 10f))
                });
    }
}