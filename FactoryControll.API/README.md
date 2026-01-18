# Desafio Técnico Senai - API

API desenvolvida em ASP.NET Core (.NET 9) para gestão administrativa, autenticação, usuários, colaboradores e reembolsos.

## Sumário

- [Visão Geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Como Executar](#como-executar)
- [Exemplos de Endpoints](#exemplos-de-endpoints)
- [Tratamento de Erros](#tratamento-de-erros)
- [Testes](#testes)
- [Observações](#observações)

---

## Visão Geral

Este projeto é uma API RESTful que oferece recursos para autenticação de usuários, administração de cargos, funções, colaboradores e controle de reembolsos. Utiliza autenticação JWT, FluentValidation, AutoMapper e Entity Framework Core.

---

## Funcionalidades

- **Autenticação**: Login e registro de usuários com geração de JWT.
- **Administração**: CRUD de cargos e funções.
- **Usuários**: Gerenciamento de usuários do sistema.
- **Colaboradores**: Cadastro e manutenção de colaboradores.
- **Reembolsos**: Solicitação e acompanhamento de reembolsos.
- **Tratamento global de exceções**: Middleware para respostas padronizadas de erro.

---

## Tecnologias Utilizadas

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- FluentValidation
- Identity (autenticação e autorização)
- JWT Bearer Authentication
- xUnit & Moq (testes)

---

## Como Executar

1. **Pré-requisitos**:
   - .NET 9 SDK
   - SQL Server (ou outro banco configurado)
2. **Configuração**:
   - Ajuste as strings de conexão e configurações JWT no `appsettings.json`.
3. **Migrações**:
   - Execute as migrações do Entity Framework para criar o banco: ```bash
 dotnet ef database update --project FactoryControll.InfraData
 ```4. **Execução**:
   - Inicie a API: ```bash
 dotnet run --project FactoryControll.API
 ```5. **Swagger**:
   - Acesse a documentação interativa em: `http://localhost:<porta>/swagger`

---

## Exemplos de Endpoints

### Autenticação

- **Login**
  - POST /api/Autenticacao/login ```json
    { 
      "email": "usuario@teste.com", 
      "password": "Senha123!" 
    }```
   **Resposta:** JWT Token

### Cargos

- **Listar cargos**
- GET /api/Cargo

- **Criar cargo**
POST /api/Cargo ```json
{ 
    "nome": "Analista", 
    "descricao": "Analista de Sistemas" 
}```
  **Resposta:** Cargo criado com sucesso

### Reembolsos

- **Solicitar reembolso**
POST /api/Reembolso ```json
{ 
    "colaboradorId": 1, 
    "valor": 100.00, 
    "descricao": "Reembolso de despesas" 
}```
  **Resposta:** Reembolso solicitado com sucesso

---

## Tratamento de Erros

A API utiliza um middleware global para capturar exceções e retornar respostas padronizadas em JSON, facilitando o consumo e o debug.

---

## Testes

Os testes unitários e de integração estão localizados na pasta `FactoryControll.Tests`. Para rodar os testes:
---

## Observações

- Para acessar endpoints protegidos, é necessário autenticar-se e fornecer o token JWT no header `Authorization`.
- O projeto segue boas práticas de arquitetura, separando áreas, models, mapeamentos e validações.
- Para dúvidas ou sugestões, utilize o repositório ou entre em contato com o responsável pelo projeto.

---
