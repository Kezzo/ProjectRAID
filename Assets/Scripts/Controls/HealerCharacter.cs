using System.Collections.Generic;

public class HealerCharacter : BaseRangeCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_InteractionTarget = InteractionTarget.Heal;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Mage,
            InteractionTarget.Rogue,
            InteractionTarget.Tank
        };

        m_AutoInteractionCD = BaseBalancing.m_HealerAutoHealCd;
        m_TimeSinceLastAutoInteraction = BaseBalancing.m_HealerAutoHealCd;
        
    }
}
