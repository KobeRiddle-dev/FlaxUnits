#if USE_LARGE_WORLDS
using Real = System.Double;
using Mathr = FlaxEngine.Mathd;
#else
using Real = System.Single;
using Mathr = FlaxEngine.Mathf;
#endif

using System;
using FlaxEngine;

namespace Units.Vectors;

/// <summary>
/// Represents an acceleration value, a change in velocity over time.
/// </summary>
[Serializable]
public struct Acceleration3
{
    /// <summary>Velocity, the numerator of the Acceleration</summary>
    [Serialize]
    public Velocity3 Velocity { get; set; }

    /// <summary>Time, the denominator of the Acceleration</summary>
    [Serialize]
    public TimeSpan Time { get; set; }

    /// <summary>
    /// Creates an Acceleration representing the change in Velocity over the time specified.
    /// <br/> This is only meant to be used in this class and by Velocity's (Velocity / Time) operator.
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="time"></param>
    internal Acceleration3(Velocity3 velocity, TimeSpan time)
    {
        if (time.TotalSeconds == 0)
            throw new ArgumentException("Time cannot be exactly 0");

        this.Velocity = velocity;
        this.Time = time;
    }

    // TODO: should this instead just take in the unsquared time?

    /// <param name="distance"></param>
    /// <param name="timeSquared"></param>
    /// <returns>An Acceleration representing distance over timeSquared</returns>
    public static Acceleration3 FromDistanceAndTimeSquared(Distance3 distance, TimeSpan timeSquared)
    {
        TimeSpan time = TimeSpan.FromSeconds(Mathd.Sqrt(timeSquared.TotalSeconds));
        return (distance / time) / time;
    }

    /// <summary>
    /// The numerator, Distance, of the standard representation of Acceleration3, Distance / Time².
    /// <br/>Equivalent to Acceleration3.Velocity.Distance
    /// </summary>
    public readonly Distance3 Distance
    {
        get { return this.Velocity.Distance; }
    }

    /// <summary>
    /// The quotient, Time², of the standard representation of Acceleration3, Distance / Time²
    /// <para>this.Time² is equivalent to the this.Velocity.Time * this.Time</para>
    /// </summary>
    public readonly TimeSpan TimeSquared
    {
        get { return TimeSpan.FromSeconds(this.Velocity.Time.TotalSeconds * this.Time.TotalSeconds); }
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>An acceleration representing the sum of left and right</returns>
    public static Acceleration3 operator +(Acceleration3 left, Acceleration3 right)
    {
        Vector3 leftCentiMetersPerSecondSquared = left.Distance.Centimeters / (Real)left.TimeSquared.TotalSeconds;
        Vector3 rightCentiMetersPerSecondSquared = right.Distance.Meters / (Real)right.TimeSquared.TotalSeconds;

        return Distance3.FromCentimeters(leftCentiMetersPerSecondSquared + rightCentiMetersPerSecondSquared) / TimeSpan.FromSeconds(1) / TimeSpan.FromSeconds(1);
    }

    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    /// <returns>A force of mass * acceleration</returns>
    public static Force3 operator *(Mass mass, Acceleration3 acceleration)
    {
        return new Force3(mass, acceleration);
    }

    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    /// <returns>A force of mass * acceleration</returns>
    public static Force3 operator *(Acceleration3 acceleration, Mass mass)
    {
        return new Force3(mass, acceleration);
    }

    /// <param name="acceleration"></param>
    /// <param name="time"></param>
    /// <returns>A Velocity representing the total change in velocity over the TimeSpan specified</returns>
    public static Velocity3 operator *(Acceleration3 acceleration, TimeSpan time)
    {
        return Distance3.FromMeters(acceleration.Distance.Meters * (Real)time.TotalSeconds) / TimeSpan.FromSeconds(Mathd.Sqrt(acceleration.TimeSquared.TotalSeconds));
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Acceleration3 other = (Acceleration3)obj;
        Vector3 thisCentimetersPerSecondSquared = this.Distance.Centimeters / (Real)this.TimeSquared.TotalSeconds;
        Vector3 otherCentimetersPerSecondSquared = other.Distance.Centimeters / (Real)other.TimeSquared.TotalSeconds;

        Real allowableDifference = (Real)0.00001;
        Vector3 difference = (thisCentimetersPerSecondSquared - otherCentimetersPerSecondSquared).Absolute;
        return difference.X <= allowableDifference
            && difference.Y <= allowableDifference
            && difference.Z <= allowableDifference;
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
    public static bool operator ==(Acceleration3 left, Acceleration3 right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Acceleration3 left, Acceleration3 right)
    {
        return !left.Equals(right);
    }
}