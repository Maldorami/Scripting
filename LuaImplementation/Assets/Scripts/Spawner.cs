using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void Start()
    {
        //string[] tmp = new string[5];
        //tmp[0] = "Cube";

        //CreateObj(tmp);
    }

    public string CreateObj(string[] arg)
    {
        string log = "";
        string tmp = arg[0];

        switch (tmp)
        {
            case "Cube":
                {
                    Instantiate((GameObject)Resources.Load("Cube"));
                    log += "Objeto creado.";
                }

                break;
            case "Sphere":
                {
                    Instantiate((GameObject)Resources.Load("Sphere"));
                    log += "Objeto creado.";
                }
                break;
            default:
                log += "Error: imposible crear ese obj.";
                break;
        }

        return log;
    }

}
