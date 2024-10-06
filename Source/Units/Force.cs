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
/// Represents a physical force
/// </summary>
public struct Force
{
    /// <summary>The Mass component of the Force</summary>
    public Mass Mass { get; set; }

    /// <summary>The Acceleration component of the Force</summary>
    public Acceleration Acceleration { get; set; }

    /// <summary>
    /// Creates a Force.
    /// <br/> This is only meant to be used in this class and by Mass's (Mass * Acceleration) operator.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    internal Force(Mass mass, Acceleration acceleration)
    {
        this.Mass = mass;
        this.Acceleration = acceleration;
    }

    /// <summary>
    /// The Force in Newtons. 
    /// <br/>1 N = 1 kg * m / s²
    /// </summary>
    public readonly Real Newtons
    {
        get { return Mass.Kilograms * Acceleration.Distance.Meters * Acceleration.TimeSquared.Seconds; }
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Force other = (Force)obj;
        return this.Mass.Equals(other.Mass) && this.Acceleration.Equals(other.Acceleration);
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return this.Mass.GetHashCode() + this.Acceleration.GetHashCode();
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