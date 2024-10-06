using System;


namespace Units;

/// <summary>
/// An acceleration; Velocity over Time
/// </summary>
public struct Acceleration
{
    public Velocity Velocity { get; set; }

    public TimeSpan Time { get; set; }

    public Acceleration(Velocity velocity, TimeSpan time)
    {
        this.Velocity = velocity;
        this.Time = time;
    }

    public readonly Distance Distance
    {
        get { return this.Velocity.Distance; }
    }

    public readonly TimeSpan TimeSquared
    {
        get { return TimeSpan.FromSeconds(this.Velocity.Time.TotalSeconds * this.Time.TotalSeconds); }
    }

    public static Velocity operator *(Acceleration left, TimeSpan right)
    {
        return Distance.FromMeters(left.Distance.Meters * (float)right.TotalSeconds) / TimeSpan.FromSeconds(Math.Sqrt(left.TimeSquared.TotalSeconds));
    }
}