using Projeto_Rodrigo.Models;
using Projeto_Rodrigo.Services;
using Projeto_Rodrigo.Services.Projeto_Rodrigo.Services;

using Projeto_Rodrigo.Models;
using Projeto_Rodrigo.Services;

var salaService = new SalaService();
var reservaService = new ReservaService();

while (true)
{
    Console.Clear();

    Console.WriteLine("===== SISTEMA DE REUNIÕES =====");
    Console.WriteLine("1 - Cadastrar Sala");
    Console.WriteLine("2 - Listar Salas");
    Console.WriteLine("3 - Buscar Sala");
    Console.WriteLine("4 - Excluir Sala");
    Console.WriteLine("5 - Atualizar Sala");
    Console.WriteLine("6 - Criar Reserva");
    Console.WriteLine("7 - Listar Reservas");
    Console.WriteLine("8 - Buscar Reserva");
    Console.WriteLine("9 - Cancelar Reserva");
    Console.WriteLine("10 - Sair");
    Console.Write("\nOpção: ");

    var opcao = Console.ReadLine();

    Console.Clear();

    switch (opcao)
    {
        case "1":
            {
                var sala = new Sala();

                Console.Write("Nome: ");
                sala.Nome = Console.ReadLine();

                Console.Write("Andar: ");
                sala.Andar = int.Parse(Console.ReadLine());

                Console.Write("Quantidade de assentos: ");
                sala.QuantidadeAssentos = int.Parse(Console.ReadLine());

                if (salaService.Criar(sala, out var erros))
                {
                    Console.WriteLine("Sala cadastrada com sucesso!");
                }
                else
                {
                    foreach (var erro in erros)
                    {
                        Console.WriteLine(erro.ErrorMessage);
                    }
                }

                break;
            }

        case "2":
            {
                Console.Write("Página: ");
                int pagina = int.Parse(Console.ReadLine());

                var salas = salaService.Listar(pagina);

                foreach (var sala in salas)
                {
                    Console.WriteLine(
                        $"{sala.Id} - {sala.Nome} | Andar: {sala.Andar} | Assentos: {sala.QuantidadeAssentos}"
                    );
                }

                break;
            }

        case "3":
            {
                Console.Write("ID da sala: ");
                int id = int.Parse(Console.ReadLine());

                var sala = salaService.Buscar(id);

                if (sala == null)
                {
                    Console.WriteLine("Sala não encontrada.");
                }
                else
                {
                    Console.WriteLine($"Id: {sala.Id}");
                    Console.WriteLine($"Nome: {sala.Nome}");
                    Console.WriteLine($"Andar: {sala.Andar}");
                    Console.WriteLine($"Assentos: {sala.QuantidadeAssentos}");
                }

                break;
            }

        case "4":
            {
                Console.Write("ID da sala: ");
                int id = int.Parse(Console.ReadLine());

                salaService.Excluir(id);

                Console.WriteLine("Sala excluída.");
                break;
            }

        case "5":
            {
                Console.Write("ID da sala: ");
                int id = int.Parse(Console.ReadLine());

                var sala = salaService.Buscar(id);

                if (sala == null)
                {
                    Console.WriteLine("Sala não encontrada.");
                    break;
                }

                Console.Write("Novo nome: ");
                sala.Nome = Console.ReadLine();

                Console.Write("Novo andar: ");
                sala.Andar = int.Parse(Console.ReadLine());

                Console.Write("Nova quantidade de assentos: ");
                sala.QuantidadeAssentos = int.Parse(Console.ReadLine());

                if (salaService.Atualizar(sala, out var erros))
                {
                    Console.WriteLine("Sala atualizada com sucesso!");
                }
                else
                {
                    foreach (var erro in erros)
                    {
                        Console.WriteLine(erro.ErrorMessage);
                    }
                }

                break;
            }

        case "6":
            {
                var reserva = new Reserva();

                Console.Write("ID da sala: ");
                reserva.SalaId = int.Parse(Console.ReadLine());

                Console.Write("Início (dd/MM/yyyy HH:mm): ");
                reserva.Inicio = DateTime.Parse(Console.ReadLine());

                Console.Write("Fim (dd/MM/yyyy HH:mm): ");
                reserva.Fim = DateTime.Parse(Console.ReadLine());

                if (reservaService.Criar(reserva, out var erros))
                {
                    Console.WriteLine("Reserva criada com sucesso!");
                }
                else
                {
                    foreach (var erro in erros)
                    {
                        Console.WriteLine(erro.ErrorMessage);
                    }
                }

                break;
            }

        case "7":
            {
                var reservas = reservaService.Listar(2);

                foreach (var reserva in reservas)
                {
                    Console.WriteLine(
                        $"{reserva.Id} - Sala {reserva.SalaId} | {reserva.Inicio:dd/MM/yyyy HH:mm} até {reserva.Fim:dd/MM/yyyy HH:mm}"
                    );
                }

                break;
            }

        case "8":
            {
                Console.Write("ID da reserva: ");
                int id = int.Parse(Console.ReadLine());

                var reserva = reservaService.Buscar(id);

                if (reserva == null)
                {
                    Console.WriteLine("Reserva não encontrada.");
                }
                else
                {
                    Console.WriteLine($"Id: {reserva.Id}");
                    Console.WriteLine($"Sala: {reserva.SalaId}");
                    Console.WriteLine($"Início: {reserva.Inicio}");
                    Console.WriteLine($"Fim: {reserva.Fim}");
                }

                break;
            }

        case "9":
            {
                Console.Write("ID da reserva: ");
                int id = int.Parse(Console.ReadLine());

                reservaService.Excluir(id);

                Console.WriteLine("Reserva cancelada.");
                break;
            }

        case "10":
            {
                return;
            }

        default:
            {
                Console.WriteLine("Opção inválida.");
                break;
            }
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}