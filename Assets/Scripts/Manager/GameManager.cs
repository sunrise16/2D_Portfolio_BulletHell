#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MEC;
#endregion

public partial class GameManager : UnitySingleton<GameManager>
{
    #region VARIABLE
    [HideInInspector] public Camera pMainCamera;
    [HideInInspector] public GameScene pGameScene;
    [HideInInspector] public GameObject pActiveEnemy;
    [HideInInspector] public GameObject pPlayerObject;
    [HideInInspector] public GameObject pTempObject;
    public Sprite[] pBulletSpriteArray;
    public Sprite[] pEffectSpriteArray;
    public Sprite[] pPlayerSpriteArray;
    public Sprite[] pEnemySpriteArray;
    public Sprite[] pItemSpriteArray;
    public Font[] pFontArray;
    [HideInInspector] public ESceneType enSceneType = ESceneType.None;
    [HideInInspector] public EGameDifficultyType enGameDifficultyType = EGameDifficultyType.None;
    [HideInInspector] public int iScore = 0;
    [HideInInspector] public int iTempScore = 0;
    [HideInInspector] public int iMaxScore = 0;
    [HideInInspector] public int iCurrentLife = 0;
    [HideInInspector] public int iCurrentBomb = 0;
    [HideInInspector] public int iMissCount = 0;
    [HideInInspector] public int iBombCount = 0;
    [HideInInspector] public int iDestroyCount = 0;
    [HideInInspector] public int iPhase = 0;
    [HideInInspector] public int iWaitingCount = 0;
    [HideInInspector] public float fElapsedTime = 0.0f;
    [HideInInspector] public float fPhaseTime = 0.0f;
    [HideInInspector] public bool bRunPhase = false;
    [HideInInspector] public bool bCheckEndTiming = false;
    [HideInInspector] public bool bWarningTime = false;
    [HideInInspector] public bool bAllKill = false;
    [HideInInspector] public bool bWaiting = false;
    [HideInInspector] public bool bGameOver = false;

    private List<GameObject> pTempBulletList;
    private int iTempCount = 0;
    private bool bInit = false;
    #endregion

