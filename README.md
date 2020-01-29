# TaskList
Projeto de controle de tarefas

**SwaggerURL:** https://localhost:44369/swagger

## Exemplos

### Requisitando o Token
**URL:** https://localhost:44369/token

**Body:**
```
"83a486ef58ee4b349a432d53ecc23463"
```

### Exemplo de POST
**URL:** https://localhost:44369/api/tasks

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

