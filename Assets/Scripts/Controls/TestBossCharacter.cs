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

        m_MovementSpeed = BaseBalancing.GeneralEnemy.m_EnemyMovementSpeed;

        m_AutoInteractionCd = BaseBalancing.TestBoss.m_AutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.TestBoss.m_AutoAttackMaxRange;

        m_AutoAttackDamage = BaseBalancing.TestBoss.m_AutoAttackDamage;

        m_StatManagement.Initialize(BaseBalancing.TestBoss.m_BaseMaxHealth);
    }
}
