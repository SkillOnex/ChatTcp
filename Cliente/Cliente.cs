using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

class Client
{
    // Variáveis de instância para armazenar informações sobre a conexão e os objetos de comunicação
    private string _ipAddress;
    private int _port;
    private TcpClient _client;
    private StreamWriter _sWriter;
    private StreamReader _sReader;
    private CancellationTokenSource _cancellationTokenSource;

    // Construtor da classe Client, usado para inicializar valores
    public Client(string ipAddress, int port)
    {
        // Valores padrão para o endereço IP e a porta (substitua por valores reais, se necessário)
        _ipAddress = ipAddress;
        _port = port;

        // Inicializa um objeto CancellationTokenSource para gerenciar a cancelabilidade da thread
        _cancellationTokenSource = new CancellationTokenSource();
    }

    // Método principal para iniciar a comunicação do cliente com o servidor
    public void Start()
    {
        try
        {
            // Conecta-se ao servidor usando o endereço IP e a porta especificados
            _client = new TcpClient(_ipAddress, _port);

            // Inicializa os objetos de escrita e leitura para comunicação com o servidor
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.UTF8);
            _sReader = new StreamReader(_client.GetStream(), Encoding.UTF8);

            // Inicie uma nova thread para ler continuamente as mensagens do servidor
            Thread readThread = new Thread(() =>
            {
                try
                {
                    while (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        // Lê uma linha da resposta do servidor
                        String serverResponse = _sReader.ReadLine();

                        // Limpa a linha atual antes de exibir a mensagem do servidor
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth));

                        // Exibe a mensagem recebida do servidor no cliente
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine("Um usuário disse:" + serverResponse);
                        
                        // Exibe a instrução para o usuário digitar algo
                        Console.Write("Digite algo (ou 'exit' para sair): ");
                    }
                }
                catch (IOException)
                {
                    // Se ocorrer uma IOException, a thread será encerrada
                }
            });
            readThread.Start();

            // Loop principal para enviar mensagens ao servidor
            while (true)
            {
                // Solicita ao usuário que digite algo
                Console.Write("Digite algo (ou 'exit' para sair): ");
                string sData = Console.ReadLine();

                // Verifica se o usuário deseja sair
                if (sData.ToLower() == "exit")
                {
                    // Envia um comando de saída para o servidor
                    _sWriter.WriteLine("exit");
                    _sWriter.Flush();
                    break;
                }

                // Envia a mensagem digitada pelo usuário para o servidor
                _sWriter.WriteLine(sData);
                _sWriter.Flush();
            }
        }
        catch (Exception ex)
        {
            // Exibe mensagens de erro no console, se ocorrerem
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Encerra o token de cancelamento e aguarda a conclusão da thread de leitura
            _cancellationTokenSource.Cancel();
            _client.Close();
        }
    }
}