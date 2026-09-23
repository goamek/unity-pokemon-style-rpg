using System.Collections.Generic;
using UnityEngine;

// Move data asset: power, type, accuracy, PP, and effects
[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    // Intentionally hides Object.name so the display name is editable in the Inspector
#pragma warning disable CS0108
    [SerializeField] string name;
#pragma warning restore CS0108

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PokemonType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] bool alwaysHits;
    [SerializeField] int pp;
    [SerializeField] int priority;
    [SerializeField] MoveCategory category;
    [SerializeField] MoveEffects effects;
    [SerializeField] List<SecondaryEffects> secondaries;
    [SerializeField] MoveTarget target;

    public string Name => name;
    public string Description => description;
    public PokemonType Type => type;
    public int Power => power;
    public int Accuracy => accuracy;
    public bool AlwaysHits => alwaysHits;
    public int PP => pp;
    public int Priority => priority;
    public MoveCategory Category => category;
    public MoveEffects Effects => effects;
    public List<SecondaryEffects> Secondaries => secondaries;
    public MoveTarget Target => target;
}

// Stat changes and/or a status condition applied by a move
[System.Serializable]
public class MoveEffects
{
    [SerializeField] List<StatBoost> boosts;
    [SerializeField] ConditionID status;

    public List<StatBoost> Boosts => boosts;
    public ConditionID Status => status;
}

// An effect with a percent chance to trigger after a move hits
[System.Serializable]
public class SecondaryEffects : MoveEffects
{
    [SerializeField] int chance;
    [SerializeField] MoveTarget target;

    public int Chance => chance;
    public MoveTarget Target => target;
}

[System.Serializable]
public class StatBoost
{
    public Stat stat;
    public int boost;
}

public enum MoveCategory { Physical, Special, Status }

public enum MoveTarget { Foe, Self }
