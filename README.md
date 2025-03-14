# Developer Evaluation Project

## **Visão Geral do Sistema**

Este projeto foi desenvolvido para implementar uma API de gerenciamento de vendas, incluindo funcionalidades como CRUD de vendas, aplicação de regras de negócio e publicação de eventos internos. O sistema é baseado em **Arquitetura Limpa (Clean Architecture)**, utiliza **CQRS com MediatR** e está estruturado para ser escalável e modular.

## **Fluxo Operacional**

1. **Criação de Venda**:

   - O cliente realiza uma requisição para criar uma venda.
   - A API processa os itens e aplica as regras de negócio.
   - A venda é salva no banco de dados e um evento **SaleCreated** é gerado.

2. **Atualização de Venda**:

   - A API valida os itens e recalcula o total da venda.
   - A venda é atualizada e o evento **SaleModified** é publicado.

3. **Cancelamento de Venda**:

   - A venda é marcada como cancelada e um evento **SaleCancelled** é publicado.

4. **Consulta de Vendas**:

   - Permite obter todas as vendas cadastradas ou uma venda específica por ID.

## **Regras de Negócio**

- Compras acima de 4 itens idênticos recebem **10% de desconto**.
- Compras entre 10 e 20 itens idênticos recebem **20% de desconto**.
- É proibido vender mais de **20 unidades** do mesmo produto.
- Compras abaixo de **4 itens** não possuem desconto.

## **Componentes da Arquitetura**

### **1. API de Vendas**

- Implementa o **CRUD de vendas**.
- Utiliza **CQRS com MediatR** para separação de comandos e queries.
- Publica eventos internos (**SaleCreated, SaleModified, SaleCancelled**).

### **2. Banco de Dados**

- **PostgreSQL**: Armazena as informações de vendas e itens.
- **MongoDB**: Pode ser utilizado para armazenar logs ou histórico de vendas.
- **Redis**: Armazena dados temporários para otimizar consultas.

### **3. Testes Automatizados**

- **Testes Unitários**: Validam regras de negócio.
- **Testes de Integração**: Garantem a comunicação entre os componentes.
- **Testes Funcionais**: Simulam interações do usuário com a API.

## **Execução do Projeto**

### **1. Configuração do Ambiente**

#### **Requisitos**

- **.NET 9**
- **Docker e Docker Compose**
- **PostgreSQL, MongoDB e Redis**
- **Visual Studio 2022 ou VS Code**

### **2. Clonando o Repositório**

```sh
 git clone <URL_DO_REPOSITORIO>
 cd <NOME_DO_PROJETO>
```

### **3. Configuração do Docker**

```sh
 docker-compose up -d
```

Este comando subirá os seguintes containers:

| **Serviço** | **Porta** |
| ----------- | --------- |
| API         | 8080      |
| PostgreSQL  | 5432      |
| MongoDB     | 27017     |
| Redis       | 6379      |

### **4. Rodando o Projeto **

```sh
 Basta no visual studio setar o docker-compose como projeto principal e iniciar apertando o play ou executar o passo 3 (configuração do docker)
```

### **5. Testando a API**

A API pode ser testada via **Swagger** em:

```sh
 http://localhost:8080/swagger/index.html
```

**Postman** (Nota: será preciso gerar o token e ids e em seguida substituir o que está nas apis na collection)  em:
[Postman](./doc/DeveloperEvalution.postman_collection.json)


## **Arquitetura do Projeto**

```
Ambev.DeveloperEvaluation
│── src
│   ├── Application      # Casos de uso e handlers CQRS
│   ├── Domain           # Entidades e contratos
│   ├── ORM              # Contexto e repositórios do banco
│   ├── WebApi           # Controllers e endpoints
│   ├── IoC              # Configuração de DI
│
│── tests
│   ├── Unit            # Testes unitários
│   ├── Integration     # Testes de integração
│   ├── Functional      # Testes funcionais
│
│── docker-compose.yml  # Configuração do ambiente Docker
```

## **Melhorias Futuras**

- Implementação de um **API Gateway** para gerenciamento centralizado.
- Monitoramento com **Prometheus e Grafana**.
- Integração com **RabbitMQ** para filas de eventos.
- Ajuste nos metodos de testes para comportar todas as classes.

## **Licença**

Este projeto está licenciado sob a [MIT License](LICENSE).

