using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    // TcpListener para aceitar conexões de clientes
    private TcpListener _server;

    // Flag para indicar se o servidor está em execução
    private bool _isRunning;

    // Lista para armazenar clientes conectados
    private List<TcpClient> _clients = new List<TcpClient>();

    // Dicionário para armazenar escritores (stream de saída) associados a cada cliente
    private Dictionary<TcpClient, StreamWriter> _clientWriters = new Dictionary<TcpClient, StreamWriter>();

    // Construtor para inicializar o servidor com a porta especificada
    public Server(int port)
    {
        // Cria um TcpListener que escuta em qualquer endereço IP disponível na porta especificada
        _server = new TcpListener(IPAddress.Any, port);

        // Inicia o TcpListener
        _server.Start();

        // Inicializa a flag indicando que o servidor está em execução
        _isRunning = true;
    }

    // Método para iniciar o servidor e começar a aceitar clientes
    public void Start()
    {
        // Inicia o loop para aceitar clientes
        LoopClients();
    }

    // Método para parar o servidor
    public void Stop()
    {
        // Define a flag indicando que o servidor não está mais em execução
        _isRunning = false;

        // Para o TcpListener
        _server.Stop();
    }

    // Loop para aceitar clientes continuamente enquanto o servidor estiver em execução
    private void LoopClients()
    {
        while (_isRunning)
        {
            // Aguarda a conexão de um cliente e aceita o TcpClient associado
            TcpClient newClient = _server.AcceptTcpClient();

            // Adiciona o novo cliente à lista de clientes
            _clients.Add(newClient);

            // Inicializa um StreamWriter para o cliente e o armazena no dicionário
            _clientWriters[newClient] = new StreamWriter(newClient.GetStream(), Encoding.UTF8);

            // Inicia uma nova thread para lidar com o cliente
            Thread t = new Thread(() => HandleClient(newClient));
            t.Start();
        }
    }

    // Método para enviar uma mensagem para todos os clientes, excluindo opcionalmente um cliente específico
    public void BroadcastMessage(string message, TcpClient excludeClient = null)
    {
        foreach (var writer in _clientWriters)
        {
            // Verifica se o cliente atual não é o cliente excluído
            if (excludeClient == null || writer.Key != excludeClient)
            {
                // Envia a mensagem para o cliente atual
                writer.Value.WriteLine(message);
                writer.Value.Flush();
            }
        }
    }

    // Método para lidar com as comunicações de um cliente específico
    private void HandleClient(object obj)
    {
        // Converte o objeto para um TcpClient
        TcpClient client = (TcpClient)obj;

        // Obtém o StreamWriter associado ao cliente do dicionário
        StreamWriter sWriter = _clientWriters[client];

        // Inicializa um StreamReader para ler mensagens do cliente
        StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);

        try
        {
            // Loop para ler continuamente mensagens do cliente enquanto o servidor estiver em execução
            while (_isRunning)
            {
                // Lê uma linha da mensagem do cliente
                string message = sReader.ReadLine();

                // Verifica se a mensagem não é nula
                if (message != null)
                {
                    // Exibe a mensagem recebida no console do servidor
                    Console.WriteLine($"Received: {message}");

                    // Envia a mensagem recebida para todos os clientes, excluindo o cliente atual
                    BroadcastMessage(message, client);
                }
            }
        }
        catch (Exception ex)
        {
            // Exibe mensagens de erro no console do servidor
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Remove o cliente da lista de clientes e o StreamWriter do dicionário
            _clients.Remove(client);
            _clientWriters.Remove(client);

            // Fecha o cliente
            client.Close();

            // Informa a todos os clientes que um usuário saiu do chat
            BroadcastMessage($"Um usuário saiu do chat.", client);
        }
    }
}