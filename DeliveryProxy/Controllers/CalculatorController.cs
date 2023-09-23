using DeliveryProxy.Calculator;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryProxy.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : Controller

{
    private readonly CalculatorService _calculatorService;

    [HttpGet("shipment/price")]
    public async Task<IActionResult> GetShipmentPrice(ShipmentDto shipment)
    {

        var request =
        var calc = _calculatorService.GetShipmentPriceAsync()
    }
}