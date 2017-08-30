using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    Console.CommandOnConsole CreateObjDelegate;

    private void Start()
    {
        CreateObjDelegate = CreateObj;
        Console.instance.AddCommand("CreateObj".ToLower(), CreateObjDelegate);
    }

    public string CreateObj(string[] arg)
    {
        string log = "";
        string tmp = "";
        string name = "";

        if (arg != null)
        {
            if (arg.Length == 2)
            {
                name = arg[1];
                tmp = arg[0];
            }
            else
            {
                log += "Error al crear objetos. Parámetros válidos: 'Tipo' 'Nombre'";
                return log;
            }                
        }
        else
        {
            log += "Error al crear objetos. Parámetros válidos: 'Tipo' 'Nombre'";
            return log;
        }


        switch (tmp)
        {
            case "cube":
                {
                    GameObject obj = Instantiate((GameObject)Resources.Load("Cube"));
                    obj.AddComponent<MoveObject>();
                    obj.name = name;
                    log += "Objeto creado: Cube, con nombre: "+name+"...";
                }

                break;
            case "sphere":
                {
                    GameObject obj = Instantiate((GameObject)Resources.Load("Sphere"));
                    obj.AddComponent<MoveObject>();
                    obj.name = name;
                    log += "Objeto creado: Cube, con nombre: " + name + "...";
                }
                break;
            default:
                log += "Error: imposible crear el objeto \"" + tmp + "\"...";
                break;
        }

        return log;
    }

}
