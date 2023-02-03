using System.Collections.Generic;

public static class DataHolder
{
    public static int playerStep { get; set; }
    public static bool numberInputed { get; set; }

    public static List<string> Names = new List<string>() {
        "Sofia", "Ann", "Maria", "Arisu", "Emma", "Ivan",
        "Alex", "Jack", "Andrew", "Mark", "Li", "Minato"};

    public static bool[] IsPlayerBot = {false, true, true, true, true};
}