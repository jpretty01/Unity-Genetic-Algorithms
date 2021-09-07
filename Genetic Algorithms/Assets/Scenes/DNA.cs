using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    // Gene for color
    public float r;
    public float g;
    public float b;

    // Has person been clicked on or not
    bool dead = false;

    // How long did the person live for?
    public float timeToDie = 0;

    // Access to sprite rendered and collider
    SpriteRenderer sRenderer;
    Collider2D sCollider;


    // Creating the action on the mouse down
    void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        //Debug.Log("Dead at: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initial rendered and collider
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();

        // Color to the people
        sRenderer.color = new Color(r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
