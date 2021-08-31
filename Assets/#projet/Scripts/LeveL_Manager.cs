using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeveL_Manager : MonoBehaviour
{

    public int row = 3;
    public int col = 4;

    public float gapRow = 1.5f;
    public float gapCol = 1.5f;
    
    [Range(0f,5f)] // Permet de transformer la variable en slider
    public float timeBeforeReset = 1f;
    private bool resetOnGoing = false;

    public GameObject itemPrefab;

    public Material[] materials;
    public Material defaultMaterial;

    public ItemBehavior[] items;

    public List<int> selected = new List<int>();
    public List<int> matches = new List<int>();

    private Dictionary<int, Material> itemMaterial = new Dictionary<int, Material>();

    // Start is called before the first frame update
    void Start()
    {
        items = new ItemBehavior[row * col];
        int index = 0;
        for (int x = 0; x < col; x++)
        {
            for (int z = 0; z < row; z++)
            {
                Vector3 position = new Vector3(x * gapCol, 0, z * gapRow);
                GameObject item = Instantiate(itemPrefab, position, Quaternion.identity);
                item.GetComponent<Renderer>().material = defaultMaterial;
                items[index] = item.GetComponent<ItemBehavior>(); // il faut s'assurer que le Prefab ait un ItemBehavior sinon ca ne marchera pas

                items[index].id = index; // id --> devient l'index qu'on pourra ensuite référencier
                items[index].manager = this;

                index++;
            }
        }

        GiveMaterials();

    }

    private void GiveMaterials()
    {
        List<int> possibilities = new List<int>();
        for (int i = 0; i < row * col; i++)
        {
            possibilities.Add(i);
        }

        for (int i = 0; i < materials.Length; i++) // éviter le foreach pour les jv
        {
            if (possibilities.Count < 2) break;
            int idPos = Random.Range(0, possibilities.Count);
            int id1 = possibilities[idPos];
            possibilities.RemoveAt(idPos);

            idPos = Random.Range(0, possibilities.Count);
            int id2 = possibilities[idPos];
            possibilities.RemoveAt(idPos);

            itemMaterial.Add(id1, materials[i]);
            itemMaterial.Add(id2, materials[i]);

            // items[id1].GetComponent<Renderer>().material = materials[i];
            // items[id2].GetComponent<Renderer>().material = materials[i];
        }
    }

    private IEnumerator ResetMaterials(int id1, int id2)
    {
        resetOnGoing = true;
        yield return new WaitForSeconds(timeBeforeReset);
        ResetMaterial(id1);
        ResetMaterial(id2);
        resetOnGoing = false;

    }

    public void RevealMaterial(int id)
    {
        if (resetOnGoing == false && !selected.Contains(id) && !matches.Contains(id))
        {
            selected.Add(id);
            Material material = itemMaterial[id];
            items[id].GetComponent<Renderer>().material = material;
            // items[id].GetComponent<Renderer>().material = itemMaterial[id];
        }
    }

    private void ResetMaterial(int id)
    {
        items[id].GetComponent<Renderer>().material = defaultMaterial;
    }
    // Update is called once per frame
    void Update()
    {
        if (selected.Count == 2)
        {
            if (itemMaterial[selected[0]] == itemMaterial[selected[1]])
            {
                matches.Add(selected[0]);
                matches.Add(selected[1]);
            }
            else
            {
                StartCoroutine(ResetMaterials(selected[0], selected[1]));
            }
            selected.Clear();
        }
    }
}
