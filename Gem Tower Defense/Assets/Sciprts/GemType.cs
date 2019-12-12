using System;
using UnityEngine;

public class GemType
{
    public string Name { get; }
    public Color Color { get; }

    private GemType(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    private static Color emeraldColor = new Color(34/255f,177/255f,76/255f);
    private static Color topazColor = new Color(1f, 242/255f, 0);
    private static Color rubyColor = new Color(237/255f, 28/255f, 36/255f);
    private static Color diamondColor = new Color(195/255f, 195/255f, 195/255f);
    private static Color aquamarineColor = new Color(153/255f, 217/255f, 234/255f);
    private static Color sapphireColor = new Color(63/255f, 72/255f, 204/255f);
    private static Color amethystColor = new Color(1f, 174/255f, 201/255f);
    private static Color opalColor = new Color(20/255f, 135/255f, 121/255f);

    public static GemType EMERALD => new GemType("Emerald", emeraldColor);
    public static GemType TOPAZ => new GemType("Topaz", topazColor);
    public static GemType RUBY => new GemType("Ruby", rubyColor);
    public static GemType DIAMOND => new GemType("Diamond", diamondColor);
    public static GemType AQUAMRINE => new GemType("Aquamarine", aquamarineColor);
    public static GemType SAPPHIRE => new GemType("Sapphire", sapphireColor);
    public static GemType AMETHYST => new GemType("Amethyst", amethystColor);
    public static GemType OPAL => new GemType("Opal", opalColor);

    public static GemType GetGemType(string gemType)
    {
        switch (gemType.ToUpper())
        {
            case "EMERALD": return EMERALD;
            case "TOPAZ": return TOPAZ;
            case "RUBY": return RUBY;
            case "DIAMOND": return DIAMOND;
            case "AQUAMRINE": return AQUAMRINE;
            case "SAPPHIRE": return SAPPHIRE;
            case "AMETHYST": return AMETHYST;
            case "OPAL": return OPAL;
            default: throw new Exception("\"" + gemType + "\" is not a known type of gem");
        }
    }
}
