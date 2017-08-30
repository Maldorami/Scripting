using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{

    struct Command
    {
        public string cmdName;
        public string[] arg;
    }

    public static Console instance;
    public delegate string CommandOnConsole(string[] arg);

    Dictionary<string, CommandOnConsole> commandDic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        commandDic = new Dictionary<string, CommandOnConsole>();
    }

    public void AddCommand(string cmd, CommandOnConsole func)
    {
        commandDic.Add(cmd, func);
    }


    public string ExecuteCommand(string cmdLine)
    {
        string log = "";
        Command cmd = ReturnCommand(cmdLine);

        if (commandDic.ContainsKey(cmd.cmdName))
        {
            log += commandDic[cmd.cmdName](cmd.arg) + "\n";
        }
        else
        {
            log += "Comando incorrecto \"" + cmd.cmdName + "\"...";
        }

        return log;
    }

    public string ExecuteCommandByLines(string cmdLine)
    {
        string log = "";
        string[] lines = cmdLine.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            log += ExecuteCommand(lines[i]);
        }

        return log;
    }

    Command ReturnCommand(string cmdLine)
    {
        Command cmd = new Command();

        string[] cmdLineParseada = cmdLine.Split(' ');
        cmd.cmdName = cmdLineParseada[0];

        cmd.arg = new string[cmdLineParseada.Length - 1];
        for (int i = 1; i < cmdLineParseada.Length; i++)
        {
            cmd.arg[i - 1] = cmdLineParseada[i];
        }

        return cmd;
    }
}