using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

    public int id;
    public LeveL_Manager manager;

    public bool mouseOver = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && mouseOver)
        {
            // Debug.Log($"Click: {id}"); pour avoir les couleurs d�s le d�but
            manager.RevealMaterial(id); // pour r�v�ler les couleurs quand on leur click dessus
        }
    }

    void OnMouseOver()
    {
        transform.localScale = new Vector3(1, 2, 1);
        mouseOver = true;
    }

    void OnMouseExit()
    {
        transform.localScale = Vector3.one;
        mouseOver = false;
    }
}
