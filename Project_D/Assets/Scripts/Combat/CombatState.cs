namespace ProjectD.Combat
{
    public enum CombatState
    {
        Processing,
        Start,
        SelectingAbility,
        SelectingTarget,
        PlayerTurn,
        EnemyTurn,
        AddActionToQueue,
        DoActions,
        Won,
        Loss
    }
}