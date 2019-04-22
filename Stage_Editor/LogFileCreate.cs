using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class LogFileCreate : MonoBehaviour
{
    private string path;
    private string file_name;
    private string file_pos;
    public static LogFileCreate instance;

    private List<Command> commands;

    public void Awake()
    {
        if (instance == null)
            instance = this;

        path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/BossFightUltimate_Stage_Logs/";
        file_name = "Pattern_Log.txt";
        file_pos = path + file_name;

    }

    public void CreateLog(List<Command> commands)
    {

        try
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                directory.Create();
            }
        }
        catch (Exception ee)
        {
            Debug.Log("Exception : " + ee);
        }

        this.commands = new List<Command>(commands);

        FileCreate();
        FileWrite();
    }

    private void FileCreate()
    {
        try
        {
            if (!File.Exists(file_pos))
            {
                File.Create(file_pos).Close();
            }
        }
        catch (Exception ee)
        {
            Debug.Log("Exception : " + ee);
        }
    }

    private void FileWrite()
    {
        try
        {
            StreamWriter file_write = new StreamWriter(file_pos);
            foreach (Command command in commands)
            {
                switch (command.pattern)
                {
                    case 0:
                        file_write.WriteLine("@"+"/"+command.beat.ToString());
                        break;
                    case 1:
                        file_write.WriteLine("#" + "/" + command.beat.ToString());
                        break;
                    case 2:
                        file_write.WriteLine("$" + "/" + command.beat.ToString());
                        break;
                }

                file_write.Flush();
            }
            file_write.Close();
        }
        catch (Exception ee)
        {
            Debug.Log("Exception : " + ee.Message);
            Debug.Log("Path : " + file_pos);
        }
    }
}
