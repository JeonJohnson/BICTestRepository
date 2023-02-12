using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuildingState
{
    Vector2 scale;
}

public class Building : MonoBehaviour
{
    public GameObject mesh;
    public GameObject minimapIcon;

    public void SettingScale(Vector3 scale)
    {//x,y -> 가로 세로 사이즈
        //z -> 높이
        mesh.transform.localScale = scale;

        Vector3 meshPos = mesh.transform.position;
        meshPos.y = scale.z * 0.5f;
        mesh.transform.position = meshPos;


        Vector3 iconScale = minimapIcon.transform.localScale;
        iconScale.x = scale.x;
        iconScale.y = scale.y;

        minimapIcon.transform.localScale = iconScale;
    }

	private void Awake()
	{
		
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
