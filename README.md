# DesafioAutoGlass

https://youtu.be/d5O57v_ZtFs

<img align="left" src="https://i2.wp.com/ilovecode.com.br/wp-content/uploads/2020/03/post_ok.gif?fit=200%2C209&ssl=1" />

# Paulo Rogério Martins Marques

[![Blog](https://img.shields.io/badge/Blog-blue.svg?style=for-the-badge&logo=wordpress)](https://olha.la/ilovecode)
[![Youtube](https://img.shields.io/badge/Youtube-red.svg?style=for-the-badge&logo=youtube)](https://www.youtube.com/channel/UChoveUE94dFSAPfPiJhFsew)
[![Linkedin](https://img.shields.io/badge/LinkedIn-blue?style=for-the-badge&logo=Linkedin)](https://www.linkedin.com/in/paulorogerio/)



Olá, me chamo Paulo Rogério e sou desenvolvedor fullstack, fundei a comunidade ilovecode onde compartilho artigos, cursos técnicos e ajudo outros devs no meu tempo livre.
Minha stack favorita é basicamente qualquer coisa com C# (aplicações web, desktop, mobile e automação), aplicativos híbridos com ionic framework, angular e etc.
Não me limito somente a essas tecnologias, ja fiz apps em flutter, xamarin, react=native, usei diversos frameworks frontend e etc.

## Sobre o Desafio
Achei o desafio interessante por não precisar criar uma estrutura muito grande para mostrar nosso conhecimento.
Tomei liberdade em acrescentar algumas coisas novas ao projeto que podem facilitar muito o processo de desenvolvimento.

## Observação
Gravei o vídeo antes de terminar 100% do projeto, como fiquei sabendo do desafio hoje e já tinha alguns compromissos, comecei a desenvolver tudo a noite.
Amanhã não terei disponibilidade para gravar o vídeo apresentando o projeto, então tomei a liberdade de mostrar todo o projeto já desenvolvido, com todas regras solicitadas, mas ainda esta pendente o desenvolvimento do teste unitário.
Pretendo entregar o teste unitário amanhã, mas ele não fará parte do vídeo.

## Organização do projeto

### Estrutura da Solution
Criei 4 projetos
API -> Responsável por externalizar os recursos
Dominio -> Responsável por toda parte de negócio e contratos
Infra -> Responsável por fazer acesso a repositórios, arquivos e acessar arquivos externos
Test -> Responsável por testar todo o projeto

![image](https://github.com/pauloanalista/DesafioAutoGlass/assets/6010161/3053d65f-8b27-4fc8-b342-41eade33941d)

### Vamos falar da API
Normalmente toda configuração inicial da API fica no arquivo Startup.cs, eu acho que fica um pouco bagunçado a parte de DI, então criei um arquivo chamado Setup.cs com o propósito de organizar a DI.

Veja como o Startup.cs fica mais organizado

![image](https://github.com/pauloanalista/DesafioAutoGlass/assets/6010161/a2bbede9-c2b8-4a0a-8459-48e77c4509b4)


Confira o arquivo Setup.cs

![image](https://github.com/pauloanalista/DesafioAutoGlass/assets/6010161/af2c4253-3e80-4251-b27f-239098e3e3c0)


Organizar desta forma deixa nosso Startup mais clean e objetivo, veja um exemplo de como fica um projeto real.

#### Exemplo de como configuro meus projetos
```csharp
  services.ConfigureMediatR();
  services.AddControllers();
  services.ConfigureSwagger();
  services.ConfigureUploadLimitToMaximum();
  services.ConfigureServices();
  services.ConfigureRepositories();
  services.ConfigureCompression();
  services.ConfigureAuthentication();
```

### Organização da API
Embora eu não organize a estrutura de minhas APIs desta forma, eu procurei separar um pouco das responsabilidades de cada coisa.

Confira imagem abaixo:

![image](https://user-images.githubusercontent.com/6010161/140450490-ed23306d-2357-460e-adbc-9423f2e4ad93.png)

Como podemos observar eu criei uma pasta Domain e uma pasta Infra, normalmente eu crio um projeto separado para cada uma delas, gosto de separar minha API do domínio e camada de infraestrutura.

No domínio trabalho diretamente com interfaces, seja serviços externos, comandos enviados para API e etc.
No projeto de dominio não dever haver dependencias diretas, apenas deve depender das suas próprias interfaces criadas no projeto de domínio.

Procurei criar uma entidade para cuidar do Juros Compostos, coloquei a regra de negócio dentro dela, criei até uma regra para ilustrar notificações caso passe valores zerados.

Não costumo levantar exceções em meus domínios apenas notificações, isso evita que minha API receba diversas requisições para tratar apenas um request.

Muitos conceitos eu não abordei neste API, como trabalhar com Enums, Operações Explicitas, Extensions Methods e etc.

Removi toda regra de negócio da Controller e agora depende apenas de uma Interface que deverá ser injetada no Startup do projeto. Tomei a liberdade de criar 2 actions, uma com o resultado solicitado no desafio e outra com o resultado padronizado. Eu gosto de padronizar o retorno da Api, isso facilita o frontend e o projeto fica mais organizado.

![image](https://user-images.githubusercontent.com/6010161/140522958-af28a4ab-986e-46c7-8295-c6467895e784.png)

A responsabilidade de calcular a taxa é de meu Command.

![image](https://user-images.githubusercontent.com/6010161/140451174-f014ee18-3ceb-4302-8336-b0d5380be9ee.png)

Na classe concreta de meu command que fica em meu Dominio em implemento sempre utilizando interfaces, como podemos observar o uso do serviço IServiceTaxa para obter a taxa da outra API.
Procurei trabalhar com notificações, caso a entidade JuroComposto tenha algum tipo de problema, ela dispara uma notificação que é adicionada ao meu command.
Meu command devolve o resultado da requisição de forma padronizada, podendo ter a resposta desejada ou notificações.

![image](https://user-images.githubusercontent.com/6010161/140523413-e4a057ea-c2d9-4b88-bf33-bfe1a470cbc4.png)


Para calcular o Juros Composto eu joguei a responsabilidade para minha entidade

![image](https://user-images.githubusercontent.com/6010161/140523878-69255939-7aae-4fa4-98b7-4cdbf55ef167.png)


Através do método ObterValorTruncadoSemArrendondamento, temos o resultado do calculo em mãos

![image](https://user-images.githubusercontent.com/6010161/140451519-de2f05e3-dee6-4af9-b0f5-74ed09477cd2.png)

Para obter o juros em meu command eu passo a responsabilidade para um serviço externo, como só preciso me preocupar com a interface o código fica bem simples.

![image](https://user-images.githubusercontent.com/6010161/140524367-cd6d40c4-1e87-478f-91e5-450f3323d82d.png)


A implementação para acessar serviços externos, enviar e-mail, acessar arquivos devem ficar  na camada de infraestrutura.
Veja baixo como ficou nossa chamada para API.

![image](https://user-images.githubusercontent.com/6010161/140451862-d1937f85-7268-4381-90bc-3535c0b18b3f.png)

### Teste unitários
Sei que é possível criar teste de unidade, teste de integração, teste de UI, mas foquei apenas no teste de unidade.

OBS: Na empresa que atuo usa-se pouco teste unitário, eu quando utilizo uso muito o MSTest da Microsoft, mas ultimamente resolvi fazer um curso do XUnit e achei sensacional, então tomei a liberdade em criar os testes com ele.

![image](https://user-images.githubusercontent.com/6010161/140543118-6fce3d8c-50eb-4181-9da9-4348800ef1eb.png)

### Como rodar a aplicação pelo Visual Studio?
Para rodar pelo Visual Studio é bem fácil, basta pressionar F5 que as 2 apis irão ser levantadas. Eu configurei para o Visual Studio abrir ambas em sua execução.
Ao rodar verá 2 APIS levantadas com a documentação do Swagger.
A configuração da API de Taxas fica no appsettings.json

### Como rodar a aplicação pelo Visual Studio Code ou Prompt de Comando?
Entre no diretório do projeto da API e utilize o comando dotnet run

```
dotnet run Softplan.Calc.Api.csproj
```

### Como rodar com docker
Faça o mesmo procedimento com ambas as APIS dando nome de imagens diferentes, acesse o diretório do projeto da api e digite o comando abaixo:

```
docker build . -t softplancalcapi
```
Verá uma tela semelhante a esta
![image](https://user-images.githubusercontent.com/6010161/140551516-2f4c6d48-622e-4a8e-a5bf-259b58b0142c.png)

Após de executar todo procedimento vamos visualizar a imagem digitando o comando abaixo:
```
docker image ls
```
Irá aparecer a imagem criada como podemos ver abaixo:
![image](https://user-images.githubusercontent.com/6010161/140551824-9171e9ef-ba21-4a2a-b535-66b1d0e96639.png)

Para rodar execute o comando abaixo:
```
docker run --name calcapi -p 44390:44390 softplancalcapi
```

Como podemos ver no portainer API está rodando
![image](https://user-images.githubusercontent.com/6010161/140553202-e560c285-86c8-435c-8fa3-0b3876d94cca.png)

### Agradecimento
Gostaria de agradecer pelo desafio passado e peço que avaliem o projeto com calma e me mandem feedbacks, mesmo que negativo.
Sei que alguns pontos ainda da para melhorar bastante, mas acho interessante batermos um papo sobre arquitetura pessoalmente.

Desde já agradeço pela oportunidade de se tornar um SoftPlayer!

Att,
Paulo
