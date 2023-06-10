using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float ghostDelayCounter;
    public bool canMakeGhost = false;

    [SerializeField]
    private GameObject ghostPrefab;
    public float ghostDelay;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelayCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMakeGhost)
        {
            ghostDelayCounter += Time.deltaTime;
            if (ghostDelayCounter > ghostDelay)
            {
                GameObject currentGhost = Instantiate(ghostPrefab, transform.position, transform.rotation);
                currentGhost.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                Destroy(currentGhost, 1f);
                ghostDelayCounter = 0.0f;
            }
        }
    }

    public void setGhost(bool canMakeGhost)
    {
        this.canMakeGhost = canMakeGhost;
    }
}
