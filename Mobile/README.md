# 📱 **ScoreView**  

Um aplicativo mobile desenvolvido em **.NET MAUI** para avaliação de score financeiro, criado como Projeto Integrador do curso de Desenvolvimento de Software.  

---

## 🚀 **Funcionalidades**  
✔ **Cadastro de Usuário**: Nome e sexo.  
✔ **Questionário Dinâmico**: 10 perguntas sobre finanças pessoais.    
✔ **Integração com API**: Envio dos dados para cálculo do score.  
✔ **Resultado Visual**: Exibição do score após teste.  

---

## 🛠 **Tecnologias e Bibliotecas**  
- **Frontend**: .NET MAUI (XAML + C#)  
- **Backend**: API REST (disponível em `five-dsm-pi-backend.onrender.com`)  
- **Armazenamento**:    
  - Nuvem: API externa.  
- **Padrão Arquitetural**: MVVM (Model-View-ViewModel).  

---

## 📂 **Estrutura do Projeto**  
```plaintext
Mobile/
├── Models/            # Entidades (Usuario, Pergunta, Resposta, etc.)
├── ViewModels/        # Lógica das telas (TesteViewModel, etc.)
├── Views/             # Telas XAML (CadastroPage, TestePage, etc.)
├── Services/          # Serviços (ApiService, LocalStorageService)
├── Resources/         # Estilos, imagens e fonts
└── MauiProgram.cs     # Configuração inicial do app
```

---

## 🔧 **Pré-requisitos**  
- Visual Studio 2022 (com workload **.NET MAUI**).  
- Android SDK (para emulação ou dispositivo físico).  
- Acesso à API (URL base: `https://five-dsm-pi-backend.onrender.com`).  

---

## 🏁 **Como Executar**  
1. Clone o repositório:  
   ```bash
   git clone [URL_DO_REPO]
   ```
2. Abra o projeto no **Visual Studio 2022**.  
3. Configure o arquivo `MauiProgram.cs` com a URL da sua API (se necessário).  
4. Execute em um **emulador Android** ou dispositivo físico.  

---


## 📝 **Regras de Negócio**  
- O score é calculado com base nas respostas do questionário.  
- Dados são  sincronização com a API.   

---

## 🤝 **Contribuidores**  
- Allison Rodrigues de Paula e Silva  
- Paula Cristina Abib Teixeira  
- Pauliane Ester Silveira  
- Samir Lopes Rosa  

---

## 📄 **Licença**  
Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para detalhes.  

---


<p align="center">
  ⭐️ <strong>Deixe sua estrela no repositório se gostou do projeto!</strong> ⭐️
</p>


---

🔗 **Links Úteis**:  
- [Documentação .NET MAUI](https://learn.microsoft.com/pt-br/dotnet/maui/)  
- [Repositório da API](https://github.com/seu-repo/backend)  

--- 

