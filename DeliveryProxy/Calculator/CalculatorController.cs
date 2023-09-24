using DeliveryProxy.Calculator.Cdek;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryProxy.Calculator;

[ApiController]
[Route("[controller]")]
public class CalculatorController : Controller
{
    private readonly CalculatorService _calculatorService;

    public CalculatorController(CalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    [HttpGet("shipment/price")]
    public async Task<IActionResult> GetShipmentPriceAsync([FromQuery] ShipmentDto shipment, CancellationToken cancellation)
    {
        var request = shipment.Adapt<CdekCalcRequest>();
        var calc = await _calculatorService.GetShipmentPriceAsync(request, cancellation);
        var response = calc.Adapt<PriceDto>();

        return Ok(response);
    }
}