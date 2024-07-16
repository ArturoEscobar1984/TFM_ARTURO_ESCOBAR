using System;
using System.Globalization;
using System.IO;
using System.Web.Services;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace WebService
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private static readonly string modelPath = @"C:\Users\Administrador\Documents\Visual Studio 2022\Projects\myMLApp\myMLApp\bin\Debug\net8.0\fastTreeModel.zip";
        private static readonly MLContext mlContext = new MLContext();
        private static readonly ITransformer loadedModel;
        private static readonly PredictionEngine<ModelInput, ModelOutput> predictionEngine;

        static WebService1()
        {
            if (!System.IO.File.Exists(modelPath))
            {
                throw new FileNotFoundException($"El archivo del modelo no se encuentra en la ruta especificada: {modelPath}");
            }

            loadedModel = mlContext.Model.Load(modelPath, out var modelInputSchema);

            var inputSchemaDefinition = SchemaDefinition.Create(typeof(ModelInput));
            inputSchemaDefinition[nameof(ModelInput.quintil_correcto)].ColumnType = NumberDataViewType.Single;

            predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(loadedModel, inputSchemaDefinition: inputSchemaDefinition);
        }

        [WebMethod]
        public ModelOutput Predecir(string decIngresoFam_Ficha)
        {
            // Reemplazar la coma por un punto
            string sanitizedInput = decIngresoFam_Ficha.Replace(',', '.');

            // Convertir a float
            if (!float.TryParse(sanitizedInput, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedValue))
            {
                throw new ArgumentException("El valor ingresado no es un número válido.");
            }

            var input = new ModelInput { decIngresoFam_Ficha = parsedValue };
            var prediction = predictionEngine.Predict(input);
            prediction.PredictedLabelMessage = $"Usted está ubicado en el quintil {prediction.PredictedLabel}";
            return prediction;
        }
    }

    // Clase para representar los datos de entrada
    public class ModelInput
    {
        public float decIngresoFam_Ficha { get; set; }
        public float quintil_correcto { get; set; } // Asegurarse de que esta columna está presente
    }

    // Clase para representar la salida del modelo
    public class ModelOutput
    {
        public float[] Score { get; set; }
        public float PredictedLabel { get; set; } // Ajustar el tipo a Single
        public string PredictedLabelMessage { get; set; } // Agregar el mensaje personalizado
    }
}
