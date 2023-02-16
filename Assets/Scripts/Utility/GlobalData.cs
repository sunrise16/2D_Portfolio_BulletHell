#region USING
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public struct UserData
{
    public string szPlayerName;
    public int iTopPhase;
    public int iTopScore;

    public UserData(string szPlayerName, int iTopPhase, int iTopScore)
    {
        this.szPlayerName = szPlayerName;
        this.iTopPhase = iTopPhase;
        this.iTopScore = iTopScore;
    }

    public int GetSize()
    {
        return Marshal.SizeOf<UserData>();
    }
}

public sealed class GlobalData
{
    #region VARIABLE
    public static string szAnimationPath = "Animations/";
    public static string szVideoPath = "Animations/Video/";
    public static string szMaterialPath = "Materials/";
    public static string szTexturePath = "Materials/Texture/";

    public static string szBulletPrefabPath = "Prefabs/Object/Bullet";
    public static string szEffectPrefabPath = "Prefabs/Object/Effect/";
    public static string szItemPrefabPath = "Prefabs/Object/Item";
    public static string szPlayerPrefabPath = "Prefabs/Object/Player";
    public static string szEnemyPrefabPath = "Prefabs/Object/Enemy";
    public static string szSoundPrefabPath = "Prefabs/Object/Sound";

    public static string szUITextPrefabPath = "Prefabs/UI/Text";
    public static string szUIImagePrefabPath = "Prefabs/UI/Image";
    public static string szUIRawImagePrefabPath = "Prefabs/UI/RawImage";
    public static string szUIButtonPrefabPath = "Prefabs/UI/Button";
    public static string szUIPanelPrefabPath = "Prefabs/UI/Panel";
    public static string szUIScrollbarPrefabPath = "Prefabs/UI/Scrollbar";
    public static string szUISliderPrefabPath = "Prefabs/UI/Slider";
    public static string szUITogglePrefabPath = "Prefabs/UI/Toggle";

    public static string szBulletSpritePath = "Sprites/Object/Bullet";
    public static string szEffectSpritePath = "Sprites/Object/Effect";
    public static string szItemSpritePath = "Sprites/Object/Item";
    public static string szPlayerSpritePath = "Sprites/Object/Player";
    public static string szEnemySpritePath = "Sprites/Object/Enemy";
    public static string szUISpritePath = "Sprites/UI";

    public static string szFontPath = "Fonts/";
    public static string szBGMClipPath = "Sounds/BGM/";
    public static string szSFXClipPath = "Sounds/SFX/";

    public static int iStartingLife = 3;
    public static int iStartingBomb = 2;
    public static int iBGMVolume = 10;
    public static int iSFXVolume = 10;

    public static UserData[] pEasyRanking = new UserData[10];
    public static UserData[] pNormalRanking = new UserData[10];
    public static UserData[] pHardRanking = new UserData[10];

    public static bool bFirstGame = true;
    #endregion

