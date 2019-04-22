using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public int Beat;
    public ShameCtrl BeatShame;
    private List<Command> commands = new List<Command>();

    private void Awake()
    {
        Beat = 0;
        BeatShame.UpdateShame(Beat);
    }

    public void BeatUp() {
        Beat++;
        BeatShame.UpdateShame(Beat);
    }

    public void BeatDown()
    {
        Beat--;
        BeatShame.UpdateShame(Beat);
    }

    public void Pattern_in(int pattern_number)
    {
        Command newCommand = new Command(Beat,pattern_number);
        commands.Add(newCommand);
    }

    public void Pattern_out() {
        LogFileCreate.instance.CreateLog(commands);
    }

    public void Cancle()
    {
        commands.Remove(commands[commands.Count]);
    }
}
