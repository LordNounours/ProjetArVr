using UnityEngine;
using Unity.Sentis;

public class ClassifyCardTest : MonoBehaviour
{
    [SerializeField] private Texture2D inputTexture;
    [SerializeField] private ModelAsset modelAsset;
    [SerializeField] private float[] results_final;

    private Model runtimeModel;
    private Worker worker; // Utilisation de Worker (non IWorker)
    private Tensor inputTensor; // Tensor générique

    private void Start()
    {
        // Charger le modèle
        runtimeModel = ModelLoader.Load(modelAsset);

        // Créer un Worker
        worker = new Worker(runtimeModel, BackendType.GPUCompute);
        
        ExecuteModel();
    }

    private void ExecuteModel()
    {
        // Libérer l'ancien tensor s'il existe
        inputTensor?.Dispose();

        // Créer le tensor à partir de la texture (aucun type générique requis ici)
        inputTensor = TextureConverter.ToTensor(inputTexture, 640, 640, 3);

        // Exécuter le modèle
        worker.Schedule(inputTensor);

        // Obtenir la sortie du modèle
        Tensor<float> outputTensor = worker.PeekOutput() as Tensor<float>;
        float[] results = outputTensor.DownloadToArray(); // Convertir les données en tableau float[]

        float[] rawResults = new float[results.Length];

        // Copier les résultats dans un tableau brut
        for (int i = 0; i < results.Length; i++)
        {
            rawResults[i] = results[i];
        }

        // Appliquer Softmax
        results_final = ApplySoftmax(rawResults);
        outputTensor.Dispose(); // Libérer le tensor de sortie
    }

    private float[] ApplySoftmax(float[] logits)
    {
        float maxLogit = float.MinValue;

        // Trouver le maximum pour éviter l'overflow numérique
        foreach (float logit in logits)
        {
            if (logit > maxLogit) maxLogit = logit;
        }

        float sumExp = 0f;
        float[] expValues = new float[logits.Length];

        // Calculer exp(x - maxLogit) pour chaque logit
        for (int i = 0; i < logits.Length; i++)
        {
            expValues[i] = Mathf.Exp(logits[i] - maxLogit);
            sumExp += expValues[i];
        }

        // Normaliser pour obtenir les probabilités
        for (int i = 0; i < expValues.Length; i++)
        {
            expValues[i] /= sumExp;
        }

        return expValues;
    }

    private void OnDisable()
    {
        // Libérer les ressources
        inputTensor?.Dispose();
        worker.Dispose();
    }
}
