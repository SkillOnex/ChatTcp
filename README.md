# Chat em C#

Este é um simples aplicativo de chat em C# usando sockets TCP.

## Funcionalidades

- Servidor que aceita conexões de vários clientes.
- Comunicação entre clientes por meio do servidor.


## Pré-requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) instalado.

## Como usar

### Servidor

1. Abra o terminal e navegue até o diretório do servidor (`Servidor`).
2. Execute `dotnet run` para iniciar o servidor.

### Cliente

1. Abra outro terminal e navegue até o diretório do cliente (`Client`).
2. Execute `dotnet run` para iniciar o cliente.
3. O Ip está para padrão LocalHost e a porta 8080 Caso queira mudar navegue até (`Promgram.cs`) dentro da pasta (`Cliente`).
4. Comece a enviar e receber mensagens no chat.

## Configurações

Você pode ajustar as configurações no código.

## Contribuindo

Sinta-se à vontade para abrir problemas ou enviar pull requests para melhorar o chat.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
