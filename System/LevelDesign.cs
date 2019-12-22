using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class LevelDesign : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private Text beatText;

    [SerializeField]
    private Text pathText;

    [SerializeField]
    private Text lastCommandText;

    private Queue<string> commandQue = new Queue<string>();

    private int beat = 0;
    private float beatSec = 0.39f;

    private bool isPause;

    public int Beat {
        get => beat;
        set
        {
            beatText.text = "Now Beat : " + beat.ToString();
            beat = value;
        }
    }

    public void MusicQue()
    {
        audioSource.clip = clip;
        audioSource.Play();
        isPause = false;
        StartCoroutine(BeatUP());
    }

    private IEnumerator BeatUP()
    {
        yield return YieldInstructionCache.WaitingSecond(beatSec);
        Beat++;
        StartCoroutine(BeatUP());
    }

    public void MusicStop()
    {
        audioSource.Stop();
        audioSource.clip = null;
        StopAllCoroutines();
        Beat = 0;
    }

    public void Pause()
    {
        if (!isPause)
        {
            audioSource.Pause();
            StopAllCoroutines();
            isPause = true;
        }
        else
        {
            audioSource.UnPause();
            StartCoroutine(BeatUP());
            isPause = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            commandQue.Enqueue("@/" + beat + "/0");
            lastCommandText.text = "@/" + beat + "/0";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            commandQue.Enqueue("@/" + beat + "/1");
            lastCommandText.text = "@/" + beat + "/1";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            commandQue.Enqueue("@/" + beat + "/2");
            lastCommandText.text = "@/" + beat + "/2";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            commandQue.Enqueue("@/" + beat + "/3");
            lastCommandText.text = "@/" + beat + "/3";
        }
     
    }

    public void Save()
    {
        string path = Application.dataPath + "ChaosFantasy";
        pathText.text = path;

        FileStream fs = new FileStream(path, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < commandQue.Count; i++)
        {
            sw.WriteLine(commandQue.Dequeue());
        }
        sw.Close();
        fs.Close();
    }
}
