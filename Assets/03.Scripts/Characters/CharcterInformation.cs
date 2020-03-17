using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Character Data", menuName = "New Charcter Data", order=1)]
public class CharcterInformation : ScriptableObject
{  
    [SerializeField]
    private string charcterName;
    
    [SerializeField]
    private string description;
    
    public string CharacterName => CharacterName;
    public string Description => description;

}
