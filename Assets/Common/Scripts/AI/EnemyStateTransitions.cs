public static class EnemyStateTransitions
{
    public static EnemyState AfterSound(
        EnemyState currentState
    )
    {
        if (currentState == EnemyState.Chasing)
        {
            return EnemyState.Chasing;
        }

        return EnemyState.Investigating;
    }
}