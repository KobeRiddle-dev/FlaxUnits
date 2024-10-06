using Unity.VisualScripting;

public struct Force
{
    public Mass Mass { get; set; }
    public Acceleration Acceleration { get; set; }

    public Force(Mass mass, Acceleration acceleration)
    {
        this.Mass = mass;
        this.Acceleration = acceleration;
    }

    public readonly float Newtons
    {
        get { return Mass.Kilograms * Acceleration.Distance.Meters * Acceleration.TimeSquared.Seconds; }
    }
}