## Тестовое задание для компании OCS

### Запуск проекта

- Склонировать репозиторий

  git clone https://github.com/mikhailasalkhanov/OcsTestTask.git

- В файле appsettings.json отредактировать строку подключения, добавив данные вашего сервера Postgres

- Запустить в вашей IDE, либо перейти в папку _Ordering\Ordering.Api_
  и запустить команду

  dotnet run --project Ordering.Api.csproj

Приложение запустится на 5000 порту.

### Маршруты

- Получение заказа
  GET /orders/{id}

- Создание заказа
  POST /orders
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "lines": [
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "qty": 2
  }
  ]
  }

- Редактирование заказа
  PUT /orders/{id}
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "lines": [
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "qty": 2
  },
  {
  "id": "3fa85f66-5717-4562-b7fc-2c963f66afa9",
  "qty": 3
  }
  ]
  }
- Удаление заказа
  DELETE /orders/{id}
