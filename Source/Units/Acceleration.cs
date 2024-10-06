#if USE_LARGE_WORLDS
using Real = System.Double;
using Mathr = FlaxEngine.Mathd;
#else
using Real = System.Single;
using Mathr = FlaxEngine.Mathf;
#endif

using System;
using FlaxEngine;

namespace Units;

/// <summary>
/// Represents an acceleration value, a change in velocity over time.
/// </summary>
public struct Acceleration
{
    /// <summary>Velocity, the numerator of the Acceleration</summary>
    public Velocity Velocity { get; set; }

    /// <summary>Time, the denominator of the Acceleration</summary>
    public TimeSpan Time { get; set; }

    /// <summary>
    /// Creates an Acceleration representing the change in Velocity over the time specified.
    /// <br/> This is only meant to be used in this class and by Velocity's (Velocity / Time) operator.
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="time"></param>
    internal Acceleration(Velocity velocity, TimeSpan time)
    {
        this.Velocity = velocity;
        this.Time = time;
    }

    // TODO: should this instead just take in the unsquared time?
    /// <param name="distance"></param>
    /// <param name="timeSquared"></param>
    /// <returns>An Acceleration representing distance over timeSquared</returns>
    public static Acceleration FromDistanceAndTimeSquared(Distance distance, TimeSpan timeSquared)
    {
        TimeSpan time = TimeSpan.FromSeconds(Mathd.Sqrt(timeSquared.TotalSeconds));
        return (distance / time) / time;
    }

    /// <summary>
    /// The numerator, Distance, of the standard representation of Acceleration, Distance / Time².
    /// <br/>Equivalent to Acceleration.Velocity.Distance
    /// </summary>
    public readonly Distance Distance
    {
        get { return this.Velocity.Distance; }
    }

    /// <summary>
    /// The quotient, Time², of the standard representation of Acceleration, Distance / Time²
    /// <para>this.Time² is equivalent to the this.Velocity.Time * this.Time</para>
    /// </summary>
    public readonly TimeSpan TimeSquared
    {
        get { return TimeSpan.FromSeconds(this.Velocity.Time.TotalSeconds * this.Time.TotalSeconds); }
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>An acceleration representing the sum of left and right</returns>
    public static Acceleration operator +(Acceleration left, Acceleration right)
    {
        Real leftMetersPerSecondSquared = left.Distance.Meters / (Real)left.TimeSquared.TotalSeconds;
        Real rightMetersPerSecondSquared = right.Distance.Meters / (Real)right.TimeSquared.TotalSeconds;

        return Acceleration.FromDistanceAndTimeSquared(Distance.FromMeters(leftMetersPerSecondSquared + rightMetersPerSecondSquared), TimeSpan.FromSeconds(1));
    }

    /// <param name="acceleration"></param>
    /// <param name="time"></param>
    /// <returns>A Velocity representing the total change in velocity over the TimeSpan specified</returns>
    public static Velocity operator *(Acceleration acceleration, TimeSpan time)
    {
        return Distance.FromMeters(acceleration.Distance.Meters * (Real)time.TotalSeconds) / TimeSpan.FromSeconds(Mathd.Sqrt(acceleration.TimeSquared.TotalSeconds));
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Acceleration other = (Acceleration)obj;
        Real thisCentimetersPerSecondSquared = this.Distance.Centimeters / (Real)this.TimeSquared.TotalSeconds;
        Real otherCentimetersPerSecondSquared = other.Distance.Centimeters / (Real)other.TimeSquared.TotalSeconds;

        Real allowableDifference = (Real)0.00001;
        return Mathr.Abs(thisCentimetersPerSecondSquared - otherCentimetersPerSecondSquared) <= allowableDifference;
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return this.Velocity.GetHashCode() + this.Time.GetHashCode();
    }

    /// <summary>Equivalent to left.Equals(right)</summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>True if left and right are both Velocities and are equal, false otherwise</returns>
    public static bool operator ==(Acceleration left, Acceleration right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Acceleration left, Acceleration right)
    {
        return !left.Equals(right);
    }
}