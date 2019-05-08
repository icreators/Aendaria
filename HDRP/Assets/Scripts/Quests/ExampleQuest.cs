using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleQuest : MonoBehaviour
{
    private QuestManager manager;
    void Start()
    {
        manager=GameObject.Find("QuestsUI").GetComponent<QuestManager>();
    }

    void Update()
    {
        //możesz to dać nawet w updacie, bo nie można duplikować zadań, jednak lepiej korzystaj z QuestIsExist() dla lepszej wydajności
        manager.AddQuest("Scroll","testowe");
        manager.AddTarget("testowe","TEST SCROLLA!","scroll1",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll2",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll3",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll4",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll5",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll6",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll7",3);
        manager.AddTarget("testowe","TEST SCROLLA!","scroll8",3);

        //sprawdzanie czy "quest1" istnieje
        if(!manager.QuestIsExist("quest1"))
        {
            //Dodawanie questa. 
            //Pierwszy  argument to nazwa widoczna w menu.
            //Drugie    to id questa.
            manager.AddQuest("Nauka Sterowania","quest1");

            //Dodawanie zadania. 
            //Pierwszy  argument to id questa.
            //drugi     to napis w menu.
            //trzeci    to id zadania w questcie
            //czwarty   to maksymalny progres 
            manager.AddTarget("quest1","Nacisnij spacje 5 razy","spacja",5);
            manager.AddTarget("quest1","Uruchom 2 razy EQ","eq",2);
        }else{
            //Jeżeli gracz skoczy to dodaj progres.
            //Pierwszy  argument to id questa
            //Drugi     to id zadania
            //Trzeci    to ile progresu ma się dodać
            if(Input.GetButtonDown("Jump"))
                manager.AddProgress("quest1","spacja",1);
            if(Input.GetButtonDown("Inventory"))
                manager.AddProgress("quest1","eq",1);

            //JESZCZE NIE DZIAŁA
            //Funkcja będzie zwracać true tylko jeden raz, po tym jak quest zostanie wykonany
            if(manager.Reward("quest1"))
                Debug.Log("zadanie ukończone!");
        }
    }
}
