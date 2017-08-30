using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelNoteData
{
    /// <summary>
    /// 显示的序号
    /// </summary>
    public int showIndex;
    public enum eNoteType
    {
        Circle,
        Slider,
        Disk,
    }
    /// <summary>
    /// 当前的Note类型
    /// </summary>
    public eNoteType curType;
    /// <summary>
    /// 创建出来后的位置
    /// </summary>
    public Vector3 position;
    /// <summary>
    /// 创建出来后的角度
    /// </summary>
    public Vector3 eulerAngles;
    /// <summary>
    /// 创建时间
    /// </summary>
    public float creatTime;
    /// <summary>
    /// 延迟表现时间
    /// </summary>
    public float delayTime = 0.5f;
    /// <summary>
    /// 开始持续时间
    /// </summary>
    public float startTime = 1;
    /// <summary>
    /// 操作时间
    /// </summary>
    public float operationTime = 0;
    /// <summary>
    /// 删除时间
    /// </summary>
    public float desTime = 0.5f;
    /// <summary>
    /// 目标值
    /// </summary>
    public float targetValue = 0;
}

public class Page_GamePlay : MonoBehaviour
{

    /// <summary>
    /// 关卡信息
    /// </summary>
    public List<LevelNoteData> levelNoteList = new List<LevelNoteData>();
    /// <summary>
    /// 当前时间
    /// </summary>
    private float curTime;

    


    public void Update()
    {
        curTime += Time.deltaTime;

        if (levelNoteList.Count > 0)
        {
            if (curTime >= levelNoteList[0].creatTime)
            {
                string loadPath = "";
                switch (levelNoteList[0].curType)
                {
                    case LevelNoteData.eNoteType.Circle:
                        loadPath="assetsbundles/ui/Com_HitCircle";
                        break;
                    case LevelNoteData.eNoteType.Slider:
                        loadPath = "assetsbundles/ui/Com_Slider";
                        break;
                    case LevelNoteData.eNoteType.Disk:
                        loadPath = "assetsbundles/ui/Com_TurnAround";
                        break;
                }
                //创建出UI组件
                var  noteObj = GameObject.Instantiate(Resources.Load(loadPath)) as GameObject;
                var mNoteScript = noteObj.GetComponent<NoteLogic>();
                //设置数据
                noteObj.transform.parent = gameObject.transform;
                noteObj.transform.localPosition = levelNoteList[0].position;
                noteObj.transform.localEulerAngles = levelNoteList[0].eulerAngles;
                mNoteScript.curType = levelNoteList[0].curType;
                mNoteScript.showIndex = levelNoteList[0].showIndex;
                mNoteScript.delayTime = levelNoteList[0].delayTime;
                mNoteScript.startTime = levelNoteList[0].startTime;
                mNoteScript.judgeTime = levelNoteList[0].operationTime;
                mNoteScript.desTime = levelNoteList[0].desTime;
                mNoteScript.targetValue = levelNoteList[0].targetValue;
                levelNoteList.RemoveAt(0);
            }
        }

    }
}
