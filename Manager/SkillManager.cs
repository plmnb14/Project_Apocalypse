using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillActType { Active, Buff, Passive, End };

public class SkillDBLoad
{
    public SkillActType skillActType;
    public string skillDBName;
}

public class SkillManager : Singleton<SkillManager>
{
    #region Public Fields
    public readonly int maxSkillCount = 6;
    public int curSkillCount = 2;
    public SkillMenuUI skillMenuUI;
    public PlayerSkillSlot playerSkillSlot;
    #endregion

    #region Property Fields
    public Dictionary<int, SkillDB>[] skillDBDic { get; private set; }
    #endregion

    #region Awake Events
    private void AwakeSetUp()
    {
        skillDBDic = new Dictionary<int, SkillDB>[(int)SkillActType.End];
        for(int i = 0; i < (int)SkillActType.End; i++)
        {
            skillDBDic[i] = new Dictionary<int, SkillDB>();
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        AwakeSetUp();
        LoadSkillDB();
    }

    private void LoadSkillDBtoPath(SkillActType type, string path)
    {
        string defaultPath = "SkillDB/" + path;
        var skillDB = Resources.Load<SkillDB>(defaultPath);
        skillDBDic[(int)type].Add(skillDB.skillID, skillDB);
    }

    private void LoadSkillDB()
    {
        CSVReader.GetSkillDBOnCSV(
            out List<SkillDBLoad> skillDBList, "SkillDBLoadData.csv");

        int listCount = skillDBList.Count;
        for(int i = 0; i < listCount; i++)
        {
            LoadSkillDBtoPath(skillDBList[i].skillActType, skillDBList[i].skillDBName);
        }
    }
    #endregion

    #region Start Events
    private void Start()
    {
        skillMenuUI.SetSkillDBOnIcon();
        playerSkillSlot.UnlockSkillSlot();
        //다음으로 저장된 플레이어 스킬 정보 불러오기
    }
    #endregion

    #region Events
    public void MountSkill()
    {
        skillMenuUI.MountSkill();
    }

    public void SetMountMode(bool isActive = true)
    {
        bool isSimple = skillMenuUI.GetSkillInvenMode();
        if(isSimple && isActive == true)
        {
            int idx = skillMenuUI.openSkillSlotIdx;
            playerSkillSlot.SetSkillOnSlot(idx);
            PopUpManager.instance.RemovePopUp();
        }

        else
        {
            if (isActive)
            {
                skillMenuUI.SetMountMode();
            }
            else
            {
                int popUpCnt = isSimple ? 0 : 2;
                PopUpManager.instance.RemovePopUp(popUpCnt);
            }

            playerSkillSlot.SetMountMode(isActive);
        }
    }

    public SkillIcon GetSelectedSkill()
    {
        return skillMenuUI.GetSelectedSkill();
    }

    public void ExchangeSkillSlot(int slotA, int slotB)
    {
        playerSkillSlot.ExchangeSkillSlot(slotA, slotB);
    }

    public void DismountSkillSlot(int skillIndex)
    {
        playerSkillSlot.DismountSkillSlot(skillIndex);
    }

    public void DismountSkillImage(int slotIdx)
    {
        skillMenuUI.DismountSkillImage(slotIdx);
    }

    public void DismountSkillSlot()
    {
        var skillIcon = GetSelectedSkill();
        int selectSkillSlotIdex = skillIcon.mountSlotIndex;

        if (selectSkillSlotIdex == -1)
        {
            Debug.Log("장착되지 않은 슬롯입니다.");
        }
        else
        {
            playerSkillSlot.DismountSkillSlot(selectSkillSlotIdex);
        }
    }

    public void ChangeSkillStatus(ref SkillIcon skillIcon)
    {
        skillMenuUI.ChangeSkillStatus(ref skillIcon);
    }

    public void SetOpenSkillSlotIndex(int slotIndex)
    {
        skillMenuUI.openSkillSlotIdx = slotIndex;
    }
    #endregion
}
