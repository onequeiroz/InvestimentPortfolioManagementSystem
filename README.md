# Investiment Portfolio Management System
## Sistema de Gerenciamento de Portfolio de Investimentos
Trata-se de um Sistema que permite a Gestão de Produtos Financeiros por parte de Administradores e permite que os Clientes comprem, vendam e acompanhem seus investimentos.

## Features
##### - Disparo de email recorrente - No momento, informa aos administradores sobre produtos que estão próximos do vencimento
##### - Fornece APIs para:
> CRUD de Produtos (Investimentos) e de Usuários (Clientes)\
> Compra e Venda de Produtos (Transações)\
> Consulta de Produtos disponíveis e de Extrato de Produtos

## Ferramentas Utilizadas + Organização do Projeto
##### - Entity Framework Core
##### - Injeção de Dependência
##### - Pacote Quartz e SendGrid para envio de Emails
##### - Organização em: 
> Projeto de API com Controllers\
> Projeto da Aplicação com Services | Models | Repositories

## Configurando a aplicação
Faça a clonagem do repositório em sua máquina. 
> Esse link pode te ajudar: https://docs.github.com/pt/repositories/creating-and-managing-repositories/cloning-a-repository

Em seguida, abra o projeto em seu editor (tal como Visual Studio).
Você poderá notar que há 2 projetos - InvestimentPortfolioManagementSystem.API e InvestimentPortfolioManagementSystem.Application.
> ...System.API é o projeto principal (Startup Project) e ...System.Application deve ser adicionado como Projeto de Referência.

Garanta que os pacotes abaixo estejam instalados (o projeto foi criado com .NET 6, portanto garanta compatibilidade dos pacotes com a versão .NET 6x).
> Microsoft.EntityFrameworkCore\
> Microsoft.EntityFrameworkCore.Abstractions\
> Microsoft.EntityFrameworkCore.Relational\
> Microsoft.EntityFrameworkCore.SqlServer\
> Microsoft.Extensions.DependencyInjection\
> Quartz\
> Quartz.AspNetCore\
> Quartz.Extensions.DependencyInjection\
> SendGrid\
> SendGrid.Extensions.DependencyInjection

Neste momento, já deve ser possível rodar a aplicação!
> **Importante notar**: o disparo de Email não está funcionando por motivos de segurança.\
> Caso deseje rodar o disparo de Email, *entre em contato comigo!*\
> Posso fornecer uma chave que funcione para teste ou orientar como criar uma.
> Tutorial seguido para implementação: https://www.twilio.com/en-us/blog/how-to-send-recurring-emails-in-csharp-dotnet-using-sendgrid-and-quartz-net

## Como utilizar a aplicação
Ao rodar a aplicação, será possível ver o Swagger com os endpoints criados.
Vamos utilizá-los para criar Produtos, Usuários e Transações.
Para auxiliar na criação, deixei ao final do arquivo alguns objetos em JSON para serem utilizados como modelo.
> Note que é importante fornecer Emails válidos para que a feature de disparo de Emails funcione.

### Criando Produtos, Usuários e Transações
Primeiramente, crie ao menos 1 Usuário Administrador e ao menos 1 Usuário Cliente
Em seguida, crie ao menos 1 Produto
Utilize os métodos de Consulta para consultar os IDs criados e para ter certeza que os dados informados estão corretos

Para criar uma Transação, informe o Id de um Produto e do Usuário Cliente criado. Informe também o Tipo da Transação.
É possível consultar as Transações realizadas, até mesmo com Id de Usuário e com Id de Produto
> api/Transaction/GetAllTransactions\
> api/Transaction/GetTransactionsByUserId\
> api/Transaction/GetTransactionsByProductId

É possível consultar Produtos Comprados 
> api/Product/ConsultProductsByOwnerId

Consulta de Produtos Disponíveis:
> api/Product/GetAvailableProductsForSell

Consulta de Extrato de Produto:
> api/Product/GetProductExtractByProductId

Também temos um CRUD de Usuários, no qual também é possível Habilitar e Desabilitar usuários.

### Regras de Negócio
Durante a utilização da aplicação, é possível notar que há algumas regras:

##### - Apenas Usuários Administradores podem criar e atualizar Produtos
##### - Não é possível incluir Produtos com nomes duplicados
##### - Não é possível incluir Usuários com nomes e emails duplicados
##### - Não é possível passar a propriedade "OwnerId" a um Produto diretamente. Isso é feito apenas em uma Transação.
##### - Usuários inativos não podem negociar Produtos
##### - Produtos indisponíveis para Venda não podem ser negociados
##### - Usuários que possuem produtos não podem ser deletados do sistema

### Seguem abaixo exemplos (com comentários) a serem utilizados para criação de novos Produtos, Usuários e Transações.
#### User Type:
> Manager (Administrador) = 1\
> Customer (Clientes) = 2\
> Se informar outro valor, a requisição dará erro

#### Transaction Type:
> Buy (Compra) = 1\
> Sell (Venda) = 2\
> Se informar outro valor, a requisição dará erro

#### JSON
```json
User
{
  "name": "Your Name",
  "emailAddress": "your_email_address@gmail.com",
  "userType": 1
}

Product
{
  "name": "Investiment1",
  "description": "Investiment1",
  "value": 100,
  "expirationDate": "2024-04-25T02:17:33.280Z",
  "userOperationId": "63e45e8b-f74a-450d-9017-05a06b673a93"
}

Transaction
{
  "userId": "aa6dc766-3993-47ca-a754-f5dff711a648",
  "productId": "41bb182a-11a6-4289-af1a-7fef8b89a989",
  "productValue": 0,
  "transactionType": 1
}
```
#### Comentários:
> User "emailAddress": O endereço de email informado deve ser único\
> Product "userOperationId": Usuário Administrador que está criando o Produto\
> Transaction "userId": Usuário Cliente que está Vendendo ou Comprando um produto\
> (opcional) Transaction "productValue": valor do produto no momento da negociação. Se não for informado, o valor atual do produto será informado.
