using UnityEngine;
using UnityEngine.UI;

public class ChancesText : MonoBehaviour
{
    public Chances currentChances;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        currentChances = Chances.CHANCES_0;
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PrintChances();
    }

    private string PrintChances()
    {
        return
            "Chances:\n" +
            "Chipped: " + currentChances.ChippedGemChance * 100 + "%\n" +
            "Flawed: " + currentChances.FlawedGemChance * 100 + "%\n" +
            "Normal: " + currentChances.NormalGemChance * 100 + "%\n" +
            "Flawless: " + currentChances.FlawlessGemChance * 100 + "%\n" +
            "Perfect: " + currentChances.PerfectGemChance * 100 + "%";
    }

    public void UpdateChancesText()
    {
        Chances upgrade = currentChances.NextUpgrade;
        currentChances = upgrade ?? throw new System.Exception("The chances are already fully upgraded!");
    }
}
