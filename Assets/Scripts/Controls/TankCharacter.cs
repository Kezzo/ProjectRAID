using System.Collections.Generic;
using UnityEngine;

public class TankCharacter : BaseMeeleCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_CharacterId = "TankCharacter";

        //m_AutoInteractionCD = BaseBalancing.m_AutoAttackCd;
        //m_TimeSinceLastAutoInteraction = BaseBalancing.m_AutoAttackCd;
        m_InteractionTarget = InteractionTarget.Tank;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        };

        m_MovementSpeed = BaseBalancing.Tank.m_MovementSpeed;

        m_AutoInteractionCd = BaseBalancing.Tank.m_AutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.Tank.m_AutoAttackMaxRange;

        m_AutoAttackDamage = BaseBalancing.Tank.m_AutoAttackDamage;

        m_StatManagement.Initialize(BaseBalancing.Tank.m_BaseMaxHealth);
    }

    /// <summary>
    /// Called when an automatic attack hit.
    /// </summary>
    protected override void OnAutoAttackHit()
    {
        base.OnAutoAttackHit();
        
        Debug.Log(string.Format("OnAutoAttackHit called on: '{0}'", this.name));

        m_lastAutoAttackTarget.m_StatManagement.ChangeHealth(-m_AutoAttackDamage);

        BaseAi targetAi = m_lastAutoAttackTarget.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(this, (int)(m_AutoAttackDamage * BaseBalancing.Tank.m_AutoAttackThreatModifier));
        }
    }
}
