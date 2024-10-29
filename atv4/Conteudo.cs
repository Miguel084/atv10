class Conteudo
{
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int ClassificacaoIndicativa { get; set; }
    public int AnoLancamento { get; set; }

    public override string ToString()
    {
        return $"Título: {Titulo} | Gênero: {Genero} | Classificação: {ClassificacaoIndicativa}+ | Ano: {AnoLancamento}";
    }
}