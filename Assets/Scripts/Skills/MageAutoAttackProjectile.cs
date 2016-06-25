public class MageAutoAttackProjectile : BaseProjectile
{
    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_MinHitDistance = BaseBalancing.m_MageAutoAttackCollisionDistance;
        m_ProjectileSpeed = BaseBalancing.m_MageAutoAttackProjectileSpeed;
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected override void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        base.OnTargetHit(hitTargetCharacter);

        Destroy(this.gameObject);
        hitTargetCharacter.m_StatManagement.ChangeHealth(-BaseBalancing.m_MageAutoAttackProjectileDamage);

        BaseAi targetAi = hitTargetCharacter.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(ProjectileCaster, (int)(BaseBalancing.m_MageAutoAttackProjectileDamage * BaseBalancing.m_MageAutoAttackProjectileThreatModifier));
        }
    }
}
