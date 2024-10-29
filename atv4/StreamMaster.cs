class StreamMaster
{
    private List<Conteudo> catalogo;
    private List<string> historicoVisualizacoes;
    private Dictionary<string, int> contagemGeneros;

    public StreamMaster()
    {
        catalogo = new List<Conteudo>();
        historicoVisualizacoes = new List<string>();
        contagemGeneros = new Dictionary<string, int>();
    }

    public void AdicionarConteudo(string titulo, string genero, int classificacao, int ano)
    {
        var novoConteudo = new Conteudo
        {
            Titulo = titulo,
            Genero = genero,
            ClassificacaoIndicativa = classificacao,
            AnoLancamento = ano
        };
        catalogo.Add(novoConteudo);
        Console.WriteLine($"\nConteúdo '{titulo}' adicionado com sucesso!");
    }

    public void VisualizarCatalogo()
    {
        if (catalogo.Count == 0)
        {
            Console.WriteLine("\nO catálogo está vazio!");
            return;
        }

        Console.WriteLine("\nCatálogo completo:");
        foreach (var conteudo in catalogo)
        {
            Console.WriteLine(conteudo);
        }
    }

    public void FiltrarConteudo(string genero, int classificacao)
    {
        var resultados = catalogo.Where(c => 
            (string.IsNullOrEmpty(genero) || c.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase)) &&
            (classificacao == 0 || c.ClassificacaoIndicativa == classificacao)
        ).ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("\nNenhum conteúdo encontrado com os critérios especificados.");
            return;
        }

        Console.WriteLine("\nConteúdos encontrados:");
        foreach (var conteudo in resultados)
        {
            Console.WriteLine(conteudo);
        }
    }

    public void AssistirConteudo(string titulo)
    {
        var conteudo = catalogo.FirstOrDefault(c => 
            c.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        
        if (conteudo == null)
        {
            Console.WriteLine("\nConteúdo não encontrado no catálogo!");
            return;
        }

        historicoVisualizacoes.Add(titulo);
        
        if (!contagemGeneros.ContainsKey(conteudo.Genero))
            contagemGeneros[conteudo.Genero] = 0;
        contagemGeneros[conteudo.Genero]++;

        Console.WriteLine($"\nVocê está assistindo: {titulo}");
    }

    public void MostrarRecomendacoes()
    {
        if (contagemGeneros.Count == 0)
        {
            Console.WriteLine("\nAssista alguns títulos para receber recomendações personalizadas!");
            return;
        }

        var generoMaisAssistido = contagemGeneros.OrderByDescending(x => x.Value).First().Key;
        var recomendacoes = catalogo
            .Where(c => c.Genero == generoMaisAssistido && !historicoVisualizacoes.Contains(c.Titulo))
            .ToList();

        if (recomendacoes.Count == 0)
        {
            Console.WriteLine($"\nNão há novas recomendações do gênero {generoMaisAssistido} disponíveis.");
            return;
        }

        Console.WriteLine($"\nRecomendações baseadas no seu gênero mais assistido ({generoMaisAssistido}):");
        foreach (var recomendacao in recomendacoes)
        {
            Console.WriteLine(recomendacao);
        }
    }

    public void RemoverConteudo(string titulo)
    {
        var conteudo = catalogo.FirstOrDefault(c => 
            c.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        
        if (conteudo == null)
        {
            Console.WriteLine("\nConteúdo não encontrado no catálogo!");
            return;
        }

        catalogo.Remove(conteudo);
        Console.WriteLine($"\nConteúdo '{titulo}' removido com sucesso!");
    }

    public void ExecutarMenu()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n=== StreamMaster - Menu ===");
            Console.WriteLine("1. Adicionar Filme/Série ao Catálogo");
            Console.WriteLine("2. Visualizar Catálogo");
            Console.WriteLine("3. Filtrar Conteúdo");
            Console.WriteLine("4. Assistir a um Filme/Série");
            Console.WriteLine("5. Ver Recomendações");
            Console.WriteLine("6. Remover Filme/Série do Catálogo");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Gênero: ");
                    string genero = Console.ReadLine();
                    Console.Write("Classificação Indicativa: ");
                    int classificacao = int.Parse(Console.ReadLine());
                    Console.Write("Ano de Lançamento: ");
                    int ano = int.Parse(Console.ReadLine());
                    AdicionarConteudo(titulo, genero, classificacao, ano);
                    break;

                case 2:
                    VisualizarCatalogo();
                    break;

                case 3:
                    Console.Write("Gênero (Enter para pular): ");
                    string generoFiltro = Console.ReadLine();
                    Console.Write("Classificação Indicativa (0 para pular): ");
                    int classificacaoFiltro = int.Parse(Console.ReadLine());
                    FiltrarConteudo(generoFiltro, classificacaoFiltro);
                    break;

                case 4:
                    Console.Write("Digite o título que deseja assistir: ");
                    string tituloAssistir = Console.ReadLine();
                    AssistirConteudo(tituloAssistir);
                    break;

                case 5:
                    MostrarRecomendacoes();
                    break;

                case 6:
                    Console.Write("Digite o título que deseja remover: ");
                    string tituloRemover = Console.ReadLine();
                    RemoverConteudo(tituloRemover);
                    break;

                case 7:
                    Console.WriteLine("\nObrigado por usar o StreamMaster!");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (opcao != 7);
    }
}