using System;

class Perceptron
{
    static void Main(string[] args)
    {
        // Definição dos dados de treinamento (tamanho, peso, viés)
        double[][] dadosTreinamento = new double[][]
        {
            new double[] {0.6, 0.7, 1},  // Laranja
            new double[] {0.8, 0.9, 1},  // Laranja
            new double[] {0.3, 0.2, 1},  // Maçã
            new double[] {0.4, 0.5, 1},  // Maçã
            new double[] {0.9, 0.85, 1}, // Laranja
            new double[] {0.2, 0.3, 1}   // Maçã
        };

        // Rótulos correspondentes (1 = Laranja, 0 = Maçã)
        int[] rotulos = { 1, 1, 0, 0, 1, 0 };

        // Treinamento do perceptron
        double[] pesosFinais;
        double[][] historicoPesos = TreinarPerceptron(dadosTreinamento, rotulos, out pesosFinais);

        // Novos casos para classificar
        double[][] novosCasos = new double[][]
        {
            new double[] {0.7, 0.8, 1},
            new double[] {0.4, 0.4, 1},
            new double[] {0.5, 0.6, 1},
            new double[] {0.9, 0.95, 1},
            new double[] {0.3, 0.3, 1}
        };

        // Classificar os novos casos
        string[] previsoes = Classificar(pesosFinais, novosCasos);

        // Mostrar os resultados
        Console.WriteLine("Pesos Finais: " + string.Join(", ", pesosFinais));
        Console.WriteLine("Classificações dos novos casos:");
        foreach (var previsao in previsoes)
        {
            Console.WriteLine(previsao);
        }
    }

    // Função de treino do perceptron
    static double[][] TreinarPerceptron(double[][] dados, int[] rotulos, out double[] pesosFinais, double taxaAprendizado = 0.1, int epocas = 6)
    {
        Random random = new Random();
        // Inicializa pesos aleatórios
        pesosFinais = new double[] { random.NextDouble(), random.NextDouble(), random.NextDouble() };
        double[][] historicoPesos = new double[epocas + 1][];
        historicoPesos[0] = (double[])pesosFinais.Clone();

        // Treina o perceptron
        for (int epoca = 0; epoca < epocas; epoca++)
        {
            for (int i = 0; i < dados.Length; i++)
            {
                double saidaLinear = ProdutoEscalar(dados[i], pesosFinais);
                int previsao = saidaLinear >= 0 ? 1 : 0;
                int erro = rotulos[i] - previsao;

                // Atualiza os pesos se a predição estiver errada
                for (int j = 0; j < pesosFinais.Length; j++)
                {
                    pesosFinais[j] += taxaAprendizado * erro * dados[i][j];
                }
            }
            historicoPesos[epoca + 1] = (double[])pesosFinais.Clone();
        }
        return historicoPesos;
    }

    // Função para classificar os novos casos
    static string[] Classificar(double[] pesos, double[][] novosDados)
    {
        string[] previsoes = new string[novosDados.Length];
        for (int i = 0; i < novosDados.Length; i++)
        {
            double saida = ProdutoEscalar(novosDados[i], pesos);
            previsoes[i] = saida >= 0 ? "Laranja" : "Maçã";
        }
        return previsoes;
    }

    // Função auxiliar para calcular o produto escalar
    static double ProdutoEscalar(double[] dados, double[] pesos)
    {
        double resultado = 0;
        for (int i = 0; i < dados.Length; i++)
        {
            resultado += dados[i] * pesos[i];
        }
        return resultado;
    }
}
