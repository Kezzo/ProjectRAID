using System.Collections.Generic;

public class HealerCharacter : BaseRangeCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_CharacterId = "HealerCharacter";

        m_InteractionTarget = InteractionTarget.Heal;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Mage,
            InteractionTarget.Rogue,
            InteractionTarget.Tank,
            InteractionTarget.Heal
        };

        m_MovementSpeed = BaseBalancing.m_HealerMovementSpeed;

        m_AutoInteractionCd = BaseBalancing.m_HealerAutoHealCd;
        m_AutoInteractionMaxRange = BaseBalancing.m_HealerAutoHealMaxRange;
        m_TimeSinceLastAutoInteraction = BaseBalancing.m_HealerAutoHealCd;

        m_StatManagement.Initialize(BaseBalancing.m_HealerBaseMaxHealth);
    }
}
