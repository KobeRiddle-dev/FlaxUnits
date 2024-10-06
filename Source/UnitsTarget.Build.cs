using Flax.Build;

public class UnitsTarget : GameProjectTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for game
        Modules.Add("Units");
    }
}
