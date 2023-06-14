using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuBackground : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] private float _x, _y;
    // Start is called before the first frame update
    void Start()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, image.uvRect.size);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MainMenu.isTutorial)
            image.uvRect = new Rect(image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, image.uvRect.size);
    }
}
