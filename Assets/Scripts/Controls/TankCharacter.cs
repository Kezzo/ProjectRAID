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

        m_StatManagement.Initialize(BaseBalancing.m_TankBaseMaxHealth);
    }

    /// <summary>
    /// Called when an automatic interaction was triggered.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);
         
        targetToInteractWith.m_StatManagement.ChangeHealth(-BaseBalancing.m_TankAutoAttackDamage);

        BaseAi targetAi = targetToInteractWith.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(this, (int)(BaseBalancing.m_TankAutoAttackDamage*BaseBalancing.m_TankAutoAttackThreatModifier));
        }
    }
}
