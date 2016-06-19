using System.Collections.Generic;

public class MageCharacter : BaseRangeCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_InteractionTarget = InteractionTarget.Mage;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        };

        m_MovementSpeed = BaseBalancing.m_MageMovementSpeed;

        m_AutoInteractionCD = BaseBalancing.m_MageAutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.m_MageAutoAttackMaxRange;
        m_TimeSinceLastAutoInteraction = BaseBalancing.m_MageAutoAttackCd;
        
    }
}
