namespace DeliveryProxy.Calculator;

public class ShipmentDto
{
    public int WeightGrams { get; set; }

    public int SizeMmX { get; set; }

    public int SizeMmY { get; set; }

    public int SizeMmZ { get; set; }

    public int FiasFrom { get; set; }

    public int FiasDest { get; set; }
}