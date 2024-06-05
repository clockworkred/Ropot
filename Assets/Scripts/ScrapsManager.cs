using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapsManager : MonoBehaviour
{
    public List<GameObject> scraps;

    private int _selectedPartIndex = 0;
    // Start is called before the first frame update
    public void GrabPart(GameObject scrap)
    {
        // Logic to grab part from middle area and add to player's inventory
        Debug.Log("Part grabbed: " + scrap.name);
        scraps.Add(scrap); // Add part to the inventory
    }

    public void SelectPreviousPart()
    {
        if (_selectedPartIndex > 0)
        {
            _selectedPartIndex--;
        }
        else
        {
            _selectedPartIndex = scraps.Count - 1;
        }
        UpdateSelectedPartDisplay();
    }

    public void SelectNextPart()
    {
        if (_selectedPartIndex < scraps.Count - 1)
        {
            _selectedPartIndex++;
        }
        else
        {
            _selectedPartIndex = 0;
        }
        UpdateSelectedPartDisplay();
    }

    private void UpdateSelectedPartDisplay()
    {
        // Update UI to show currently selected part
    }

    public void ConsumePart()
    {
        // Logic to consume selected part and trigger its effect
        if (scraps.Count > 0)
        {
            GameObject partToConsume = scraps[_selectedPartIndex];
            scraps.RemoveAt(_selectedPartIndex);
            Destroy(partToConsume); // Destroy the consumed part

            // Check for adjacent parts and merge
            CheckForMerge();
        }
    }

    private void CheckForMerge()
    {
        // Logic to check and merge adjacent parts
    }
}
