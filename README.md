# 👕 [EcommerceApp] - Loja de Roupas

![Status: Em Desenvolvimento](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)
![.NET 8](https://img.shields.io/badge/.NET-8-blueviolet?logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-blue)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core%208-blueviolet)
![AutoMapper](https://img.shields.io/badge/AutoMapper-13.0-orange)

Backend de um sistema de e-commerce desenvolvido para portfólio, focado em boas práticas de desenvolvimento, arquitetura limpa e performance.

## 🚀 Sobre o Projeto

O objetivo deste projeto é construir a API e a lógica de negócios de uma loja virtual robusta, lidando com catálogo de produtos, gerenciamento de usuários, carrinho de compras e processamento de pedidos.

### Funcionalidades Implementadas e Roadmap
- [x] Estrutura inicial do banco com Produtos e Categorias
- [x] Implementação do Padrão Repository e Unit of Work
- [x] Implementação da Camada de Aplicação (Services e DTOs)
- [x] CRUD completo para Categorias (Controllers, Views, Services)
- [ ] CRUD completo para Produtos (Controllers, Views, Services)
- [ ] Sistema de Autenticação e Autorização com ASP.NET Core Identity (Roles: Cliente e Admin)
- [ ] Funcionalidade de Carrinho de Compras
- [ ] Testes Unitários

## 🏛️ Arquitetura

O projeto utiliza **Arquitetura Limpa (Clean Architecture)** para garantir a separação de responsabilidades (SoC), testabilidade e manutenibilidade.

## 🛠️ Tecnologias Utilizadas

* **.NET 8**
* **ASP.NET Core MVC**
* **Entity Framework Core 8** (para ORM)
* **SQL Server** (Banco de Dados)
* **Arquitetura Limpa (Clean Architecture)**
* **Injeção de Dependência (DI)** nativa do ASP.NET Core
* **AutoMapper** (para mapeamento entre Entidades e DTOs)
* **Padrão Repositório (Repository Pattern)**
* **Padrão Unidade de Trabalho (Unit of Work)**
