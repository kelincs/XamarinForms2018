using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using App01_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App01_ConsultarCEP.Servico
{
    public class ViaCEPServico
    {

        private static readonly string enderecoUrl = "http://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoViaCEP(string cep)
        {

            Endereco end = new Endereco();
            try
            {
                string novoEnderecoURL = string.Format(enderecoUrl, cep);
                string conteudoJson;

                using (WebClient wc = new WebClient())
                {
                    conteudoJson = wc.DownloadString(novoEnderecoURL);
                }
                //WebClient wc = new WebClient();
                

                end = JsonConvert.DeserializeObject<Endereco>(conteudoJson);

                if (end.erro)
                {
                    end.erroDescricao = "Não foi possível obter o resultado ou o cep não existe.";
                }

                

            }
            catch(Exception e)
            {
                end.erro = true;
                end.erroDescricao = e.Message;
            }

            return end;


        }
    }
}
