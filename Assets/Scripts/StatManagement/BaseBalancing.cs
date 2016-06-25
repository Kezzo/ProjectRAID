public static class BaseBalancing
{
    //TODO: Add subclasses for different balancing categories.
    //public static float m_CharacterSpeed = 5f; obsolete

    public static float m_TargetChangeByThreatDifference = 1.2f;

    #region Tank

    public static float m_TankMovementSpeed = 8f;
    public static float m_TankAutoAttackCd = 0.5f;
    public static float m_TankAutoAttackMaxRange = 5f;

    public static int m_TankAutoAttackDamage = 2;
    public static float m_TankAutoAttackThreatModifier = 3.0f;

    public static int m_TankBaseMaxHealth = 100;

    #endregion

    #region Rogue

    public static float m_RogueMovementSpeed = 10f;
    public static float m_RogueAutoAttackCd = 0.5f;
    public static float m_RogueAutoAttackMaxRange = 5f;

    public static int m_RogueAutoAttackDamage = 5;
    public static float m_RogueAutoAttackThreatModifier = 1.0f;

    public static int m_RogueBaseMaxHealth = 50;

    #endregion

    #region Healer

    public static float m_HealerMovementSpeed = 5f;

    public static float m_HealerAutoHealCd = 0.5f;
    public static float m_HealerAutoHealMaxRange = 10f;
    public static float m_HealerAutoHealCollisionDistance = 0.5f;
    public static float m_HealerAutoHealProjectileSpeed = 15f;

    public static int m_HealerAutoHealValue = 10;
    public static float m_HealerAutoHealThreatModifier = 1.0f;

    public static int m_HealerBaseMaxHealth = 40;

    #endregion

    #region Mage

    public static float m_MageMovementSpeed = 5f;
    public static float m_MageAutoAttackCd = 1f;
    public static float m_MageAutoAttackMaxRange = 17f;
    public static float m_MageAutoAttackCollisionDistance = 0.5f;
    public static float m_MageAutoAttackProjectileSpeed = 20f;

    public static int m_MageAutoAttackProjectileDamage = 10;
    public static float m_MageAutoAttackProjectileThreatModifier = 1.0f;

    public static int m_MageBaseMaxHealth = 30;

    #endregion

    #region Enemies

    public static float m_EnemyMovementSpeed = 3f;

    #region TestBoss

    public static float m_TestBossAutoAttackCd = 1f;
    public static float m_TestBossAutoAttackMaxRange = 5f;

    public static int m_TestBossAutoAttackDamage = 15;

    public static int m_TestBossBaseMaxHealth = 1000;

    #endregion

    #endregion
}
