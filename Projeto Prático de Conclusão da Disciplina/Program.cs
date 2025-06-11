using System;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    private static GerenciadorClinica gerenciador = new GerenciadorClinica();

    public static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo ao Sistema de Gerenciamento de Clínica Médica!");

        PopularDadosIniciais(); 

        while (true)
        {
            ExibirMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarNovoMedico();
                    break;
                case "2":
                    CadastrarNovoPaciente();
                    break;
                case "3":
                    AgendarNovaConsulta();
                    break;
                case "4":
                    gerenciador.ListarConsultas();
                    break;
                case "5":
                    BuscarConsultasUI();
                    break;
                case "6":
                    Console.WriteLine("Saindo do sistema. Até mais!");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                    break;
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void ExibirMenu()
    {
        Console.WriteLine("\n--- Menu Principal ---");
        Console.WriteLine("1. Cadastrar Médico");
        Console.WriteLine("2. Cadastrar Paciente");
        Console.WriteLine("3. Agendar Consulta");
        Console.WriteLine("4. Listar Consultas Agendadas");
        Console.WriteLine("5. Buscar Consultas");
        Console.WriteLine("6. Sair");
        Console.Write("Escolha uma opção: ");
    }

    private static void CadastrarNovoMedico()
    {
        Console.WriteLine("\n--- Cadastrar Novo Médico ---");
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Especialidade: ");
        string especialidade = Console.ReadLine();
        Console.Write("CRM: ");
        string crm = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(especialidade) || string.IsNullOrWhiteSpace(crm))
        {
            Console.WriteLine("Erro: Todos os campos são obrigatórios.");
            return;
        }

        Medico novoMedico = new Medico(nome, especialidade, crm);
        gerenciador.CadastrarMedico(novoMedico);
    }

    private static void CadastrarNovoPaciente()
    {
        Console.WriteLine("\n--- Cadastrar Novo Paciente ---");
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Data de Nascimento (dd/MM/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
        {
            Console.WriteLine("Erro: Formato de data inválido.");
            return;
        }
        Console.Write("CPF: ");
        string cpf = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(cpf))
        {
            Console.WriteLine("Erro: Nome e CPF são obrigatórios.");
            return;
        }

        Paciente novoPaciente = new Paciente(nome, dataNascimento, cpf);
        gerenciador.CadastrarPaciente(novoPaciente);
    }

    private static void AgendarNovaConsulta()
    {
        Console.WriteLine("\n--- Agendar Nova Consulta ---");

        var medicosDisponiveis = gerenciador.ListarMedicos();
        if (!medicosDisponiveis.Any())
        {
            Console.WriteLine("Nenhum médico cadastrado. Cadastre um médico primeiro.");
            return;
        }
        Console.WriteLine("\nMédicos Disponíveis:");
        for (int i = 0; i < medicosDisponiveis.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {medicosDisponiveis[i].Nome} ({medicosDisponiveis[i].Especialidade}) - CRM: {medicosDisponiveis[i].CRM}");
        }
        Console.Write("Digite o CRM do médico para a consulta: ");
        string crmMedico = Console.ReadLine();
        Medico medicoSelecionado = gerenciador.ObterMedicoPorCRM(crmMedico);
        if (medicoSelecionado == null)
        {
            Console.WriteLine("Médico não encontrado.");
            return;
        }

        var pacientesDisponiveis = gerenciador.ListarPacientes();
        if (!pacientesDisponiveis.Any())
        {
            Console.WriteLine("Nenhum paciente cadastrado. Cadastre um paciente primeiro.");
            return;
        }
        Console.WriteLine("\nPacientes Disponíveis:");
        for (int i = 0; i < pacientesDisponiveis.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pacientesDisponiveis[i].Nome} - CPF: {pacientesDisponiveis[i].CPF}");
        }
        Console.Write("Digite o CPF do paciente para a consulta: ");
        string cpfPaciente = Console.ReadLine();
        Paciente pacienteSelecionado = gerenciador.ObterPacientePorCPF(cpfPaciente);
        if (pacienteSelecionado == null)
        {
            Console.WriteLine("Paciente não encontrado.");
            return;
        }

        Console.Write("Data e Hora da Consulta (dd/MM/yyyy HH:mm): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataHora))
        {
            Console.WriteLine("Erro: Formato de data e hora inválido.");
            return;
        }

        gerenciador.AgendarConsulta(medicoSelecionado, pacienteSelecionado, dataHora);
    }

    private static void BuscarConsultasUI()
    {
        Console.WriteLine("\n--- Buscar Consultas ---");
        Console.Write("Buscar por (medico, paciente, data): ");
        string tipoBusca = Console.ReadLine();
        Console.Write($"Digite o termo de busca ({tipoBusca}): ");
        string termoBusca = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(tipoBusca) || string.IsNullOrWhiteSpace(termoBusca))
        {
            Console.WriteLine("Erro: Tipo de busca e termo de busca são obrigatórios.");
            return;
        }

        gerenciador.BuscarConsultas(termoBusca, tipoBusca);
    }

    private static void PopularDadosIniciais()
    {
        gerenciador.CadastrarMedico(new Medico("Dr. João Silva", "Cardiologista", "CRM-SP12345"));
        gerenciador.CadastrarMedico(new Medico("Dra. Maria Souza", "Pediatra", "CRM-RJ67890"));
        gerenciador.CadastrarPaciente(new Paciente("Carlos Alberto", new DateTime(1980, 5, 10), "111.222.333-44"));
        gerenciador.CadastrarPaciente(new Paciente("Ana Paula", new DateTime(1995, 11, 20), "555.666.777-88"));

        Medico joao = gerenciador.ObterMedicoPorCRM("CRM-SP12345");
        Paciente carlos = gerenciador.ObterPacientePorCPF("111.222.333-44");
        Medico maria = gerenciador.ObterMedicoPorCRM("CRM-RJ67890");
        Paciente ana = gerenciador.ObterPacientePorCPF("555.666.777-88");

        if (joao != null && carlos != null)
            gerenciador.AgendarConsulta(joao, carlos, DateTime.Now.AddDays(1).AddHours(10));
        if (maria != null && ana != null)
            gerenciador.AgendarConsulta(maria, ana, DateTime.Now.AddDays(2).AddHours(14));
    }
}



