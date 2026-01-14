# 📝 Gerenciador de Tarefas

Sistema web desenvolvido em **ASP.NET Core MVC** para gerenciamento de tarefas, permitindo criar, visualizar, atualizar e remover tarefas de forma simples e organizada.

---

## 🚀 Funcionalidades

- ✅ Criar tarefas com:
  - Nome
  - Descrição
  - Prioridade
  - Data de entrega
- 📋 Listar tarefas cadastradas
- ✏️ Editar tarefas existentes
- ❌ Remover tarefas
- 🎯 Indicação visual de status:
  - **Pendente** (amarelo)
  - **Concluída** (verde)
  - **Atrasada** (vermelho)
  - **No dia** (laranja)

---

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Razor Pages (CSHTML)
- HTML5
- CSS3
- Bootstrap

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- .NET SDK 7 ou superior
- SQL Server
- Visual Studio ou VS Code

### Passos para execução

```bash
git clone https://github.com/Gabriel-Dev-Muniz/Gestao-de-Tarefas.git
cd Gestao-de-Tarefas
dotnet restore
dotnet ef database update
dotnet run
