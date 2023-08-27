using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming;

public class webcamdisplay : MonoBehaviour
{
    public RawImage rawImage; // RawImage�R���|�[�l���g�ւ̎Q��
    private GameObject GO;
    private WebCamTexture webcamTexture;
    public Dropdown microphonedrop;
   // public Dropdown cameradrop;

    private void Start()
    {

        int n = 0;
        int m = 0;
        // ���p�\�ȃJ�����̏����擾
        WebCamDevice[] devices = WebCamTexture.devices;
        // Anker PowerConf C300�Ƃ������O�̃J������T��
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
            // �J�������N������RawImage�ɉf����\��
            webcamTexture = new WebCamTexture(targetCamera.name);
            Debug.Log("�J��������" + n + targetCamera.name);
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
            Debug.LogError("�w�肳�ꂽ�J������������܂���ł���: " + targetCameraName);
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
        // �X�N���v�g�������ɂȂ����Ƃ��ɃJ�����̎g�p���~����
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
    public RawImage rawImage; // RawImage�R���|�[�l���g�ւ̎Q��
    private GameObject GO;
    private WebCamTexture webcamTexture;
    [SerializeField]
    private Dropdown dropdown;

    private void Start()
    {

        int n = 0;
        int m = 0;
        // ���p�\�ȃJ�����̏����擾
        WebCamDevice[] devices = WebCamTexture.devices;
        string[] microphones = Microphone.devices;
        // HP Wide Vision HD Camera�Ƃ������O�̃J������T��
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
            // �J�������N������RawImage�ɉf����\��
            webcamTexture = new WebCamTexture(targetCamera.name);
            Debug.Log("�J��������"+n+targetCamera.name);
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
            Debug.LogError("�w�肳�ꂽ�J������������܂���ł���: " + targetCameraName);
        }
    }


    private void Update()
    {
        GO.GetComponent<AudioStreamSender>().sourceDeviceIndex = dropdown.value;
    }

    private void OnDisable()
    {
        // �X�N���v�g�������ɂȂ����Ƃ��ɃJ�����̎g�p���~����
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
*/
