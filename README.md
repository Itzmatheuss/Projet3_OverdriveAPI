# API de Gestão de Usuários

Esta é uma API RESTful desenvolvida em C# utilizando o ASP.NET Core no Visual Studio. A API oferece funcionalidades para gerenciamento de usuários, incluindo operações de CRUD (Criar, Ler, Atualizar, Deletar). O projeto utiliza um banco de dados SQL Server embutido (localdb).

## Requisitos

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [.NET 6.0 SDK ou superior](https://dotnet.microsoft.com/download)
- [SQL Server Express ou LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

## Configuração do Projeto

### Passos para Configuração e Execução

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
2. **Abra o projeto no Visual Studio:**

   1. No Visual Studio, clique em "Abrir um projeto ou solução".
   2. Navegue até a pasta do repositório clonado e selecione o arquivo `.sln` para abrir o projeto.

3. **Configurar o banco de dados:**

   1. Certifique-se de que o SQL Server Express ou LocalDB esteja instalado e em execução.
   2. No arquivo `appsettings.json`, atualize a string de conexão com o banco de dados, caso necessário:

      ```json
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SeuBancoDeDados;Trusted_Connection=True;MultipleActiveResultSets=true"
      }
      ```

   3. Abra o terminal do Visual Studio (ou o terminal na pasta do projeto) e execute os seguintes comandos para aplicar as migrações do banco de dados:

      ```bash
      dotnet ef migrations add InitialCreate
      dotnet ef database update
      ```
4. **Aplicar as migrações do Entity Framework:**

   1. Abra o terminal do Visual Studio (ou o terminal na pasta do projeto).
   2. Execute o seguinte comando para criar as migrações iniciais do banco de dados:

      ```bash
      dotnet ef migrations add InitialCreate
      ```

   3. Aplique as migrações ao banco de dados com o seguinte comando:

      ```bash
      dotnet ef database update
      ```

5. **Executar a API:**

   1. No Visual Studio, clique em "Executar" ou pressione `F5` para iniciar o servidor de desenvolvimento.
   2. A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`.
      
6. **Testar a API com Swagger:**

   1. Ao executar a aplicação no Visual Studio (pressionando `F5`), a documentação interativa da API, gerada automaticamente pelo Swagger, será aberta no navegador.
   2. A documentação estará disponível na URL: `https://localhost:5001/swagger` ou `http://localhost:5000/swagger`.
   3. No Swagger, você poderá visualizar todas as rotas disponíveis na API, além de realizar testes diretamente pela interface:
      - Selecione a rota desejada (GET, POST, PUT, DELETE).
      - Clique no botão **"Try it out"** para fazer uma requisição.
      - Insira os parâmetros necessários e clique em **"Execute"** para testar a API.

   4. O Swagger fornece detalhes sobre as respostas da API, como códigos de status e o formato de retorno dos dados, facilitando a validação e depuração dos endpoints.
