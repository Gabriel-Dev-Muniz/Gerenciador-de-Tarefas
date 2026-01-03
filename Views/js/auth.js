const API_AUTH = "http://localhost:5257/api/auth";

// ================= LOGIN =================
async function login() {
    const email = document.getElementById("email").value.trim();
    const senha = document.getElementById("senha").value.trim();
    const msg = document.getElementById("msg");
    const btn = document.getElementById("btnLogin");

    msg.innerText = "";
    msg.className = "";

    if (!email || !senha) {
        msg.innerText = "Preencha email e senha";
        msg.classList.add("erro");
        return;
    }

    btn.disabled = true;
    btn.innerText = "Entrando...";

    try {
        const res = await fetch(`${API_AUTH}/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, senha })
        });

        if (!res.ok) throw new Error();

        const data = await res.json();

        localStorage.setItem("token", data.token);
        localStorage.setItem("usuario", JSON.stringify(data.usuario));

        // ✅ Ajustado para nova estrutura
        window.location.href = "views/html/tarefas.html";

    } catch {
        msg.innerText = "Email ou senha inválidos";
        msg.classList.add("erro");
        btn.disabled = false;
        btn.innerText = "Entrar";
    }
}

// ================= CADASTRO =================
async function cadastrar() {
    const nome = document.getElementById("nome").value.trim();
    const email = document.getElementById("email").value.trim();
    const senha = document.getElementById("senha").value.trim();
    const msg = document.getElementById("msg");
    const btn = document.getElementById("btnCadastro");

    msg.innerText = "";
    msg.className = "";

    if (!nome || !email || !senha) {
        msg.innerText = "Preencha todos os campos";
        msg.classList.add("erro");
        return;
    }

    btn.disabled = true;
    btn.innerText = "Cadastrando...";

    try {
        const res = await fetch(`${API_AUTH}/register`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ nome, email, senha })
        });

        if (!res.ok) throw new Error();

        msg.innerText = "Cadastro realizado com sucesso!";
        msg.classList.add("sucesso");

        setTimeout(() => {
            window.location.href = "../../index.html";
        }, 1500);

    } catch {
        msg.innerText = "Erro ao cadastrar";
        msg.classList.add("erro");
        btn.disabled = false;
        btn.innerText = "Cadastrar";
    }
}
