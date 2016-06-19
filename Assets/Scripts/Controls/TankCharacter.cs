using System.Collections.Generic;

public class TankCharacter : BaseMeeleCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        //m_AutoInteractionCD = BaseBalancing.m_MageAutoAttackCd;
        //m_TimeSinceLastAutoInteraction = BaseBalancing.m_MageAutoAttackCd;
        m_InteractionTarget = InteractionTarget.Tank;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        };

        m_MovementSpeed = BaseBalancing.m_TankMovementSpeed;

        m_AutoInteractionCD = BaseBalancing.m_TankAutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.m_TankAutoAttackMaxRange;
    }
}
