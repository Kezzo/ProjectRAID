using System.Collections.Generic;

public class MageCharacter : BaseRangeCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_CharacterId = "MageCharacter";

        m_InteractionTarget = InteractionTarget.Mage;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        };

        m_MovementSpeed = BaseBalancing.Mage.m_MovementSpeed;

        m_AutoInteractionCd = BaseBalancing.Mage.m_AutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.Mage.m_AutoAttackMaxRange;
        m_TimeSinceLastAutoInteraction = BaseBalancing.Mage.m_AutoAttackCd;

        m_StatManagement.Initialize(BaseBalancing.Mage.m_BaseMaxHealth);
    }
}
