using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.RenderStreaming.Samples
{
    class BidirectionalSample : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private RenderStreaming renderStreaming;
        [SerializeField] private Button setUpButton;
        [SerializeField] private Button hangUpButton;
        [SerializeField] private InputField connectionIdInput;
        [SerializeField] private RawImage remoteVideoImage;
        [SerializeField] private AudioSource receiveAudioSource;
        [SerializeField] private VideoStreamSender webCamStreamer;
        [SerializeField] private VideoStreamReceiver receiveVideoViewer;
        [SerializeField] private AudioStreamSender microphoneStreamer;
        [SerializeField] private AudioStreamReceiver receiveAudioViewer;
        [SerializeField] private SingleConnection singleConnection;
#pragma warning restore 0649

        private string connectionId;
        private RenderStreamingSettings settings;

        void Awake()
        {
            hangUpButton.interactable = false;
            connectionIdInput.interactable = true;
            setUpButton.onClick.AddListener(SetUp);
            hangUpButton.onClick.AddListener(HangUp);
            connectionIdInput.onValueChanged.AddListener(input => connectionId = input);
            connectionIdInput.text = $"{Random.Range(0, 99999):D5}";
            webCamStreamer.OnStartedStream += id => receiveVideoViewer.enabled = true;

            settings = SampleManager.Instance.Settings;


            receiveVideoViewer.OnUpdateReceiveTexture += texture => remoteVideoImage.texture = texture;

            receiveAudioViewer.targetAudioSource = receiveAudioSource;
            receiveAudioViewer.OnUpdateReceiveAudioSource += source =>
            {
                source.loop = true;
                source.Play();
            };
        }

        void Start()
        {
            if (renderStreaming.runOnAwake)
                return;
            renderStreaming.Run(signaling: settings?.Signaling);
        }

        private void SetUp()
        {
            setUpButton.interactable = false;
            hangUpButton.interactable = true;
            connectionIdInput.interactable = false;


            singleConnection.CreateConnection(connectionId);
        }

        private void HangUp()
        {
            singleConnection.DeleteConnection(connectionId);

            remoteVideoImage.texture = null;
            setUpButton.interactable = true;
            hangUpButton.interactable = false;
            connectionIdInput.interactable = true;
            connectionIdInput.text = $"{Random.Range(0, 99999):D5}";
        }
    }
}
