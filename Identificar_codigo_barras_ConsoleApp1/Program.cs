﻿
// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.IO;

namespace Identificar_codigo_barras_ConsoleApp1
{
    class Program
    {
        private static int quantidadeSemCodigoBarras = 0;
        private static int quantidadeTotal = 0;

        static void Main(string[] args)
        {

            var dir = new DirectoryInfo(@"C:\Users\ander\Pictures\analisar");

            var files = dir.GetFiles("*.jpg");

            foreach (var item in files)
            {
                Console.WriteLine("\n");
                TestarModelo(item.FullName, item.Name);

            }

            Console.WriteLine($"Total sem codigo: {quantidadeSemCodigoBarras}");
            Console.WriteLine($"Total com codigo: {(quantidadeTotal - quantidadeSemCodigoBarras)}");
        }

        private static void ExemploGerado()
        {
            // Create single instance of sample data from first line of dataset for model input
            Identificar_codigo_barras.ModelInput sampleData = new Identificar_codigo_barras.ModelInput()
            {
                ImageSource = @"C:\Users\ander\Pictures\analise-codigo-barras\com-codigo-barras\14643407-f9c5-4d36-aab8-865c1ccccda4_20220601143546718_page_1.jpg",
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = Identificar_codigo_barras.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");


            Console.WriteLine($"ImageSource: {@"C:\Users\ander\Pictures\analise-codigo-barras\com-codigo-barras\14643407-f9c5-4d36-aab8-865c1ccccda4_20220601143546718_page_1.jpg"}");


            Console.WriteLine($"\n\nPredicted Label value: {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        private static void TestarModelo(string imgSource, string name)
        {
            // Create single instance of sample data from first line of dataset for model input
            Identificar_codigo_barras.ModelInput sampleData = new Identificar_codigo_barras.ModelInput()
            {
                ImageSource = imgSource,
            };

            // Make a single prediction on the sample data and print results            
            var predictionResult = Identificar_codigo_barras.Predict(sampleData);

            //Console.WriteLine("Usando o modelo para fazer uma previsão única -- Comparando o Rótulo real com o Rótulo previsto a partir de dados de amostra...\n\n");


            //Console.WriteLine($"ImageSource: {imgSource}");
            if (predictionResult.Prediction.Contains("sem-codi"))            
                quantidadeSemCodigoBarras++;
            

            quantidadeTotal++;

            Console.WriteLine("[ " + predictionResult.Prediction + " ] - " +name);

            Console.WriteLine($"Valor do rótulo previsto: {predictionResult.Prediction} \nPontuações de rótulos previstas: [{String.Join(",", predictionResult.Score)}]");
            //Console.WriteLine("=============== Fim do processo ===============");
            //Console.ReadKey();
        }
    }
}
