using EasyRoads3Dv3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChanger : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    TerrainData _terrainData;
    float[,,] alphamaps;
    public int value = 0;
    public int valuetwo = 0;
    private void Start()
    {
        _terrainData = terrain.terrainData;
        float[,,] alphamaps = _terrainData.GetAlphamaps(0, 0, _terrainData.alphamapWidth, _terrainData.alphamapHeight);
    }


    [ContextMenu("Change")]
    public void SetTerrain()
    {
        for (int i = 0; i < _terrainData.alphamapHeight; i++)
        {
            for (int y = 0; y < _terrainData.alphamapWidth; y++)
            {
                alphamaps[i, y, 0] = value;
                alphamaps[i, y, 1] = valuetwo;

            }
        }

        _terrainData.SetAlphamaps(0, 0, alphamaps);
    }


}
