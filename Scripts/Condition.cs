using System;

// A status condition (poison, burn, etc.) and the hooks that drive it in battle
public class Condition
{
    public ConditionID Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartMessage { get; set; }

    public Action<Pokemon> OnStart { get; set; }

    // Returns false if the pokemon cannot move this turn
    public Func<Pokemon, bool> OnBeforeMove { get; set; }

    public Action<Pokemon> OnAfterTurn { get; set; }
}
