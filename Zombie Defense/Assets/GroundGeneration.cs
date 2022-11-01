using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class GroundGeneration : MonoBehaviour
{

    public int noiseScale;
    public Player player;
    public Tilemap tilemap;
    public Tilemap collidor;
    public int range;
    public TileBase[] grassTiles;
    public TileBase road;
    public TileBase[] roadEdges;
    public TileBase water;



    private int chunkSize = 50;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3Int(Random.Range(0, 10000), Random.Range(0, 10000));
        tilemap = GetComponent<Tilemap>();
        Vector3 pos = player.transform.position;
        Vector3Int player_pos_on_grid = tilemap.WorldToCell(pos);
        int counter = 0;
        do
        {
            if(counter > 10)
                break;
            GenerateChunk(new Vector3Int(0, 0, 0));
            GenerateChunk(new Vector3Int(-chunkSize, chunkSize, 0));
            GenerateChunk(new Vector3Int(-chunkSize, 0, 0));
            GenerateChunk(new Vector3Int(0, chunkSize, 0));
            counter++;

        } while (tilemap.GetTile(player_pos_on_grid) == water);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        Vector3 player_pos_on_grid = tilemap.WorldToCell(pos);

        

        for (int i = -range; i < range; i++)
        {
            for(int j = -range; j < range; j++)
            {
                int x = (int)player_pos_on_grid.x + i;
                int y = (int)player_pos_on_grid.y + j;
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (!tilemap.HasTile(tilePos))
                {
                    Debug.Log(tilePos);
                    int chunk_x = Mathf.FloorToInt(tilePos.x/(float)chunkSize)*chunkSize;
                    int chunk_y = Mathf.CeilToInt(tilePos.y/(float)chunkSize)*chunkSize;

                    Vector3Int chunk = new Vector3Int(chunk_x, chunk_y, 0);
                    GenerateChunk(chunk);
                }

            }
        }
    }

    void GenerateChunk(Vector3Int pos)
    {
        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {

                int x = (int)pos.x + i;
                int y = (int)pos.y - j;
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                float raw_perlin = Mathf.PerlinNoise((x - offset.x) / noiseScale, (y - offset.y) / noiseScale);
                float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);

                if(clamp_perlin > 0.4f && clamp_perlin < 0.5f)
                {
                    tilemap.SetTile(tilePos, road);
                }
                else if(clamp_perlin < 0.2f)
                {
                    collidor.SetTile(tilePos, water);
                    tilemap.SetTile(tilePos, water);
                }
                else if(clamp_perlin > 0.8f)
                {
                    tilemap.SetTile(tilePos, grassTiles[1]);
                }
                else if (clamp_perlin > 0.78f)
                {
                    tilemap.SetTile(tilePos, grassTiles[2]);
                }
                else
                {
                    int choice = Random.Range(0, 10);
                    if(choice < 9)
                    {
                        tilemap.SetTile(tilePos, grassTiles[0]);
                    }
                    else
                    {
                        choice = Random.Range(1, grassTiles.Length-1);
                        tilemap.SetTile(tilePos, grassTiles[choice]);
                    }
                }
                

            }
        }
        /*
        for(int i = 0; i < Random.Range(75,100); i++)
        {
            Vector3Int tilePos;
            do
            {
                tilePos = pos + new Vector3Int(Random.Range(0, chunkSize), Random.Range(0, chunkSize), 0);
            } while (tilemap.GetTile(tilePos) == road);

            tilemap.SetTile(tilePos, grassTiles[Random.Range(1, grassTiles.Length-1)]);
        }

        int num_of_roads_per_chunk;

        do
        {
            num_of_roads_per_chunk = Random.Range(0, 5);
        } while (num_of_roads_per_chunk == 1);

        Vector3Int[] road_pos = new Vector3Int[num_of_roads_per_chunk];

        for (int i = 0; i < num_of_roads_per_chunk; i++)
        {
            Vector3Int randPos;
            randPos = pos + (new Vector3Int(Random.Range(0, chunkSize), -Random.Range(0, chunkSize), 0));
            
            road_pos[i] = randPos;
            tilemap.SetTile(randPos, road);
            tilemap.SetTile(randPos + new Vector3Int(1, 0, 0), road);
            tilemap.SetTile(randPos + new Vector3Int(0, 1, 0), road);
        }

        List<Vector3Int> road_path = new List<Vector3Int>();
        int offset = Random.Range(0, num_of_roads_per_chunk);
        for (int i = 0; i < num_of_roads_per_chunk; i++)
        {
            for (int j = i + 1; j < num_of_roads_per_chunk; j++)
            {
                Vector3Int start = road_pos[i];
                Vector3Int end = road_pos[j];

                for (int x = 1; x < Mathf.Abs(end.x - start.x); x++)
                {
                    int x_pos;
                    if (end.x - start.x > 0)
                        x_pos = x;
                    else
                        x_pos = -x;
                    road_path.Add((start + new Vector3Int(x_pos, 0, 0)));
                }
                
                for (int y = 1; y < Mathf.Abs(end.y - start.y); y++)
                {
                    int y_pos;
                    if (end.y - start.y > 0)
                        y_pos = y;
                    else
                        y_pos = -y;
                    road_path.Add((end + new Vector3Int(0, -y_pos, 0)));
                }
                road_path.Add(new Vector3Int(end.x, start.y));

                foreach(Vector3Int tile in road_path)
                {
                    tilemap.SetTile(tile, road);
                    tilemap.SetTile(tile + new Vector3Int(1,0,0), road);
                    tilemap.SetTile(tile + new Vector3Int(0, 1, 0), road);
                }
            }
        }
        */


    }

}


