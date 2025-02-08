using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner   towerSpawner;

    [SerializeField]
    private TowerDataViewer towerDataViewer;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            // 카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성
            // ray.origin : 광선의 시작점 (카메라 위치)
            // ray.direction : 광선의 진행 방향 (마우스 위치)
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 2D 모니터를 통해 3D 월드의 오브젝트를 마우스로 선택
            // 광선에 부딫히는 오브젝트를 검출해서 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;
                if (hit.collider.tag == "Tile")
                {
                    // Debug.Log("타일 클릭");
                    towerSpawner.SpawnTower(hit.transform);
                }
                else if (hit.collider.tag == "Tower")
                {
                    // Debug.Log("타워 클릭");
                    towerDataViewer.OnPanel(hit.transform);
                }
                // hope this works LOL
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {

            //Debug.Log("HI");
            if (hitTransform == null || !hitTransform.CompareTag("Tower"))
            {
                towerDataViewer.OffPanel();
            }
            hitTransform = null;
        }
    }
}
