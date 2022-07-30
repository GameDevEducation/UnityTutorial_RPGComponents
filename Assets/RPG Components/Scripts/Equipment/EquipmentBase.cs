using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemQuality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public abstract class EquipmentBase : ScriptableObject
{
    [field: SerializeField] public EItemQuality Quality { get; protected set; } = EItemQuality.Common;

    [field: SerializeField] public EEquipmentSlot Slot { get; protected set; } = EEquipmentSlot.Unknown;
    [SerializeField] protected string DisplayName;
    [SerializeField] protected string Description;

    [field: SerializeField] public int Stamina { get; protected set; } = 0;
    [field: SerializeField] public int Health { get; protected set; } = 0;
    [field: SerializeField] public int Strength { get; protected set; } = 0;
    [field: SerializeField] public int Dexterity { get; protected set; } = 0;
    [field: SerializeField] public int Intellect { get; protected set; } = 0;

    public virtual string GetShortName()
    {
        return DisplayName;
    }

    public virtual string GetStatString()
    {
        // TODO: Use string builder to assemble

        return string.Empty;
    }

    public virtual string GetDescription()
    {
        var statString = GetStatString();

        return DisplayName + System.Environment.NewLine +
               (string.IsNullOrEmpty(statString) ? "" : (statString + System.Environment.NewLine)) + 
               Description;
    }

    public virtual void Configure(ItemGenerator.WorkingStatBlock stats, string name, EEquipmentSlot slot)
    {
        DisplayName = name;
        Slot = slot;

        Dexterity = stats.Dexterity;
        Intellect = stats.Intellect;
        Stamina = stats.Stamina;
        Strength = stats.Strength;
    }
}
