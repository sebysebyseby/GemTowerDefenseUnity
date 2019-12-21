public class Chances
{
    public float ChippedGemChance { get; }
    public float FlawedGemChance { get; }
    public float NormalGemChance { get; }
    public float FlawlessGemChance { get; }
    public float PerfectGemChance { get; }
    public int CostToBuy { get; }
    public Chances NextUpgrade { get; }

    private Chances(
        float chipped, 
        float flawed, 
        float normal, 
        float flawless, 
        float perfect,
        int cost,
        Chances upgrade)
    {
        ChippedGemChance = chipped;
        FlawedGemChance = flawed;
        NormalGemChance = normal;
        FlawlessGemChance = flawless;
        PerfectGemChance = perfect;
        CostToBuy = cost;
        NextUpgrade = upgrade;

        float totalChance = ChippedGemChance + FlawedGemChance + NormalGemChance + FlawlessGemChance + PerfectGemChance;
        if (totalChance != 1.0f)
        {
            throw new System.Exception("Chances did not add up to 100%!");
        }
    }

    public static Chances CHANCES_0 => new Chances(1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0, CHANCES_1);
    public static Chances CHANCES_1 => new Chances(0.7f, 0.3f, 0.0f, 0.0f, 0.0f, 20, CHANCES_2);
    public static Chances CHANCES_2 => new Chances(0.5f, 0.3f, 0.2f, 0.0f, 0.0f, 170, CHANCES_3);
    public static Chances CHANCES_3 => new Chances(0.3f, 0.3f, 0.4f, 0.0f, 0.0f, 110, CHANCES_4);
    public static Chances CHANCES_4 => new Chances(0.2f, 0.3f, 0.4f, 0.1f, 0.0f, 150, CHANCES_5);
    public static Chances CHANCES_5 => new Chances(0.1f, 0.3f, 0.3f, 0.3f, 0.0f, 190, CHANCES_6);
    public static Chances CHANCES_6 => new Chances(0.0f, 0.3f, 0.3f, 0.3f, 0.1f, 230, null);
}
