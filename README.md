# 📘 README - API SpaceX Data

## 📋 Visão Geral do Projeto

**SpaceX Data API** é uma API RESTful desenvolvida em .NET 8 que fornece endpoints para gerenciar dados de lançamentos e foguetes da SpaceX, com integração ao Firebase Realtime Database.

### 🎯 Objetivo do Trabalho

Desenvolver uma API completa que demonstre conceitos fundamentais de desenvolvimento backend:
- Arquitetura RESTful
- Operações CRUD completas (Create, Read, Update, Delete)
- Integração com banco de dados NoSQL (Firebase)
- Tratamento de erros e logging
- Documentação automática com Swagger

---

## 🏗️ Arquitetura da Solução

```
ApiSpaceX/
├── Controllers/
│   └── SpaceXController.cs      # Controlador principal da API
├── Models/
│   ├── Launch.cs                 # Modelo de Lançamento
│   ├── Rocket.cs                 # Modelo de Foguete
│   └── Stats.cs                  # Modelo de Estatísticas
├── Services/
│   └── FirebaseService.cs        # Serviço de comunicação com Firebase
├── Program.cs                    # Configuração da aplicação
└── appsettings.json              # Configurações da aplicação
```

### 📊 Diagrama de Arquitetura

```
┌─────────────┐     HTTP/HTTPS     ┌─────────────────┐
│   Cliente   │ ◄────────────────► │   API SpaceX    │
│   (WPF/Web) │     REST API       │  (.NET 8)       │
└─────────────┘                    └────────┬────────┘
                                             │
                                    Firebase SDK
                                             │
                                    ┌────────▼────────┐
                                    │    Firebase     │
                                    │  Realtime DB    │
                                    └─────────────────┘
```

---

## 🔧 Tecnologias Utilizadas

| Tecnologia | Versão | Finalidade |
|------------|--------|-------------|
| .NET | 8.0 | Framework principal da API |
| ASP.NET Core | 8.0 | Criação dos endpoints REST |
| Firebase Realtime Database | - | Banco de dados NoSQL |
| Swagger/Swashbuckle | 6.5.0 | Documentação interativa da API |
| HttpClient | - | Comunicação com Firebase |

---

## 📦 Modelos de Dados

### 1. Launch (Lançamento)

```csharp
public class Launch
{
    public string Id { get; set; }        // Identificador único (ObjectId)
    public string Name { get; set; }      // Nome da missão
    public bool Success { get; set; }     // Sucesso da missão
    public string Details { get; set; }   // Detalhes do lançamento
}
```

### 2. Rocket (Foguete)

```csharp
public class Rocket
{
    public string Id { get; set; }          // Identificador único
    public string Name { get; set; }        // Nome do foguete
    public string Description { get; set; } // Descrição detalhada
    public bool Active { get; set; }        // Status de atividade
    public int SuccessRatePct { get; set; } // Taxa de sucesso (0-100)
}
```

### 3. Stats (Estatísticas)

```csharp
public class Stats
{
    public int TotalLaunches { get; set; }      // Total de lançamentos
    public int SuccessfulLaunches { get; set; } // Lançamentos bem-sucedidos
    public int FailedLaunches { get; set; }     // Lançamentos com falha
    public double SuccessRate { get; set; }     // Taxa de sucesso (%)
}
```

---

## 🌐 Endpoints da API

### Base URL
```
https://apispacex.runasp.net/api/SpaceX
```

### Endpoints de Lançamentos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/launches` | Lista todos os lançamentos |
| GET | `/launches/{id}` | Busca lançamento por ID |
| POST | `/launches` | Cria novo lançamento |
| PUT | `/launches/{id}` | Atualiza lançamento existente |
| DELETE | `/launches/{id}` | Remove lançamento |
| POST | `/launches/batch` | Cria múltiplos lançamentos |

### Endpoints de Foguetes

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/rockets` | Lista todos os foguetes |
| GET | `/rockets/{id}` | Busca foguete por ID |
| POST | `/rockets` | Cria novo foguete |
| PUT | `/rockets/{id}` | Atualiza foguete existente |
| DELETE | `/rockets/{id}` | Remove foguete |
| POST | `/rockets/batch` | Cria múltiplos foguetes |

### Endpoints de Estatísticas

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/stats` | Obtém estatísticas gerais |

---

## 📝 Exemplos de Requisições e Respostas

### 1. Criar um Lançamento (POST)

