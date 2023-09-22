namespace DeliveryProxy.Calculator;

public class ShipmentDto
{
    public int WeightGrams { get; set; }

    public int SizeMillimetersX { get; set; }

    public int SizeMillimetersY { get; set; }

    public int SizeMillimetersZ { get; set; }

    public int FiasCodeFrom { get; set; }

    public int FiasCodeDest { get; set; }
}