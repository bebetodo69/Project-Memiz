using System;

public static class HudEvents
{
    // int = novo valor
    public static Action<int> OnCoinsChanged;
    public static Action<int> OnLifeChanged;
}