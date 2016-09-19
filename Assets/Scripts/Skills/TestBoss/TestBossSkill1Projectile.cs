using UnityEngine;

public class TestBossSkill1Projectile : BaseProjectile
{
    [SerializeField]
    private GameObject m_bloodPoolPrefab;

    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_MinHitDistance = BaseBalancing.TestBossSkill1.m_CollisionDistance;
        m_ProjectileSpeed = BaseBalancing.TestBossSkill1.m_BossSkill1ProjectileSpeed;
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected override void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        base.OnTargetHit(hitTargetCharacter);
        
        //hitTargetCharacter.m_StatManagement.ChangeHealth(-BaseBalancing.m_Damage);
        GameObject projectileGameobject = Instantiate(m_bloodPoolPrefab, this.transform.position, Quaternion.identity) as GameObject;

        if (projectileGameobject != null)
        {
            BaseAreaEffect areaEffectScript = projectileGameobject.GetComponent<BaseAreaEffect>();

            if (areaEffectScript != null)
            {
                areaEffectScript.InitializeBalancingParameter();
                areaEffectScript.StartAreaEffect();
            }
        }

        Destroy(this.gameObject);
    }
}
