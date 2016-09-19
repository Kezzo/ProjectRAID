using System.Collections.Generic;

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

        m_StatManagement.Initialize(BaseBalancing.Tank.m_BaseMaxHealth);
    }

    /// <summary>
    /// Called when an automatic interaction was triggered.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);
         
        targetToInteractWith.m_StatManagement.ChangeHealth(-BaseBalancing.Tank.m_AutoAttackDamage);

        BaseAi targetAi = targetToInteractWith.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(this, (int)(BaseBalancing.Tank.m_AutoAttackDamage*BaseBalancing.Tank.m_AutoAttackThreatModifier));
        }
    }
}
