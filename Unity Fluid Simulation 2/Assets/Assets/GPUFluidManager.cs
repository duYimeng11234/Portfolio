using UnityEngine;

public class GPUFluidManager : MonoBehaviour
{
    public ComputeShader fluidShader;
    public float smoothingRadius = 0.8f;
    public float pressureStrength = 6f;
    private ComputeBuffer particleBuffer;
    private ParticleData[] particleData;
    private Spawner spawner;
    private int kernelID;
    private int n;

    struct ParticleData
    {
        public Vector2 position;
        public Vector2 velocity;
    }

    void Start()
    {
        spawner = GetComponent<Spawner>();
        n = spawner.particleCount;
        kernelID = fluidShader.FindKernel("CalculatePressure");
        particleBuffer = new ComputeBuffer(n, sizeof(float) * 4);
        particleData = new ParticleData[n];
        fluidShader.SetBuffer(kernelID, "particles", particleBuffer);
    }

    void Update()
    {
        for(int i = 0; i < n; i++)
        {
            particleData[i].position = spawner.transforms[i].position;
            particleData[i].velocity = spawner.velocities[i];
        }
        particleBuffer.SetData(particleData);
        fluidShader.SetInt("particleCount", n);
        fluidShader.SetFloat("smoothingRadius", smoothingRadius);
        fluidShader.SetFloat("pressureStrength", pressureStrength);
        fluidShader.SetFloat("deltaTime", Time.deltaTime);
        int groups = (n + 63) / 64;
        fluidShader.Dispatch(kernelID, groups, 1, 1);
        particleBuffer.GetData(particleData);
        for(int i = 0; i< n; i++)
        {
            spawner.velocities[i] = particleData[i].velocity;
        }
    }

    void OnDestroy()
    {
        particleBuffer?.Release();
    }
}
