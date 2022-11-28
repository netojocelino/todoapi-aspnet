# TodoApi

Aplicação desenvolvida seguindo o tutorial de [ASP.NET Core] da Microsoft Learn.

Será criada a API com as seguintes rotas

|             API            |        Descrição       | Corpo da Requisição | Corpo da Resposta|
| ------------------------- | ----------------------- | ------------------- | ---------------- |
| GET `/todoitems`          | Retorna todos os items  |        ---          |  Lista de Items  |
| GET `/todoitems/complete` | Retorna itens completos |        ---          |  Lista de Items  |
| GET `/todoitems/{id}`     | Retorna item pelo `id`  |        ---          |      Item        |        
| POST `/todoitems`         | Insere novo item        |       TODO          |      Item        |  
| PUT `/todoitems/{id}`     | Atualiza item pelo `id` |       TODO          |       ---        |         
| DELETE `/todoitems/{id}`  | Remove item pelo `id`   |        ---          |       ---        |       


```bash
# Para executar a aplicação, por linha de comando, é possíve utilizar
$ dotnet run
```

Para Listar todos os itens cadastrados é possível utilizar
```http
GET /todoitems
```

Para Listar todos os itens cadastrados é possível utilizar
```http
GET /todoitems/complete
```

Para retornar um único item
```http
GET /todoitems/{id}
```

Para cadastrar um novo item
```http
POST /todoitems
Content-Type: application/json

{
  "title": "Estudar C#",
  "isComplete": false
}
```

Para atualizar um item
```http
PUT /todoitems/{id}
Content-Type: application/json

{
    "title": "Jogar Dark Souls II SOTFS",
    "isComplete": false
}
```

Para remover um item da lista
```http
DELETE /todoitems/{id}
```

[ASP.NET Core]: https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio-code