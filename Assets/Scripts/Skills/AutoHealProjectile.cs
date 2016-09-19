using System.Collections.Generic;

public class AutoHealProjectile : BaseProjectile
{
    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_MinHitDistance = BaseBalancing.Healer.m_AutoHealCollisionDistance;
        m_ProjectileSpeed = BaseBalancing.Healer.m_AutoHealProjectileSpeed;
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected override void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        base.OnTargetHit(hitTargetCharacter);

        Destroy(this.gameObject);
        hitTargetCharacter.m_StatManagement.ChangeHealth(BaseBalancing.Healer.m_AutoHealValue);

        var threatTargets = ControllerContainer.TargetingController.GetCharactersWithInteractionTarget(new HashSet<InteractionTarget>
        {
            InteractionTarget.Boss,
            InteractionTarget.Add
        });

        for (int targetIndex = 0; targetIndex < threatTargets.Count; targetIndex++)
        {
            BaseAi targetAi = threatTargets[targetIndex].GetComponent<BaseAi>();

            if (targetAi != null)
            {
                targetAi.ChangeThreat(ProjectileCaster, (int)(BaseBalancing.Healer.m_AutoHealValue * BaseBalancing.Healer.m_AutoHealThreatModifier));
            }
        }
    }
}
