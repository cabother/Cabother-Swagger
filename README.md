# Cabother-Swagger

Biblioteca de apoio ao uso de swagger em api's
## Qual sua finalidade?
- Apoiar no versionamento de controllers com uso de swagger (permitindo ter V1, V2, V3 ...etc)
- Permitir diferentes versões de controllers / rotas no mesmo projeto de forma organizada
- Simplificar o uso e configuração do swagger


## Nuget
```
https://www.nuget.org/packages/Cabother.Swagger.UI/
```
## Como configurar?

- Adicionar no projeto do tipo webapi, o pacote do [nuget](https://www.nuget.org/packages/Cabother.Swagger.UI/)

- Executar retore das dependências instaladas através do comando:
  -  `dotnet restore`
- Caso apresente mensagem de *conflito* com biblioteca do swagger ja existente no projeto, pode remover a biblioteca, pois no nuget acima já temos a versão do swagger atualizada.
   
- No seu arquivo `startup.cs` adicionar o seguinte using:
  -  `using Cabother.Swagger.UI.Extensions;` 

- No seu método `ConfigureServices(IServiceCollection services)` adicionar a seguinte informação:
  - ```
    services.AddControllers();
    services.AddDocumentations(Configuration);
    ```

- No seu método `Configure(IApplicationBuilder app, IWebHostEnvironment env)`:
  - Alterar para receber também um terceiro parâmetro:
    - ```
        Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        ```
    - Agora, dentro deste método acima, adicionar a seguinte informação:
      - ```
        app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        app.UseDocumentations(provider);
        ```

- Em suas controllers, agora deverá separá-las por pastas distintas, sendo cada pasta o nome da versão correspondente:
  - ![controller](./Images/controllers-folder.png)

- Agora falta apenas um pequeno passo, em suas respectivas controllers basta adicionar as seguintes informações:
  - ```
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/games")]
    [ApiController, Produces(MediaTypeNames.Application.Json)]
    public class GameController : ControllerBase
    {
        ... conteúdo
    }
    ```
    Lembrando que o `[ApiExplorerSettings(GroupName = "v1")]` será o agrupador no swagger, sendo assim você poderá ter diferentes GroupNames, tais como "V1", "V2"...e assim por diante.

- Agora, basta iniciar seu projeto com o seguinte comando:
  - `dotnet run`

- Seu resultado será visível na porta default 5000 / 5001, exemplo: `https://localhost:5001/api/docs` 
    - ![controller](./Images/swagger-example.png)

Por padrão a documentação está disponível no endpoint `/api/docs` . A documentação pode ser customizada a partir do arquivo `appsettings.json` alterando a seção exemplificada abaixo:

```json
{
  "ApiDocumentation": {
    "AppName": "Nome da sua API",
    "Description": "Descrição da sua API",
    "Owner": "Seu Nome",
    "Email": "seuemail@seudominio.com",
    "DocJson": "/swagger/v1/swagger.json",
    "DocRoute": "api/docs"
  }
}
```



<a href="https://www.buymeacoffee.com/cabother" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a> 