using UnityEngine;

namespace Utilities
{
    public class URLOpener : MonoBehaviour
    {
        public string url;
        public void OpenURL()
        {
            OpenURL(url);
        }
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}