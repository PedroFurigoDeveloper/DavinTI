const API_CONTATOS = "https://localhost:7242/api/Contato";
const API_CONTATOS_TELEFONE = "https://localhost:7242/api/Contato/comTelefones";
const API_TELEFONES = "https://localhost:7242/api/Telefone/contato";

const form = document.getElementById('formContato');
const tabela = document.querySelector('#tabelaContatos tbody');
const pesquisa = document.getElementById('pesquisa');
const telefonesDiv = document.getElementById('telefones');
const btnAddTelefone = document.getElementById('addTelefone');

let contatos = [];
let editId = null;
let telefonesOriginais = [];
let telefonesRemovidos = [];

async function carregarContatos() {
  try {
    const res = await fetch(API_CONTATOS_TELEFONE, { mode: "cors" });
    if (!res.ok) throw new Error("Erro ao carregar contatos");
    contatos = await res.json();
    renderizarContatos();
  } catch (err) {
    console.error("Erro ao carregar contatos:", err);
    alert("Falha ao buscar contatos no servidor. Verifique se o backend está rodando.");
  }
}

async function salvarContato(contato) {
  try {
    const metodo = editId ? "PUT" : "POST";
    const url = editId ? `${API_CONTATOS}/${editId}` : API_CONTATOS;

    const body = {
      IdContato: editId || 0,
      Nome: contato.nome,
      Idade: contato.idade,
      Telefones: contato.telefones.map(t => ({
        IdTelefone: t.idTelefone || 0,
        Numero: t.numero
      }))
    };

    const res = await fetch(url, {
      method: metodo,
      mode: "cors",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body)
    });

    const textoErro = await res.text();
    console.warn("Resposta do servidor:", textoErro);
    if (!res.ok) throw new Error(`Erro ao salvar contato (${res.status})`);

    await carregarContatos();
    form.reset();
    telefonesDiv.innerHTML = '';
    editId = null;
    telefonesOriginais = [];
    telefonesRemovidos = [];

  } catch (err) {
    console.error("Erro ao salvar contato:", err);
    alert("Erro ao salvar contato!");
  }
}

async function atualizarTelefones(idContato, telefonesAtuais) {
  const idsOriginais = telefonesOriginais.map(t => t.idTelefone);

  // Atualiza e cria novos
  for (const t of telefonesAtuais) {
    if (t.idTelefone && idsOriginais.includes(t.idTelefone)) {
      await fetch(`${API_TELEFONES}/${t.idTelefone}`, {
        method: "PUT",
        mode: "cors",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          idTelefone: t.idTelefone,
          numero: t.numero,
          idContato
        })
      });
    } else {
      await fetch(API_TELEFONES, {
        method: "POST",
        mode: "cors",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          numero: t.numero,
          idContato
        })
      });
    }
  }

  // Deleta telefones removidos
  for (const id of telefonesRemovidos) {
    await fetch(`${API_TELEFONES}/${id}`, {
      method: "DELETE",
      mode: "cors"
    });
  }

  telefonesRemovidos = [];
}

async function excluirContato(id) {
  if (!confirm("Deseja realmente excluir este contato?")) return;
  try {
    const res = await fetch(`${API_CONTATOS}/${id}`, { method: "DELETE", mode: "cors" });
    if (!res.ok) throw new Error("Erro ao excluir contato");
    await carregarContatos();
  } catch (err) {
    console.error("Erro ao excluir contato:", err);
    alert("Erro ao excluir contato!");
  }
}

function renderizarContatos(filtro = '') {
  tabela.innerHTML = '';
  contatos
    .filter(c =>
      c.nome.toLowerCase().includes(filtro.toLowerCase()) ||
      (c.telefones ?? []).some(t => t.numero.includes(filtro))
    )
    .forEach(c => {
      const row = tabela.insertRow();
      row.innerHTML = `
        <td class="fw-semibold">${c.nome}</td>
        <td>${c.idade ?? '-'}</td>
        <td>${(c.telefones ?? []).map(t => t.numero).join('<br>')}</td>
        <td>
          <button class="btn btn-sm btn-outline-warning me-2" onclick="editar(${c.idContato})">
            <i class="bi bi-pencil-square"></i>
          </button>
          <button class="btn btn-sm btn-outline-danger" onclick="excluirContato(${c.idContato})">
            <i class="bi bi-trash-fill"></i>
          </button>
        </td>
      `;
    });
}

btnAddTelefone.addEventListener('click', () => adicionarCampoTelefone());

function adicionarCampoTelefone(telefone = { numero: '', idTelefone: null }) {
  const div = document.createElement('div');
  div.classList.add('telefone-input');
  div.innerHTML = `
    <input type="text" class="form-control telefone" placeholder="(00) 00000-0000"
           value="${telefone.numero || ''}" maxlength="15" required
           data-id="${telefone.idTelefone || ''}">
    <button type="button" class="btn btn-outline-danger btn-sm remover">
      <i class="bi bi-x-lg"></i>
    </button>
  `;

  div.querySelector('.remover').addEventListener('click', e => {
    const id = div.querySelector('.telefone').dataset.id;
    if (id) telefonesRemovidos.push(parseInt(id));
    div.remove();
  });

  aplicarMascara(div.querySelector('.telefone'));
  telefonesDiv.appendChild(div);
}

function aplicarMascara(input) {
  input.addEventListener('input', e => {
    let valor = e.target.value.replace(/\D/g, '');
    if (valor.length > 11) valor = valor.slice(0, 11);
    if (valor.length <= 10)
      e.target.value = valor.replace(/(\d{2})(\d{4})(\d{0,4})/, '($1) $2-$3');
    else
      e.target.value = valor.replace(/(\d{2})(\d{5})(\d{0,4})/, '($1) $2-$3');
  });
}

form.addEventListener('submit', async e => {
  e.preventDefault();

  const nome = document.getElementById('nome').value.trim();
  const idade = parseInt(document.getElementById('idade').value);
  const telefones = Array.from(document.querySelectorAll('.telefone'))
    .map(t => ({
      numero: t.value.trim(),
      idTelefone: t.dataset.id ? parseInt(t.dataset.id) : null
    }))
    .filter(t => t.numero);

  if (!nome || telefones.length === 0) {
    alert('Preencha todos os campos.');
    return;
  }

  const contato = { nome, idade, telefones };
  await salvarContato(contato);
});

pesquisa.addEventListener('input', e => renderizarContatos(e.target.value));

async function editar(id) {
  const contato = contatos.find(c => c.idContato === id);
  if (!contato) return;

  document.getElementById('nome').value = contato.nome;
  document.getElementById('idade').value = contato.idade ?? '';

  telefonesDiv.innerHTML = '';
  telefonesOriginais = contato.telefones ?? [];
  telefonesOriginais.forEach(t => adicionarCampoTelefone(t));

  if (telefonesDiv.childElementCount === 0) adicionarCampoTelefone();

  editId = id;
  telefonesRemovidos = [];

  window.scrollTo({ top: 0, behavior: 'smooth' });
}

carregarContatos();