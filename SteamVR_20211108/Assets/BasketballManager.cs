using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 籃球場遊戲管理器
/// 偵測進球與分數管理
/// 三分區域偵測
/// </summary>
public class BasketballManager : MonoBehaviour
{
    #region 欄位
    [Header("進球區域")]
    public Vector3 positionBallIn;
    public Color colorBallIn = new Color(1, 0.2f, 0.3f, 0.3f);
    [Range(0, 10)]
    public float rangeBallIn = 3;
    [Header("三分區域")]
    public Vector3 positionThreePoint;
    public Color colorThreePoint = new Color(0.2f, 0.2f, 1, 0.3f);
    public Vector3 sizeThreePoint = new Vector3(5, 3, 10);
    #endregion

    #region 繪製圖示
    private void OnDrawGizmos()
    {
        #region 進球區域
        Gizmos.color = colorBallIn;
        Gizmos.DrawSphere(positionBallIn, rangeBallIn);
        #endregion
        #region 三分區域
        Gizmos.color = colorThreePoint;
        Gizmos.DrawCube(positionThreePoint, sizeThreePoint);
        #endregion
    }
    #endregion

    [Header("進球資料")]
    public int scoreAdd = 2;
    public int score;
    public Text textScore;

    private void Update()
    {
        CheckBallIn();
        TargetInThreePoint();
    }

    /// <summary>
    /// 偵測球是否進
    /// </summary>
    private void CheckBallIn()
    {
        Collider[] hits = Physics.OverlapSphere(positionBallIn, rangeBallIn, 1 << 3);

        if (hits.Length > 0)
        {
            score += scoreAdd;
            textScore.text = "SCORE " + score;
            hits[0].gameObject.layer = 0;           // 圖層改為 Defult 避免重複加分
        }
    }

    /// <summary>
    /// 檢查目標物 (玩家) 是否進入三分區
    /// </summary>
    private void TargetInThreePoint()
    {
        Collider[] hits = Physics.OverlapBox(positionThreePoint, sizeThreePoint / 2, Quaternion.identity, 1 << 6);

        if (hits.Length > 0) scoreAdd = 3;
        else scoreAdd = 2;
    }
}
