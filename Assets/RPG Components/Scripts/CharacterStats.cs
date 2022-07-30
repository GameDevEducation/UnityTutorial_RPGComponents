using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EquipmentTracker))]
public class CharacterStats : MonoBehaviour
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] public int BaseValue = 0;
        [SerializeField] public int Multiplier = 5;
        [SerializeField] public int Offset = 10;
        [SerializeField] public TextMeshProUGUI StatDisplay;
    }

    [SerializeField] Stat Stat_Stamina;
    [SerializeField] Stat Stat_Health;

    [SerializeField] Stat Stat_Dexterity;
    [SerializeField] Stat Stat_Strength;
    [SerializeField] Stat Stat_Intellect;

    protected EquipmentTracker Equipment;

    public int BaseStamina { get; protected set; } = 0;

    public int Stamina => Stat_Stamina.BaseValue + Equipment.StaminaModifier;
    public int MaxHealth => Stamina * Stat_Health.Multiplier + Stat_Health.BaseValue + Equipment.HealthModifier;

    public int Dexterity => Stat_Dexterity.BaseValue + Equipment.DexterityModifier;
    public int Strength => Stat_Strength.BaseValue + Equipment.StrengthModifier;
    public int Intellect => Stat_Intellect.BaseValue + Equipment.IntellectModifier;

    private void Awake()
    {
        Equipment = GetComponent<EquipmentTracker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpdateLevel(int previousLevel, int currentLevel)
    {
        Stat_Stamina.BaseValue = Stat_Stamina.Multiplier * currentLevel + Stat_Stamina.Offset;
        Stat_Strength.BaseValue = Stat_Strength.Multiplier * currentLevel + Stat_Strength.Offset;
        Stat_Dexterity.BaseValue = Stat_Dexterity.Multiplier * currentLevel + Stat_Dexterity.Offset;
        Stat_Intellect.BaseValue = Stat_Intellect.Multiplier * currentLevel + Stat_Intellect.Offset;

        RefreshUI();
    }

    public void OnItemEquipped(EquipmentBase itemEquipped)
    {
        RefreshUI();
    }

    public void OnItemUnequipped(EquipmentBase itemUnequipped)
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        Stat_Stamina.StatDisplay.text = $"Stamina: {Stamina}";
        Stat_Health.StatDisplay.text = $"Max Health: {MaxHealth}";
        Stat_Strength.StatDisplay.text = $"Strength: {Strength}";
        Stat_Dexterity.StatDisplay.text = $"Dexterity: {Dexterity}";
        Stat_Intellect.StatDisplay.text = $"Intellect: {Intellect}";
    }
}
