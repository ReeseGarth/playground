using NUnit.Framework;

public class EnemyStateTransitionsTests
{
    [TestCase(
        EnemyState.Patrolling,
        EnemyState.Investigating
    )]
    [TestCase(
        EnemyState.Investigating,
        EnemyState.Investigating
    )]
    [TestCase(
        EnemyState.Scanning,
        EnemyState.Investigating
    )]
    [TestCase(
        EnemyState.Chasing,
        EnemyState.Chasing
    )]
    public void AfterSound_ReturnsExpectedState(
        EnemyState currentState,
        EnemyState expectedState
    )
    {
        EnemyState nextState =
            EnemyStateTransitions.AfterSound(currentState);

        Assert.That(
            nextState,
            Is.EqualTo(expectedState)
        );
    }
}