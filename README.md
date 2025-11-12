# DavinTI

![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)  
![License](https://img.shields.io/badge/license-MIT-blue)

DavinTI é um sistema simples de gerenciamento de contatos com suporte a múltiplos telefones por contato. Ele foi desenvolvido com **.NET Core, C# e PostgreSQL**, utilizando arquitetura de camadas (Domain, Service, Repository, API) para facilitar a manutenção e evolução do projeto.

---

## 🔧 Tecnologias

- **Backend:** C# (.NET Core 7)
- **Banco de dados:** PostgreSQL
- **ORM:** Entity Framework Core
- **API:** ASP.NET Core Web API
- **Ferramenta de desenvolvimento:** Visual Studio / VS Code
- **Cliente SQL (opcional):** DBeaver / pgAdmin

---

## 🗂 Estrutura do Banco de Dados

### Tabelas

#### `contato`
| Coluna      | Tipo         | Descrição                     |
|------------|-------------|--------------------------------|
| id_contato | SERIAL      | Chave primária                 |
| nome       | VARCHAR(100)| Nome do contato (obrigatório) |
| idade      | INTEGER     | Idade do contato               |

#### `telefone`
| Coluna      | Tipo         | Descrição                                       |
|------------|-------------|------------------------------------------------|
| id         | SERIAL      | Identificador único do telefone               |
| id_contato | INTEGER     | FK para o contato                              |
| numero     | VARCHAR(16) | Número de telefone                              |

**Relacionamento:**  
- Um contato pode ter vários telefones.  
- Telefone possui **FOREIGN KEY** para `contato` com `ON DELETE CASCADE`.

---

## ⚙️ Setup

### 1️⃣ Criar o banco

DavinTi.sql

CREATE DATABASE "DavinTI";
2️⃣ Criar tabelas

Execute os scripts SQL para criar as tabelas contato e telefone e inserir dados de exemplo (conforme script completo do banco
).

3️⃣ Configurar conexão no backend

No appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DavinTI;Username=seu_usuario;Password=sua_senha"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

4️⃣ Rodar a API
dotnet run --project DavinTI.Api


A API estará disponível em:

http://localhost:5163

📦 Endpoints da API
Método	Endpoint	Descrição
GET	/api/contato	Lista todos os contatos
GET	/api/contato/{id}	Busca contato por ID
POST	/api/contato	Cria um novo contato
PUT	/api/contato/{id}	Atualiza um contato existente
DELETE	/api/contato/{id}	Remove um contato
GET	/api/contato/comTelefones	Lista contatos com telefones
📌 Observações

As operações de CRUD já incluem validações básicas de dados.

Telefones de um contato são atualizados de forma automática na atualização do contato.

Para backups do banco, recomenda-se exportar via DBeaver ou pgAdmin, evitando conflitos de versão do pg_dumpall.

📝 License

Este projeto está licenciado sob a licença MIT. Veja o arquivo LICENSE
 para mais detalhes.
