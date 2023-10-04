# Projeto de Exemplo com Aplicações ASP.NET MVC, React e REST API (Ponta & Gabriel Guaitolini)

![Ponta](https://pontaagro.com/wp-content/uploads/2023/03/cropped-Marca-Site-Topo-2.png)

Criar uma API RESTful que permitirá aos usuários gerenciar uma lista de tarefas. A API deve ser construída em C# usando a plataforma .NET e deve seguir as melhores práticas de desenvolvimento, incluindo boas práticas de arquitetura, segurança e documentação. 

## Conteúdo

- [Visão Geral](#visão-geral)
- [Requisitos](#requisitos)
- [Executando o Projeto](#executando-o-projeto)
- [Navegação](#navegação)
- [Requisitos Entregues](#requisitos-entregues)
- [Estrutura do Projeto](#estrutura-do-projeto)

## Visão Geral

O projeto consiste em três partes principais:

1. **REST API**: Uma API em ASP.NET que fornece endpoints para criar, ler, atualizar e excluir tarefas.
2. **Aplicação React**: Uma aplicação cliente em React para interagir com a REST API.


## Requisitos

Certifique-se de ter os seguintes requisitos antes de iniciar o projeto:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio Community 2012](https://visualstudio.microsoft.com/pt-br/vs/community/)

## Executando o Projeto

1. Clone o repositório para o seu sistema local:

   ```bash
   git clone https://github.com/fuyangli/teste-ponta

2. Navegue até a pasta do projeto:

   ```bash
   cd teste-ponta
   ```

   Configure e inicie a REST API:

   ```bash
   dotnet run --project '.\TestePonta.Api\'
   dotnet run --project '.\TestePonta.App\'
   ```
   **OU execute o arquivo run.ps1 com o Powershell**

   ```bash
   run.ps1
   ```

Abra o navegador em "https://localhost:7001/swagger/index.html" para ver o Swagger da API
Abra o navegador em "https://localhost:7080/" para navegar na aplicação.

## Navegação
A API ao ser executada já cria três usuários para facilitar o acesso a página web.

| Usuário     | Email                    | Senha         |
|-------------|--------------------------|---------------|
| Paulo Dias    | paulodias@ponta.com.br     | senha         |
| Camila Oliveira   | camilaoliveira@ponta.com.br     | senha         |
| Dev1    | dev1@ponta.com.br     | senha         |

A senha é "senha" para também facilitar o login.
A autenticação é feita utilizando JWT e armazenada no localstorage do usuário.

Novos usuários podem ser criados e/ou deletados na tela de usuários. Há também a opção de se modificar um usuário (nesse caso, faça via Swagger ou chamada para a API).

Tarefas podem ser criadas na tela de tarefas. Apenas os usuários que criaram a tarefa podem atualiza-la ou deleta-la. Isso pode ser feito diretamente na tela ou via Swagger ou chamada para a API.

O usuário pode deslogar e logar com outro usuário para poder testar o permissionamento das tarefas.


Login: https://localhost:7080/login
Tarefas: https://localhost:7080/tasks
Usuários: https://localhost:7080/

## Requisitos Entregues:
##### Mínimo: 


- CRUD de Tarefas: 
   - Crie, leia, atualize e exclua tarefas. <span style="color: green;">☑</span>
   - Cada tarefa deve ter um título, uma descrição, uma data de criação e um status (pendente, em andamento, concluída). <span style="color: green;">☑</span>


- Listagem de Tarefas: 
   - Os usuários devem ser capazes de listar todas as tarefas ou filtrá-las com base em seu status. <span style="color: green;">☑</span>


- Autenticação e Autorização: 

   - A API deve suportar autenticação de usuários. <span style="color: green;">☑</span>
   - Apenas os criadores das tarefas devem poder atualizá-las ou excluí-las. <span style="color: green;">☑</span>
   - As tarefas devem ser visíveis para todos os usuários autenticados. <span style="color: green;">☑</span>


- Documentação: 

   - Documente sua API de forma clara e concisa, preferencialmente usando o Swagger ou ferramenta similar. <span style="color: green;">☑</span>
 

##### Diferenciais (aumenta suas chances de classificação): 

- Testes Unitários: 
   - Escreva testes unitários para garantir a robustez e a confiabilidade da API. 

- Segurança: 
   - Garanta que a API esteja protegida contra ataques comuns, como injeção SQL e ataques de força bruta. <span style="color: green;">☑</span>

- Boas Práticas: 
   - Siga as melhores práticas de desenvolvimento, como o uso de padrões de design apropriados e a separação de preocupações. <span style="color: green;">☑</span>

- Escalabilidade: 
   - Projete a API para ser escalável, considerando um grande volume de tarefas e usuários. <span style="color: green;">☑</span>

- Performance: 
    - Otimize a API para garantir alta performance e baixa latência. <span style="color: green;">☑</span>

- Logs: 
   - Implemente logs para rastrear eventos importantes na API, o que pode ser útil para depuração e monitoramento. <span style="color: green;">☑</span>
 
- Ferramentas desejáveis:
   - Usar uma ORM, preferencialmente o EF Core. <span style="color: green;">☑</span>
   - Utilizar EF Core In-Memory, ou qualquer banco de dados relacional de sua escolha, neste caso, preferencialmente o PostgreSQL. <span style="color: green;">☑</span>
 

- Entregáveis: 
   - Um repositório Git (GitHub, GitLab, etc.) com o código-fonte da API. <span style="color: green;">☑</span>
   - Documentação clara sobre como executar a API localmente. <span style="color: green;">☑</span>
   - Instruções para autenticar-se na API e realizar operações CRUD. <span style="color: green;">☑</span>



## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte maneira:

/TestaPonta.Api: Contém o código-fonte da REST API em ASP.NET (com Swagger).
/TestaPonta.App: Contém o código-fonte da aplicação React.
/TestaPonta.Application: Contém o código-fonte da camada de aplicação.
/TestaPonta.Core: Contém o código-fonte da biblioteca compartilhada do projeto.
/TestaPonta.Domain: Contém o código-fonte da camada de Domínio.
/TestaPonta.Infrastructure: Contém o código-fonte da camada de Infraestrutura.
/TestaPonta.Tests: Contém o código-fonte dos testes da aplicação.