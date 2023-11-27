using EasyRoads3Dv3;
using sc.terrain.proceduralpainter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TerrainChanger : MonoBehaviour
{

    [SerializeField] TerrainPainter painter;
    [SerializeField] Terrain terrain;
    [SerializeField] GameObject ModernPrefab;
    [SerializeField] GameObject OldPrefab;

    private void Update()
    {
        TreeInstance[] trees = terrain.terrainData.treeInstances;

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetTerrainSand();
            SetOldTrees(trees);
            terrain.Flush();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetTerrainGrass();
            SetModernTrees(trees);
            terrain.Flush();
        }
    }

    public void SetTerrainGrass()
    {
        painter.layerSettings[4].layer = painter.layerSettings[2].layer;
        painter.RepaintAll();
    }

    public void SetModernTrees(TreeInstance[] trees)
    {
        for (int i = 0; i < trees.Length; i++)
        {
            GameObject newTree = Instantiate(ModernPrefab);
            newTree.transform.position = terrain.transform.position + Vector3.Scale(terrain.terrainData.size, trees[i].position);
            
            terrain.AddTreeInstance(new TreeInstance()
            {
                position = newTree.transform.position,
                prototypeIndex = ModernPrefab.GetInstanceID(),
                widthScale = trees[i].widthScale,
                heightScale = trees[i].heightScale,
                color = trees[i].color,
                lightmapColor = trees[i].lightmapColor
            });
        }
    }

    [ContextMenu("Change")]
    public void SetTerrainSand()
    {
        painter.layerSettings[4].layer = painter.layerSettings[3].layer;
        painter.RepaintAll();
    }

    public void SetOldTrees(TreeInstance[] trees)
    {
        for (int i = 0; i < trees.Length; i++)
        {
            GameObject newTree = Instantiate(OldPrefab);
            newTree.transform.position = terrain.transform.position + Vector3.Scale(terrain.terrainData.size, trees[i].position);
            
            terrain.AddTreeInstance(new TreeInstance()
            {
                position = newTree.transform.position,
                prototypeIndex = OldPrefab.GetInstanceID(),
                widthScale = trees[i].widthScale,
                heightScale = trees[i].heightScale,
                color = trees[i].color,
                lightmapColor = trees[i].lightmapColor
            });
        }
    }


}
