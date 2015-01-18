using UnityEngine;
using System.Collections;

/** 
 * Programatically generated ribbon effect. 
 * Leading edge is oriented to the object's transform.
 */

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Ribbon : MonoBehaviour
{

	// Properties
	// ----------------------------------------------------------------------------
	
	/** How long the ribbon effect emits full strength samples. */
	public float Duration = 0.5f;
	
	/** How long the ribbon effect takes to fade in. */
	public float FadeInDuration = 0.1f;

	/** How long the ribbon effect takes to fade out. */
	public float FadeOutDuration = 0.1f;

	/** Delay before starting to emit ribbon samples. */
	public float StartDelay = 0;
	
	/** How long a ribbon sample remains active. */
	public float StartLifetime = 0.5f;

	/** Width at the start of the ribbon. */
	public float StartWidth = 1;

	/** Width at the end of the ribbon. */
	public float EndWidth = 0;

	/** Color at the start of the ribbon. */
	public Color StartColor;
	
	/** Color at the end of the ribbon. */
	public Color EndColor;

	/** Orientation of the ribbon in transform's local space. */
	public Vector3 Orientation = Vector3.forward;

	/** Minimum distance between ribbon samples. */
	public float MinDistance = 0.1f;

	/** Whether ribbon starts playing automatically. */
	public bool PlayOnAwake = true;

    /** Whether riboon plays forever. */
    public bool PlayForever = true;

	/** Whether the ribbon effect is playing. */
	public bool Playing
		{ get { return playing; } }



	// Members
	// ----------------------------------------------------------------------------
	
	/** A sample in the ribbon trail. */
	private class Sample
	{
		public Vector3 Position;
		public Vector3 Orientation;
		public float Timestamp;
		public float Alpha;
	}

	/** The mesh geometry. */
	private Mesh mesh;

	/** Buffer of ribbon sample points. */
	private Sample[] samples;

	/** Maximum number of samples to take per second. */
	private int maxSamplesPerSecond = 60;

	/** Number of samples in the sample buffer. */
	private int sampleCount;

	/** Current starting index in the sample buffer. */
	private int startIndex;

	/** Number of active samples. */
	private int activeCount;

	/** Whether ribbon effect is playing. */
	private bool playing = false;

	/** Time at which ribbon effect was started. */
	private float startTime;

	/** Time at which ribbon effect should stop. */
	private float stopTime;

	/** Time at which ribbon effect should stop. */
	private float fadeOutTime;

	/** Time at which ribbon samples should start emitting. */
	private float emitTime;

	
	// Unity Implementation
	// ----------------------------------------------------------------------------
	
	/** Initialization. */
	void Awake()
	{
		// Create a new mesh.
		mesh = new Mesh();
		mesh.MarkDynamic();

		// Initialize the sample buffer.
		InitializeSamples();

		// Generate mesh geometry.
		InitializeMesh();

		// Assign mesh to mesh filter.
		GetComponent<MeshFilter>().mesh = mesh;

		// Start emitting if needed.
		if (PlayOnAwake)
			Play();
	}

	/** Update. */
	void LateUpdate()
	{
		// Check if we should stop playing.
		float t = Time.time;
        if (t >= stopTime && !PlayForever)
			Stop();

		// Update the sample buffer.
		if (playing && t >= emitTime)
			UpdateSamples();

		// Update the mesh to reflect ribbon state.
		UpdateMesh();
	}

	/** Enable. */
	void OnEnable()
	{
		// Clear the ribbon to starting state.
		Clear();

		// Start emitting if needed.
		if (PlayOnAwake)
			Play();
	}


	// Public Methods
	// ----------------------------------------------------------------------------

	/** Starts playing the ribbon effect. */
	public void Play()
	{
		// Check if we're already playing.
		if (playing)
			return;

		// Put ourselves into the playing state.
		startTime = Time.time;
		stopTime = startTime + Duration;
		emitTime = startTime + StartDelay;
		playing = true;
	}

	/** Stops playing the ribbon effect. */
	public void Stop()
	{
		// Check if we're already stopped.
		if (!playing)
			return;
		
		// No longer in playing state.
		playing = false;
	}

	/** Clears the ribbon to default state. */
	public void Clear()
	{
		// Reset sample buffer.
		ResetSamples();
		
		// Reset the mesh.
		ResetMesh();
	}



	// Private Methods
	// ----------------------------------------------------------------------------

	/** Initialize the sample buffer. */
	private void InitializeSamples()
	{
		// Figure out how many samples to use in the mesh.
		sampleCount = Mathf.CeilToInt(StartLifetime * maxSamplesPerSecond);

		// Allocate circular sample buffer.
		samples = new Sample[sampleCount];
		startIndex = -1;
		activeCount = 0;
	}

	/** Reset the sample buffer. */
	private void ResetSamples()
	{
		// Reset buffer state.
		startIndex = -1;
		activeCount = 0;
	}

	/** Update the sample buffer. */
	private void UpdateSamples()
	{
		// Check if current position is too close to previous sample.
		if (activeCount > 0)
		{
			Vector3 o = samples[startIndex].Position;
			Vector3 p = transform.position;
			if (Vector3.Distance(o, p) < MinDistance)
				return;
		}

		// Ensure current sample exists.
		startIndex = (startIndex + 1) % sampleCount;
		if (samples[startIndex] == null)
			samples[startIndex] = new Sample();

		// Record a new sample.
		Sample sample = samples[startIndex];
		sample.Position = transform.position;
		sample.Orientation = transform.TransformDirection(Orientation);
		sample.Timestamp = Time.time;
		sample.Alpha = 1; 

		// Fade sample in/out as needed.
		float t = sample.Timestamp;
		if (t < (startTime + FadeInDuration))
			sample.Alpha = Mathf.Lerp(0, 1, Mathf.Max(0, t - startTime) / FadeInDuration);
		else if (sample.Timestamp > (stopTime - FadeOutDuration))
			sample.Alpha = Mathf.Lerp(1, 0, Mathf.Max(0, t - (stopTime - FadeOutDuration)) / FadeOutDuration);

		// Update sample buffer state.
		activeCount = Mathf.Min(sampleCount, activeCount + 1);
	}

	/** Initialize the mesh geometry. */
	private void InitializeMesh()
	{
		// Generate mesh vertex attributes.
		int vertexCount = sampleCount * 2;
		Vector3[] vertices = new Vector3[vertexCount];
		Vector2[] uv = new Vector2[vertexCount];
		Color[] colors = new Color[vertexCount];

		// Populate the vertex attributes.
		for (int i = 0; i < sampleCount; i++)
		{
			float u = (float) i / (sampleCount - 1);
			vertices[i] = Vector3.zero;
			uv[i] = new Vector2(u, 0);
			colors[i] = Color.white;
			int j = i + sampleCount;
			vertices[j] = Vector3.zero;
			uv[j] = new Vector2(u, 1);
			colors[j] = Color.white;
		}

		// Populate triangle indices.
		int quadCount = sampleCount - 1;
		int indexCount = quadCount * 6;
		int[] triangles = new int[indexCount];
		int index = 0;
		for (int i = 0; i < quadCount; i++)
		{
			int index0 = i + sampleCount;
			int index1 = index0 + 1;
			int index2 = i;
			int index3 = i + 1;

			triangles[index++] = index0;
			triangles[index++] = index2;
			triangles[index++] = index1;
			triangles[index++] = index2;
			triangles[index++] = index3;
			triangles[index++] = index1;
		}

		// Assign mesh attribute arrays.
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.colors = colors;
		mesh.triangles = triangles;
	}

	/** Update mesh attributes to reflect current ribbon state. */
	private void UpdateMesh()
	{
		// Check if we have any active samples.
		if (activeCount == 0)
			return;

		// Get vertex attributes from mesh.
		Vector3[] vertices = mesh.vertices;
		Color[] colors = mesh.colors;

		// Get current sample buffer state.
		int index = startIndex;
		Sample sample = samples[index];
		float t = Time.time;
		int count = 0;

		// Populate the vertex attributes from sample buffer.
		// Start with the most recent sample and work backwards.
		for (int i = 0; i < sampleCount; i++)
		{
			// Get a fresh sample from the sample buffer.
			// Stop updating once we run out of active samples.
			if (count < activeCount)
			{
				sample = samples[index];
				index--; 
				if (index < 0) 
					index = sampleCount - 1;
				count++;
			}

			// Determine attributes at sample location.
			float age = Mathf.Clamp01((t - sample.Timestamp) / StartLifetime);
			float width = Mathf.Lerp(StartWidth, EndWidth, age);
			Vector3 delta = (width * 0.5f) * sample.Orientation;
			Color color = Color.Lerp(StartColor, EndColor, age);

			// Apply sample opacity.
			color.a *= sample.Alpha;

			// Update vertex attributes.
			vertices[i] = transform.InverseTransformPoint(sample.Position + delta);
			colors[i] = color;
			int j = i + sampleCount;
			vertices[j] = transform.InverseTransformPoint(sample.Position - delta);
			colors[j] = color;

            // Debug.DrawLine(sample.Position, sample.Position + delta, Color.red, 0.1f);

			// If we've reached maximum age, stop updating samples.
			if (age >= 1)
				count = activeCount;
		}

		// Update mesh with new values.
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.RecalculateBounds();
	}

	/** Reset mesh attributes to reflect current ribbon state. */
	private void ResetMesh()
	{
		// Check that mesh has been initialized.
		if (!mesh)
			return;

		// Reset each sample in the mesh.
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < sampleCount; i++)
		{
			vertices[i] = Vector3.zero;
			vertices[i + sampleCount] = Vector3.zero;
		}

		// Update mesh with new values.
		mesh.vertices = vertices;
	}


}
