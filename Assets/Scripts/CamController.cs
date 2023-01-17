using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FloatRagne
{
    public float min;
    public float max;
}

public class CamController : MonoBehaviour
{
    public float moveSpd;

    public bool scrollLock;

    public Vector2 curMousePos;
    public Vector2 preMousePos;


    public FloatRagne scrollLock_x; //x = min , y = max;
    //public float scrollLock_x;
    //public float curScroll_x;

    public FloatRagne scrollLock_y;
    //public float scrollLock_y;
    //public float curScroll_y;

    public Vector2 curScroll;

    private float screenWidth;
    private float screenHeight;



    private void CamMove()
    {
        preMousePos = curMousePos;
        curMousePos = Input.mousePosition;

        Vector3 moveVal = Vector3.zero;


        if (Input.GetKey(KeyCode.Mouse0))
        {
            //클릭하고 마우스 움직이면 그 움직임값 만큼 
            //UI나 다른 오브젝트 클릭한건 이게 안 먹혀야함.
            Vector2 dir = (curMousePos - preMousePos).normalized;

            moveVal = -1f * new Vector3(dir.x, 0f, dir.y) * Time.deltaTime * moveSpd;
        }
        else
        {
            Vector2 mousePosRatio = new Vector2(curMousePos.x / screenWidth, curMousePos.y / screenHeight);
            //curMousePos.x /= screenWidth;
            //curMousePos.y /= screenHeight;

            if (Input.GetKey(KeyCode.LeftArrow) || mousePosRatio.x <= 0.01f)
            {
                //transform.position -= Vector3.right * Time.deltaTime * moveSpd;
                moveVal = -1f * Vector3.right * Time.deltaTime * moveSpd;
            }

            if (Input.GetKey(KeyCode.RightArrow) || mousePosRatio.x >= 0.99f)
            {
                //transform.position += Vector3.right * Time.deltaTime * moveSpd;
                moveVal = Vector3.right * Time.deltaTime * moveSpd;
            }


            if (Input.GetKey(KeyCode.UpArrow) || mousePosRatio.y <= 0.01f)
            {
                //transform.position -= Vector3.forward * Time.deltaTime * moveSpd;

                moveVal = -1f * Vector3.forward * Time.deltaTime * moveSpd;
            }
            if (Input.GetKey(KeyCode.DownArrow) || mousePosRatio.y >= 0.99f)
            {
                //transform.position += Vector3.forward * Time.deltaTime * moveSpd;

                moveVal = Vector3.forward * Time.deltaTime * moveSpd;
            }
        }


        Vector2 tempScroll = curScroll;
        tempScroll.x += moveVal.x;
        tempScroll.y += moveVal.z;

        if (tempScroll.x <= scrollLock_x.min || tempScroll.x >= scrollLock_x.max)
        {
            moveVal.x = 0f;
        }

        if (tempScroll.y <= scrollLock_y.min || tempScroll.y >= scrollLock_y.max)
        {
            moveVal.z = 0f;
        }
        
        transform.position += moveVal;
        curScroll.x += moveVal.x;
        curScroll.y += moveVal.z;
    }


    private void Awake()
	{
        screenWidth = Screen.width;
        screenHeight = Screen.height;

	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();
    }

	private void OnDrawGizmos()
	{
        Vector3 LB = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, transform.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(LB, 1f);
	}
}
