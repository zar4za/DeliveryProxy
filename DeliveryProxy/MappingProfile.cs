﻿using System.Net;
using DeliveryProxy.Calculator;
using DeliveryProxy.Calculator.Cdek;
using DeliveryProxy.Exceptions;
using Mapster;

namespace DeliveryProxy;

public static class MappingProfile
{
    public static void CreateMappers()
    {
        TypeAdapterConfig<ShipmentDto, CdekCalcRequest>
            .NewConfig()
            .Map(dest => dest.FromLocation,
                src => new CdekLocation(src.FiasFrom))
            .Map(dest => dest.ToLocation,
                src => new CdekLocation(src.FiasDest))
            .Map(dest => dest.Packages,
                src => new CdekPackage[]
                {
                    // note that cdek uses integer centimeter so any size after conversion should be ceiled to lowest integer
                    // ex: 105 mm = 10.5 cm, so lowest integer is 11 cm
                    new(src.WeightGrams, 
                        (int)Math.Ceiling(src.SizeMmX / 10f), 
                        (int)Math.Ceiling(src.SizeMmY / 10f), 
                        (int)Math.Ceiling(src.SizeMmZ / 10f))
                });

        TypeAdapterConfig<CdekCalcResponse, PriceDto>
            .NewConfig()
            .Map(dest => dest.Price,
                src => src.TotalPrice);
    }

    public static void CreateExceptionMappers()
    {
        TypeAdapterConfig<ExternalApiException, ExceptionResponse>
            .NewConfig()
            .Map(dest => dest.Status,
                _ => HttpStatusCode.BadGateway)
            .Map(dest => dest.ErrorName,
                _ => nameof(ExternalApiException));

        TypeAdapterConfig<CalculationBadRequestException, ExceptionResponse>
            .NewConfig()
            .Map(dest => dest.Status,
                _ => HttpStatusCode.BadRequest)
            .Map(dest => dest.ErrorName,
                _ => nameof(CalculationBadRequestException));
    }
}