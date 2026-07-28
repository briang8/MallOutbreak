using UnityEngine;

// Central access point for enemy pools, keyed by type. Enemies return
// themselves here on death instead of being destroyed, and levels can
// request a fresh enemy from the pool instead of Instantiating directly.
public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance { get; private set; }

    [SerializeField] private ObjectPool walkerPool;
    [SerializeField] private ObjectPool runnerPool;
    [SerializeField] private ObjectPool brutePool;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnWalker(Vector3 position) => walkerPool.Get(position, Quaternion.identity);
    public GameObject SpawnRunner(Vector3 position) => runnerPool.Get(position, Quaternion.identity);
    public GameObject SpawnBrute(Vector3 position) => brutePool.Get(position, Quaternion.identity);

    public void ReturnWalker(GameObject obj) => walkerPool.ReturnToPool(obj);
    public void ReturnRunner(GameObject obj) => runnerPool.ReturnToPool(obj);
    public void ReturnBrute(GameObject obj) => brutePool.ReturnToPool(obj);
}