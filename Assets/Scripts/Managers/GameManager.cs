using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _INPUT_MANAGER;

    //Contendra las recargas de la ulti

    //Funciones para upgradear las stats

    //Una lista de los objetos recogidos


    void Start()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this){
            Destroy(_INPUT_MANAGER);
        }else{
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    void Update()
    {
        
    }
}
