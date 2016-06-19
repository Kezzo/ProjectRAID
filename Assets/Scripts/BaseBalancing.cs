public static class BaseBalancing
{
    //TODO: Add subclasses for different balancing categories.
    //public static float m_CharacterSpeed = 5f; obsolete

    #region Tank

    public static float m_TankMovementSpeed = 8f;
    public static float m_TankAutoAttackCd = 0.5f;
    public static float m_TankAutoAttackMaxRange = 5f;

    #endregion

    #region Rogue

    public static float m_RogueMovementSpeed = 10f;
    public static float m_RogueAutoAttackCd = 0.5f;
    public static float m_RogueAutoAttackMaxRange = 5f;

    #endregion

    #region Healer

    public static float m_HealerMovementSpeed = 5f;
    public static float m_HealerAutoHealCd = 1f;
    public static float m_HealerAutoHealMaxRange = 10f;
    public static float m_HealerAutoHealCollisionDistance = 0.5f;
    public static float m_HealerAutoHealProjectileSpeed = 15f;

    #endregion

    #region Mage

    public static float m_MageMovementSpeed = 5f;
    public static float m_MageAutoAttackCd = 1f;
    public static float m_MageAutoAttackMaxRange = 17f;
    public static float m_MageAutoAttackCollisionDistance = 0.5f;
    public static float m_MageAutoAttackProjectileSpeed = 7f;

    #endregion
}
