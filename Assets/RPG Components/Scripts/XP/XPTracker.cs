using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class XPTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentLevelText;
    [SerializeField] TextMeshProUGUI CurrentXPText;
    [SerializeField] TextMeshProUGUI XPToNextLevelText;

    [SerializeField] BaseXPTranslation XPTranslationType;

    [SerializeField] UnityEvent<int, int> OnLevelChanged = new UnityEvent<int, int>();

    BaseXPTranslation XPTranslation;

    public int CurrentLevel => XPTranslation.CurrentLevel;

    private void Awake()
    {
        XPTranslation = ScriptableObject.Instantiate(XPTranslationType);
    }

    public void AddXP(int amount)
    {
        int previousLevel = XPTranslation.CurrentLevel;
        if (XPTranslation.AddXP(amount))
        {
            OnLevelChanged.Invoke(previousLevel, XPTranslation.CurrentLevel);
        }

        RefreshDisplays();
    }

    public void SetLevel(int level)
    {
        int previousLevel = XPTranslation.CurrentLevel;
        XPTranslation.SetLevel(level);

        if (previousLevel != XPTranslation.CurrentLevel)
        {
            OnLevelChanged.Invoke(previousLevel, XPTranslation.CurrentLevel);
        }

        RefreshDisplays();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshDisplays();

        OnLevelChanged.Invoke(0, XPTranslation.CurrentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshDisplays()
    {
        CurrentLevelText.text = $"Current Level: {XPTranslation.CurrentLevel}";
        CurrentXPText.text = $"Current XP: {XPTranslation.CurrentXP}";
        if (!XPTranslation.AtLevelCap)
            XPToNextLevelText.text = $"XP To Next Level: {XPTranslation.XPRequiredForNextLevel}";
        else
            XPToNextLevelText.text = $"XP To Next Level: At Max";
    }
}
