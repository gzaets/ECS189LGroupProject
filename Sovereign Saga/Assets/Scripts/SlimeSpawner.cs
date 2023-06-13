using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slimePrefabs;

    [SerializeField]
    private GameObject Hero;

    private float playerX;
    
    private void Start()
    {
        StartCoroutine(SpawnSlime());
        playerX = Hero.transform.position.x;
    }

    private IEnumerator SpawnSlime()
    {
        playerX = Hero.transform.position.x;
        // Check if player is in cave or in combat
        if (playerX < -59f)
        {
            GameObject selectedPrefab = slimePrefabs[Random.Range(0, slimePrefabs.Length)];
            GameObject newSlime = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
            SlimeController slimeController = newSlime.GetComponent<SlimeController>();
            yield return new WaitForSeconds(5);
        }
        else
        {
            yield return null;
        }
    }
}