using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CharacterCreateWindow : EditorWindow
{

    private Sprite basicCharacterSprite;
    private Sprite skillCharacterSprite;

    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;

    static CharacterCreateWindow window;


    [MenuItem("Editor/Character Macro")]
    public static void Open()
    {
        GetWindow<CharacterCreateWindow>();
    }

    private void OnGUI()
    {
        if (Event.current.keyCode == KeyCode.Escape)
            window.Close();
    }
}
