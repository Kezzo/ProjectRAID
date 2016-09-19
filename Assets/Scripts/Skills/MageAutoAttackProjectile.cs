public class MageAutoAttackProjectile : BaseProjectile
{
    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_MinHitDistance = BaseBalancing.Mage.m_AutoAttackCollisionDistance;
        m_ProjectileSpeed = BaseBalancing.Mage.m_AutoAttackProjectileSpeed;
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected override void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        base.OnTargetHit(hitTargetCharacter);

        Destroy(this.gameObject);
        hitTargetCharacter.m_StatManagement.ChangeHealth(-BaseBalancing.Mage.m_AutoAttackProjectileDamage);

        BaseAi targetAi = hitTargetCharacter.GetComponent<BaseAi>();

        if (targetAi != null)
        {
            targetAi.ChangeThreat(ProjectileCaster, (int)(BaseBalancing.Mage.m_AutoAttackProjectileDamage * BaseBalancing.Mage.m_AutoAttackProjectileThreatModifier));
        }
    }
}
