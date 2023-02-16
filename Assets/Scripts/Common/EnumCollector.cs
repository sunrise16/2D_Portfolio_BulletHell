#region GAMEOBJECT
public enum EObjectType
{
    None = -1,
    Type_Player,
    Type_Enemy,
    Type_Boss,
    Type_Bullet,
    Type_Effect,
    Type_Item,
    Type_UI,
    Type_Area,
    Max
}
#endregion

#region PLAYER
public enum EPlayerType
{
    None = -1,
    Type_A,
    Type_B,
    Max
}
public enum EPlayerWeaponType
{
    None = -1,
    Type_A,
    Type_B,
    Max
}
public enum EPlayerSpriteType
{
    None = -1,
    Type_Player_Basic,
    Type_Player_Bomb,
    Type_Player_BulletPrimary,
    Type_Player_BulletSecondary,
    Type_Player_HitPoint,
    Type_Player_Life,
    Type_Player_Secondary,
    Max
}
#endregion

#region ENEMY
public enum EEnemySpriteType
{
    None = -1,
    Type_Bomber_Green,
    Type_Bomber_Red,
    Type_Drone_Blue,
    Type_Drone_Green,
    Type_Drone_Orange,
    Type_Drone_Purple,
    Type_Heavy_Green,
    Type_Heavy_Yellow,
    Type_Scout_Red,
    Type_Scout_Yellow,
    Max
}
#endregion

#region BULLET
public enum EBulletType
{
    None = -1,
    Type_Empty,
    Type_Circle,
    Type_Capsule,
    Type_Box,
    Max
}
public enum EBulletShooterType
{
    None = -1,
    Type_Player,
    Type_Enemy,
    Max
}
public enum EPlayerBulletType
{
    None = -1,
    Type_APrimary,
    Type_ASecondary,
    Type_BPrimary,
    Type_BSecondary,
    Max
}
public enum EEnemyBulletType
{
    None = -1,
    Type_Circle_01,
    Type_Circle_02,
    Type_Circle_03,
    Type_Circle_04,
    Type_Circle_05,
    Type_Circle_06,
    Type_Circle_07,
    Type_Circle_08,
    Type_Circle_09,
    Type_SmallArrow_01,
    Type_SmallArrow_02,
    Type_SmallArrow_03,
    Type_SmallArrow_04,
    Type_SmallArrow_05,
    Type_SmallArrow_06,
    Type_SmallArrow_07,
    Type_SmallArrow_08,
    Type_SmallArrow_09,
    Type_MediumArrow_01,
    Type_MediumArrow_02,
    Type_MediumArrow_03,
    Type_MediumArrow_04,
    Type_MediumArrow_05,
    Type_MediumArrow_06,
    Type_MediumArrow_07,
    Type_MediumArrow_08,
    Type_MediumArrow_09,
    Type_LargeArrow_01,
    Type_LargeArrow_02,
    Type_LargeArrow_03,
    Type_LargeArrow_04,
    Type_LargeArrow_05,
    Type_LargeArrow_06,
    Type_LargeArrow_07,
    Type_LargeArrow_08,
    Type_LargeArrow_09,
    Type_Glow_01,
    Type_Glow_02,
    Type_Glow_03,
    Type_Glow_04,
    Type_Glow_05,
    Type_Glow_06,
    Type_Glow_07,
    Type_Glow_08,
    Type_Glow_09,
    Type_Glow_10,
    Type_Glow_11,
    Type_Glow_12,
    Type_Glow_13,
    Type_Glow_14,
    Type_Glow_15,
    Type_Glow_16,
    Type_Glow_17,
    Type_Glow_18,
    Type_Glow_19,
    Type_Glow_20,
    Max
}
#endregion

#region EFFECT
public enum EEffectType
{
    None = -1,
    Type_Enemy_Destroy_01,
    Type_Enemy_Destroy_02,
    Type_Enemy_Destroy_03,
    Type_Player_Bomb,
    Type_Player_Destroy,
    Max
}
#endregion

#region UI
public enum EUIType
{
    None = -1,
    Type_Image,
    Type_Button,
    Type_Panel,
    Type_Scrollbar,
    Type_Text,
    Type_RawImage,
    Type_Slider,
    Type_Toggle,
    Max
}
public enum EUICanvasType
{
    None = -1,
    Type_UICanvas,
    Type_BGCanvas,
    Max
}
public enum EUISpriteType
{
    None = -1,
    Type_Background_Loading,
    Type_Background_Main,
    Type_Background_Result,
    Type_GameDifficulty_EasyIllust,
    Type_GameDifficulty_HardIllust,
    Type_GameDifficulty_NormalIllust,
    Type_GameDifficulty_EasyText,
    Type_GameDifficulty_HardText,
    Type_GameDifficulty_NormalText,
    Type_Loading_Done,
    Type_Loading_NowLoading,
    Type_Main_ExitText,
    Type_Main_OptionText,
    Type_Main_StartText,
    Type_Main_TitleImage,
    Type_Option_Image,
    Type_Result_MainText,
    Type_Result_RankingText,
    Max
}
public enum EUITextFontType
{
    None = -1,
    Type_Custom1,
    Type_NotoSansKRBlack,
    Type_NotoSansKRBold,
    Type_NotoSansKRLight,
    Type_NotoSansKRMedium,
    Type_NotoSansKRRegular,
    Type_NotoSansKRThin,
    Max
}
#endregion

#region SOUND
public enum EBGMType
{
    None = -1,
    Type_BGM01_Start,
    Type_BGM02_Phase1_10,
    Type_BGM03_Phase11_20,
    Type_BGM04_Phase21_30,
    Type_BGM05_Phase31_40,
    Type_BGM06_Phase41_50,
    Type_BGM07_Phase51_60,
    Type_BGM08_Phase61_70,
    Type_BGM09_Phase71_80,
    Type_BGM10_Phase81_90,
    Type_BGM11_Phase91_100,
    Type_BGM12_Phase101_110,
    Type_BGM13_Phase111_120,
    Type_BGM14_Phase121_130,
    Type_BGM15_Phase131_140,
    Type_BGM16_Phase141_150,
    Type_BGM17_Phase151_160,
    Type_BGM18_Phase161_170,
    Type_BGM19_Phase171_180,
    Type_BGM20_Phase181_190,
    Type_BGM21_Phase191_200,
    Type_BGM22_Phase201,
    Type_BGM23_StaffRoll,
    Type_BGM24_Result,
    Max
}
public enum ESFXType
{
    None = -1,
    Type_SFX_Bonus,
    Type_SFX_Cancel,
    Type_SFX_EnemyAttack1,
    Type_SFX_EnemyAttack2,
    Type_SFX_EnemyAttack3,
    Type_SFX_EnemyDamage1,
    Type_SFX_EnemyDamage2,
    Type_SFX_EnemyDestroy1,
    Type_SFX_EnemyDestroy2,
    Type_SFX_Item,
    Type_SFX_OptionChange,
    Type_SFX_Pause,
    Type_SFX_PhaseUp,
    Type_SFX_PlayerAttack,
    Type_SFX_PlayerBomb,
    Type_SFX_PlayerDestroy,
    Type_SFX_Select,
    Type_SFX_TimeWarning,
    Type_SFX_ValueChange,
    Max
}
#endregion

#region SYSTEM
public enum ESceneType
{
    None = -1,
    Type_LoadingScene,
    Type_MainScene,
    Type_GameScene,
    Type_ResultScene,
    Max
}
public enum EGameDifficultyType
{
    None = -1,
    Type_Easy,
    Type_Normal,
    Type_Hard,
    Max
}
#endregion