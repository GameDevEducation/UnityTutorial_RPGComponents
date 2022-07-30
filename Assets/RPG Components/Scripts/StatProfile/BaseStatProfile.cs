using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Stat Profiles/Profile", fileName = "StatProfile")]
public class BaseStatProfile : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] float DexterityProportion = 0f;
    [SerializeField, Range(0f, 1f)] float IntellectProportion = 0f;
    [SerializeField, Range(0f, 1f)] float StaminaProportion = 0f;
    [SerializeField, Range(0f, 1f)] float StrengthProportion = 0f;

    [SerializeField] int DexterityCost = 0;
    [SerializeField] int IntellectCost = 0;
    [SerializeField] int StaminaCost = 0;
    [SerializeField] int StrengthCost = 0;

    [SerializeField, Range(0f, 0.25f)] float StatVariation = 0f;

    [SerializeField] float LeftOverBudgetStatScale = 0.1f;

    [SerializeField] string[] Themes;

    float Variation => 1f + Random.Range(-StatVariation, StatVariation);

    public virtual string GetRandomTheme()
    {
        return Themes[Random.Range(0, Themes.Length)];
    }

    public virtual void AllocateBudget(int budget, ItemGenerator.WorkingStatBlock stats)
    {
        float workingDexProportion = Mathf.Max(0f, DexterityProportion * Variation);
        float workingIntProportion = Mathf.Max(0f, IntellectProportion * Variation);
        float workingStaProportion = Mathf.Max(0f, StaminaProportion * Variation);
        float workingStrProportion = Mathf.Max(0f, StrengthProportion * Variation);

        float sum = workingDexProportion + workingIntProportion + workingStaProportion + workingStrProportion;

        // normalise the proportions
        workingDexProportion /= sum;
        workingIntProportion /= sum;
        workingStaProportion /= sum;
        workingStrProportion /= sum;

        // spend the budget
        stats.Dexterity = Mathf.FloorToInt(workingDexProportion * budget / DexterityCost);
        stats.Intellect = Mathf.FloorToInt(workingIntProportion * budget / IntellectCost);
        stats.Stamina   = Mathf.FloorToInt(workingStaProportion * budget / StaminaCost);
        stats.Strength  = Mathf.FloorToInt(workingStrProportion * budget / StrengthCost);

        // check how much spent
        int budgetSpent = stats.Dexterity * DexterityCost +
                          stats.Intellect * IntellectCost +
                          stats.Stamina * StaminaCost +
                          stats.Strength * StrengthCost;

        int leftoverBudget = budget - budgetSpent;
        if (leftoverBudget > 0)
        {
            stats.Dexterity += Mathf.FloorToInt(leftoverBudget * LeftOverBudgetStatScale);
            stats.Intellect += Mathf.FloorToInt(leftoverBudget * LeftOverBudgetStatScale);
            stats.Stamina   += Mathf.FloorToInt(leftoverBudget * LeftOverBudgetStatScale);
            stats.Strength  += Mathf.FloorToInt(leftoverBudget * LeftOverBudgetStatScale);
        }
    }
}
