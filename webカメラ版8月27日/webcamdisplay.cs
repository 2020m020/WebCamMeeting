using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming;

public class webcamdisplay : MonoBehaviour
{
    public RawImage rawImage; // RawImageコンポーネントへの参照
    private GameObject GO;
    private WebCamTexture webcamTexture;
    public Dropdown microphonedrop;
   // public Dropdown cameradrop;

    private void Start()
    {

        int n = 0;
        int m = 0;
        // 利用可能なカメラの情報を取得
        WebCamDevice[] devices = WebCamTexture.devices;
        // Anker PowerConf C300という名前のカメラを探す
        string targetCameraName = "Anker PowerConf C300";
        WebCamDevice targetCamera = default;

        foreach (WebCamDevice device in devices)
        {
           // cameradrop.options.Add(new Dropdown.OptionData { text = device.name });
           // cameradrop.RefreshShownValue();
            Debug.Log(device.name);
            if (device.name == targetCameraName)
            {
                targetCamera = device;
                break;
            }
            n++;
        }

        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            microphonedrop.options.Add(new Dropdown.OptionData { text = device });
            microphonedrop.RefreshShownValue();
            m++;
        }



        if (!string.IsNullOrEmpty(targetCamera.name))
        {
            // カメラを起動してRawImageに映像を表示
            webcamTexture = new WebCamTexture(targetCamera.name);
            Debug.Log("カメラ発見" + n + targetCamera.name);
            rawImage.texture = webcamTexture;
            rawImage.SetNativeSize();
            GO = GameObject.Find("GameObject");
            //GO.GetComponent<VideoStreamSender>().sourceDeviceIndex = n;
            webcamTexture.Play();
            rawImage.SetNativeSize();
            GO.GetComponent<VideoStreamSender>().sourceTexture = webcamTexture;
        }
        else
        {
            Debug.LogError("指定されたカメラが見つかりませんでした: " + targetCameraName);
        }
    }


    private void Update()
    {
        //webcamTexture = new WebCamTexture(cameradrop.name);
        //rawImage.texture = webcamTexture;
        //webcamTexture.Play();
        rawImage.SetNativeSize();
        GO.GetComponent<AudioStreamSender>().sourceDeviceIndex = microphonedrop.value;
        //GO.GetComponent<VideoStreamSender>().sourceTexture = webcamTexture;
    }

    private void OnDisable()
    {
        // スクリプトが無効になったときにカメラの使用を停止する
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}

/* using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming;

public class webcamdisplay : MonoBehaviour
{
    public RawImage rawImage; // RawImageコンポーネントへの参照
    private GameObject GO;
    private WebCamTexture webcamTexture;
    [SerializeField]
    private Dropdown dropdown;

    private void Start()
    {

        int n = 0;
        int m = 0;
        // 利用可能なカメラの情報を取得
        WebCamDevice[] devices = WebCamTexture.devices;
        string[] microphones = Microphone.devices;
        // HP Wide Vision HD Cameraという名前のカメラを探す
        string targetCameraName = "Anker PowerConf C300";
        //string targetCameraName = "Live Streaming Video Device";
        WebCamDevice targetCamera = default;

        foreach (WebCamDevice device in devices)
        {
            
            Debug.Log(device.name);
            if (device.name == targetCameraName)
            {
                targetCamera = device;
                break;
            }
            n++;
        }

        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            dropdown.options.Add(new Dropdown.OptionData { text = device });
            dropdown.RefreshShownValue();
            m++;
        }



        if (!string.IsNullOrEmpty(targetCamera.name))
        {
            // カメラを起動してRawImageに映像を表示
            webcamTexture = new WebCamTexture(targetCamera.name);
            Debug.Log("カメラ発見"+n+targetCamera.name);
            rawImage.texture = webcamTexture;
            rawImage.SetNativeSize();
            GO = GameObject.Find("GameObject");
            //GO.GetComponent<VideoStreamSender>().sourceDeviceIndex = n;
            webcamTexture.Play();
            rawImage.SetNativeSize();
            GO.GetComponent<VideoStreamSender>().sourceTexture = webcamTexture;
        }
        else
        {
            Debug.LogError("指定されたカメラが見つかりませんでした: " + targetCameraName);
        }
    }


    private void Update()
    {
        GO.GetComponent<AudioStreamSender>().sourceDeviceIndex = dropdown.value;
    }

    private void OnDisable()
    {
        // スクリプトが無効になったときにカメラの使用を停止する
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
*/
