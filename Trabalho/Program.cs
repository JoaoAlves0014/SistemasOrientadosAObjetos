using System;

struct Retangulo
{
    public double Largura;
    public double Altura;
    public double CalcularArea() => Largura * Altura;
    public double CalcularPerimetro() => 2 * (Largura + Altura);
}

struct Data
{
    public int Dia;
    public int Mes;
    public int Ano;
    public bool EhValida() => DateTime.TryParse($"{Ano}-{Mes}-{Dia}", out _);
}

struct Cor
{
    public byte R, G, B;
    public string ParaHexadecimal() => $"#{R:X2}{G:X2}{B:X2}";
}

struct Produto
{
    public string Nome;
    public double Preco;
    public int Quantidade;
    public double ValorTotalEstoque() => Preco * Quantidade;
}

class Program
{
    static void Main()
    {
        Retangulo retangulo = new Retangulo();
        Data data = new Data();
        Cor cor = new Cor();
        Produto produto = new Produto();

        bool sair = false;

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== MENU PRINCIPAL =====");
            Console.WriteLine("1 - Cadastrar dados");
            Console.WriteLine("2 - Visualizar dados");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("== Cadastro de Retângulo ==");
                    Console.Write("Largura: ");
                    retangulo.Largura = double.Parse(Console.ReadLine());
                    Console.Write("Altura: ");
                    retangulo.Altura = double.Parse(Console.ReadLine());

                    Console.WriteLine("\n== Cadastro de Data ==");
                    Console.Write("Dia: ");
                    data.Dia = int.Parse(Console.ReadLine());
                    Console.Write("Mês: ");
                    data.Mes = int.Parse(Console.ReadLine());
                    Console.Write("Ano: ");
                    data.Ano = int.Parse(Console.ReadLine());

                    Console.WriteLine("\n== Cadastro de Cor (RGB) ==");
                    Console.Write("R: ");
                    cor.R = byte.Parse(Console.ReadLine());
                    Console.Write("G: ");
                    cor.G = byte.Parse(Console.ReadLine());
                    Console.Write("B: ");
                    cor.B = byte.Parse(Console.ReadLine());

                    Console.WriteLine("\n== Cadastro de Produto ==");
                    Console.Write("Nome: ");
                    produto.Nome = Console.ReadLine();
                    Console.Write("Preço: ");
                    produto.Preco = double.Parse(Console.ReadLine());
                    Console.Write("Quantidade: ");
                    produto.Quantidade = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nDados cadastrados com sucesso!");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("== Escolha o que deseja visualizar ==");
                    Console.WriteLine("1 - Retângulo");
                    Console.WriteLine("2 - Data");
                    Console.WriteLine("3 - Cor");
                    Console.WriteLine("4 - Produto");
                    Console.Write("Digite a opção: ");
                    string vis = Console.ReadLine();

                    Console.WriteLine();

                    switch (vis)
                    {
                        case "1":
                            Console.WriteLine($"Retângulo -> Área: {retangulo.CalcularArea()} | Perímetro: {retangulo.CalcularPerimetro()}");
                            break;
                        case "2":
                            Console.WriteLine($"Data: {data.Dia}/{data.Mes}/{data.Ano} -> Válida? {data.EhValida()}");
                            break;
                        case "3":
                            Console.WriteLine($"Cor RGB: ({cor.R}, {cor.G}, {cor.B}) -> Hex: {cor.ParaHexadecimal()}");
                            break;
                        case "4":
                            Console.WriteLine($"Produto: {produto.Nome}, Preço: R$ {produto.Preco:F2}, Quantidade: {produto.Quantidade}, Total: R$ {produto.ValorTotalEstoque():F2}");
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                    break;

                case "0":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }

        Console.WriteLine("Programa encerrado.");
    }
}
