using Estacionamento.Estacionamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;

namespace Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        private Veiculo _veiculo;
        private Operador _operador;
        private Patio _estacionamento;

        public ITestOutputHelper SaidaConsoleTeste;
        public PatioTestes(ITestOutputHelper saidaConsoleTeste)
        {
            _veiculo = new Veiculo();
            _operador = new Operador();
            _estacionamento = new Patio();
            SaidaConsoleTeste = saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor Carregado");
        }


        [Fact]
        public void ValidaFaturamentoDoEstacionamentoDoVeiculo()
        {
            //Arrange
            _operador.Nome = "MarceloD2";
                        
            _estacionamento.OperadorPatio = _operador;

            _veiculo.Proprietario = "Roberto Carlos";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Fusion";
            _veiculo.Placa = "KJS-9547";

            _estacionamento.RegistrarEntradaVeiculo(_veiculo);
            _estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            var faturamento = _estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", TipoVeiculo.Automovel, "Gol", "Roberto Silva")]
        [InlineData("José Ribeiro", "KSS-9532", "Cinza", TipoVeiculo.Automovel, "Palio", "Marcos Menson")]
        [InlineData("Nando Vieira", "JMA-3210", "Vermelha", TipoVeiculo.Motocicleta, "Titan 160cc", "Otário Franco")]
        public void ValidaFaturamentoComVariosVeiculos(string proprietario,
            string placa,
            string cor,
            TipoVeiculo tipoVeiculo,
            string modelo,
            string operador)
        {
            //Arrange
            _operador.Nome = operador;

            _estacionamento.OperadorPatio = _operador;
            
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = tipoVeiculo;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            _estacionamento.RegistrarEntradaVeiculo(_veiculo);
            _estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            var faturamento = _estacionamento.TotalFaturado();

            //Assent
            int valorEsperado;

            switch (_veiculo.Tipo)
            {
                case TipoVeiculo.Automovel:
                    valorEsperado = 2;
                    break;
                case TipoVeiculo.Motocicleta:
                    valorEsperado = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Assert.Equal(valorEsperado, faturamento);

        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", TipoVeiculo.Automovel, "Gol", "Roberto Silva")]
        public void LocalizaVeiculoNoPatioComBaseNaPlaca(string proprietario,
                    string placa,
                    string cor,
                    TipoVeiculo tipoVeiculo,
                    string modelo,
                    string operador)
        {
            //Arrange
            _operador.Nome = operador;

            _estacionamento.OperadorPatio = _operador;
            
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = tipoVeiculo;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            _estacionamento.RegistrarEntradaVeiculo(_veiculo);

            //Act
            var consultado = _estacionamento.PesquisaVeiculoPorPlaca(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Fact]
        public void AlterarDadosDoProprioVeiculo()
        {
            //Arrange
            _operador.Nome = "MarceloD2";

            _estacionamento.OperadorPatio = _operador;
            
            _veiculo.Proprietario = "Roberto Carlos";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Fusion";
            _veiculo.Placa = "KJS-9547";
            _estacionamento.RegistrarEntradaVeiculo(_veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "Roberto Carlos";
            veiculoAlterado.Tipo = TipoVeiculo.Automovel;
            veiculoAlterado.Cor = "Prata";
            veiculoAlterado.Modelo = "Fusion";
            veiculoAlterado.Placa = "KJS-9547";

            //Act
            var alterado = _estacionamento.AlteraDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose Invocado");
        }
    }
}
