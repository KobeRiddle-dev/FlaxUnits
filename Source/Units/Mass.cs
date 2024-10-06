using System;
using FlaxEngine;

namespace Units;

/// <summary>
/// Represents a mass.
/// </summary>
public struct Mass
{
    private const float GRAMS_PER_KILOGRAM = 1000;

    private readonly float kilograms;

    internal Mass(float kilograms)
    {
        this.kilograms = kilograms;
    }

    /// <summary>The mass in kilograms</summary>
    public readonly float Kilograms { get => this.kilograms; }

    /// <summary>The mass in grams</summary>
    public readonly float Grams { get => this.kilograms * GRAMS_PER_KILOGRAM; }

    /// <param name="kilograms"></param>
    /// <returns>A Mass with the specified mass in kilograms</returns>
    public static Mass FromKilograms(float kilograms)
    {
        return new Mass(kilograms);
    }

    /// <param name="grams"></param>
    /// <returns>A Mass with the specified mass in grams</returns>
    public static Mass FromGrams(float grams)
    {
        return new Mass(grams * GRAMS_PER_KILOGRAM);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The sum of the two masses</returns>
    public static Mass operator +(Mass left, Mass right)
    {
        return Mass.FromKilograms(left.Kilograms + right.Kilograms);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass - the right mass</returns>
    public static Mass operator -(Mass left, Mass right)
    {
        return Mass.FromKilograms(left.Kilograms - right.Kilograms);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass * the right scalar value</returns>
    public static Mass operator *(Mass left, float right)
    {
        return Mass.FromKilograms(left.Kilograms * right);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass / the right mass</returns>
    public static Mass operator /(Mass left, float right)
    {
        return Mass.FromKilograms(left.Kilograms * right);
    }

    ///<param name="mass"></param>
    /// <returns>The negation of mass</returns>
    public static Mass operator -(Mass mass)
    {
        return Mass.FromKilograms(-mass.kilograms);
    }

    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    /// <returns>A force of mass * acceleration</returns>
    public static Force operator *(Mass mass, Acceleration acceleration)
    {
        return new Force(mass, acceleration);
    }

    /// <param name="mass"></param>
    /// <param name="acceleration"></param>
    /// <returns>A force of mass * acceleration</returns>
    public static Force operator *(Acceleration acceleration, Mass mass)
    {
        return new Force(mass, acceleration);
    }

    /// <param name="obj"></param>
    /// <returns>Whether obj is a mass and is equal in mass to this.</returns>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        Mass other = (Mass)obj;
        float allowableDifference = 0.00001f;

        return Mathf.Abs(this.Kilograms - other.Kilograms) <= allowableDifference;
    }

    ///<summary>The implementation is equivalent to this.Grams.GetHashCode()</summary>
    /// <returns>The hash code of this mass</returns>
    public override readonly int GetHashCode()
    {
        return this.kilograms.GetHashCode();
    }

    /// <returns>Whether left is equal in mass to right.</returns>
    public static bool operator ==(Mass left, Mass right)
    {
        return left.Equals(right);
    }


    /// <returns>Whether left is not equal in mass to right.</returns>
    public static bool operator !=(Mass left, Mass right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return this.Kilograms.ToString();
    }
}
