using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [System.Serializable]
    public class QualitySetting
    {
        public EItemQuality Quality;
        public int ItemBudget_Offset;
        public int ItemBudget_Multiplier;
    }

    public class WorkingStatBlock
    {
        public int Dexterity;
        public int Intellect;
        public int Stamina;
        public int Strength;
    }

    [SerializeField] QualitySetting[] QualitySettings;
    [SerializeField] int Default_BudgetOffset = 10;
    [SerializeField] int Default_BudgetMultiplier = 10;

    [SerializeField] BaseStatProfile[] Profiles;
    [SerializeField] EEquipmentSlot[] SupportedSlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateItem(GameObject characterGO)
    {
        var allQualities = System.Enum.GetValues(typeof(EItemQuality));

        var currentLevel = characterGO.GetComponent<XPTracker>().CurrentLevel;
        var selectedQuality = (EItemQuality) allQualities.GetValue(Random.Range(0, allQualities.Length));
        var selectedProfile = Profiles[Random.Range(0, Profiles.Length)];
        var selectedSlot = SupportedSlots[Random.Range(0, SupportedSlots.Length)];

        EquipmentBase newItem = Internal_GenerateItem(currentLevel, selectedQuality, selectedSlot, selectedProfile);
        characterGO.GetComponent<EquipmentTracker>().EquipItem(newItem, true);
    }

    EquipmentBase Internal_GenerateItem(int level, EItemQuality quality, EEquipmentSlot slot, BaseStatProfile profile)
    {
        int budget = DetermineBudget(level, quality);

        // spend the budget
        WorkingStatBlock statBlock = new WorkingStatBlock();
        profile.AllocateBudget(budget, statBlock);

        string itemName = $"{quality} {slot} of {profile.GetRandomTheme()}";
        Debug.Log($"Budget is {budget} for {itemName}");

        EquipmentBase newEquipment;
        if (slot >= EEquipmentSlot.Armour_Begins && slot <= EEquipmentSlot.Armour_Ends)
            newEquipment = ScriptableObject.CreateInstance<Equipment_Armour>();
        else if (slot >= EEquipmentSlot.Weapon_Begins && slot <= EEquipmentSlot.Weapon_Ends)
            newEquipment = ScriptableObject.CreateInstance<Equipment_Weapon>();
        else
            return null;

        newEquipment.Configure(statBlock, itemName, slot);

        return newEquipment;
    }

    int DetermineBudget(int level, EItemQuality quality)
    {
        int workingMultiplier = Default_BudgetMultiplier;
        int workingOffset = Default_BudgetOffset;

        foreach(var qualitySetting in QualitySettings)
        {
            if (qualitySetting.Quality == quality)
            {
                workingMultiplier = qualitySetting.ItemBudget_Multiplier;
                workingOffset = qualitySetting.ItemBudget_Offset;
            }
        }

        return workingMultiplier * level + workingOffset;
    }
}
