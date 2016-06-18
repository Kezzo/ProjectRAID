using System.Collections.Generic;

public class TestBossCharacter : BaseMeeleCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_InteractionTarget = InteractionTarget.Boss;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Mage,
            InteractionTarget.Rogue,
            InteractionTarget.Tank,
            InteractionTarget.Heal
        };

        m_AutoInteractionCD = BaseBalancing.m_MageAutoAttackCd;
        m_TimeSinceLastAutoInteraction = BaseBalancing.m_MageAutoAttackCd;
    }

}
