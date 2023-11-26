using EasyRoads3Dv3;
using sc.terrain.proceduralpainter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChanger : MonoBehaviour
{

    [SerializeField] TerrainPainter painter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetTerrainSand();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetTerrainGrass();
        }
    }

    public void SetTerrainGrass()
    {
        painter.layerSettings[4].layer = painter.layerSettings[2].layer;
        painter.RepaintAll();
    }

    [ContextMenu("Change")]
    public void SetTerrainSand()
    {
        painter.layerSettings[4].layer = painter.layerSettings[3].layer;
        painter.RepaintAll();
    }


}
