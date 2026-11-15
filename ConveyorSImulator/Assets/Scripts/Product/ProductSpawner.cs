using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private conveyor startConveyor;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCooldown = 2.0f;
    private float nextSpawnTime;

    [Header("Product Prefabs")]
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject canPrefab;

    public void SpawnBox()
    {
        Spawn(boxPrefab);
    }

    public void SpawnCan()
    {
        Spawn(canPrefab);
    }

    private void Spawn(GameObject prefab)
    {
        if (Time.time < nextSpawnTime)
        {
            return;
        }

        nextSpawnTime = Time.time + spawnCooldown;

        GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Product product = obj.GetComponent<Product>();

        if (product == null)
        {
            Debug.LogError($"{prefab.name} does not have a Product component.");
            return;
        }

        product.SetCurrentConveyor(startConveyor);
    }
}
