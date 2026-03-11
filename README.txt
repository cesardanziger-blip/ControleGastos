# Controle de Gastos

Sistema de controle de gastos residenciais.

## Tecnologias

- Backend: C# e .NET (Swagger, Dependency Injection, MVC, EntityFrameworkCore, AutoMapper etc)
- Frontend: React + TypeScript + Axios + Toast + Recharts
- Persistência: SQLite

## Funcionalidades

### Cadastro de Pessoas
- Criar, editar, listar e deletar pessoas
- Deletar pessoa apaga todas as transações vinculadas

### Cadastro de Categorias
- Criar, listar e deletar categorias
- Finalidade: despesa/receita/ambas
- Deletar categoria apaga todas as transações vinculadas

### Cadastro de Transações
- Criar, listar e deletar transações
- Restrições para menores de idade
- Restrições de categoria conforme tipo

### Consultas
- Totais por pessoa
- Totais por categoria
- Gráfico Despesa x Receita

## Rodando o projeto

### Backend
1. cd ControleGastos.Api
2. Abra o terminal
3. Restaurar dependências - dotnet restore
4. Execute o projeto - dotnet run
4. Porta Back - 7260

### Frontend
1. cd ControleGastos.Front
2. Abra o terminal
3. Restaurar dependências - dotnet restore
4. Execute o projeto - npm run dev
4. Porta Back - 5173

Obs -> 
1. Caso dê problema de Cors, verificar program no back a porta que esta liberada é a 5173
2. Caso o back esteja em execução e não esteja carregando os dados no front, verificar a porta de acesso as API's no Axios , pasta api/api.js no front
