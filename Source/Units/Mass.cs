/// <summary>
/// A struct for easy unit conversion of masses.
/// </summary>
/// <param name="grams"></param>
public struct Mass
{
    private const float GramsPerKilogram = 1000;

    private readonly float grams;

    private Mass(float grams)
    {
        this.grams = grams;
    }

    /// <summary>
    /// The mass in kilograms
    /// </summary>
    public readonly float Kilograms { get => this.grams / GramsPerKilogram; }

    /// <summary>
    /// The mass in grams
    /// </summary>
    public readonly float Grams { get => this.grams; }

    /// <param name="kilograms"></param>
    /// <returns>A Mass with the specified mass in kilograms</returns>
    public static Mass FromKilograms(float kilograms)
    {
        return new Mass(kilograms * GramsPerKilogram);
    }

    /// <param name="grams"></param>
    /// <returns>A Mass with the specified mass in grams</returns>
    public static Mass FromGrams(float grams)
    {
        return new Mass(grams);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass + the right mass</returns>
    public static Mass operator +(Mass left, Mass right)
    {
        return new Mass(left.grams + right.grams);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass - the right mass</returns>
    public static Mass operator -(Mass left, Mass right)
    {
        return new Mass(left.grams - right.grams);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass * the right mass</returns>
    public static Mass operator *(Mass left, Mass right)
    {
        return new Mass(left.grams * right.grams);
    }

    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The left mass / the right mass</returns>
    public static Mass operator /(Mass left, Mass right)
    {
        return new Mass(left.grams / right.grams);
    }

    ///<param name="mass"></param>
    /// <returns>The negation of mass</returns>
    public static Mass operator -(Mass mass)
    {
        return new Mass(-mass.grams);
    }


    /// <param name="obj"></param>
    /// <returns>Whether obj is a mass and is equal in mass to this.</returns>
    public override readonly bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        return this.grams.Equals(((Mass)obj).grams);
    }

    ///<summary>The implementation is equivalent to this.Grams.GetHashCode()</summary>
    /// <returns>The hash code of this mass</returns>
    public override readonly int GetHashCode()
    {
        return this.grams.GetHashCode();
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
