using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] int BaseStamina_PerLevel = 5;
    [SerializeField] int BaseStamina_Offset = 10;

    [SerializeField] int StaminaToHealthConversion = 10;

    [SerializeField] TextMeshProUGUI StaminaText;
    [SerializeField] TextMeshProUGUI HealthText;

    public int BaseStamina { get; protected set; } = 0;

    public int Stamina
    {
        get
        {
            return BaseStamina;
        }
    }

    public int MaxHealth
    {
        get
        {
            return Stamina * StaminaToHealthConversion;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpdateLevel(int previousLevel, int currentLevel)
    {
        BaseStamina = BaseStamina_PerLevel * currentLevel + BaseStamina_Offset;

        StaminaText.text = $"Stamina: {Stamina}";
        HealthText.text = $"Max Health: {MaxHealth}";
    }
}
