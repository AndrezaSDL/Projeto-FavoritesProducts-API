# Projeto-FavoritesProductsAPI

![alt text](https://github.com/AndrezaSDL/Projeto-FavoritesProducts-API/blob/master/_files/kisspng-computer-icons.jpg)

# Projeto-PostTwitter
---------------------
   
 * Introdução
 * Documentação da APIs
 * Teste da API
 * Documentação de como podemos subir uma cópia deste ambiente localmente
 * Logging

INTRODUÇÃO
------------

Projeto criado coletar e vincular os produtos favoritos de nossos clientes Magalu.

 * Para a descrição dos produtos da Api externa magalu:
   https://gist.github.com/Bgouveia/9e043a3eba439489a35e70d1b5ea08ec


DOCUMENTAÇÃO DA APIs
-------------

Swagger no ASP.Net Core
Para rodar sua API e acessar a documentação através do endpoint /swagger
https://localhost:44347/swagger

Exemplo:

![Collection_API](https://github.com/AndrezaSDL/Projeto-FavoritesProducts-API/blob/master/_files/20210720_111609.gif)

Teste da API
-------------

Você poderá solicitar as requisições e vizualizar os recursos pelo Postman.

![alt text](https://github.com/AndrezaSDL/Projeto-FavoritesProducts-API/blob/master/_files/20210720_112909.gif)

Segue abaixo a collection pronta para consumo
<a href="https://github.com/AndrezaSDL/Projeto-FavoritesProducts-API/blob/master/_files/products-favorites-api.postman_collection.json" download="FILENAME">TITLE</a>

## Autenticação e Autorização Jwt

```javascript
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://jpproject-sso:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = "api-secret";
                    options.ApiName = "report-api";
                    options.RoleClaimType = JwtClaimTypes.Role;
                    options.NameClaimType = JwtClaimTypes.Name;
                });
```

DOCUMENTAÇÃO DE COMO PODEMOS SUBIR UMA CÓPIA DESTE AMBIENTE LOCALMENTE
-------------

REQUIRIMENTOS

Para acessar esse projeto precisará ter acesso a essa ferramentas

- VS 2019 - API
- MySQL - Banco de Dados
- Postman - Testar chamadas Api

INSTALAÇÃO(caso for necessário)

```  Install
  -  https://visualstudio.microsoft.com/pt-br/downloads/ (Visual Studio)
  -  https://dev.mysql.com/downloads/installer/ (MySQL)
  -  https://www.postman.com/downloads/ (PostMan)
```

LOGGING
-------------

Para acessar o arquivo de logs acessar a pasta FavoritesProductsAPI\Logs
arquivo exemplo Api.Log-20210720,txt

   

