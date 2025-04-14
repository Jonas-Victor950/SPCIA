🛡️ Sistema SPCIA .NET – Auth, AWS, Permissões e Kubernetes
Este projeto é uma app microserviço desenvolvida com .NET 8, com foco em escalabilidade, segurança e arquitetura moderna.
Implementa autenticação JWT, controle de permissões por cargo, integração com serviços AWS, deploy com Docker e Kubernetes, e banco de dados MySQL.

🛡️ Permissionamento baseado em cargos (ex: Admin, Gerente, Usuário) com verificação por middleware

🧩 Arquitetura organizada com:

Models (DTOs e entidades)

Controllers (responsáveis pelas rotas)

Services (regra de negócio centralizada)

☁️ Integração com AWS:

S3 (upload de arquivos)

Secrets Manager (segredos e conexões)

CloudWatch (logs)

🐬 Banco de dados MySQL com EF Core

🧪 Testes unitários com xUnit e Moq

🐳 Deploy com Docker

☸️ Orquestração com Kubernetes

🔄 CI/CD preparado para ambientes reais

🔍 Swagger com autenticação embutida

📦 Stack Técnica
Camada	Tecnologias
Backend	.NET 8, ASP.NET Core, C#
Autenticação	JWT, Middleware customizado
Banco de Dados	MySQL com Entity Framework Core
Cloud	AWS S3, Secrets Manager, CloudWatch
Testes	xUnit, Moq
Deploy	Dockerfile, Docker Compose
Orquestração	Kubernetes (YAML)

🔐 Sistema de Permissões
O sistema de autenticação implementa JWT com suporte a Claims por cargo.
Cada rota pode ser protegida por um [Authorize(Roles = "Admin")], e o middleware personalizado verifica os privilégios com base no token.

Exemplo de cargos:

Admin – Acesso total

Manager – Acesso a gestão e relatórios

User – Acesso restrito às suas próprias informações

☁️ Integração AWS
S3 – Upload de arquivos do usuário, com controle por pasta

Secrets Manager – Armazenamento seguro de strings de conexão

CloudWatch – Log centralizado de eventos, erros e acessos

🧪 Testes Automatizados
Cenários cobertos:

Autenticação e tokens

Validações de entrada

Regras de negócio em services

Integração com banco de dados

🧠 Aprendizados e Propósito
Este projeto foi desenvolvido para aplicar e solidificar práticas modernas de backend profissional com .NET.
Ele aborda desde o ciclo de autenticação segura até deploy e observabilidade em ambiente distribuído.

Mesmo sendo um projeto de prática, o código reflete preocupações reais de produção: segurança, estrutura, testabilidade, escalabilidade e manutenibilidade.

✍️ Autor
Desenvolvido por Jonas Paiva
📧 jonasvictor950@gmail.com
