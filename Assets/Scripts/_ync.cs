using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public struct Orientation
{
    public float w;
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class PoseJSON
{
    public float x;
    public float y;
    public float z;
    public float pitch;
    public float roll;
    public float yaw;
}

public class TargetJSON
{
    public int status;
    public float x;
    public float y;
    public float z;
    public float yaw;
}

public class _ync : MonoBehaviour
{
	public string remoteIP = "192.168.0.148";
	public bool isSimulation;
	public GameObject droneObject;
	public Button takeoffLandButton;
	public int buff_size = 1024;


	private Thread clientReceiveThread;
	private TcpClient socketConnection;

	private int status = 0;

	private Vector3 current_position = new Vector3(0, 0, 0);
	private Vector3 current_orientation = new Vector3(0, 0, 0);
	private Vector3 target_displacement = new Vector3(0, 0, 0);
	private Vector3 target_position = new Vector3(0, 1, 0);
	private Vector3 target_orientation = new Vector3(0, 0, 0);

	private Queue<byte[]> frame_queue;
	private Texture2D plane_texture;

	// Start is called before the first frame update
	void Start()
    {
		ConnectToTcpServer();

		plane_texture = new Texture2D(2560, 720);
		frame_queue = new Queue<byte[]>();
	}

    // Update is called once per frame
    void Update()
    {
		droneObject.transform.localPosition = current_position;
		droneObject.transform.localEulerAngles = current_orientation;

		if (frame_queue.Count > 0)
		{
			try
			{
				// Debug.Log("Converting texture");
				// plane_texture.LoadRawTextureData(image_bytes);
				// if (image_bytes[0] == 255 && image_bytes[1] == 216 && image_bytes[2] == 255 && image_bytes[3] == 224 && image_bytes[4] == 0 && image_bytes[5] == 16 && image_bytes[6] == 74 && image_bytes[7] == 70 && image_bytes[8] == 73 && image_bytes[9] == 70)
				// {
				plane_texture.LoadImage(frame_queue.Dequeue());
				//Material Right_Eye_Mat = GameObject.Find("Plane_Right_Eye").GetComponent<Renderer>().material;
				Material Right_Eye_Mat = GameObject.Find("Right_Curved_Plane").GetComponent<MeshRenderer>().material;
				//Material Left_Eye_Mat = GameObject.Find("Plane_Left_Eye").GetComponent<Renderer>().material;
				Material Left_Eye_Mat = GameObject.Find("Left_Curved_Plane").GetComponent<MeshRenderer>().material;

				Right_Eye_Mat.mainTexture = plane_texture;
				//Right_Eye_Mat.SetTexture("_EmissionMap", plane_texture);
				//Right_Eye_Mat.SetTextureOffset("_EmissionMap", new Vector2(.5f, 0));
				//Right_Eye_Mat.SetTextureScale("_EmissionMap", new Vector2(.5f, 1.0f));
				Right_Eye_Mat.mainTextureOffset = new Vector2(.5f, 0);
				Right_Eye_Mat.mainTextureScale = new Vector2(.5f, 1.0f);
				Left_Eye_Mat.mainTexture = plane_texture;
				Left_Eye_Mat.mainTextureScale = new Vector2(.5f, 1.0f);
				//Debug.Log("OK");
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
			}
		}
	}

	private void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient(remoteIP, 13579);

			if (socketConnection.Connected)
				Debug.Log("TCP Server connected.");
			while (true)
			{
				using (NetworkStream stream = socketConnection.GetStream())
				{
					Byte[] bytes = new Byte[1024];


					// Read incomming stream into byte arrary.                  
					while (true)
					{
						try
						{
							TargetJSON t = new TargetJSON();
							t.status = status;
							t.x = target_position.x;
							t.y = target_position.y;
							t.z = -target_position.z;
							// t.yaw = target_orientation.y / 180f * Mathf.PI;
							t.yaw = target_orientation.y;
							Byte[] send_msg = Encoding.UTF8.GetBytes(JsonUtility.ToJson(t));
							// Byte[] send_msg = Encoding.UTF8.GetBytes("RECV");
							stream.Write(send_msg, 0, send_msg.Length);
							Array.Clear(bytes, 0, bytes.Length);
							stream.Read(bytes, 0, bytes.Length);
							string json_str = Encoding.UTF8.GetString(bytes);
							print(json_str);
							PoseJSON p = JsonUtility.FromJson<PoseJSON>(json_str);

							byte[] msg = Encoding.UTF8.GetBytes("NEXTFRAME");
							stream.Write(msg, 0, msg.Length);

							Byte[] length_bytes;

							length_bytes = new Byte[16];
							stream.Read(length_bytes, 0, 16);
							int length = Convert.ToInt32(Encoding.ASCII.GetString(length_bytes));
                            Debug.Log("Receiving data of length = " + length.ToString());

                            Byte[] full_buff = new Byte[length];
							int current_length = 0;
							int chunk_length;
							Byte[] buff = new Byte[buff_size];

							while (stream.CanRead && current_length < length)
							{
								while (stream.DataAvailable && (chunk_length = stream.Read(buff, 0, buff_size)) != 0)
								{
									for (int i = 0; i < chunk_length; i++)
									{
										full_buff[i + current_length] = buff[i];
									}
									current_length += chunk_length;
								}
							}



							// JPEG Format Check
							if (full_buff[0] == 255 && full_buff[1] == 216)
							{
								// image_bytes = full_buff;
								Debug.Log(frame_queue.Count);
								frame_queue.Enqueue(full_buff);
							}

							current_position.x = p.x;
							current_position.y = p.y;
							current_position.z = -p.z;
							current_orientation.x = -p.pitch * 180f / Mathf.PI;
							current_orientation.y = p.yaw * 180f / Mathf.PI;
							current_orientation.z = p.roll * 180f / Mathf.PI;

						}
						catch (Exception e)
						{
							stream.Flush();
							// Debug.Log(e);
						}
					}
				}
			}
		}

		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}
