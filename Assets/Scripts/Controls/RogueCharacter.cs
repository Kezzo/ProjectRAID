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

        //m_AutoInteractionCD = BaseBalancing.m_AutoAttackCd;
        //m_TimeSinceLastAutoInteraction = BaseBalancing.m_AutoAttackCd;
        m_InteractionTarget = InteractionTarget.Rogue;
        m_PossibleInteractionTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        };

        m_MovementSpeed = BaseBalancing.Rogue.m_MovementSpeed;

        m_AutoInteractionCd = BaseBalancing.Rogue.m_AutoAttackCd;
        m_AutoInteractionMaxRange = BaseBalancing.Rogue.m_AutoAttackMaxRange;

        m_StatManagement.Initialize(BaseBalancing.Rogue.m_BaseMaxHealth);
    }

    /// <summary>
    /// Called when an automatic interaction was triggered.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);

        targetToInteractWith.m_StatManagement.ChangeHealth(-BaseBalancing.Rogue.m_AutoAttackDamage);

        BaseAi targetAi = targetToInteractWith.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(this, (int)(BaseBalancing.Rogue.m_AutoAttackDamage * BaseBalancing.Rogue.m_AutoAttackThreatModifier));
        }
    }
}
