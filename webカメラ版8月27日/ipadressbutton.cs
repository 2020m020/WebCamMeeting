using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Unity.RenderStreaming;
using Unity.RenderStreaming.Signaling;

namespace Unity.RenderStreaming.Samples
{
    public class ipadressbutton : MonoBehaviour
    {
        public RenderStreaming renderStreaming;
        [SerializeField] private InputField connectionIPInput;
        [SerializeField] private Button connectButton;

        private void Start()
        {
            renderStreaming = GetComponent<RenderStreaming>();
            connectionIPInput.text = "ws://localhost";
            renderStreaming.urlSignaling = connectionIPInput.text;

#if UNITY_EDITOR
            EditorUtility.SetDirty(renderStreaming);
#endif
        }

        public void changeIP()
        {
            renderStreaming = GetComponent<RenderStreaming>();
            renderStreaming.urlSignaling = connectionIPInput.text;

#if UNITY_EDITOR
            EditorUtility.SetDirty(renderStreaming);
#endif
        }
    }
}