**Requisição:**
```json
POST /api/SpaceX/launches
Content-Type: application/json

{
  "id": "5eb87d42ffd86e000604b384",
  "name": "FalconSat",
  "success": false,
  "details": "Primeiro voo de teste do Falcon 1"
}
```

**Resposta (201 Created):**
```json
{
  "id": "5eb87d42ffd86e000604b384",
  "name": "FalconSat",
  "success": false,
  "details": "Primeiro voo de teste do Falcon 1"
}
```

### 2. Listar Todos os Lançamentos (GET)

**Resposta (200 OK):**
```json
[
  {
    "id": "5eb87d42ffd86e000604b384",
    "name": "FalconSat",
    "success": false,
    "details": "Primeiro voo de teste do Falcon 1"
  },
  {
    "id": "5eb87d43ffd86e000604b385",
    "name": "DemoSat",
    "success": true,
    "details": "Primeiro lançamento bem-sucedido"
  }
]
```

### 3. Criar Foguete (POST)

**Requisição:**
```json
POST /api/SpaceX/rockets
Content-Type: application/json

{
  "id": "5e9d0d95eda69973a809d1ec",
  "name": "Falcon 9",
  "description": "Foguete reutilizável de dois estágios",
  "active": true,
  "successRatePct": 98
}
```

### 4. Batch de Lançamentos

**Requisição:**
```json
POST /api/SpaceX/launches/batch
Content-Type: application/json

[
  {
    "id": "5eb87d42ffd86e000604b384",
    "name": "FalconSat",
    "success": false,
    "details": "Primeiro voo"
  },
  {
    "id": "5eb87d43ffd86e000604b385",
    "name": "DemoSat",
    "success": true,
    "details": "Primeiro sucesso"
  }
]
```

**Resposta (200 OK):**
```json
{
  "message": "Processados 2 de 2 lançamentos.",
  "saved": 2,
  "failed": 0,
  "total": 2,
  "errors": []
}
```

### 5. Estatísticas (GET)

**Resposta (200 OK):**
```json
{
  "totalLaunches": 10,
  "successfulLaunches": 8,
  "failedLaunches": 2,
  "successRate": 80.0
}
```

---

## 🚦 Códigos de Status HTTP

| Código | Significado | Uso |
|--------|-------------|-----|
| 200 | OK | Requisição bem-sucedida (GET, PUT, DELETE) |
| 201 | Created | Recurso criado com sucesso (POST) |
| 400 | Bad Request | Dados inválidos ou ausentes |
| 404 | Not Found | Recurso não encontrado |
| 409 | Conflict | ID já existe (duplicado) |
| 500 | Internal Error | Erro interno do servidor |

---

