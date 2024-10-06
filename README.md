# README

Flax Units: units for Flax Engine.

Flax Units is a simple C# library containing definitions of various struct data types, which can be used in order to avoid confusion and improve the robustness of code. Inspired by C#'s System.TimeSpan.

## Why?

Naming things in code is hard. Naming things that have units is harder.

Consider the following code, which could appear in a character controller or some other Flax script.

```C#
void Move(Real distance)
{
    ...
}
```

How does the user know how far the object will move?

This is an improvement:

```C#
void Move(Real distanceCentimeters)
{
    ...
}
```
That's better, but now the user needs to convert whatever value they have. What if they're dealing with some sort of SI constant, and now it has to be converted?

`Distance` solves this problem.

```C#
void Move(Distance distance)
{
    ...
}
```

```C#
void MoveThings(
{
    thing.move(Distance.FromMeters(10));
}
```

Meters/second or centimeters/hour? Use a `Velocity` and this question is irrelevant.

The structs also contain conversion operators:

- `Distance / TimeSpan` => `Velocity`
- `Velocity / TimeSpan` => `Acceleration`
- `Mass * Acceleration` => `Force`

  The reverse is also supported, such as `Force / Mass` => Acceleration.
