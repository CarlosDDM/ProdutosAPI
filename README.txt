Para instanciar o banco de dados PostgreSQL:

	Host = localhost
	Database = produto
	Username = postgres
	Password = postgres

Abir no console de gerenciamento de pacotes que esta dentro do Gerenciador de
pacotes NuGet o comando:
	
	Add-Migration "nomeDaSuaMigration"

Sera criada uma pasta com o nome Migration e executar o comando:

	Update-Database

Com isso seu banco de dados devera ter sido criado e instanciado.

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

Api usuário e senha:
usuário  : admin
password : password

Perguntas:
	1. Utilizei o princípio da responsabilidade única para a criação as classes
do projeto e tambem utilizei o princípio de inversão de dependência para construir
a classe do controller e do services. O motive da escolha de ambos se deve para deixar
o codigo bem modularizado para facilitar a manutenção dele e deixar minhas classes
fáceis de reaproveitar no codigo.

	2. Dado o cenário seria tornar mais assíncrono meus metodos que se ligam ao banco
de dados e fazer uma implementação de um micro serviço para fazer a fazer a autenticação
fora do ProdutosAPI(nome da aplicação criada) para deixar eles com banco de dados
separados entregando serviços diferentes.