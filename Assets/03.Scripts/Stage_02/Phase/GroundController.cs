using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{   
    [SerializeField]
    private GameObject defaultGround;

    [SerializeField]
    private GameObject panelGround;

    public void PanelGroundOn(){
        panelGround.gameObject.SetActive(true);
        defaultGround.gameObject.SetActive(false);
    }
    
    public void PanelGroundOff(){
        defaultGround.gameObject.SetActive(true);
        panelGround.gameObject.SetActive(false);
    }
}
