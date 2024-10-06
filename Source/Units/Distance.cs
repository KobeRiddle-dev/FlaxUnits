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
/// Represents a distance
/// </summary>
public struct Distance
{
    private const Real metersPerKilometer = 1000;
    private const Real centimetersPerMeter = 1000;
    private const Real millimetersPerCentimeter = 10;

    private readonly Real centimeters;

    /// <summary>The Distance in kilometers</summary>
    public readonly Real Kilometers => this.Meters / metersPerKilometer;

    /// <summary>The Distance in meters</summary>
    public readonly Real Meters => this.centimeters / centimetersPerMeter;

    /// <summary>The Distance in centimeters</summary>
    public readonly Real Centimeters => this.centimeters;

    /// <summary>The Distance in millimeters</summary>
    public readonly Real Millimeters => this.centimeters * millimetersPerCentimeter;

    private Distance(Real centimeters)
    {
        this.centimeters = centimeters;
    }


    /// <param name="meters"></param>
    /// <returns>A Distance representing the specified number of meters</returns>
    public static Distance FromMeters(Real meters)
    {
        return new Distance(meters * centimetersPerMeter);
    }

    /// <param name="kilometers"></param>
    /// <returns>A Distance representing the specified number of kilometers</returns>
    public static Distance FromKilometers(Real kilometers)
    {
        return FromMeters(kilometers * metersPerKilometer);
    }

    /// <param name="centimeters"></param>
    /// <returns>A Distance representing the specified number of centimeters</returns>
    public static Distance FromCentimeters(Real centimeters)
    {
        return new Distance(centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the sum of the two Distances</returns>
    public static Distance operator +(Distance left, Distance right)
    {
        return new Distance(left.centimeters + right.centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the result of the right distance subtracted from the left</returns>
    public static Distance operator -(Distance left, Distance right)
    {
        return new Distance(left.centimeters - right.centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the product of the two Distances</returns>
    public static Distance operator *(Distance left, Distance right)
    {
        return new Distance(left.centimeters * right.centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the quotient of the two Distances</returns>
    public static Distance operator /(Distance left, Distance right)
    {
        return new Distance(left.centimeters / right.centimeters);
    }

    /// <param name="distance"></param>
    /// <returns>A Distance representing a negation of the distance</returns>
    public static Distance operator -(Distance distance)
    {
        return new Distance(-distance.centimeters);
    }

    /// <param name="distance"></param>
    /// <param name="time"></param>
    /// <returns>A Velocity representing a change in distance over the specified time</returns>
    public static Velocity operator /(Distance distance, TimeSpan time)
    {
        return new Velocity(distance, time);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Distance other = (Distance)obj;
        Real allowableDifference = (Real)0.00001;
        return Mathr.Abs(this.centimeters - other.centimeters) <= allowableDifference;
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return this.centimeters.GetHashCode();
    }

    /// <summary>Equivalent to left.Equals(right)</summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>True if left and right are both distances and are of equal length, false otherwise</returns>
    public static bool operator ==(Distance left, Distance right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Distance left, Distance right)
    {
        return !left.Equals(right);
    }

    
}
