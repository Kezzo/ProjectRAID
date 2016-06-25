using System.Collections.Generic;

public class RogueCharacter : BaseMeeleCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_CharacterId = "RogueCharacter";

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

        m_StatManagement.Initialize(BaseBalancing.m_RogueBaseMaxHealth);
    }

    /// <summary>
    /// Called when an automatic interaction was triggered.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);

        targetToInteractWith.m_StatManagement.ChangeHealth(-BaseBalancing.m_RogueAutoAttackDamage);

        BaseAi targetAi = targetToInteractWith.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(this, (int)(BaseBalancing.m_RogueAutoAttackDamage * BaseBalancing.m_RogueAutoAttackThreatModifier));
        }
    }
}
