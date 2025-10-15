# Sistema de Agendamento

Este é um sistema complexo de agendamento que simula um ambiente de negócios real, desenvolvido em .NET Core MVC. O projeto utiliza SQL avançado com transações para evitar double booking, front-end interativo com JavaScript e calendário, APIs REST, e um webhook em PHP para notificações.

## Funcionalidades

- **Agendamento de Serviços**: Clientes podem agendar horários com profissionais específicos, com validação de disponibilidade e transações SQL para integridade de dados.
- **Calendário de Eventos**: Interface interativa para registrar eventos em datas específicas (feriados, aniversários, etc.), com visualização em calendário e lista.
- **APIs REST**: Endpoints para gerenciar disponibilidade, agendamentos, serviços e eventos.
- **Webhook em PHP**: Notificação automática via PHP (OOP) quando um novo agendamento é criado.
- **Banco de Dados SQLite**: Armazenamento local com Entity Framework Core, incluindo migrations.

## Tecnologias Utilizadas

- **Backend**: .NET Core 9.0 MVC
- **Banco de Dados**: SQLite com Entity Framework Core
- **Front-End**: HTML, CSS, JavaScript (FullCalendar.js)
- **APIs**: RESTful com ASP.NET Core Web API
- **Webhook**: PHP (Orientado a Objetos)
- **Outros**: Bootstrap para UI, jQuery para AJAX

## Pré-requisitos

- .NET Core SDK 9.0 ou superior
- PHP 7.0 ou superior (para o webhook)
- Navegador web moderno

## Instalação e Execução

### 1. Clonar o Repositório
```bash
git clone https://github.com/seu-usuario/sistema-agendamento.git
cd sistema-agendamento
```

### 2. Restaurar Dependências
```bash
dotnet restore
```

### 3. Aplicar Migrations do Banco de Dados
```bash
dotnet ef database update
```

### 4. Executar a Aplicação
```bash
dotnet run
```
A aplicação estará disponível em `http://localhost:5296`.

### 5. Configurar o Webhook (Opcional)
- Navegue até a pasta `WebhookPHP`.
- Execute um servidor PHP local (ex: `php -S localhost:8080`).
- Ajuste a URL no `ApiAgendamentosController.cs` se necessário.

## Como Usar

### Calendário de Eventos
- Abra a página inicial em `http://localhost:5296`.
- Clique em uma data no calendário para preencher automaticamente o campo de data no formulário.
- Preencha o nome e descrição do evento.
- Clique em "Adicionar Evento".
- Eventos aparecem no calendário e na lista lateral.
- Clique em "Excluir" para remover um evento.

### Agendamento (Funcionalidade Original)
- Embora o foco tenha mudado para eventos, o sistema ainda suporta agendamento via APIs.
- Use Postman para testar endpoints como `/api/agendamentos` (POST) com dados de cliente, serviço e disponibilidade.

## Estrutura do Projeto

```
SistemaAgendamento/
├── Controllers/          # Controladores MVC e API
│   ├── ApiAgendamentosController.cs
│   ├── ApiDisponibilidadeController.cs
│   ├── ApiEventosController.cs
│   ├── ApiServicosController.cs
│   └── HomeController.cs
├── Data/
│   └── AppDbContext.cs   # Contexto do Entity Framework
├── Models/               # Modelos de dados
│   ├── Agendamento.cs
│   ├── Cliente.cs
│   ├── Disponibilidade.cs
│   ├── Evento.cs
│   ├── Profissional.cs
│   └── Servico.cs
├── Views/
│   └── Home/
│       └── Index.cshtml  # Página principal com calendário
├── wwwroot/              # Arquivos estáticos (CSS, JS)
├── WebhookPHP/           # Webhook em PHP
│   └── NotificacaoAgendamento.php
├── appsettings.json      # Configurações
├── Program.cs            # Ponto de entrada
└── README.md             # Este arquivo
```

## APIs Disponíveis

- `GET /api/eventos`: Lista todos os eventos.
- `POST /api/eventos`: Cria um novo evento.
- `DELETE /api/eventos/{id}`: Exclui um evento por ID.
- `GET /api/disponibilidade?data=YYYY-MM-DD`: Busca disponibilidade por data.
- `POST /api/agendamentos`: Cria um agendamento (com transação).
- `GET /api/servicos`: Lista serviços disponíveis.

## Contribuição

Contribuições são bem-vindas! Siga estes passos:

1. Fork o projeto.
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`).
3. Commit suas mudanças (`git commit -am 'Adiciona nova feature'`).
4. Push para a branch (`git push origin feature/nova-feature`).
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para detalhes.

## Autor

Desenvolvido por [Seu Nome]. Para dúvidas, entre em contato via [seu-email@example.com].
