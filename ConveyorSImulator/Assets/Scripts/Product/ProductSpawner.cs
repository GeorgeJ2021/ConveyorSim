using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private conveyor startConveyor;
    [SerializeField] private Transform spawnPoint;

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