    #region UNITY LIFE CYCLE
    void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (enSceneType.Equals(ESceneType.Type_GameScene))
        {
            if (iScore < iTempScore)
            {
                iScore += 1111;
                if (iScore > iTempScore)
                {
                    iScore = iTempScore;
                }
            }
            // if (iTempScore > iTempMaxScore)
            // {
            //     iTempMaxScore = iTempScore;
            // }
            if (iMaxScore < iScore)
            {
                iMaxScore += 1111;
                if (iMaxScore > iScore)
                {
                    iMaxScore = iScore;
                }
            }
            pGameScene.pGameScoreText.text = iScore.ToString();
            pGameScene.pGameMaxScoreText.text = iMaxScore.ToString();

            if (bGameOver.Equals(false))
            {
                fElapsedTime += Time.deltaTime;
                if (fPhaseTime > 0.0f && bRunPhase.Equals(true)) fPhaseTime -= Time.deltaTime;
                if ((fPhaseTime > 0.0f && fPhaseTime < 10.0f) && bRunPhase.Equals(true) && bWarningTime.Equals(false))
                {
                    iTempCount = 10;
                    bWarningTime = true;
                    pGameScene.pPhaseTimeText.GetComponent<Text>().color = new Color(1.0f, 0.1f, 0.1f, 1.0f);
                }
                pGameScene.pPhaseTimeText.GetComponent<Text>().text = ((int)fPhaseTime).ToString();

                if (bRunPhase.Equals(true))
                {
                    if (bCheckEndTiming.Equals(true) && pActiveEnemy.transform.childCount.Equals(0))
                    {
                        bAllKill = true;
                        Timing.RunCoroutine(Waiting(0.0f, 1.5f, false), "Waiting_Normal");
                    }
                    else if (fPhaseTime <= 0.0f)
                    {
                        fPhaseTime = 0.0f;
                        Enemy_Main pEnemyMain;

                        for (int i = 0; i < pActiveEnemy.transform.childCount; ++i)
                        {
                            pEnemyMain = pActiveEnemy.transform.GetChild(i).GetComponent<Enemy_Main>();
                            pEnemyMain.DestroyEnemy(false, false);
                        }
                        if (bAllKill.Equals(false)) Timing.RunCoroutine(Waiting(0.0f, 1.5f, true), "Waiting_TimeOut");
                    }
                }

                if (bWarningTime.Equals(true))
                {
                    if (iTempCount > (int)fPhaseTime)
                    {
                        iTempCount = (int)fPhaseTime;
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_TimeWarning);
                        pGameScene.pPhaseTimeText.GetComponent<UI_Main>().RunDelegateUIScale("Game_UIPhaseTime_Scale", new Vector2(-0.2f, -0.2f),
                            new Vector2(pGameScene.pPhaseTimeText.transform.localScale.x * 2.5f, pGameScene.pPhaseTimeText.transform.localScale.y * 2.5f), Vector2.one);
                    }
                }
            }
        }
    }
    #endregion

    #region COMMON METHOD
    public void Init(ESceneType enSceneType)
    {
        this.enSceneType = enSceneType;
        pMainCamera = GameObject.Find("MAINCAMERA").GetComponent<Camera>();
        if (enSceneType.Equals(ESceneType.Type_GameScene))
        {
            pTempBulletList = new List<GameObject>();
            pActiveEnemy = GameObject.Find("ACTIVEENEMY");
            pPlayerObject = GameObject.Find("Player");
        }

        if (bInit.Equals(true))
        {
            return;
        }

        pBulletSpriteArray = Resources.LoadAll<Sprite>(GlobalData.szBulletSpritePath);
        pEffectSpriteArray = Resources.LoadAll<Sprite>(GlobalData.szEffectSpritePath);
        pPlayerSpriteArray = Resources.LoadAll<Sprite>(GlobalData.szPlayerSpritePath);
        pEnemySpriteArray = Resources.LoadAll<Sprite>(GlobalData.szEnemySpritePath);
        pItemSpriteArray = Resources.LoadAll<Sprite>(GlobalData.szItemSpritePath);
        pFontArray = Resources.LoadAll<Font>(GlobalData.szFontPath);
        bInit = true;
    }
    public void SetPlayerInfo()
    {
        GameObject pPlayer = Instantiate(Resources.Load(GlobalData.szPlayerPrefabPath)) as GameObject;
        pPlayer.name = "Player";
        pPlayerObject = pPlayer;

        Player_Main pPlayerMain = pPlayer.GetComponent<Player_Main>();
        pPlayerMain.Init(EPlayerType.Type_A, EPlayerWeaponType.Type_A);

        Player_Base pPlayerBase = pPlayerMain.GetPlayerBase();
        pPlayerBase.SetPlayerPower(0.0f);
        pPlayerBase.SetPlayerPrimaryDamage((pPlayerBase.GetPlayerWeaponType().Equals(EPlayerWeaponType.Type_A)) ? 1.5f : 1.3f);
        pPlayerBase.SetPlayerSecondaryDamage((pPlayerBase.GetPlayerWeaponType().Equals(EPlayerWeaponType.Type_A)) ? 1.3f : 1.5f);
        pPlayerBase.SetPlayerMoveSpeed((pPlayerBase.GetPlayerWeaponType().Equals(EPlayerWeaponType.Type_A)) ? 0.1f : 0.12f, (pPlayerBase.GetPlayerWeaponType().Equals(EPlayerWeaponType.Type_A)) ? 0.03f : 0.04f);
        Timing.RunCoroutine(pPlayerMain.PlayerStart());
    }
    public void SetPlayerLife(int iLife)
    {
        iCurrentLife += iLife;
    }
    public void PhaseActive(int iCurrentPhase)
    {
        bRunPhase = true;
        if (iCurrentPhase > 0 && iCurrentPhase <= 10)
        {
            Timing.RunCoroutine(Phase1_10Active(), "Phase1_10Active");
        }
        else if (iCurrentPhase > 10 && iCurrentPhase <= 20)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 20 && iCurrentPhase <= 30)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 30 && iCurrentPhase <= 40)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 40 && iCurrentPhase <= 50)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 50 && iCurrentPhase <= 60)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 60 && iCurrentPhase <= 70)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 70 && iCurrentPhase <= 80)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 80 && iCurrentPhase <= 90)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 90 && iCurrentPhase <= 100)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 100 && iCurrentPhase <= 110)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 110 && iCurrentPhase <= 120)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 120 && iCurrentPhase <= 130)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 130 && iCurrentPhase <= 140)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 140 && iCurrentPhase <= 150)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 150 && iCurrentPhase <= 160)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 160 && iCurrentPhase <= 170)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 170 && iCurrentPhase <= 180)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 180 && iCurrentPhase <= 190)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else if (iCurrentPhase > 190 && iCurrentPhase <= 200)
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
        else
        {
            Timing.RunCoroutine(Phase11_20Active(), "Phase11_20Active");
        }
    }
    public void ClearBullet(bool bUsingBomb = false)
    {
        for (int i = 0; i < BulletManager.Instance.GetBulletPool().GetActiveBulletParent().childCount; ++i)
        {
            pTempObject = BulletManager.Instance.GetBulletPool().GetActiveBulletParent().GetChild(i).gameObject;
            if (pTempObject.GetComponent<Bullet_Main>().GetBulletBase().GetBulletShooterType().Equals(EBulletShooterType.Type_Enemy))
            {
                pTempBulletList.Add(BulletManager.Instance.GetBulletPool().GetActiveBulletParent().GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < pTempBulletList.Count; ++i)
        {
            Timing.RunCoroutine(EffectManager.Instance.ExtractEffect(pTempBulletList[i].transform.position, Vector3.zero, new Vector3(2.0f, 2.0f, 1.0f), EEffectType.Type_Enemy_Destroy_03, 1.0f), "Bullet_DestroyEffect");
            BulletManager.Instance.GetBulletPool().ReturnPool(pTempBulletList[i]);
            if (bUsingBomb.Equals(true)) iTempScore += (10 * iCurrentLife);
        }
        pTempObject = null;
        pTempBulletList.Clear();
    }
    public void PlayerBonus()
    {
        if (iCurrentLife >= 6) iTempScore += 50000;
        else
        {
            iCurrentLife++;
            UIManager.Instance.pUIDictionary[string.Format("Game_UIPlayerLife_{0}", iCurrentLife)].SetActive(true);
        }
        if (iCurrentBomb >= 6) iTempScore += 50000;
        else
        {
            iCurrentBomb++;
            UIManager.Instance.pUIDictionary[string.Format("Game_UIPlayerBomb_{0}", iCurrentBomb)].SetActive(true);
        }
    }
    #endregion

    #region IENUMRATOR
    public IEnumerator<float> Waiting(float fWaitTime1 = 1.5f, float fWaitTime2 = 0.5f, bool bClearBullet = false)
    {
        bRunPhase = false;
        bCheckEndTiming = false;
        bWarningTime = false;
        iTempCount = 0;
        if (iPhase > 0 && iPhase <= 10)
        {
            Timing.KillCoroutines("Phase1_10Active");
        }
        else if (iPhase > 10 && iPhase <= 20)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 20 && iPhase <= 30)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 30 && iPhase <= 40)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 40 && iPhase <= 50)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 50 && iPhase <= 60)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 60 && iPhase <= 70)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 70 && iPhase <= 80)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 80 && iPhase <= 90)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 90 && iPhase <= 100)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 100 && iPhase <= 110)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 110 && iPhase <= 120)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 120 && iPhase <= 130)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 130 && iPhase <= 140)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 140 && iPhase <= 150)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 150 && iPhase <= 160)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 160 && iPhase <= 170)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 170 && iPhase <= 180)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 180 && iPhase <= 190)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else if (iPhase > 190 && iPhase <= 200)
        {
            Timing.KillCoroutines("Phase11_20Active");
        }
        else
        {
            Timing.KillCoroutines("Phase11_20Active");
        }

        yield return Timing.WaitForSeconds(fWaitTime1);

        if (fPhaseTime > 0.0f) fPhaseTime = 0.0f;
        pGameScene.pPhaseTimeText.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
        if (bClearBullet.Equals(true)) ClearBullet();

        yield return Timing.WaitForSeconds(fWaitTime2);

        Timing.RunCoroutine(pGameScene.ChangePhase());
        bAllKill = false;
        bWaiting = false;
        iWaitingCount = 0;

        yield break;
    }
    public IEnumerator<float> EnemyMoveCommon(GameObject pGameObject, float fDelayTime, Vector3 vPosition, iTween.EaseType pEaseType, float fMoveTime)
    {
        yield return Timing.WaitForSeconds(fDelayTime);

        if (pGameObject != null) iTween.MoveTo(pGameObject, iTween.Hash("position", vPosition, "easetype", pEaseType, "time", fMoveTime));

        yield break;
    }

    #region ENEMY PATTERN
    #region PHASE 1-10
    public IEnumerator<float> Phase1_10Active()
    {
        int iRandomValue = UnityEngine.Random.Range(1, 6);
        
        if (iRandomValue.Equals(1))
        {
            fPhaseTime = 30.0f;

            yield return Timing.WaitForSeconds(0.5f);

            for (int i = 0; i < 7; ++i)
            {
                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2(i < 3 ? -2.5f + (2.5f * i) : -3.75f + (2.5f * (i - 3)), 5.5f),
                    Vector3.zero, new Vector2(5.0f, 5.0f), EEnemySpriteType.Type_Bomber_Green, "Enemy", 275.0f + (5.0f * (iPhase - 1)), 256.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase1_10Pattern1(pEnemy)));
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, pEnemy.transform.position.y - (i < 3 ? 3.5f : 2.5f)),
                    "easetype", iTween.EaseType.easeOutQuad, "time", 1.5f));
            
                if (i < 6) yield return Timing.WaitForSeconds(1.75f);
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(2))
        {
            fPhaseTime = 26.0f;

            yield return Timing.WaitForSeconds(0.5f);

            for (int i = 0; i < 12; ++i)
            {
                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2(-2.5f + (1.0f * (i % 6)), 5.5f),
                    Vector3.zero, new Vector2(5.0f, 5.0f), EEnemySpriteType.Type_Bomber_Red, "Enemy", 200.0f + (5.0f * (iPhase - 1)), 256.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.bLookAt = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase1_10Pattern2(pEnemy, pPlayerObject)));
                pEnemyMain.pDelegateMove = (float fDelayTime) =>
                {
                    return EnemyMoveCommon(pEnemy, 1.0f, new Vector3(pEnemy.transform.position.x, pEnemy.transform.position.y + 20.0f),
                        iTween.EaseType.easeInQuad, 4.0f);
                };
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, pEnemy.transform.position.y - ((i < 6) ? 3.0f : 4.0f)),
                    "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f));
            
                if (i < 11) yield return Timing.WaitForSeconds(1.0f);
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(3))
        {
            Vector3 vTempPosition = Vector3.zero;
            Vector3 vTempTargetPosition = Vector3.zero;

            fPhaseTime = 30.0f;

            yield return Timing.WaitForSeconds(1.5f);

            for (int i = 0; i < 9; ++i)
            {
                switch (i)
                {
                    case 0:
                        vTempPosition = new Vector3(-6.0f, 5.5f);
                        vTempTargetPosition = new Vector3(-4.5f, 2.0f);
                        break;
                    case 1:
                        vTempPosition = new Vector3(6.0f, 5.5f);
                        vTempTargetPosition = new Vector3(4.5f, 2.0f);
                        break;
                    case 2:
                        vTempPosition = new Vector3(-3.0f, 5.5f);
                        vTempTargetPosition = new Vector3(-2.25f, 2.5f);
                        break;
                    case 3:
                        vTempPosition = new Vector3(3.0f, 5.5f);
                        vTempTargetPosition = new Vector3(2.25f, 2.5f);
                        break;
                    case 4:
                        vTempPosition = new Vector3(-4.5f, 5.5f);
                        vTempTargetPosition = new Vector3(-3.375f, 2.75f);
                        break;
                    case 5:
                        vTempPosition = new Vector3(4.5f, 5.5f);
                        vTempTargetPosition = new Vector3(3.375f, 2.75f);
                        break;
                    case 6:
                        vTempPosition = new Vector3(-1.5f, 5.5f);
                        vTempTargetPosition = new Vector3(-1.125f, 2.875f);
                        break;
                    case 7:
                        vTempPosition = new Vector3(-1.5f, 5.5f);
                        vTempTargetPosition = new Vector3(1.125f, 2.875f);
                        break;
                    case 8:
                        vTempPosition = new Vector3(0.0f, 5.5f);
                        vTempTargetPosition = new Vector3(0.0f, 3.0f);
                        break;
                    default:
                        break;
                }

                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(vTempPosition, Vector3.zero, new Vector2(4.0f, 4.0f),
                    EEnemySpriteType.Type_Heavy_Green, "Enemy", 200.0f + (5.0f * (iPhase - 1)), 256.0f, true, true);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.bLookAt = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase1_10Pattern3(pEnemy, pPlayerObject)));
                pEnemyMain.pDelegateCounterPattern = Phase1_10CounterPattern1;
                iTween.MoveTo(pEnemy, iTween.Hash("position", vTempTargetPosition, "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f));

                if (i < 9) yield return Timing.WaitForSeconds(1.5f);
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(4))
        {
            float fEnemyHP = 0.0f;
            fPhaseTime = 32.0f;
            bWaiting = true;

            yield return Timing.WaitForSeconds(0.5f);

            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fEnemyHP = 1250.0f;
                    break;
                case EGameDifficultyType.Type_Normal:
                    fEnemyHP = 1500.0f;
                    break;
                case EGameDifficultyType.Type_Hard:
                    fEnemyHP = 1750.0f;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 2; ++i)
            {
                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2(0.0f, 5.5f),
                    Vector3.zero, new Vector2(5.5f, 5.5f), EEnemySpriteType.Type_Heavy_Yellow, "Enemy", fEnemyHP, 256.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.bLookAt = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pDelegateDestroy = () => { GameManager.Instance.bWaiting = false; };
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase1_10Pattern4(pEnemy, pPlayerObject)));
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, 2.5f), "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f));
                
                while (bWaiting.Equals(true)) { yield return Timing.WaitForOneFrame; }

                if (i < 1) yield return Timing.WaitForSeconds(1.0f);
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(5))
        {
            int iRepeatCount = 0;
            float fEnemyHP = 0.0f;

            if (iPhase < 4)
            {
                fPhaseTime = 20.0f;
                iRepeatCount = 8;
                fEnemyHP = 100.0f;
            }
            else if (iPhase >= 4 && iPhase < 7)
            {
                fPhaseTime = 24.0f;
                iRepeatCount = 10;
                fEnemyHP = 125.0f;
            }
            else if (iPhase >= 7 && iPhase <= 10)
            {
                fPhaseTime = 28.0f;
                iRepeatCount = 12;
                fEnemyHP = 150.0f;
            }

            yield return Timing.WaitForSeconds(0.5f);

            for (int i = 0; i < iRepeatCount; ++i)
            {
                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2((i % 2).Equals(0) ? UnityEngine.Random.Range(-4.0f, 0.0f) : UnityEngine.Random.Range(0.0f, 4.0f), 5.5f),
                    Vector3.zero, new Vector2(3.5f, 3.5f), EEnemySpriteType.Type_Drone_Blue, "Enemy", fEnemyHP, 256.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pDelegateMove = (float fDelayTime) =>
                {
                    return EnemyMoveCommon(pEnemy, fDelayTime, new Vector3(pEnemy.transform.position.x, pEnemy.transform.position.y + 20.0f),
                        iTween.EaseType.easeInQuad, 3.0f);
                };
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase1_10Pattern5(pEnemy, pPlayerObject, i, iRepeatCount - 6)));
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, 2.5f), "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f));

                if (i < iRepeatCount - 1) yield return Timing.WaitForSeconds(1.0f);
            }
            bCheckEndTiming = true;
        }
        yield break;
    }
    #endregion
    #region PHASE 11-20
    public IEnumerator<float> Phase11_20Active()
    {
        int iRandomValue = UnityEngine.Random.Range(1, 6);

        if (iRandomValue.Equals(1))
        {
            float fEnemyHP = 0.0f;
            fPhaseTime = 28.0f;

            yield return Timing.WaitForSeconds(0.5f);

            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fEnemyHP = 2000.0f;
                    break;
                case EGameDifficultyType.Type_Normal:
                    fEnemyHP = 2250.0f;
                    break;
                case EGameDifficultyType.Type_Hard:
                    fEnemyHP = 2500.0f;
                    break;
                default:
                    break;
            }

            GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2(0.0f, 5.5f),
                    Vector3.zero, new Vector2(4.0f, 4.0f), EEnemySpriteType.Type_Drone_Blue, "Enemy", fEnemyHP, 256.0f, false, false);
            pEnemy.SetActive(true);
            Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
            pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
            pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
            pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase11_20Pattern1(pEnemy)));
            iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, 2.0f), "easetype", iTween.EaseType.easeOutQuad, "time", 1.5f));

            yield return Timing.WaitForSeconds(1.5f);

            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(2))
        {
            fPhaseTime = 19.0f;

            yield return Timing.WaitForSeconds(1.0f);

            for (int i = 0; i < 20; ++i)
            {
                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2((i % 2).Equals(0) ? -5.25f : 5.25f, 4.0f), Vector3.zero,
                    new Vector2(3.5f, 3.5f), EEnemySpriteType.Type_Scout_Red, "Enemy", 42.0f, 128.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.bLookAt = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase11_20Pattern2(pEnemy, pPlayerObject)));
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x + ((i % 2).Equals(0) ? 20.0f : -20.0f),
                    pEnemy.transform.position.y), "easetype", iTween.EaseType.linear, "time", 4.75f));

                if (i < 19) yield return Timing.WaitForSeconds(0.8f);
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(3))
        {
            fPhaseTime = 22.0f;

            for (int i = 0; i < 4; ++i)
            {
                float fAngle = Utility.GetAimedAngle(new Vector3((i % 2).Equals(0) ? -5.25f : 5.25f, 4.0f), new Vector3((i % 2).Equals(0) ? 10.5f : -10.5f, -0.5f));

                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector3((i % 2).Equals(0) ? -5.25f : 5.25f, 4.0f), Vector3.zero,
                    new Vector2(3.5f, 3.5f), EEnemySpriteType.Type_Scout_Yellow, "Enemy", 250.0f + (10.0f * ((iPhase - 1) % 10)), 128.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.GetEnemyBase().SetRotationZ(fAngle + 90.0f);
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pDelegateMove = (float fDelayTime) =>
                {
                    return EnemyMoveCommon(pEnemy, fDelayTime, new Vector3((i % 2).Equals(0) ? 10.5f : -10.5f, -0.5f), iTween.EaseType.easeInQuad, 5.0f);
                };
                Timing.RunCoroutine(pEnemyMain.pDelegateMove(5.0f));
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase11_20Pattern3(pEnemy)));
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(0.0f, 2.5f), "easetype", iTween.EaseType.easeOutQuad, "time", 5.0f));

                if (i < 3) yield return Timing.WaitForSeconds(4.0f - (0.8f * i));
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(4))
        {
            fPhaseTime = 21.0f;

            for (int i = 0; i < 20; ++i)
            {
                GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector3(UnityEngine.Random.Range(-3.5f, 3.5f), 5.5f), Vector3.zero,
                    new Vector2(4.0f, 4.0f), EEnemySpriteType.Type_Bomber_Red, "Enemy", 80.0f, 192.0f, false, false);
                pEnemy.SetActive(true);
                Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
                pEnemyMain.bLookAt = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
                pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
                pEnemyMain.pDelegateMove = (float fDelayTime) =>
                {
                    return EnemyMoveCommon(pEnemy, fDelayTime, new Vector3(pEnemy.transform.position.x, 10.0f), iTween.EaseType.easeInQuad, 2.0f);
                };
                Timing.RunCoroutine(pEnemyMain.pDelegateMove(1.0f));
                pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase11_20Pattern4(pEnemy)));
                iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, UnityEngine.Random.Range(1.5f, 2.25f)),
                    "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f));

                if (i < 19) yield return Timing.WaitForSeconds(1.0f);
            }
            bCheckEndTiming = true;
        }
        else if (iRandomValue.Equals(5))
        {
            float fEnemyHP = 0.0f;
            fPhaseTime = 35.0f;

            yield return Timing.WaitForSeconds(0.5f);

            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fEnemyHP = 2500.0f;
                    break;
                case EGameDifficultyType.Type_Normal:
                    fEnemyHP = 2750.0f;
                    break;
                case EGameDifficultyType.Type_Hard:
                    fEnemyHP = 3000.0f;
                    break;
                default:
                    break;
            }

            GameObject pEnemy = EnemyManager.Instance.ExtractEnemy(new Vector2(0.0f, 5.5f), Vector3.zero, new Vector2(4.5f, 4.5f),
                EEnemySpriteType.Type_Drone_Green, "Enemy", fEnemyHP, 256.0f, false, false);
            pEnemy.SetActive(true);
            Enemy_Main pEnemyMain = pEnemy.GetComponent<Enemy_Main>();
            pEnemyMain.GetEnemyBase().GetCircleCollider().enabled = true;
            pEnemyMain.GetEnemyBase().GetCircleCollider().radius = 0.15f;
            pEnemyMain.pDelegateMove = (float fDelayTime) =>
            {
                return EnemyMoveCommon(pEnemy, fDelayTime, new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(1.25f, 2.75f)), iTween.EaseType.easeOutQuad, 1.0f);
            };
            pEnemyMain.pPatternList.Add(Timing.RunCoroutine(Phase11_20Pattern5(pEnemy, pPlayerObject)));
            iTween.MoveTo(pEnemy, iTween.Hash("position", new Vector3(pEnemy.transform.position.x, 1.5f), "easetype", iTween.EaseType.easeOutQuad, "time", 1.5f));

            yield return Timing.WaitForSeconds(1.5f);

            bCheckEndTiming = true;
        }
    }
    #endregion
    #endregion
    #endregion
}