## 🔌 Como Executar o Projeto Localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou VS Code
- Conta no [Firebase](https://console.firebase.google.com/)

### Passos para execução

```bash
# 1. Clonar o repositório
git clone https://github.com/seu-usuario/ApiSpaceX.git

# 2. Entrar na pasta do projeto
cd ApiSpaceX

# 3. Restaurar pacotes
dotnet restore

# 4. Configurar Firebase
# Edite o arquivo Services/FirebaseService.cs com sua URL do Firebase

# 5. Executar a aplicação
dotnet run

# 6. Acessar o Swagger
# https://localhost:5001/swagger
```

### Configuração do Firebase

1. Acesse [Firebase Console](https://console.firebase.google.com/)
2. Crie um novo projeto
3. Ative o Realtime Database
4. Configure as regras como "Modo de teste"
5. Copie a URL do banco de dados
6. Atualize no `FirebaseService.cs`:

```csharp
private const string FIREBASE_BASE_URL = "https://seu-projeto.firebaseio.com/";
```

---

## 📊 Estrutura do Firebase

```
apispacex-ddbbb-default-rtdb (Firebase)
│
├── launches/
│   ├── 5eb87d42ffd86e000604b384/
│   │   ├── id: "5eb87d42ffd86e000604b384"
│   │   ├── name: "FalconSat"
│   │   ├── success: false
│   │   └── details: "..."
│   └── ...
│
└── rockets/
    ├── 5e9d0d95eda69973a809d1ec/
    │   ├── id: "5e9d0d95eda69973a809d1ec"
    │   ├── name: "Falcon 9"
    │   ├── description: "..."
    │   ├── active: true
    │   └── successRatePct: 98
    └── ...
```

---

## 🧪 Testes com cURL

### Testes de Lançamentos

```bash
# Listar lançamentos
curl -X GET "https://apispacex.runasp.net/api/SpaceX/launches"

# Criar lançamento
curl -X POST "https://apispacex.runasp.net/api/SpaceX/launches" \
  -H "Content-Type: application/json" \
  -d '{"id":"test123","name":"Teste","success":true,"details":"Teste"}'

# Atualizar lançamento
curl -X PUT "https://apispacex.runasp.net/api/SpaceX/launches/test123" \
  -H "Content-Type: application/json" \
  -d '{"id":"test123","name":"Teste Atualizado","success":true,"details":"Atualizado"}'

# Deletar lançamento
curl -X DELETE "https://apispacex.runasp.net/api/SpaceX/launches/test123"
```

### Testes de Foguetes

```bash
# Listar foguetes
curl -X GET "https://apispacex.runasp.net/api/SpaceX/rockets"

# Criar foguete
curl -X POST "https://apispacex.runasp.net/api/SpaceX/rockets" \
  -H "Content-Type: application/json" \
  -d '{"id":"falcon9","name":"Falcon 9","description":"Foguete reutilizável","active":true,"successRatePct":98}'
```

### Batch Tests

```bash
# Batch de lançamentos
curl -X POST "https://apispacex.runasp.net/api/SpaceX/launches/batch" \
  -H "Content-Type: application/json" \
  -d '[
    {"id":"launch1","name":"Missão 1","success":true,"details":"Sucesso"},
    {"id":"launch2","name":"Missão 2","success":false,"details":"Falha"}
  ]'
```

---

## 🐛 Tratamento de Erros

A API implementa tratamento robusto de erros com logging:

```csharp
try
{
    // Operação
}
catch (Exception ex)
{
    _logger.LogError(ex, "Erro na operação");
    return StatusCode(500, new { error = "Erro interno do servidor" });
}
```

### Exemplos de Respostas de Erro

**400 - Bad Request:**
```json
{
  "error": "O ID do lançamento é obrigatório."
}
```

**404 - Not Found:**
```json
{
  "error": "Lançamento com ID 123 não foi encontrado."
}
```

**409 - Conflict:**
```json
{
  "error": "Lançamento com ID 5eb87d42ffd86e000604b384 já existe."
}
```

---

## 📈 Logging

A API utiliza `ILogger` para registrar:

- **Information**: Operações bem-sucedidas
- **Warning**: Tentativas inválidas, recursos não encontrados
- **Error**: Exceções e falhas

### Exemplo de Logs

```
info: SpaceXController[0] Buscando todos os lançamentos
info: SpaceXController[0] Buscando lançamento com ID: 123
warn: SpaceXController[0] Lançamento com ID 123 não encontrado
error: SpaceXController[0] Erro ao buscar lançamentos
```

---

## 🚀 Deploy

### Publicar para Produção

```bash
# Publicar aplicação
dotnet publish -c Release -o ./publish

# Os arquivos estarão na pasta ./publish
```

### Hospedagem (RunASP.NET)

1. Acesse [RunASP.NET](https://runasp.net)
2. Faça upload dos arquivos da pasta `publish`
3. Configure a aplicação para .NET 8
4. Acesse sua API em `https://seudominio.runasp.net`

---

## 📚 Documentação da API

A documentação interativa está disponível via Swagger:

- **Local**: `https://localhost:5001/swagger`
- **Produção**: `https://apispacex.runasp.net/swagger`

---

## 👨‍💻 Autor

**Desenvolvido para Trabalho de Curso Técnico**

- **Disciplina**: Desenvolvimento de APIs RESTful
- **Tecnologia**: .NET 8 Web API
- **Banco de Dados**: Firebase Realtime Database

---

## 📄 Licença

Este projeto é para fins educacionais.

---

## 🔗 Links Úteis

- [Swagger UI](https://apispacex.runasp.net/swagger)
- [Firebase Console](https://console.firebase.google.com/)
- [Documentação .NET 8](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-8)

---

## ✅ Checklist de Avaliação

| Critério | Status |
|----------|--------|
| API RESTful completa | ✅ |
| Operações CRUD | ✅ |
| Integração com Firebase | ✅ |
| Tratamento de erros | ✅ |
| Logging | ✅ |
| Documentação Swagger | ✅ |
| Endpoints de batch | ✅ |
| Códigos HTTP corretos | ✅ |
| README técnico | ✅ |

---

**Data de Entrega:** Maio/2026

**Status do Projeto:** ✅ Concluído e Publicado
