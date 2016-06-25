public class AutoHealProjectile : BaseProjectile
{
    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_MinHitDistance = BaseBalancing.m_HealerAutoHealCollisionDistance;
        m_ProjectileSpeed = BaseBalancing.m_HealerAutoHealProjectileSpeed;
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected override void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        base.OnTargetHit(hitTargetCharacter);

        Destroy(this.gameObject);
        hitTargetCharacter.m_StatManagement.ChangeHealth(BaseBalancing.m_HealerAutoHealValue);


        //TODO: Implement heal aggro -> surrounding mobs
        //BaseAi targetAi = hitTargetCharacter.GetComponent<BaseAi>();

        //if (targetAi != null)
        //{
        //    targetAi.ChangeThreat(ProjectileCaster, (int)(BaseBalancing.m_HealerAutoHealValue * BaseBalancing.m_HealerAutoHealThreatModifier));
        //}
    }
}
