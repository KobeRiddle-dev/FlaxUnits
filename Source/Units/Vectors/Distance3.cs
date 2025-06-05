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
/// Represents a distance
/// </summary>
[Serializable]
public struct Distance3
{
    private const Real metersPerKilometer = 1000;
    private const Real centimetersPerMeter = 100;
    private const Real millimetersPerCentimeter = 10;

    [Serialize]
    private readonly Vector3 centimeters;

    /// <summary>The Distance in kilometers</summary>
    public readonly Vector3 Kilometers => this.Meters / metersPerKilometer;

    /// <summary>The Distance in meters</summary>
    public readonly Vector3 Meters => this.centimeters / centimetersPerMeter;

    /// <summary>The Distance in centimeters</summary>
    public readonly Vector3 Centimeters => this.centimeters;

    /// <summary>The Distance in millimeters</summary>
    public readonly Vector3 Millimeters => this.centimeters * millimetersPerCentimeter;

    public readonly Distance X => Distance.FromCentimeters(this.centimeters.X);
    public readonly Distance Y => Distance.FromCentimeters(this.centimeters.Y);
    public readonly Distance Z => Distance.FromCentimeters(this.centimeters.Z);


    private Distance3(Vector3 centimeters)
    {
        this.centimeters = centimeters;
    }


    /// <param name="meters"></param>
    /// <returns>A Distance representing the specified number of meters</returns>
    public static Distance3 FromMeters(Vector3 meters)
    {
        return new Distance3(meters * centimetersPerMeter);
    }

    /// <param name="kilometers"></param>
    /// <returns>A Distance representing the specified number of kilometers</returns>
    public static Distance3 FromKilometers(Vector3 kilometers)
    {
        return FromMeters(kilometers * metersPerKilometer);
    }

    /// <param name="centimeters"></param>
    /// <returns>A Distance representing the specified number of centimeters</returns>
    public static Distance3 FromCentimeters(Vector3 centimeters)
    {
        return new Distance3(centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the sum of the two Distances</returns>
    public static Distance3 operator +(Distance3 left, Distance3 right)
    {
        return new Distance3(left.centimeters + right.centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance representing the result of the right distance subtracted from the left</returns>
    public static Distance3 operator -(Distance3 left, Distance3 right)
    {
        return new Distance3(left.centimeters - right.centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance3 representing the dot product of the two Distance3s</returns>
    public static Distance3 operator *(Distance3 left, Distance3 right)
    {
        return new Distance3(left.centimeters * right.centimeters);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Distance3 representing the quotient of the two Distance3s</returns>
    public static Distance3 operator /(Distance3 left, Distance3 right)
    {
        return new Distance3(left.centimeters / right.centimeters);
    }

    /// <param name="distance"></param>
    /// <returns>A Distance representing a negation of the distance</returns>
    public static Distance3 operator -(Distance3 distance)
    {
        return new Distance3(-distance.centimeters);
    }

    /// <param name="distance"></param>
    /// <param name="time"></param>
    /// <returns>A Velocity representing a change in distance over the specified time</returns>
    public static Velocity3 operator /(Distance3 distance, TimeSpan time)
    {
        return new Velocity3(distance, time);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Distance3 other = (Distance3)obj;
        return this.centimeters.Equals(other.centimeters);
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
    public static bool operator ==(Distance3 left, Distance3 right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Distance3 left, Distance3 right)
    {
        return !left.Equals(right);
    }


}
