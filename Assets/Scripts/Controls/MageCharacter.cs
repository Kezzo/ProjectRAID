﻿using System.Collections.Generic;

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

        m_AutoInteractionCD = BaseBalancing.m_MageAutoAttackCd;
        m_TimeSinceLastAutoInteraction = BaseBalancing.m_MageAutoAttackCd;
        
    }
}
