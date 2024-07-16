using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.LightGbm;
using System.Reflection;


namespace myMLApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mlContext = new MLContext();

            // Ruta absoluta al archivo CSV
            var dataPath = @"C:\Users\Administrador\Documents\final_dataset_corrected.csv";
            var dataView = mlContext.Data.LoadFromTextFile<ModelInput>(dataPath, hasHeader: true, separatorChar: ';');
            // Dividir el dataset en 80% para entrenamiento y 20% para prueba


            //    ///SdcaMaximumEntropy
            //    // Dividir los datos en entrenamiento y prueba
            //    var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            //var trainData = trainTestSplit.TrainSet;
            //var testData = trainTestSplit.TestSet;

            //// Definir el pipeline de entrenamiento con MaxIterations
            //var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
            //    .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
            //    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features", maximumNumberOfIterations: 5000))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));


            //    // Entrenar el modelo
            //    var model = pipeline.Fit(trainData);

            //// Evaluar el modelo
            //var predictions = model.Transform(testData);
            //var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

            //// Mostrar métricas
            //Console.WriteLine($"Perdida logistica: {metrics.LogLoss}");
            //Console.WriteLine($"Macro presición: {metrics.MacroAccuracy}");
            //Console.WriteLine($"Micro presición: {metrics.MicroAccuracy}");
            //SdcaMaximumEntropy



            //FastTree
            //var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            //var trainData = trainTestSplit.TrainSet;
            //var testData = trainTestSplit.TestSet;

            //// Definir el pipeline de entrenamiento
            //var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
            //    .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
            //    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(
            //        binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree()))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            //// Entrenar el modelo
            //var model = pipeline.Fit(trainData);

            //// Evaluar el modelo
            //var predictions = model.Transform(testData);
            //var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

            //// Mostrar métricas
            //Console.WriteLine($"Perdida Logaritmica: {metrics.LogLoss}");
            //Console.WriteLine($"Macro presición: {metrics.MacroAccuracy}");
            //Console.WriteLine($"Micro presición: {metrics.MicroAccuracy}");


            ////validacion cruzada
            //// Definir el pipeline de entrenamiento


            //// Validación cruzada
            //var cvResults = mlContext.MulticlassClassification.CrossValidate(dataView, pipeline, numberOfFolds: 5);

            //// Mostrar métricas de validación cruzada
            //for (int i = 0; i < cvResults.Count; i++)
            //{
            //    var result = cvResults[i];
            //    Console.WriteLine($"Fold: {i + 1}");
            //    Console.WriteLine($"Log-loss: {result.Metrics.LogLoss}");
            //    Console.WriteLine($"Macro accuracy: {result.Metrics.MacroAccuracy}");
            //    Console.WriteLine($"Micro accuracy: {result.Metrics.MicroAccuracy}");
            //    Console.WriteLine();
            //}

            // validacion cruxzada
            //FastTree



            //LightGBM

            // Definir el pipeline de entrenamiento
            //var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
            //    .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
            //    .Append(mlContext.MulticlassClassification.Trainers.LightGbm(
            //        new LightGbmMulticlassTrainer.Options
            //        {
            //            LabelColumnName = "Label",
            //            FeatureColumnName = "Features"
            //        }))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            //// Validación cruzada
            //var cvResults = mlContext.MulticlassClassification.CrossValidate(dataView, pipeline, numberOfFolds: 5);

            //// Mostrar métricas de validación cruzada
            //for (int i = 0; i < cvResults.Count; i++)
            //{
            //    var result = cvResults[i];
            //    Console.WriteLine($"Fold: {i + 1}");
            //    Console.WriteLine($"Perdida logaritmica: {result.Metrics.LogLoss}");
            //    Console.WriteLine($"Macro presición: {result.Metrics.MacroAccuracy}");
            //    Console.WriteLine($"Micro presición: {result.Metrics.MicroAccuracy}");
            //    Console.WriteLine();
            //}
            //LightGBM 

            //matriz de confusion
            // Dividir los datos en entrenamiento y prueba
            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var trainData = trainTestSplit.TrainSet;
            var testData = trainTestSplit.TestSet;

            //// Algoritmos a probar
            //var algorithms = new (string name, IEstimator<ITransformer> estimator)[]
            //{
            //("SdcaMaximumEntropy", mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
            //    .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
            //    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))),

            //("FastTree", mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
            //    .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
            //    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(
            //        binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree()))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))),

            //("LightGbm", mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
            //    .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
            //    .Append(mlContext.MulticlassClassification.Trainers.LightGbm(
            //        new LightGbmMulticlassTrainer.Options
            //        {
            //            LabelColumnName = "Label",
            //            FeatureColumnName = "Features"
            //        }))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel")))
            //};

            //// Evaluar cada algoritmo
            //foreach (var (name, estimator) in algorithms)
            //{
            //    Console.WriteLine($"Evaluando {name}...");
            //    var model = estimator.Fit(trainData);
            //    var predictions = model.Transform(testData);
            //    var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

            //    // Mostrar métricas
            //    Console.WriteLine($"Perdida Logaritmica: {metrics.LogLoss}");
            //    Console.WriteLine($"Macro precisión: {metrics.MacroAccuracy}");
            //    Console.WriteLine($"Micro precisión: {metrics.MicroAccuracy}");

            //    // Generar y mostrar la matriz de confusión
            //    var confusionMatrix = metrics.ConfusionMatrix;
            //    var matrix = confusionMatrix.GetFormattedConfusionTable();
            //    Console.WriteLine("Matriz de Confusión:");
            //    Console.WriteLine(matrix);

            //    // Guardar el modelo
            //    var modelPath = "quintiles.zip";
            //    mlContext.Model.Save(model, trainData.Schema, modelPath);
            //    Console.WriteLine($"Modelo guardado en {modelPath}");
            //}
            ////matriz de confusion


            // Definir el pipeline de entrenamiento para FastTree
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "quintil_correcto")
                .Append(mlContext.Transforms.Concatenate("Features", "decIngresoFam_Ficha"))
                .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(
                    binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree()))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Entrenar el modelo
            Console.WriteLine("Entrenando el modelo FastTree...");
            var model = pipeline.Fit(trainData);

            // Evaluar el modelo
            var predictions = model.Transform(testData);
            var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

            // Mostrar métricas
            Console.WriteLine($"Log-loss: {metrics.LogLoss}");
            Console.WriteLine($"Macro accuracy: {metrics.MacroAccuracy}");
            Console.WriteLine($"Micro accuracy: {metrics.MicroAccuracy}");

            // Guardar el modelo
            var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "fastTreeModel1.zip");
            mlContext.Model.Save(model, trainData.Schema, modelPath);
            Console.WriteLine($"Modelo guardado en {modelPath}");


        }

        // Clase para representar los datos de entrada
        public class ModelInput
        {
            [LoadColumn(0)]
            public float decIngresoFam_Ficha { get; set; }

            [LoadColumn(1)]
            public float quintil_correcto { get; set; }
        }
    }
 }

    
