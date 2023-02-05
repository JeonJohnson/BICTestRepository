using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Boundary
{
    public static Boundary zero
    {
        get
        {
            var bound = new Boundary();
            bound.xMin = 0f;
            bound.xMax = 0f;
            bound.yMin = 0f;
            bound.yMax = 0f;

            return bound;
        }
    }

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}

[System.Serializable]
public struct Vec3Boundary
{
    public static Vec3Boundary zero
    {
        get
        {
            var bound = new Vec3Boundary();
            bound.LT = Vector3.zero;
            bound.RT = Vector3.zero;
            bound.RB = Vector3.zero;
            bound.LB = Vector3.zero;

            return bound;
        }
    }

    public Vector3 LT;
    public Vector3 RT;
    public Vector3 RB;
    public Vector3 LB;
}

public class CamController2 : MonoBehaviour
{
    //직교투영 렌더 바운더리의 각 꼭짓점 pos 구한 다음
    //카메라 위치와 각 pos의 차이벡터를 구한뒤에
    //맵상 LT, RT, RB, LB 한계지점 계산해서
    //그에 맞는 카메라 pos의 clamp를 애초에 구하고 게임 시작한 뒤
    //못 넘게하기!!!

    //*스크롤을 먼저하고 그 다음에 위치확인해서 클램프 걸기

    Camera mainCam;
    
    public float zoomSpd;
    public float zoomMin;
    public float zoomMax;

	public float scrollSpd;

    public Boundary mapBoundary;
    public Boundary camClampBoundary; //카메라 clamp걸 용도;

    public Vec3Boundary camBoundary;//직교투영카메라 렌더링 바운더리 모서리 4위치
    public Vec3Boundary camBoundDir; //각 모서리 부터 카메라까지의 차이벡터

    void SetInitPos()
    {
        float x = (mapBoundary.xMin + mapBoundary.xMax) * 0.5f;
        float y = (mapBoundary.yMin + mapBoundary.yMax) * 0.5f;

        transform.position = new Vector3(x, 10, y);

        UpdateCamBoundary();
    }



    void Zoom()
    {
        float wheelScroll = Input.GetAxis("Mouse ScrollWheel");
        //스크롤 다운 -> 음수
        //스크롤 업 -> 양수

        if (wheelScroll == 0f)
        {
            return;
        }

        float preSize = Camera.main.orthographicSize;

        float zoomVal = (-1f * wheelScroll * zoomSpd * Time.deltaTime) + Camera.main.orthographicSize;

        Camera.main.orthographicSize = Mathf.Clamp(zoomVal, zoomMin, zoomMax);


        if(preSize != Camera.main.orthographicSize)
		{
            UpdateCamBoundary();
        }
        
    }

    void UpdateCamBoundary()
    {
        //1. 각 바운더리 좌표 구하기
        //LB가 00임 ㅋㅋ;
        CheckOrthoCamBoundary(ref camBoundary.LT, new Vector2(0, 1));
        CheckOrthoCamBoundary(ref camBoundary.RT, new Vector2(1, 1));
        CheckOrthoCamBoundary(ref camBoundary.RB, new Vector2(1, 0));
        CheckOrthoCamBoundary(ref camBoundary.LB, new Vector2(0, 0));


        //2. Dir구하기
        CalcDir();

        //3. 맵 끝부분에서의 카메라 위치 구하기?
        //3-1. 맵 LB에서의 LB 카메라 위치
        camClampBoundary.xMin =
             (new Vector3(mapBoundary.xMin, 0f, mapBoundary.yMin) + camBoundDir.LB).x;

        camClampBoundary.xMax =
            (new Vector3(mapBoundary.xMax, 0f, mapBoundary.yMax) + camBoundDir.RT).x;

        camClampBoundary.yMin =
             (new Vector3(mapBoundary.xMin, 0f, mapBoundary.yMin) + camBoundDir.LB).z;

        camClampBoundary.yMax =
            (new Vector3(mapBoundary.xMax, 0f, mapBoundary.yMax) + camBoundDir.RT).z;
    }

    void CheckOrthoCamBoundary(ref Vector3 boundary, Vector2 viewPortPos)
    {
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPos.x, viewPortPos.y, 0f));

        Vector3 camDir = Camera.main.transform.forward;

        //var ray = new Structs.RayResult();
        //Funcs.RayToWorld(ref ray, camDir, worldPos,LayerMask.GetMask("CameraPlane"));

        var ray = new Ray(worldPos, camDir);
        var rayResult = new RaycastHit();

        int nameToLayer = LayerMask.NameToLayer("CameraPlane"); //3 -> Layer의 index번호 
        int getMask = LayerMask.GetMask("CameraPlane"); //8 -> Layer의 비트 0000 1000
                                                        //(Int형이라서 8로 보이는거)
        
        //즉 Bit 플래그로 쓸꺼면
        //GetMask로 바로 불러와서 쓰던가
        //nameToLayer로 불러와서 1 << nameToLayer 해야함.
        //Ray쓸때에는 Index(NameToLayer)로 하면됨!
        if (Physics.Raycast(ray, out rayResult,100f, nameToLayer))
        {
            boundary = rayResult.point;
        }

        
    }

    void CalcDir()
    {
        camBoundDir.LT = transform.position - camBoundary.LT;
        camBoundDir.RT = transform.position - camBoundary.RT;
        camBoundDir.RB = transform.position - camBoundary.RB;
        camBoundDir.LB = transform.position - camBoundary.LB;
    }

    void Scroll()
    {
        Vector3 moveVal = transform.position;
        Vector2 mousePosRatio = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        if (mousePosRatio.x <= 0.01f)
        {
            moveVal += -1f * Vector3.right * Time.deltaTime * scrollSpd;
        }
        if (mousePosRatio.x >= 0.99f)
        {
            moveVal += Vector3.right * Time.deltaTime * scrollSpd;
        }
        if (mousePosRatio.y <= 0.01f)
        {
            moveVal += -1f * Vector3.forward * Time.deltaTime * scrollSpd;
        }
        if (mousePosRatio.y >= 0.99)
        {
            moveVal += Vector3.forward * Time.deltaTime * scrollSpd;
        }

        //transform.position += moveVal;

        moveVal.x = Mathf.Clamp(moveVal.x, camClampBoundary.xMin, camClampBoundary.xMax);
        moveVal.z = Mathf.Clamp(moveVal.z, camClampBoundary.yMin, camClampBoundary.yMax);

        transform.position = moveVal;
    }


	private void Awake()
	{
        

    }

	// Start is called before the first frame update
	void Start()
    {
        SetInitPos();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LateUpdate()
	{
        //1. 줌 체크하기
        // 줌이 바뀌면 카메라 바운더리, dir도 재계산
        Zoom();
        //2. 스크롤
        Scroll();

	}
}
