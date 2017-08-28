using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class MoveObject : MonoBehaviour 
{
    public struct luaAction{        

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
    
	void Start ()
	{
        actions = new Queue<luaAction>();
        rend = GetComponent<Renderer>();

        string scriptCode = @"

			function start ()
				sphere.MoveX(5,2)
				sphere.MoveY(3,2)
                sphere.ChangeColor(1)
				sphere.MoveY(-3,3)
				sphere.MoveX(-5,3)
                sphere.ChangeColor(0)
			end";
		
		UserData.RegisterType<MoveObject>();		
		DynValue shpere = UserData.Create(this);
		script.Globals.Set("sphere", shpere);
		script.DoString(scriptCode);
        script.Call(script.Globals["start"]);

        setOriginalPosForActions();
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

	void Update () {
        
        if(actions.Count > 0)
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
                        rend.material.color = Color.white;
                        break;
                }
                actions.Dequeue();
            }
        }        			
	}
}
