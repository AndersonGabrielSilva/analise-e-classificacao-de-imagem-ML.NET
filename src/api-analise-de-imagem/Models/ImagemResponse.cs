using System.Collections.Generic;

namespace IdentificarCodigoDeBarras.Models
{
    public class ImagemResponse 
    {
        public ImagemResponse()
        {
            Erros = new List<string>();
        }

        public string Prediction { get; set; }

        public float[] Score { get; set; }

        public bool ContemCodigoDeBarras { get; set; }

        public List<string> Erros { get; set; }


        public void AdicionaErro(string mensagemErro)=>        
            Erros.Add(mensagemErro);
        
    }
}
