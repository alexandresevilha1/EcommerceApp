# 👕 [EcommerceApp] - Loja de Roupas

![Status: Em Desenvolvimento](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)
![.NET 8](https://img.shields.io/badge/.NET-8-blueviolet?logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-blue)
![Entity Framework Core 8](https://img.shields.io/badge/Entity%20Framework-Core%208-blueviolet)
![AutoMapper](https://img.shields.io/badge/AutoMapper-13.0-orange)

Backend de um sistema de e-commerce desenvolvido para portfólio, focado em boas práticas de desenvolvimento, arquitetura limpa e performance.

## 🚀 Sobre o Projeto

O objetivo deste projeto é construir a lógica de negócios e a fundação de uma loja virtual robusta, lidando com catálogo de produtos, gerenciamento de usuários, carrinho de compras e processamento de pedidos.

### Funcionalidades Implementadas e Roadmap

- [x] **Arquitetura Limpa (Clean Architecture) de 4 camadas**
- [x] **Repository Pattern** e Unit of Work**
- [x] Repositórios e Serviços 100% Assíncronos (`async/await`)
- [x] Implementação da Camada de Aplicação (Services, DTOs, AutoMapper)
- [x] **CRUD completo para Categorias** (Controller, Services e Views)
- [x] **CRUD completo para Produtos** (Controller, Services e Views)
- [ ] Sistema de Autenticação e Autorização com ASP.NET Core Identity
- [ ] Funcionalidade de Carrinho de Compras
- [ ] Testes Unitários

## 🏛️ Arquitetura

O projeto foi refatorado para seguir estritamente os princípios da **Arquitetura Limpa (Clean Architecture)**, separando o código em 4 projetos independentes com responsabilidades bem definidas:

* **`EcommerceApp.Core`**:
    * Contém as Entidades de Domínio (`ProductModel`, `CategoryModel`).
    * Contém as *interfaces* dos Repositórios (`IRepository`, `IUnitOfWork`, `ICategoryRepository`, etc.).

* **`EcommerceApp.Application`**:
    * Depende apenas do `Core`.
    * Contém os Serviços (`ICategoryService`, `IProductService`) que orquestram as regras de negócio.
    * Contém os DTOs (Data Transfer Objects) e os perfis de mapeamento do AutoMapper.

* **`EcommerceApp.Infrastructure`**:
    * Depende de `Application` e `Core`.
    * Contém a implementação do acesso a dados com Entity Framework Core (`AppDbContext`).
    * Contém a implementação dos Repositórios (`UnitOfWork`, `ProductRepository`, etc.).
    * Gerencia as Migrações do banco de dados.

* **`EcommerceApp.Web`**:
    * Depende de `Application` e `Infrastructure`.
    * Contém os Controllers do ASP.NET Core MVC, Views e `wwwroot`.
    * Responsável pela configuração e Injeção de Dependência (`Program.cs`).

## 🛠️ Tecnologias Utilizadas

* **.NET 8**
* **ASP.NET Core MVC**
* **Entity Framework Core 8**
* **SQL Server**
* **Arquitetura Limpa (Clean Architecture)**
* **Programação Assíncrona** (`async/await`)
* **Injeção de Dependência (DI)**
* **AutoMapper**
* **Padrão Repositório Genérico (Repository Pattern)**
* **Padrão Unit of Work**
4.  **Execute o Projeto:**
    * No Visual Studio, defina `EcommerceApp.Web` como o projeto de inicialização (clique direito > "Definir como Projeto de Inicialização").
    * Rode o projeto (F5 ou `dotnet run --project EcommerceApp.Web`).
