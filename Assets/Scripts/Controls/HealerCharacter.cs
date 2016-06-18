public class HealerCharacter : BaseRangeCharacter
{
    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public override void InitializeBalancingParameter()
    {
        base.InitializeBalancingParameter();

        m_AutoInteractionCD = BaseBalancing.HealerAutoHealCD;
        m_TimeSinceLastAutoInteraction = BaseBalancing.HealerAutoHealCD;
    }
}
