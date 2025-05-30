# 📌 Trackit
`Backend - Aplicação para controle de Tickets, Clientes e Técnicos`

### 📁  Volumes

`./Docker/*`
  - Volume utilizado pelo docker para armazenar dados do postgres

`./Uploads/*`
  - Volume utilizado pela aplicação para armazenar arquivos ( anexos e avatars )

`./logs/*`
  - Volume utilizado pela aplicação para armazenar os logs

### ⚙️ Setup

`Environments`
- appsettings.json
    - Arquivo de configuração de variáveis de ambiente da aplicação
    - Exemplo de configuração em appsettings.example.json
- .env
    - Arquivo utilizado pelo **docker** para sobreescrever as variáveis de ambiente da aplicação
    - Exemplo de configuração em .env.example

### 🚀 Deploy

- ```docker-compose up -d```
  - Sobe os containers da aplicação no docker ( database e backend )
  - Ao iniciar a aplicação já é executado as migrations e o seed do usuário default
  - Portas 8080 ( backend ) e 5432 ( database )

### 📦 Tecnologias

- C# - Liguagem
- .NET ( ASP.NET Core ) 9.0 - Framework
- PostgreSQL - Banco de dados relacional
- JWT - Autenticação e autorização
- Entity Framework Core - ORM e Migrations
- Serilog - Logs da aplicação