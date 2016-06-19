using System.Collections.Generic;

public class RogueCharacter : BaseMeeleCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        //m_AutoInteractionCD = BaseBalancing.m_MageAutoAttackCd;
        //m_TimeSinceLastAutoInteraction = BaseBalancing.m_MageAutoAttackCd;
        m_InteractionTarget = InteractionTarget.Rogue;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        };

        m_MovementSpeed = BaseBalancing.m_RogueMovementSpeed;

        m_AutoInteractionCD = BaseBalancing.m_RogueAutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.m_RogueAutoAttackMaxRange;
    }
}