// ----------------------------------- Classes ------------------------------------------

public class Medico
{
    public string Nome { get; set; }
    public string Especialidade { get; set; }
    public string CRM { get; set; }

    public Medico(string nome, string especialidade, string crm)
    {
        Nome = nome;
        Especialidade = especialidade;
        CRM = crm;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Especialidade: {Especialidade}, CRM: {CRM}";
    }
}

public class Paciente
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Paciente(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Data Nasc.: {DataNascimento:dd/MM/yyyy}, CPF: {CPF}";
    }
}

public class Consulta
{
    public Medico Medico { get; set; }
    public Paciente Paciente { get; set; }
    public DateTime DataHora { get; set; }

    public Consulta(Medico medico, Paciente paciente, DateTime dataHora)
    {
        Medico = medico;
        Paciente = paciente;
        DataHora = dataHora;
    }

    public override string ToString()
    {
        return $"Data/Hora: {DataHora:dd/MM/yyyy HH:mm}\n Médico: {Medico.Nome} ({Medico.Especialidade})\n Paciente: {Paciente.Nome} (CPF: {Paciente.CPF})\n";
    }
}

public class GerenciadorClinica
{
    private List<Medico> medicos;
    private List<Paciente> pacientes;
    private List<Consulta> consultas;

    public GerenciadorClinica()
    {
        medicos = new List<Medico>();
        pacientes = new List<Paciente>();
        consultas = new List<Consulta>();
    }

    public void CadastrarMedico(Medico medico)
    {
        if (medico == null)
        {
            Console.WriteLine("Erro: Dados do médico inválidos.");
            return;
        }
        if (medicos.Any(m => m.CRM == medico.CRM))
        {
            Console.WriteLine($"Erro: Já existe um médico com o CRM {medico.CRM}.");
            return;
        }
        medicos.Add(medico);
        Console.WriteLine($"Médico {medico.Nome} cadastrado com sucesso!");
    }

