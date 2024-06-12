## Resumo

O projeto consiste em uma API com funcionalidades de E-Commerce, com usuários, pedidos, lista de desejo, pagamentos e produtos. Para ter acesso a funcionalidade total do projeto, o usuário cria uma conta, realiza o login e um token é gerado.

## Tecnologias Usadas

C# / ASP.NET Core

Entity FrameWork

SQL Server

Git

Além das boas práticas de codificação, modelo de API Restful e Token JWT para segurança da aplicação.

## Descrição Geral

Como já havia dito, para ter acesso ao projeto completo, teria que ter um usuário (fictício mesmo), daí o usuário realiza o login e um token JWT é gerado, permitindo que ele acesse todos os outros componentes disponíveis.

## Componentes

- Users: criação, remoção e login de usuário;

- Products: listagem de todos os produtos da loja;

- Cart: adição, remoção e listagem de todos os produtos adicionados no carrinho;

- WishList: adição, remoção e listagem de todos os produtos adicionados na lista de desejo;

- Orders: solicitação, cancelamento e listagem de pedidos pendentes;

- Payment: pagamento de pedidos pendentes, ao pagar, o pedido é excluído automáticamente. O pagamento é realizado com um cartão de crédito.

<img src="https://api.scalar.com/cdn/images/ot1GtluuVHyLxWwNp6TGZ/UpTSB6IasEXtXuJmR1u-A.png">

## Validações

Neste projeto, criei algumas validações importantes, que são elas:

- Validações de Usuário

Criação da Conta: estrutura de e-mail correta, senha maior que 4 caracteres e se caso o e-mail ou nome de usuário já exista no banco de dados, não será possível criar um novo usuário com as credenciais estabelecidas.

Login: Caso o e-mail ou senha estejam errados, não será possível fazer o login e se as credenciais não existirem, também não será possível.

- Validações de Pagamento


Caso o número do cartão seja inválido, por exemplo, com muitos ou poucos números, não será possível realizar o pagamento, mesma coisa para o CVV e a data de validade.

- Validações de Carrinho


Adição do produto ao carrinho: Caso o id do produto seja igual o menor que 0, será o id inválido, mesma coisa para a quantidade de produto a ser adicionado, caso a quantidade desejada seja maior que a disponível na loja, não será possível adicionar  o produto.


Remoção do produto do carrinho:  Mesma validação para o id e quantidade.

## Link do Scalar

Para mais detalhes: <a href="https://ecommercerapi.apidocumentation.com/reference">Scalar</a>
