using UnityEngine;
using System.Collections.Generic;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Chunks[] _chunkPrefabs;
    [SerializeField] private Chunks _firstChunk;
    [SerializeField] private Transform _canvas;

    private List<Chunks> spawnedChunks = new List<Chunks>();

    private void Start()
    {
        spawnedChunks.Add(_firstChunk);
    }

    private void Update()
    {
        if (_player.position.y > spawnedChunks[spawnedChunks.Count - 1].End.position.y - 5 
            && ScoreManager.Score < Level_change.endGame - 50)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        Chunks newChunk = Instantiate(_chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        newChunk.transform.SetParent(_canvas.transform);

        spawnedChunks.Add(newChunk);

        if (spawnedChunks.Count >= 4)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    } 
}
