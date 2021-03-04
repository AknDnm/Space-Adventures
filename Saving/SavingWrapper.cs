using UnityEngine;

namespace Space_Adventures.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "SpaceAdventures";


        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}
