class Program
{
    // Main do projeto 
    static void Main(string[] args)
    {
        // Class que manda dois parametros para o client ip e porta
        Client client = new Client("127.0.0.1", 8080);
        client.Start();
    }
}