import { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { Pessoa } from "../types";
import {
  listarPessoas,
  criarPessoa,
  deletarPessoa,
  atualizarPessoa
} from "../services/pessoaService";

function Pessoas() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");

  const [modalAberto, setModalAberto] = useState(false);
  const [editandoId, setEditandoId] = useState<string | null>(null);

  const [nomeEdit, setNomeEdit] = useState("");
  const [idadeEdit, setIdadeEdit] = useState("");

  useEffect(() => {
    carregarPessoas();
  }, []);

  async function carregarPessoas() {
    try {
      const data = await listarPessoas();
      setPessoas(data);
    } catch (err) {
      console.error("Erro ao carregar pessoas:", err);
    }
  }

  async function salvar(e: React.FormEvent) {
    e.preventDefault();

    if (!nome || !idade) {
      toast.error("Preencha nome e idade.");
      return;
    }

    try {
      await criarPessoa({
        nome,
        idade: parseInt(idade)
      });
      setNome("");
      setIdade("");
      carregarPessoas();
    } catch (err) {
      console.error("Erro ao criar pessoa:", err);
    }
  }

  function abrirModal(pessoa: Pessoa) {
    setEditandoId(pessoa.id); // GUID como string
    setNomeEdit(pessoa.nome);
    setIdadeEdit(pessoa.idade.toString());
    setModalAberto(true);
  }

  function fecharModal() {
    setModalAberto(false);
    setEditandoId(null);
  }

  async function atualizar() {
    if (!nomeEdit || !idadeEdit) {
      toast.error("Preencha nome e idade.");
      return;
    }

    if (editandoId) {
      try {
        await atualizarPessoa(editandoId, {
          nome: nomeEdit,
          idade: parseInt(idadeEdit)
        });
        fecharModal();
        carregarPessoas();
      } catch (err) {
        console.error("Erro ao atualizar pessoa:", err);
      }
    }
  }

  async function excluir(id: string) {
    const confirmar = window.confirm(
      "Tem certeza que deseja excluir esta pessoa?"
    );
    if (!confirmar) return;

    try {
      await deletarPessoa(id);
      carregarPessoas();
    } catch (err) {
      console.error("Erro ao deletar pessoa:", err);
    }
  }

  return (
    <div className="content">
      <h2>Pessoas</h2>

      {/* Formulário criar */}
      <div className="form-card">
        <h3>Nova Pessoa</h3>
        <form onSubmit={salvar}>
          <div className="form-group">
            <input
              placeholder="Nome"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
            />
            <input
              type="number"
              placeholder="Idade"
              value={idade}
              onChange={(e) => setIdade(e.target.value)}
            />
            <button type="submit">Salvar</button>
          </div>
        </form>
      </div>

      {/* Tabela */}
      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Idade</th>
            <th style={{ width: 200 }}>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.length > 0 ? (
            pessoas.map((p) => (
              <tr key={p.id}>
                <td>{p.nome}</td>
                <td>{p.idade}</td>
                <td>
                  <div className="actions">
                    <button className="btn-edit" onClick={() => abrirModal(p)}>
                      Editar
                    </button>
                    <button className="btn-delete" onClick={() => excluir(p.id)}>
                      Excluir
                    </button>
                  </div>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={3} style={{ textAlign: "center", color: "#6b7280" }}>
                Nenhuma pessoa cadastrada
              </td>
            </tr>
          )}
        </tbody>
      </table>

      {/* Modal editar */}
      {modalAberto && (
        <div className="modal-overlay">
          <div className="modal">
            <h3>Editar Pessoa</h3>
            <input
              placeholder="Nome"
              value={nomeEdit}
              onChange={(e) => setNomeEdit(e.target.value)}
            />
            <input
              type="number"
              placeholder="Idade"
              value={idadeEdit}
              onChange={(e) => setIdadeEdit(e.target.value)}
            />
            <div className="actions">
              <button className="btn-edit" onClick={atualizar}>
                Atualizar
              </button>
              <button className="btn-delete" onClick={fecharModal}>
                Cancelar
              </button>
            </div>
          </div>
        </div>
      )}
    {/* Toastify Container */}
    <ToastContainer
      position="top-right"
      autoClose={3000}
      hideProgressBar={false}
      newestOnTop={false}
      closeOnClick
      rtl={false}
      pauseOnFocusLoss
      draggable
      pauseOnHover
    />
    </div>
  );
}

export default Pessoas;