    public void CadastrarPaciente(Paciente paciente)
    {
        if (paciente == null)
        {
            Console.WriteLine("Erro: Dados do paciente inválidos.");
            return;
        }
        if (pacientes.Any(p => p.CPF == paciente.CPF))
        {
            Console.WriteLine($"Erro: Já existe um paciente com o CPF {paciente.CPF}.");
            return;
        }
        pacientes.Add(paciente);
        Console.WriteLine($"Paciente {paciente.Nome} cadastrado com sucesso!");
    }

    public void AgendarConsulta(Medico medico, Paciente paciente, DateTime dataHora)
    {
        if (medico == null || paciente == null)
        {
            Console.WriteLine("Erro: Médico ou paciente inválido para agendamento.");
            return;
        }
        if (!medicos.Contains(medico))
        {
            Console.WriteLine("Erro: Médico não encontrado no sistema.");
            return;
        }
        if (!pacientes.Contains(paciente))
        {
            Console.WriteLine("Erro: Paciente não encontrado no sistema.");
            return;
        }
        if (consultas.Any(c => c.Medico == medico && c.DataHora == dataHora))
        {
            Console.WriteLine($"Erro: Médico {medico.Nome} já possui uma consulta agendada para {dataHora:dd/MM/yyyy HH:mm}.");
            return;
        }
        if (consultas.Any(c => c.Paciente == paciente && c.DataHora == dataHora))
        {
            Console.WriteLine($"Erro: Paciente {paciente.Nome} já possui uma consulta agendada para {dataHora:dd/MM/yyyy HH:mm}.");
            return;
        }

        Consulta novaConsulta = new Consulta(medico, paciente, dataHora);
        consultas.Add(novaConsulta);
        Console.WriteLine($"Consulta agendada com sucesso para {dataHora:dd/MM/yyyy HH:mm} com Dr(a). {medico.Nome} e Paciente {paciente.Nome}.");
    }

    public void ListarConsultas()
    {
        if (!consultas.Any())
        {
            Console.WriteLine("Nenhuma consulta agendada.");
            return;
        }
        Console.WriteLine("\n--- Consultas Agendadas ---");
        foreach (var consulta in consultas.OrderBy(c => c.DataHora))
        {
            Console.WriteLine(consulta);
        }
        Console.WriteLine("---------------------------\n");
    }

    public void BuscarConsultas(string termoBusca, string tipoBusca)
    {
        List<Consulta> resultados = new List<Consulta>();

        switch (tipoBusca.ToLower())
        {
            case "medico":
                resultados = consultas.Where(c => c.Medico.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "paciente":
                resultados = consultas.Where(c => c.Paciente.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase) || c.Paciente.CPF == termoBusca).ToList();
                break;
            case "data":
                if (DateTime.TryParse(termoBusca, out DateTime dataBusca))
                {
                    resultados = consultas.Where(c => c.DataHora.Date == dataBusca.Date).ToList();
                }
                else
                {
                    Console.WriteLine("Formato de data inválido. Tente dd/MM/yyyy.");
                    return;
                }
                break;
            default:
                Console.WriteLine("Tipo de busca inválido. Use 'medico', 'paciente' ou 'data'.");
                return;
        }

        if (!resultados.Any())
        {
            Console.WriteLine($"Nenhuma consulta encontrada para '{termoBusca}' ({tipoBusca}).");
            return;
        }

        Console.WriteLine($"\n--- Resultados da Busca para '{termoBusca}' ({tipoBusca}) ---");
        foreach (var consulta in resultados.OrderBy(c => c.DataHora))
        {
            Console.WriteLine(consulta);
        }
        Console.WriteLine("---------------------------------------------------\n");
    }

    public Medico ObterMedicoPorCRM(string crm)
    {
        return medicos.FirstOrDefault(m => m.CRM == crm);
    }

    public Paciente ObterPacientePorCPF(string cpf)
    {
        return pacientes.FirstOrDefault(p => p.CPF == cpf);
    }

    public List<Medico> ListarMedicos()
    {
        return medicos;
    }

    public List<Paciente> ListarPacientes()
    {
        return pacientes;
    }
}