using Estacionamento.Estacionamento.Models;
using Xunit.Abstractions;

namespace Estacionamento.Testes
{
    public class VeiculoTestes : IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTeste;

        public VeiculoTestes(ITestOutputHelper saidaConsoleTeste)
        {
            SaidaConsoleTeste = saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor Carregado");
            veiculo = new Veiculo();
        }

        [Fact]
        //[Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerarComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            veiculo.Acelerar(10);
            //Assent
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        //[Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFrearComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact(Skip = "Teste ainda não implementado, ignorar!")]
        public void ValidaNomeProprietarioDoVeiculo()
        {

        }

        [Fact]
        public void FichaDeInformacaoDoVeiculos() 
        {
            //Arrange
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Carlos Andrade";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "RTV-1784";
            veiculo.Cor = "Vermelho";
            veiculo.Modelo = "Polo";

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do Veículo:", dados);

        }

        [Fact]
        public void TestarNomeProprietarioTamanhoNome() 
        {
            //Arrange
            string nomeProprietario = "AB";

            //Assert
            Assert.Throws<System.FormatException>(

                () => new Veiculo(nomeProprietario)
                );



        }

        [Fact]
        public void TestarSeparadorDaPlacaDoVeiculo()
        {
            //Arrange
            string placa = "ASDF9999";

            //Act
            var mensagem = Assert.Throws<System.FormatException>(
                  () => new Veiculo() .Placa = placa
                );

            //Assert
            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose Invocado");
        }
    }
}