using System.Collections.Generic;
using UnityEngine;

// Definitions for every status condition, keyed by ConditionID
public class ConditionsDB
{
    // Copies each dictionary key onto its condition's Id
    public static void Init()
    {
        foreach (var kvp in Conditions)
            kvp.Value.Id = kvp.Key;
    }

    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn,
            new Condition()
            {
                Name = "Poison",
                StartMessage = "has been poisoned",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    // Lose 1/16 of max HP, at least 1
                    var value = Mathf.Max(1, pokemon.MaxHP / 16);
                    pokemon.UpdateHP(value);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} took damage due to poison");
                }
            }
        },
        {
            ConditionID.brn,
            new Condition()
            {
                Name = "Burn",
                StartMessage = "has been burned",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    // Lose 1/16 of max HP, at least 1
                    var value = Mathf.Max(1, pokemon.MaxHP / 16);
                    pokemon.UpdateHP(value);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} took damage due to burn");
                }
            }
        },
        {
            ConditionID.par,
            new Condition()
            {
                Name = "Paralyzed",
                StartMessage = "has been paralyzed",
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    // 25% chance to be fully paralyzed
                    if (Random.Range(1, 5) == 1)
                    {
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is paralyzed and cannot move!");
                        return false;
                    }

                    return true;
                }
            }
        },
        {
            ConditionID.frz,
            new Condition()
            {
                Name = "Frozen",
                StartMessage = "has been frozen",
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    // 25% chance to thaw each turn
                    if (Random.Range(1, 5) == 1)
                    {
                        pokemon.CureStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} thawed out!");
                        return true;
                    }

                    return false;
                }
            }
        },
        {
            ConditionID.slp,
            new Condition()
            {
                Name = "Sleep",
                StartMessage = "has fallen asleep",
                OnStart = (Pokemon pokemon) =>
                {
                    // Sleep for 1 to 3 turns
                    pokemon.StatusTime = Random.Range(1, 4);
                },
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (pokemon.StatusTime <= 0)
                    {
                        pokemon.CureStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} woke up!");
                        return true;
                    }

                    pokemon.StatusTime--;
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is sleeping");

                    return false;
                }
            }
        }
    };
}

public enum ConditionID { none, psn, brn, slp, par, frz }
