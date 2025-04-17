using System;
using System.Collections.Generic;
using System.Linq;

class Jogos
{
    //lista com os objetos da classe Jogos
    public static List<Jogos> catalogoJogos = new List<Jogos>();

    private string _nome;
    public double Preco { get; set; }
    private string _genero;

    //propriedade com validção de nome
    public string Nome
    {
        get { return _nome; }

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _nome = "Não especificado";
            }
            else
            {
                _nome = value;
            }
        }
    }

    //propriedade com validação de genero
    public string Genero
    {
        get { return _genero; }

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _genero = "Não especificado";
            }
            else
            {
                _genero = value;
            }
        }
    }

    //método para adicionar jogos
    public static void AdicionarJogos()
    {
        Console.WriteLine("Digite o nome do jogo:");
        string nomeJogo = Console.ReadLine();

        if (catalogoJogos.Count > 0)
        {
            //foreach pra percorrer cada item da lista, validando com condicional
            foreach (var jogos in catalogoJogos)
            {
                if (nomeJogo == jogos.Nome)
                {
                    Console.WriteLine($"Esse jogo já está presente no catálogo: {jogos.Nome}. Retornando ao menu.");
                    return;
                }
            }
        }
        Console.WriteLine("Digite o preço do jogo:");

        if (!double.TryParse(Console.ReadLine(), out double precoValido))
        {
            Console.WriteLine("Você digitou um valor inválido. Retornando ao menu.");
            return;
        }
        else
        {
            Console.WriteLine("Digite o gênero do jogo:");
            string genero = Console.ReadLine();

            catalogoJogos.Add(new Jogos { Nome = nomeJogo, Preco = precoValido, Genero = genero });
            Console.WriteLine($"Jogo: {nomeJogo}. Adicionado ao catálogo com sucesso!");
        }
    }

    //método para remover um jogo
    public static void RemoverJogos()
    {
        Console.WriteLine("Digite o nome do jogo que deseja remover:");
        string nomeJogo = Console.ReadLine();

        if (string.IsNullOrEmpty(nomeJogo))
        {
            Console.WriteLine("Nome inválido. Retornando ao menu.");
            return;
        }
        else
        {
            var remover = from jogo in catalogoJogos
                          where jogo.Nome == nomeJogo
                          select jogo;

            if (remover.Count() > 0)
            {
                List<Jogos> listaRemocao = remover.ToList();

                catalogoJogos.Remove(listaRemocao[0]);
                listaRemocao.RemoveAt(0);
                Console.WriteLine($"Jogo: {nomeJogo}. Removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Jogo não encontrado. Retornando ao menu.");
                return;
            }
        }
    }

    public static void ListarJogos()
    {
        if (catalogoJogos.Count() > 0)
        {
            Console.WriteLine("Jogos no catálogo:");
            foreach (var jogos in catalogoJogos)
            {
                Console.WriteLine($"Nome: {jogos.Nome}. Gênero: {jogos.Genero}. Preço: {jogos.Preco:F2}R$");
            }
        }
        else
        {
            Console.WriteLine("Catálogo vazio. Retornando ao menu.");
            return;
        }
    }
}

class Clientes
{
    static List<Clientes> listaClientes = new List<Clientes>();

    private string _nome;
    public double Saldo { get; set; }

