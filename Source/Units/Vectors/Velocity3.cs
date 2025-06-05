#if USE_LARGE_WORLDS
using Real = System.Double;
using Mathr = FlaxEngine.Mathd;
#else
using Real = System.Single;
using Mathr = FlaxEngine.Mathf;
#endif

using System;
using FlaxEngine;
using FlaxEngine.Utilities;

namespace Units.Vectors;

/// <summary>
/// Represents a velocity, a change in distance over time.
/// </summary>
[Serializable]
public struct Velocity3
{

    /// <summary>Distance, the numerator of the Velocity</summary>
    [Serialize]
    public Distance3 Distance { get; set; }

    /// <summary>Time, the denominator of the Velocity</summary>
    [Serialize]
    public TimeSpan Time { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Creates a Velocity representing the distance over the specified TimeSpan. 
    /// <br/>This is only meant to be used by this class and by Distance's (Distance / TimeSpan) operator.
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="time"></param>
    internal Velocity3(Distance3 distance, TimeSpan time)
    {
        if (time.TotalSeconds == 0)
            throw new ArgumentException("Time cannot be exactly 0");

        this.Distance = distance;
        this.Time = time;
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Velocity representing the sum of the two Velocities</returns>
    public static Velocity3 operator +(Velocity3 left, Velocity3 right)
    {
        Vector3 centimetersPerSecond = left.Distance.Centimeters / (Real)left.Time.TotalSeconds
                            + right.Distance.Centimeters / (Real)right.Time.TotalSeconds;

        return new Velocity3(Distance3.FromCentimeters(centimetersPerSecond), TimeSpan.FromSeconds(1));
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Velocity representing the sum of the two Velocities</returns>
    public static Velocity3 operator -(Velocity3 left, Velocity3 right)
    {
        return left + (-right);
    }

    /// <param name="velocity"></param>
    /// <returns>A Velocity representing the negation of velocity</returns>
    public static Velocity3 operator -(Velocity3 velocity)
    {
        return new Velocity3(-velocity.Distance, velocity.Time);
    }

    /// <param name="velocity"></param>
    /// <param name="time"></param>
    /// <returns>An Acceleration representing a change in Velocity over the time specified</returns>
    public static Acceleration3 operator /(Velocity3 velocity, TimeSpan time)
    {
        return new Acceleration3(velocity, time);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the total change in distance by this Velocity over the time specified</returns>
    public static Distance3 operator *(Velocity3 left, TimeSpan right)
    {
        Vector3 metersPerSecond = left.Distance.Meters / (Real)left.Time.TotalSeconds;
        Vector3 meters = metersPerSecond * (Real)right.TotalSeconds;
        return Distance3.FromMeters(meters);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Velocity3 other = (Velocity3)obj;
        Vector3 thisCentimetersPerSecond = this.Distance.Centimeters / (Real)this.Time.TotalSeconds;
        Vector3 otherCentimetersPerSecond = other.Distance.Centimeters / (Real)other.Time.TotalSeconds;

        Real allowableDifference = (Real)0.00001;
        Vector3 difference = (thisCentimetersPerSecond - otherCentimetersPerSecond).Absolute;
        return difference.X <= allowableDifference 
            && difference.Y <= allowableDifference 
            && difference.Z <= allowableDifference;
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
    public static bool operator ==(Velocity3 left, Velocity3 right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Velocity3 left, Velocity3 right)
    {
        return !left.Equals(right);
    }
}