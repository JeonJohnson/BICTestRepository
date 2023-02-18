using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRTSCam : MonoBehaviour
{
    public Camera rtsCam;
    public Camera fogCam;
    public Camera minimapCam;
    public Transform minimapRenderIcon;

    public float zoomMin;
    public float zoomMax;
    public float zoomSpd;

    public float scrollSpd;

    public Boundary camClamp;

    public Vec3Boundary camRenderBoundary;  //직교투영카메라 렌더링 바운더리 모서리 4위치
    public Vec3Boundary camBoundaryDir;    //각 모서리 부터 카메라까지의 차이벡터

    void UpdateCamBoundary()
    {
        CheckOrthoCamBoundary(ref camRenderBoundary.LT, new Vector2(0, 1));
        CheckOrthoCamBoundary(ref camRenderBoundary.RT, new Vector2(1, 1));
        CheckOrthoCamBoundary(ref camRenderBoundary.RB, new Vector2(1, 0));
        CheckOrthoCamBoundary(ref camRenderBoundary.LB, new Vector2(0, 0));

        camRenderBoundary.Center = (camRenderBoundary.LT + camRenderBoundary.RB) * 0.5f;

        CalcDir();

        //3. 맵 끝부분에서의 카메라 위치 구하기?
        //3-1. 맵 LB에서의 LB 카메라 위치

        
        Vec2Boundary mapBoundary = NewMapGen.Instance.camBoundary;
        Boundary boundary = new Boundary();
        boundary.ComputeBoundary(mapBoundary);
        
        camClamp.xMin =
             (new Vector3(boundary.xMin, 0f, boundary.yMin) + camBoundaryDir.LB).x;

        camClamp.xMax =
            (new Vector3(boundary.xMax, 0f, boundary.yMax) + camBoundaryDir.RT).x;

        camClamp.yMin =
             (new Vector3(boundary.xMin, 0f, boundary.yMin) + camBoundaryDir.LB).z;

        camClamp.yMax =
            (new Vector3(boundary.xMax, 0f, boundary.yMax) + camBoundaryDir.RT).z;
    }

    void UpdateMinimapCamIcon()
    {
        //float aspect = (float)Screen.width / (float)Screen.height;
        minimapRenderIcon.position = camRenderBoundary.Center;

        float height = (camRenderBoundary.LT.z - camRenderBoundary.LB.z);
        float width = (camRenderBoundary.RT.x - camRenderBoundary.LT.x);

        Vector3 scale = minimapRenderIcon.localScale;
        scale.y = height;
        scale.x = width;
        minimapRenderIcon.localScale = scale;

        float size = NewMapGen.Instance.camBoundary.LT.y - NewMapGen.Instance.camBoundary.LB.y;
        minimapCam.orthographicSize = size * 0.5f;
    }

    void CheckOrthoCamBoundary(ref Vector3 boundary, Vector2 viewPortPos)
    {
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPos.x, viewPortPos.y, 0f));

        Vector3 camDir = Camera.main.transform.forward;

        var ray = new Ray(worldPos, camDir);
        var rayResult = new RaycastHit();

        if (Physics.Raycast(ray, out rayResult, 100f, LayerMask.GetMask("CameraPlane")))
        {
            boundary = rayResult.point;
        }
    }
    void CalcDir()
    {
        camBoundaryDir.LT = transform.position - camRenderBoundary.LT;
        camBoundaryDir.RT = transform.position - camRenderBoundary.RT;
        camBoundaryDir.RB = transform.position - camRenderBoundary.RB;
        camBoundaryDir.LB = transform.position - camRenderBoundary.LB;
    }

    void Zoom()
    {
        float wheelScroll = Input.GetAxis("Mouse ScrollWheel");

        if (wheelScroll == 0f)
        {
            return;
        }

        float preSize = Camera.main.orthographicSize;

        float zoomVal = (-1f * wheelScroll * zoomSpd * Time.deltaTime) + Camera.main.orthographicSize;

        Camera.main.orthographicSize = Mathf.Clamp(zoomVal, zoomMin, zoomMax);
        fogCam.orthographicSize = Mathf.Clamp(zoomVal, zoomMin, zoomMax);
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

        moveVal.x = Mathf.Clamp(moveVal.x, camClamp.xMin, camClamp.xMax);
        moveVal.z = Mathf.Clamp(moveVal.z, camClamp.yMin, camClamp.yMax);

        transform.position = moveVal;
    }
    void SetInitPos()
    {
        transform.position = new Vector3(NewMapGen.Instance.camBoundary.Center.x, 20f, NewMapGen.Instance.camBoundary.Center.y);

        UpdateCamBoundary();
    }

    void SettingMinimapCam()
    {
        if (!minimapCam)
        {
            return;
        }

        minimapCam.transform.position = transform.position;
        float size = camClamp.yMax - camClamp.yMin;
        minimapCam.orthographicSize = size * 0.5f;
    }


    void SettingFogCam()
    {
        fogCam.orthographicSize = rtsCam.orthographicSize;
    }

    private void Awake()
	{
		
	}

	void Start()
    {
        UpdateCamBoundary();

        SetInitPos();
        SettingMinimapCam();
        SettingFogCam();

        UpdateMinimapCamIcon();
    }

    
    void Update()
    {
        Zoom();
        //2. 스크롤
        Scroll();

        UpdateCamBoundary();
        //SettingMinimapCam();
        UpdateMinimapCamIcon();
    }



}
