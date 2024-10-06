using Flax.Build;

public class UnitsEditorTarget : GameProjectEditorTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for editor
        Modules.Add("Units");
        Modules.Add("UnitsEditor");
    }
}