    #region COMMON METHOD
    public static bool OptionFileSave(bool bFirst = false)
    {
        try
        {
            if (bFirst.Equals(false)) File.Delete(Directory.GetCurrentDirectory() + "/Save/Save_Option.ljs");
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/Save/Save_Option.ljs", FileMode.Create);
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                bw.Write(GlobalData.bFirstGame);
                bw.Write(GlobalData.iStartingLife);
                bw.Write(GlobalData.iStartingBomb);
                bw.Write(GlobalData.iBGMVolume);
                bw.Write(GlobalData.iSFXVolume);

                bw.Close();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
    }
    public static bool RankingFileSave(EGameDifficultyType enGameDifficultyType, bool bFirst = false)
    {
        try
        {
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    if (bFirst.Equals(false)) File.Delete(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Easy.ljs");
                    Stream stream_e = new FileStream(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Easy.ljs", FileMode.Create);
                    using (BinaryWriter bw = new BinaryWriter(stream_e))
                    {
                        for (int i = 0; i < 10; ++i)
                        {
                            if (bFirst.Equals(true))
                            {
                                GlobalData.pEasyRanking[i].szPlayerName = "AAAAAA";
                                GlobalData.pEasyRanking[i].iTopPhase = 0;
                                GlobalData.pEasyRanking[i].iTopScore = 0;
                            }
                            bw.Write(bFirst.Equals(true) ? "AAAAAA" : GlobalData.pEasyRanking[i].szPlayerName);
                            bw.Write(bFirst.Equals(true) ? 0 : GlobalData.pEasyRanking[i].iTopPhase);
                            bw.Write(bFirst.Equals(true) ? 0 : GlobalData.pEasyRanking[i].iTopScore);
                        }
                        bw.Close();
                    }
                    break;
                case EGameDifficultyType.Type_Normal:
                    if (bFirst.Equals(false)) File.Delete(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Normal.ljs");
                    Stream stream_n = new FileStream(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Normal.ljs", FileMode.Create);
                    using (BinaryWriter bw = new BinaryWriter(stream_n))
                    {
                        for (int i = 0; i < 10; ++i)
                        {
                            if (bFirst.Equals(true))
                            {
                                GlobalData.pNormalRanking[i].szPlayerName = "AAAAAA";
                                GlobalData.pNormalRanking[i].iTopPhase = 0;
                                GlobalData.pNormalRanking[i].iTopScore = 0;
                            }
                            bw.Write(bFirst.Equals(true) ? "AAAAAA" : GlobalData.pNormalRanking[i].szPlayerName);
                            bw.Write(bFirst.Equals(true) ? 0 : GlobalData.pNormalRanking[i].iTopPhase);
                            bw.Write(bFirst.Equals(true) ? 0 : GlobalData.pNormalRanking[i].iTopScore);
                        }
                        bw.Close();
                    }
                    break;
                case EGameDifficultyType.Type_Hard:
                    if (bFirst.Equals(false)) File.Delete(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Hard.ljs");
                    Stream stream_h = new FileStream(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Hard.ljs", FileMode.Create);
                    using (BinaryWriter bw = new BinaryWriter(stream_h))
                    {
                        for (int i = 0; i < 10; ++i)
                        {
                            if (bFirst.Equals(true))
                            {
                                GlobalData.pHardRanking[i].szPlayerName = "AAAAAA";
                                GlobalData.pHardRanking[i].iTopPhase = 0;
                                GlobalData.pHardRanking[i].iTopScore = 0;
                            }
                            bw.Write(bFirst.Equals(true) ? "AAAAAA" : GlobalData.pHardRanking[i].szPlayerName);
                            bw.Write(bFirst.Equals(true) ? 0 : GlobalData.pHardRanking[i].iTopPhase);
                            bw.Write(bFirst.Equals(true) ? 0 : GlobalData.pHardRanking[i].iTopScore);
                        }
                        bw.Close();
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
    }
    public static bool FileLoad(bool bFirst)
    {
        try
        {
            using (var br = new BinaryReader(File.Open(Directory.GetCurrentDirectory() + "/Save/Save_Option.ljs", FileMode.Open)))
            {
                GlobalData.bFirstGame = br.ReadBoolean();
                GlobalData.iStartingLife = br.ReadInt32();
                GlobalData.iStartingBomb = br.ReadInt32();
                GlobalData.iBGMVolume = br.ReadInt32();
                GlobalData.iSFXVolume = br.ReadInt32();

                br.Close();
            }
            using (var br = new BinaryReader(File.Open(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Easy.ljs", FileMode.Open)))
            {
                for (int i = 0; i < 10; ++i)
                {
                    GlobalData.pEasyRanking[i].szPlayerName = br.ReadString();
                    GlobalData.pEasyRanking[i].iTopPhase = br.ReadInt32();
                    GlobalData.pEasyRanking[i].iTopScore = br.ReadInt32();
                }
                br.Close();
            }
            using (var br = new BinaryReader(File.Open(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Normal.ljs", FileMode.Open)))
            {
                for (int i = 0; i < 10; ++i)
                {
                    GlobalData.pNormalRanking[i].szPlayerName = br.ReadString();
                    GlobalData.pNormalRanking[i].iTopPhase = br.ReadInt32();
                    GlobalData.pNormalRanking[i].iTopScore = br.ReadInt32();
                }
                br.Close();
            }
            using (var br = new BinaryReader(File.Open(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Hard.ljs", FileMode.Open)))
            {
                for (int i = 0; i < 10; ++i)
                {
                    GlobalData.pHardRanking[i].szPlayerName = br.ReadString();
                    GlobalData.pHardRanking[i].iTopPhase = br.ReadInt32();
                    GlobalData.pHardRanking[i].iTopScore = br.ReadInt32();
                }
                br.Close();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
    }
    #endregion
}