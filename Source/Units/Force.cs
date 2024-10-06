#if USE_LARGE_WORLDS
using Real = System.Double;
using Mathr = FlaxEngine.Mathd;
#else
using Real = System.Single;
using Mathr = FlaxEngine.Mathf;
#endif

using System;
using FlaxEngine;
using System.Reflection;

namespace Units;

/// <summary>
/// Represents a physical force
/// </summary>
public struct Force
{
    private readonly float newtons;

    /// <summary>
    /// Creates a Force.
    /// <br/> This is only meant to be used in this struct and by Mass's (Mass * Acceleration) operator.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    internal Force(Mass mass, Acceleration acceleration)
    {
        this.newtons = mass.Kilograms * (float)(acceleration.Distance.Meters / acceleration.TimeSquared.TotalSeconds);
    }

    private Force(float newtons)
    {
        this.newtons = newtons;
    }

    /// <param name="newtons"></param>
    /// <returns>A force</returns>
    public static Force FromNewtons(float newtons)
    {
        return new Force(newtons);
    }

    /// <summary>
    /// The Force in Newtons. 
    /// <br/>1 N = 1 kg * m / s²
    /// </summary>
    public readonly Real Newtons
    {
        get { return this.newtons; }
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Force representing the sum of left and right</returns>
    public static Force operator +(Force left, Force right)
    {
        return Force.FromNewtons(left.newtons + right.newtons);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Force representing the result of the right Force subtracted from the left</returns>
    public static Force operator -(Force left, Force right)
    {
        return Force.FromNewtons(left.newtons - right.newtons);
    }

    /// <param name="force"></param>
    /// <param name="mass"></param>
    /// <returns>An acceleration representing force / mass</returns>
    public static Acceleration operator /(Force force, Mass mass)
    {
        Real metersPerSecondSquared = force.newtons / mass.Kilograms;
        return Distance.FromMeters(metersPerSecondSquared) / TimeSpan.FromSeconds(1) / TimeSpan.FromSeconds(1);
    }

    /// <param name="force"></param>
    /// <param name="acceleration"></param>
    /// <returns>A mass representing force / acceleration</returns>
    public static Mass operator /(Force force, Acceleration acceleration)
    {
        float kilograms = (float)(force.newtons / acceleration.Distance.Meters / acceleration.TimeSquared.TotalSeconds);
        return Mass.FromKilograms(kilograms);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Force other = (Force)obj;
        float allowableDifference = 0.00001f;

        return Mathf.Abs(this.newtons - other.newtons) <= allowableDifference;
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return this.newtons.GetHashCode();
    }

    /// <summary>Equivalent to left.Equals(right)</summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>True if left and right are both Velocities and are equal, false otherwise</returns>
    public static bool operator ==(Force left, Force right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Force left, Force right)
    {
        return !left.Equals(right);
    }
}