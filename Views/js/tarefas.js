const API_URL = "http://localhost:5257/api/tarefa";
const token = localStorage.getItem("token");

if (!token) logout();

let filtroAtual = "todas";
let confirmandoConclusao = null;
let confirmandoRemocao = null;

document.addEventListener("DOMContentLoaded", listarTarefas);

// ================= LISTAR =================
async function listarTarefas() {
    const res = await fetch(`${API_URL}?page=1&pageSize=50`, {
        headers: { Authorization: `Bearer ${token}` }
    });

    if (res.status === 401) return logout();

    const data = await res.json();
    const tarefas = data.itens ?? data;

    const lista = document.getElementById("lista");
    lista.innerHTML = "";

    const hoje = new Date().toISOString().split("T")[0];

    tarefas
        .filter(filtrarLista)
        .forEach(t => {
            const li = document.createElement("li");
            const dataEntrega = t.dataEntrega.split("T")[0];

            // Fundo conforme status
            if (t.status === 3) li.classList.add("concluidas");
            else if (new Date(t.dataEntrega) < new Date()) li.classList.add("atrasadas");
            else if (dataEntrega === hoje) li.classList.add("hoje");
            else li.classList.add("pendentes");

            // Bolinha de prioridade
            let classePrioridade = t.prioridade == 1 ? "prioridade-baixa"
                                : t.prioridade == 2 ? "prioridade-media"
                                : "prioridade-alta";

            li.innerHTML = `
                <div class="linha">
                    <span class="titulo-tarefa">${t.titulo}</span>
                    <span class="badge-prioridade ${classePrioridade}"></span>
                </div>
                <div class="linha">
                    <span class="descricao-tarefa">${t.descricao}</span>
                    ${t.status !== 3
                        ? `<button class="btn-concluir" onclick="confirmarConclusao(this, ${t.id})">Concluir</button>`
                        : `<button class="btn-concluir" disabled>Concluída</button>`}
                </div>
                <div class="linha">
                    <span class="data-tarefa">Entrega: ${formatarData(t.dataEntrega)}</span>
                    <button class="btn-remover" onclick="confirmarRemocao(this, ${t.id})">Remover</button>
                </div>
            `;
            lista.appendChild(li);
        });
}

// ================= FILTROS =================
function filtrar(tipo) {
    filtroAtual = tipo;
    listarTarefas();
}

function filtrarLista(t) {
    const atrasada = new Date(t.dataEntrega) < new Date() && t.status !== 3;

    if (filtroAtual === "pendentes") return t.status !== 3 && !atrasada;
    if (filtroAtual === "atrasadas") return atrasada;
    if (filtroAtual === "concluidas") return t.status === 3;

    return true;
}

// ================= CRIAR =================
async function criarTarefa() {
    await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify({
            titulo: titulo.value,
            descricao: descricao.value,
            dataEntrega: dataEntrega.value,
            prioridade: prioridade.value
        })
    });

    limpar();
    listarTarefas();
}

// ================= CONCLUIR =================
function confirmarConclusao(btn, id) {
    if (confirmandoConclusao === id) return concluir(id);

    resetarBotoes();
    confirmandoConclusao = id;

    btn.innerText = "Confirmar";
    btn.classList.add("confirmar");
}

async function concluir(id) {
    await fetch(`${API_URL}/${id}/concluir`, {
        method: "PUT",
        headers: { Authorization: `Bearer ${token}` }
    });

    confirmandoConclusao = null;
    listarTarefas();
}

// ================= REMOVER =================
function confirmarRemocao(btn, id) {
    if (confirmandoRemocao === id) return remover(id);

    resetarBotoes();
    confirmandoRemocao = id;

    btn.innerText = "Confirmar";
    btn.classList.add("confirmar-remover");
}

async function remover(id) {
    await fetch(`${API_URL}/${id}`, {
        method: "DELETE",
        headers: { Authorization: `Bearer ${token}` }
    });

    confirmandoRemocao = null;
    listarTarefas();
}

// ================= UTIL =================
function resetarBotoes() {
    confirmandoConclusao = null;
    confirmandoRemocao = null;

    document.querySelectorAll(".btn-concluir").forEach(b => {
        b.innerText = b.disabled ? "Concluída" : "Concluir";
        b.classList.remove("confirmar");
    });

    document.querySelectorAll(".btn-remover").forEach(b => {
        b.innerText = "Remover";
        b.classList.remove("confirmar-remover");
    });
}

function formatarData(d) {
    return new Date(d).toLocaleDateString("pt-BR");
}

function limpar() {
    titulo.value = "";
    descricao.value = "";
    dataEntrega.value = "";
    prioridade.value = "1";
}

function logout() {
    localStorage.clear();
    window.location.href = "index.html";
}
