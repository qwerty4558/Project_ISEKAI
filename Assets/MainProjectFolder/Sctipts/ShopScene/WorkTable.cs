using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

[System.Serializable]
public class WorkTable
{
    [System.Serializable]
    public class TableRef
    {
        public EWorkTable eType;
        public MeshCollider collider;
        public Outline outline;
    }

    public EWorkTable selected;

    public TableRef[] tables;

    public Camera sceneCamera;

    public bool isWorkTableRayHit;

    public GameObject[] combinataionPanel;

    Dictionary<EWorkTable, TableRef> _lookupByType;
    Dictionary <string, TableRef> _lookupByName;

    public void Init() 
    {
        int lentable = tables.Length;
        int combinationPanelLengh = combinataionPanel.Length;
        _lookupByName = new Dictionary<string, TableRef>(2 * lentable);
        _lookupByType = new Dictionary<EWorkTable, TableRef>(2 * lentable);

        selected = EWorkTable.None;

        for (int i = 0; i< lentable; ++i)
        {
            TableRef t = tables[i];

            _lookupByType.Add(t.eType, t);
            _lookupByName.Add(t.collider.name, t);
        }

        for(int i =0; i < combinationPanelLengh; ++i)
        {
            combinataionPanel[i].transform.DOLocalMoveY(-217, 1);
            combinataionPanel[i].gameObject.SetActive(false);
        }
    }

    void ShowCombinationPanel(EWorkTable e)
    {
        int length = combinataionPanel.Length;
        switch (e)
        {
            case EWorkTable.None:
                for(int i = 0;i < length; ++i)
                {
                    combinataionPanel[i].transform.DOLocalMoveY(-217, 1);
                    combinataionPanel[i].gameObject.SetActive(false);
                }
                break;
            case EWorkTable.Carpentry:
                combinataionPanel[0].transform.DOLocalMoveY(700, 1);
                combinataionPanel[0].gameObject.SetActive(true);
                combinataionPanel[1].transform.DOLocalMoveY(-217, 1);
                combinataionPanel[1].gameObject.SetActive(false);
                combinataionPanel[2].transform.DOLocalMoveY(-217, 1);
                combinataionPanel[2].gameObject.SetActive(false);
                break;
            case EWorkTable.Smithy:
                combinataionPanel[0].transform.DOLocalMoveY(-217, 1);
                combinataionPanel[0].gameObject.SetActive(false);
                combinataionPanel[1].transform.DOLocalMoveY(700, 1);
                combinataionPanel[1].gameObject.SetActive(true);
                combinataionPanel[2].transform.DOLocalMoveY(-217, 1);
                combinataionPanel[2].gameObject.SetActive(false);
                break;
            case EWorkTable.Alchemy:
                combinataionPanel[0].transform.DOLocalMoveY(-217, 1);
                combinataionPanel[0].gameObject.SetActive(false);
                combinataionPanel[1].transform.DOLocalMoveY(-217, 1);
                combinataionPanel[1].gameObject.SetActive(false);
                combinataionPanel[2].transform.DOLocalMoveY(700, 1);
                combinataionPanel[2].gameObject.SetActive(true);
                break;
        }
    }

    public void Tick()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        Ray clickedRay = sceneCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            TableRef t_selected = _lookupByName[hitInfo.collider.name];
            isWorkTableRayHit = true;
            int lenTables = tables.Length;
            for (int i = 0; i < lenTables; ++i)
            {
                TableRef t = tables[i];
                if (t.eType == t_selected.eType)
                {                   
                    t.outline.OutlineWidth = 3f;
                }
                else
                {
                    t.outline.OutlineWidth = 0f;
                }                
            }
        }
        else 
        {
            isWorkTableRayHit = false;
            int lenTables = tables.Length;
            for(int i = 0; i < lenTables; ++i)
            {
                TableRef t = tables[i];
                t.outline.OutlineWidth = 1f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if(Physics.Raycast(clickedRay, out RaycastHit rayClickHit))
            {
                TableRef t_selected = _lookupByName[rayClickHit.collider.name];
                if (isWorkTableRayHit == true)
                {
                    // 여기는 해당 작업대 클릭 시 제작UI가 올라옴
                    int lenTable = tables.Length;
                    for(int i = 0; i < lenTable; ++i)
                    {
                        TableRef t = tables[i];

                        if (t.eType == t_selected.eType)
                        {
                            Debug.Log(rayClickHit.collider.name);
                            selected = t_selected.eType;
                            ShowCombinationPanel(selected);
                        }
                        else
                        {
                           
                        }
                    }
                }
                else if(isWorkTableRayHit == false)
                {
                    // 아무것도 히트되지 않을 시 UI는 초기위치로 돌아감
                    Debug.Log("no collision");
                    
                }
            }
        }
    }
}
