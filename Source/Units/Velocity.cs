using System;
namespace Units;
public struct Velocity
{

    public Distance Distance { get; set; }
    public TimeSpan Time { get; set; }

    public Velocity(Distance distance, TimeSpan time)
    {
        this.Distance = distance;
        this.Time = time;
    }

    public static Velocity operator +(Velocity left, Velocity right)
    {
        return new Velocity(left.Distance + right.Distance, right.Time);
    }

    public static Acceleration operator /(Velocity left, TimeSpan right)
    {
        return new Acceleration(left, right);
    }

    public static Distance operator *(Velocity left, TimeSpan right)
    {
        float metersPerSecond = left.Distance.Meters / (float)left.Time.TotalSeconds;
        float meters = metersPerSecond * (float)right.TotalSeconds;
        return Distance.FromMeters(meters);
    }
}