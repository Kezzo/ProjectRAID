public class AutoHealProjectile : BaseProjectile
{
    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_MinHitDistance = BaseBalancing.HealerAutoHealActivationDistance;
        m_ProjectileSpeed = BaseBalancing.HealerAutoHealProjectileSpeed;
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected override void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        base.OnTargetHit(hitTargetCharacter);

        Destroy(this.gameObject);
    }
}
