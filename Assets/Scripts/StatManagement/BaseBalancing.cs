using System.Collections.Generic;

public static class BaseBalancing
{
    //TODO: Add subclasses for different balancing categories.
    //public static float m_CharacterSpeed = 5f; obsolete

    public static float m_TargetChangeByThreatDifference = 1.2f;

    #region Tank

    public struct Tank
    {
        public static float m_MovementSpeed = 8f;
        public static float m_AutoAttackCd = 0.5f;
        public static float m_AutoAttackMaxRange = 5f;

        public static int m_AutoAttackDamage = 2;
        public static float m_AutoAttackThreatModifier = 6.0f;

        public static int m_BaseMaxHealth = 100;
    }

    #endregion

    #region Rogue

    public struct Rogue
    {
        public static float m_MovementSpeed = 10f;
        public static float m_AutoAttackCd = 0.5f;
        public static float m_AutoAttackMaxRange = 3f;

        public static int m_AutoAttackDamage = 5;
        public static float m_AutoAttackThreatModifier = 1.0f;

        public static int m_BaseMaxHealth = 50;
    }

    #endregion

    #region Healer

    public struct Healer
    {
        public static float m_MovementSpeed = 5f;

        public static float m_AutoHealCd = 0.5f;
        public static float m_AutoAttackAnimationSpeed = 2f;

        public static float m_AutoHealMaxRange = 10f;
        public static float m_AutoHealCollisionDistance = 0.5f;
        public static float m_AutoHealProjectileSpeed = 15f;

        public static int m_AutoHealValue = 10;
        public static float m_AutoHealThreatModifier = 0.2f;

        public static int m_BaseMaxHealth = 40;
    }

    #endregion

    #region Mage

    public struct Mage
    {
        public static float m_MovementSpeed = 5f;
        public static float m_AutoAttackCd = 1f;
        public static float m_AutoAttackAnimationSpeed = 1f;

        public static float m_AutoAttackMaxRange = 17f;
        public static float m_AutoAttackCollisionDistance = 0.5f;
        public static float m_AutoAttackProjectileSpeed = 20f;

        public static int m_AutoAttackProjectileDamage = 10;
        public static float m_AutoAttackProjectileThreatModifier = 1.0f;

        public static int m_BaseMaxHealth = 30;
    }

    #endregion

    #region Enemies

    public struct GeneralEnemy
    {
        public static float m_EnemyMovementSpeed = 3f;
    }

    #region TestBoss

    public struct TestBoss
    {
        public static float m_AutoAttackCd = 1f;
        public static float m_AutoAttackMaxRange = 6f;

        public static int m_AutoAttackDamage = 30;

        public static int m_BaseMaxHealth = 1000;
    }

    #region Skill1

    public struct TestBossSkill1
    {
        public static int m_Damage = 25;
        public static float m_Cooldown = 5f;

        public static float m_CollisionDistance = 0.5f;
        public static float m_BossSkill1ProjectileSpeed = 20f;

        public static float m_EffectRange = 1.5f;
        public static float m_EffectActivationDelay = 1.5f;

        public static HashSet<InteractionTarget> m_PossibleTargets = new HashSet<InteractionTarget>
        {
            InteractionTarget.Tank,
            InteractionTarget.Heal,
            InteractionTarget.Mage,
            InteractionTarget.Rogue
        };
    }

    #endregion

    #endregion

    #endregion
}