    public string Nome
    {
        get { return _nome; }

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _nome = "Anônimo";
            }
            else
            {
                _nome = value;
            }
        }
    }

    //metodo simples (sem validação) pra cadastrar clientes
    public static void CadastrarCliente()
    {
        Console.WriteLine("Digite seu nome:");
        string nomeCLiente = Console.ReadLine();

        var busca = from cliente in listaClientes
                    where cliente.Nome == nomeCLiente
                    select cliente;

        foreach (var cliente in busca)
        {
            if (cliente.Nome == nomeCLiente)
            {
                Console.WriteLine("Já existe um usuário com este nome. Retornando ao menu.");
                return;
            }
        }
        //aqui o saldo é setado como 0 porque o objetivo é o usuário chamar a função de adicionar saldo no menu
        listaClientes.Add(new Clientes { Nome = nomeCLiente, Saldo = 0 });
        Console.WriteLine("Cliente cadastrado com sucesso!");
    }

    //meotodo pra adicionar saldo
    public static void AdicionarSaldo()
    {
        Console.WriteLine("Digite seu nome de usuário:");
        string nomeCliente = Console.ReadLine();

        var busca = from cliente in listaClientes
                    where cliente.Nome == nomeCliente
                    select cliente;

        if (busca.Count() == 0)
        {
            Console.WriteLine("Não existe um usuário com esse nome. Retornando ao menu.");
            return;
        }
        else
        {
            List<Clientes> cliente = busca.ToList();
            Console.WriteLine("Digite o saldo que deseja adicionar:");

            if (!double.TryParse(Console.ReadLine(), out double saldoValido))
            {
                Console.WriteLine("Você digitou caracteres inválidos. Retornando ao menu.");
                return;
            }
            else
            {
                if (saldoValido > 1000)
                {
                    Console.WriteLine("Valor digitado acima do permitido: (1000). Retornando ao menu.");
                    return;
                }
                else
                {
                    cliente[0].Saldo += saldoValido;
                    Console.WriteLine($"Saldo adicionado com sucesso. Seu saldo: {cliente[0].Saldo:F2}R$");
                }
            }
        }
    }

    //metodo pra emprestar saldo
    public static void EmprestarSaldo()
    {
        Console.WriteLine("Digite seu nome de usuário:");
        string nomeCliente = Console.ReadLine();

        var busca = from cliente in listaClientes
                    where cliente.Nome == nomeCliente
                    select cliente;

        if (busca.Count() == 0)
        {
            Console.WriteLine("Não existe um usuário com esse nome. Retornando ao menu.");
            return;
        }
        List<Clientes> cliente1 = busca.ToList();

        Console.WriteLine("Digite o nome da pessoa que você irá emprestar saldo:");
        string nomeEmprestimo = Console.ReadLine();

        var busca2 = from clientee in listaClientes
                     where clientee.Nome == nomeEmprestimo
                     select clientee;

        if (busca2.Count() == 0)
        {
            Console.WriteLine("Não existe um usuário com esse nome. Retornando ao menu.");
            return;
        }
        List<Clientes> cliente2 = busca2.ToList();

        Console.WriteLine($"Seu saldo: {cliente1[0].Saldo:F2}R$. Quanto você deseja emprestar?");

        if (!double.TryParse(Console.ReadLine(), out double valorValido) || cliente1[0].Saldo < valorValido)
        {
            Console.WriteLine("Você digitou um valor inválido. Retornando ao menu.");
        }
        else
        {
            cliente1[0].Saldo -= valorValido;
            cliente2[0].Saldo += valorValido;
            Console.WriteLine($"Você emprestou: {valorValido:F2}R$. Para: {cliente2[0].Nome}");
        }
    }

    //metodo pra comprar jogo
    public static void ComprarJogo()
    {
        Console.WriteLine("Digite seu nome de usuário:");
        string nomeCliente = Console.ReadLine();

        var buscaNome = from cliente in listaClientes
                        where cliente.Nome == nomeCliente
                        select cliente;

        if (buscaNome.Count() == 0)
        {
            Console.WriteLine("Não existe um usuário com esse nome. Retornando ao menu.");
            return;
        }
        Console.WriteLine("Digite o nome do jogo que você deseja comprar:");
        string nomeJogo = Console.ReadLine();

        var buscaJogo = from jogo in Jogos.catalogoJogos
                        where jogo.Nome == nomeJogo
                        select jogo;

        List<Jogos> carrinhoJogos = buscaJogo.ToList();

        foreach (var cliente in buscaNome)
        {
            if (cliente.Saldo >= carrinhoJogos[0].Preco)
            {
                cliente.Saldo -= carrinhoJogos[0].Preco;
                Console.WriteLine($"Parábens, você acaba de comprar: {carrinhoJogos[0].Nome}. Seu saldo: {cliente.Saldo:F2}R$.");
            }
            else
            {
                Console.WriteLine($"(Saldo: {cliente.Saldo:F2}R$). Você não tem saldo suficiente para realizar a compra. Retornando ao menu.");
                return;
            }
        }
    }

}

class Sistema
{
    static void Main()
    {
        //loop do menu
        bool ativo = true;

        while (ativo)
        {
            //menu interativo
            Console.WriteLine("1 - Adicionar jogos");
            Console.WriteLine("2 - Remover jogos");
            Console.WriteLine("3 - Listar jogos");
            Console.WriteLine("4 - Cadastrar usuário");
            Console.WriteLine("5 - Adicionar saldo");
            Console.WriteLine("6 - Emprestar saldo");
            Console.WriteLine("7 - Comprar jogo");
            Console.WriteLine("8 - Sair");

            if (!int.TryParse(Console.ReadLine(), out int escolhaMenu) || escolhaMenu == 0 || escolhaMenu > 9)
            {
                Console.WriteLine("Você digitou valores inválidos. Tente novamente.");
            }
            else
            {
                switch (escolhaMenu)
                {
                    case 1:
                        Jogos.AdicionarJogos();
                        break;
                    case 2:
                        Jogos.RemoverJogos();
                        break;
                    case 3:
                        Jogos.ListarJogos();
                        break;
                    case 4:
                        Clientes.CadastrarCliente();
                        break;
                    case 5:
                        Clientes.AdicionarSaldo();
                        break;
                    case 6:
                        Clientes.EmprestarSaldo();
                        break;
                    case 7:
                        Clientes.ComprarJogo();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Sistema encerrado. Volte sempre.");
                        return;
                }
            }
        }
    }
}