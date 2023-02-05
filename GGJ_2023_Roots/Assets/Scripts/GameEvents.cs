using System;

public static class GameEvents
{
    public static Action<Player> OnInteract;
    public static Action<int> OnUsedBag;
}
