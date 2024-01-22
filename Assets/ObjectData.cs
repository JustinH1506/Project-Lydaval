using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public enum ObjectStates
{
    StartCutscene,
    ForestEntry,
    AfterTutorialFight,
    BeforeVillageDuringAttack,
    ReonsKidnapping,
    FrowinAfterAttack,
    HildaAfterAttack,
    AfterBossFight
}
public class ObjectData : MonoBehaviour
{

    [System.Serializable]
    public class Data
    {
        public bool fightWon, bossFightWon;

        public ObjectStates objectStates;
    }

    public Data data;

    [SerializeField] private GameObject houses, burnedHouses, enemy;

    [SerializeField] private PlayableDirector afterTutorialCutscene, afterBossFight;

    [SerializeField] private List<GameObject> timelineCollider;

    [SerializeField] private List<string> questTexts;

    [SerializeField] private TextMeshProUGUI questTextPlace;

    /// <summary>
    /// Depending on the state we activate certain objects and play afterTutorial cutscene if fight won is true.
    /// If bossFightWon is true we play afterBossFight timeline. 
    /// </summary>
    private void Start()
    {
        var loadedData = GameStateManager.instance.data.objectData;

        if (loadedData != null)
        {
            data = loadedData;

            if(data.objectStates == ObjectStates.StartCutscene)
            {
                timelineCollider[0].SetActive(true);
                questTextPlace.text = questTexts[0];
            }            
            else if(data.objectStates == ObjectStates.ForestEntry)
            {
                timelineCollider[1].SetActive(true);
                questTextPlace.text = questTexts[1];
            }           
            else if (data.objectStates == ObjectStates.AfterTutorialFight)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
                enemy.SetActive(true);
                timelineCollider[2].SetActive(true);
                questTextPlace.text = questTexts[2];
            }
            else if(data.objectStates == ObjectStates.BeforeVillageDuringAttack)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
                enemy.SetActive(true);
                timelineCollider[3].SetActive(true);
                questTextPlace.text = questTexts[3];
            }            
            else if(data.objectStates == ObjectStates.ReonsKidnapping)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
                enemy.SetActive(true);
                timelineCollider[4].SetActive(true);
                questTextPlace.text = questTexts[4];
            }            
            else if(data.objectStates == ObjectStates.FrowinAfterAttack)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
                enemy.SetActive(true);
                timelineCollider[5].SetActive(true);
                questTextPlace.text = questTexts[5];

            }           
            else if(data.objectStates == ObjectStates.HildaAfterAttack)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
                enemy.SetActive(true);
                timelineCollider[6].SetActive(true);
                questTextPlace.text = questTexts[6];
            }       
            else if(data.objectStates == ObjectStates.AfterBossFight)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
                enemy.SetActive(true);
                questTextPlace.text = questTexts[7];
            }            
            
            if(data.fightWon)
            {
                data.fightWon = false;
                
                afterTutorialCutscene.Play();
            }      
            
            if(data.bossFightWon)
            {
                data.bossFightWon = false;
                
                afterBossFight.Play();
            }        
        }

        GameStateManager.instance.data.objectData = data;
    }

    /// <summary>
    /// We get the text for our questTracker depending on the State we are in and save it to the GameStateManager. 
    /// </summary>
    public void ChangeQuestTracker()
    {
            if(data.objectStates == ObjectStates.StartCutscene)
            {
                questTextPlace.text = questTexts[0];
            }            
            else if(data.objectStates == ObjectStates.ForestEntry)
            {
                questTextPlace.text = questTexts[1];
            }           
            else if (data.objectStates == ObjectStates.AfterTutorialFight)
            {
                questTextPlace.text = questTexts[2];
            }
            else if(data.objectStates == ObjectStates.BeforeVillageDuringAttack)
            {
                questTextPlace.text = questTexts[3];
            }            
            else if(data.objectStates == ObjectStates.ReonsKidnapping)
            {
                questTextPlace.text = questTexts[4];
            }            
            else if(data.objectStates == ObjectStates.FrowinAfterAttack)
            {
                questTextPlace.text = questTexts[5];
            }           
            else if(data.objectStates == ObjectStates.HildaAfterAttack)
            {
                questTextPlace.text = questTexts[6];
            }       
            else if(data.objectStates == ObjectStates.AfterBossFight)
            {
                questTextPlace.text = questTexts[7];
            }

            GameStateManager.instance.data.objectData = data;
    }
}