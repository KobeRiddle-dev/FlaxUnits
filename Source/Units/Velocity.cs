#if USE_LARGE_WORLDS
using Real = System.Double;
using Mathr = FlaxEngine.Mathd;
#else
using Real = System.Single;
using Mathr = FlaxEngine.Mathf;
#endif

using System;

namespace Units;

/// <summary>
/// Represents a velocity, a change in distance over time.
/// </summary>
public struct Velocity
{

    /// <summary>Distance, the numerator of the Velocity</summary>
    public Distance Distance { get; set; }

    /// <summary>Time, the denominator of the Velocity</summary>
    public TimeSpan Time { get; set; }

    /// <summary>
    /// Creates a Velocity representing the distance over the specified TimeSpan. 
    /// <br/>This is only meant to be used by this class and by Distance's (Distance / TimeSpan) operator.
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="time"></param>
    internal Velocity(Distance distance, TimeSpan time)
    {
        this.Distance = distance;
        this.Time = time;
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Velocity representing the sum of the two Velocities</returns>
    public static Velocity operator +(Velocity left, Velocity right)
    {
        Real centimetersPerSecond = left.Distance.Centimeters / (Real)left.Time.TotalSeconds
                            + right.Distance.Centimeters / (Real)right.Time.TotalSeconds;

        return new Velocity(Distance.FromCentimeters(centimetersPerSecond), TimeSpan.FromSeconds(1));
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Velocity representing the sum of the two Velocities</returns>
    public static Velocity operator -(Velocity left, Velocity right)
    {
        return left + (-right);
    }

    /// <param name="velocity"></param>
    /// <returns>A Velocity representing the negation of velocity</returns>
    public static Velocity operator -(Velocity velocity)
    {
        return new Velocity(-velocity.Distance, velocity.Time);
    }

    /// <param name="velocity"></param>
    /// <param name="time"></param>
    /// <returns>An Acceleration representing a change in Velocity over the time specified</returns>
    public static Acceleration operator /(Velocity velocity, TimeSpan time)
    {
        return new Acceleration(velocity, time);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the total change in distance by this Velocity over the time specified</returns>
    public static Distance operator *(Velocity left, TimeSpan right)
    {
        Real metersPerSecond = left.Distance.Meters / (Real)left.Time.TotalSeconds;
        Real meters = metersPerSecond * (Real)right.TotalSeconds;
        return Distance.FromMeters(meters);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Velocity other = (Velocity)obj;
        Real thisCentimetersPerSecond = this.Distance.Centimeters / (Real)this.Time.TotalSeconds;
        Real otherCentimetersPerSecond = other.Distance.Centimeters / (Real)other.Time.TotalSeconds;

        Real allowableDifference = (Real)0.00001;
        return Mathr.Abs(thisCentimetersPerSecond - otherCentimetersPerSecond) <= allowableDifference;
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return this.Distance.GetHashCode() + this.Time.GetHashCode();
    }

    /// <summary>Equivalent to left.Equals(right)</summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>True if left and right are both Velocities and are equal, false otherwise</returns>
    public static bool operator ==(Velocity left, Velocity right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Velocity left, Velocity right)
    {
        return !left.Equals(right);
    }
}