using System.Collections.Generic;

public class TestBossSkill1BloodPool : BaseAreaEffect
{
    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        m_EffectDamage = BaseBalancing.TestBossSkill1.m_Damage;
        m_EffectRange = BaseBalancing.TestBossSkill1.m_EffectRange;

        m_EffectActivationDelay = BaseBalancing.TestBossSkill1.m_EffectActivationDelay;

        m_PossibleTargets = BaseBalancing.TestBossSkill1.m_PossibleTargets;
    }

    /// <summary>
    /// Damages all given characters.
    /// </summary>
    /// <param name="enemiesToApplyEffectOn">The enemies to damage.</param>
    protected override void OnEffectTrigger(List<BaseCharacter> enemiesToApplyEffectOn)
    {
        base.OnEffectTrigger(enemiesToApplyEffectOn);

        for (int enemyIndex = 0; enemyIndex < enemiesToApplyEffectOn.Count; enemyIndex++)
        {
            enemiesToApplyEffectOn[enemyIndex].m_StatManagement.ChangeHealth(-m_EffectDamage);
        }

        Destroy(this.gameObject);
    }
}
