using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class MoveObject : MonoBehaviour
{
    public struct luaAction
    {

        public float speed;

        public float moveOnX;
        public float moveOnY;
        public float moveOnZ;

        public bool movement;
        public int color;
    };

    Renderer rend;
    Vector3 originalPosForActions;
    Queue<luaAction> actions;

    Script script = new Script();

    Console.CommandOnConsole MoveXDelegate;
    Console.CommandOnConsole MoveYDelegate;
    Console.CommandOnConsole MoveZDelegate;
    Console.CommandOnConsole ChangeColorDelegate;

    void Start()
    {
        actions = new Queue<luaAction>();
        rend = GetComponent<Renderer>();
        
        setOriginalPosForActions();

        MoveXDelegate = MoveXConsoleCmd;
        MoveYDelegate = MoveYConsoleCmd;
        MoveZDelegate = MoveZConsoleCmd;
        ChangeColorDelegate = ChangeColorConsoleCmd;

        Console.instance.AddCommand(("MoveX" + gameObject.name).ToLower(), MoveXDelegate);
        Console.instance.AddCommand(("MoveY" + gameObject.name).ToLower(), MoveYDelegate);
        Console.instance.AddCommand(("MoveZ" + gameObject.name).ToLower(),  MoveZDelegate);
        Console.instance.AddCommand(("ChangeColor" + gameObject.name).ToLower(), ChangeColorDelegate);
    }

    public string MoveXConsoleCmd(string[] arg)
    {
        string log = "";

        if (arg.Length == 2)
        {
            int x = 0;
            int speed = 0;

            x = System.Convert.ToInt32(arg[0]);
            speed = System.Convert.ToInt32(arg[1]);

            luaAction action = new luaAction();
            action.moveOnX = x;
            action.speed = speed;
            action.movement = true;
            actions.Enqueue(action);
            log += "El objeto "+gameObject.name+" se moverá "+x+" en X, a "+ speed +" de velocidad...";
        }
        else
        {
            log += "Error de parámetros. Parámetros válidos: 'cantidad' 'velocidad'...";
        }

        return log;
    }
    public string MoveYConsoleCmd(string[] arg)
    {
        string log = "";
        if (arg.Length == 2)
        {
            int y = 0;
            int speed = 0;

            y = System.Convert.ToInt32(arg[0]);
            speed = System.Convert.ToInt32(arg[1]);

            luaAction action = new luaAction();
            action.moveOnY = y;
            action.speed = speed;
            action.movement = true;
            actions.Enqueue(action);
            log += "El objeto " + gameObject.name + " se moverá " + y + " en X, a " + speed + " de velocidad...";
        }
        else
        {
            log += "Error de parámetros. Parámetros válidos: 'cantidad' 'velocidad'...";
        }


        return log;
    }
    public string MoveZConsoleCmd(string[] arg)
    {
        string log = "";
        if (arg.Length == 2)
        {
            int z = 0;
            int speed = 0;

            z = System.Convert.ToInt32(arg[0]);
            speed = System.Convert.ToInt32(arg[1]);

            luaAction action = new luaAction();
            action.moveOnZ = z;
            action.speed = speed;
            action.movement = true;

            actions.Enqueue(action);
            log += "El objeto " + gameObject.name + " se moverá " + z + " en X, a " + speed + " de velocidad...";

        }
        else
        {
            log += "Error de parámetros. Parámetros válidos: 'cantidad' 'velocidad'...";
        }
        return log;
    }

    public string ChangeColorConsoleCmd(string[] arg)
    {
        string log = "";
        int index = -1;
        if (arg != null)
            index = System.Convert.ToInt32(arg[0]);

        if(index > 0 && index < 3)
        {
            luaAction action = new luaAction();
            action.movement = false;
            action.color = index;
            actions.Enqueue(action);

            log += "Color cambiado...";
        }
        else
        {
            log += "Error índice de color. Índice válido: 1 (para rojo), 2 (para blanco)..."; 
        }

        return log;
    }

    public void MoveX(float x, float speed)
    {
        luaAction action = new luaAction();
        action.moveOnX = x;
        action.speed = speed;
        action.movement = true;

        actions.Enqueue(action);
    }

    public void MoveY(float y, float speed)
    {
        luaAction action = new luaAction();
        action.moveOnY = y;
        action.speed = speed;
        action.movement = true;


        actions.Enqueue(action);
    }

    public void MoveZ(float z, float speed)
    {
        luaAction action = new luaAction();
        action.moveOnZ = z;
        action.speed = speed;
        action.movement = true;

        actions.Enqueue(action);
    }

    public void ShowLog(string log)
    {
        Debug.Log("Lua log: " + log);
    }

    public void ChangeColor(int index)
    {
        luaAction action = new luaAction();
        action.movement = false;
        action.color = index;

        actions.Enqueue(action);
    }

    void setOriginalPosForActions()
    {
        originalPosForActions = transform.position;
    }

    void Update()
    {

        if (actions.Count > 0)
        {
            if (actions.Peek().movement)
            {
                Vector3 tmp = originalPosForActions;
                tmp.x += actions.Peek().moveOnX;
                tmp.y += actions.Peek().moveOnY;
                tmp.z += actions.Peek().moveOnZ;
                transform.position = Vector3.MoveTowards(transform.position, tmp, actions.Peek().speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, tmp) == 0)
                {
                    transform.position = tmp;
                    actions.Dequeue();
                    setOriginalPosForActions();
                }
            }
            else
            {
                switch (actions.Peek().color)
                {
                    case 0:
                        rend.material.color = Color.white;
                        break;
                    case 1:
                        rend.material.color = Color.red;
                        break;
                    default:
                        break;
                }
                actions.Dequeue();
            }
        }
    }
}
