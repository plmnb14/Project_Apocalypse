using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneDetailUI : PopUpUI
{
    #region Enum
    private enum DroneDetailStats { Damage, AttackSpeed, End };
    #endregion

    #region Private Field
    private const int maxStatCount = 2;
    private const int maxStarCount = 10;
    private DroneCard targetDrone;
    private DroneCard droneClone;
    private Text titleName;
    private Text[] currentStat;
    private Text[] nextStat;
    private GameObject[] tierStarImages;
    #endregion

    #region Property Fields
    public int currentCardIndex { get; set; }
    #endregion

    #region Update Event
    public void CopyOriginDrone(ref DroneCard copyDrone)
    {
        targetDrone = copyDrone;
        droneClone.DeepCopy(ref copyDrone);
        droneClone.SetOnTeam(copyDrone.isOnTeam);

        ChangePortrait();
        ChangeTierStar();
        ChangeCurrentStatsText();
        ChangeNextStatsText();
        ChangeName();
    }

    private void ChangePortrait()
    {
        droneClone.dronePortrait.sprite = targetDrone.dronePortrait.sprite;
    }

    public void ChangeTierStar()
    {
        int currentTier = targetDrone.droneStatusForSave.tier;
        for(int i = 0; i < maxStarCount; i++)
        {
            if(currentTier > i)
            {
                tierStarImages[i].gameObject.SetActive(true);
            }
            else
            {
                tierStarImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpgradeDroneEvent()
    {
        ChangeTierStar();
        ChangeCurrentStatsText();
        ChangeNextStatsText();
    }

    public void ChangeCurrentStatsText()
    {
        currentStat[(int)DroneDetailStats.Damage].text = targetDrone.droneStatusForLocal.damagePercent.ToString();
        currentStat[(int)DroneDetailStats.AttackSpeed].text = targetDrone.droneStatusForLocal.attackSpeed.ToString();
    }

    public void ChangeNextStatsText()
    {
        int currentTier =
            targetDrone.droneStatusForSave.tier > 10 ? 10 : targetDrone.droneStatusForSave.tier;

        DataManger dataManager = DataManger.instance;
        float addDamage = dataManager.droneStatusDBList[targetDrone.cardIndex].growthDamagePercent * currentTier
            + dataManager.droneStatusDBList[targetDrone.cardIndex].damagePercent;
        float addAttackSpeed = dataManager.droneStatusDBList[targetDrone.cardIndex].growthAttackSpeed * currentTier
            + dataManager.droneStatusDBList[targetDrone.cardIndex].attackSpeed;

        nextStat[(int)DroneDetailStats.Damage].text = addDamage.ToString();
        nextStat[(int)DroneDetailStats.AttackSpeed].text = addAttackSpeed.ToString();
    }

    private void ChangeName()
    {
        titleName.text = targetDrone.droneStatusForLocal.myName;
    }
    #endregion

    #region Awake Event
    private void AwakeSetUp()
    {
        isOpenOldPopUp = true;
        droneClone = transform.GetChild(2).GetComponent<DroneCard>();
        titleName = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        currentStat = new Text[maxStatCount];
        nextStat = new Text[maxStatCount];

        GameObject statTextPanel = transform.GetChild(4).GetChild(1).gameObject;
        for(int i = 0; i < maxStatCount; i++)
        {
            currentStat[i] = statTextPanel.transform.GetChild(2 + i).GetComponent<Text>();
            nextStat[i] = statTextPanel.transform.GetChild(4 + i).GetComponent<Text>();
        }

        tierStarImages = new GameObject[maxStarCount];
        GameObject starCanvas = transform.GetChild(3).gameObject;
        for(int i = 0; i < maxStarCount; i++)
        {
            tierStarImages[i] = starCanvas.transform.GetChild(i).gameObject;
        }
    }

    private void Awake()
    {
        AwakeSetUp();
    }
    #endregion
}
