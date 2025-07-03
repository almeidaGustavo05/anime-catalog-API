# 📺 Catálogo de Animes API

Uma Web API desenvolvida com **.NET 9** para gerenciamento de um catálogo de animes. A aplicação adota os princípios da **Clean Architecture**, com foco em **escalabilidade**, **organização de código** e **manutenibilidade**.

---

## 🚀 Tecnologias Utilizadas

- **.NET 9**
- **ASP.NET Core** – construção da API RESTful
- **Entity Framework Core** – ORM para acesso a dados
- **SQL Server** – banco de dados relacional
- **MediatR** – implementação do padrão CQRS
- **Serilog** – logging estruturado
- **xUnit** – testes de unidade
- **Docker / Docker Compose** – containerização e orquestração

---

## 🧱 Estrutura da Solução

A estrutura segue o padrão da **Clean Architecture**, separando responsabilidades em camadas distintas:

- **`AnimeCatalog.Domain`**: Entidades, interfaces de repositório e regras de domínio
- **`AnimeCatalog.Application`**: Casos de uso (comandos e queries), DTOs e validações
- **`AnimeCatalog.Infrastructure`**: Implementações de repositórios, DbContext e serviços externos
- **`AnimeCatalog.API`**: Camada de apresentação, onde os endpoints REST são expostos
- **`AnimeCatalog.Tests`**: Projeto de testes unitários da aplicação

---

## ⚙️ Como Executar o Projeto

### ✅ Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- SQL Server (local ou via container)

### 💾 Configurando o Banco de Dados

1. Certifique-se de que uma instância do SQL Server está em execução
2. Atualize a string de conexão no arquivo `appsettings.json` do projeto `AnimeCatalog.API`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Sua-String-de-Conexão-Aqui"
     }
   }
   ```

3. Aplique as migrações do Entity Framework:

   ```bash
   dotnet ef database update --project AnimeCatalog.Infrastructure --startup-project AnimeCatalog.API
   ```

### ▶️ Executando via .NET CLI

Com o terminal na raiz do projeto, execute:

```bash
dotnet run --project AnimeCatalog.API/AnimeCatalog.API.csproj
```

A API estará disponível em:
- 📍 **http://localhost:5075** (ou conforme configurado)

### 🐳 Executando com Docker Compose

1. Navegue até a raiz do projeto
2. Execute:

   ```bash
   docker-compose up --build
   ```

3. Após a inicialização:
   - **API disponível em**: http://localhost
   - **Swagger UI**: http://localhost/swagger

### 🧪 Executando os Testes

Para rodar os testes de unidade:

```bash
dotnet test
```

---

## 📋 Endpoints da API

A documentação completa dos endpoints está disponível através do **Swagger UI** quando a aplicação estiver em execução.

---