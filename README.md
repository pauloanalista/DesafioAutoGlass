# DesafioAutoGlass

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

### Sobre minha implementação
Apesar do desfio ser simples, tentei criar uma arquitetura mais robusta que pode ser utilizada em um projeto real.
Como usei vários Design Patners, ficaria muito extenso escrever uma documentação, então gravei um vídeo mostrando toda arquitetura.

https://youtu.be/d5O57v_ZtFs

### Agradecimento
Gostaria de agradecer pelo desafio passado e peço que avaliem o projeto com calma e me mandem feedbacks, mesmo que negativo.
Sei que alguns pontos ainda da para melhorar bastante, mas acho interessante batermos um papo sobre arquitetura pessoalmente.

Desde já agradeço pela oportunidade!

Att,
Paulo
