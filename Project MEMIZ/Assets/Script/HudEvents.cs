using System;

public static class HudEvents
{
    // int = novo valor
    public static Action<int> OnCoinsChanged;

    public static void ChangeCoins(int amount)
    {
        OnCoinsChanged?.Invoke(amount);
    }
    
    public static Action<int> OnLifeChanged;
}