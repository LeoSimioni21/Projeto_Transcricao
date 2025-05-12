# Projeto_Transcricao
Projeto para transcrever audios utilizando a Api da OpenAi
Este projeto permite enviar arquivos de áudio (ex: `.mp3`) via API e obter a transcrição usando a API Whisper da OpenAI. As transcrições são salvas em um banco de dados SQL Server.

Funcionalidades

- Upload de áudio via Swagger (multipart/form-data) - utilizando o metodo POST
- Integração com OpenAI Whisper
- Armazena transcrições no banco de dados
- Consulta de transcrições anteriores - utilizando o metodo GET
  
Tecnologias

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- OpenAI API (Whisper)
