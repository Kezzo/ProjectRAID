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

        m_MovementSpeed = BaseBalancing.Healer.m_MovementSpeed;

        m_AutoInteractionCd = BaseBalancing.Healer.m_AutoHealCd;
        m_AutoInteractionMaxRange = BaseBalancing.Healer.m_AutoHealMaxRange;
        m_TimeSinceLastAutoInteraction = BaseBalancing.Healer.m_AutoHealCd;

        m_StatManagement.Initialize(BaseBalancing.Healer.m_BaseMaxHealth);
    }
}
