using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EquipmentTracker))]
public class CharacterStats : MonoBehaviour
{
    [SerializeField] int BaseStamina_PerLevel = 5;
    [SerializeField] int BaseStamina_Offset = 10;

    [SerializeField] int StaminaToHealthConversion = 10;

    [SerializeField] TextMeshProUGUI StaminaText;
    [SerializeField] TextMeshProUGUI HealthText;

    protected EquipmentTracker Equipment;

    public int BaseStamina { get; protected set; } = 0;

    public int Stamina
    {
        get
        {
            return BaseStamina + Equipment.StaminaModifier;
        }
    }

    public int MaxHealth
    {
        get
        {
            return Stamina * StaminaToHealthConversion + Equipment.HealthModifier;
        }
    }

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
        BaseStamina = BaseStamina_PerLevel * currentLevel + BaseStamina_Offset;

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
        StaminaText.text = $"Stamina: {Stamina}";
        HealthText.text = $"Max Health: {MaxHealth}";
    }
}
