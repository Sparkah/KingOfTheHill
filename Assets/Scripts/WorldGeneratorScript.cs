using System.Collections.Generic;
using UnityEngine;

public class WorldGeneratorScript : MonoBehaviour
{
    [SerializeField] List <GameObject> ladderBlocks;
    [SerializeField] GameObject enemy1Cube;

    public void MoveBlocks()
    {
        ladderBlocks[0].transform.position = new Vector3(ladderBlocks[ladderBlocks.ToArray().Length - 1].transform.position.x,
        ladderBlocks[ladderBlocks.ToArray().Length - 1].transform.position.y + 1, ladderBlocks[ladderBlocks.ToArray().Length - 1].transform.position.z + 1);
        GameObject removedBlock = ladderBlocks[0];
        ladderBlocks.RemoveAt(0);
        ladderBlocks.Add(removedBlock);
        SpawnEnemyMovedBlock(removedBlock);
    }

    private void SpawnEnemyMovedBlock(GameObject block)
    {
        ESpawnLocations[] spawnLocations = block.GetComponentsInChildren<ESpawnLocations>();
        int shouldSpawn = Random.Range(0, 5);
        if(shouldSpawn==0)
        {
            int spawnLocation = Random.Range(0, spawnLocations.Length-1);
            Instantiate(enemy1Cube, new Vector3(spawnLocations[spawnLocation].transform.position.x, spawnLocations[spawnLocation].transform.position.y +0.5f, spawnLocations[spawnLocation].transform.position.z), Quaternion.identity);
        }
    }
}