class Program
{
    // Main do projeto 
    static void Main(string[] args)
    {
        // Paramentro do servidor que se inicia na porta 8080
        Server server = new Server(8080);
        server.Start();
    }
}