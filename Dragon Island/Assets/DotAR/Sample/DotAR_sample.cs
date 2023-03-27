using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotAR_sample : MonoBehaviour {

	// debug text
	public Text text;

	// flip XY
	public bool flipXY = false;

	// camera manager
	public WebCamTexture webcamTexture = null;

	// DotAR class
	public DotAR dotar = null;

	// raw image texture
	private Texture2D texture = null;

	// board object
	private GameObject board = null;

	void Start()
	{
		WebCamDevice[] devices = WebCamTexture.devices;

		int camid = 0;
		int width = 320;
		int height = 240;

		if (devices.Length > camid) {
			// init camera
			webcamTexture = new WebCamTexture (devices [camid].name, width, height);
			webcamTexture.Play ();
		}

		board = GameObject.Find ("Board");
	}

	void Update () {
		
		if (webcamTexture == null || webcamTexture.width < 100 || webcamTexture.height < 100)
			return;

		// get current image
		Color32[] color = webcamTexture.GetPixels32 ();
		int width = webcamTexture.width;
		int height = webcamTexture.height;


		// set texture
		if (texture == null) {
			texture = new Texture2D (width, height);

			RawImage image = GameObject.Find ("RawImage").GetComponent<RawImage> ();
			image.texture = texture;

			if (flipXY == true) {
				Vector3 scale = image.transform.localScale;
				scale.x = -1;
				scale.y = -1;
				image.transform.localScale = scale;
			}
		}
		{
			texture.SetPixels32 (color);
			texture.Apply ();
		}

		// initialize
		if (dotar == null) {
			dotar = new DotAR();

			// set camera size
			dotar.setCam (width, height);

			// set marker parameter
			dotar.setMrk (6, 6, 1.0);

			Camera camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
			camera.fieldOfView = dotar.getFOV ();
		}


		float calc_time = Time.realtimeSinceStartup;

		// detect
		{
			bool ret = dotar.execute (color);

			if (ret == true) {
				board.SetActive (true);

				board.transform.localRotation = dotar.rotation;
				board.transform.localPosition = dotar.position;

			} else {
				board.SetActive (false);
			}

			if (ret == true && flipXY == true) {
				board.transform.localRotation = Quaternion.Euler(0, 0, 180) * board.transform.localRotation;
				board.transform.localPosition = Quaternion.Euler(0, 0, 180) * board.transform.localPosition;
			}
		}

		calc_time = Time.realtimeSinceStartup - calc_time;

		text.text = "calc_time:" +  calc_time.ToString("0.000");
	}
}
