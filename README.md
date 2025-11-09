# 👕 [EcommerceApp] - Loja de Roupas

![Status: Em Desenvolvimento](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)
![.NET 8](https://img.shields.io/badge/.NET-8-blueviolet?logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-blue)
![Entity Framework Core 8](https://img.shields.io/badge/Entity%20Framework-Core%208-blueviolet)
![AutoMapper](https://img.shields.io/badge/AutoMapper-13.0-orange)
![xUnit](https://img.shields.io/badge/Teste-xUnit-success)

Backend de um sistema de e-commerce desenvolvido para portfólio, focado em boas práticas de desenvolvimento, arquitetura limpa e performance.

## 🚀 Sobre o Projeto

O objetivo deste projeto é construir a lógica de negócios e a fundação de uma loja virtual robusta, lidando com catálogo de produtos, gerenciamento de usuários, carrinho de compras e processamento de pedidos.

### Funcionalidades Implementadas e Roadmap

- [x] **Arquitetura Limpa (Clean Architecture) de 4 camadas**
- [x] **Padrão Repositório Genérico (Repository Pattern)** e **Unidade de Trabalho (Unit of Work)**
- [x] Repositórios e Serviços 100% Assíncronos (`async/await`)
- [x] Implementação da Camada de Aplicação (Services, DTOs, AutoMapper)
- [x] **CRUD completo para Categorias** (Controller, Services e Views)
- [x] **CRUD completo para Produtos** (Controller, Services e Views)
- [x] **Vitrine de Produtos** (Home/Index e Busca)
- [x] **Sistema de Autenticação e Autorização** com ASP.NET Core Identity
    - [x] Registro e Login de usuários
    - [x] Papéis (Roles): Admin e Cliente
    - [x] Proteção de rotas administrativas com `[Authorize(Roles = "Admin")]`
- [x] **Funcionalidade de Carrinho de Compras Híbrido**
    - [x] Carrinho anônimo (armazenado na `Session`)
    - [x] Carrinho persistente para usuários logados (armazenado no SQL Server)
    - [x] Migração automática de itens da sessão para o banco após login
- [x] **Checkout de Pedidos**
    - [x] Conversão de Carrinho para Pedido (Order)
- [x] **Testes Unitários** (xUnit, Moq, FluentAssertions) para a camada de Aplicação

## 🏛️ Arquitetura

O projeto foi refatorado para seguir estritamente os princípios da **Arquitetura Limpa (Clean Architecture)**, separando o código em 4 projetos independentes com responsabilidades bem definidas:

* **`EcommerceApp.Core`**: O núcleo do sistema. Não depende de nenhum outro projeto.
    * Contém as Entidades de Domínio (`ProductModel`, `CategoryModel`, `CartModel`, `OrderModel`).
    * Contém as *interfaces* dos Repositórios (`IRepository`, `IUnitOfWork`, `ICategoryRepository`, etc.).

* **`EcommerceApp.Application`**: A camada de lógica de negócios (onde o "pensamento" acontece).
    * Depende apenas do `Core`.
    * Contém os Serviços (`CartService`, `OrderService`, etc.) que orquestram as regras de negócio.
    * Contém os DTOs (Data Transfer Objects) e os perfis de mapeamento do AutoMapper.

* **`EcommerceApp.Infrastructure`**: A camada de detalhes de implementação.
    * Depende apenas do `Core`.
    * Contém a implementação do acesso a dados com Entity Framework Core (`AppDbContext`).
    * Contém a implementação dos Repositórios e do Unit of Work.
    * Gerencia as Migrações do banco de dados.

* **`EcommerceApp.Web`**: A camada de apresentação (o projeto de inicialização).
    * Depende de `Application` e `Infrastructure`.
    * Contém os Controllers do ASP.NET Core MVC, Views e `wwwroot`.
    * Responsável pela configuração e Injeção de Dependência (`Program.cs`).

## 🛠️ Tecnologias Utilizadas

* **.NET 8**
* **ASP.NET Core MVC**
* **Entity Framework Core 8** (para ORM)
* **SQL Server** (Banco de Dados)
* **ASP.NET Core Identity** (Autenticação e Autorização)
* **xUnit, Moq, FluentAssertions** (Testes Unitários)
* **Arquitetura Limpa (Clean Architecture)** de 4 camadas
* **Programação Assíncrona** (`async/await`)
* **Injeção de Dependência (DI)** nativa
* **AutoMapper** (para mapeamento entre Entidades e DTOs)
* **Padrão Repositório Genérico (Repository Pattern)**
* **Padrão Unidade de Trabalho (Unit of Work)**
