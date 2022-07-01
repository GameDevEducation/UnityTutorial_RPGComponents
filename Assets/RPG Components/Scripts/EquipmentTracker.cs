using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EEquipmentSlot
{
    Unknown     = 0,

    Feet        = 1,
    Legs        = 10,
    Waist       = 20,
    Chest       = 30,
    Arms        = 40,
    Hands       = 50,
    Neck        = 60,
    Head        = 70,
    Ring1       = 80,
    Ring2       = 90
}

public class EquipmentTracker : MonoBehaviour
{
    [SerializeField] UnityEvent<EquipmentBase> OnItemEquipped = new UnityEvent<EquipmentBase>();
    [SerializeField] UnityEvent<EquipmentBase> OnItemUnequipped = new UnityEvent<EquipmentBase>();

    [SerializeField] List<EquipmentBase> DEBUG_ToEquip;
    [SerializeField] bool DEBUG_Equip;
    [SerializeField] bool DEBUG_UnequipAll;

    public Dictionary<EEquipmentSlot, EquipmentBase> Current { get; private set; } = new Dictionary<EEquipmentSlot, EquipmentBase>();
    public int StaminaModifier { get; private set; } = 0;
    public int HealthModifier { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG_Equip)
        {
            DEBUG_Equip = false;

            // equip all the items in the list
            bool didEquipItem = false;
            foreach(var item in DEBUG_ToEquip)
            {
                if (item == null)
                    continue;

                EquipItem(item, false);

                didEquipItem = true;
            }

            if (didEquipItem)
                OnItemEquipped.Invoke(null);
        }

        if (DEBUG_UnequipAll)
        {
            DEBUG_UnequipAll = false;

            // clear the list
            Current.Clear();

            // reset the modifiers
            StaminaModifier = HealthModifier = 0;

            OnItemUnequipped.Invoke(null);
        }
    }

    public void UnequipItem(EEquipmentSlot slot)
    {
        var previousItem = Current[slot];

        // nothing to do if nothing was equipped
        if (previousItem == null)
            return;

        // remove the stamina gain from the item
        StaminaModifier -= previousItem.Stamina;
        HealthModifier -= previousItem.Health;

        OnItemUnequipped.Invoke(previousItem);
    }

    public void EquipItem(EquipmentBase itemToEquip, bool sendNotifications)
    {
        var previousItem = Current.ContainsKey(itemToEquip.Slot) ? Current[itemToEquip.Slot] : null;
        var newItem = ScriptableObject.Instantiate(itemToEquip);

        Current[itemToEquip.Slot] = newItem;

        // if there was a previous item remove its bonuses
        if (previousItem != null)
        {
            StaminaModifier -= previousItem.Stamina;
            HealthModifier -= previousItem.Health;
        }

        // add the bonuses of the new item
        StaminaModifier += newItem.Stamina;
        HealthModifier += newItem.Health;

        if (sendNotifications)
            OnItemEquipped.Invoke(newItem);
    }
}
