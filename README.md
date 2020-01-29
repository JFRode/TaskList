# TaskList
Projeto CRUD de tarefas

Os projetos **Tasklist.Application** e **Tasklist.API** possuem dependência do repositório [Tasklist-Contratos](https://github.com/JFRode/Tasklist-Contratos) o qual seria um NuGet. Ao realizar o build do projeto você precisará apontar para a .dll do seu diretório para ambas as referências.

**SwaggerURL:** https://localhost:44369/swagger

## Exemplos

### Requisitando o Token
**Endpoint:** https://localhost:44369/token

**Body:**
```
"83a486ef58ee4b349a432d53ecc23463"
```

### Exemplo de POST
**Endpoint:** https://localhost:44369/api/tasks

**Body:**
```
{
    "Id": "dd76174b-2793-43cb-8c88-a6d94bc2686e",
    "Titulo": "Fazer um bolo",
    "Status": true,
    "Descricao": "Preparar massa e deixar no forno por 30 minutos",
    "DataDeCriacao": "2019-09-27T19:46:06.7534281-03:00",
    "DataDeEdicao": "2019-09-27T19:46:06.7534281-03:00"
}
```

**Bulk edit dos Headers no Postman**:
```
Authorization:bearer "aqui vai o token gerado"
Content-Type:application/json
```

### Exemplo de chamada com OData
No endpoint https://localhost:44369/api/tasks é retornado todas as tasks do banco. Esse endpoint está habilitado para uso do OData como por exemplo **OrderBy**: ```https://localhost:44369/api/tasks?$orderby=titulo asc```

