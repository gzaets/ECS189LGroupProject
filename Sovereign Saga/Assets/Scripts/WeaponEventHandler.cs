using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEventHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    private WeaponController wc;

    void Start()
    {
        wc = parent.GetComponent<WeaponController>();
    }
    
    public void Adjust()
    {
        wc.SetStall(true);
    }
}
