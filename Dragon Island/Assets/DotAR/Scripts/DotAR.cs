#define DOTAR_USE_NATIVE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.UI;

public class DotAR {

	// dst: rotation and position
	public Quaternion rotation;
	public Vector3 position;

	#if DOTAR_USE_NATIVE && (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_ANDROID || UNITY_IPHONE)

	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
	[DllImport("DotAR_win")] private static extern IntPtr DotAR_init();
	[DllImport("DotAR_win")] private static extern void DotAR_del(IntPtr detector);
	[DllImport("DotAR_win")] private static extern void DotAR_setCam(IntPtr detector, int dsize0, int dsize1);
	[DllImport("DotAR_win")] private static extern void DotAR_setMrk(IntPtr detector, int dsize0, int dsize1, double distance);
	[DllImport("DotAR_win")] private static extern bool DotAR_execute(IntPtr detector, IntPtr src, int dsize0, int dsize1, int ch);
	[DllImport("DotAR_win")] private static extern void DotAR_getPose(IntPtr detector, float[] dst);
	[DllImport("DotAR_win")] private static extern void DotAR_getFOV(IntPtr detector, float[] dst);
	#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
	[DllImport("DotAR_osx")] private static extern IntPtr DotAR_init();
	[DllImport("DotAR_osx")] private static extern void DotAR_del(IntPtr detector);
	[DllImport("DotAR_osx")] private static extern void DotAR_setCam(IntPtr detector, int dsize0, int dsize1);
	[DllImport("DotAR_osx")] private static extern void DotAR_setMrk(IntPtr detector, int dsize0, int dsize1, double distance);
	[DllImport("DotAR_osx")] private static extern bool DotAR_execute(IntPtr detector, IntPtr src, int dsize0, int dsize1, int ch);
	[DllImport("DotAR_osx")] private static extern void DotAR_getPose(IntPtr detector, float[] dst);
	[DllImport("DotAR_osx")] private static extern void DotAR_getFOV(IntPtr detector, float[] dst);
	#elif UNITY_ANDROID
	[DllImport("DotAR_and")] private static extern IntPtr DotAR_init();
	[DllImport("DotAR_and")] private static extern void DotAR_del(IntPtr detector);
	[DllImport("DotAR_and")] private static extern void DotAR_setCam(IntPtr detector, int dsize0, int dsize1);
	[DllImport("DotAR_and")] private static extern void DotAR_setMrk(IntPtr detector, int dsize0, int dsize1, double distance);
	[DllImport("DotAR_and")] private static extern bool DotAR_execute(IntPtr detector, IntPtr src, int dsize0, int dsize1, int ch);
	[DllImport("DotAR_and")] private static extern void DotAR_getPose(IntPtr detector, float[] dst);
	[DllImport("DotAR_and")] private static extern void DotAR_getFOV(IntPtr detector, float[] dst);
	#elif UNITY_IPHONE
	[DllImport("__Internal")] private static extern IntPtr DotAR_init();
	[DllImport("__Internal")] private static extern void DotAR_del(IntPtr detector);
	[DllImport("__Internal")] private static extern void DotAR_setCam(IntPtr detector, int dsize0, int dsize1);
	[DllImport("__Internal")] private static extern void DotAR_setMrk(IntPtr detector, int dsize0, int dsize1, double distance);
	[DllImport("__Internal")] private static extern bool DotAR_execute(IntPtr detector, IntPtr src, int dsize0, int dsize1, int ch);
	[DllImport("__Internal")] private static extern void DotAR_getPose(IntPtr detector, float[] dst);
	[DllImport("__Internal")] private static extern void DotAR_getFOV(IntPtr detector, float[] dst);
	#endif

	// detector ptr
	private IntPtr detector = IntPtr.Zero;

	private int[] dsize = null;

	private GCHandle handle;
	private IntPtr color_ptr = IntPtr.Zero;

	public DotAR(){
		detector = DotAR_init ();
	}
	~DotAR(){
		DotAR_del (detector);
	}

	public void setMrk(int width, int height, double distance){
		DotAR_setMrk (detector, width, height, distance);
	}

	public void setCam(int width, int height){
		dsize = new int[2] { width, height };
		DotAR_setCam (detector, width, height);
	}

	public float getFOV() {
		float[] fov = new float[1];
		DotAR_getFOV (detector, fov);
		return fov[0];
	}

	public bool execute(Color32[] color) {
		if (dsize == null)
			return false;
		
		handle = GCHandle.Alloc(color, GCHandleType.Pinned);
		color_ptr = handle.AddrOfPinnedObject();

		bool ret = DotAR_execute (detector, color_ptr, dsize [0], dsize [1], 4);

		if (ret == true) {
			float[] pose = new float[7];
			DotAR_getPose (detector, pose);

			rotation = new Quaternion (pose [0], pose [1], pose [2], pose [3]);
			position = new Vector3 (pose [4], pose [5], pose [6]);
		}
		
		handle.Free ();
		return ret;
	}
	#else

	// detector class
	private DotAR_gen.Detector detector = null;

	// buffer
	private byte[,] gray = null;

	public DotAR(){
		detector = new DotAR_gen.Detector();
	}

	public void setMrk(int width, int height, double distance){
		detector.setMrk (width, height, distance);
	}

	public void setCam(int width, int height){
		detector.setCam (width, height);
		gray = new byte[height, width];
	}

	public float getFOV() {
		return detector.getFOV ();
	}

	public bool execute(Color32[] color) {
		
		// extract 1ch
		int cnt = 0;
		for (int v = 0; v < gray.GetLength (0); v++) {
			for (int u = 0; u < gray.GetLength (1); u++) {
				gray [v, u] = color [cnt++].g;
			}
		}

		if (detector.execute (gray) == true) {
			float[] pose = detector.getPose ();

			rotation = new Quaternion (pose [0], pose [1], pose [2], pose [3]);
			position = new Vector3 (pose [4], pose [5], pose [6]);

			return true;
		} else {
			return false;
		}
	}
	#endif

}
