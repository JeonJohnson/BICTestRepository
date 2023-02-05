using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float camScrollSpd;
    //int width;
    //int height;

    public float zoomSpd;
    public float minZoom;
    public float maxZoom;

    Camera mainCam;


    public Vector2 mapClampX;
    public Vector2 mapClampY;

    //size에 따른 각 렌더링 바운더리부터  카메라 위치까지의 Dir
    public Vector3 LTDir;
    public Vector3 RTDir;
    public Vector3 RBDir;
    public Vector3 LBDir;

    public Vector3[] boundaryDir = new Vector3[4];

    //float minZ;
    //float maxZ;
    //int cubeSize = 1;


//    직교투영 렌더 바운더리의 각 꼭짓점 pos 구한 다음
//카메라 위치와 각 pos의 차이벡터를 구한뒤에
//맵상 LT, RT, RB, LB 한계지점 계산해서
//그에 맞는 카메라 pos의 clamp를 애초에 구하고 게임 시작한 뒤
//못 넘게하기!!!

//*스크롤을 먼저하고 그 다음에 위치확인해서 클램프 걸기

    private void CamScroll_Old()
    {
        Vector3 moveVal = Vector3.zero;
        
        Vector2 mousePosRatio = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        if (mousePosRatio.x <= 0.01f)
        {
            moveVal = -1f * Vector3.right * Time.deltaTime * camScrollSpd;
        }

        if (mousePosRatio.x >= 0.99f)
        {
            moveVal = Vector3.right * Time.deltaTime * camScrollSpd;
        }

        if (mousePosRatio.y <= 0.01f)
        {
            moveVal = -1f * Vector3.forward * Time.deltaTime * camScrollSpd;
        }

        if (mousePosRatio.y >= 0.99)
        {
            moveVal = Vector3.forward * Time.deltaTime * camScrollSpd;
        }

        Vector3 destPos = transform.position + moveVal;

        //직교 투영에서의 Size란?
        //현재 렌더링 되고 있는 화면의 상하범위의 반.(유니티 월드 Size 1 기준)
        //즉 직교투영에서의 Size가 5라면, 현재 스크린의 세로 범위 렌더링은 유니티 사이즈 10만큼 됨.
        //=> 1X1 큐브 10개가 들어간다는 소리임.
        //그렇다면 화면 비율이 16 : 9인 경우에는?
        // 사이즈 5인 경우
        // 세로는 10UnityWorldUnit의 크기 (세로는 그냥 사이즈 * 2개)
        // 가로는 10 * 16/9 개. (세로 * 화면비) 

        float cubeSize = Mathf.Sqrt(1f * 0.5f) * 2f;
        Debug.Log("cube size : " + cubeSize);
        float width = Screen.width;
        float height = Screen.height;
        float aspect = width / height;

        float vertExtent = mainCam.orthographicSize;

		//float yMin = vertExtent - mapClampY.x * 0.5f;
		//float yMax = mapClampY.y * 0.5f - vertExtent;
		float yMin = mapClampY.x + vertExtent;
		float yMax = mapClampY.y - vertExtent;

        //float horzExtent = vertExtent * (width / height);
        //      float xMin = horzExtent - (mapClampX.x * 0.5f);
        //      float xMax = (mapClampX.y * 0.5f) - horzExtent;
        float horzExtent = mainCam.orthographicSize * (width / height);
        float xMin = mapClampX.x + horzExtent;
        float xMax = mapClampX.y- horzExtent;

        //xMax *= cubeSize;

        //yMax *= cubeSize;

        //Debug.Log("X Min : " + xMin + ",X Max : " + xMax);
        //Debug.Log("Y Min : " + yMin + ",Y Max : " + yMax);

        transform.position += moveVal;


		#region OldMove
		//Vector2 mousePos = Input.mousePosition;
		//mousePos.x = mousePos.x / width;
		//mousePos.y = mousePos.y / height;

		//Vector3 originPos = transform.position;

		//if (mousePos.x <= 0.01f)
		//{
		//    transform.position -= (Vector3.right * Time.deltaTime * camScrollSpd);
		//}

		//if (mousePos.x >= 0.99f)
		//{
		//    transform.position += (Vector3.right * Time.deltaTime * camScrollSpd);
		//}

		//if (mousePos.y <= 0.01f)
		//{
		//    transform.position -= (Vector3.forward * Time.deltaTime * camScrollSpd);
		//}

		//if (mousePos.y >= 0.99f)
		//{
		//    transform.position += (Vector3.forward * Time.deltaTime * camScrollSpd);
		//}


		//Vector3 LB = mainCam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.y));
		//Vector3 RT = mainCam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.y));

		//if (LB.x <= minX
		//    |   RT.x >= maxX
		//    |   LB.z <= minZ
		//    |   RT.z >= maxZ)
		//{
		//    Debug.Log("더 ㄴㄴㄴㄴㄴ");
		//    transform.position = originPos;  
		//}
		#endregion
	}

    void CamScroll()
    {
        Vector3 prePos = transform.position;
        Vector3 moveVal = Vector3.zero;

        Vector2 mousePosRatio = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        if (mousePosRatio.x <= 0.01f)
        {
            moveVal = -1f * Vector3.right * Time.deltaTime * camScrollSpd;
        }
        if (mousePosRatio.x >= 0.99f)
        {
            moveVal = Vector3.right * Time.deltaTime * camScrollSpd;
        }
        if (mousePosRatio.y <= 0.01f)
        {
            moveVal = -1f * Vector3.forward * Time.deltaTime * camScrollSpd;
        }
        if (mousePosRatio.y >= 0.99)
        {
            moveVal = Vector3.forward * Time.deltaTime * camScrollSpd;
        }

        transform.position += moveVal;
        //Vector3 destPos = transform.position + moveVal;

        if (CheckOrthoCamBoundary(new Vector2(0, 0)) &  CheckOrthoCamBoundary(new Vector2(0, 1))
            & CheckOrthoCamBoundary(new Vector2(1, 1)) & CheckOrthoCamBoundary(new Vector2(1, 0)))
        {

        }
        else
        {
            transform.position = prePos;
        }
	}

	bool CheckOrthoCamBoundary(Vector2 viewPortPos)
    {
        Vector3 worldPos = mainCam.ViewportToWorldPoint(viewPortPos);

        Vector3 camDir = mainCam.transform.forward;

        var ray1 = new Structs.RayResult();

        if (Funcs.RayToWorld(ref ray1, camDir, worldPos))
        {
            return true; //화면 안.
        }
        else
        {
            return false;
        }
    }


    void CamZoom_Old()
    {
        #region oldZooom
        //float wheelScroll = Input.GetAxis("Mouse ScrollWheel");

        //if (wheelScroll == 0f)
        //{
        //    return;
        //}
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit point;
        //Physics.Raycast(ray, out point, transform.position.y + 1f);
        //Vector3 Scrolldirection = ray.GetPoint(minZoom);
        
        //float step = zoomSpd * Time.deltaTime;

        //Vector3 destPos = Vector3.MoveTowards(transform.position, Scrolldirection, wheelScroll * step);

        //if (destPos.y > maxZoom)
        //{
        //    destPos = transform.position;
        //    destPos.y = maxZoom;
        //}
        //else if (destPos.y < minZoom)
        //{
        //    destPos = transform.position;
        //    destPos.y = minZoom;
        //}
        //else
        //{
            
        //}
        //transform.position = destPos;

        ////줌 아웃 일 때는 경게 부분 넘는지 판단해서 넘는 만큼 위치값 옮겨주기


        ////      float wheelScroll = Input.GetAxis("Mouse ScrollWheel");

        ////      Debug.Log(wheelScroll);
        ////      if(wheelScroll ==0f)
        ////{
        ////          return;

        ////}

        ////      Vector2 mousePos = Input.mousePosition;
        ////      mousePos.x = mousePos.x / width;
        ////      mousePos.y = mousePos.y / height;
        ////      Vector3 worldPos = mainCam.ViewportToWorldPoint(new Vector3(mousePos.x, mousePos.y,15f));
        ////      worldPos.y = 15f;
        ////      Debug.Log($"WorldPos : {worldPos}");

        ////      //transform.position = worldPos;

        ////      mainCam.fieldOfView -= wheelScroll * Time.deltaTime * zoomSpd;
        #endregion
    }

    void CamZoom()
    {
        float wheelScroll = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log(wheelScroll);
        //스크롤 다운 -> 음수
        //스크롤 업 -> 양수

        if (wheelScroll == 0f)
        {
            return;
        }

        float zoomVal = (-1f * wheelScroll * zoomSpd * Time.deltaTime) + mainCam.orthographicSize;
        //Debug.Log(zoomVal);

        mainCam.orthographicSize = Mathf.Clamp(zoomVal, minZoom, maxZoom);

        CamBoundaryCalc();
    }

    bool CheckOrthoCamBoundary(ref Vector3 bounday, Vector2 viewPortPos)
    {
        Vector3 worldPos = mainCam.ViewportToWorldPoint(viewPortPos);

        Vector3 camDir = mainCam.transform.forward;

        var ray1 = new Structs.RayResult();

        if (Funcs.RayToWorld(ref ray1, camDir, worldPos))
        {
            return true; //화면 안.
        }
        else
        {
            return false;
        }
    }

    Vector3[] CamBoundaryCalc()
    {
        var arr = new Vector3[4];

        CheckOrthoCamBoundary(ref arr[0], new Vector3(0, 0));
        CheckOrthoCamBoundary(ref arr[1], new Vector3(1, 0));
        CheckOrthoCamBoundary(ref arr[2], new Vector3(1, 1));
        CheckOrthoCamBoundary(ref arr[3], new Vector3(0, 1));

        for (int i = 0; i < 4; ++i)
        {
            boundaryDir[i] = transform.position - arr[i];
        }

        return arr;
    }
    

    private void Awake()
	{  
        Cursor.lockState = CursorLockMode.Confined;
        mainCam = GetComponentInChildren<Camera>();

        //width = Screen.width;
        //height = Screen.height;

        
        minZoom = minZoom < mainCam.nearClipPlane ? mainCam.nearClipPlane : minZoom;
    }
	// Start is called before the first frame update
	void Start()
    {

        //mapClampX.y = Mathf.Sqrt(1f * 0.5f) * 2f * 64f;
        //minX = 0;
        //minZ = 0;
        //maxX = minX + (128 * cubeSize);
        //maxZ = minZ + (128 * cubeSize);


    }

    // Update is called once per frame
    void Update()
    {
     

        //if (Input.GetKey(KeyCode.K))
        //{
        //    mainCam.fieldOfView = 30f;
        //}
        //else if (Input.GetKeyUp(KeyCode.K))
        //{
        //    mainCam.fieldOfView = 60f;
        //}


    }

	private void LateUpdate()
	{


        CamScroll();
        CamZoom();
    }

	private void OnDrawGizmos()
	{
		if (mainCam != null)
		{
			//mainCam.ViewportToWorldPoint
			Vector3 LB = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
			Vector3 RB = mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0));
			Vector3 RT = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
			Vector3 LT = mainCam.ViewportToWorldPoint(new Vector3(0, 1, 0));

            //Debug.Log($"LB : {LB}");
            //Debug.Log($"RB : {RB}");
            //Debug.Log($"RT : {RT}");
            //Debug.Log($"LT : {LT}");

            //var ray = new Ray(LB,transform.forward);
            //if (Physics.Raycast(ray))
            //{
            //    Debug.Log(ray.);
            //}

            Vector3 dir = Camera.main.transform.forward;
            
            var ray1 = new Structs.RayResult();
            var ray2 = new Structs.RayResult();
            var ray3 = new Structs.RayResult();
            var ray4 = new Structs.RayResult();

            if (Funcs.RayToWorld(ref ray1, dir, LB))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(LB, 1f);
                Gizmos.DrawSphere(ray1.hitPosition, 1f);
            }

            if (Funcs.RayToWorld(ref ray2, dir, RB))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(RB, 1f);
                Gizmos.DrawSphere(ray2.hitPosition, 1f);
            }

            if (Funcs.RayToWorld(ref ray3, dir, RT))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(RT, 1f);
                Gizmos.DrawSphere(ray3.hitPosition, 1f);
            }

            if (Funcs.RayToWorld(ref ray4, dir, LT))
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(LT, 1f);
                Gizmos.DrawSphere(ray4.hitPosition, 1f);
            }



            //Gizmos.color = Color.yellow;
            //Gizmos.DrawSphere(RB, 1f);

            //Gizmos.color = Color.green;
            //Gizmos.DrawSphere(RT, 1f);

            //Gizmos.color = Color.blue;
            //Gizmos.DrawSphere(LT, 1f);
        }
	}
}
