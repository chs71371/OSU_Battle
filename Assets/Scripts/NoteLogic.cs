using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoteLogic : MonoBehaviour
{
    [HideInInspector]
    public int showIndex;
    /// <summary>
    /// 数字编号
    /// </summary>
    [Header("数字编号")]
    public Text numText;
    /// <summary>
    /// 提示的圆环物体
    /// </summary>
    [Header("提示的圆环物体")]
    public GameObject circleTipObj;
    /// <summary>
    /// 判定开始按钮
    /// </summary>
    [Header("判定开始按钮")]
    public Button runButton;
    /// <summary>
    /// 显示的主体物体
    /// </summary>
    [Header("显示的主要物体")]
    public GameObject mainShowObj;
    /// <summary>
    /// 存放评分图片的物体数组
    /// </summary>
    [Header("存放评分图片的物体数组")]
    public GameObject[] statePointArr = new GameObject[3];
    /// <summary>
    /// 延迟时间
    /// </summary>
    public float delayTime;
    /// <summary>
    /// 判定时间
    /// </summary>
    public float startTime;
    /// <summary>
    /// 移动时间
    /// </summary>
    public float judgeTime;
    /// <summary>
    /// 销毁时间
    /// </summary>
    public float desTime;
    /// <summary>
    /// 目标值
    /// </summary>
    public float targetValue;

    /// <summary>
    /// 判定点击类型事件
    /// </summary>
    public enum eState
    {
        None,
        Delay,
        Wait,
        Operation,
        Over
    }
    public enum eScore
    {
        Fail,
        Good,
        Perfect,
    }
    /// <summary>
    /// 当前Node类型
    /// </summary>
    public LevelNoteData.eNoteType curType = LevelNoteData.eNoteType.Circle;
    /// <summary>
    /// 当前判定类型
    /// </summary>
    [HideInInspector]
    public eState curState = eState.None;
    /// <summary>
    /// 当前评分
    /// </summary>
    [HideInInspector]
    public eScore curScore = eScore.Fail;
    /// <summary>
    /// 当前UI组件打开的时间
    /// </summary>
    [HideInInspector]
    public float curTime;

    public void Start()
    {
        //通关代码的方式添加按下判定，Buton本身的OnClick需要抬起才能操作

        var eventTrigger = runButton.gameObject.AddComponent<EventTriggerListener>();
        eventTrigger.onPressDown = OnStartClick;
 
        //设置初始状态
        SetCurState(eState.Delay);

        if (numText != null)
        {
            //设置显示的序号
            numText.text = showIndex.ToString();
            numText.gameObject.transform.eulerAngles = Vector3.zero;
        }
    }
 

    public void SetCurState(eState rState)
    {
        curState = rState;
 
        switch (curState)
        {
            //界面在延迟等待的阶段处理渐入效果
            case eState.Delay:
                curTime = 0;
                var canvasGroup = gameObject.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0;
                gameObject.GetComponent<CanvasGroup>().DOFade(1, delayTime);
                break;
            //等待判定阶段，将圆圈图片做缩放动画
            case eState.Wait:
                if (curType != LevelNoteData.eNoteType.Disk)
                {
                    circleTipObj.gameObject.SetActive(true);
                    circleTipObj.transform.DOScale(1, startTime).OnComplete(() => { circleTipObj.gameObject.SetActive(false); });
                }
                break;
            //操作阶段调用虚方法，让继承的类来自定义该状态功能
            case eState.Operation:
                if (curType != LevelNoteData.eNoteType.Disk)
                {
                    circleTipObj.gameObject.SetActive(false);
                }
                OnJudgetOperation();
                break;
            //结束状态打开得分提示
            case eState.Over:
                curTime = 0;
                ShowScore();
                break;
        }
    }


    public void ShowScore()
    {
        if (mainShowObj != null)
        {
            mainShowObj.gameObject.SetActive(false);
        }

        switch (curScore)
        {
            case eScore.Good:
                statePointArr[0].gameObject.SetActive(true);
                statePointArr[0].transform.eulerAngles = Vector3.zero;
                break;
            case eScore.Perfect:
                statePointArr[1].gameObject.SetActive(true);
                statePointArr[1].transform.eulerAngles = Vector3.zero;
                break;
            case eScore.Fail:
                statePointArr[2].gameObject.SetActive(true);
                statePointArr[2].transform.eulerAngles = Vector3.zero;
                break;
        }
    }


    /// <summary>
    /// wait状态中等待点击操作
    /// </summary>
    public void OnStartClick( )
    {
        if (curState != eState.Wait)
        {
            return;
        }


        SetCurState(eState.Operation);
    }

    /// <summary>
    /// 做判定操作使用的虚函数
    /// </summary>
    public virtual void OnJudgetOperation()
    {

    }


    private void Update()
    {
        switch (curState)
        {
            case eState.Delay:
                {
                    curTime += Time.deltaTime;
                    if (curTime > delayTime)
                    {
                        curTime = 0;
                        SetCurState(eState.Wait);
                    }
                }
                break;
            case eState.Operation:
            case eState.Wait:
                {
                    curTime += Time.deltaTime;
                    if (curTime > startTime+judgeTime+0.3f)
                    {
                        curTime = 0;
                        SetCurState(eState.Over);
                    }
                }
                break;
            case eState.Over:
                {
                    curTime += Time.deltaTime;
                    if (curTime > desTime)
                    {
                        SetCurState(eState.None);
                        Destroy(gameObject);
                    }
                }
                break;
        }
    }


}
