using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float camScrollSpd;
    int width;
    int height;

    public float zoomSpd;
    public float minZoom;
    public float maxZoom;


    Camera mainCam;


	float minX;
	float maxX;
	float minZ;
	float maxZ;
    int cubeSize = 1;
	private void CamScroll()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x / width;
        mousePos.y = mousePos.y / height;

        Vector3 originPos = transform.position;

        if (mousePos.x <= 0.01f)
        {
            transform.position -= (Vector3.right * Time.deltaTime * camScrollSpd);
        }
        else if (mousePos.x >= 0.99f)
        {
            transform.position += (Vector3.right * Time.deltaTime * camScrollSpd);
        }

        if (mousePos.y <= 0.01f)
        {
            transform.position -= ( Vector3.forward * Time.deltaTime * camScrollSpd);
        }
        else if (mousePos.y >= 0.99f)
        {
            transform.position += ( Vector3.forward * Time.deltaTime * camScrollSpd);
        }


        Vector3 LB = mainCam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.y));
        Vector3 RT = mainCam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.y));

        if (LB.x <= minX
            | RT.x >= maxX
            | LB.z <= minZ
            |RT.z >= maxZ)
        {
            Debug.Log("더 ㄴㄴㄴㄴㄴ");
            transform.position = originPos;  
        }
    }

    void CamZoom()
    {


        float wheelScroll = Input.GetAxis("Mouse ScrollWheel");

        if (wheelScroll == 0f)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit point;
        Physics.Raycast(ray, out point, transform.position.y + 1f);
        Vector3 Scrolldirection = ray.GetPoint(minZoom);
        
        float step = zoomSpd * Time.deltaTime;

        Vector3 destPos = Vector3.MoveTowards(transform.position, Scrolldirection, wheelScroll * step);

        if (destPos.y > maxZoom)
        {
            destPos = transform.position;
            destPos.y = maxZoom;
        }
        else if (destPos.y < minZoom)
        {
            destPos = transform.position;
            destPos.y = minZoom;
        }
        else
        {
            
        }
        transform.position = destPos;

        //줌 아웃 일 때는 경게 부분 넘는지 판단해서 넘는 만큼 위치값 옮겨주기


        //      float wheelScroll = Input.GetAxis("Mouse ScrollWheel");

        //      Debug.Log(wheelScroll);
        //      if(wheelScroll ==0f)
        //{
        //          return;

        //}

        //      Vector2 mousePos = Input.mousePosition;
        //      mousePos.x = mousePos.x / width;
        //      mousePos.y = mousePos.y / height;
        //      Vector3 worldPos = mainCam.ViewportToWorldPoint(new Vector3(mousePos.x, mousePos.y,15f));
        //      worldPos.y = 15f;
        //      Debug.Log($"WorldPos : {worldPos}");

        //      //transform.position = worldPos;

        //      mainCam.fieldOfView -= wheelScroll * Time.deltaTime * zoomSpd;
    }

    private void Awake()
	{  
        Cursor.lockState = CursorLockMode.Confined;
        mainCam = GetComponent<Camera>();

        width = Screen.width;
        height = Screen.height;
    }
	// Start is called before the first frame update
	void Start()
    {

		minX = 0;
		minZ = 0;
		maxX = minX + (128 * cubeSize);
		maxZ = minZ + (128 * cubeSize);


	}

    // Update is called once per frame
    void Update()
    {
        CamScroll();

        if (Input.GetKey(KeyCode.K))
        {
            mainCam.fieldOfView = 30f;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            mainCam.fieldOfView = 60f;
        }


        CamZoom();

    }


	private void OnDrawGizmos()
	{
        if (mainCam != null)
        {
            //mainCam.ViewportToWorldPoint
            Vector3 LB = mainCam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.y));
            Vector3 RT = mainCam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.y));
            Vector3 RB = mainCam.ViewportToWorldPoint(new Vector3(1, 0, transform.position.y));
            Vector3 LT = mainCam.ViewportToWorldPoint(new Vector3(0, 1, transform.position.y));

			//Debug.Log($"LB : {LB}");
   //         Debug.Log($"RB : {RB}");
   //         Debug.Log($"RT : {RT}");
   //         Debug.Log($"LT : {LT}");


            Gizmos.color = Color.red;
            Gizmos.DrawSphere(LB, 1f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(RB, 1f);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(RT, 1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(LT, 1f);
        }
    }
}
