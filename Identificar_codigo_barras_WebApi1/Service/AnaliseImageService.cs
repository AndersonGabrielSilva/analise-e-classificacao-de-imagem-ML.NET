using IdentificarCodigoDeBarras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdentificarCodigoDeBarras.Service
{
    public class AnaliseImageService
    {
        #region Atributos
        private List<string> Erros;

        private readonly PredictionEnginePool<Identificar_codigo_barras.ModelInput, Identificar_codigo_barras.ModelOutput> _predictionEnginePool;
        #endregion

        #region Construtor
        public AnaliseImageService(PredictionEnginePool<Identificar_codigo_barras.ModelInput, Identificar_codigo_barras.ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
            Erros = new List<string>();
        }    
        #endregion

        #region Analise da Imagem
        public async Task<ImagemResponse> VerificaCodigoDeBarras(IFormFile arquivo)
        {
            var response = new ImagemResponse();

            if (arquivo == null || arquivo.Length == 0)
            {
                AddMeensagemErro("O Arquivo não foi encontrado");
                return response;
            }

            var filepath = await SalvarImagemTemp(arquivo);

            if (string.IsNullOrEmpty(filepath))            
                AddMeensagemErro("Não foi possivel montar o caminho para salvar o arquivo.");            

            //Define parametro de entrada para analise
            var input = new Identificar_codigo_barras.ModelInput
            {
                ImageSource = filepath
            };

            // Verifica analise
            var result = _predictionEnginePool.Predict(input);
            var _comtemCodigoDebarras = result.Prediction.Contains("com-codigo");

            RemoverImagemTemp(filepath);

            return new ImagemResponse
            {
                Prediction = result.Prediction,
                Score = result.Score,
                ContemCodigoDeBarras = _comtemCodigoDebarras
            };
        }
        
        private async Task<string> SalvarImagemTemp(IFormFile arquivo)
        {            
            var filePath = Path.Combine("Temp", Guid.NewGuid().ToString()+".jpg");

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }                                              
            }
            catch (Exception ex)
            {
                var msgErro = ex.Message;
                filePath = string.Empty;
            }

            return filePath;
        }

        private void RemoverImagemTemp(string filepath)
        {
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
                return;

            File.Delete(filepath);
        }
        #endregion

        #region Metodos de Erros
        public bool PossuiErros() => Erros.Any();

        public void AddMeensagemErro(string mensagem)=>        
            Erros?.Add(mensagem);
        
        public List<string> ObterErros() =>
           Erros;
        #endregion
    }
}
