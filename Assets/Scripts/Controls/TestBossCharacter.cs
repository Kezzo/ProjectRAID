using System.Collections.Generic;

public class TestBossCharacter : BaseMeeleCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_CharacterId = "TestBossCharacter";

        m_InteractionTarget = InteractionTarget.Boss;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Mage,
            InteractionTarget.Rogue,
            InteractionTarget.Tank,
            InteractionTarget.Heal
        };

        m_MovementSpeed = BaseBalancing.m_EnemyMovementSpeed;

        m_AutoInteractionCD = BaseBalancing.m_TestBossAutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.m_TestBossAutoAttackMaxRange;
    }

}
