using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    // Creating the prefab
    public GameObject personPrefab;
    
    // Number of people to create
    public int populationSize = 10;

    // Create the list of the population
    List<GameObject> population = new List<GameObject>();

    // Elapsed time
    public static float elapsed = 0;

    // How long we want the trial
    int trialTime = 10;

    // What level are we at?
    int generation = 1;

    // Creating a Gui to show the score and time
    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Loop for all the people to click
        for(int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            population.Add(go);
        }
    }

    //
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        // Swap parent DNA
        offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;

        return offspring;
    }

    // Breed New Population
    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();

        //Get rid of unfit individuals
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();

        // Breed upper half of sorted list
        for (int i = (int) (sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++ )
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // Destory all parent and previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }    

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
