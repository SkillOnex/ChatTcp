# Chat em C#

Este é um simples aplicativo de chat em C# usando sockets TCP.

## Funcionalidades

- Servidor que aceita conexões de vários clientes.
- Comunicação entre clientes por meio do servidor.


## Pré-requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) instalado.

## Como usar

### Servidor

1. Abra o terminal e navegue até o diretório do servidor (`ServerApp`).
2. Execute `dotnet run` para iniciar o servidor.

### Cliente

1. Abra outro terminal e navegue até o diretório do cliente (`ClientApp`).
2. Execute `dotnet run` para iniciar o cliente.
3. Insira o endereço IP e a porta do servidor quando solicitado.
4. Comece a enviar e receber mensagens no chat.

## Configurações

Você pode ajustar as configurações no código, incluindo a chave de criptografia AES e o vetor de inicialização no servidor.

## Contribuindo

Sinta-se à vontade para abrir problemas ou enviar pull requests para melhorar o chat.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
