using App01_ConsultarCEP.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            botao.Clicked += BuscarCep;
        }

        private void BuscarCep(object sender, EventArgs args)
        {
            //todo: Logica
            //todo: validações
            string cepPreenchido = cep.Text.Trim();
            if (isValidCep(cepPreenchido))
            {
                var endereco = Servico.ViaCEPServico.BuscarEnderecoViaCEP(cepPreenchido);

                if (endereco.erro)
                {
                    //resultado.Text = $"Erro: {endereco.erroDescricao}";
                    DisplayAlert("ERRO", endereco.erroDescricao, "OK");
                }
                else
                {

                    resultado.Text = $"Endereço = {endereco.logradouro}\n" +
                                        $"Bairro: {endereco.bairro} {endereco.complemento}\n" +
                                        $"Cidade: {endereco.localidade}\n" +
                                        $"Estado: {endereco.uf}\n" +
                                        $"Número IBGE: {endereco.ibge}\n" +
                                        $"Número GIA: {endereco.gia}";
                }
            }

            

        }

        private bool isValidCep(string cep)
        {
            bool valid = true;
            int cepConvertido;

            if (cep.Length != 8)
            {
                valid = false;
                DisplayAlert("Erro", "Cep inválido, ele deve possuir 8 números.", "OK");

            }

            if (!int.TryParse(cep, out cepConvertido))
            {
                valid = false;
                DisplayAlert("Erro", "Cep inválido, deve possuir somente números.", "OK");
            }



            return valid;

        }
    }
}
