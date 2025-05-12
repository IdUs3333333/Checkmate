using UnityEngine;

public class Box_kjy : MonoBehaviour
{
    [SerializeField] private int boxDieCount; //몇번 맞아야 부서지는지

    private Rigidbody2D rigid;
    private GameObject thisBox;

    private int boxHitCount = 0; // 몇번 맞았는지

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        thisBox = this.gameObject;
    }

    //공격을 맞을 때만 파괴되도록 코드 수정 요망
    private void OnCollisionEnter2D(Collision2D collision)//충돌 체크
    {
        Debug.Log("충돌함");
        boxHitCount++;//충돌 했음 증가
        if(boxHitCount >= boxDieCount)//파괴 조건 만족 체크
        {
            Debug.Log("파괴됨");
            Destroy(thisBox);//만족했음 박스 파괴
        }
    }
}
