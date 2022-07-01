using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBase : ScriptableObject
{
    [field: SerializeField] public EEquipmentSlot Slot { get; protected set; } = EEquipmentSlot.Unknown;
    [SerializeField] protected string DisplayName;
    [SerializeField] protected string Description;

    [field: SerializeField] public int Stamina { get; protected set; } = 0;
    [field: SerializeField] public int Health { get; protected set; } = 0;

    public virtual string GetShortName()
    {
        return DisplayName;
    }

    public virtual string GetStatString()
    {
        // modifies stamina but not health
        if (Stamina != 0 && Health == 0)
            return Stamina > 0 ? $"+{Stamina} Sta" : $"{Stamina} Sta";
        else if (Stamina == 0 && Health != 0) // modifies health but not stamina
            return Health > 0 ? $"+{Health} HP" : $"{Health} HP";
        else if (Stamina != 0 && Health != 0) // modifies health and stamina
        {
            return (Stamina > 0 ? $"+{Stamina} Sta" : $"{Stamina} Sta") + " " + 
                   (Health  > 0 ? $"+{Health} HP"   : $"{Health} HP");
        }

        return string.Empty;
    }

    public virtual string GetDescription()
    {
        var statString = GetStatString();

        return DisplayName + System.Environment.NewLine +
               (string.IsNullOrEmpty(statString) ? "" : (statString + System.Environment.NewLine)) + 
               Description;
    }
}
