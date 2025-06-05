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

namespace Units.Vectors;

/// <summary>
/// Represents a physical force
/// </summary>
[Serializable]
public struct Force3
{
    [Serialize]
    private readonly Vector3 newtons;

    /// <summary>
    /// Creates a Force.
    /// <br/> This is only meant to be used in this struct and by Mass's (Mass * Acceleration) operator.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    internal Force3(Mass mass, Acceleration3 acceleration)
    {
        this.newtons = (Real)mass.Kilograms * acceleration.Distance.Meters / (Real)acceleration.TimeSquared.TotalSeconds;
    }

    private Force3(Vector3 newtons)
    {
        this.newtons = newtons;
    }

    /// <param name="newtons"></param>
    /// <returns>A force</returns>
    public static Force3 FromNewtons(Vector3 newtons)
    {
        return new Force3(newtons);
    }

    public readonly Force Length
    {
        get { return Force.FromNewtons(this.newtons.Length); }
    }

    /// <summary>
    /// The Force in Newtons. 
    /// <br/>1 N = 1 kg * m / s²
    /// </summary>
    public readonly Vector3 Newtons
    {
        get { return this.newtons; }
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Force representing the sum of left and right</returns>
    public static Force3 operator +(Force3 left, Force3 right)
    {
        return Force3.FromNewtons(left.newtons + right.newtons);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A Force representing the result of the right Force subtracted from the left</returns>
    public static Force3 operator -(Force3 left, Force3 right)
    {
        return Force3.FromNewtons(left.newtons - right.newtons);
    }

    /// <param name="force"></param>
    /// <param name="mass"></param>
    /// <returns>An acceleration representing force / mass</returns>
    public static Acceleration3 operator /(Force3 force, Mass mass)
    {
        Vector3 metersPerSecondSquared = force.newtons / (Real)mass.Kilograms;
        return Distance3.FromMeters(metersPerSecondSquared) / TimeSpan.FromSeconds(1) / TimeSpan.FromSeconds(1);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Force3 other = (Force3)obj;
        Real allowableDifference = (Real)0.00001;

        Vector3 difference = (this.newtons - other.newtons).Absolute;
        return difference.X <= allowableDifference
            && difference.Y <= allowableDifference
            && difference.Z <= allowableDifference;
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
    public static bool operator ==(Force3 left, Force3 right)
    {
        return left.Equals(right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>False if left == right, true otherwise</returns>
    public static bool operator !=(Force3 left, Force3 right)
    {
        return !left.Equals(right);
    }
}